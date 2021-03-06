﻿using StockAnalyzer.StockClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace StockAnalyzerApp.CustomControl.AlertDialog
{
    public partial class AlertDlg : Form
    {
        private AlertControl alertControl;

        public AlertDlg(StockAlertConfig alertCfg)
        {
            InitializeComponent();

            this.alertControl = this.elementHost1.Child as AlertControl;
            this.alertControl.TimeFrameComboBox.SelectedIndex = StockAlertConfig.AlertConfigs.IndexOf(alertCfg);

            StockAnalyzerForm.MainFrame.AlertDetected += MainFrame_AlertDetected;
            StockAnalyzerForm.MainFrame.AlertDetectionProgress += MainFrame_AlertDetectionProgress;
            StockAnalyzerForm.MainFrame.AlertDetectionStarted += MainFrame_AlertDetectionStarted;
        }

        public AlertDlg(StockAlertLog dailyAlertLog, List<StockAlertDef> dailyAlertDefs)
        {
            throw new NotImplementedException("AlertDlg(StockAlertLog dailyAlertLog, List<StockAlertDef> dailyAlertDefs)");
        }

        void MainFrame_AlertDetectionProgress(string stockName)
        {
            this.alertControl.SelectedTimeFrame.AlertLog.ProgressName = stockName;
            this.alertControl.SelectedTimeFrame.AlertLog.ProgressValue++;
        }

        void MainFrame_AlertDetectionStarted(int nbStock)
        {
            this.alertControl.SelectedTimeFrame.AlertLog.ProgressValue = 0;
            this.alertControl.SelectedTimeFrame.AlertLog.ProgressMax = nbStock;
            this.alertControl.SelectedTimeFrame.AlertLog.ProgressVisibility = true;
        }

        void MainFrame_AlertDetected()
        {
            this.Activate();
            this.alertControl.SelectedTimeFrame.AlertLog.ProgressValue = 0;
            this.alertControl.SelectedTimeFrame.AlertLog.ProgressVisibility = false;
        }
    }
}
