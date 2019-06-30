using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace CAP_Tools.Pages.Settings
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : UserControl
    {
        public About()
        {
            InitializeComponent();
        }

        private void Change_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string changelog = AppDomain.CurrentDomain.BaseDirectory + "changelog.txt";
            string str = File.ReadAllText(changelog, Encoding.Default);
            var dlg = new ModernDialog
            {
                Title = "更新日志",
                Content = str
            };
            dlg.Buttons = new Button[] { dlg.OkButton, dlg.CancelButton };
            dlg.ShowDialog();
            
        }
    }
}
