using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace ChromiumBasedBrowser
{
    public partial class Browser : Form
    {

        public ChromiumWebBrowser browser;
        public Browser()
        {
            InitializeComponent();
            InitializeBrowser();
            InitializeForm();
        }

        private void InitializeForm()
        {
            BrowserTabs.Height = ClientRectangle.Height - 25;
        }

        private void InitializeBrowser()
        {
            Cef.Initialize(new CefSettings());
            AddBrowserTab();
            BrowserTabs.TabPages[0].Dispose();
            BrowserTabs.TabPages[0].Dispose();


        }

        private void toolStripButtonGo_Click(object sender, EventArgs e)
        {
            Navigate(toolStripAddressBar.Text);
        }


        private void toolStripButtonBack_Click(object sender, EventArgs e)
        {
            browser.Back();
        }

        private void toolStripButtonForward_Click(object sender, EventArgs e)
        {
            browser.Forward();
        }

        private void Browser_TitleChanged(object sender, TitleChangedEventArgs e)
        {
            var selectedBrowser = (ChromiumWebBrowser)sender;
            this.Invoke(new MethodInvoker(() =>
            {
                selectedBrowser.Parent.Text = e.Title;
            }));
        }

        private void Browser_AddressChanged(object sender, AddressChangedEventArgs e)
        {
            var selectedBrowser = (ChromiumWebBrowser)sender;
            this.Invoke(new MethodInvoker(() =>
            {
                toolStripAddressBar.Text = e.Address;
            }));
        }

        private void toolStripButtonReload_Click(object sender, EventArgs e)
        {
            browser.Reload();
        }

        private void toolStripAddressBar_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                Navigate(toolStripAddressBar.Text);
            }
        }

        private void Navigate(string address)
        {
            try
            {
                var selectedBrowser = (ChromiumWebBrowser)BrowserTabs.SelectedTab.Controls[0];
                selectedBrowser.Load(address);
            }
            catch
            {

            }
        }

        private void toolStripButtonAddTab_Click(object sender, EventArgs e)
        {
            AddBrowserTab();
            BrowserTabs.SelectedTab = BrowserTabs.TabPages[BrowserTabs.TabPages.Count - 1];
        }

        private void AddBrowserTab()
        {
            var newTabPage = new TabPage();
            newTabPage.Text = "New Tab";
            BrowserTabs.TabPages.Add(newTabPage);

            
            browser = new ChromiumWebBrowser("https:/google.com");
            browser.AddressChanged += Browser_AddressChanged;
            browser.TitleChanged += Browser_TitleChanged;
            browser.Dock = DockStyle.Fill;
            newTabPage.Controls.Add(browser);
            
        }
    }
}
