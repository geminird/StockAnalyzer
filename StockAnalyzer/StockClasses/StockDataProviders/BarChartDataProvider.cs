﻿using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Runtime.Serialization.Json;
using StockAnalyzer.StockClasses.StockDataProviders.StockDataProviderDlgs;
using StockAnalyzer.StockLogging;
using System.Collections.Generic;

namespace StockAnalyzer.StockClasses.StockDataProviders
{
    public class BarChartDataProvider : StockDataProviderBase, IConfigDialog
    {
        static private string FOLDER = @"\data\daily\BarChart";
        static private string ARCHIVE_FOLDER = @"\data\archive\daily\BarChart";

        static private string CONFIG_FILE = @"\BarChartDownload.cfg";
        static private string CONFIG_FILE_USER = @"\BarChartDownload.user.cfg";
        public string UserConfigFileName { get { return CONFIG_FILE_USER; } }


        public override bool SupportsIntradayDownload
        {
            get { return false; }
        }
        public override void InitDictionary(string rootFolder, StockDictionary stockDictionary, bool download)
        {
            // Parse BarChart.cfg file// Create data folder if not existing
            if (!Directory.Exists(rootFolder + FOLDER))
            {
                Directory.CreateDirectory(rootFolder + FOLDER);
            }
            if (!Directory.Exists(rootFolder + ARCHIVE_FOLDER))
            {
                Directory.CreateDirectory(rootFolder + ARCHIVE_FOLDER);
            }

            // Parse BarChartDownload.cfg file
            InitFromFile(rootFolder, stockDictionary, download, rootFolder + CONFIG_FILE);
            InitFromFile(rootFolder, stockDictionary, download, rootFolder + CONFIG_FILE_USER);
        }

        private void InitFromFile(string rootFolder, StockDictionary stockDictionary, bool download, string fileName)
        {
            string line;
            if (File.Exists(fileName))
            {
                using (StreamReader sr = new StreamReader(fileName, true))
                {
                    sr.ReadLine(); // Skip first line
                    while (!sr.EndOfStream)
                    {
                        line = sr.ReadLine();
                        if (!line.StartsWith("#") && !string.IsNullOrWhiteSpace(line))
                        {
                            string[] row = line.Split(',');

                            string shortName = row[0].Contains(':') ? row[0].Split(':')[1] : row[0];
                            StockSerie stockSerie = new StockSerie(row[1], shortName, (StockSerie.Groups)Enum.Parse(typeof(StockSerie.Groups), row[2]), StockDataProvider.BarChart);

                            if (!stockDictionary.ContainsKey(row[1]))
                            {
                                stockDictionary.Add(row[1], stockSerie);
                            }
                            else
                            {
                                StockLog.Write("BarChart Entry: " + row[1] + " already in stockDictionary");
                            }
                            if (download && this.needDownload)
                            {
                                this.DownloadDailyData(rootFolder, stockSerie);
                            }
                        }
                    }
                }
            }
        }
        public override bool LoadData(string rootFolder, StockSerie stockSerie)
        {
            if (stockSerie.StockName == "TRIN_LOG")
            {
                StockSerie trinSerie = StockDictionary.StockDictionarySingleton["TRIN"];
                trinSerie.Initialise();
                return this.GenerateLogSerie(stockSerie, trinSerie, true);
            }
            if (stockSerie.StockName == "TRIN_SUM")
            {
                StockSerie trinSerie = StockDictionary.StockDictionarySingleton["TRIN"];
                trinSerie.Initialise();
                return this.GenerateSumSerie(stockSerie, trinSerie, true);
            }
            if (stockSerie.StockName == "TRIN_LOG_SUM")
            {
                StockSerie trinSerie = StockDictionary.StockDictionarySingleton["TRIN_LOG"];
                trinSerie.Initialise();
                return this.GenerateSumSerie(stockSerie, trinSerie, true);
            }
            // Read archive first
            bool res = false;
            string fileName = stockSerie.ShortName + "_" + stockSerie.StockName + "_" + stockSerie.StockGroup.ToString() + "_*.csv";
            string[] files = System.IO.Directory.GetFiles(rootFolder + ARCHIVE_FOLDER, fileName);
            foreach (string archiveFileName in files)
            {
                res |= ParseCSVFile(stockSerie, archiveFileName);
            }

            fileName = rootFolder + FOLDER + "\\" + stockSerie.ShortName + "_" + stockSerie.StockName + "_" + stockSerie.StockGroup.ToString() + ".csv";
            return ParseCSVFile(stockSerie, fileName) || res;
        }

