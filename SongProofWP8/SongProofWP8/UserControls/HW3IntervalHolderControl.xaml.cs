using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace SongProofWP8.UserControls
{
    public sealed partial class HW3IntervalHolderControl : UserControl, INotifyPropertyChanged
    {
        private const int MIN_NOTE_SIZE = 2;
        private const int COLUMN_WIDTH = 106;

        private List<TextBlock> NoteControls;

        public event PropertyChangedEventHandler PropertyChanged;

        public HW3IntervalHolderControl(string[] noteList)
        {
            this.InitializeComponent();
            DataContext = this;

            if (noteList.Length < MIN_NOTE_SIZE)
            {
                throw new Exception("Note size is lower than minimum");
            }
            NoteControls = new List<TextBlock>();

            int gridSize = (noteList.Length * 2) - 1;
            int noteIndex = 0;

            for (int i = 0; i < gridSize; i++)
            {
                TextBlock tb = new TextBlock()
                {
                    Text = i % 2 == 0 ? noteList[noteIndex] : "←",
                    Style = i % 2 == 0 ? TBStyle : TBArrowStyle
                };

                if (i % 2 == 0)
                {
                    noteIndex++;
                }
                NoteControls.Add(tb);
                
            }
            NoteStack.ItemsSource = NoteControls;
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
