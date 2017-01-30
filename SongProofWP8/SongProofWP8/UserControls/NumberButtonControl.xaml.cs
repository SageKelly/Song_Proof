using System;
using System.Collections.Generic;
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
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace SongProofWP8.UserControls
{
    public sealed partial class NumberButtonControl : UserControl
    {
        private MethodInfo _noteClickMethod;
        private object _methodTarget;
        public string[] NotesUsed { get; private set; }
        private List<Button> Notes;

        public NumberButtonControl(string[] sessionPiano, string[] usedNotes,
            string pianoKeysMethodName, object methodTarget, Type targetType)
        {
            this.InitializeComponent();
            DataContext = this;
            _noteClickMethod = targetType.GetTypeInfo().GetDeclaredMethod(pianoKeysMethodName);
            _methodTarget = methodTarget;
            LBScale.ItemsSource = usedNotes;
            Notes = new List<Button>();

            foreach (UIElement child in NoteGrid.Children.OfType<Button>())
            {
                Notes.Add((Button)child);
            }

            if (Notes.Count == sessionPiano.Length)
            {
                for (int i = 0; i < Notes.Count; i++)
                {
                    Notes[i].Content = sessionPiano[i];
                }
            }
            else
            {
                throw new Exception("An error occurred between Note Count and Session Piano");
            }
        }

        public void InstallInCenter(Control control)
        {
            NoteGrid.Children.Add(control);
            Grid.SetRow(control, 1);
            Grid.SetRowSpan(control, 2);
            Grid.SetColumn(control, 1);
            Grid.SetColumnSpan(control, 2);
        }

        private void _noteClick(object sender, RoutedEventArgs e)
        {
            _noteClickMethod.Invoke(_methodTarget, new object[1] { sender });
        }

        private void ToggleScaleView(object sender, RoutedEventArgs e)
        {
            if ((bool)B_Cheat.IsChecked)
            {
                B_Cheat.Content = "Be Honest?";
                FadeIn.Begin();
            }
            else
            {
                B_Cheat.Content = "Cheat?";
                FadeOut.Begin();
            }
        }
    }
}
