﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StockAnalyzerSettings.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
    public sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("EventAll")]
        public string EventFilterMode {
            get {
                return ((string)(this["EventFilterMode"]));
            }
            set {
                this["EventFilterMode"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Data")]
        public string InputDataFolder {
            get {
                return ((string)(this["InputDataFolder"]));
            }
            set {
                this["InputDataFolder"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string AnalysisFile {
            get {
                return ((string)(this["AnalysisFile"]));
            }
            set {
                this["AnalysisFile"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("INDICES")]
        public string SelectedGroup {
            get {
                return ((string)(this["SelectedGroup"]));
            }
            set {
                this["SelectedGroup"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("OrderFile.xml")]
        public string PortofolioFile {
            get {
                return ((string)(this["PortofolioFile"]));
            }
            set {
                this["PortofolioFile"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool ShowSummaryOrders {
            get {
                return ((bool)(this["ShowSummaryOrders"]));
            }
            set {
                this["ShowSummaryOrders"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool ShowOrders {
            get {
                return ((bool)(this["ShowOrders"]));
            }
            set {
                this["ShowOrders"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool ShowDrawings {
            get {
                return ((bool)(this["ShowDrawings"]));
            }
            set {
                this["ShowDrawings"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool DownloadData {
            get {
                return ((bool)(this["DownloadData"]));
            }
            set {
                this["DownloadData"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool ShowEventMarquee {
            get {
                return ((bool)(this["ShowEventMarquee"]));
            }
            set {
                this["ShowEventMarquee"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string SelectedEvents {
            get {
                return ((string)(this["SelectedEvents"]));
            }
            set {
                this["SelectedEvents"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string EventMarquees {
            get {
                return ((string)(this["EventMarquees"]));
            }
            set {
                this["EventMarquees"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("EventAll")]
        public string EventMarqueeMode {
            get {
                return ((string)(this["EventMarqueeMode"]));
            }
            set {
                this["EventMarqueeMode"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Default")]
        public string SelectedTheme {
            get {
                return ((string)(this["SelectedTheme"]));
            }
            set {
                this["SelectedTheme"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string RootFolder {
            get {
                return ((string)(this["RootFolder"]));
            }
            set {
                this["RootFolder"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2012-01-01")]
        public global::System.DateTime DownloadStartDate {
            get {
                return ((global::System.DateTime)(this["DownloadStartDate"]));
            }
            set {
                this["DownloadStartDate"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int BarType {
            get {
                return ((int)(this["BarType"]));
            }
            set {
                this["BarType"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool SupportIntraday {
            get {
                return ((bool)(this["SupportIntraday"]));
            }
            set {
                this["SupportIntraday"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool GenerateBreadth {
            get {
                return ((bool)(this["GenerateBreadth"]));
            }
            set {
                this["GenerateBreadth"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("-1, -1")]
        public global::System.Drawing.Point ThemeToolbarLocation {
            get {
                return ((global::System.Drawing.Point)(this["ThemeToolbarLocation"]));
            }
            set {
                this["ThemeToolbarLocation"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("-1, -1")]
        public global::System.Drawing.Point StockToolbarLocation {
            get {
                return ((global::System.Drawing.Point)(this["StockToolbarLocation"]));
            }
            set {
                this["StockToolbarLocation"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("-1, -1")]
        public global::System.Drawing.Point drawingToolbarLocation {
            get {
                return ((global::System.Drawing.Point)(this["drawingToolbarLocation"]));
            }
            set {
                this["drawingToolbarLocation"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("16777215,16777215,16777215,16777215,16777215,16777215,16777215,16777215,16777215," +
            "16777215,16777215,16777215,16777215,16777215,16777215,16777215")]
        public string CustomColors {
            get {
                return ((string)(this["CustomColors"]));
            }
            set {
                this["CustomColors"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool LoggingEnabled {
            get {
                return ((bool)(this["LoggingEnabled"]));
            }
            set {
                this["LoggingEnabled"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool ShowStatusBar {
            get {
                return ((bool)(this["ShowStatusBar"]));
            }
            set {
                this["ShowStatusBar"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string UserId {
            get {
                return ((string)(this["UserId"]));
            }
            set {
                this["UserId"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string MachineID {
            get {
                return ((string)(this["MachineID"]));
            }
            set {
                this["MachineID"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1:255:0:0:0:Solid")]
        public string DrawingPen {
            get {
                return ((string)(this["DrawingPen"]));
            }
            set {
                this["DrawingPen"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool ShowVariation {
            get {
                return ((bool)(this["ShowVariation"]));
            }
            set {
                this["ShowVariation"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("100")]
        public int DefaultBarNumber {
            get {
                return ((int)(this["DefaultBarNumber"]));
            }
            set {
                this["DefaultBarNumber"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool ShowCommentMarquee {
            get {
                return ((bool)(this["ShowCommentMarquee"]));
            }
            set {
                this["ShowCommentMarquee"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("david.carbonel@free.fr")]
        public string UserEMail {
            get {
                return ((string)(this["UserEMail"]));
            }
            set {
                this["UserEMail"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("smtp.free.fr")]
        public string UserSMTP {
            get {
                return ((string)(this["UserSMTP"]));
            }
            set {
                this["UserSMTP"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool SupportShortSelling {
            get {
                return ((bool)(this["SupportShortSelling"]));
            }
            set {
                this["SupportShortSelling"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2011-01-03")]
        public global::System.DateTime StrategyStartDate {
            get {
                return ((global::System.DateTime)(this["StrategyStartDate"]));
            }
            set {
                this["StrategyStartDate"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("16000")]
        public float PortofolioValue {
            get {
                return ((float)(this["PortofolioValue"]));
            }
            set {
                this["PortofolioValue"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ROC(120,1)")]
        public string MomentumIndicator {
            get {
                return ((string)(this["MomentumIndicator"]));
            }
            set {
                this["MomentumIndicator"] = value;
            }
        }
    }
}
