﻿#pragma checksum "..\..\TorrentWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "433107DA9712F7805C3536AD4A53511B"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using ClientApp;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace ClientApp {
    
    
    /// <summary>
    /// TorrentWindow
    /// </summary>
    public partial class TorrentWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 22 "..\..\TorrentWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SearchtextBox;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\TorrentWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SearchButton;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\TorrentWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView searchResultsview;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\TorrentWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\TorrentWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView UploadsView;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\TorrentWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView DownloadsView;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\TorrentWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label_Copy1;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\TorrentWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image image;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\TorrentWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button_Duck;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\TorrentWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button_Car;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\TorrentWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button signout_button;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ClientApp;component/torrentwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\TorrentWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 8 "..\..\TorrentWindow.xaml"
            ((ClientApp.TorrentWindow)(target)).Closed += new System.EventHandler(this.Window_Closed);
            
            #line default
            #line hidden
            return;
            case 2:
            this.SearchtextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.SearchButton = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\TorrentWindow.xaml"
            this.SearchButton.Click += new System.Windows.RoutedEventHandler(this.SearchButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.searchResultsview = ((System.Windows.Controls.ListView)(target));
            
            #line 24 "..\..\TorrentWindow.xaml"
            this.searchResultsview.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.searchResultsview_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 5:
            this.label = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.UploadsView = ((System.Windows.Controls.ListView)(target));
            return;
            case 7:
            this.DownloadsView = ((System.Windows.Controls.ListView)(target));
            return;
            case 8:
            this.label_Copy1 = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.image = ((System.Windows.Controls.Image)(target));
            return;
            case 10:
            this.button_Duck = ((System.Windows.Controls.Button)(target));
            
            #line 57 "..\..\TorrentWindow.xaml"
            this.button_Duck.Click += new System.Windows.RoutedEventHandler(this.button_Duck_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.button_Car = ((System.Windows.Controls.Button)(target));
            
            #line 58 "..\..\TorrentWindow.xaml"
            this.button_Car.Click += new System.Windows.RoutedEventHandler(this.button_Car_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.signout_button = ((System.Windows.Controls.Button)(target));
            
            #line 59 "..\..\TorrentWindow.xaml"
            this.signout_button.Click += new System.Windows.RoutedEventHandler(this.signout_button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

