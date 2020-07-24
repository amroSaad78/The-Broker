using Apartment.API.Extensions;
using Apartment.API.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Apartment.API.Infrastructure
{
    public class ApartmentContextSeed
    {
        public async Task SeedAsync(ApartmentContext context, IWebHostEnvironment env, IOptions<ApartmentSettings> settings, ILogger<ApartmentContextSeed> logger)
        {
            var policy = CreatePolicy(logger, nameof(ApartmentContextSeed));

            await policy.ExecuteAsync(async () =>
            {
                var useCustomizationData = settings.Value.UseCustomizationData;
                var contentRootPath = env.ContentRootPath;

                if (!context.Bedroom.Any())
                {
                    await context.Bedroom.AddRangeAsync(useCustomizationData
                        ? GetDataFromFile(contentRootPath, logger, GetPreconfiguredBedrooms, CreateBedroom)
                        : GetPreconfiguredBedrooms());

                    await context.SaveChangesAsync();
                }

                if (!context.Country.Any())
                {
                    await context.Country.AddRangeAsync(useCustomizationData
                        ? GetDataFromFile(contentRootPath, logger, GetPreconfiguredCountries, CreateCountry)
                        : GetPreconfiguredCountries());

                    await context.SaveChangesAsync();
                }

                if (!context.Furniture.Any())
                {
                    await context.Furniture.AddRangeAsync(useCustomizationData
                        ? GetDataFromFile(contentRootPath, logger, GetPreconfiguredFurnishings, CreateFurniture)
                        : GetPreconfiguredFurnishings());

                    await context.SaveChangesAsync();                    
                }

                if (!context.Purpose.Any())
                {
                    await context.Purpose.AddRangeAsync(useCustomizationData
                        ? GetDataFromFile(contentRootPath, logger, GetPreconfiguredPurposes, CreatePurpose)
                        : GetPreconfiguredPurposes());

                    await context.SaveChangesAsync();
                }

                if (!context.Period.Any())
                {
                    await context.Period.AddRangeAsync(useCustomizationData
                        ? GetDataFromFile(contentRootPath, logger, GetPreconfiguredPeriods, CreatePeriod)
                        : GetPreconfiguredPeriods());

                    await context.SaveChangesAsync();
                }
            });
        }

        private IEnumerable<T> GetDataFromFile<T>(string contentRootPath, ILogger<ApartmentContextSeed> logger,
                                                            Func<IEnumerable<T>> preconfiguredData, Func<string, T> selector)
        {
            string entity = typeof(T).Name;
            string csvFile = Path.Combine(contentRootPath, "Setup", $"{entity}.csv");
            if (!File.Exists(csvFile))
            {
                return preconfiguredData();
            }

            return File.ReadAllLines(csvFile)
                                        .Skip(1) // skip header row
                                        .SelectTry(selector)
                                        .OnCaughtException(ex => { logger.LogError(ex, "EXCEPTION ERROR: {Message}", ex.Message); return default; })
                                        .Where(x => x != null);
        }

        
        private Bedrooms CreateBedroom(string bedroomsCount)
        {
            bedroomsCount = bedroomsCount.Trim('"').Trim();

            if (String.IsNullOrEmpty(bedroomsCount))
            {
                throw new Exception("Bedroom is empty");
            }

            return new Bedrooms
            {
                BedroomsCount = bedroomsCount
            };
        }

        private Countries CreateCountry(string country)
        {
            country = country.Trim('"').Trim();

            if (String.IsNullOrEmpty(country))
            {
                throw new Exception("Country is empty");
            }

            return new Countries
            {
                Country = country
            };
        }

        private Furnishings CreateFurniture(string furniture)
        {
            furniture = furniture.Trim('"').Trim();

            if (String.IsNullOrEmpty(furniture))
            {
                throw new Exception("Furniture is empty");
            }

            return new Furnishings
            {
                FurnitureType = furniture
            };
        }

        private Purpose CreatePurpose(string purpose)
        {
            purpose = purpose.Trim('"').Trim();

            if (String.IsNullOrEmpty(purpose))
            {
                throw new Exception("Purpose is empty");
            }

            return new Purpose
            {
                PurposeType = purpose
            };
        }

        private Periods CreatePeriod(string period)
            {
                period = period.Trim('"').Trim();

                if (String.IsNullOrEmpty(period))
                {
                    throw new Exception("Period is empty");
                }

                return new Periods
                {
                    Period = period
                };
            }

        private IEnumerable<Bedrooms> GetPreconfiguredBedrooms() => new List<Bedrooms>()
            {
                new Bedrooms() { BedroomsCount = "Studio"},
                new Bedrooms() { BedroomsCount = "1 Bedroom"},
                new Bedrooms() { BedroomsCount = "2 Bedrooms"},
                new Bedrooms() { BedroomsCount = "3 Bedrooms"},
                new Bedrooms() { BedroomsCount = "4 Bedrooms"},
                new Bedrooms() { BedroomsCount = "5 Bedrooms"}
            };

        private IEnumerable<Countries> GetPreconfiguredCountries() => new List<Countries>()
            {
                new Countries() { Country = "Egypt"},
                new Countries() { Country = "UAE"},
                new Countries() { Country = "Qatar"},
                new Countries() { Country = "Kuwait"},
                new Countries() { Country = "Saudi"},
            };

        private IEnumerable<Furnishings> GetPreconfiguredFurnishings() => new List<Furnishings>()
            {
                new Furnishings() { FurnitureType = "All furnishings"},
                new Furnishings() { FurnitureType = "Furnished"},
                new Furnishings() { FurnitureType = "Unfurnished"},
                new Furnishings() { FurnitureType = "Partly furnished"}
            };

        private IEnumerable<Purpose> GetPreconfiguredPurposes() => new List<Purpose>()
            {
                new Purpose() { PurposeType = "Sale"},
                new Purpose() { PurposeType = "Rent"}
            };

        private IEnumerable<Periods> GetPreconfiguredPeriods() => new List<Periods>()
            {
                new Periods() { Period = "Daily"},
                new Periods() { Period = "Monthly"},
                new Periods() { Period = "Quarterly"},
                new Periods() { Period = "Yearly"}
            };

        private AsyncRetryPolicy CreatePolicy(ILogger<ApartmentContextSeed> logger, string prefix, int retries = 3)
        {
            return Policy.Handle<SqlException>().
                WaitAndRetryAsync(
                    retryCount: retries,
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                    onRetry: (exception, timeSpan, retry, ctx) =>
                    {
                        logger.LogWarning(exception, "[{prefix}] Exception {ExceptionType} with message {Message} detected on attempt {retry} of {retries}", prefix, exception.GetType().Name, exception.Message, retry, retries);
                    }
                );
        }
    }
}
