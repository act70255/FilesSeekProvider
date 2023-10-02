using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesSeeker
{
    public class FormAdditionalSourceDialog : Form
    {
        public EventHandler SourceChanged;
        Panel pnlContent = new Panel();
        Panel pnlControl = new Panel();
        ListView lvContent = new ListView();
        Button btnAdd = new Button();
        Button btnRemove = new Button();
        TextBox txtKeyword = new TextBox();
        ColumnHeader columnWord = new ColumnHeader();
        public List<string> DataSource
        {
            get
            {
                return _dataSource;
            }
            set
            {
                _dataSource = value;
                lvContent.Items.Clear();
                foreach (var key in value)
                {
                    lvContent.Items.Add(key);
                }
                OnSourceChanged();
            }
        }
        List<string> _dataSource = new List<string>();

        public FormAdditionalSourceDialog()
        {
            InitializeCompoment();
        }
        public void InitializeCompoment()
        {
            this.SuspendLayout();

            this.Size = new System.Drawing.Size(250, 500);

            columnWord.Width = 250;
            columnWord.Text = "KeyWord";

            txtKeyword.Width = 130;
            txtKeyword.Dock = DockStyle.Fill;

            lvContent.Dock = DockStyle.Fill;
            lvContent.UseCompatibleStateImageBehavior = false;
            lvContent.View = View.Details;
            lvContent.Alignment = ListViewAlignment.Default;
            lvContent.Columns.AddRange(new ColumnHeader[] { columnWord });
            lvContent.GridLines = true;
            lvContent.Location = new Point(0, 0);
            lvContent.MultiSelect = false;

            pnlControl.Size = new Size(250, 25);
            pnlControl.Dock = DockStyle.Bottom;

            pnlContent.Dock = DockStyle.Fill;

            btnAdd.Size = new Size(60, 30);
            btnAdd.Text = "+";
            btnAdd.Dock = DockStyle.Right;
            btnAdd.TextAlign = ContentAlignment.MiddleCenter;
            btnRemove.Size = new Size(60, 30);
            btnRemove.Text = "-";
            btnRemove.Dock = DockStyle.Right;
            btnRemove.TextAlign = ContentAlignment.MiddleCenter;

            pnlControl.Controls.Add(btnAdd);
            pnlControl.Controls.Add(btnRemove);
            pnlControl.Controls.Add(txtKeyword);

            pnlContent.Controls.Add(lvContent);

            Controls.Add(pnlControl);
            Controls.Add(pnlContent);

            this.ResumeLayout(false);

            Shown += FormAdditionalSourceDialog_Shown;
            txtKeyword.KeyPress += TxtKeyword_KeyPress;
            btnAdd.Click += BtnAdd_Click;
            btnRemove.Click += BtnRemove_Click;
            lvContent.KeyDown += LvContent_KeyDown;
            lvContent.SelectedIndexChanged += LvContent_SelectedIndexChanged;
        }

        private void FormAdditionalSourceDialog_Shown(object? sender, EventArgs e)
        {
            txtKeyword.Focus();
        }
        
        private void LvContent_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                BtnRemove_Click(btnRemove, new EventArgs());
            }
        }

        private void TxtKeyword_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                BtnAdd_Click(btnAdd, new EventArgs());
            }
        }

        private void LvContent_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (lvContent.SelectedItems.Count == 0)
                return;
            txtKeyword.Text = lvContent.SelectedItems[0].Text;
        }

        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtKeyword.Text))
                return;
            if (DataSource.Contains(txtKeyword.Text))
                return;

            var keywordlist = new List<string>(DataSource);
            keywordlist.Add(txtKeyword.Text);
            DataSource = keywordlist;
            txtKeyword.Text = string.Empty;
            txtKeyword.Focus();
        }

        private void BtnRemove_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtKeyword.Text))
                return;
            if (!DataSource.Contains(txtKeyword.Text))
                return;

            var keywordlist = new List<string>(DataSource);
            keywordlist.Remove(txtKeyword.Text);
            DataSource = keywordlist;
        }

        void OnSourceChanged()
        {
            SourceChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
