using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Phone.Controls;
using Microsoft.Devices;

namespace PhoneApp4
{
    public partial class MainPage : PhoneApplicationPage
    {
        int trackCountdown;
        int targetCount = 0;
        int score = 0;
        int startCountdown = 10;
        bool isChaseOn = false;

        
        Button[] buttonArray = new Button[9];
        List<Button> buttonList = new List<Button>();

        VibrateController vc = VibrateController.Default;

        DispatcherTimer trackTimer = new DispatcherTimer();
        DispatcherTimer gameTimer = new DispatcherTimer();


        // Constructor
        public MainPage()
        {
            InitializeComponent();

            buttonArray[0] = button1;
            buttonArray[1] = button2;
            buttonArray[2] = button3;
            buttonArray[3] = button4;
            buttonArray[4] = button5;
            buttonArray[5] = button6;
            buttonArray[6] = button7;
            buttonArray[7] = button8;
            buttonArray[8] = button9;

            scoreBox.Text = "Score: 0000";
            gameTimer.Tick += OnGameTick;
            trackTimer.Tick += OnTrackTick;
            gameTimer.Interval = TimeSpan.FromMilliseconds(3000);
            
        }

        void OnGameTick(object sendder, EventArgs args)
        {
            buttonList.Clear();
            //todo rand
            buttonList.Add(button3);
            buttonList.Add(button7);
            buttonList.Add(button2);
            //rand

            foreach(Button butt in buttonList)
            {                
                butt.Background = new SolidColorBrush(Color.FromArgb(255,0,255,0));
            }

            trackTimer.Interval = TimeSpan.FromMilliseconds(100);
            isChaseOn = true;
            trackCountdown = startCountdown;
            trackTimer.Start();
        }

        void OnTrackTick(object sendder, EventArgs args)
        {
            if (isChaseOn == true)
            {
                if(trackCountdown >= 0)
                {
                    countdown.Text = trackCountdown--.ToString();
                }else{
                    countdown.Text = "";
                    isChaseOn = false;
                }
                
            }else{
                trackTimer.Stop();
                startCountdown--;
                if (targetCount == 0)
                {
                    countdown.Text = "game over";
                    startCountdown = 10;
                    gameTimer.Stop();
                    
                }
                targetCount = 0;
                if (buttonList.Count != 0)
                {
                    foreach (Button buttz in buttonList)
                    {
                        buttz.Background = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                    }
                    buttonList.Clear();
                }
            }
        }


        private void track_button_Click(object sender, RoutedEventArgs e)
        {

            if (isChaseOn == true)
            { 
                if(buttonList.Contains((Button)sender))
                {
                    Button butty = new Button();
                    butty = (Button)sender;
                    butty.Background = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                    buttonList.Remove(butty);
                    score++;
                    targetCount++;
                    scoreBox.Text = "score: " + score.ToString();
                }
            }
        }

         private void start_button_Click(object sender, RoutedEventArgs e)
        {
            countdown.Text = "";
            score = 0;
            scoreBox.Text = "score: " + score.ToString();
            gameTimer.Start();
            

        }

         private void change_button_click(object sender, RoutedEventArgs e)
         {
             string dest = "/page1.xaml";
             this.NavigationService.Navigate(new Uri(dest, UriKind.Relative));
         }
    }
}