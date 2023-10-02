using Service.Interface;
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
    public partial class FormSeeker : Form
    {
        IFileSeekService _fileSeekService = null;
        public FormSeeker(IFileSeekService fileReaderService)
        {
            InitializeComponent();
            _fileSeekService = fileReaderService;
            tabBase.Selecting += TabBase_Selecting;
            Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(1000);
                    if (!this.Visible)
                        continue;
                    PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                    PerformanceCounter ramCounter = new PerformanceCounter("Memory", "Available MBytes");
                    this.BeginInvoke(new Action(() =>
                    {
                        this.Text = $"CPU:{cpuCounter.NextValue()} | MEM:{ramCounter.NextValue()} ";
                        this.Refresh();
                    }));
                }
            });
        }

        private void FormSeeker_Load(object sender, EventArgs e)
        {
            tabBasePage.Controls.Add(new UControlSeeker(_fileSeekService) { Size = tabBasePage.Size });
            tabBasePage.SizeChanged += (s, e) => { tabBasePage.Controls[0].Size = tabBasePage.Size; };
        }

        private void TabBase_Selecting(object? sender, TabControlCancelEventArgs e)
        {
            if (sender is TabControl ctrl)
            {
                if (e.TabPageIndex == ctrl.TabCount - 1)
                {
                    AddNewTab($"Tab[{ctrl.TabCount}]");
                    e.Cancel = true;
                }
                foreach (TabPage tab in ctrl.TabPages)
                {
                    if (ctrl.SelectedTab != tab && tab.Controls.Count > 0 && tab.Controls[0] is UControlSeeker seeker)
                    {
                        seeker.KeyWordsDialog.Hide();
                    }
                }
            }
        }
        private void AddNewTab(string tabName)
        {
            var tabPage = new TabPage(tabName);
            tabPage.Controls.Add(new UControlSeeker(_fileSeekService) { Size = tabPage.Size });
            tabPage.SizeChanged += (s, e) => { tabPage.Controls[0].Size = tabPage.Size; };
            tabBase.TabPages.Insert(tabBase.TabCount - 1, tabPage);
        }
    }
}
