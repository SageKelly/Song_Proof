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
    public sealed partial class HW3IntervalHolderControl : UserControl, INotifyPropertyChanged
    {
        private const int MIN_NOTE_SIZE = 2;
        private const int COLUMN_WIDTH = 106;


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

        private List<TextBlock> NoteControls;

        public event PropertyChangedEventHandler PropertyChanged;

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
        private string[] notes;

        public HW3IntervalHolderControl(int[] intervalList, string[] piano, string methodName, Type targetType, object methodTarget)
        {
            this.InitializeComponent();
            DataContext = this;

            _methodTarget = methodTarget;
            timerMethod = targetType.GetTypeInfo().GetDeclaredMethod(methodName);

            TrackingTime = true;
            CountingDown = true;

            SetTimerInterval(3000);

            notes = new string[intervalList.Length + 1];

            if (intervalList.Length < MIN_NOTE_SIZE)
            {
                throw new Exception("Note size is lower than minimum");
            }
            NoteControls = new List<TextBlock>();

            int gridSize = (intervalList.Length * 2) + 1;

            //Set up letters for proofing
            Random r = new Random();
            int pianoIndex = r.Next(0, piano.Length);
            notes[0] = piano[pianoIndex];
            int noteIndex = 1;
            do
            {
                pianoIndex = (pianoIndex + intervalList[noteIndex - 1]) % piano.Length;

                notes[noteIndex] = piano[pianoIndex];
                noteIndex++;
            } while (noteIndex < notes.Length);

            //places the letters in the grid
            int intervalIndex = 0;
            for (int i = 0; i < gridSize; i++)
            {
                TextBlock tb = new TextBlock()
                {
                    Text = i % 2 == 0 ? notes[intervalIndex].ToString() : "←",
                    Style = i % 2 == 0 ? TBStyle : TBArrowStyle
                };

                if (i % 2 == 0)
                {
                    intervalIndex++;
                }
                NoteControls.Add(tb);

            }
            NoteStack.ItemsSource = NoteControls;
        }

        public void Next()
        {
            SlideLeft.Begin();
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
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
