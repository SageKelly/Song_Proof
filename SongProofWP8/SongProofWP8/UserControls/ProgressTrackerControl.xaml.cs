using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace SongProofWP8.UserControls
{
    public sealed partial class ProgressTrackerControl : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int _minValue;
        public int MinValue
        {
            get
            {
                return _minValue;
            }
            set
            {
                if (_minValue != value)
                {
                    _minValue = value;
                    NotifyPropertyChanged("MinValue");
                }
            }
        }

        private int _maxValue;
        public int MaxValue
        {
            get
            {
                return _maxValue;
            }
            set
            {
                if (_maxValue != value)
                {
                    _maxValue = value;
                    NotifyPropertyChanged("MaxValue");
                }
            }
        }

        private string _testingLabel;
        public string TestingLabel
        {
            get { return _testingLabel; }
            set
            {
                if (_testingLabel != value)
                {
                    _testingLabel = value;
                    NotifyPropertyChanged("TestingLabel");
                }
            }
        }

        private string _testingValue;
        public string TestingValue
        {
            get { return _testingValue; }
            set
            {
                if (_testingValue != value)
                {
                    _testingValue = value;
                    NotifyPropertyChanged("TestingValue");
                }
            }
        }

        private double _pBValue;
        public double PBValue
        {
            get { return _pBValue; }
            set
            {
                if (_pBValue != value)
                {
                    _pBValue = value;
                    NotifyPropertyChanged("PBValue");
                }
            }
        }

        public int RemainingTime
        {
            get
            {
                return (int)ColorChange.GetCurrentTime().TotalMilliseconds;
            }
        }
        /// <summary>
        /// Denotes whether or not the initial countdown is happening
        /// </summary>
        public bool CountingDown { get; set; }

        /// <summary>
        /// Denotes whether or not the timer should be working.
        /// </summary>
        public bool TrackingTime { get; set; }

        private object _methodTarget;
        private MethodInfo timerMethod;
        /// <summary>
        /// Makes a ProgressTrackerControl to visually represent the user's progress
        /// </summary>
        /// <param name="testingLabel">The label to be printed
        /// above the place showing the testing value</param>
        public ProgressTrackerControl(string testingLabel, string timerMethodName, object methodTarget, Type targetType)
        {
            this.InitializeComponent();
            TestingLabel = testingLabel;

            _methodTarget = methodTarget;
            timerMethod = targetType.GetTypeInfo().GetDeclaredMethod(timerMethodName);

            TrackingTime = true;

            DataContext = this;
            CountingDown = true;
            SetTimerInterval(3000);

            ColorChange.Completed += ColorChange_Completed;
        }

        void ColorChange_Completed(object sender, object e)
        {
            timerMethod.Invoke(_methodTarget, new object[1] { sender });
        }

        public void StartTimer()
        {
            if (TrackingTime)
            {
                ColorChange.Stop();
                ColorChange.Begin(); 
            }
        }

        public void StopTimer()
        {
            if (TrackingTime)
            {
                ColorChange.Pause(); 
            }
        }

        /// <summary>
        /// Sets the interval for the timer, in milliseconds
        /// </summary>
        /// <param name="interval">The interval value</param>
        public void SetTimerInterval(int interval)
        {
            CKFM.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(interval / 2.00));
            CKFE.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(interval));

            VKFM.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(interval / 2.00));
            VKFE.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(interval));
        }


        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
