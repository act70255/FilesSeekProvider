namespace FilesSeeker
{
    partial class FormFileSeeker
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
            components = new System.ComponentModel.Container();
            pnlSearch = new Panel();
            chkAdditionalFilter = new CheckBox();
            btnAdditionalKeyWord = new Button();
            chkIgnoreCase = new CheckBox();
            lblKeyword = new Label();
            lblPath = new Label();
            txtPath = new TextBox();
            txtKeyword = new TextBox();
            chkRegex = new CheckBox();
            btnSearch = new Button();
            spcResult = new SplitContainer();
            gvResult = new DataGridView();
            ColumnFileName = new DataGridViewTextBoxColumn();
            ColumnLine = new DataGridViewTextBoxColumn();
            ColumnPath = new DataGridViewTextBoxColumn();
            bsResult = new BindingSource(components);
            rtxDetail = new RichTextBox();
            pnlFilter = new Panel();
            txtFilter = new TextBox();
            chkHighLightMultiKey = new CheckBox();
            btnFilterPrev = new Button();
            btnFilterNext = new Button();
            lblResultStatus = new Label();
            pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)spcResult).BeginInit();
            spcResult.Panel1.SuspendLayout();
            spcResult.Panel2.SuspendLayout();
            spcResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gvResult).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsResult).BeginInit();
            pnlFilter.SuspendLayout();
            SuspendLayout();
            // 
            // pnlSearch
            // 
            pnlSearch.Controls.Add(chkAdditionalFilter);
            pnlSearch.Controls.Add(btnAdditionalKeyWord);
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
            pnlSearch.Size = new Size(1431, 60);
            pnlSearch.TabIndex = 1;
            // 
            // chkAdditionalFilter
            // 
            chkAdditionalFilter.AutoSize = true;
            chkAdditionalFilter.Location = new Point(1293, 21);
            chkAdditionalFilter.Name = "chkAdditionalFilter";
            chkAdditionalFilter.Size = new Size(15, 14);
            chkAdditionalFilter.TabIndex = 11;
            chkAdditionalFilter.UseVisualStyleBackColor = true;
            // 
            // btnAdditionalKeyWord
            // 
            btnAdditionalKeyWord.Location = new Point(1215, 15);
            btnAdditionalKeyWord.Name = "btnAdditionalKeyWord";
            btnAdditionalKeyWord.Size = new Size(72, 25);
            btnAdditionalKeyWord.TabIndex = 10;
            btnAdditionalKeyWord.Text = "[F6] +";
            btnAdditionalKeyWord.UseVisualStyleBackColor = true;
            // 
            // chkIgnoreCase
            // 
            chkIgnoreCase.AutoSize = true;
            chkIgnoreCase.Checked = true;
            chkIgnoreCase.CheckState = CheckState.Checked;
            chkIgnoreCase.Location = new Point(906, 7);
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
            // 
            // txtKeyword
            // 
            txtKeyword.Location = new Point(124, 5);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.PlaceholderText = "[F3]Keyword";
            txtKeyword.Size = new Size(777, 23);
            txtKeyword.TabIndex = 4;
            // 
            // chkRegex
            // 
            chkRegex.AutoSize = true;
            chkRegex.Location = new Point(906, 35);
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
            // 
            // spcResult
            // 
            spcResult.Dock = DockStyle.Fill;
            spcResult.Location = new Point(0, 60);
            spcResult.Name = "spcResult";
            spcResult.Orientation = Orientation.Horizontal;
            // 
            // spcResult.Panel1
            // 
            spcResult.Panel1.Controls.Add(gvResult);
            spcResult.Panel1.Controls.Add(lblResultStatus);
            // 
            // spcResult.Panel2
            // 
            spcResult.Panel2.Controls.Add(rtxDetail);
            spcResult.Panel2.Controls.Add(pnlFilter);
            spcResult.Size = new Size(1431, 549);
            spcResult.SplitterDistance = 295;
            spcResult.TabIndex = 2;
            // 
            // gvResult
            // 
            gvResult.AllowUserToAddRows = false;
            gvResult.AllowUserToDeleteRows = false;
            gvResult.AllowUserToResizeRows = false;
            gvResult.AutoGenerateColumns = false;
            gvResult.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gvResult.Columns.AddRange(new DataGridViewColumn[] { ColumnFileName, ColumnLine, ColumnPath });
            gvResult.DataSource = bsResult;
            gvResult.Dock = DockStyle.Fill;
            gvResult.Location = new Point(0, 0);
            gvResult.MultiSelect = false;
            gvResult.Name = "gvResult";
            gvResult.ReadOnly = true;
            gvResult.RowHeadersVisible = false;
            gvResult.RowTemplate.Height = 25;
            gvResult.Size = new Size(1431, 295);
            gvResult.TabIndex = 0;
            // 
            // ColumnFileName
            // 
            ColumnFileName.DataPropertyName = "FileName";
            ColumnFileName.HeaderText = "FileName";
            ColumnFileName.Name = "ColumnFileName";
            ColumnFileName.ReadOnly = true;
            // 
            // ColumnLine
            // 
            ColumnLine.DataPropertyName = "Line";
            ColumnLine.HeaderText = "Line";
            ColumnLine.Name = "ColumnLine";
            ColumnLine.ReadOnly = true;
            // 
            // ColumnPath
            // 
            ColumnPath.DataPropertyName = "Path";
            ColumnPath.HeaderText = "Path";
            ColumnPath.Name = "ColumnPath";
            ColumnPath.ReadOnly = true;
            // 
            // rtxDetail
            // 
            rtxDetail.Dock = DockStyle.Fill;
            rtxDetail.Location = new Point(0, 23);
            rtxDetail.Name = "rtxDetail";
            rtxDetail.Size = new Size(1431, 227);
            rtxDetail.TabIndex = 8;
            rtxDetail.Text = "";
            // 
            // pnlFilter
            // 
            pnlFilter.Controls.Add(txtFilter);
            pnlFilter.Controls.Add(chkHighLightMultiKey);
            pnlFilter.Controls.Add(btnFilterPrev);
            pnlFilter.Controls.Add(btnFilterNext);
            pnlFilter.Dock = DockStyle.Top;
            pnlFilter.Location = new Point(0, 0);
            pnlFilter.Name = "pnlFilter";
            pnlFilter.Size = new Size(1431, 23);
            pnlFilter.TabIndex = 7;
            // 
            // txtFilter
            // 
            txtFilter.Dock = DockStyle.Fill;
            txtFilter.Location = new Point(0, 0);
            txtFilter.Name = "txtFilter";
            txtFilter.Size = new Size(1366, 23);
            txtFilter.TabIndex = 3;
            // 
            // chkHighLightMultiKey
            // 
            chkHighLightMultiKey.AutoSize = true;
            chkHighLightMultiKey.Checked = true;
            chkHighLightMultiKey.CheckState = CheckState.Checked;
            chkHighLightMultiKey.Dock = DockStyle.Right;
            chkHighLightMultiKey.Location = new Point(1366, 0);
            chkHighLightMultiKey.Name = "chkHighLightMultiKey";
            chkHighLightMultiKey.Size = new Size(15, 23);
            chkHighLightMultiKey.TabIndex = 4;
            chkHighLightMultiKey.UseVisualStyleBackColor = true;
            // 
            // btnFilterPrev
            // 
            btnFilterPrev.Dock = DockStyle.Right;
            btnFilterPrev.Location = new Point(1381, 0);
            btnFilterPrev.Name = "btnFilterPrev";
            btnFilterPrev.Size = new Size(25, 23);
            btnFilterPrev.TabIndex = 1;
            btnFilterPrev.Text = "<";
            btnFilterPrev.UseVisualStyleBackColor = true;
            // 
            // btnFilterNext
            // 
            btnFilterNext.Dock = DockStyle.Right;
            btnFilterNext.Location = new Point(1406, 0);
            btnFilterNext.Name = "btnFilterNext";
            btnFilterNext.Size = new Size(25, 23);
            btnFilterNext.TabIndex = 0;
            btnFilterNext.Text = ">";
            btnFilterNext.UseVisualStyleBackColor = true;
            // 
            // lblResultStatus
            // 
            lblResultStatus.Dock = DockStyle.Bottom;
            lblResultStatus.Location = new Point(0, 280);
            lblResultStatus.Name = "lblResultStatus";
            lblResultStatus.Size = new Size(1431, 15);
            lblResultStatus.TabIndex = 1;
            // 
            // FormFileSeeker
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1431, 609);
            Controls.Add(spcResult);
            Controls.Add(pnlSearch);
            Name = "FormFileSeeker";
            Text = "FormFileSeeker";
            pnlSearch.ResumeLayout(false);
            pnlSearch.PerformLayout();
            spcResult.Panel1.ResumeLayout(false);
            spcResult.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)spcResult).EndInit();
            spcResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gvResult).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsResult).EndInit();
            pnlFilter.ResumeLayout(false);
            pnlFilter.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlSearch;
        private Button btnAdditionalKeyWord;
        private CheckBox chkIgnoreCase;
        private Label lblKeyword;
        private Label lblPath;
        private TextBox txtPath;
        private TextBox txtKeyword;
        private CheckBox chkRegex;
        private Button btnSearch;
        private SplitContainer spcResult;
        private DataGridView gvResult;
        private RichTextBox rtxDetail;
        private Panel pnlFilter;
        private TextBox txtFilter;
        private Button btnFilterPrev;
        private Button btnFilterNext;
        private BindingSource bsResult;
        private DataGridViewTextBoxColumn ColumnFileName;
        private DataGridViewTextBoxColumn ColumnLine;
        private DataGridViewTextBoxColumn ColumnPath;
        private CheckBox chkHighLightMultiKey;
        private CheckBox chkAdditionalFilter;
        private Label lblResultStatus;
    }
}