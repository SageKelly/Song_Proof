using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public sealed partial class PianoKeyControl : UserControl
    {
        private MethodInfo _noteClickMethod;
        private object _methodTarget;
        public string[] NotesUsed { get; private set; }
        private List<Button> Notes;
        private const string DEFAULT_RES_NAME_PART = "Default";
        private const string CHECK_RES_NAME_PART = "Check";
        private const string X_RES_NAME_PART = "X";
        private const string SHARP_RES_NAME_PART = "Sharp";
        private const string KEY_RES_NAME_PART = "Key";
        private Button lastStyledButton;

        public PianoKeyControl(string[] sessionPiano, string[] usedNotes,
            string pianoKeysMethodName, object methodTarget, Type targetType)
        {
            this.InitializeComponent();
            DataContext = this;
            _noteClickMethod = targetType.GetTypeInfo().GetDeclaredMethod(pianoKeysMethodName);
            _methodTarget = methodTarget;
            B_Cheat.Tag = string.Join(", ", usedNotes);
            Notes = new List<Button>();
            NotesUsed = sessionPiano;
            //foreach (UIElement child in WhiteKeyGrid.Children.OfType<Button>())
            //{
            //    Notes.Add((Button)child);
            //}

            //foreach (UIElement child in SharpKeyGrid.Children.OfType<Button>())
            //{
            //    Notes.Add((Button)child);
            //}

            //if (Notes.Count == sessionPiano.Length)
            //{
            //    for (int i = 0; i < Notes.Count; i++)
            //    {
            //        Notes[i].Content = sessionPiano[i];
            //    }
            //}
            //else
            //{
            //    Debug.WriteLine("An error occurred between Note Count and Session Piano: {0}", Notes.Count);
            //    throw new Exception("An error occurred between Note Count and Session Piano");
            //}
        }

        public void InstallControl(Control control)
        {
            NoteGrid.Children.Add(control);
            Grid.SetRow(control, 2);
            Grid.SetColumn(control, 0);
        }

        private void _noteClick(object sender, RoutedEventArgs e)
        {
            _noteClickMethod.Invoke(_methodTarget, new object[1] { sender });
        }

        private void ToggleScaleView(object sender, RoutedEventArgs e)
        {
            string temp = (string)B_Cheat.Tag;
            B_Cheat.Tag = B_Cheat.Content;
            B_Cheat.Content = temp;
        }

        /// <summary>
        /// Styles the button to represent default, correct, or incorrect status
        /// </summary>
        /// <param name="b">the button to style</param>
        /// <param name="isDefault">Should the button be neutral</param>
        /// <param name="correct">If not neutral, should the button be correct / incorrect?</param>
        public void StyleButton(Button b, bool isDefault, bool correct = false)
        {
            //Get the raw note out of the key that comes in
            string note = b.Tag.ToString().Trim();
            string resName = string.Empty;

            //Set the key to default
            if (isDefault)
            {
                if (note.Contains(ScaleResources.FLAT) || note.Contains(ScaleResources.SHARP))
                {
                    resName = string.Format("{0} {1} {2}", DEFAULT_RES_NAME_PART, SHARP_RES_NAME_PART, KEY_RES_NAME_PART);
                }
                else
                {
                    resName = string.Format("{0} {1} {2}", DEFAULT_RES_NAME_PART, note, KEY_RES_NAME_PART);
                }
            }
            //else, set to either correct / incorrect status
            else if (correct)
            {
                if (note.Contains(ScaleResources.FLAT) || note.Contains(ScaleResources.SHARP))
                {
                    resName = string.Format("{0} {1} {2}", CHECK_RES_NAME_PART, SHARP_RES_NAME_PART, KEY_RES_NAME_PART);
                }
                else
                {
                    resName = string.Format("{0} {1} {2}", CHECK_RES_NAME_PART, note, KEY_RES_NAME_PART);
                }
            }
            else
            {
                if (note.Contains(ScaleResources.FLAT) || note.Contains(ScaleResources.SHARP))
                {
                    resName = string.Format("{0} {1} {2}", X_RES_NAME_PART, SHARP_RES_NAME_PART, KEY_RES_NAME_PART);
                }
                else
                {
                    resName = string.Format("{0} {1} {2}", X_RES_NAME_PART, note, KEY_RES_NAME_PART);
                }
            }

            //Make the resulting brush
            ImageBrush brush = new ImageBrush()
            {
                ImageSource = (ImageSource)Resources[resName],
                Stretch = Stretch.Uniform
            };
            b.Background = brush;
        }
    }
}