        private bool GenerateSumSerie(StockSerie stockSerie, StockSerie trinSerie, bool inverse)
        {
            StockDailyValue logValue = null;
            float sum = 0f;
            foreach (StockDailyValue dailyValue in trinSerie.Values)
            {
                sum += dailyValue.CLOSE;

                logValue = new StockDailyValue(
                   sum,
                   sum,
                   sum,
                   sum, 0, dailyValue.DATE);
                stockSerie.Add(logValue.DATE, logValue);
                logValue.Serie = stockSerie;
            }
            return true;
        }
        private bool GenerateLogSerie(StockSerie stockSerie, StockSerie ratioSerie, bool inverse)
        {
            StockDailyValue logValue = null;
            float ratio = 0;
            foreach (StockDailyValue dailyValue in ratioSerie.Values)
            {
                if (dailyValue.CLOSE > 0f)
                {
                    ratio = (float)Math.Log10(dailyValue.CLOSE);
                    if (inverse) ratio = -ratio;
                }
                logValue = new StockDailyValue(
                   ratio,
                   ratio,
                   ratio,
                   ratio, 0, dailyValue.DATE);
                stockSerie.Add(logValue.DATE, logValue);
                logValue.Serie = stockSerie;
            }
            return true;
        }
       

        private bool ParseBarChartFile(StockSerie stockSerie, string fileName)
        {
            bool res = false;
            try
            {
                if (File.Exists(fileName))
                {
                    using (StreamReader sr = new StreamReader(fileName))
                    {
                        DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(BarChartResponse));
                        BarChartResponse jsonResponse = jsonSerializer.ReadObject(sr.BaseStream) as BarChartResponse;
                        if (jsonResponse != null && jsonResponse.error != null)
                        {
                            foreach (var data in jsonResponse.data.series[0].data)
                            {
                                DateTime date = refDate.AddMilliseconds(data[0]).Date;
                                if (!stockSerie.ContainsKey(date))
                                {
                                    stockSerie.Add(date, new StockDailyValue((float)data[1], (float)data[2], (float)data[3], (float)data[4], 0, date));
                                }
                            }

                            res = true;
                        }
                    }
                }
            }
            catch (System.Exception e)
            {
                StockLog.Write(e);
                res = false;
            }
            return res;
        }
        public class Series
        {
            public string name { get; set; }
            public string id { get; set; }
            public string key { get; set; }
            public List<List<double>> data { get; set; }
        }

        public class Data
        {
            public List<Series> series { get; set; }
        }

        public class BarChartResponse
        {
            public string error { get; set; }
            public string data_time_type { get; set; }
            public int interval { get; set; }
            public long start { get; set; }
            public long end { get; set; }
            public Data data { get; set; }
            public bool record_range { get; set; }
            public string record_type { get; set; }
        }

