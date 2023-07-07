using Microsoft.Win32;
using Service.Interface;
using Service.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FilesSeeker
{
    public partial class FormFileSeeker : Form
    {
        IFileSeekService _fileSeekService;

        string[] FileExtentions;
        FormAdditionalSourceDialog KeyWordsDialog = new FormAdditionalSourceDialog();

        public bool IgnoreCase
        {
            get => chkIgnoreCase.Checked;
            set => chkIgnoreCase.Checked = value;
        }

        public bool IsRegex
        {
            get => chkRegex.Checked;
            set => chkRegex.Checked = value;
        }

        public string KeyWord
        {
            get => txtKeyword.Text;
            set => txtKeyword.Text = value;
        }

        public string FilterText
        {
            get => txtFilter.Text;
            set => txtFilter.Text = value;
        }

        public string FolderPath
        {
            get => txtPath.Text;
            set => txtPath.Text = value;
        }

        public List<SeekFileResultModel> SeekResults
        {
            get => _seekResults;
            set
            {
                _seekResults = value;
                bsResult.DataSource = value.OrderBy(o => o.FileName);
                gvResult.AutoResizeColumns();
                gvResult.AutoResizeRows();
            }
        }
        List<SeekFileResultModel> _seekResults = new List<SeekFileResultModel>();

        SeekFileResultModel CurrentDataModel
        {
            get
            {
                if (gvResult.SelectedCells.Count >= 1
                    && gvResult.Rows[gvResult.SelectedCells[0].RowIndex]?.DataBoundItem is SeekFileResultModel model)
                    return model;
                else
                    return null;
            }
        }

        public FormFileSeeker(IFileSeekService fileSeekService)
        {
            InitializeComponent();
            _fileSeekService = fileSeekService;

            FileExtentions = new string[] { ".txt", ".log", ".cs" };

            #region Event
            Shown += (s, e) =>
            {
                txtKeyword.Focus();
                BtnAdditionalKeyWord_Click(this, new EventArgs());
            };
            btnAdditionalKeyWord.Click += BtnAdditionalKeyWord_Click;
            btnFilterNext.Click += BtnFilterNext_Click;
            btnFilterPrev.Click += BtnFilterPrev_Click;
            btnSearch.Click += BtnSearch_Click;
            txtPath.Click += TxtPath_Click;
            txtFilter.TextChanged += TxtFilter_TextChanged;
            txtKeyword.KeyPress += TxtKeyword_KeyPress;
            gvResult.CellDoubleClick += GvResult_CellDoubleClick;
            gvResult.SelectionChanged += GvResult_SelectionChanged;
            chkHighLightMultiKey.CheckedChanged += ChkHighLightMultiKey_CheckedChanged;
            KeyWordsDialog.FormClosing += KeyWordsDialog_FormClosing;
            KeyWordsDialog.SourceChanged += KeyWordsDialog_SourceChanged;
            #endregion
        }

        private void KeyWordsDialog_SourceChanged(object? sender, EventArgs e)
        {
            btnAdditionalKeyWord.Text = "[F6] +" + (KeyWordsDialog.DataSource.Count > 0 ? $" - {KeyWordsDialog.DataSource.Count}" : "");
        }

        private void KeyWordsDialog_FormClosing(object? sender, FormClosingEventArgs e)
        {
            KeyWordsDialog.Hide();
            e.Cancel = true;
        }
        
        private void ChkHighLightMultiKey_CheckedChanged(object? sender, EventArgs e)
        {
            if (sender is CheckBox view)
            {
                if (view.Checked)
                {
                    foreach (var each in KeyWordsDialog.DataSource)
                    {
                        HighLightFilter(rtxDetail, each, Color.Orange);
                    }
                }
                else
                {
                    ClearHighLight(rtxDetail);
                    HighLightFilter(rtxDetail, txtFilter.Text);
                }
            }
        }
        
        private void TxtFilter_TextChanged(object? sender, EventArgs e)
        {
            ClearHighLight(rtxDetail);
            HighLightFilter(rtxDetail, txtFilter.Text);
        }
        
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F1)
                IgnoreCase = !IgnoreCase;
            else if (keyData == Keys.F2)
                chkRegex.Checked = !chkRegex.Checked;
            else if (keyData == Keys.F3)
            {
                txtKeyword.Clear();
                txtKeyword.Focus();
            }
            else if (keyData == Keys.F4)
                TxtPath_Click(txtPath, new EventArgs());
            else if (keyData == Keys.F5)
                BtnSearch_Click(btnSearch, new EventArgs());
            else if (keyData == Keys.F6)
                BtnAdditionalKeyWord_Click(btnSearch, new EventArgs());
            else if (keyData == Keys.F9)
                BtnFilterPrev_Click(btnFilterPrev, new EventArgs());
            else if (keyData == Keys.F10)
                BtnFilterNext_Click(btnFilterNext, new EventArgs());
            
            return base.ProcessCmdKey(ref msg, keyData);
        }
        
        private void TxtPath_Click(object? sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    FolderPath = fbd.SelectedPath;
                    if (!string.IsNullOrEmpty(txtKeyword.Text))
                        btnSearch.Focus();
                }
            }
        }

        private void BtnSearch_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(FolderPath))
                return;
            if (string.IsNullOrEmpty(KeyWord))
                return;

            var result = _fileSeekService.SeekInFolder(FolderPath, FileExtentions, KeyWord, KeyWordsDialog.DataSource.ToArray(), IgnoreCase, IsRegex);
            if (result != null && result.Any())
            {
                FilterText = KeyWord;
                SeekResults = result;
            }
            else
                MessageBox.Show($"KeyWord:{KeyWord} not found");
        }

        private void GvResult_SelectionChanged(object? sender, EventArgs e)
        {
            if (sender is DataGridView view && view.SelectedCells.Count > 0 && view.Rows[view.SelectedCells[0].RowIndex]?.DataBoundItem is SeekFileResultModel model)
            {
                var matchPos = model.Data.SetFilter(FilterText, IgnoreCase);
                SetupDisplayResult(model);
            }
        }

        private void TxtKeyword_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (string.IsNullOrEmpty(FolderPath))
                    TxtPath_Click(txtPath, new EventArgs());
            }
        }

        private void GvResult_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            else if (sender is DataGridView view && view.Rows[e.RowIndex]?.DataBoundItem is SeekFileResultModel model)
            {
                var nppDir = (string)Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Notepad++", null, null);
                var nppExePath = Path.Combine(nppDir, "Notepad++.exe");

                Process p = new Process();
                ProcessStartInfo pi = new ProcessStartInfo();
                pi.UseShellExecute = true;
                pi.FileName = nppExePath;
                pi.Arguments = model.Path;
                p.StartInfo = pi;
                p.Start();
            }
        }

        private void BtnFilterPrev_Click(object? sender, EventArgs e)
        {
            if (sender is Button view && view.Tag is int index)
            {
                chkHighLightMultiKey.CheckedChanged -= ChkHighLightMultiKey_CheckedChanged;
                chkHighLightMultiKey.Checked = false;
                chkHighLightMultiKey.CheckedChanged += ChkHighLightMultiKey_CheckedChanged;
                SetupDisplayResult(CurrentDataModel, index);
            }
        }

        private void BtnFilterNext_Click(object? sender, EventArgs e)
        {
            if (sender is Button view && view.Tag is int index)
            {
                SetupDisplayResult(CurrentDataModel, index);
            }
        }

        private void BtnAdditionalKeyWord_Click(object? sender, EventArgs e)
        {
            if (KeyWordsDialog.Visible)
                KeyWordsDialog.Focus();
            else
                KeyWordsDialog.Show();
        }

        void SetupDisplayResult(SeekFileResultModel model, int posIndex = 0)
        {
            rtxDetail.Tag = posIndex;
            if (model.Data.MatchPositions.Count > posIndex + 1)
                btnFilterNext.Tag = posIndex + 1;
            if (posIndex > 0)
                btnFilterPrev.Tag = posIndex - 1;

            ShowDisplayRsult(model.Data.Content, model.Data.FilterKey, model.Data.MatchPositions[posIndex]);
        }

        void ShowDisplayRsult(string Content, string filter, int filterStartIndex)
        {
            int displayStartIndex = 0;
            int displayEndIndex = Content.Length;
            string prefix = "";
            string nextfix = "";
            if (filterStartIndex > 500)
            {
                displayStartIndex = filterStartIndex - 500;
                prefix = $"....more({displayStartIndex}){Environment.NewLine}";
            }
            if (Content.Length > filterStartIndex + filter.Length + 700)
            {
                displayEndIndex = filterStartIndex + filter.Length + 700;
                nextfix = $"{Environment.NewLine}more....({Content.Length - displayEndIndex - filter.Length})";
            }

            var displayString = $"{prefix}{Content.Substring(displayStartIndex, displayEndIndex - displayStartIndex)}{nextfix}";
            if (rtxDetail.Text != displayString)
                rtxDetail.Text = displayString;

            HighLightFilter(rtxDetail, filter);
        }

        void ClearHighLight(RichTextBox rtx)
        {
            rtx.SelectionStart = 0;
            rtx.SelectAll();
            rtx.SelectionBackColor = Color.White;
        }

        void HighLightFilter(RichTextBox rtx, string filterKey)
        {
            HighLightFilter(rtx, filterKey, Color.Lime);
            if (rtx.Text.StartsWith("....more"))
                HighLightFilter(rtx, "....more", Color.Tomato);
            if (rtx.Text.Contains("....more") && rtx.Text.EndsWith(")"))
                HighLightFilter(rtx, "more....", Color.Tomato);
        }

        void HighLightFilter(RichTextBox rtx, string filterKey, Color color)
        {
            int startindex = 0;
            while (startindex < rtx.TextLength)
            {
                int wordstartIndex = rtx.Find(filterKey, startindex, RichTextBoxFinds.None);
                if (wordstartIndex != -1)
                {
                    rtx.SelectionStart = wordstartIndex;
                    rtx.SelectionLength = filterKey.Length;
                    rtx.SelectionBackColor = color;
                }
                else
                    break;
                startindex += wordstartIndex + filterKey.Length;
            }
        }
    }
}
