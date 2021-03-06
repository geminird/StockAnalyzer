﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StockAnalyzerApp.GraphControls;
using StockAnalyzer.StockClasses;
using StockAnalyzer.StockDrawing;

namespace StockAnalyzerApp.CustomControl
{
    public partial class StockDataSelectorDialog : Form
    {
        public GraphCurveTypeList CurveList { get; set; }

        public StockDataSelectorDialog()
        {
            InitializeComponent();
        }

        private void StockDataSelectorDialog_Load(object sender, EventArgs e)
        {
            this.dataSelectionList.CheckBoxes = true;

            string[] thicknesses = new string[] {
                "1", "2", "3"};

            bool found = false;
            float thickness = 1.0f;
            Color color;
            foreach (StockDataType dataType in Enum.GetValues(typeof(StockDataType)))
            {
                found = false;
                thickness = 1.0f;
                color = Color.Black;
                foreach (GraphCurveType graphCurveType in CurveList)
                {
                    if (graphCurveType.CurveDataType == dataType)
                    {
                        found = true;
                        thickness = graphCurveType.CurvePen.Width;
                        color = graphCurveType.CurvePen.Color;
                        break;
                    }
                }
                ListViewItem listViewItem = new ListViewItem(new string[] {
                dataType.ToString(),
                thickness.ToString(),
                color.ToArgb().ToString(),
                ""});
                listViewItem.Checked = found;
                this.dataSelectionList.Items.Add(listViewItem);
            }
            this.dataSelectionList.AddComboBoxCell(-1, 1, thicknesses);
        }
        private void dataSelectionList_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            Color color;
            switch (e.ColumnIndex)
            {
                case 2:
                    color = Color.FromArgb(int.Parse(e.SubItem.Text));

                    Rectangle rect = e.Bounds;
                    rect.Inflate(-4, -2);

                    using (SolidBrush brush = new SolidBrush(color))
                    {
                        e.Graphics.FillRectangle(brush, rect);
                        e.Graphics.DrawRectangle(Pens.Black, rect);
                    }
                    break;
                case 3:
                    float x1 = e.SubItem.Bounds.X;
                    float x2 = e.SubItem.Bounds.X + e.SubItem.Bounds.Width;
                    float y = e.SubItem.Bounds.Y + e.SubItem.Bounds.Height / 2;

                    color = Color.FromArgb(int.Parse(e.Item.SubItems[2].Text));
                    float thickness = float.Parse(e.Item.SubItems[1].Text);

                    using (Pen pen = new Pen(color, thickness))
                    {
                        e.Graphics.DrawLine(pen, x1, y, x2, y);
                    }
                    break;
                default:
                    e.DrawDefault = true;
                    break;
            }
        }
        private void dataSelectionList_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
        }
        private void dataSelectionList_MouseClick(object sender, MouseEventArgs e)
        {
            ListViewItem viewItem = this.dataSelectionList.GetItemAt(e.X, e.Y);
            int index = this.dataSelectionList.GetSubItemIndexAt(e.X, e.Y);
            if (index == 2)
            {
                ColorDialog colorDlg = new ColorDialog();
                colorDlg.Color = Color.FromArgb(int.Parse(viewItem.SubItems[index].Text));
                colorDlg.ShowDialog();
                viewItem.SubItems[index].Text = colorDlg.Color.ToArgb().ToString();
            }
        }

        void OKBtn_Click(object sender, System.EventArgs e)
        {
            // Store new selection
            StockDataType currentType;
            GraphCurveType curveType;
            foreach (ListViewItem viewItem in this.dataSelectionList.Items)
            {
                currentType = (StockDataType)Enum.Parse(typeof(StockDataType), viewItem.Text);
                int index = GetGraphCurveTypeIndex(currentType);
                if (viewItem.Checked)
                {
                    Color color = Color.FromArgb(int.Parse(viewItem.SubItems[2].Text));
                    float thickness = float.Parse(viewItem.SubItems[1].Text);
                    // Current element is selected
                    if (index == -1)
                    {
                        // Add new curve
                        Pen pen = new Pen(color, thickness);
                        curveType = new GraphCurveType(pen, currentType, true, true);
                        CurveList.Add(curveType);
                    }
                    else
                    {
                        // Update existing curve
                        curveType = CurveList[index];
                        curveType.CurvePen.Color = color;
                        curveType.CurvePen.Width = thickness;
                    }
                }
                else
                {
                    // Current element not selected need to be removed if exist in current configuration
                    if (index != -1)
                    {
                        CurveList.RemoveAt(index);
                    }
                }
            }


            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private int GetGraphCurveTypeIndex(StockDataType dataType)
        {
            int index = 0;
            foreach (GraphCurveType graphCurveType in CurveList)
            {
                if (graphCurveType.CurveDataType == dataType)
                {
                    return index;
                }
                index++;
            }
            return -1;
        }
    }
}
