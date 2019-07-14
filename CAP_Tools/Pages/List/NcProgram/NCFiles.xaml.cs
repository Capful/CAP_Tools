using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using static CAP_Tools.MainWindow;

namespace CAP_Tools.Pages.List.NcProgram
{
    /// <summary>
    /// Interaction logic for NCFiles.xaml
    /// </summary>
    public partial class NCFiles : UserControl
    {
        public NCFiles()
        {
            InitializeComponent();
        }

        private void SX_Click(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(Cap.NCFileRoute))
                {
                ///如果存在，将替换按钮显示
                XJ1.IsEnabled = true;
                XJ2.IsEnabled = true;
                XJ3.IsEnabled = true;
                XJ4.IsEnabled = true;
                XJ5.IsEnabled = true;
                FileRoute.Text = System.IO.Path.GetDirectoryName(Cap.NCFileRoute) + "\\程序串联";
            }
            else
            {
                ///如果不存在存在，将替换按钮显示
                XJ1.IsEnabled = false;
                XJ2.IsEnabled = false;
                XJ3.IsEnabled = false;
                XJ4.IsEnabled = false;
                XJ5.IsEnabled = false;
                ModernDialog.ShowMessage("路径不存在                      ", "警告", MessageBoxButton.OK);
            }
        }

        private void XJ1_Click(object sender, RoutedEventArgs e)
        {
            string A1 = FileRoute.Text + "\\" + A.Text + "-" + AA.Text;
            if (Directory.Exists(A1))
            {
                ModernDialog.ShowMessage(A.Text + "-" + AA.Text +" 文件夹已存在！", "警告", MessageBoxButton.OK);
            }
            else
            {
                Directory.CreateDirectory(A1);
            }
        }

        private void XJ2_Click(object sender, RoutedEventArgs e)
        {
            string B1 = FileRoute.Text + "\\" + B.Text + "-" + BB.Text;
            if (Directory.Exists(B1))
            {
                ModernDialog.ShowMessage(B.Text + "-" + BB.Text + " 文件夹已存在！", "警告", MessageBoxButton.OK);
            }
            else
            {
                Directory.CreateDirectory(B1);
            }
        }

        private void XJ3_Click(object sender, RoutedEventArgs e)
        {
            string C1 = FileRoute.Text + "\\" + C.Text + "-" + CC.Text;
            if (Directory.Exists(C1))
            {
                ModernDialog.ShowMessage(C.Text + "-" + CC.Text + " 文件夹已存在！", "警告", MessageBoxButton.OK);
            }
            else
            {
                Directory.CreateDirectory(C1);
            }
        }
        private void XJ4_Click(object sender, RoutedEventArgs e)
        {
            string D1 = FileRoute.Text + "\\" + D.Text + "-" + DD.Text;
            if (Directory.Exists(D1))
            {
                ModernDialog.ShowMessage(D.Text + "-" + DD.Text + " 文件夹已存在！", "警告", MessageBoxButton.OK);
            }
            else
            {
                Directory.CreateDirectory(D1);
            }
        }

        private void XJ5_Click(object sender, RoutedEventArgs e)
        {
            string F1 = FileRoute.Text + "\\" + F.Text + "-" + FF.Text;
            if (Directory.Exists(F1))
            {
                ModernDialog.ShowMessage(F.Text + "-" + FF.Text + " 文件夹已存在！", "警告", MessageBoxButton.OK);
            }
            else
            {
                Directory.CreateDirectory(F1);
            }
        }

        private void FileRoute_Click(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(FileRoute.Text))
            {
                ///如果存在
                Process.Start(FileRoute.Text);
            }
            else
            {
                ///如果不存在
                Process.Start(Cap.NCFileRoute);
            }
        }

    }
}
