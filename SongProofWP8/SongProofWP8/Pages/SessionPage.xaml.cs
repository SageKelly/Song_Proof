using SongProofWP8.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.Phone.UI.Input;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace SongProofWP8.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SessionPage : Page, INotifyPropertyChanged
    {
        private int curIndex;
        private int note_index;
        private int note_number;
        private string scale_name;
        private bool SessionRunning = false;
        private string[] piano;

        private const string IH = "H";
        private const string IW = "W";
        private const string I3 = "-3";

        /// <summary>
        /// Represents the last button that was styled via validation
        /// </summary>
        private Button lastStyledButton;

        private SolidColorBrush XBrush;
        private SolidColorBrush CheckBrush;
        private SolidColorBrush DefaultBrush;

        #region Properties
        public Session curSession { get; private set; }

        public string ScaleName
        {
            get
            {
                return scale_name;
            }
            set
            {
                if (scale_name != value)
                {
                    scale_name = value;
                    NotifyPropertyChanged("ScaleName");
                }
            }
        }

        public int NoteNumber
        {
            get
            {
                return note_number;
            }
            set
            {
                if (note_number != value)
                {
                    note_number = value;
                    NotifyPropertyChanged("NoteNumber");
                }
            }
        }

        #region HW3 Variables

        private int incrementor;
        #endregion

        /// <summary>
        /// The note index number in relation to the scale
        /// </summary>
        public int NoteIndex
        {
            get
            { return note_index; }
            private set
            {
                if (note_index != value)
                {
                    note_index = value;
                    NotifyPropertyChanged("NoteIndex");
                }
            }
        }
        #endregion

        ProgressTrackerControl ptc;
        SessionButtonsControl sbc;
        HW3ProgressTrackerControl hptc;

        public event PropertyChangedEventHandler PropertyChanged;

        public SessionPage()
        {
            this.InitializeComponent();

            XBrush = new SolidColorBrush(new Color()
            {
                //#FFB60000
                R = 182,
                G = 0,
                B = 0,
                A = 255
            });

            CheckBrush = new SolidColorBrush(new Color()
            {
                //#FF5BD419
                R = 91,
                G = 212,
                B = 25,
                A = 255
            });

            curSession = DataHolder.SM.CurrentSession;
            if (DataHolder.ProofType == DataHolder.ProofingTypes.PlacingTheNote)
            {
                ScaleName = curSession.ScaleUsed.Name;
            }
            piano = curSession.Piano;
            DataContext = this;
            curIndex = -1;

            incrementor = 0;
            try
            {

                SetTitleText();
                SetupProgressTracker();
                SetupButtons();
                SetScreenOrientation();
            }
            catch (Exception ex)
            {
                string exception = DataHolder.GetInnerException(ex);
                Debug.WriteLine(exception);
                throw ex;
            }


            sbc = new SessionButtonsControl("B_Start_Click", "B_Quit_Click", "B_ViewResults_Click", this, typeof(SessionPage));

            LayoutRoot.Children.Add(sbc);
            Grid.SetRow(sbc, 3);
            sbc.EnableViewResults(false);

            if (DataHolder.ProofType == DataHolder.ProofingTypes.PlacingTheNote)
            {
                ptc.MaxValue = curSession.ProofingData.Length;
                ptc.PBValue = 0;
            }

            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            e.Handled = true;
            Frame.Navigate(typeof(MainPage));
        }
        #region Global Methods
        private void SetTitleText()
        {
            TitleBarControl tbc = new TitleBarControl("");
            switch (DataHolder.ProofType)
            {
                case DataHolder.ProofingTypes.PlacingTheNote:
                    tbc = new TitleBarControl(ScaleName, 32);
                    break;
                case DataHolder.ProofingTypes.HW3:
                    tbc = new TitleBarControl("HW3");
                    break;
            }
            LayoutRoot.Children.Add(tbc);
            Grid.SetRow(tbc, 0);
        }

        private void SetupProgressTracker()
        {
            switch (DataHolder.ProofType)
            {
                case DataHolder.ProofingTypes.PlacingTheNote:
                    ptc = new ProgressTrackerControl("Note Amount", "TimerComplete", this, typeof(SessionPage));
                    break;
                case DataHolder.ProofingTypes.HW3:
                    hptc = new HW3ProgressTrackerControl(curSession.Data.Length, "HW3TickDown", this, typeof(SessionPage));
                    LayoutRoot.Children.Add(hptc);
                    Grid.SetRow(hptc, 1);
                    break;
            }
        }

        private void SetupButtons()
        {
            switch (DataHolder.ProofType)
            {
                case DataHolder.ProofingTypes.PlacingTheNote:
                    PianoKeyControl pkc = new PianoKeyControl(curSession.Piano, curSession.ScaleUsed.Notes, "NoteClick", this, typeof(SessionPage));
                    LayoutRoot.Children.Add(pkc);
                    Grid.SetRow(pkc, 2);
                    pkc.InstallControl(ptc);
                    break;
                case DataHolder.ProofingTypes.HW3:
                    HW3ButtonsControl hbc = new HW3ButtonsControl(IH, IW, I3, "IntervalClick", this, typeof(SessionPage));
                    LayoutRoot.Children.Add(hbc);
                    Grid.SetRow(hbc, 2);
                    break;
            }
        }

        private void RecordData(int index, bool correct)
        {
            switch (DataHolder.ProofType)
            {
                case DataHolder.ProofingTypes.PlacingTheNote:
                    curSession.StoreData(index, correct, curSession.Diff.GetHashCode() - ptc.RemainingTime);
                    break;
                case DataHolder.ProofingTypes.HW3:
                    curSession.StoreData(index, correct, curSession.Diff.GetHashCode() - hptc.RemainingTime);
                    break;
            }
        }

        private void SetScreenOrientation()
        {
            switch (DataHolder.ProofType)
            {
                case DataHolder.ProofingTypes.Dummy:
                    break;
                case DataHolder.ProofingTypes.BuildTheScale:
                    break;
                case DataHolder.ProofingTypes.PlacingTheNote:
                    DisplayInformation.AutoRotationPreferences = DisplayOrientations.Landscape;
                    break;
                case DataHolder.ProofingTypes.FindTheVoice:
                    break;
                case DataHolder.ProofingTypes.BuildTheChord:
                    break;
                case DataHolder.ProofingTypes.HW3:
                    break;
                case DataHolder.ProofingTypes.GrabBag:
                    break;
                case DataHolder.ProofingTypes.ScaleWriting:
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Particular Methods

        private void StyleButton(Button b, bool correct)
        {
            if (lastStyledButton != null)
            {
                lastStyledButton.Background = DefaultBrush;
            }

            b.Background = correct ? CheckBrush : XBrush;

            lastStyledButton = b;
        }

        #region Placing The Note
        private void TimerComplete(object sender)
        {
            if (ptc.CountingDown)
            {
                ptc.CountingDown = false;
                ptc.SetTimerInterval(DataHolder.SM.CurrentSession.Diff.GetHashCode());
            }
            if (curIndex > -1)
            {
                //you ran out of time on the last note
                RecordData(NoteIndex, false);
                ptc.PBValue++;
            }
            NextNote();
        }

        private void NoteClick(object sender)
        {
            if (SessionRunning)
            {
                ptc.StopTimer();

                Button b = sender as Button;
                if (b != null && !ptc.CountingDown)
                {
                    //give the note the button's value
                    string note = b.Content.ToString();
                    ;
                    int noteIndex = curSession.ProofingData[curIndex];
                    //use that value to check to see if it was the
                    //right one to press for the current note
                    bool correct = curSession.ScaleUsed.Notes[noteIndex] == note;

                    RecordData(noteIndex, correct);
                    StyleButton(b, correct);
                    NextNote();
                    ptc.PBValue++;
                }
            }
        }

        private void NextNote()
        {
            if (SessionRunning)
            {
                curIndex++;
                if (curIndex < curSession.ProofingData.Length)
                {
                    NoteNumber = curSession.ProofingData[curIndex] + 1;
                    ptc.TestingValue = NoteNumber.ToString();
                    ptc.StartTimer();
                }
                else
                {
                    ptc.StopTimer();
                    sbc.EnableViewResults(true);
                    SessionRunning = false;
                }
            }
        }
        #endregion

        #region HW3 Reaction
        private void IntervalClick(object sender)
        {
            Button sentinel = sender as Button;
            int interval = 1;
            if (sentinel != null && !hptc.CountingDown)
            {
                string pressedButton = sentinel.Content.ToString();
                switch (pressedButton)
                {
                    case IH:
                        interval = 1;
                        break;
                    case IW:
                        interval = 2;
                        break;
                    case I3:
                        interval = 3;
                        break;
                }
                //TODO: Fix curIndex here, now that it starts with -1
                int curData = curSession.ProofingData[curIndex - (hptc.PBValue > 3 ? 1 : 3)];
                bool correct = curData == interval;
                RecordData(curData, correct);
                GetNextHW3Note();
            }
        }
        private void HPTCTimer_Tick(object sender)
        {
            hptc.RemainingTime -= hptc.CountingDown ? 1000 : 100;
            if (hptc.RemainingTime == 0)
            {
                if (hptc.CountingDown)
                {
                    hptc.CountingDown = false;
                    hptc.SetTimerInterval(TimeSpan.Parse("00:00:0.100"));
                    GetNextHW3Interval();
                    GetNextHW3Interval();
                    GetNextHW3Interval();
                    hptc.ToggleViewIntervals();
                }
                if (curIndex > 0)
                {
                    //you ran out of time on the last note
                    RecordData(NoteIndex, false);
                }
                GetNextHW3Note();
            }
        }

        private void GetNextHW3Interval(bool Slide = false)
        {
            curIndex++;
            incrementor = (incrementor + curSession.ProofingData[curIndex]) % piano.Length;
            if (!Slide)
            {
                hptc.SetNextNote(piano[incrementor]);
            }
            else
            {
                hptc.NextInterval(piano[incrementor]);
            }
        }

        private void GetNextHW3Note()
        {
            if (curIndex < curSession.ProofingData.Length)
            {
                if (hptc.PBValue > 3)
                {
                    GetNextHW3Interval();
                }
                hptc.RemainingTime = curSession.Diff.GetHashCode();
                hptc.IncrementProgress();
                GetNextHW3Interval(true);
            }
            else
            {
                hptc.StopTimer();
                sbc.EnableViewResults(true);
                hptc.SetTimerText(true);
            }
        }
        #endregion
        #endregion


        //TODO: Research TimeSpan.Parse()

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        /// <summary>
        /// Handler for each time the Timer ticks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>



        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region Extra Button Handlers
        private void B_ViewResults_Click(object sender)
        {
            DataHolder.SM.CurrentSession = curSession;
            Frame.Navigate(typeof(SessionResultsPage));
        }

        private void B_Start_Click(object sender)
        {
            if (!SessionRunning)
            {
                SessionRunning = true;
                sbc.EnableStart(false);

                switch (DataHolder.ProofType)
                {
                    case DataHolder.ProofingTypes.PlacingTheNote:
                        if (DataHolder.SM.CurrentSession.Diff != ScaleResources.Difficulties.Zen)
                        {
                            ptc.StartTimer();
                        }
                        else
                        {
                            ptc.CountingDown = false;
                            ptc.TrackingTime = false;
                            NextNote();
                        }
                        break;
                    case DataHolder.ProofingTypes.HW3:
                        if (DataHolder.SM.CurrentSession.Diff != ScaleResources.Difficulties.Zen)
                        {
                            hptc.StartTimer();
                        }
                        else
                        {
                            hptc.CountingDown = false;
                            GetNextHW3Interval();
                            GetNextHW3Interval();
                            GetNextHW3Interval();
                            hptc.ToggleViewIntervals();
                            hptc.RemainingTime = curSession.Diff.GetHashCode();
                        }
                        break;
                }
            }
        }

        private void B_Quit_Click(object sender)
        {
            switch (DataHolder.ProofType)
            {
                case DataHolder.ProofingTypes.PlacingTheNote:
                    ptc.StopTimer();
                    break;
                case DataHolder.ProofingTypes.HW3:
                    if (hptc.TimerEnabled())
                        hptc.StopTimer();
                    break;
            }
            Frame.Navigate(typeof(MainPage));
        }


        #endregion
    }
}
