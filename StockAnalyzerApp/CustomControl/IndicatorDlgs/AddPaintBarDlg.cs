﻿using System.Windows.Forms;
using StockAnalyzer.StockClasses.StockViewableItems.StockPaintBars;

namespace StockAnalyzerApp.CustomControl.IndicatorDlgs
{
   public partial class AddPaintBarDlg : Form
   {
      public string PaintBarName { get { return this.paintBarComboBox.SelectedItem.ToString(); } }

      public AddPaintBarDlg()
      {
         InitializeComponent();

         foreach (string indicatorName in StockPaintBarManager.GetPaintBarList())
         {
            this.paintBarComboBox.Items.Add(indicatorName);
         }
         this.paintBarComboBox.SelectedItem = this.paintBarComboBox.Items[0];
      }

      private void paintBarComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
      {
         IStockPaintBar ts = StockPaintBarManager.CreatePaintBar(this.PaintBarName);
         this.descriptionTextBox.Text = ts.Definition;
      }
   }
}
