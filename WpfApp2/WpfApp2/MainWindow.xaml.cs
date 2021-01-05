using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Windows.Media.Animation;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.IO.Compression;


namespace WpfApp2
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        DirectoryInfo winTemp = new DirectoryInfo(System.IO.Path.GetTempPath());
        public long size;
        public void Size()
        {
            /*DirectoryInfo winTemp = new DirectoryInfo(System.IO.Path.GetTempPath());*/
            long size = 0;
            /*var myDir = $@"C:\Users\Youcode\Desktop\screen";
             
            var dirInfo = new DirectoryInfo(myDir);*/

            foreach (FileInfo fi in winTemp.EnumerateFiles("*", SearchOption.AllDirectories))
            {
                size += fi.Length;
            }
            lbl_title_nett.Content = $"Espace à nettoyer :   {size} bytes";
        }

        public long Sizef()
        {
            /*DirectoryInfo winTemp = new DirectoryInfo(System.IO.Path.GetTempPath());*/
            long size = 0;
            /*var myDir = $@"C:\Users\Youcode\Desktop\screen";
            var dirInfo = new DirectoryInfo(myDir);*/

            foreach (FileInfo fi in winTemp.EnumerateFiles("*", SearchOption.AllDirectories))
            {
                size += fi.Length;
            }
            return size;
        }

        private void Analyse_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hello first analyse");
            progress.Visibility = Visibility.Visible;
            lbl_analyse_pc.Content = "Analyse en cours...";
            progress_pourc.Visibility = Visibility.Visible;
            lbl_title_analyse.Visibility = Visibility.Hidden;
            lbl_analyse.Visibility = Visibility.Hidden;
            lbl_title_maj.Visibility = Visibility.Hidden;
            lbl_maj.Visibility = Visibility.Hidden;
            lbl_title_nett.Visibility = Visibility.Hidden;
            lbl_title_nett.Visibility = Visibility.Hidden;
            btn_vue.IsEnabled = false;
            btn_histo.IsEnabled = false;
            btn_option.IsEnabled = false;
            btn_nettoyer.IsEnabled = false;
            btn_historique.IsEnabled = false;
            btn_maj.IsEnabled = false;
            btn_analyse.IsEnabled = false;


            Task.Run(() =>
            {
                for (int i = 0; i <= 100; i++)
                {
                    //Duration duration = new Duration(TimeSpan.FromSeconds(50));
                    //DoubleAnimation doubleAnimation = new DoubleAnimation(progress.Value + i, duration);
                    //progress.BeginAnimation(ProgressBar.ValueProperty, doubleAnimation);

                    Thread.Sleep(50);
                    this.Dispatcher.Invoke(() =>
                    {
                        progress.Value = i;
                        progress_pourc.Text = i.ToString() + "%";
                        Size();

                        if (progress.Value == 100)
                        {
                            MessageBox.Show("analyse terminée");
                            lbl_title_analyse.Visibility = Visibility.Visible;
                            lbl_analyse.Visibility = Visibility.Visible;
                            lbl_title_maj.Visibility = Visibility.Visible;
                            lbl_maj.Visibility = Visibility.Visible;
                            lbl_title_nett.Visibility = Visibility.Visible;
                            btn_vue.IsEnabled = true;
                            btn_histo.IsEnabled = true;
                            btn_option.IsEnabled = true;
                            btn_nettoyer.IsEnabled = true;
                            btn_historique.IsEnabled = true;
                            btn_maj.IsEnabled = true;
                            btn_analyse.IsEnabled = true;
                            progress.Visibility = Visibility.Hidden;
                            progress_pourc.Visibility = Visibility.Hidden;
                            lbl_analyse.Content = DateTime.Now.ToString();
                        }
                    });
                }
            });
        }

        public void Delete()
        {
            using (StreamWriter sw = File.AppendText("file.txt"))
            {
                sw.WriteLine($"Size of analyse is {Sizef()} MB");
                sw.WriteLine("Date of analyse: " + DateTime.Now.ToString());
                sw.WriteLine("==================");
            }

            foreach (FileInfo file in winTemp.EnumerateFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in winTemp.EnumerateDirectories())
            {
                dir.Delete(true);
            }
        }
        private void nettoyer_click(object sender, RoutedEventArgs e)
        {
            /*string path = @"C:\Users\Youcode\Desktop\screen\";

            DirectoryInfo directory = new DirectoryInfo(path);

            foreach (FileInfo file in directory.GetFiles())
            {
                file.Delete();
            }
            MessageBox.Show("les fichier sont supprimer");*/

            Delete();
        }

        private void historique_click(object sender, RoutedEventArgs e)
        {
            Process.Start("file.txt");
        }

        private void btn_maj_Click_1(object sender, RoutedEventArgs e)
        {
            InitializeComponent();

            WebClient webClient = new WebClient();
            if (!webClient.DownloadString("https://pastebin.com/raw/FLt8bViR").Contains("1.1"))
            {
                var answer = MessageBox.Show("there is an availabale update, would you like to downoald it", "compuetr scan", MessageBoxButton.YesNo, MessageBoxImage.Information);

                if (answer == MessageBoxResult.Yes)
                {
                    this.Hide();
                    updater update = new updater();
                    update.ShowDialog();
                }
            }
        }
    }
}

