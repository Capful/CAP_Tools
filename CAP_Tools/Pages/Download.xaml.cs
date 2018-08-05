using FirstFloor.ModernUI.Windows.Controls;
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

namespace CAP_Tools.Pages
{
    /// <summary>
    /// Interaction logic for Download.xaml
    /// </summary>
    public partial class Download : UserControl
    {
        public Download()
        {
            InitializeComponent();
        }

        private void ModernButton_Click_1(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText("7t1i");
            System.Diagnostics.Process.Start("https://pan.baidu.com/s/1hYtNW-0blxtdLdSoDDKeuQ");
            ModernDialog.ShowMessage("密码已复制到剪切板", "提示", MessageBoxButton.OK);
        }

        private void ModernButton_Click_2(object sender, RoutedEventArgs e)
        {
            
        }

        private void ModernButton_Click_3(object sender, RoutedEventArgs e)
        {

        }

        private void ModernButton_Click_4(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText("51py");
            System.Diagnostics.Process.Start("https://pan.baidu.com/s/18G9oj8SLNiXd3Kxj8SE50w");
            ModernDialog.ShowMessage("密码已复制到剪切板", "提示", MessageBoxButton.OK);
        }
    }
}
