using FilesSeekProvider.Compoment;

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
            chkIgnoreCase = new CheckBox();
            lblKeyword = new Label();
            lblPath = new Label();
            txtPath = new TextBox();
            txtKeyword = new TextBox();
            chkRegex = new CheckBox();
            btnSearch = new Button();
            pnlResult = new Panel();
            lblResult = new TextBox();
            rtxResult = new ExtRichTextBox();
            lstResultRow = new ListView();
            columnRow = new ColumnHeader();
            txtFilter = new TextBox();
            lblFilsContent = new Label();
            spResult = new SplitContainer();
            lstFiles = new ListView();
            columnResult = new ColumnHeader();
            pnlInfo = new Panel();
            lblInfos = new Label();
            pnlSearch.SuspendLayout();
            pnlResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)spResult).BeginInit();
            spResult.Panel1.SuspendLayout();
            spResult.Panel2.SuspendLayout();
            spResult.SuspendLayout();
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
            pnlSearch.Size = new Size(1382, 60);
            pnlSearch.TabIndex = 0;
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
            txtPath.PlaceholderText = "[F4]Folder Path";
            txtPath.Size = new Size(777, 23);
            txtPath.TabIndex = 6;
            txtPath.Click += txtPath_Click;
            // 
            // txtKeyword
            // 
            txtKeyword.Location = new Point(124, 5);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.PlaceholderText = "[F3]Keyword";
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
            btnSearch.Text = "[F5]Seek!";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // pnlResult
            // 
            pnlResult.Controls.Add(lblResult);
            pnlResult.Controls.Add(rtxResult);
            pnlResult.Controls.Add(lstResultRow);
            pnlResult.Controls.Add(txtFilter);
            pnlResult.Controls.Add(lblFilsContent);
            pnlResult.Location = new Point(37, 36);
            pnlResult.Name = "pnlResult";
            pnlResult.Size = new Size(748, 469);
            pnlResult.TabIndex = 1;
            // 
            // lblResult
            // 
            lblResult.Dock = DockStyle.Fill;
            lblResult.Location = new Point(90, 23);
            lblResult.Multiline = true;
            lblResult.Name = "lblResult";
            lblResult.ReadOnly = true;
            lblResult.ScrollBars = ScrollBars.Both;
            lblResult.Size = new Size(658, 446);
            lblResult.TabIndex = 4;
            lblResult.WordWrap = false;
            // 
            // rtxResult
            // 
            rtxResult.Dock = DockStyle.Fill;
            rtxResult.Location = new Point(90, 23);
            rtxResult.Name = "rtxResult";
            rtxResult.Size = new Size(658, 446);
            rtxResult.TabIndex = 4;
            rtxResult.Text = "";
            // 
            // lstResultRow
            // 
            lstResultRow.Alignment = ListViewAlignment.Default;
            lstResultRow.Columns.AddRange(new ColumnHeader[] { columnRow });
            lstResultRow.Dock = DockStyle.Left;
            lstResultRow.Location = new Point(0, 23);
            lstResultRow.Name = "lstResultRow";
            lstResultRow.Size = new Size(90, 446);
            lstResultRow.TabIndex = 5;
            lstResultRow.UseCompatibleStateImageBehavior = false;
            lstResultRow.View = View.Details;
            lstResultRow.SelectedIndexChanged += lstResultRow_SelectedIndexChanged;
            // 
            // columnRow
            // 
            columnRow.Text = "Row Index";
            columnRow.Width = 90;
            // 
            // txtFilter
            // 
            txtFilter.Dock = DockStyle.Top;
            txtFilter.Location = new Point(0, 0);
            txtFilter.Name = "txtFilter";
            txtFilter.Size = new Size(748, 23);
            txtFilter.TabIndex = 3;
            txtFilter.TextChanged += txtFilter_TextChanged;
            // 
            // lblFilsContent
            // 
            lblFilsContent.Location = new Point(375, 164);
            lblFilsContent.Name = "lblFilsContent";
            lblFilsContent.Size = new Size(100, 23);
            lblFilsContent.TabIndex = 2;
            lblFilsContent.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // spResult
            // 
            spResult.Dock = DockStyle.Fill;
            spResult.Location = new Point(0, 60);
            spResult.Name = "spResult";
            // 
            // spResult.Panel1
            // 
            spResult.Panel1.Controls.Add(lstFiles);
            // 
            // spResult.Panel2
            // 
            spResult.Panel2.Controls.Add(pnlResult);
            spResult.Size = new Size(1382, 615);
            spResult.SplitterDistance = 397;
            spResult.TabIndex = 6;
            // 
            // lstFiles
            // 
            lstFiles.Alignment = ListViewAlignment.Default;
            lstFiles.Columns.AddRange(new ColumnHeader[] { columnResult });
            lstFiles.Dock = DockStyle.Fill;
            lstFiles.GridLines = true;
            lstFiles.Location = new Point(0, 0);
            lstFiles.MultiSelect = false;
            lstFiles.Name = "lstFiles";
            lstFiles.Size = new Size(397, 615);
            lstFiles.TabIndex = 1;
            lstFiles.UseCompatibleStateImageBehavior = false;
            lstFiles.View = View.Details;
            lstFiles.SelectedIndexChanged += lstFiles_SelectedIndexChanged;
            // 
            // columnResult
            // 
            columnResult.Text = "Result Path";
            columnResult.Width = 300;
            // 
            // pnlInfo
            // 
            pnlInfo.Controls.Add(lblInfos);
            pnlInfo.Dock = DockStyle.Bottom;
            pnlInfo.Location = new Point(0, 675);
            pnlInfo.Name = "pnlInfo";
            pnlInfo.Size = new Size(1382, 25);
            pnlInfo.TabIndex = 4;
            // 
            // lblInfos
            // 
            lblInfos.BorderStyle = BorderStyle.FixedSingle;
            lblInfos.Dock = DockStyle.Fill;
            lblInfos.Location = new Point(0, 0);
            lblInfos.Name = "lblInfos";
            lblInfos.Size = new Size(1382, 25);
            lblInfos.TabIndex = 0;
            lblInfos.TextAlign = ContentAlignment.MiddleLeft;
            lblInfos.Click += lblInfos_Click;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1382, 700);
            Controls.Add(spResult);
            Controls.Add(pnlInfo);
            Controls.Add(pnlSearch);
            Name = "FormMain";
            Text = "MultiSeek";
            pnlSearch.ResumeLayout(false);
            pnlSearch.PerformLayout();
            pnlResult.ResumeLayout(false);
            pnlResult.PerformLayout();
            spResult.Panel1.ResumeLayout(false);
            spResult.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)spResult).EndInit();
            spResult.ResumeLayout(false);
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
        private TextBox txtPath;
        private Label lblKeyword;
        private Label lblPath;
        private SplitContainer spResult;
        private CheckBox chkIgnoreCase;
        private ColumnHeader columnResult;
        private ExtRichTextBox rtxResult;
        private TextBox txtFilter;
        private ListView lstResultRow;
        public ColumnHeader columnRow;
    }
}