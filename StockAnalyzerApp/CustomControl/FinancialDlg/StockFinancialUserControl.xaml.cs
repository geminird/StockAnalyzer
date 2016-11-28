﻿using StockAnalyzer.StockClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StockAnalyzerApp.CustomControl.FinancialDlg
{
    /// <summary>
    /// Interaction logic for StockFinancialUserControl.xaml
    /// </summary>
    public partial class StockFinancialUserControl : UserControl
    {
        public StockFinancialUserControl()
        {
            InitializeComponent();

            foreach (var prop in (typeof(StockFinancial)).GetProperties())
            {
                if (prop.PropertyType == typeof(DataTable) || prop.PropertyType.Name.StartsWith("List"))
                    continue;
                StackPanel propertyRow = new StackPanel() { Orientation = Orientation.Horizontal };

                // Create financials control
                Label labelTitle = new Label();
                labelTitle.Content = prop.Name;
                labelTitle.Width = 100;

                TextBlock labelValue = new TextBlock();
                labelValue.Margin = new Thickness(0, 5, 0, 0);
                if (prop.Name == "Yield")
                {
                    labelValue.SetBinding(TextBlock.TextProperty, new Binding(prop.Name) { StringFormat = "P2" });
                }
                else
                {
                    labelValue.SetBinding(TextBlock.TextProperty, new Binding(prop.Name));
                }
                propertyRow.Children.Add(labelTitle);
                propertyRow.Children.Add(labelValue);

                this.propertyPanel.Children.Add(propertyRow);
            }
        }
    }
}
