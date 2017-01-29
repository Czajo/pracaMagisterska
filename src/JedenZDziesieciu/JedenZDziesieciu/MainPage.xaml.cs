// Copyright (c) Microsoft. All rights reserved.

using System;
using System.Threading;
using Windows.Devices.Gpio;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;

namespace JedenZDziesieciu
{
    public sealed partial class MainPage : Page
    {
        private const int LED_PIN = 5;
        private GpioPin pin;
        private GpioPinValue pinValue;
        private DispatcherTimer timer;
        private const int LED_PIN2 = 6;
        private GpioPin pin2;
        private GpioPinValue pinValue2;


        public MainPage()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += Timer_Tick;
            InitGPIO();
                
        }

        private void InitGPIO()
        {
            var gpio = GpioController.GetDefault();

            
            if (gpio == null)
            {
                pin = null;
                return;
            }

            pin = gpio.OpenPin(LED_PIN);
            pin2 = gpio.OpenPin(LED_PIN2);
            pinValue = GpioPinValue.High;
            pinValue2 = GpioPinValue.High;
            pin.Write(pinValue);
            pin2.Write(pinValue2);
            pin.SetDriveMode(GpioPinDriveMode.Output);
            pin2.SetDriveMode(GpioPinDriveMode.Output);
            
        }

        private void Timer_Tick(object sender, object e)
        {
            if (pinValue == GpioPinValue.High)
            {
                pinValue = GpioPinValue.Low;
                pinValue2 = GpioPinValue.High;
                pin.Write(pinValue);
                pin2.Write(pinValue2);
            }
            else
            {
                pinValue = GpioPinValue.High;
                pinValue2 = GpioPinValue.Low;
                pin.Write(pinValue);
                pin2.Write(pinValue2);
            }
        }

        private void mix_Click(object sender, RoutedEventArgs e)
        {
            if (pinValue == GpioPinValue.High)
            {
                pinValue = GpioPinValue.Low;
                pinValue2 = GpioPinValue.High;
                pin.Write(pinValue);
                pin2.Write(pinValue2);
            }
            else
            {
                pinValue = GpioPinValue.High;
                pinValue2 = GpioPinValue.Low;
                pin.Write(pinValue);
                pin2.Write(pinValue2);
            }
        }

        private void relay1_Click(object sender, RoutedEventArgs e)
        {
            if (pinValue == GpioPinValue.High)
            {
                pinValue = GpioPinValue.Low;
                pin.Write(pinValue);
            }
            else
            {
                pinValue = GpioPinValue.High;
                pin.Write(pinValue);
            }
        }

        private void relay2_Click(object sender, RoutedEventArgs e)
        {
            if (pinValue2 == GpioPinValue.High)
            {

                pinValue2 = GpioPinValue.Low;
                pin2.Write(pinValue2);
            }
            else
            {
                pinValue2 = GpioPinValue.High;
                pin2.Write(pinValue2);
            }
        }

        private void dispatcher_Click(object sender, RoutedEventArgs e)
        {
            if (timer.IsEnabled)
                timer.Stop();
            else
                timer.Start();
        }
    }
}
