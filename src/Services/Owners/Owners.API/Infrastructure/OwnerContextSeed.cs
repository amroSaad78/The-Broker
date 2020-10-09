using Owners.API.Extensions;
using Owners.API.Model;
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
using System.Text.RegularExpressions;

namespace Owners.API.Infrastructure
{
    public class OwnerContextSeed
    {
        public async Task SeedAsync(OwnerContext context, IWebHostEnvironment env, IOptions<AppSettings> settings, ILogger<OwnerContextSeed> logger)
        {
            var policy = CreatePolicy(logger, nameof(OwnerContextSeed));

            await policy.ExecuteAsync(async () =>
            {
                var useCustomizationData = settings.Value.UseCustomizationData;
                var contentRootPath = env.ContentRootPath;

                if (!context.Owners.Any())
                {
                    await context.Owners.AddRangeAsync(useCustomizationData
                        ? GetDataFromFile(contentRootPath, logger)
                        : GetPreconfiguredOwners());

                    await context.SaveChangesAsync();
                }
            });
        }

        private IEnumerable<Owner> GetDataFromFile(string contentRootPath, ILogger<OwnerContextSeed> logger)
        {
            string csvFilePath = Path.Combine(contentRootPath, "Setup", "Owner.csv");

            if (!File.Exists(csvFilePath))
            {
                return GetPreconfiguredOwners();
            }

            string[] csvheaders;
            try
            {
                string[] requiredHeaders = {"FirstName", "LastName", "Email", "Mobile", "City", "Company", "Address", "ZIP"};
                csvheaders = GetHeaders(csvFilePath, requiredHeaders);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "EXCEPTION ERROR: {Message}", ex.Message);
                return GetPreconfiguredOwners();
            }

            return File.ReadAllLines(csvFilePath)
                        .Skip(1) // skip header row
                        .Select(row => Regex.Split(row, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)"))
                        .SelectTry(column => CreateOwner(column, csvheaders))
                        .OnCaughtException(ex => { logger.LogError(ex, "EXCEPTION ERROR: {Message}", ex.Message); return null; })
                        .Where(x => x != null);
        }


        private Owner CreateOwner(string[] column, string[] headers)
        {
            if (column.Count() != headers.Count())
            {
                throw new Exception($"column count '{column.Count()}' not the same as headers count'{headers.Count()}'");
            }

            var owner = new Owner()
            {
                FirstName = column[Array.IndexOf(headers, "FirstName")].Trim('"').Trim(),
                LastName = column[Array.IndexOf(headers, "LastName")].Trim('"').Trim(),
                Email = column[Array.IndexOf(headers, "Email")].Trim('"').Trim(),
                Mobile = column[Array.IndexOf(headers, "Mobile")].Trim('"').Trim(),
                City = column[Array.IndexOf(headers, "City")].Trim('"').Trim(),
                Company = column[Array.IndexOf(headers, "Company")].Trim('"').Trim(),
                Address = column[Array.IndexOf(headers, "Address")].Trim('"').Trim(),
                ZIP = column[Array.IndexOf(headers, "ZIP")].Trim('"').Trim(),

            };

            return owner;
        }

        private string[] GetHeaders(string csvfile, string[] requiredHeaders)
        {
            string[] csvheaders = File.ReadLines(csvfile).First().Split(',');

            if (csvheaders.Count() < requiredHeaders.Count())
            {
                throw new Exception($"requiredHeader count '{ requiredHeaders.Count()}' is bigger then csv header count '{csvheaders.Count()}' ");
            }

            foreach (var requiredHeader in requiredHeaders)
            {
                if (!csvheaders.Contains(requiredHeader))
                {
                    throw new Exception($"does not contain required header '{requiredHeader}'");
                }
            }

            return csvheaders;
        }

        private IEnumerable<Owner> GetPreconfiguredOwners() => new List<Owner>()
        {
            new Owner() {FirstName="Amro", LastName="Saad", Email="amrosaad78@gmail.com", Company="Microsoft", Address="36 Abou baker elsdeeq st.", Mobile="+201014026468", City="Mansourah", ZIP="35511"},
            new Owner() {FirstName="Yousef", LastName="Amro", Email="yousef@gmail.com", Company="Google", Address="Abou baker elsdeeq st.", Mobile="+201014026468", City="Mansourah", ZIP="35511"}
        };

        private AsyncRetryPolicy CreatePolicy(ILogger<OwnerContextSeed> logger, string prefix, int retries = 3)
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
