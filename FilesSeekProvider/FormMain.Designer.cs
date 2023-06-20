namespace FilesSeekProvider
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlSearch = new Panel();
            lblKeyword = new Label();
            lblPath = new Label();
            txtPath = new TextBox();
            txtKeyword = new TextBox();
            chkRegex = new CheckBox();
            btnSearch = new Button();
            pnlResult = new Panel();
            spResult = new SplitContainer();
            lstFiles = new ListView();
            lblResult = new TextBox();
            gvResult = new DataGridView();
            lblFilsContent = new Label();
            pnlInfo = new Panel();
            lblInfos = new Label();
            chkIgnoreCase = new CheckBox();
            pnlSearch.SuspendLayout();
            pnlResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)spResult).BeginInit();
            spResult.Panel1.SuspendLayout();
            spResult.Panel2.SuspendLayout();
            spResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gvResult).BeginInit();
            pnlInfo.SuspendLayout();
            SuspendLayout();
            // 
            // pnlSearch
            // 
            pnlSearch.Controls.Add(chkIgnoreCase);
            pnlSearch.Controls.Add(lblKeyword);
            pnlSearch.Controls.Add(lblPath);
            pnlSearch.Controls.Add(txtPath);
            pnlSearch.Controls.Add(txtKeyword);
            pnlSearch.Controls.Add(chkRegex);
            pnlSearch.Controls.Add(btnSearch);
            pnlSearch.Dock = DockStyle.Top;
            pnlSearch.Location = new Point(0, 0);
            pnlSearch.Name = "pnlSearch";
            pnlSearch.Size = new Size(1182, 60);
            pnlSearch.TabIndex = 0;
            // 
            // lblKeyword
            // 
            lblKeyword.AutoSize = true;
            lblKeyword.Location = new Point(31, 9);
            lblKeyword.Name = "lblKeyword";
            lblKeyword.Size = new Size(56, 15);
            lblKeyword.TabIndex = 8;
            lblKeyword.Text = "Keyword";
            // 
            // lblPath
            // 
            lblPath.AutoSize = true;
            lblPath.Location = new Point(31, 36);
            lblPath.Name = "lblPath";
            lblPath.Size = new Size(71, 15);
            lblPath.TabIndex = 7;
            lblPath.Text = "Folder Path";
            // 
            // txtPath
            // 
            txtPath.Location = new Point(124, 33);
            txtPath.Name = "txtPath";
            txtPath.PlaceholderText = "[F3]Folder Path";
            txtPath.Size = new Size(777, 23);
            txtPath.TabIndex = 6;
            txtPath.Click += txtPath_Click;
            // 
            // txtKeyword
            // 
            txtKeyword.Location = new Point(124, 5);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.PlaceholderText = "[ESC]Keyword";
            txtKeyword.Size = new Size(777, 23);
            txtKeyword.TabIndex = 4;
            txtKeyword.KeyPress += txtKeyword_KeyPress;
            // 
            // chkRegex
            // 
            chkRegex.AutoSize = true;
            chkRegex.Location = new Point(906, 7);
            chkRegex.Margin = new Padding(0);
            chkRegex.Name = "chkRegex";
            chkRegex.Size = new Size(118, 19);
            chkRegex.TabIndex = 5;
            chkRegex.Text = "[F2]RegexMode";
            chkRegex.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(1082, 15);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(72, 25);
            btnSearch.TabIndex = 1;
            btnSearch.Text = "[F4]Seek!";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // pnlResult
            // 
            pnlResult.Controls.Add(spResult);
            pnlResult.Controls.Add(lblFilsContent);
            pnlResult.Dock = DockStyle.Fill;
            pnlResult.Location = new Point(0, 60);
            pnlResult.Name = "pnlResult";
            pnlResult.Size = new Size(1182, 430);
            pnlResult.TabIndex = 1;
            // 
            // spResult
            // 
            spResult.Dock = DockStyle.Fill;
            spResult.Location = new Point(0, 0);
            spResult.Name = "spResult";
            // 
            // spResult.Panel1
            // 
            spResult.Panel1.Controls.Add(lstFiles);
            // 
            // spResult.Panel2
            // 
            spResult.Panel2.Controls.Add(lblResult);
            spResult.Panel2.Controls.Add(gvResult);
            spResult.Size = new Size(1182, 430);
            spResult.SplitterDistance = 340;
            spResult.TabIndex = 6;
            // 
            // lstFiles
            // 
            lstFiles.Alignment = ListViewAlignment.Default;
            lstFiles.Dock = DockStyle.Fill;
            lstFiles.GridLines = true;
            lstFiles.Location = new Point(0, 0);
            lstFiles.MultiSelect = false;
            lstFiles.Name = "lstFiles";
            lstFiles.Size = new Size(340, 430);
            lstFiles.TabIndex = 1;
            lstFiles.UseCompatibleStateImageBehavior = false;
            lstFiles.View = View.List;
            lstFiles.SelectedIndexChanged += lstFiles_SelectedIndexChanged;
            // 
            // lblResult
            // 
            lblResult.Dock = DockStyle.Fill;
            lblResult.Location = new Point(0, 0);
            lblResult.Multiline = true;
            lblResult.Name = "lblResult";
            lblResult.ReadOnly = true;
            lblResult.ScrollBars = ScrollBars.Both;
            lblResult.Size = new Size(838, 430);
            lblResult.TabIndex = 4;
            lblResult.WordWrap = false;
            // 
            // gvResult
            // 
            gvResult.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gvResult.Dock = DockStyle.Fill;
            gvResult.Location = new Point(0, 0);
            gvResult.Name = "gvResult";
            gvResult.RowTemplate.Height = 25;
            gvResult.Size = new Size(838, 430);
            gvResult.TabIndex = 5;
            // 
            // lblFilsContent
            // 
            lblFilsContent.Location = new Point(375, 164);
            lblFilsContent.Name = "lblFilsContent";
            lblFilsContent.Size = new Size(100, 23);
            lblFilsContent.TabIndex = 2;
            lblFilsContent.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlInfo
            // 
            pnlInfo.Controls.Add(lblInfos);
            pnlInfo.Dock = DockStyle.Bottom;
            pnlInfo.Location = new Point(0, 490);
            pnlInfo.Name = "pnlInfo";
            pnlInfo.Size = new Size(1182, 25);
            pnlInfo.TabIndex = 4;
            // 
            // lblInfos
            // 
            lblInfos.BorderStyle = BorderStyle.FixedSingle;
            lblInfos.Dock = DockStyle.Fill;
            lblInfos.Location = new Point(0, 0);
            lblInfos.Name = "lblInfos";
            lblInfos.Size = new Size(1182, 25);
            lblInfos.TabIndex = 0;
            lblInfos.TextAlign = ContentAlignment.MiddleLeft;
            lblInfos.Click += lblInfos_Click;
            // 
            // chkIgnoreCase
            // 
            chkIgnoreCase.AutoSize = true;
            chkIgnoreCase.Location = new Point(906, 35);
            chkIgnoreCase.Margin = new Padding(0);
            chkIgnoreCase.Name = "chkIgnoreCase";
            chkIgnoreCase.Size = new Size(114, 19);
            chkIgnoreCase.TabIndex = 9;
            chkIgnoreCase.Text = "[F1]Ignore Case";
            chkIgnoreCase.UseVisualStyleBackColor = true;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1182, 515);
            Controls.Add(pnlResult);
            Controls.Add(pnlInfo);
            Controls.Add(pnlSearch);
            Name = "FormMain";
            Text = "MultiSeek";
            pnlSearch.ResumeLayout(false);
            pnlSearch.PerformLayout();
            pnlResult.ResumeLayout(false);
            spResult.Panel1.ResumeLayout(false);
            spResult.Panel2.ResumeLayout(false);
            spResult.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)spResult).EndInit();
            spResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gvResult).EndInit();
            pnlInfo.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlSearch;
        private Button btnSearch;
        private Panel pnlResult;
        private ListView lstFiles;
        private StatusStrip stInfo;
        private Label lblFilsContent;
        private TextBox txtKeyword;
        private Panel pnlInfo;
        private Label lblInfos;
        private CheckBox chkRegex;
        private TextBox lblResult;
        private DataGridView gvResult;
        private TextBox txtPath;
        private Label lblKeyword;
        private Label lblPath;
        private SplitContainer spResult;
        private CheckBox chkIgnoreCase;
    }
}