        public override bool DownloadDailyData(string rootFolder, StockSerie stockSerie)
        {
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                string shortName = stockSerie.ShortName;

                bool isUpTodate = false;
                stockSerie.Initialise();
                if (stockSerie.Count > 0)
                {
                    // This serie already exist, download just the missing data.
                    DateTime lastDate = stockSerie.Keys.Last();

                    isUpTodate = (lastDate >= DateTime.Today) ||
                        (lastDate.DayOfWeek == DayOfWeek.Friday && (DateTime.Now - lastDate).Days <= 2 && (DateTime.Today.DayOfWeek == DayOfWeek.Monday && DateTime.UtcNow.Hour < 20)) ||
                        (lastDate >= DateTime.Today.AddDays(-1) && DateTime.UtcNow.Hour < 20);

                    NotifyProgress("Downloading " + stockSerie.StockGroup.ToString() + " - " + stockSerie.StockName);
                    if (!isUpTodate)
                    {
                        for (int year = lastDate.Year; year < DateTime.Today.Year; year++)
                        {
                            // Happy new year !!! it's time to archive old data...
                            if (
                               !File.Exists(rootFolder + ARCHIVE_FOLDER + "\\" + stockSerie.ShortName + "_" +
                                            stockSerie.StockName + "_" + stockSerie.StockGroup.ToString() + "_" +
                                            year.ToString() + ".csv"))
                            {
                                this.DownloadFileFromProvider(rootFolder + ARCHIVE_FOLDER,
                                   stockSerie.ShortName + "_" + stockSerie.StockName + "_" + stockSerie.StockGroup.ToString() +
                                   "_" + year.ToString() + ".csv", new DateTime(year, 1, 1), new DateTime(year, 12, 31),
                                   stockSerie.ShortName);
                            }
                        }
                        DateTime startDate = new DateTime(DateTime.Today.Year, 01, 01);
                        string fileName = stockSerie.ShortName + "_" + stockSerie.StockName + "_" + stockSerie.StockGroup.ToString() + ".csv";
                        this.DownloadFileFromProvider(rootFolder + FOLDER, fileName, startDate, DateTime.Today, shortName);

                        if (stockSerie.StockName == "SPY") // Check if something new has been downloaded using ANGLO AMERICAN as the reference for all downloads
                        {
                            this.ParseCSVFile(stockSerie, rootFolder + FOLDER + "\\" + fileName);
                            if (lastDate == stockSerie.Keys.Last())
                            {
                                this.needDownload = false;
                            }
                        }
                    }
                    stockSerie.IsInitialised = isUpTodate;
                }
                else
                {
                    NotifyProgress("Creating archive for " + stockSerie.StockName + " - " + stockSerie.StockGroup.ToString());
                    DateTime lastDate = new DateTime(DateTime.Today.Year, 01, 01);
                    int archive_start_year = DateTime.Now.Year - 2;
                    for (int i = lastDate.Year - 1; i >= archive_start_year ; i--)
                    {
                        if (!this.DownloadFileFromProvider(rootFolder + ARCHIVE_FOLDER, stockSerie.ShortName + "_" + stockSerie.StockName + "_" + stockSerie.StockGroup.ToString() + "_" + i.ToString() + ".csv", new DateTime(i, 1, 1), new DateTime(i, 12, 31), shortName))
                        {
                            break;
                        }
                    }
                    this.DownloadFileFromProvider(rootFolder + FOLDER, stockSerie.ShortName + "_" + stockSerie.StockName + "_" + stockSerie.StockGroup.ToString() + ".csv", lastDate, DateTime.Today, shortName);
                }
            }
            return true;
        }
        static string BARCHART_API_KEY = "ebc71dae1c7ca3157e243600383649e7";

        public static string FormatDailyURL(string symbol, DateTime startDate, DateTime endDate)
        {
            //period = "minutes";
            string period = "daily";
            int interval = 0;
            string start = startDate.ToString("yyyyMMdd");
            string end = endDate.ToString("yyyyMMdd");

            string url = "http://ondemand.websol.barchart.com/getHistory.csv?key=$KEY&symbol=$NAME&type=$PERIOD&startDate=$START&endDate=$END&maxRecords=10&interval=$INTERVAL&order=asc&sessionFilter=EFK&splits=true&dividends=true&volume=sum&nearby=1&exchange=NYSE%2CAMEX%2CNASDAQ&backAdjust=false&daysToExpiration=1&contractRoll=expiration";
            url = "http://marketdata.websol.barchart.com/getHistory.csv?key=$KEY&symbol=$NAME&type=$PERIOD&interval=$INTERVAL&startDate=$START&endDate=$END";
            url = url.Replace("$KEY", BARCHART_API_KEY);
            url = url.Replace("$NAME", symbol);
            url = url.Replace("$PERIOD", period);
            url = url.Replace("$START", start);
            url = url.Replace("$END", end);
            url = url.Replace("$INTERVAL", interval.ToString());

            return url;
        }

        private static DateTime refDate = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        public bool DownloadFileFromProvider(string destFolder, string fileName, DateTime startDate, DateTime endDate, string stockName)
        {
            string url = FormatDailyURL(stockName, startDate, endDate);

            int nbTries = 2;
            while (nbTries > 0)
            {
                try
                {
                    using (WebClient wc = new WebClient())
                    {
                        wc.Proxy.Credentials = CredentialCache.DefaultCredentials;

                        string fileContent = wc.DownloadString(url);
                        if (fileContent.StartsWith("You have reached the maximum number of requests"))
                            return false;
                        fileContent = fileContent.Replace("Request.JSONP.request_map.request_1(", "");
                        fileContent = fileContent.Replace("Request.JSONP.request_map.request_2(", "");
                        fileContent = fileContent.Replace(")", "");

                        if (fileContent.Length <= 200 || fileContent.StartsWith("<!DOCTYPE", StringComparison.CurrentCultureIgnoreCase))
                        {
                            return false;
                        }
                        else
                        {
                            // Save content to file
                            using (StreamWriter writer = new StreamWriter(destFolder + "\\" + fileName))
                            {
                                writer.Write(fileContent);
                                //StockLog.Write("Download succeeded: " + destFolder + "\\" + fileName);
                            }
                        }

                        StockLog.Write("Download succeeded: " + stockName);
                        return true;
                    }
                }
                catch (SystemException e)
                {
                    StockLog.Write(e);
                    nbTries--;
                }
            }
            if (nbTries <= 0)
            {
                return false;
            }
            return true;
        }

        public bool DownloadFileFromProvider2(string destFolder, string fileName, DateTime startDate, DateTime endDate, string stockName)
        {
            string url = @"http://jscharts-e-barchart.aws.barchart.com//charts/update_dynamic_zoom?callback=Request.JSONP.request_map.request_1&data_time=daily&symbol=$NAME&end=$END&start=$START&cookie_index=0";

            // Build URL
            url = url.Replace("$NAME", stockName);

            url = url.Replace("$START", (startDate - refDate).TotalMilliseconds.ToString());
            url = url.Replace("$END", (endDate.AddHours(23).AddMinutes(59).AddSeconds(59) - refDate).TotalMilliseconds.ToString());

            int nbTries = 2;
            while (nbTries > 0)
            {
                try
                {
                    using (WebClient wc = new WebClient())
                    {
                        wc.Proxy.Credentials = CredentialCache.DefaultCredentials;

                        string fileContent = wc.DownloadString(url);

                        fileContent = fileContent.Replace("Request.JSONP.request_map.request_1(", "");
                        fileContent = fileContent.Replace("Request.JSONP.request_map.request_2(", "");
                        fileContent = fileContent.Replace(")", "");

                        if (fileContent.Length <= 200 || fileContent.StartsWith("<!DOCTYPE", StringComparison.CurrentCultureIgnoreCase))
                        {
                            return false;
                        }
                        else
                        {
                            // Save content to file
                            using (StreamWriter writer = new StreamWriter(destFolder + "\\" + fileName))
                            {
                                writer.Write(fileContent);
                                //StockLog.Write("Download succeeded: " + destFolder + "\\" + fileName);
                            }
                        }

                        StockLog.Write("Download succeeded: " + stockName);
                        return true;
                    }
                }
                catch (SystemException e)
                {
                    StockLog.Write(e);
                    nbTries--;
                }
            }
            if (nbTries <= 0)
            {
                return false;
            }
            return true;
        }

        public DialogResult ShowDialog(StockDictionary stockDico)
        {
            //BarChartDataProviderConfigDlg configDlg = new BarChartDataProviderConfigDlg(stockDico);
            //return configDlg.ShowDialog();
            return DialogResult.Cancel;
        }

        public string DisplayName
        {
            get { return "BarChart"; }
        }
    }
}
