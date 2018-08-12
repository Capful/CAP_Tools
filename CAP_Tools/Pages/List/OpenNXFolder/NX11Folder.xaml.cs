using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;

namespace CAP_Tools.Pages.List.OpenFolder
{
    /// <summary>
    /// Interaction logic for NX11Folder.xaml
    /// </summary>
    public partial class NX11Folder : UserControl
    {
        public NX11Folder()
        {
            InitializeComponent();
            ///判断NX11是否安装
            if (CheckNX11() == true)
            {
                ///已安装
                ///指定路径
                RegistryKey driverKey = NXregistry();
                string EXE = (String)driverKey.GetValue("Unigraphics V29.0");
                string Home = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@EXE)));
                string UGII_LJ = @"UGII";
                string ModelTemplates_LJ = @"LOCALIZATION\prc\simpl_chinese\startup";
                string Template_Part_LJ = @"MACH\resource\template_part\metric";
                string Postprocessor_LJ = @"MACH\resource\postprocessor";
                ///合并路径
                string UGII = System.IO.Path.Combine(Home, UGII_LJ);
                string ModelTemplates = System.IO.Path.Combine(Home, ModelTemplates_LJ);
                string Template_Part = System.IO.Path.Combine(Home, Template_Part_LJ);
                string Postprocessor = System.IO.Path.Combine(Home, Postprocessor_LJ);
                ///指定鼠标悬停提示
                this.Home.ToolTip = Home.ToString();
                this.UGII.ToolTip = UGII.ToString();
                this.ModelTemplates.ToolTip = ModelTemplates.ToString();
                this.Template_Part.ToolTip = Template_Part.ToString();
                this.Postprocessor.ToolTip = Postprocessor.ToString();
            }
            else
            {
                ///未安装
                ///定义按钮为灰色不可选
                this.Home.IsEnabled = false;
                this.UGII.IsEnabled = false;
                this.ModelTemplates.IsEnabled = false;
                this.Template_Part.IsEnabled = false;
                this.Postprocessor.IsEnabled = false;
                ///定义提示文字
                this.Tip.Text = "抱歉，您未安装NX11";
            }
        }

        private static RegistryKey NXregistry()
        {
            return Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Unigraphics Solutions\Installed Applications");
        }

        private bool CheckNX11()
        {
            RegistryKey driverKey = NXregistry();
            string NX11EXE = (String)driverKey.GetValue("Unigraphics V29.0");
            string NX11 = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@NX11EXE)));
            if (NX11 != null)
            {
                return true;
            }
            return false;
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            ///获取NX安装路径
            RegistryKey driverKey = NXregistry();
            string EXE = (String)driverKey.GetValue("Unigraphics V29.0");
            string Home = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@EXE)));
            ///回退2级目录(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@"C:\ABC\Temp\DC\")))得到"C:\ABC\Temp"
            ///打开主目录
            System.Diagnostics.Process.Start(@Home);
        }

        private void UGII_Click(object sender, RoutedEventArgs e)
        {
            RegistryKey driverKey = NXregistry();
            string EXE = (String)driverKey.GetValue("Unigraphics V29.0");
            string Home = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@EXE)));
            string UGII_LJ = @"UGII";
            string Path = System.IO.Path.Combine(Home, UGII_LJ);
            ///打开UGII目录
            System.Diagnostics.Process.Start(@Path);
        }

        private void ModelTemplates_Click(object sender, RoutedEventArgs e)
        {
            RegistryKey driverKey = NXregistry();
            string EXE = (String)driverKey.GetValue("Unigraphics V29.0");
            string Home = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@EXE)));
            string ModelTemplates_LJ = @"LOCALIZATION\prc\simpl_chinese\startup";
            string Path = System.IO.Path.Combine(Home, ModelTemplates_LJ);
            ///打开默认模板目录
            System.Diagnostics.Process.Start(@Path);
        }

        private void Template_Part_Click(object sender, RoutedEventArgs e)
        {
            RegistryKey driverKey = NXregistry();
            string EXE = (String)driverKey.GetValue("Unigraphics V29.0");
            string Home = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@EXE)));
            string Template_Part_LJ = @"MACH\resource\template_part\metric";
            string Path = System.IO.Path.Combine(Home, Template_Part_LJ);
            ///打开加工模板目录
            System.Diagnostics.Process.Start(@Path);
        }

        private void Postprocessor_Click(object sender, RoutedEventArgs e)
        {
            RegistryKey driverKey = NXregistry();
            string EXE = (String)driverKey.GetValue("Unigraphics V29.0");
            string Home = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@EXE)));
            string Postprocessor_LJ = @"MACH\resource\postprocessor";
            string Path = System.IO.Path.Combine(Home, Postprocessor_LJ);
            ///打开机床后处理目录
            System.Diagnostics.Process.Start(@Path);
        }
    }
}
