﻿namespace WebMVC
{
    public class AppSettings
    {
        //public Connectionstrings ConnectionStrings { get; set; }
        public string ApartmentUrl { get; set; }        
        public string ApartmentAggUrl { get; set; }        
        public string OwnersUrl { get; set; }        
        public Logging Logging { get; set; }
        public bool UseCustomizationData { get; set; }
        public long FileSizeLimit { get; set; }
    }

    public class Connectionstrings
    {
        public string DefaultConnection { get; set; }
    }

    public class Logging
    {
        public bool IncludeScopes { get; set; }
        public Loglevel LogLevel { get; set; }
    }

    public class Loglevel
    {
        public string Default { get; set; }
        public string System { get; set; }
        public string Microsoft { get; set; }
    }

    public class Uris
    {
        public static string GetUrl(string path, string type) => $"{path}/ap/api/v1/apartment/{type}";
    }
}