using PdfiumViewer;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;

namespace Dispatcher
{
    internal enum Environment
    {
        Production,
        Test
    }

    internal class ResultException<TMessage, T>
    {
        public ResultException(TMessage message, T item)
        {
            this.Message = message;
            this.Item = item;
        }

        public TMessage Message
        {
            get;
        }

        public T Item
        {
            get;
        }
    }

    internal class Result<T>
    {

        public List<T> Completed
        {
            get;
        } = new List<T>();

        public List<ResultException<string, T>> Exception
        {
            get;
        } = new List<ResultException<string, T>>();

        internal void AddException(ResultException<string, T> item)
        {
            this.Exception.Add(item);
        }

        internal void AddCompleted(T item)
        {
            this.Completed.Add(item);
        }
    }

    class DPDUpdateTrackingRefReult
    {
        public int Success { get; set; }

        public int Failed { get; set; }

        public DPDUpdateTrackingRefReult(int success, int failed)
        {
            this.Success = success;
            this.Failed = failed;
        }
    }

    public class RawPrinterHelper
    {
        // Structure and API declarions:
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public class DOCINFOA
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDocName;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pOutputFile;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDataType;
        }
        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

        [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten);

        // SendBytesToPrinter()
        // When the function is given a printer name and an unmanaged array
        // of bytes, the function sends those bytes to the print queue.
        // Returns true on success, false on failure.
        public static bool SendBytesToPrinter(string szPrinterName, IntPtr pBytes, Int32 dwCount)
        {
            Int32 dwError = 0, dwWritten = 0;
            IntPtr hPrinter = new IntPtr(0);
            DOCINFOA di = new DOCINFOA();
            bool bSuccess = false; // Assume failure unless you specifically succeed.

            di.pDocName = "My C#.NET RAW Document";
            di.pDataType = "RAW";

            // Open the printer.
            if (OpenPrinter(szPrinterName.Normalize(), out hPrinter, IntPtr.Zero))
            {
                // Start a document.
                if (StartDocPrinter(hPrinter, 1, di))
                {
                    // Start a page.
                    if (StartPagePrinter(hPrinter))
                    {
                        // Write your bytes.
                        bSuccess = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);
                        EndPagePrinter(hPrinter);
                    }
                    EndDocPrinter(hPrinter);
                }
                ClosePrinter(hPrinter);
            }
            // If you did not succeed, GetLastError may give more information
            // about why not.
            if (bSuccess == false)
            {
                dwError = Marshal.GetLastWin32Error();
            }
            return bSuccess;
        }

        public static bool SendFileToPrinter(string szPrinterName, string szFileName)
        {
            // Open the file.
            FileStream fs = new FileStream(szFileName, FileMode.Open);
            // Create a BinaryReader on the file.
            BinaryReader br = new BinaryReader(fs);
            // Dim an array of bytes big enough to hold the file's contents.
            Byte[] bytes = new Byte[fs.Length];
            bool bSuccess = false;
            // Your unmanaged pointer.
            IntPtr pUnmanagedBytes = new IntPtr(0);
            int nLength;

            nLength = Convert.ToInt32(fs.Length);
            // Read the contents of the file into the array.
            bytes = br.ReadBytes(nLength);
            // Allocate some unmanaged memory for those bytes.
            pUnmanagedBytes = Marshal.AllocCoTaskMem(nLength);
            // Copy the managed byte array into the unmanaged array.
            Marshal.Copy(bytes, 0, pUnmanagedBytes, nLength);
            // Send the unmanaged bytes to the printer.
            bSuccess = SendBytesToPrinter(szPrinterName, pUnmanagedBytes, nLength);
            // Free the unmanaged memory that you allocated earlier.
            Marshal.FreeCoTaskMem(pUnmanagedBytes);
            br.Close();
            fs.Close();
            return bSuccess;
        }
    }

    internal sealed class BusinessProcess
    {
        private static readonly Lazy<BusinessProcess> Lazy = new Lazy<BusinessProcess>(() => new BusinessProcess());
        private readonly Random _random = new Random();

        private SystemDataSource _systemDataSource = new SystemDataSource();
        public UserProfile UserProfile { get; private set; }
        public Environment Environment { get; private set; }
        private DPDDataSource _dpdDataSource = new DPDDataSource();
        private DilveryOrderDataSource _dilveryOrderDataSource = null;

        private PrinterDataSource _printerDataSource = null;
        private PrinterQueueDataSource _printerQueueDataSource = null;
        
        private BusinessProcess()
        {
            UserProfile = _systemDataSource.RetrieveUserProfile();
            
            UserPreferences userPreferences = _systemDataSource.RetrieveUserPreferences();                        
            if (userPreferences == null)
            {
                userPreferences = new UserPreferences();
                userPreferences.PrinterID = 6;
                userPreferences.ServiceAPC = "Disable";
                userPreferences.ServiceDPD = "Disable";

                _systemDataSource.StoreUserPreferences(userPreferences);
            }
            
            APCPrinterPreferences apcPrinterPreferences = _systemDataSource.RetrieveAPCPrinterPreferences();
            if (apcPrinterPreferences == null)
            {
                apcPrinterPreferences = new APCPrinterPreferences();
                apcPrinterPreferences.AccountNumber = "";
                apcPrinterPreferences.APIEndpoint = "https://apc-training.hypaship.com/api/3.0/";
                apcPrinterPreferences.Username = "test@chameleoncodewing.co.uk";
                apcPrinterPreferences.Password = "Test1234";
                apcPrinterPreferences.LabelPrinterName = "";
                apcPrinterPreferences.PrintingFormat = "ZPL";

                _systemDataSource.StoreAPCPrinterPreferences(apcPrinterPreferences);
            }


            if (UserProfile != null)
            {
                Environment = UserProfile.Env == "test" ? Environment.Test : Environment.Production;
                _printerDataSource = new PrinterDataSource(Environment, UserProfile.UID.ToString(), UserProfile.Password);

                _printerQueueDataSource =
                    new PrinterQueueDataSource(Environment, userPreferences.PrinterID.ToString(), UserProfile.UID.ToString(), UserProfile.Password);
                _dilveryOrderDataSource = new DilveryOrderDataSource(Environment, UserProfile.UID.ToString(), UserProfile.Password);
            }
        }

        public static BusinessProcess Instance => Lazy.Value;

        private readonly Dictionary<string, List<string>> _textData = new Dictionary<string, List<string>>();
        

        internal void DoLogin(Environment envType, string username, string password)
        {
            var auth = new AuthDataSource(envType);
            var loginResult = auth.Login(username, password);
            Environment = envType;            
            UserProfile = new UserProfile();
            UserProfile.UID = loginResult.UID;
            UserProfile.Username = username;
            UserProfile.Name = loginResult.Name;
            UserProfile.Password = loginResult.Password;
            UserProfile.Env = envType.Equals(Environment.Test) ? "test" : "production";
            UserPreferences userPreferences = _systemDataSource.RetrieveUserPreferences();

            if (userPreferences == null)
            {
                userPreferences = new UserPreferences();
                userPreferences.PrinterID = 6;
                userPreferences.ServiceAPC = "Disable";
                userPreferences.ServiceDPD = "Disable";

                _systemDataSource.StoreUserPreferences(userPreferences);
            }

            _systemDataSource.StoreUserProfile(UserProfile);

            _printerDataSource = new PrinterDataSource(Environment, UserProfile.UID.ToString(), UserProfile.Password);
            _printerQueueDataSource =
                new PrinterQueueDataSource(Environment, userPreferences.PrinterID.ToString(), loginResult.UID.ToString(), loginResult.Password);
            _dilveryOrderDataSource = new DilveryOrderDataSource(Environment, UserProfile.UID.ToString(), UserProfile.Password);
        }

        internal void DoLogout()
        {
            _printerQueueDataSource = null;
            _systemDataSource.ClearUserProfile();
        }

        internal bool IsLoggedIn()
        {
            return _printerQueueDataSource != null;
        }

        internal List<Printer> ListPrinter()
        {
            return _printerDataSource.FetchPrinters();
        }

        internal Result<PrinterQueue> DoPrinting(List<PrinterQueue> queues)
        {
            var returnValue = new Result<PrinterQueue>();
            UserPreferences userPreferences = this.RetrieveUserPreferences();

            foreach (var elm in queues)
            {                
                if (elm.FileType == "pdf")
                {
                    try
                    {
                        PrintToPdfPrinter(elm);
                        returnValue.AddCompleted(elm);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        returnValue.AddException(new ResultException<string, PrinterQueue>(ex.Message, elm));
                    }
                }
                else if (elm.FileName.EndsWith("dpd") && userPreferences.ServiceDPD != UserPreferences.SERVICE_DISABLE)
                {
                    try
                    {
                        if (userPreferences.ServiceDPD == UserPreferences.SERVICE_LEGACY)
                        {
                            QueueTextData(elm);
                        }
                        else if (userPreferences.ServiceDPD == UserPreferences.SERVICE_API_WB)
                        {
                            DPDLabelPrinting(elm);
                        }                        
                        returnValue.AddCompleted(elm);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        returnValue.AddException(new ResultException<string, PrinterQueue>(ex.Message, elm));
                    }
                }
                else if (elm.FileName.EndsWith("apc") && userPreferences.ServiceAPC != UserPreferences.SERVICE_DISABLE)
                {
                    try
                    {
                        if (userPreferences.ServiceAPC == UserPreferences.SERVICE_LEGACY)
                        {
                            QueueTextData(elm);
                        }
                        else if (userPreferences.ServiceAPC == UserPreferences.SERVICE_API_WB)
                        {
                            APCLabelPrinting(elm);
                        }
                        returnValue.AddCompleted(elm);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        returnValue.AddException(new ResultException<string, PrinterQueue>(ex.Message, elm));
                    }
                }
                else if (elm.FileName.EndsWith("dl") && userPreferences.ServiceDL != UserPreferences.SERVICE_DISABLE)
                {
                    try
                    {
                        if (userPreferences.ServiceDL == UserPreferences.SERVICE_LEGACY)
                        {
                            QueueTextData(elm);
                        }
                        else if (userPreferences.ServiceDL == UserPreferences.SERVICE_API_WB)
                        {
                            DLLabelPrinting(elm);
                        }
                        returnValue.AddCompleted(elm);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        returnValue.AddException(new ResultException<string, PrinterQueue>(ex.Message, elm));
                    }
                }
                else if (elm.FileName.EndsWith("rm"))
                {
                    try
                    {
                        QueueTextData(elm);
                        returnValue.AddCompleted(elm);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        returnValue.AddException(new ResultException<string, PrinterQueue>(ex.Message, elm));
                    }
                }
            }
            StoreTextFile();
            return returnValue;
        }

        private void APCLabelPrinting(PrinterQueue elm)
        {
            var _businessProcess = BusinessProcess.Instance;
            var _apcPreferences = _businessProcess.RetrieveAPCPrinterPreferences();

            string _printerName = _apcPreferences.LabelPrinterName;

            string stringData = _printerQueueDataSource.GetAPCLabel(elm, _apcPreferences);


            byte[] data = Convert.FromBase64String(stringData);
            if (_apcPreferences.PrintingFormat.Equals("ZPL"))
            {
                var _tempFile = Path.GetTempFileName();
                File.WriteAllBytes(_tempFile, data);
                RawPrinterHelper.SendFileToPrinter(_printerName, _tempFile);
            }
            else
            {
                this.PrintPdf(_printerName, data);
            } 
            
        }

        private void DLLabelPrinting(PrinterQueue elm)
        {

            var _businessProcess = BusinessProcess.Instance;
            var _preferences = _businessProcess.RetrieveDLPreferences();

            string _printerName = _preferences.LabelPrinterName;

            string stringData = _printerQueueDataSource.GetDLLabel(elm, _preferences);


            byte[] data = Convert.FromBase64String(stringData);
            
            this.PrintPdf(_printerName, data);

        }

        private void DPDLabelPrinting(PrinterQueue elm)
        {
            var _businessProcess = BusinessProcess.Instance;
            var _dpdPreferences = _businessProcess.RetrieveDPDPrinterPreferences();
            string _printerName = _dpdPreferences.PrinterName;
            string _format;
            if (_dpdPreferences.PrintFormat.Equals("ELP"))
            {
                _format = "text/vnd.eltron-epl;";
            }
            else
            {
                _format = "ext/vnd.citizen-clp;";
            }

            string _template = _dpdPreferences.Template.ToLower();

            string data = _printerQueueDataSource.GetLabel(elm, _format, _template);

            var _tempFile = Path.GetTempFileName();
            File.WriteAllText(_tempFile, data);

            RawPrinterHelper.SendFileToPrinter(_printerName, _tempFile);
        }

        private void PrintToPdfPrinter(PrinterQueue job)
        {
            if (job.File != null)
            {
                byte[] data = Convert.FromBase64String(job.File);                
                this.PrintPdf(job.PrinterName, data);
            }
            else
            {
                throw new Exception("File content is empty");
            }
            
        }

        private void PrintPdf(string printer, byte[] data, string paperName = "A4", int copies = 1)
        {
            using (var ms = new MemoryStream())
            {
                ms.Write(data, 0, data.Length);                
                this.PrintPdf(printer, paperName, ms, copies);
            }
        }

        private void PrintPdf(string printer, string paperName, Stream stream, int copies)
        {
            // Create the printer settings for our printer
            var printerSettings = new PrinterSettings
            {
                PrinterName = printer,
                Copies = (short)copies,
            };

            // Create our page settings for the paper size selected
            var pageSettings = new PageSettings(printerSettings)
            {
                Margins = new Margins(0, 0, 0, 0),
            };
            
            foreach (PaperSize paperSize in printerSettings.PaperSizes)
            {
                if (paperSize.PaperName == paperName)
                {
                    pageSettings.PaperSize = paperSize;
                    break;
                }
            }

            // Now print the PDF document
            using (var document = PdfDocument.Load(stream))
            {
                using (var printDocument = document.CreatePrintDocument())
                {
                    printDocument.PrinterSettings = printerSettings;
                    printDocument.DefaultPageSettings = pageSettings;
                    printDocument.PrintController = new StandardPrintController();
                    printDocument.Print();
                }
            }
        }


        private void StoreTextFile()
        {
            foreach (var key in _textData.Keys)
            {
                Directory.CreateDirectory($"C:\\Users\\Public\\Dispatcher\\{key}");
                if (key == "pf")
                {
                    foreach (var line in _textData[key])
                    {
                        File.WriteAllText($"C:\\Users\\Public\\Dispatcher\\{key}\\{key.ToUpper()}_{RandomString(25)}.csv", line);
                    }
                }
                else
                {
                    File.WriteAllLines($"C:\\Users\\Public\\Dispatcher\\{key}\\{key.ToUpper()}_{RandomString(25)}.{key}", _textData[key]);
                }
            }
            _textData.Clear();
        }

        private void QueueTextData(PrinterQueue item)
        {
            if (item.File == null)
            {
                throw new Exception("File content is empty");
            }
            var fileName = item.FileName;
            var index = fileName.LastIndexOf('.');
            var keyExt = fileName.Substring(index + 1);
            if (!_textData.ContainsKey(keyExt))
            {
                _textData[keyExt] = new List<string>();

            }
            _textData[keyExt].Add(item.File.Replace('\n', ' '));
        }

        private string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        internal void CloseJobs(Result<PrinterQueue> result)
        {
            
            foreach (var elm in result.Completed)
            {
                _printerQueueDataSource.CloseJob(elm);
            }

            foreach (var elmException in result.Exception)
            {
                _printerQueueDataSource.ExceptionJob(elmException.Item, elmException.Message);
            }

        }

        public List<PrinterQueue> FetchUnPrintJobs()
        {
            return _printerQueueDataSource.FetchUnPrint();
        }

        public DPDUpdateTrackingRefReult DPDUpdateTrackingRef(string file)
        {
            var records = _dpdDataSource.ProcessFile(file);
            int success = 0;
            int failed = 0;
            foreach(var record in records)
            {
                try
                {
                    if ("N".Equals(record.Void))
                    {                        
                        success++;
                    }
                    
                }
                catch (Exception ex)
                {
                    failed++;
                    Console.WriteLine(ex.Message);
                }
            }

            return new DPDUpdateTrackingRefReult(success, failed);
        }

        public void StoreLastDPDLocation(string path)
        {
            path = Path.GetFullPath(path);
            _systemDataSource.StoreLastUsedPath(path);
        }

        public string RetrieveLastDPDLocation()
        {
            return _systemDataSource.RetrieveLastUsedPath();
        }

        public void StoreDPDPrinterPreferences(DPDPrinterPreferences dpdPrinterPreferences)
        {
            _systemDataSource.StoreDPDPrinterPreferences(dpdPrinterPreferences);
        }

        public DPDPrinterPreferences RetrieveDPDPrinterPreferences()
        {
            return _systemDataSource.RetrieveDPDPrinterPreferences();
        }

        public void StoreAPCPrinterPreferences(APCPrinterPreferences apcPrinterPreferences)
        {
            _systemDataSource.StoreAPCPrinterPreferences(apcPrinterPreferences);
        }

        public void StoreDLPreferences(DLPreferences dlPreferences)
        {
            _systemDataSource.StoreDLPreferences(dlPreferences);
        }

        public APCPrinterPreferences RetrieveAPCPrinterPreferences()
        {
            return _systemDataSource.RetrieveAPCPrinterPreferences();
        }

        internal DLPreferences RetrieveDLPreferences()
        {
            return _systemDataSource.RetrieveDLPreferences();
        }

        public void StoreUserPreferences(UserPreferences userPreferences)
        {
            _systemDataSource.StoreUserPreferences(userPreferences);
        }

        public UserPreferences RetrieveUserPreferences()
        {
            return _systemDataSource.RetrieveUserPreferences();
        }

        public void ApplyChanges()
        {
            UserPreferences userPreferences = this.RetrieveUserPreferences();
            _printerQueueDataSource.PrinterID = userPreferences.PrinterID.ToString();
        }
    }
}
