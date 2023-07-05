using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesSeekProvider
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

            btnAdd.Click += BtnAdd_Click;
            btnRemove.Click += BtnRemove_Click;
            lvContent.SelectedIndexChanged += LvContent_SelectedIndexChanged;
        }

        void OnSourceChanged()
        {
            SourceChanged?.Invoke(this, EventArgs.Empty);
        }

        private void LvContent_SelectedIndexChanged(object? sender, EventArgs e)
        {
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
    }
}
