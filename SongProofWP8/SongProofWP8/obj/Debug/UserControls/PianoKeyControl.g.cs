﻿

#pragma checksum "C:\PersonalProjects\PersonalProjects\SongProofWP8\SongProofWP8\UserControls\PianoKeyControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "491AA542C02A0AB867091610EFB9477A"
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
    partial class PianoKeyControl : global::Windows.UI.Xaml.Controls.UserControl, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 62 "..\..\UserControls\PianoKeyControl.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this._noteClick;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 50 "..\..\UserControls\PianoKeyControl.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.ToggleScaleView;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}

