﻿using System.Windows.Forms;
namespace StockAnalyzerApp.CustomControl.GraphControls
{
    partial class GraphCloseControl
    {
        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        protected override void InitializeComponent()
        {
            this.SuspendLayout();
            base.InitializeComponent();

            this.contextMenu = new System.Windows.Forms.ContextMenu();
            this.buyMenu = new System.Windows.Forms.MenuItem();
            this.sellMenu = new System.Windows.Forms.MenuItem();
            this.shortMenu = new System.Windows.Forms.MenuItem();
            this.coverMenu = new System.Windows.Forms.MenuItem();
            this.deleteOperationMenu = new System.Windows.Forms.MenuItem();
            this.commentMenu = new System.Windows.Forms.MenuItem();
            this.agendaMenu = new MenuItem();
            this.openInABCMenu = new MenuItem();
            this.openInPEAPerfMenu = new MenuItem();
            this.openInZBMenu = new MenuItem();
            this.openInSocGenMenu = new MenuItem();
            this.statMenu = new MenuItem();
            this.separator1 = new MenuItem();
            this.separator2 = new MenuItem();
            // 
            // contextMenu
            // 
            this.contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.buyMenu,
            this.sellMenu,
            this.shortMenu,
            this.coverMenu,
            this.deleteOperationMenu,
            this.separator1,
            this.agendaMenu,
            this.commentMenu,
            this.separator2,
            this.openInABCMenu,
            this.openInPEAPerfMenu,
            this.openInZBMenu,
            this.openInSocGenMenu,
            this.statMenu});
            // 
            // buyMenu
            // 
            this.buyMenu.Index = 0;
            this.buyMenu.Text = "Buy";
            this.buyMenu.Click += new System.EventHandler(this.buyMenu_Click);
            // 
            // sellMenu
            // 
            this.sellMenu.Index = 1;
            this.sellMenu.Text = "Sell";
            this.sellMenu.Click += new System.EventHandler(this.sellMenu_Click);
            // 
            // shortMenu
            // 
            this.shortMenu.Index = 2;
            this.shortMenu.Text = "Short";
            this.shortMenu.Click += new System.EventHandler(this.shortMenu_Click);
            // 
            // coverMenu
            // 
            this.coverMenu.Index = 3;
            this.coverMenu.Text = "Cover";
            this.coverMenu.Click += new System.EventHandler(this.coverMenu_Click);
            // 
            // deleteOperationMenu
            // 
            this.deleteOperationMenu.Index = 4;
            this.deleteOperationMenu.Text = "Delete";
            this.deleteOperationMenu.Click += new System.EventHandler(this.deleteOperationMenu_Click);
            // 
            // separator1
            // 
            this.separator1.Index = 5;
            this.separator1.Text = "-";
            // 
            // commentMenu
            // 
            this.commentMenu.Index = 6;
            this.commentMenu.Text = "Add Comment";
            this.commentMenu.Click += new System.EventHandler(this.commentMenu_Click);
            // 
            // agendaMenu
            // 
            this.agendaMenu.Index = 7;
            this.agendaMenu.Text = "Agenda";
            this.agendaMenu.Click += new System.EventHandler(this.agendaMenu_Click);
            // 
            // separator2
            // 
            this.separator2.Index = 8;
            this.separator2.Text = "-";
            // 
            // openInABCMenu
            // 
            this.openInABCMenu.Index = 9;
            this.openInABCMenu.Text = "Open in ABCBourse";
            this.openInABCMenu.Click += new System.EventHandler(this.openInABCMenu_Click);
            // 
            // openInPEAPerf
            // 
            this.openInPEAPerfMenu.Index = 9;
            this.openInPEAPerfMenu.Text = "Open in PEAPerformance";
            this.openInPEAPerfMenu.Click += new System.EventHandler(this.openInPEAPerf_Click);
            // 
            // openInZBMenu
            // 
            this.openInZBMenu.Index = 10;
            this.openInZBMenu.Text = "Open in Zone Bourse";
            this.openInZBMenu.Click += new System.EventHandler(this.openInZBMenu_Click);
            // 
            // openInSocGenMenu
            // 
            this.openInSocGenMenu.Index = 10;
            this.openInSocGenMenu.Text = "Open in SogGen Products";
            this.openInSocGenMenu.Click += new System.EventHandler(this.openInSocGenMenu_Click);
            // 
            // statMenu
            // 
            this.statMenu.Index = 11;
            this.statMenu.Text = "Make Stats";
            this.statMenu.Click += new System.EventHandler(this.statMenu_Click);
            // 
            // GraphCloseControl
            // 
            this.Name = "GraphCloseControl";
            this.ResumeLayout(false);

        }

        private ContextMenu contextMenu;
        private MenuItem buyMenu;
        private MenuItem sellMenu;
        private MenuItem shortMenu;
        private MenuItem coverMenu;
        private MenuItem deleteOperationMenu;
        private MenuItem separator1;
        private MenuItem separator2;
        private MenuItem commentMenu;
        private MenuItem agendaMenu;
        private MenuItem openInABCMenu;
        private MenuItem openInPEAPerfMenu;
        private MenuItem openInZBMenu;
        private MenuItem openInSocGenMenu;
        private MenuItem statMenu;

        #endregion

    }
}
