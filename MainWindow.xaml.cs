using System;
using System.Collections.Generic;
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

namespace MatchGamev
{
    using Microsoft.Win32;
    using System.Windows.Threading;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecondsElapsed=0;
        

        TextBlock lastTextBolockClicked;
        bool findingMatch = false;
        int matchesFound = 0;
        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            SetUpGame();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            tenthsOfSecondsElapsed++;
            timeTextBlock.Text = (tenthsOfSecondsElapsed / 10f).ToString("0.0s");
            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Jeszcze raz?";
            }
        }

        private void SetUpGame()
        {
            List<string> animalEmoji = new List<string>()
            {
                "😺","😺",
                "🐵","🐵",
                "🐶","🐶",
                "🐺","🐺",
                "🦍","🦍",
                "🦝","🦝",
                "🐷","🐷",
                "🐴","🐴",
            };
            Random random = new Random();
            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name == "timeTextBlock")
                {
                    continue;
                }
                textBlock.Visibility = Visibility.Visible;
                int index = random.Next(animalEmoji.Count);
                textBlock.Text = animalEmoji[index];
                animalEmoji.RemoveAt(index);
            }
            timer.Start();
            matchesFound = 0;
            tenthsOfSecondsElapsed = 0;
        }
        
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if (findingMatch == false)
            {
                lastTextBolockClicked = textBlock;
                findingMatch = true;
                textBlock.Visibility = Visibility.Hidden;

            }
            else if (textBlock.Text == lastTextBolockClicked.Text)
            {
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
                matchesFound++;

            }
            else
            {
                lastTextBolockClicked.Visibility = Visibility.Visible;
                findingMatch= false;    
            }
        }

        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
            {
                SetUpGame();
            }
        }
    }
}
