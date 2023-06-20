using Microsoft.VisualBasic.Devices;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FilesSeekProvider
{
    public partial class FormMain : Form
    {
        List<string> Extentions = new List<string>();
        public string Path
        {
            get { return txtPath.Text; }
            set { txtPath.Text = value; }
        }

        public List<MatchDataObject> MatchResultList
        {
            get { return _matchResultList; }
            set
            {
                _matchResultList = value;
                lstFiles.Items.Clear();
                foreach (var result in _matchResultList.Select(s => s.Path).ToArray())
                {
                    lstFiles.Items.Add(result);
                }

                this.Text = $"MultiSeek - [{_matchResultList.Count}] {value}";
            }
        }
        List<MatchDataObject> _matchResultList = new List<MatchDataObject>();

        public FormMain()
        {
            InitializeComponent();
            Extentions = new List<string> { ".txt", ".log", ".cs" };
            this.Load += (s, e) =>
            {
                ActiveControl = txtKeyword;
                LstFiles_SizeChanged(lstFiles, null);
                LstFiles_SizeChanged(lstResultRow, null);
                pnlResult.Dock = DockStyle.Fill;
            };
            lstFiles.SizeChanged += LstFiles_SizeChanged;
            lstResultRow.SizeChanged += LstFiles_SizeChanged;
        }

        void SeekInFolder(string path, string keyword, bool isRegex = false, bool ignoreCase = false)
        {
            #region Input validation
            if (string.IsNullOrEmpty(path))
            {
                MessageBox.Show("Must be pick a folder");
                return;
            }
            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Must be setup keyword");
                return;
            }
            #endregion

            List<MatchDataObject> results = new List<MatchDataObject>();
            string[] files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

            foreach (string file in files.Where(f => Extentions.Contains(System.IO.Path.GetExtension(f))))
            {
                Dictionary<int, string> matchs = new Dictionary<int, string>();
                var fileContent = File.ReadAllLines(file);
                if (isRegex)
                {
                    matchs = fileContent.Select((s, i) => new { Text = s, rowIndex = i + 1 })
                        .Where(f => Regex.IsMatch(f.Text, keyword))
                        .ToDictionary(d => d.rowIndex, d => d.Text);
                }
                else
                {
                    matchs = fileContent
                        .Select((s, i) => new { Text = s, rowIndex = i + 1 })
                        .Where(f => f.Text.Contains(keyword, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.CurrentCulture))
                        .ToDictionary(d => d.rowIndex, d => d.Text);
                }
                if (matchs.Any())
                {
                    results.Add(new MatchDataObject(file, matchs));
                }
            }

            if (results.Any())
            {
                MatchResultList = results.ToList();
            }
            else
            {
                MessageBox.Show("Match not found");
            }
            if (!chkRegex.Checked)
                txtFilter.Text = txtKeyword.Text;
        }

        void PickFolder()
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    Path = fbd.SelectedPath;
                    if (!string.IsNullOrEmpty(txtKeyword.Text))
                        btnSearch_Click(null, null);
                }
            }
        }

        void HighLightFilter(RichTextBox rtx, string filterKey)
        {
            ClearHighLight(rtxResult);
            int startindex = 0;
            while (startindex < rtx.TextLength)
            {
                int wordstartIndex = rtx.Find(filterKey, startindex, RichTextBoxFinds.None);
                if (wordstartIndex != -1)
                {
                    rtx.SelectionStart = wordstartIndex;
                    rtx.SelectionLength = filterKey.Length;
                    rtx.SelectionBackColor = Color.Lime;
                }
                else
                    break;
                startindex += wordstartIndex + filterKey.Length;
            }
        }
        void ClearHighLight(RichTextBox rtx)
        {
            rtx.SelectionStart = 0;
            rtx.SelectAll();
            rtx.SelectionBackColor = Color.White;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F1)
            {
                chkIgnoreCase.Checked = !chkRegex.Checked;
            }
            else if (keyData == Keys.F2)
            {
                chkRegex.Checked = !chkRegex.Checked;
            }
            else if (keyData == Keys.F3)
            {
                txtKeyword.Clear();
                txtKeyword.Focus();
            }
            else if (keyData == Keys.F4)
            {
                txtPath_Click(null, null);
            }
            else if (keyData == Keys.F5)
            {
                btnSearch_Click(null, null);
                lstFiles.Focus();
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void LstFiles_SizeChanged(object? sender, EventArgs e)
        {
            if (sender is ListView view)
                view.Columns[0].Width = view.Width;
        }

        private void txtPath_Click(object sender, EventArgs e)
        {
            PickFolder();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            SeekInFolder(Path, txtKeyword.Text, chkRegex.Checked, chkIgnoreCase.Checked);
        }

        private void txtKeyword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (string.IsNullOrEmpty(Path))
                    txtPath_Click(null, null);
                else
                    btnSearch_Click(null, null);
            }
        }

        private void lstFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is ListView view && view.SelectedItems.Count > 0)
            {
                var pickFile = view.SelectedItems[0].Text;

                lblInfos.Text = pickFile;
                if (MatchResultList.FirstOrDefault(f => f.Path == pickFile) is MatchDataObject obj)
                {
                    lstResultRow.Items.Clear();
                    foreach (var result in obj.MatchValuePairs.Select(s => s.Key).ToArray())
                    {
                        lstResultRow.Items.Add(result.ToString());
                    }
                }
            }
        }

        private void lstResultRow_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is ListView rowView && rowView.SelectedItems.Count > 0 && lstFiles.SelectedItems.Count > 0)
            {
                if (MatchResultList.FirstOrDefault(f => f.Path == lstFiles.SelectedItems[0].Text) is MatchDataObject obj
                    && obj.MatchValuePairs.FirstOrDefault(f => f.Key.ToString() == rowView.SelectedItems[0].Text) is KeyValuePair<int, string> value)
                {
                    if (value.Value.Length > 1000)
                    {
                        lblResult.Text = value.Value;
                        lblResult.Visible = true;
                    }
                    else
                    {
                        lblResult.Visible = false;
                        rtxResult.Text = value.Value;
                        txtFilter_TextChanged(null, null);
                    }
                }
            }
        }

        private void lblInfos_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lblInfos.Text))
                return;

            System.Windows.Forms.Clipboard.SetText(lblInfos.Text);
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilter.Text))
                ClearHighLight(rtxResult);
            else
                HighLightFilter(rtxResult, txtFilter.Text);
        }
    }

    public class MatchDataObject
    {
        public MatchDataObject(string path, Dictionary<int, string> matchs)
        {
            Path = path;
            MatchValuePairs = matchs;
        }
        public string Path { get; set; }
        public Dictionary<int, string> MatchValuePairs { get; set; } = new Dictionary<int, string>();
    }
}