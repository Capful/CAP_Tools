using Microsoft.Win32;
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

namespace NX_Tool.Pages
{
    /// <summary>
    /// Interaction logic for InstallNX.xaml
    /// </summary>
    public partial class InstallNX : UserControl
    {
        public InstallNX()
        {
            InitializeComponent();
        }

        public object DialogResult { get; private set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();

            fdlg.Title = "C# Corner Open File Dialog";

            fdlg.InitialDirectory = @"c:/";

            fdlg.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";

            fdlg.FilterIndex = 2;

            fdlg.RestoreDirectory = true;
        }

    }
}

