using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.IO.Compression;


namespace WpfApp2
{
    /// <summary>
    /// Logique d'interaction pour updater.xaml
    /// </summary>
    public partial class updater : Window
    {
        WebClient client = new WebClient();
        string comprPath = @"C:/Users/Youcode/Desktop/PC-CLEANER/WpfApp2/WpfApp2/bin/Release";
        string comprLocal = @"C:/Users/Youcode/Desktop/PC-CLEANER/WpfApp2/WpfApp2/bin/versionprecedente.zip";

        public updater()
        {
            InitializeComponent();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                try
                {
                    for (int i = 0; i <= 100; i++)
                    {
                        this.Dispatcher.Invoke(() => //Use Dispather to Update UI Immediately
                        {
                            progressbar.Value = i;
                            lbl_prcnt.Content = i.ToString() + "%";

                              if (progressbar.Value == 100)
                              {
                                  ZipFile.CreateFromDirectory(comprPath, comprLocal);
                              }
                        });
                    }
                }
                catch (Exception)
                {
                    Environment.Exit(0);
                }
            });

            client.DownloadFile("https://wafaasbs.weebly.com/uploads/1/3/5/4/135400790/update.zip", @"C:\Users\Youcode\Desktop\PC-CLEANER\Nouveau dossier");
            string zipPath = @"C:/Users/Youcode/Desktop/PC-CLEANER/Nouveau dossier/update.zip";
            string extractPath = @"C:/Users/Youcode/Desktop/PC-CLEANER/WpfApp2/WpfApp2/bin/Release";

            ZipFile.ExtractToDirectory(zipPath, extractPath);
            Process.Start($"{extractPath}/WpfApp2.exe");
        }

    }
    
}
