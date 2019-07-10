using FirstFloor.ModernUI.Windows.Controls;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace CAP_Tools.Pages.List
{
    /// <summary>
    /// Interaction logic for CheckLicence.xaml
    /// </summary>
    public partial class CheckLicence : UserControl
    {
        public CheckLicence()
        {
            InitializeComponent();

            Licence_Name.Text = "Siemens PLM License Server";

            Licence_Status.Text = GetSystemServiceStatusString(Licence_Name.Text);

            ///创建bat批处理文件
            string Path = "for /f \"tokens=1,2,* \"%%i in ('REG QUERY \"HKEY_LOCAL_MACHINE\\SOFTWARE\\FLEXlm License Manager\\Siemens PLM License Server\" ^| find /i \"Lmgrd\"') do echo %%k>LicencePath.txt";
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "LicencePath.bat", Path, Encoding.Default);
            ThreadDelegate backWorkDel = new ThreadDelegate(LicencePath); //创建一个ThreadDelegate的实例，调用准备在后台运行的函数
            backWorkDel.BeginInvoke(null, null);//使用异步的形式开始执行这个委托
            
            System.Timers.Timer Check = new System.Timers.Timer(1000);//每隔2秒执行一次，没用winfrom自带的
            Check.Elapsed += Check_Elapsed;//委托，要执行的方法
            Check.AutoReset = true;//获取该定时器自动执行
            Check.Enabled = true;//这个一定要写，要不然定时器不会执行的
        }
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            SystemServiceOpen(Licence_Name.Text);
            Licence_Status.Text = GetSystemServiceStatusString(Licence_Name.Text);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            SystemServiceClose(Licence_Name.Text);
            Licence_Status.Text = GetSystemServiceStatusString(Licence_Name.Text);
        }

        private void ReStart_Click(object sender, RoutedEventArgs e)
        {
            SystemServiceClose(Licence_Name.Text);
            Thread.Sleep(100);
            SystemServiceOpen(Licence_Name.Text);
        }

        /// <summary>
        /// 关闭系统服务
        /// </summary>
        /// <param name="serviceName">系统服务名称</param>
        /// <returns></returns>
        public static bool SystemServiceClose(string serviceName)
        {
            try
            {
                using (var control = new ServiceController(serviceName))
                {

                    if (control.Status == ServiceControllerStatus.Running)
                    {
                        control.Stop();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 打开系统服务
        /// </summary>
        /// <param name="serviceName">系统服务名称</param>
        /// <returns></returns>
        public static bool SystemServiceOpen(string serviceName)
        {
            try
            {
                using (var control = new ServiceController(serviceName))
                {
                    if (control.Status != ServiceControllerStatus.Running)
                    {
                        control.Start();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 返回服务状态
        /// </summary>
        /// <param name="serviceName">系统服务名称</param>
        /// <returns>1:服务未运行 2:服务正在启动 3:服务正在停止 4:服务正在运行 5:服务即将继续 6:服务即将暂停 7:服务已暂停 0:未知状态</returns>
        public static string GetSystemServiceStatusString(string serviceName)
        {
            try
            {
                using (var control = new ServiceController(serviceName))
                {
                    var status = string.Empty;
                    switch ((int)control.Status)
                    {
                        case 1:
                            status = "服务未运行";
                            break;
                        case 2:
                            status = "服务正在启动...";
                            break;
                        case 3:
                            status = "服务正在停止...";
                            break;
                        case 4:
                            status = "服务正在运行";
                            break;
                        case 5:
                            status = "服务即将继续";
                            break;
                        case 6:
                            status = "服务即将暂停";
                            break;
                        case 7:
                            status = "服务已暂停";
                            break;
                        case 0:
                            status = "未知状态";
                            break;
                    }
                    return status;
                }
            }
            catch
            {
                return "未知状态";
            }
        }

        private void Check_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.Licence_Status.Dispatcher.Invoke(
               new Action(
                    delegate
                    {
                        Licence_Status.Text = GetSystemServiceStatusString(Licence_Name.Text);
                    }
               )
         );
        }

        
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RunBat(string filename)
        {
            Process pro = new Process();
            FileInfo file = new FileInfo(filename);
            pro.StartInfo.WorkingDirectory = file.Directory.FullName;
            pro.StartInfo.FileName = filename;
            pro.StartInfo.CreateNoWindow = false;
            pro.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            pro.Start();
            pro.WaitForExit();
        }

        private delegate void ThreadDelegate(); //申明一个专用来调用更改线程函数的委托

        private void LicencePath()
        {
            RunBat(AppDomain.CurrentDomain.BaseDirectory + "LicencePath.bat"); //运行Bat文件
            ThreadDelegate changeTetBoxDel = delegate ()  //后台中要更改主线程中的UI，于是我们还是用委托来实现，再创建一个实例
            {
                Licence_Path.Text = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "LicencePath.txt");
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "LicencePath.txt");
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "LicencePath.bat");
            };//要调用的过程
            Dispatcher.BeginInvoke(DispatcherPriority.Send, changeTetBoxDel); //使用分发器让这个委托等待执行
        }
    }
}
