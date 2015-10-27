namespace CollabAddIn
{
    partial class ReviewSelectDlg
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.tbComment = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.wrkrSuggestedReviews = new System.ComponentModel.BackgroundWorker();
            this.pbLoading = new System.Windows.Forms.PictureBox();
            this.pnlLoading = new System.Windows.Forms.Panel();
            this.lblLoading = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblExistingReviews = new System.Windows.Forms.Label();
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.lbSuggestedReviews = new System.Windows.Forms.ListBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tbReviewTitle = new System.Windows.Forms.TextBox();
            this.rBtnExistingReview = new System.Windows.Forms.RadioButton();
            this.rBtnNewReview = new System.Windows.Forms.RadioButton();
            this.lnkServerLink = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLoading)).BeginInit();
            this.pnlLoading.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CollabAddIn.Properties.Resources.CCLogoBig;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(230, 50);
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(470, 433);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnUpload
            // 
            this.btnUpload.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnUpload.Enabled = false;
            this.btnUpload.Location = new System.Drawing.Point(551, 433);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(75, 23);
            this.btnUpload.TabIndex = 9;
            this.btnUpload.Text = "Upload";
            this.btnUpload.UseVisualStyleBackColor = true;
            // 
            // tbComment
            // 
            this.tbComment.Location = new System.Drawing.Point(96, 435);
            this.tbComment.Name = "tbComment";
            this.tbComment.Size = new System.Drawing.Size(348, 20);
            this.tbComment.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 438);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Comment:";
            // 
            // wrkrSuggestedReviews
            // 
            this.wrkrSuggestedReviews.DoWork += new System.ComponentModel.DoWorkEventHandler(this.wrkrSuggestedReviews_DoWork);
            this.wrkrSuggestedReviews.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.wrkrSuggestedReviews_RunWorkerCompleted);
            // 
            // pbLoading
            // 
            this.pbLoading.ErrorImage = null;
            this.pbLoading.Image = global::CollabAddIn.Properties.Resources.loadingIndicator;
            this.pbLoading.Location = new System.Drawing.Point(42, 9);
            this.pbLoading.Name = "pbLoading";
            this.pbLoading.Size = new System.Drawing.Size(16, 11);
            this.pbLoading.TabIndex = 25;
            this.pbLoading.TabStop = false;
            // 
            // pnlLoading
            // 
            this.pnlLoading.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLoading.Controls.Add(this.lblLoading);
            this.pnlLoading.Controls.Add(this.pbLoading);
            this.pnlLoading.Location = new System.Drawing.Point(344, 0);
            this.pnlLoading.Name = "pnlLoading";
            this.pnlLoading.Size = new System.Drawing.Size(100, 50);
            this.pnlLoading.TabIndex = 26;
            this.pnlLoading.Visible = false;
            // 
            // lblLoading
            // 
            this.lblLoading.AutoSize = true;
            this.lblLoading.Location = new System.Drawing.Point(23, 28);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(54, 13);
            this.lblLoading.TabIndex = 26;
            this.lblLoading.Text = "Loading...";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblSearch);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblExistingReviews);
            this.groupBox1.Controls.Add(this.tbSearch);
            this.groupBox1.Controls.Add(this.lbSuggestedReviews);
            this.groupBox1.Controls.Add(this.lblTitle);
            this.groupBox1.Controls.Add(this.tbReviewTitle);
            this.groupBox1.Controls.Add(this.rBtnExistingReview);
            this.groupBox1.Controls.Add(this.rBtnNewReview);
            this.groupBox1.Location = new System.Drawing.Point(-9, 68);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(667, 347);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(409, 125);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(44, 13);
            this.lblSearch.TabIndex = 37;
            this.lblSearch.Text = "Search:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(380, 18);
            this.label2.TabIndex = 36;
            this.label2.Text = "Which review would you like to upload this document to?";
            // 
            // lblExistingReviews
            // 
            this.lblExistingReviews.AutoSize = true;
            this.lblExistingReviews.Location = new System.Drawing.Point(54, 125);
            this.lblExistingReviews.Name = "lblExistingReviews";
            this.lblExistingReviews.Size = new System.Drawing.Size(164, 13);
            this.lblExistingReviews.TabIndex = 35;
            this.lblExistingReviews.Text = "Existing Reviews you can add to:";
            // 
            // tbSearch
            // 
            this.tbSearch.Location = new System.Drawing.Point(459, 122);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(175, 20);
            this.tbSearch.TabIndex = 34;
            this.tbSearch.TextChanged += new System.EventHandler(this.tbSearch_TextChanged);
            // 
            // lbSuggestedReviews
            // 
            this.lbSuggestedReviews.FormattingEnabled = true;
            this.lbSuggestedReviews.Location = new System.Drawing.Point(57, 145);
            this.lbSuggestedReviews.Name = "lbSuggestedReviews";
            this.lbSuggestedReviews.Size = new System.Drawing.Size(577, 186);
            this.lbSuggestedReviews.TabIndex = 33;
            this.lbSuggestedReviews.SelectedIndexChanged += new System.EventHandler(this.lbSuggestedReviews_SelectedIndexChanged);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(54, 71);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(30, 13);
            this.lblTitle.TabIndex = 32;
            this.lblTitle.Text = "Title:";
            // 
            // tbReviewTitle
            // 
            this.tbReviewTitle.Location = new System.Drawing.Point(90, 68);
            this.tbReviewTitle.Name = "tbReviewTitle";
            this.tbReviewTitle.Size = new System.Drawing.Size(544, 20);
            this.tbReviewTitle.TabIndex = 31;
            this.tbReviewTitle.TextChanged += new System.EventHandler(this.tbReviewTitle_TextChanged);
            // 
            // rBtnExistingReview
            // 
            this.rBtnExistingReview.AutoSize = true;
            this.rBtnExistingReview.Location = new System.Drawing.Point(23, 103);
            this.rBtnExistingReview.Name = "rBtnExistingReview";
            this.rBtnExistingReview.Size = new System.Drawing.Size(134, 17);
            this.rBtnExistingReview.TabIndex = 30;
            this.rBtnExistingReview.TabStop = true;
            this.rBtnExistingReview.Text = "Add to Existing Review";
            this.rBtnExistingReview.UseVisualStyleBackColor = true;
            this.rBtnExistingReview.CheckedChanged += new System.EventHandler(this.rBtnExistingReview_CheckedChanged);
            // 
            // rBtnNewReview
            // 
            this.rBtnNewReview.AutoSize = true;
            this.rBtnNewReview.Location = new System.Drawing.Point(23, 48);
            this.rBtnNewReview.Name = "rBtnNewReview";
            this.rBtnNewReview.Size = new System.Drawing.Size(120, 17);
            this.rBtnNewReview.TabIndex = 29;
            this.rBtnNewReview.TabStop = true;
            this.rBtnNewReview.Text = "Create New Review";
            this.rBtnNewReview.UseVisualStyleBackColor = true;
            this.rBtnNewReview.CheckedChanged += new System.EventHandler(this.rBtnNewReview_CheckedChanged);
            // 
            // lnkServerLink
            // 
            this.lnkServerLink.Location = new System.Drawing.Point(248, 43);
            this.lnkServerLink.Name = "lnkServerLink";
            this.lnkServerLink.Size = new System.Drawing.Size(377, 23);
            this.lnkServerLink.TabIndex = 30;
            this.lnkServerLink.TabStop = true;
            this.lnkServerLink.Text = "server link";
            this.lnkServerLink.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lnkServerLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkServerLink_LinkClicked);
            // 
            // ReviewSelectDlg
            // 
            this.AcceptButton = this.btnUpload;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(656, 465);
            this.Controls.Add(this.lnkServerLink);
            this.Controls.Add(this.pnlLoading);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbComment);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUpload);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReviewSelectDlg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add File to Review";
            this.Load += new System.EventHandler(this.ReviewSelectDlg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLoading)).EndInit();
            this.pnlLoading.ResumeLayout(false);
            this.pnlLoading.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnUpload;
        public System.Windows.Forms.TextBox tbComment;
        private System.Windows.Forms.Label label3;
        private System.ComponentModel.BackgroundWorker wrkrSuggestedReviews;
        private System.Windows.Forms.PictureBox pbLoading;
        private System.Windows.Forms.Panel pnlLoading;
        private System.Windows.Forms.Label lblLoading;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblExistingReviews;
        public System.Windows.Forms.TextBox tbSearch;
        private System.Windows.Forms.ListBox lbSuggestedReviews;
        private System.Windows.Forms.Label lblTitle;
        public System.Windows.Forms.TextBox tbReviewTitle;
        private System.Windows.Forms.RadioButton rBtnExistingReview;
        private System.Windows.Forms.RadioButton rBtnNewReview;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.LinkLabel lnkServerLink;
    }
}