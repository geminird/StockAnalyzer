﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockAnalyzerApp.CustomControl.MultiTimeFrameDlg
{
   public partial class MTFDlg : Form
   {
      public MTFDlg()
      {
         InitializeComponent();
      }

      public MTFUserControl MtfControl
      {
         get { return mtfUserControl1; }
      }
   }
}
