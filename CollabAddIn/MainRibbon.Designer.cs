namespace CollabAddIn
{
    partial class SBMainRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public SBMainRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.sbTab = this.Factory.CreateRibbonTab();
            this.collabGroup = this.Factory.CreateRibbonGroup();
            this.box1 = this.Factory.CreateRibbonBox();
            this.btnShowActionItems = this.Factory.CreateRibbonToggleButton();
            this.btnUpload = this.Factory.CreateRibbonButton();
            this.btnOptions = this.Factory.CreateRibbonButton();
            this.sbTab.SuspendLayout();
            this.collabGroup.SuspendLayout();
            this.box1.SuspendLayout();
            // 
            // sbTab
            // 
            this.sbTab.Groups.Add(this.collabGroup);
            this.sbTab.Label = "SmartBear";
            this.sbTab.Name = "sbTab";
            // 
            // collabGroup
            // 
            this.collabGroup.Items.Add(this.box1);
            this.collabGroup.Items.Add(this.btnShowActionItems);
            this.collabGroup.Items.Add(this.btnOptions);
            this.collabGroup.Label = "Collaborator";
            this.collabGroup.Name = "collabGroup";
            // 
            // box1
            // 
            this.box1.BoxStyle = Microsoft.Office.Tools.Ribbon.RibbonBoxStyle.Vertical;
            this.box1.Items.Add(this.btnUpload);
            this.box1.Name = "box1";
            // 
            // btnShowActionItems
            // 
            this.btnShowActionItems.Description = "Toggle the action item pane.";
            this.btnShowActionItems.Label = "Show Action Items";
            this.btnShowActionItems.Name = "btnShowActionItems";
            this.btnShowActionItems.ScreenTip = "Toggle the action item pane.";
            this.btnShowActionItems.SuperTip = "Toggle the action item pane.";
            this.btnShowActionItems.Visible = false;
            // 
            // btnUpload
            // 
            this.btnUpload.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.btnUpload.Description = "Upload this document to Collaborator for review.";
            this.btnUpload.Enabled = false;
            this.btnUpload.Image = global::CollabAddIn.Properties.Resources.CCLogo;
            this.btnUpload.Label = "Upload to Review";
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.ScreenTip = "Upload this document to Collaborator for review.";
            this.btnUpload.ShowImage = true;
            this.btnUpload.SuperTip = "Upload this document to Collaborator for review.";
            this.btnUpload.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnUpload_Click);
            // 
            // btnOptions
            // 
            this.btnOptions.Description = "Configure options for the Collaborator plugin.";
            this.btnOptions.Image = global::CollabAddIn.Properties.Resources.wrench;
            this.btnOptions.Label = "Options";
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.ScreenTip = "Configure options for the Collaborator plugin.";
            this.btnOptions.ShowImage = true;
            this.btnOptions.SuperTip = "Configure options for the Collaborator plugin.";
            this.btnOptions.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnOptions_Click);
            // 
            // SBMainRibbon
            // 
            this.Name = "SBMainRibbon";
            this.RibbonType = "Microsoft.Excel.Workbook, Microsoft.Word.Document";
            this.Tabs.Add(this.sbTab);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.MainRibbon_Load);
            this.sbTab.ResumeLayout(false);
            this.sbTab.PerformLayout();
            this.collabGroup.ResumeLayout(false);
            this.collabGroup.PerformLayout();
            this.box1.ResumeLayout(false);
            this.box1.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonGroup collabGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnUpload;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnOptions;
        public Microsoft.Office.Tools.Ribbon.RibbonTab sbTab;
        internal Microsoft.Office.Tools.Ribbon.RibbonBox box1;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton btnShowActionItems;
    }

    partial class ThisRibbonCollection
    {
        internal SBMainRibbon MainRibbon
        {
            get { return this.GetRibbon<SBMainRibbon>(); }
        }
    }
}
