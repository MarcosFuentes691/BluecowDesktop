using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using HackF5.UnitySpy.HearthstoneLib;
using System.Diagnostics;
using HackF5.UnitySpy.Crawler;
using System.Threading;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DesktopBluecow
{
    static class Program
    {

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }



        
    }
}
