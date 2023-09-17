using CsvHelper;
using CsvHelper.Configuration.Attributes;
using Microsoft.Win32;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Dispatcher
{

    public class UserProfile
    {

        public string Env { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public int UID { get; set; }

    }

    public class UserPreferences
    {

        public const string SERVICE_DISABLE = "Disable";
        public const string SERVICE_LEGACY = "Legacy";
        public const string SERVICE_API_WB = "API with Write Back";

        public int PrinterID { get; set; }
        public string ServiceDPD { get; set; }
        public string ServiceAPC { get; set; }
        public string ServiceDL { get; set; }

    }

    public class DPDPrinterPreferences
    {

        public string PrinterName { get; set; }
        public string PrintFormat { get; set; }
        public string Template { get; set; }

    }

    public class APCPrinterPreferences
    {
        [JsonPropertyName("account_number")]
        public string AccountNumber { get; set; }

        [JsonPropertyName("api_endpoint")]
        public string APIEndpoint { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("label_printer_name")]
        public string LabelPrinterName { get; set; }

        [JsonPropertyName("printing_format")]
        public string PrintingFormat { get; set; }
    }

    public class DLPreferences
    {
        [JsonPropertyName("api_endpoint")]
        public string APIEndpoint { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("label_printer_name")]
        public string LabelPrinterName { get; set; }

    }

    public class SystemDataSource
    {

        private readonly RegistryKey companyKey;

        public SystemDataSource()
        {
            // Create a new key under HKEY_LOCAL_MACHINE\Software as MCBInc
            using (var softwareKey = Registry.CurrentUser.OpenSubKey("Software", true))
            {
                companyKey = softwareKey?.CreateSubKey("Chameleon Codewing Ltd");
                if (companyKey !=  null)
                {
                    companyKey = companyKey.CreateSubKey("Dispatcher");
                }
            }
        }

        public void StoreUserProfile(UserProfile userProfile)
        {
            string profileJson = JsonSerializer.Serialize(userProfile);
            companyKey?.SetValue("UserProfile", profileJson);
        }

        public UserProfile RetrieveUserProfile()
        {
            string userProfileJson = (string)companyKey?.GetValue("UserProfile");
            UserProfile userProfile = null;
            if (userProfileJson != null)
            {
                userProfile = JsonSerializer.Deserialize<UserProfile>(userProfileJson ?? string.Empty);
            }
            return userProfile;
        }

        public void StoreUserPreferences(UserPreferences userPreferences)
        {
            string profileJson = JsonSerializer.Serialize(userPreferences);
            companyKey?.SetValue("UserPreferences", profileJson);
        }

        public UserPreferences RetrieveUserPreferences()
        {
            string userPreferencesJson = (string)companyKey?.GetValue("UserPreferences");
            UserPreferences userPreferences = null;
            if (userPreferencesJson != null)
            {
                userPreferences = JsonSerializer.Deserialize<UserPreferences>(userPreferencesJson ?? string.Empty);
            }
            return userPreferences;
        }


        public void StoreDPDPrinterPreferences(DPDPrinterPreferences dpdPrinterPreferences)
        {
            string profileJson = JsonSerializer.Serialize(dpdPrinterPreferences);
            companyKey?.SetValue("DPDPrinterPreferences", profileJson);
        }

        public DPDPrinterPreferences RetrieveDPDPrinterPreferences()
        {
            string dpdPrinterPreferencesJson = (string)companyKey?.GetValue("DPDPrinterPreferences");
            DPDPrinterPreferences dpdPrinterPreferences = null;
            if (dpdPrinterPreferencesJson != null)
            {
                dpdPrinterPreferences = JsonSerializer.Deserialize<DPDPrinterPreferences>(dpdPrinterPreferencesJson ?? string.Empty);
            }
            return dpdPrinterPreferences;
        }

        internal void StoreAPCPrinterPreferences(APCPrinterPreferences apcPrinterPreferences)
        {
            string profileJson = JsonSerializer.Serialize(apcPrinterPreferences);
            companyKey?.SetValue("APCPrinterPreferences", profileJson);
        }

        internal APCPrinterPreferences RetrieveAPCPrinterPreferences()
        {
            string apcPrinterPreferencesJson = (string)companyKey?.GetValue("APCPrinterPreferences");
            APCPrinterPreferences apcPrinterPreferences = null;
            if (apcPrinterPreferencesJson != null)
            {
                apcPrinterPreferences = JsonSerializer.Deserialize<APCPrinterPreferences>(apcPrinterPreferencesJson ?? string.Empty);
            }
            return apcPrinterPreferences;
        }

        public void ClearUserProfile()
        {
            companyKey?.DeleteValue("UserProfile");
        }

        public void StoreLastUsedPath(string path)
        {
            companyKey?.SetValue("Path", path);
        }


        private static Guid FolderDownloads = new Guid("374DE290-123F-4565-9164-39C4925E467B");
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        private static extern int SHGetKnownFolderPath(ref Guid id, int flags, IntPtr token, out IntPtr path);

        public string RetrieveLastUsedPath()
        {
            string path = (string)(companyKey?.GetValue("Path"));
            if (path is null)
            {
                path = GetDownloadsPath();
            }
            return path;
        }

        private string GetDownloadsPath()
        {            

            IntPtr pathPtr = IntPtr.Zero;

            try
            {
                SHGetKnownFolderPath(ref FolderDownloads, 0, IntPtr.Zero, out pathPtr);
                return Marshal.PtrToStringUni(pathPtr);
            }
            finally
            {
                Marshal.FreeCoTaskMem(pathPtr);
            }
        }

        internal void StoreDLPreferences(DLPreferences dlPreferences)
        {
            string dlPreferencesJson = JsonSerializer.Serialize(dlPreferences);
            companyKey?.SetValue("DLPreferences", dlPreferencesJson);
        }

        internal DLPreferences RetrieveDLPreferences()
        {
            string dlPreferencesJson = (string)companyKey?.GetValue("DLPreferences");
            DLPreferences dlPreferences = null;
            if (dlPreferencesJson != null)
            {
                dlPreferences = JsonSerializer.Deserialize<DLPreferences>(dlPreferencesJson ?? string.Empty);
            }
            return dlPreferences;
        }
    }

    class Printer
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("print_system_name")]
        public string SystemName { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    class PrinterQueue
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }
        
        [JsonPropertyName("print_system_name")]
        public string PrintSystemName { get; set; }

        [JsonPropertyName("origin")]
        public string Origin { get; set; }

        [JsonPropertyName("printer_name")]
        public string PrinterName { get; set; }
        
        [JsonPropertyName("file")]
        public string File { get; set; }
        
        [JsonPropertyName("file_name")]
        public string FileName { get; set; }
        
        [JsonPropertyName("file_type")]
        public string FileType { get; set; }

        public override string ToString() {
            return $"ID={ID}, PrinterName={PrinterName}";
        }
    }

    class LoginResult
    {
        [JsonPropertyName("uid")]
        public int UID { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("display_name")]
        public string Name { get; set; }
    }

    class ErrorResult
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }

    class ClosePrinterQueue
    {
        internal ClosePrinterQueue(string state)
        {
            this.State = state;
        }

        internal ClosePrinterQueue(string state, string exceptionMessage)
        {
            this.State = state;
            this.ExceptionMessage = exceptionMessage;
        }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("exception_message")]
        public string ExceptionMessage { get; set; }

        public static ClosePrinterQueue NewPrintedInstance()
        {
            return new ClosePrinterQueue("printed", "");
        }

        public static ClosePrinterQueue NewExceptionInstance(string exceptionMessage)
        {
            return new ClosePrinterQueue("exception", exceptionMessage);
        }
    }

    class AuthDataSource
    {
        private readonly string _url;
        private readonly string _apiKey;

        public AuthDataSource(Environment environment)
        {
            switch (environment)
            {
                case Environment.Production:
                    this._url = "https://77q7jaqq13.execute-api.eu-west-2.amazonaws.com/prod/v1";
                    this._apiKey = "";
                    break;
                case Environment.Test:
                    this._url = "https://rwlzc0ciz1.execute-api.eu-west-2.amazonaws.com/dev/v1";
                    this._apiKey = "";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(environment), environment, null);
            }
        }

        public LoginResult Login(string username, string password)
        {            
            var client = new RestClient(_url);
            client.AddDefaultHeader("x-api-key", _apiKey)
                .AddDefaultQueryParameter("username", username)
                .AddDefaultQueryParameter("password", password);
            var request = new RestRequest($"/auth/login", (Method)DataFormat.Json);
            var response = client.Get(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonSerializer.Deserialize<LoginResult>(response.Content);
            }
            else
            {
                throw new Exception(JsonSerializer.Deserialize<ErrorResult>(response.Content).Message);
            }

        }
    }

    class PrinterDataSource
    {
        private string url;
        private string apiKey;
        private string uid;
        private string password;

        public PrinterDataSource(Environment environment, string uid, string password)
        {
            switch (environment)
            {
                case Environment.Production:
                    this.url = "https://77q7jaqq13.execute-api.eu-west-2.amazonaws.com/prod/v1";
                    this.apiKey = "PbDryNbvZl72u5FbK9gdZ5IbDwZXb5yVag4tDL7l";
                    break;
                case Environment.Test:
                    url = "https://rwlzc0ciz1.execute-api.eu-west-2.amazonaws.com/dev/v1";
                    apiKey = "C1AmN0J00Z6QHUwqVrLHFvUZ9i6Hik3eXWxCx4h0";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(environment), environment, null);
            }

            this.uid = uid;
            this.password = password;
        }

        public List<Printer> FetchPrinters()
        {
            var client = new RestClient(url);
            client.AddDefaultHeader("x-api-key", apiKey)
                    .AddDefaultQueryParameter("uid", uid)
                    .AddDefaultQueryParameter("password", password);

            var returnValue = new List<Printer>();
            try
            {
                var request = new RestRequest("wh/printer/", Method.Get);
                var response = client.Get(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    returnValue = JsonSerializer.Deserialize<List<Printer>>(response.Content);
                }
            }
            catch (Exception)
            {
                returnValue = new List<Printer>();
            }
            return returnValue;
        }
    }

    class PrinterQueueDataSource
    {
        private string url;
        private string apiKey;        
        private string uid;
        private string password;

        public string PrinterID { get; set; }

        public PrinterQueueDataSource(Environment environment, string printerID, string uid, string password)
        {
            switch (environment)
            {
                case Environment.Production:
                    this.url = "https://77q7jaqq13.execute-api.eu-west-2.amazonaws.com/prod/v1";
                    this.apiKey = "PbDryNbvZl72u5FbK9gdZ5IbDwZXb5yVag4tDL7l";
                    break;
                case Environment.Test:
                    url = "https://rwlzc0ciz1.execute-api.eu-west-2.amazonaws.com/dev/v1";
                    apiKey = "C1AmN0J00Z6QHUwqVrLHFvUZ9i6Hik3eXWxCx4h0";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(environment), environment, null);
            }

            this.uid = uid;
            this.password = password;
            this.PrinterID = printerID;
        }


        public List<PrinterQueue> FetchUnPrint()
        {                        
            var q = WebUtility.UrlEncode($"[[\"state\",\"=\",\"unprint\"],[\"printer\",\"=\",{this.PrinterID}]]");
            var client = new RestClient(url);
            client.AddDefaultHeader("x-api-key", apiKey)
                    .AddDefaultQueryParameter("uid", uid)
                    .AddDefaultQueryParameter("password", password)
                    .AddDefaultQueryParameter("q", q ?? throw new InvalidOperationException())
                    .AddDefaultQueryParameter("view", "list")
                    .AddDefaultQueryParameter("limit", "20");

            var returnValue = new List<PrinterQueue>();
            try
            {            
                var request = new RestRequest("wh/printer-queue/", Method.Get);                
                var response = client.Get(request);                       
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    returnValue = JsonSerializer.Deserialize<List<PrinterQueue>>(response.Content);                 
                }                
            }
            catch (Exception)
            {
                returnValue = new List<PrinterQueue>();
            }
            return returnValue;            
        }

        internal string GetLabel(PrinterQueue job, string format, string template)
        {
            var client = new RestClient(url);
            client.AddDefaultHeader("x-api-key", apiKey)
                    .AddDefaultHeader("Accept", format)
                    .AddDefaultQueryParameter("uid", uid)
                    .AddDefaultQueryParameter("password", password)
                    .AddDefaultQueryParameter("template", template);
            string _returnValue;
            try
            {                
                var request = new RestRequest($"wh/printer-queue/{job.ID}/label", Method.Get);                
                var response = client.Get(request);
                
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    _returnValue = response.Content;
                }
                else
                {
                    throw new Exception(response.Content);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }            
            return _returnValue;
        }

        private string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        internal string GetAPCLabel(PrinterQueue job, APCPrinterPreferences printerPreferences)
        {
            string strPrinterPreferences = JsonSerializer.Serialize(printerPreferences);

            var client = new RestClient(url);
            client.AddDefaultHeader("x-api-key", apiKey)
                    .AddDefaultQueryParameter("uid", uid)
                    .AddDefaultQueryParameter("password", password)
                    .AddDefaultQueryParameter("service", "apc")
                    .AddDefaultQueryParameter("service_info", this.Base64Encode(strPrinterPreferences));
            string _returnValue;
            try
            {
                string origin = job.Origin.Replace("/", "%2F");
                var request = new RestRequest($"wh/do/{origin}/label", (Method)DataFormat.Json);

                var response = client.Get(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var values = JsonSerializer.Deserialize<Dictionary<string, string>>(response.Content);
                    _returnValue = values["label_content"];
                }
                else
                {
                    var values = JsonSerializer.Deserialize<Dictionary<string, string>>(response.Content);
                    throw new Exception(values["message"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _returnValue;
        }

        internal void CloseJob(PrinterQueue job)
        {
            var client = new RestClient(url);
            client.AddDefaultHeader("x-api-key", apiKey)
                    .AddDefaultQueryParameter("uid", uid)
                    .AddDefaultQueryParameter("password", password);                   
            var body = JsonSerializer.Serialize<ClosePrinterQueue>(ClosePrinterQueue.NewPrintedInstance());
            var request = new RestRequest($"wh/printer-queue/{job.ID}", (Method)DataFormat.Json).AddJsonBody(body);
            var response = client.Put(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(response.Content);
            }
        }

        internal void ExceptionJob(PrinterQueue job, string exceptionMessage)
        {
            var client = new RestClient(url);
            client.AddDefaultHeader("x-api-key", apiKey)
                .AddDefaultQueryParameter("uid", uid)
                .AddDefaultQueryParameter("password", password);
            var body = JsonSerializer.Serialize<ClosePrinterQueue>(ClosePrinterQueue.NewExceptionInstance(exceptionMessage));
            var request = new RestRequest($"wh/printer-queue/{job.ID}", (Method)DataFormat.Json).AddJsonBody(body);
            var response = client.Put(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(response.Content);
            }
        }

        internal string GetDLLabel(PrinterQueue job, DLPreferences preferences)
        {
            string strPrinterPreferences = JsonSerializer.Serialize(preferences);

            var client = new RestClient(url);
            client.AddDefaultHeader("x-api-key", apiKey)
                    .AddDefaultQueryParameter("uid", uid)
                    .AddDefaultQueryParameter("password", password)
                    .AddDefaultQueryParameter("service", "dl")
                    .AddDefaultQueryParameter("service_info", this.Base64Encode(strPrinterPreferences));
            string _returnValue;
            try
            {
                string origin = job.Origin.Replace("/", "%2F");
                var request = new RestRequest($"wh/do/{origin}/label", (Method)DataFormat.Json);

                var response = client.Get(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var values = JsonSerializer.Deserialize<Dictionary<string, string>>(response.Content);
                    _returnValue = values["label_content"];
                }
                else
                {
                    var values = JsonSerializer.Deserialize<Dictionary<string, string>>(response.Content);
                    throw new Exception(values["message"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _returnValue;
        }
    }

    class DPDShippingRecord
    {
        [Index(0)]
        [JsonPropertyName("order_ref")]
        public string OrderRef { get; set; }
        
        [Ignore]
        [JsonPropertyName("shipping_id")]
        public int ShippingID { get; set; }

        [Index(2)]
        [JsonPropertyName("tracker_ref")]
        public string TrackingRef { get; set; }

        [Index(7)]
        [JsonPropertyName("void")]
        public string Void { get; set; }
    }

    class DPDDataSource
    {
        public List<DPDShippingRecord> ProcessFile(string file)
        {
            var returnValue = new List<DPDShippingRecord>();
            using (TextReader fileReader = File.OpenText(file))
            {
                var csv = new CsvReader(fileReader, CultureInfo.InvariantCulture);

                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var record = csv.GetRecord<DPDShippingRecord>();
                    returnValue.Add(record);

                }
            }
            return returnValue;
        }
    }

    class DilveryOrder
    {
        [JsonPropertyName("ID")]
        public string ID { get; set; }

        [JsonPropertyName("name")]
        public string OrderNumber { get; set; }

        [JsonPropertyName("carrier_tracking_ref")]
        public string CarrierTrackingRef { get; set; }

        [JsonPropertyName("customer_po_number")]
        public string CustomerPONumber { get; set; }

        [JsonPropertyName("customer_dref")]
        public string CustomerDRef { get; set; }

        [JsonPropertyName("customer_name")]
        public string CustomerName { get; set; }
    }



    class DilveryOrderDataSource
    {

        private readonly string url;
        private readonly string apiKey;
        private readonly string uid;
        private readonly string password;
        private readonly int shippingID = 5;

        public DilveryOrderDataSource(Environment environment, string uid, string password)
        {
            switch (environment)
            {
                case Environment.Production:
                    this.url = "https://77q7jaqq13.execute-api.eu-west-2.amazonaws.com/prod/v1";
                    this.apiKey = "PbDryNbvZl72u5FbK9gdZ5IbDwZXb5yVag4tDL7l";
                    break;
                case Environment.Test:
                    url = "https://rwlzc0ciz1.execute-api.eu-west-2.amazonaws.com/dev/v1";
                    apiKey = "C1AmN0J00Z6QHUwqVrLHFvUZ9i6Hik3eXWxCx4h0";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(environment), environment, null);
            }

            this.uid = uid;
            this.password = password;
        }

        public DilveryOrder GetDeliveryOrderSummary(string orderNumber)
        {
            if (orderNumber is null)
            {
                throw new ArgumentNullException(nameof(orderNumber));
            }
            var q = WebUtility.UrlEncode($"[[\"name\",\"=\",\"{orderNumber}\"]]");
            var client = new RestClient(url);
            client.AddDefaultHeader("x-api-key", apiKey)
                    .AddDefaultQueryParameter("uid", uid)
                    .AddDefaultQueryParameter("password", password)
                    .AddDefaultQueryParameter("q", q ?? throw new InvalidOperationException())
                    .AddDefaultQueryParameter("view", "list")
                    .AddDefaultQueryParameter("limit", "1");


            List<DilveryOrder> returnValue = new List<DilveryOrder>();
            try
            {
                var request = new RestRequest($"wh/delivery-order/", (Method)DataFormat.Json);
                var response = client.Get(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    returnValue = JsonSerializer.Deserialize<List<DilveryOrder>>(response.Content);
                }
            }
            catch (Exception)
            {
                returnValue = new List<DilveryOrder>();
            }

            return returnValue.Count > 0 ? returnValue[0] : null;
        }

        public void UpdateTrackerRef(DPDShippingRecord record)
        {
            var client = new RestClient(url);
            client.AddDefaultHeader("x-api-key", apiKey)
                    .AddDefaultQueryParameter("uid", uid)
                    .AddDefaultQueryParameter("password", password);
            record.ShippingID = this.shippingID;
            var body = JsonSerializer.Serialize<DPDShippingRecord>(record);
            var request = new RestRequest($"wh/update-tracker-ref", Method.Put).AddJsonBody(body);
            var response = client.Put(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception($"{ record.OrderRef }  { record.TrackingRef} - { response.Content}");
            }
        }
    }
}
