﻿using StockAnalyzerApp.CustomControl.SimulationDlgs;

namespace StockAnalyzerApp.CustomControl
{
   partial class BatchStrategySimulatorDlg
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.simulateTradingBtn = new System.Windows.Forms.Button();
         this.generateReportCheckBox = new System.Windows.Forms.CheckBox();
         this.simulationParameterControl = new SimulationParameterControl();
         this.SuspendLayout();
         // 
         // simulateTradingBtn
         // 
         this.simulateTradingBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.simulateTradingBtn.Location = new System.Drawing.Point(309, 465);
         this.simulateTradingBtn.Name = "simulateTradingBtn";
         this.simulateTradingBtn.Size = new System.Drawing.Size(99, 23);
         this.simulateTradingBtn.TabIndex = 4;
         this.simulateTradingBtn.Text = "Simulate Trading";
         this.simulateTradingBtn.UseVisualStyleBackColor = true;
         this.simulateTradingBtn.Click += new System.EventHandler(this.simulateTradingBtn_Click);
         // 
         // generateReportCheckBox
         // 
         this.generateReportCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.generateReportCheckBox.AutoSize = true;
         this.generateReportCheckBox.Checked = false;
         this.generateReportCheckBox.CheckState = System.Windows.Forms.CheckState.Unchecked;
         this.generateReportCheckBox.Location = new System.Drawing.Point(15, 469);
         this.generateReportCheckBox.Name = "generateReportCheckBox";
         this.generateReportCheckBox.Size = new System.Drawing.Size(100, 17);
         this.generateReportCheckBox.TabIndex = 0;
         this.generateReportCheckBox.Text = "Generate report";
         this.generateReportCheckBox.UseVisualStyleBackColor = true;
         // 
         // simulationParameterControl
         // 
         this.simulationParameterControl.Location = new System.Drawing.Point(0, 0);
         this.simulationParameterControl.Name = "simulationParameterControl";
         this.simulationParameterControl.Size = new System.Drawing.Size(408, 469);
         this.simulationParameterControl.TabIndex = 5;
         // 
         // BatchStrategySimulatorDlg
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(420, 497);
         this.Controls.Add(this.simulateTradingBtn);
         this.Controls.Add(this.generateReportCheckBox);
         this.Controls.Add(this.simulationParameterControl);
         this.Name = "BatchStrategySimulatorDlg";
         this.Text = "BatchTrailingOrderSimulatorDlg";
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Button simulateTradingBtn;
      private System.Windows.Forms.CheckBox generateReportCheckBox;
      public SimulationParameterControl simulationParameterControl;

   }
}