﻿using StockAnalyzer.StockClasses.StockDataProviders.StockDataProviderDlgs;
using StockAnalyzer.StockLogging;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace StockAnalyzer.StockClasses.StockDataProviders
{
    public class InvestingIntradayDataProvider : StockDataProviderBase, IConfigDialog
    {
        static private readonly string ARCHIVE_FOLDER = INTRADAY_ARCHIVE_SUBFOLDER + @"\InvestingIntraday";
        static private readonly string INTRADAY_FOLDER = INTRADAY_SUBFOLDER + @"\InvestingIntraday";
        static private readonly string CONFIG_FILE = @"\InvestingIntradayDownload.cfg";
        static private readonly string CONFIG_FILE_USER = @"\InvestingIntradayDownload.user.cfg";

        public string UserConfigFileName => CONFIG_FILE_USER;

        public override bool LoadIntradayDurationArchiveData(string rootFolder, StockSerie serie, StockBarDuration duration)
        {
            StockLog.Write("LoadIntradayDurationArchiveData Name:" + serie.StockName + " duration:" + duration);
            var durationFileName = rootFolder + ARCHIVE_FOLDER + "\\" + duration + "\\" + serie.ShortName.Replace(':', '_') + "_" + serie.StockName + "_" + serie.StockGroup.ToString() + ".txt";
            if (File.Exists(durationFileName))
            {
                var values = serie.GetValues(duration);
                if (values == null)
                    StockLog.Write("LoadIntradayDurationArchiveData Cache File Found, current size is: 0");
                else StockLog.Write("LoadIntradayDurationArchiveData Cache File Found, current size is: " + values.Count);
                serie.ReadFromCSVFile(durationFileName, duration);


                StockLog.Write("LoadIntradayDurationArchiveData New serie size is: " + serie.GetValues(duration).Count);
                if (serie.GetValues(duration).Count > 0)
                {
                    StockLog.Write("LoadIntradayDurationArchiveData First bar: " + serie.GetValues(duration).First().ToString());
                    StockLog.Write("LoadIntradayDurationArchiveData Last bar: " + serie.GetValues(duration).Last().ToString());
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        public override void InitDictionary(string rootFolder, StockDictionary stockDictionary, bool download)
        {
            // Create data folder if not existing
            if (!Directory.Exists(rootFolder + ARCHIVE_FOLDER))
            {
                Directory.CreateDirectory(rootFolder + ARCHIVE_FOLDER);
            }

            if (!Directory.Exists(rootFolder + INTRADAY_FOLDER))
            {
                Directory.CreateDirectory(rootFolder + INTRADAY_FOLDER);
            }

            // Parse CommerzBankDownload.cfg file
            this.needDownload = download;
            InitFromFile(rootFolder, stockDictionary, download, rootFolder + CONFIG_FILE);
            InitFromFile(rootFolder, stockDictionary, download, rootFolder + CONFIG_FILE_USER);
        }

        public override bool SupportsIntradayDownload => true;

        public override bool LoadData(string rootFolder, StockSerie stockSerie)
        {
            var archiveFileName = rootFolder + ARCHIVE_FOLDER + "\\" + stockSerie.ShortName.Replace(':', '_') + "_" + stockSerie.StockName + "_" + stockSerie.StockGroup.ToString() + ".txt";
            if (File.Exists(archiveFileName))
            {
                stockSerie.ReadFromCSVFile(archiveFileName);
            }

            var fileName = rootFolder + INTRADAY_FOLDER + "\\" + stockSerie.ShortName.Replace(':', '_') + "_" + stockSerie.StockName + "_" + stockSerie.StockGroup.ToString() + ".txt";

            if (File.Exists(fileName))
            {
                if (ParseIntradayData(stockSerie, fileName))
                {
                    stockSerie.Values.Last().IsComplete = false;
                    var lastDate = stockSerie.Keys.Last();

                    var firstArchiveDate = lastDate.AddMonths(-6).AddDays(-lastDate.Day + 1).Date;

                    stockSerie.SaveToCSVFromDateToDate(archiveFileName, firstArchiveDate, lastDate.AddDays(-5).Date);
                }
                else
                {
                    return false;
                }
            }
            return stockSerie.Count > 0;
        }

        public string FormatIntradayURL(long ticker, DateTime startDate)
        {
            var interval = 5;
            var from = (long)((startDate - refDate).TotalSeconds);
            var to = (long)((DateTime.Now - refDate).TotalSeconds);

            return $"http://tvc4.forexpros.com/0f8a29a810801b55700d8d096869fe1f/1567000256/1/1/8/history?symbol={ticker}&resolution={interval}&from={from}&to={to}";
        }

        public override bool DownloadDailyData(string rootFolder, StockSerie stockSerie)
        {
            return DownloadIntradayData(rootFolder, stockSerie);
        }
        static bool first = true;
        public override bool DownloadIntradayData(string rootFolder, StockSerie stockSerie)
        {
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                NotifyProgress("Downloading intraday for " + stockSerie.StockName);

                var fileName = rootFolder + INTRADAY_FOLDER + "\\" + stockSerie.ShortName.Replace(':', '_') + "_" + stockSerie.StockName + "_" + stockSerie.StockGroup.ToString() + ".txt";

                if (File.Exists(fileName))
                {
                    var lastWriteTime = File.GetLastWriteTime(fileName);
                    if (first && lastWriteTime > DateTime.Now.AddHours(-2)
                       || (DateTime.Today.DayOfWeek == DayOfWeek.Sunday && lastWriteTime.Date >= DateTime.Today.AddDays(-1))
                       || (DateTime.Today.DayOfWeek == DayOfWeek.Saturday && lastWriteTime.Date >= DateTime.Today))
                    {
                        first = false;
                        return false;
                    }
                    else
                    {
                        if (File.GetLastWriteTime(fileName) > DateTime.Now.AddMinutes(-2))
                            return false;
                    }
                }
                using (var wc = new WebClient())
                {
                    wc.Proxy.Credentials = CredentialCache.DefaultCredentials;

                    var url = string.Empty;
                    if (stockSerie.Initialise())
                    {
                        url = FormatIntradayURL(stockSerie.Ticker, stockSerie.ValueArray[stockSerie.LastCompleteIndex].DATE.Date.AddDays(-7));
                    }
                    else
                    {
                        url = FormatIntradayURL(stockSerie.Ticker, DateTime.Today.AddDays(-80));
                    }

                    int nbTries = 3;
                    while (nbTries > 0)
                    {
                        try
                        {
                            wc.DownloadFile(url, fileName);
                            stockSerie.IsInitialised = false;
                            return true;
                        }
                        catch (Exception)
                        {
                            nbTries--;
                        }
                    }
                }
            }
            return false;
        }

        private void InitFromFile(string rootFolder, StockDictionary stockDictionary, bool download, string fileName)
        {
            string line;
            if (File.Exists(fileName))
            {
                using (var sr = new StreamReader(fileName, true))
                {
                    while (!sr.EndOfStream)
                    {
                        line = sr.ReadLine();
                        if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#")) continue;

                        var row = line.Split(',');
                        if (!stockDictionary.ContainsKey(row[2]))
                        {
                            var stockSerie = new StockSerie(row[2], row[1],
                                (StockSerie.Groups)Enum.Parse(typeof(StockSerie.Groups), row[3]),
                                StockDataProvider.InvestingIntraday);
                            stockSerie.Ticker = long.Parse(row[0]);

                            stockDictionary.Add(row[2], stockSerie);
                            if (download && this.needDownload)
                            {
                                this.needDownload = this.DownloadDailyData(rootFolder, stockSerie);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Investing Intraday Entry: " + row[2] + " already in stockDictionary");
                            //MessageBox.Show("Investing Intraday Entry: " + row[2] + " already in stockDictionary");
                        }
                    }
                }
            }
        }

        static DateTime refDate = new DateTime(1970, 01, 01) + (DateTime.Now - DateTime.UtcNow);
        private static bool ParseIntradayData(StockSerie stockSerie, string fileName)
        {
            var res = false;
            try
            {
                using (var sr = new StreamReader(fileName))
                {
                    var barchartJson = BarChartJSon.FromJson(sr.ReadToEnd());

                    for (var i = 0; i < barchartJson.C.Length; i++)
                    {
                        if (barchartJson.O[i] == 0 && barchartJson.H[i] == 0 && barchartJson.L[i] == 0 && barchartJson.C[i] == 0)
                            continue;

                        var openDate = refDate.AddSeconds(barchartJson.T[i]);
                        if (!stockSerie.ContainsKey(openDate))
                        {
                            var volString = barchartJson.V[i];
                            long vol = 0;
                            long.TryParse(barchartJson.V[i], out vol);
                            var dailyValue = new StockDailyValue(
                                   barchartJson.O[i],
                                   barchartJson.H[i],
                                   barchartJson.L[i],
                                   barchartJson.C[i],
                                   vol,
                                   openDate);

                            stockSerie.Add(dailyValue.DATE, dailyValue);
                        }
                    }
                    stockSerie.ClearBarDurationCache();

                    res = true;
                }
            }
            catch (System.Exception e)
            {
                StockLog.Write("Unable to parse intraday data for " + stockSerie.StockName);
                StockLog.Write(e);
            }
            return res;
        }

        public DialogResult ShowDialog(StockDictionary stockDico)
        {
            var configDlg = new InvestingIntradayDataProviderConfigDlg(stockDico, this.UserConfigFileName);
            return configDlg.ShowDialog();
        }

        public string DisplayName => "Investing Intraday";
    }
}
