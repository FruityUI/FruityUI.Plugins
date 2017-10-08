using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using FruityUI;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Time
{



    public class Time : IPlugin, IDisposable
    {

        private string _author = "LegitSoulja";
        private string _name = "Timer";
        private string _description = "Timer Display";

        public string author { get { return _author;  } }
        public string name { get { return _name; } }
        public string description { get { return _description; } }

        protected static Core core;

        private Window tWindow;
        private System.Timers.Timer timer;

        public Time(Core _core)
        {
            core = _core;
            tWindow = core.createNewWindow(_name, 200, 100, core.screen_width - 200, core.screen_height - 100, true);

            tWindow.Background = new SolidColorBrush()
            {
                Color = Colors.Transparent
            };

            StackPanel panel = new StackPanel()
            {
                Height = tWindow.Height,
                Width = tWindow.Width,
            };

            TextBlock time = new TextBlock()
            {
                FontSize = 20,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Colors.White),
                Effect = new DropShadowEffect()
                {
                    Color = Colors.Black,
                    Opacity = 0.8

                },
                TextWrapping = TextWrapping.Wrap
            };

            panel.Children.Add(time);
            
            time.Text = "Hey! Developed by LegitSoulja :)";

            panel.UpdateLayout();

            timer = new System.Timers.Timer(1000);
            timer.Enabled = true;

            timer.Elapsed += (s, e) =>
            {
                Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    time.Text = DateTime.Now.ToString("hh:mm:ss tt");
                });
            };

            tWindow.Content = panel;
            tWindow.Show();

            timer.Start();

        }

        ~Time()
        {
            Dispose();
        }

        public void Dispose()
        {
            timer.Stop();
            timer.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
