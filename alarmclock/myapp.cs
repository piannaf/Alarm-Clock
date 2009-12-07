//http://msdn.microsoft.com/en-us/library/aa970678.aspx#Build_a_WPF_Application_Using_MSBuild
//http://www.tbiro.com/Hello-WPF-Without-XAML.htm

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Piannaf.Ports.Microsoft.Samples.WinFX.AlarmClock{
    public class MyApp : Application{
        // Entry point method
        [STAThread]
        public static void Main(){
            MyApp app = new MyApp();
            app.Run();
        }

        MyApp()
        {
            this.Startup += new StartupEventHandler(AppStartup);
        }

        void AppStartup(object sender, StartupEventArgs e){
            TraditionalClock tradClock = new TraditionalClock();
            tradClock.Opacity = .6f;
            tradClock.Show();
        }

        void clockWindow_Closed(object sender, EventArgs e){
            Application.Current.Shutdown(0);
        }
    }
}
