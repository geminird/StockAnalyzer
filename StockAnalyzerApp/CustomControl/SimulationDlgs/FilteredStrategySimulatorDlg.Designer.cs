﻿using StockAnalyzerApp.CustomControl.SimulationDlgs;


namespace StockAnalyzerApp.CustomControl
{
   partial class FilteredStrategySimulatorDlg
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
            this.Portofolio = new System.Windows.Forms.Label();
            this.portofolioComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.stockComboBox = new System.Windows.Forms.ComboBox();
            this.simulateTradingBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.filterIndicatorTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.shortFilterComboBox = new System.Windows.Forms.ComboBox();
            this.buyFilterComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.shortTriggerComboBox = new System.Windows.Forms.ComboBox();
            this.buyTriggerComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.entryTriggerIndicatorTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.simulationParameterControl = new FilteredSimulationParameterControl();
            this.strategyNameTextBox = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.coverTriggerComboBox = new System.Windows.Forms.ComboBox();
            this.sellTriggerComboBox = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.exitTriggerIndicatorTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.stopShortTriggerComboBox = new System.Windows.Forms.ComboBox();
            this.stopLongTriggerComboBox = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.stopTriggerIndicatorTextBox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // Portofolio
            // 
            this.Portofolio.AutoSize = true;
            this.Portofolio.Location = new System.Drawing.Point(12, 15);
            this.Portofolio.Name = "Portofolio";
            this.Portofolio.Size = new System.Drawing.Size(51, 13);
            this.Portofolio.TabIndex = 0;
            this.Portofolio.Text = "Portofolio";
            // 
            // portofolioComboBox
            // 
            this.portofolioComboBox.FormattingEnabled = true;
            this.portofolioComboBox.Location = new System.Drawing.Point(69, 12);
            this.portofolioComboBox.Name = "portofolioComboBox";
            this.portofolioComboBox.Size = new System.Drawing.Size(238, 21);
            this.portofolioComboBox.TabIndex = 2;
            this.portofolioComboBox.SelectedIndexChanged += new System.EventHandler(this.portofolioComboBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Stock";
            // 
            // stockComboBox
            // 
            this.stockComboBox.FormattingEnabled = true;
            this.stockComboBox.Location = new System.Drawing.Point(69, 39);
            this.stockComboBox.Name = "stockComboBox";
            this.stockComboBox.Size = new System.Drawing.Size(238, 21);
            this.stockComboBox.TabIndex = 2;
            this.stockComboBox.SelectedIndexChanged += new System.EventHandler(this.stockComboBox_SelectedIndexChanged);
            // 
            // simulateTradingBtn
            // 
            this.simulateTradingBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simulateTradingBtn.Location = new System.Drawing.Point(501, 559);
            this.simulateTradingBtn.Name = "simulateTradingBtn";
            this.simulateTradingBtn.Size = new System.Drawing.Size(99, 23);
            this.simulateTradingBtn.TabIndex = 4;
            this.simulateTradingBtn.Text = "Simulate Trading";
            this.simulateTradingBtn.UseVisualStyleBackColor = true;
            this.simulateTradingBtn.Click += new System.EventHandler(this.simulateTradingBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Indicator";
            // 
            // filterIndicatorTextBox
            // 
            this.filterIndicatorTextBox.Location = new System.Drawing.Point(70, 19);
            this.filterIndicatorTextBox.Name = "filterIndicatorTextBox";
            this.filterIndicatorTextBox.Size = new System.Drawing.Size(216, 20);
            this.filterIndicatorTextBox.TabIndex = 7;
            this.filterIndicatorTextBox.Leave += new System.EventHandler(this.filterIndicatorTextBox_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.shortFilterComboBox);
            this.groupBox1.Controls.Add(this.buyFilterComboBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.filterIndicatorTextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 418);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(292, 100);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter";
            // 
            // shortFilterComboBox
            // 
            this.shortFilterComboBox.FormattingEnabled = true;
            this.shortFilterComboBox.Location = new System.Drawing.Point(70, 73);
            this.shortFilterComboBox.Name = "shortFilterComboBox";
            this.shortFilterComboBox.Size = new System.Drawing.Size(216, 21);
            this.shortFilterComboBox.TabIndex = 10;
            // 
            // buyFilterComboBox
            // 
            this.buyFilterComboBox.FormattingEnabled = true;
            this.buyFilterComboBox.Location = new System.Drawing.Point(70, 49);
            this.buyFilterComboBox.Name = "buyFilterComboBox";
            this.buyFilterComboBox.Size = new System.Drawing.Size(216, 21);
            this.buyFilterComboBox.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Short if";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Buy if";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.shortTriggerComboBox);
            this.groupBox2.Controls.Add(this.buyTriggerComboBox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.entryTriggerIndicatorTextBox);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(12, 312);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(292, 100);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Entry";
            // 
            // shortTriggerComboBox
            // 
            this.shortTriggerComboBox.FormattingEnabled = true;
            this.shortTriggerComboBox.Location = new System.Drawing.Point(70, 73);
            this.shortTriggerComboBox.Name = "shortTriggerComboBox";
            this.shortTriggerComboBox.Size = new System.Drawing.Size(216, 21);
            this.shortTriggerComboBox.TabIndex = 10;
            // 
            // buyTriggerComboBox
            // 
            this.buyTriggerComboBox.FormattingEnabled = true;
            this.buyTriggerComboBox.Location = new System.Drawing.Point(70, 49);
            this.buyTriggerComboBox.Name = "buyTriggerComboBox";
            this.buyTriggerComboBox.Size = new System.Drawing.Size(216, 21);
            this.buyTriggerComboBox.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Short if";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Buy if";
            // 
            // entryTriggerIndicatorTextBox
            // 
            this.entryTriggerIndicatorTextBox.Location = new System.Drawing.Point(70, 19);
            this.entryTriggerIndicatorTextBox.Name = "entryTriggerIndicatorTextBox";
            this.entryTriggerIndicatorTextBox.Size = new System.Drawing.Size(216, 20);
            this.entryTriggerIndicatorTextBox.TabIndex = 7;
            this.entryTriggerIndicatorTextBox.Leave += new System.EventHandler(this.entryTriggerIndicatorTextBox_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Indicator";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(206, 559);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 9;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // simulationParameterControl
            // 
            this.simulationParameterControl.EndDate = new System.DateTime(((long)(0)));
            this.simulationParameterControl.Location = new System.Drawing.Point(1, 58);
            this.simulationParameterControl.Name = "simulationParameterControl";
            this.simulationParameterControl.SelectedStrategyName = null;
            this.simulationParameterControl.Size = new System.Drawing.Size(402, 487);
            this.simulationParameterControl.StartDate = new System.DateTime(((long)(0)));
            this.simulationParameterControl.TabIndex = 10;
            this.simulationParameterControl.SelectedStrategyChanged += new FilteredSimulationParameterControl.SelectedStrategyHandler(this.simulationParameterControl_SelectedStrategyChanged);
            // 
            // strategyNameTextBox
            // 
            this.strategyNameTextBox.Location = new System.Drawing.Point(12, 559);
            this.strategyNameTextBox.Name = "strategyNameTextBox";
            this.strategyNameTextBox.Size = new System.Drawing.Size(188, 20);
            this.strategyNameTextBox.TabIndex = 11;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.coverTriggerComboBox);
            this.groupBox3.Controls.Add(this.sellTriggerComboBox);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.exitTriggerIndicatorTextBox);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Location = new System.Drawing.Point(310, 312);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(292, 100);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Exit";
            // 
            // coverTriggerComboBox
            // 
            this.coverTriggerComboBox.FormattingEnabled = true;
            this.coverTriggerComboBox.Location = new System.Drawing.Point(70, 73);
            this.coverTriggerComboBox.Name = "coverTriggerComboBox";
            this.coverTriggerComboBox.Size = new System.Drawing.Size(216, 21);
            this.coverTriggerComboBox.TabIndex = 10;
            // 
            // sellTriggerComboBox
            // 
            this.sellTriggerComboBox.FormattingEnabled = true;
            this.sellTriggerComboBox.Location = new System.Drawing.Point(70, 49);
            this.sellTriggerComboBox.Name = "sellTriggerComboBox";
            this.sellTriggerComboBox.Size = new System.Drawing.Size(216, 21);
            this.sellTriggerComboBox.TabIndex = 10;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 77);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Cover if";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 52);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Sell if";
            // 
            // exitTriggerIndicatorTextBox
            // 
            this.exitTriggerIndicatorTextBox.Location = new System.Drawing.Point(70, 19);
            this.exitTriggerIndicatorTextBox.Name = "exitTriggerIndicatorTextBox";
            this.exitTriggerIndicatorTextBox.Size = new System.Drawing.Size(216, 20);
            this.exitTriggerIndicatorTextBox.TabIndex = 7;
            this.exitTriggerIndicatorTextBox.Leave += new System.EventHandler(this.exitTriggerIndicatorTextBox_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(48, 13);
            this.label10.TabIndex = 6;
            this.label10.Text = "Indicator";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.stopShortTriggerComboBox);
            this.groupBox4.Controls.Add(this.stopLongTriggerComboBox);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.stopTriggerIndicatorTextBox);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Location = new System.Drawing.Point(310, 418);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(292, 100);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Stop";
            // 
            // stopShortTriggerComboBox
            // 
            this.stopShortTriggerComboBox.FormattingEnabled = true;
            this.stopShortTriggerComboBox.Location = new System.Drawing.Point(70, 73);
            this.stopShortTriggerComboBox.Name = "stopShortTriggerComboBox";
            this.stopShortTriggerComboBox.Size = new System.Drawing.Size(216, 21);
            this.stopShortTriggerComboBox.TabIndex = 10;
            // 
            // stopLongTriggerComboBox
            // 
            this.stopLongTriggerComboBox.FormattingEnabled = true;
            this.stopLongTriggerComboBox.Location = new System.Drawing.Point(70, 49);
            this.stopLongTriggerComboBox.Name = "stopLongTriggerComboBox";
            this.stopLongTriggerComboBox.Size = new System.Drawing.Size(216, 21);
            this.stopLongTriggerComboBox.TabIndex = 10;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 77);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(43, 13);
            this.label11.TabIndex = 9;
            this.label11.Text = "Cover if";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 52);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(32, 13);
            this.label12.TabIndex = 8;
            this.label12.Text = "Sell if";
            // 
            // stopTriggerIndicatorTextBox
            // 
            this.stopTriggerIndicatorTextBox.Location = new System.Drawing.Point(70, 19);
            this.stopTriggerIndicatorTextBox.Name = "stopTriggerIndicatorTextBox";
            this.stopTriggerIndicatorTextBox.Size = new System.Drawing.Size(216, 20);
            this.stopTriggerIndicatorTextBox.TabIndex = 7;
            this.stopTriggerIndicatorTextBox.Leave += new System.EventHandler(this.stopTriggerIndicatorTextBox_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 22);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(48, 13);
            this.label13.TabIndex = 6;
            this.label13.Text = "Indicator";
            // 
            // FilteredStrategySimulatorDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 594);
            this.Controls.Add(this.strategyNameTextBox);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.simulateTradingBtn);
            this.Controls.Add(this.stockComboBox);
            this.Controls.Add(this.portofolioComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Portofolio);
            this.Controls.Add(this.simulationParameterControl);
            this.Name = "FilteredStrategySimulatorDlg";
            this.Text = "Filtered Strategy Simulation";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Label Portofolio;
      private System.Windows.Forms.ComboBox portofolioComboBox;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.ComboBox stockComboBox;
      private System.Windows.Forms.Button simulateTradingBtn;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.TextBox filterIndicatorTextBox;
      private System.Windows.Forms.GroupBox groupBox1;
      private System.Windows.Forms.ComboBox shortFilterComboBox;
      private System.Windows.Forms.ComboBox buyFilterComboBox;
      private System.Windows.Forms.Label label5;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.GroupBox groupBox2;
      private System.Windows.Forms.ComboBox shortTriggerComboBox;
      private System.Windows.Forms.ComboBox buyTriggerComboBox;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Label label6;
      private System.Windows.Forms.TextBox entryTriggerIndicatorTextBox;
      private System.Windows.Forms.Label label7;
      private System.Windows.Forms.Button saveButton;
      private FilteredSimulationParameterControl simulationParameterControl;
      private System.Windows.Forms.TextBox strategyNameTextBox;
      private System.Windows.Forms.GroupBox groupBox3;
      private System.Windows.Forms.ComboBox coverTriggerComboBox;
      private System.Windows.Forms.ComboBox sellTriggerComboBox;
      private System.Windows.Forms.Label label8;
      private System.Windows.Forms.Label label9;
      private System.Windows.Forms.TextBox exitTriggerIndicatorTextBox;
      private System.Windows.Forms.Label label10;
      private System.Windows.Forms.GroupBox groupBox4;
      private System.Windows.Forms.ComboBox stopShortTriggerComboBox;
      private System.Windows.Forms.ComboBox stopLongTriggerComboBox;
      private System.Windows.Forms.Label label11;
      private System.Windows.Forms.Label label12;
      private System.Windows.Forms.TextBox stopTriggerIndicatorTextBox;
      private System.Windows.Forms.Label label13;
   }
}