﻿

#pragma checksum "C:\GitHub\Song_Proof\SongProofWP8\SongProofWP8\UserControls\PianoKeyControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D5978B4FE99EFECB98CDBA28EBAF4158"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SongProofWP8.UserControls
{
    partial class PianoKeyControl : global::Windows.UI.Xaml.Controls.UserControl
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Media.Animation.Storyboard FadeIn; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Media.Animation.Storyboard FadeOut; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Style NoteStyle; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.Grid NoteGrid; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.ItemsControl LBScale; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private global::Windows.UI.Xaml.Controls.Primitives.ToggleButton B_Cheat; 
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        private bool _contentLoaded;

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent()
        {
            if (_contentLoaded)
                return;

            _contentLoaded = true;
            global::Windows.UI.Xaml.Application.LoadComponent(this, new global::System.Uri("ms-appx:///UserControls/PianoKeyControl.xaml"), global::Windows.UI.Xaml.Controls.Primitives.ComponentResourceLocation.Application);
 
            FadeIn = (global::Windows.UI.Xaml.Media.Animation.Storyboard)this.FindName("FadeIn");
            FadeOut = (global::Windows.UI.Xaml.Media.Animation.Storyboard)this.FindName("FadeOut");
            NoteStyle = (global::Windows.UI.Xaml.Style)this.FindName("NoteStyle");
            NoteGrid = (global::Windows.UI.Xaml.Controls.Grid)this.FindName("NoteGrid");
            LBScale = (global::Windows.UI.Xaml.Controls.ItemsControl)this.FindName("LBScale");
            B_Cheat = (global::Windows.UI.Xaml.Controls.Primitives.ToggleButton)this.FindName("B_Cheat");
        }
    }
}


