using Microsoft.VisualBasic.Devices;
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

        public string[] KeyWords
        {
            get => txtKeyword.Text.Split(';');
            set => txtKeyword.Text = string.Join(';', value);
        }

        public string[] FilterTexts
        {
            get => txtFilter.Text.Split(';');
            set => txtFilter.Text = string.Join(';', value);
        }

        public string FolderPath
        {
            get => txtPath.Text;
            set => txtPath.Text = value;
        }

        public List<string> AdditionalKeyWord
        {
            get { return KeyWordsDialog.DataSource; }
        }

        public bool FilterAdditionalKeywords
        {
            get { return chkAdditionalFilter.Checked; }
        }

        public List<SeekFileResultModel> SeekResults
        {
            get => _seekResults;
            set
            {
                _seekResults = value;
                var viewSource = value.AsQueryable();
                if (FilterAdditionalKeywords && AdditionalKeyWord.Any())
                    viewSource = viewSource.Where(f => AdditionalKeyWord.Any(a => f.Data.Content.Contains(a)));
                lblResultStatus.Text = $"{viewSource.Count()} match results";
                bsResult.DataSource = viewSource.OrderBy(o => o.FileName).ToList();
                gvResult.AutoResizeColumns();
                gvResult.AutoResizeRows();
            }
        }
        List<SeekFileResultModel> _seekResults = new List<SeekFileResultModel>();

        SeekFileResultModel CurrentResultData
        {
            get => bsResult.Current as SeekFileResultModel;
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
            btnSearch.Click += BtnSearch_Click;
            txtPath.Click += TxtPath_Click;
            txtFilter.TextChanged += TxtFilter_TextChanged;
            txtKeyword.KeyPress += TxtKeyword_KeyPress;
            gvResult.CellDoubleClick += GvResult_CellDoubleClick;
            gvResult.SelectionChanged += GvResult_SelectionChanged;
            chkHighLightMultiKey.CheckedChanged += ChkHighLightMultiKey_CheckedChanged;
            KeyWordsDialog.FormClosing += KeyWordsDialog_FormClosing;
            KeyWordsDialog.SourceChanged += KeyWordsDialog_SourceChanged;
            chkAdditionalFilter.CheckedChanged += ChkAdditionalFilter_CheckedChanged;
            #endregion
        }

        private void ChkAdditionalFilter_CheckedChanged(object? sender, EventArgs e)
        {
            SeekResults = SeekResults;
        }

        private void KeyWordsDialog_SourceChanged(object? sender, EventArgs e)
        {
            btnAdditionalKeyWord.Text = "[F6] +" + (AdditionalKeyWord.Count > 0 ? $" [{AdditionalKeyWord.Count}]" : "");
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
                    foreach (var each in AdditionalKeyWord)
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
                IsRegex = !IsRegex;
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
            if (!KeyWords.Any())
                return;

            var result = _fileSeekService.SeekInFolder(FolderPath, FileExtentions, KeyWords, IgnoreCase, IsRegex);
            if (result != null && result.Any())
            {
                FilterTexts = KeyWords;
                SeekResults = result;
            }
            else
                MessageBox.Show($"KeyWords not found");
        }

        private void GvResult_SelectionChanged(object? sender, EventArgs e)
        {
            if (sender is DataGridView view && view.SelectedCells.Count > 0 && view.Rows[view.SelectedCells[0].RowIndex]?.DataBoundItem is SeekFileResultModel model)
            {
                btnFilterNext.Tag = 0;
                var nextPosition = model.Data.SeekNextPosition(FilterTexts, (int)(btnFilterNext.Tag ?? 0), IgnoreCase);
                if (nextPosition != null)
                {
                    btnFilterNext.Tag = Math.Max(0,nextPosition.Index);
                    if (nextPosition.Index >= 0)
                        SetupDisplayResult(model.Data.Content, nextPosition.Content, nextPosition.Index);
                    else
                        MessageBox.Show($"Filter text not found");
                }
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

        private void BtnFilterNext_Click(object? sender, EventArgs e)
        {
            if (CurrentResultData != null && sender is Button view && view.Tag is int index)
            {
                var nextPosition = CurrentResultData.Data.SeekNextPosition(FilterTexts, index, IgnoreCase);
                if (nextPosition != null)
                {
                    if (nextPosition.Index == index)
                    {
                        btnFilterNext.Tag = 0;
                        MessageBox.Show($"Seek End!");
                    }
                    else if (nextPosition.Index >= 0)
                    {
                        btnFilterNext.Tag = Math.Max(0, nextPosition.Index);
                        SetupDisplayResult(CurrentResultData.Data.Content, nextPosition.Content, nextPosition.Index);
                    }
                    else
                        MessageBox.Show($"Filter text not found");
                }
            }
        }

        private void BtnAdditionalKeyWord_Click(object? sender, EventArgs e)
        {
            if (KeyWordsDialog.Visible)
                KeyWordsDialog.Focus();
            else
                KeyWordsDialog.Show();
        }

        void SetupDisplayResult(string content, string filter, int posIndex = 0)
        {
            rtxDetail.Tag = posIndex;
            ShowDisplayRsult(content, filter, posIndex);
        }

        void ShowDisplayRsult(string Content, string filter, int filterStartIndex)
        {
            try
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
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        void ClearHighLight(RichTextBox rtx)
        {
            rtx.SelectionStart = 0;
            rtx.SelectAll();
            rtx.SelectionBackColor = Color.White;
        }

        void HighLightFilter(RichTextBox rtx, string filterKey)
        {
            if (rtx.Text.StartsWith("....more"))
                HighLightFilter(rtx, "....more", Color.Tomato);
            if (rtx.Text.Contains("more....(") && rtx.Text.EndsWith(")"))
                HighLightFilter(rtx, "more....", Color.Tomato);
            if (chkHighLightMultiKey.Checked && FilterAdditionalKeywords && AdditionalKeyWord.Any())
                foreach (var each in AdditionalKeyWord)
                {
                    HighLightFilter(rtx, each, Color.Orange);
                }

            foreach (var each in FilterTexts)
            {
                HighLightFilter(rtx, each, Color.Lime);
            }
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
