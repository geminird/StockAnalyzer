﻿using StockAnalyzer.StockClasses;
using System.Windows.Controls;
using System.Windows.Input;

namespace StockAnalyzerApp.CustomControl.BinckPortfolioDlg
{
    /// <summary>
    /// Interaction logic for BinckPortfolioControl.xaml
    /// </summary>
    public partial class BinckPortfolioControl : UserControl
    {
        public event StockAnalyzerForm.SelectedStockChangedEventHandler SelectedStockChanged;
        public BinckPortfolioControl()
        {
            InitializeComponent();

            this.SelectedStockChanged += StockAnalyzerForm.MainFrame.OnSelectedStockChanged;
        }

        private void positionGridView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var viewModel = this.positionGridView.SelectedCells[0].Item as StockPositionViewModel;
            if (viewModel == null || !viewModel.IsValidName) return;

            if (SelectedStockChanged != null)
                this.SelectedStockChanged(viewModel.StockName, true);

            StockAnalyzerForm.MainFrame.WindowState = System.Windows.Forms.FormWindowState.Normal;
        }

        private void FilterOperatorsLoading(object sender, Telerik.Windows.Controls.GridView.FilterOperatorsLoadingEventArgs e)
        {
            var column = e.Column as Telerik.Windows.Controls.GridViewBoundColumnBase;
            if (column != null && column.DataType == typeof(string))
            {
                e.DefaultOperator1 = Telerik.Windows.Data.FilterOperator.Contains;
                e.DefaultOperator2 = Telerik.Windows.Data.FilterOperator.Contains;
            }
        }

        private void OperationGridView_AutoGeneratingColumn(object sender, Telerik.Windows.Controls.GridViewAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "NameMapping")
            {
                e.Cancel = true;
            }
        }
    }
}
