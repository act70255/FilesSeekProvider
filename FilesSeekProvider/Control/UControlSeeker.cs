using Microsoft.Diagnostics.Runtime.Utilities;
using Microsoft.Win32;
using Service;
using Service.Interface;
using Service.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utility.Extension;

namespace FilesSeeker
{
    public partial class UControlSeeker : UserControl
    {
        IFileSeekService _fileSeekService;
        public FormAdditionalSourceDialog KeyWordsDialog = new FormAdditionalSourceDialog();
        string[] FileExtentions = new string[] { ".txt", ".log", ".cs" };
        EventHandler<List<SeekResultModel>> seekHandler;

        public bool IsRegex => chkRegex.Checked;
        public bool IgnoreCase => chkIgnoreCase.Checked;
        public string KeyWord => txtKeyword.Text;
        public string SeekPath => txtPath.Text;
        public string PathKeyword => txtPathKeyword.Text;
        public bool FilterAdditionalKeywords => chkAdditionalFilter.Checked;
        public string[] FilterTexts
        {
            get => txtFilter.Text.Split(';');
            set => txtFilter.Text = string.Join(';', value);
        }
        public List<string> AdditionalKeyWord
        {
            get { return KeyWordsDialog.DataSource; }
        }
        SeekResultModel CurrentData
        {
            get => _bindingSource.Current as SeekResultModel;
        }
        private BindingSource _bindingSource { get; set; } = new BindingSource();
        private int _viewPosition = -1;
        public UControlSeeker(IFileSeekService fileSeekService)
        {
            InitializeComponent();
            _fileSeekService = fileSeekService;
            gvResult.DataSource = _bindingSource;
            #region Event
            Load += (s, e) =>
            {
                txtKeyword.Focus();
                //BtnAdditionalKeyWord_Click(this, new EventArgs());
            };
            btnAdditionalKeyWord.Click += BtnAdditionalKeyWord_Click;
            btnSearch.Click += BtnSearch_Click;
            btnExtract.Click += BtnExtract_Click;
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
        #region Events

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
            switch (keyData)
            {
                case Keys.F1:
                    TxtPath_Click(txtPath, new EventArgs());
                    break;
                case Keys.F2:
                    txtKeyword.Clear();
                    txtKeyword.Focus();
                    break;
                case Keys.F3:
                    chkIgnoreCase.Checked = !IgnoreCase;
                    break;
                case Keys.F4:
                    chkRegex.Checked = !IsRegex;
                    break;
                case Keys.F5:
                    BtnSearch_Click(btnSearch, new EventArgs());
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void TxtPath_Click(object? sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    txtPath.Text = fbd.SelectedPath;
                    if (!string.IsNullOrEmpty(txtKeyword.Text))
                        btnSearch.Focus();
                }
            }
        }

        private void BtnSearch_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SeekPath))
                return;
            if (string.IsNullOrEmpty(KeyWord))
                return;

            _bindingSource.Clear();
            txtFilter.Text = KeyWord;

            btnSearch.Enabled = false;
            var currentFile = string.Empty;

            var result = _fileSeekService.ProcessSeekTask(SeekPath, txtPathKeyword.Text, txtKeyword.Text, "*.*", IgnoreCase, IsRegex);

            lblResultStatus.Text = $"{result.Count(f => f.Line > 0)}";
            _bindingSource.DataSource = result.Where(f => f.Line > 0);
            gvResult.AutoResizeColumns();
            //Task.Run(() =>
            //{
            //    var totalCount = result.Count(c => c.Line > 0);
            //    foreach (var each in result)
            //    {
            //        this.Invoke(new Action(() =>
            //        {
            //            if (each.Line < 0 || string.IsNullOrEmpty(each.Content))
            //            {
            //                currentFile = each.Path;
            //            }
            //            else
            //            {
            //                _bindingSource.Add(each);
            //                if (_bindingSource.Count % 10 == 1)
            //                    gvResult.AutoResizeColumns();
            //            }
            //            lblResultStatus.Text = $"[{totalCount} / {_bindingSource.Count}] {currentFile}";
            //        }));
            //    }
            //}).GetAwaiter();
            btnSearch.Enabled = true;
        }

        private void GvResult_SelectionChanged(object? sender, EventArgs e)
        {
            if (sender is DataGridView view && view.SelectedCells.Count > 0 && view.Rows[view.SelectedCells[0].RowIndex]?.DataBoundItem is SeekResultModel model)
            {
                //btnFilterNext.Tag = 0;
                _viewPosition = -1;
                var nextPosition = model.SeekNextPosition(KeyWord, _viewPosition, IgnoreCase);
                if (nextPosition >= 0)
                {
                    SetupDisplayResult(model.Content, KeyWord, nextPosition);
                    _viewPosition = nextPosition;
                }
                else
                    MessageBox.Show($"Filter text not found");
            }
        }

        private void TxtKeyword_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (string.IsNullOrEmpty(SeekPath))
                    TxtPath_Click(txtPath, new EventArgs());
            }
        }

        private void GvResult_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            else if (sender is DataGridView view && view.Rows[e.RowIndex]?.DataBoundItem is SeekResultModel model)
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

        //private void BtnFilterNext_Click(object? sender, EventArgs e)
        //{
        //    if (CurrentData != null && sender is Button view && view.Tag is int index)
        //    {
        //        var nextPosition = CurrentData.Data.SeekNextPosition(FilterTexts, index, IgnoreCase);
        //        if (nextPosition != null)
        //        {
        //            if (nextPosition.Index == index)
        //            {
        //                btnFilterNext.Tag = 0;
        //                MessageBox.Show($"Seek End!");
        //            }
        //            else if (nextPosition.Index >= 0)
        //            {
        //                btnFilterNext.Tag = Math.Max(0, nextPosition.Index);
        //                SetupDisplayResult(CurrentResultData.Data.Content, nextPosition.Content, nextPosition.Index);
        //            }
        //            else
        //                MessageBox.Show($"Filter text not found");
        //        }
        //    }
        //}

        private void BtnAdditionalKeyWord_Click(object? sender, EventArgs e)
        {
            if (KeyWordsDialog.Visible)
                KeyWordsDialog.Focus();
            else
                KeyWordsDialog.Show();
        }

        private void BtnExtract_Click(object? sender, EventArgs e)
        {
            var keywords = AdditionalKeyWord.ToArray();
            //add string to keyowrds
            if (!string.IsNullOrEmpty(KeyWord))
                keywords = keywords.Append(KeyWord).ToArray();

            var result = _fileSeekService.ExtractByKeyword(SeekPath, keywords, PathKeyword);
        }
        #endregion
        #region DisplayData
        void SetupDisplayResult(string content, string filter, int posIndex = 0)
        {
            ShowDisplayRsult(content, filter, posIndex);
        }

        void ShowDisplayRsult(string Content, string filter, int filterStartIndex)
        {
            try
            {
                int displayStartIndex = 0;
                int displayEndIndex = Content.Length;
                string prefix = "";
                string endfix = "";
                if (filterStartIndex > 500)
                {
                    displayStartIndex = filterStartIndex - 500;
                    prefix = $"....more({displayStartIndex}){Environment.NewLine}";
                }
                if (Content.Length > filterStartIndex + filter.Length + 700)
                {
                    displayEndIndex = filterStartIndex + filter.Length + 700;
                    endfix = $"{Environment.NewLine}more....({Content.Length - displayEndIndex - filter.Length})";
                }

                var displayString = $"{prefix}{Content.Substring(displayStartIndex, displayEndIndex - displayStartIndex)}{endfix}";
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
        #endregion
    }
}
