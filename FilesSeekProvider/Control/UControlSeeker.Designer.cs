namespace FilesSeeker
{
    partial class UControlSeeker
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            pnlSearch = new Panel();
            btnExtract = new Button();
            lblPathKeyword = new Label();
            txtPathKeyword = new TextBox();
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
            lblResultStatus = new Label();
            rtxDetail = new RichTextBox();
            pnlFilter = new Panel();
            txtFilter = new TextBox();
            chkHighLightMultiKey = new CheckBox();
            btnFilterNext = new Button();
            pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)spcResult).BeginInit();
            spcResult.Panel1.SuspendLayout();
            spcResult.Panel2.SuspendLayout();
            spcResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gvResult).BeginInit();
            pnlFilter.SuspendLayout();
            SuspendLayout();
            // 
            // pnlSearch
            // 
            pnlSearch.Controls.Add(btnExtract);
            pnlSearch.Controls.Add(lblPathKeyword);
            pnlSearch.Controls.Add(txtPathKeyword);
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
            pnlSearch.Size = new Size(1360, 90);
            pnlSearch.TabIndex = 2;
            // 
            // btnExtract
            // 
            btnExtract.Location = new Point(1234, 55);
            btnExtract.Name = "btnExtract";
            btnExtract.Size = new Size(72, 25);
            btnExtract.TabIndex = 14;
            btnExtract.Text = "Extract";
            btnExtract.UseVisualStyleBackColor = true;
            // 
            // lblPathKeyword
            // 
            lblPathKeyword.AutoSize = true;
            lblPathKeyword.Location = new Point(15, 65);
            lblPathKeyword.Name = "lblPathKeyword";
            lblPathKeyword.Size = new Size(84, 15);
            lblPathKeyword.TabIndex = 13;
            lblPathKeyword.Text = "Path Keyword";
            // 
            // txtPathKeyword
            // 
            txtPathKeyword.Location = new Point(100, 62);
            txtPathKeyword.Name = "txtPathKeyword";
            txtPathKeyword.PlaceholderText = "File path include pattern";
            txtPathKeyword.Size = new Size(780, 23);
            txtPathKeyword.TabIndex = 12;
            // 
            // chkAdditionalFilter
            // 
            chkAdditionalFilter.AutoSize = true;
            chkAdditionalFilter.Location = new Point(1312, 21);
            chkAdditionalFilter.Name = "chkAdditionalFilter";
            chkAdditionalFilter.Size = new Size(15, 14);
            chkAdditionalFilter.TabIndex = 11;
            chkAdditionalFilter.UseVisualStyleBackColor = true;
            // 
            // btnAdditionalKeyWord
            // 
            btnAdditionalKeyWord.Location = new Point(1234, 15);
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
            chkIgnoreCase.Location = new Point(958, 7);
            chkIgnoreCase.Margin = new Padding(0);
            chkIgnoreCase.Name = "chkIgnoreCase";
            chkIgnoreCase.Size = new Size(114, 19);
            chkIgnoreCase.TabIndex = 9;
            chkIgnoreCase.Text = "[F3]Ignore Case";
            chkIgnoreCase.UseVisualStyleBackColor = true;
            // 
            // lblKeyword
            // 
            lblKeyword.AutoSize = true;
            lblKeyword.Location = new Point(15, 36);
            lblKeyword.Name = "lblKeyword";
            lblKeyword.Size = new Size(56, 15);
            lblKeyword.TabIndex = 8;
            lblKeyword.Text = "Keyword";
            // 
            // lblPath
            // 
            lblPath.AutoSize = true;
            lblPath.Location = new Point(15, 9);
            lblPath.Name = "lblPath";
            lblPath.Size = new Size(71, 15);
            lblPath.TabIndex = 7;
            lblPath.Text = "Folder Path";
            // 
            // txtPath
            // 
            txtPath.Location = new Point(100, 5);
            txtPath.Name = "txtPath";
            txtPath.PlaceholderText = "[F1]Folder Path";
            txtPath.Size = new Size(780, 23);
            txtPath.TabIndex = 6;
            // 
            // txtKeyword
            // 
            txtKeyword.Location = new Point(100, 33);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.PlaceholderText = "[F2]Keyword";
            txtKeyword.Size = new Size(780, 23);
            txtKeyword.TabIndex = 4;
            // 
            // chkRegex
            // 
            chkRegex.AutoSize = true;
            chkRegex.Location = new Point(958, 35);
            chkRegex.Margin = new Padding(0);
            chkRegex.Name = "chkRegex";
            chkRegex.Size = new Size(118, 19);
            chkRegex.TabIndex = 5;
            chkRegex.Text = "[F4]RegexMode";
            chkRegex.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(1122, 15);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(72, 25);
            btnSearch.TabIndex = 1;
            btnSearch.Text = "[F5]Seek!";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // spcResult
            // 
            spcResult.Dock = DockStyle.Fill;
            spcResult.Location = new Point(0, 90);
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
            spcResult.Size = new Size(1360, 510);
            spcResult.SplitterDistance = 236;
            spcResult.TabIndex = 3;
            // 
            // gvResult
            // 
            gvResult.AllowUserToAddRows = false;
            gvResult.AllowUserToDeleteRows = false;
            gvResult.AllowUserToResizeRows = false;
            gvResult.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gvResult.Columns.AddRange(new DataGridViewColumn[] { ColumnFileName, ColumnLine, ColumnPath });
            gvResult.Dock = DockStyle.Fill;
            gvResult.Location = new Point(0, 0);
            gvResult.MultiSelect = false;
            gvResult.Name = "gvResult";
            gvResult.ReadOnly = true;
            gvResult.RowHeadersVisible = false;
            gvResult.RowTemplate.Height = 25;
            gvResult.Size = new Size(1360, 221);
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
            // lblResultStatus
            // 
            lblResultStatus.Dock = DockStyle.Bottom;
            lblResultStatus.Location = new Point(0, 221);
            lblResultStatus.Name = "lblResultStatus";
            lblResultStatus.Size = new Size(1360, 15);
            lblResultStatus.TabIndex = 1;
            // 
            // rtxDetail
            // 
            rtxDetail.Dock = DockStyle.Fill;
            rtxDetail.Location = new Point(0, 23);
            rtxDetail.Name = "rtxDetail";
            rtxDetail.Size = new Size(1360, 247);
            rtxDetail.TabIndex = 8;
            rtxDetail.Text = "";
            // 
            // pnlFilter
            // 
            pnlFilter.Controls.Add(txtFilter);
            pnlFilter.Controls.Add(chkHighLightMultiKey);
            pnlFilter.Controls.Add(btnFilterNext);
            pnlFilter.Dock = DockStyle.Top;
            pnlFilter.Location = new Point(0, 0);
            pnlFilter.Name = "pnlFilter";
            pnlFilter.Size = new Size(1360, 23);
            pnlFilter.TabIndex = 7;
            // 
            // txtFilter
            // 
            txtFilter.Dock = DockStyle.Fill;
            txtFilter.Location = new Point(0, 0);
            txtFilter.Name = "txtFilter";
            txtFilter.Size = new Size(1320, 23);
            txtFilter.TabIndex = 3;
            // 
            // chkHighLightMultiKey
            // 
            chkHighLightMultiKey.AutoSize = true;
            chkHighLightMultiKey.Checked = true;
            chkHighLightMultiKey.CheckState = CheckState.Checked;
            chkHighLightMultiKey.Dock = DockStyle.Right;
            chkHighLightMultiKey.Location = new Point(1320, 0);
            chkHighLightMultiKey.Name = "chkHighLightMultiKey";
            chkHighLightMultiKey.Size = new Size(15, 23);
            chkHighLightMultiKey.TabIndex = 4;
            chkHighLightMultiKey.UseVisualStyleBackColor = true;
            // 
            // btnFilterNext
            // 
            btnFilterNext.Dock = DockStyle.Right;
            btnFilterNext.Location = new Point(1335, 0);
            btnFilterNext.Name = "btnFilterNext";
            btnFilterNext.Size = new Size(25, 23);
            btnFilterNext.TabIndex = 0;
            btnFilterNext.Text = ">";
            btnFilterNext.UseVisualStyleBackColor = true;
            // 
            // UControlSeeker
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            Controls.Add(spcResult);
            Controls.Add(pnlSearch);
            Name = "UControlSeeker";
            Size = new Size(1360, 600);
            pnlSearch.ResumeLayout(false);
            pnlSearch.PerformLayout();
            spcResult.Panel1.ResumeLayout(false);
            spcResult.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)spcResult).EndInit();
            spcResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gvResult).EndInit();
            pnlFilter.ResumeLayout(false);
            pnlFilter.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlSearch;
        private Label lblPathKeyword;
        private TextBox txtPathKeyword;
        private CheckBox chkAdditionalFilter;
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
        private DataGridViewTextBoxColumn ColumnFileName;
        private DataGridViewTextBoxColumn ColumnLine;
        private DataGridViewTextBoxColumn ColumnPath;
        private Label lblResultStatus;
        private RichTextBox rtxDetail;
        private Panel pnlFilter;
        private TextBox txtFilter;
        private CheckBox chkHighLightMultiKey;
        private Button btnFilterNext;
        private Button btnExtract;
    }
}
