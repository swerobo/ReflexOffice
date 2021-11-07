using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ReflexOffice.WinForm_DevExpress
{
    public partial class ReflexOfficeMainForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public class AppSettings
        {
            public string AppName { get; set; }
            public Size AppSize { get; set; }
            public string AppStartPosition { get; set;}
        }
        public ReflexOfficeMainForm()
        {
            InitializeComponent();
        }

        private void MainView_Load(object sender, EventArgs e)
        {
            var appSettings = Program.Configuration.GetSection("AppSettings").Get<AppSettings>();
            this.Text = appSettings.AppName;
            this.Size = appSettings.AppSize;
            switch(appSettings.AppStartPosition)
            {
                case "CenterScreen":
                    this.StartPosition = FormStartPosition.CenterScreen;
                break;

                case "CenterParent":
                    this.StartPosition = FormStartPosition.CenterParent;
                    break;

                default:
                    this.StartPosition = FormStartPosition.CenterScreen;
                break;
            }
                
            
        }
    }
}
