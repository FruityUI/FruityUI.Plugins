using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FruityUI;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Timers;

namespace CPU
{

    public class CPU : IPlugin, IDisposable
    {

        public string author { get { return "LegitSoulja"; } }
        public string name { get {  return "CPU";  } }
        public string description { get { return "Display CPU & RAM";  } }

        protected static Core core;

        private Window w;

        private PerformanceCounter cpu = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        private PerformanceCounter ram = new PerformanceCounter("Memory", "Available MBytes");

        public CPU(Core _core)
        {

            try
            {
                core = _core;
                w = core.createNewWindow(name, 200, 200, core.screen_width - 200, 20, true);
                w.Background = new SolidColorBrush(Colors.Transparent);


                StackPanel panel = new StackPanel()
                {
                    Width = w.Width,
                    Height = w.Height
                };

                Label label = new Label ()
                {
                    FontSize = 20,
                    FontWeight = FontWeights.Bold,
                    Foreground = new SolidColorBrush(Colors.White),
                    Effect = new DropShadowEffect()
                    {
                        Color = Colors.Black,
                        Opacity = 0.8
                    },
 
                };

                panel.Children.Add(label);

                panel.UpdateLayout();

                core.onSecond += (s, e) =>
                {
                    Application.Current.Dispatcher.Invoke((Action)delegate
                    {
                        label.Content = "CPU > " + (Convert.ToInt32(cpu.NextValue()).ToString()) + "%" + Environment.NewLine + "RAM > " + ram.NextValue().ToString() + "MB";
                    });
                };

                w.Content = panel;
                w.Show();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        ~CPU()
        {
            Dispose();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }

}
