﻿#pragma checksum "..\..\Agenda.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "8FE040B70177DA3C8B51464993D642B4B60B8E984B041FF475462DC65B239387"
//------------------------------------------------------------------------------
// <auto-generated>
//     Il codice è stato generato da uno strumento.
//     Versione runtime:4.0.30319.42000
//
//     Le modifiche apportate a questo file possono provocare un comportamento non corretto e andranno perse se
//     il codice viene rigenerato.
// </auto-generated>
//------------------------------------------------------------------------------

using Chat;
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


namespace Chat {
    
    
    /// <summary>
    /// Agenda
    /// </summary>
    public partial class Agenda : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\Agenda.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox contatti_lst;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\Agenda.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Seleziona_btn;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\Agenda.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox nome_txt;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\Agenda.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox indirizzo_txt;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\Agenda.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox porta_txt;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\Agenda.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button add_contatto_btn;
        
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
            System.Uri resourceLocater = new System.Uri("/Chat;component/agenda.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Agenda.xaml"
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
            this.contatti_lst = ((System.Windows.Controls.ListBox)(target));
            return;
            case 2:
            this.Seleziona_btn = ((System.Windows.Controls.Button)(target));
            
            #line 11 "..\..\Agenda.xaml"
            this.Seleziona_btn.Click += new System.Windows.RoutedEventHandler(this.Seleziona_btn_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.nome_txt = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.indirizzo_txt = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.porta_txt = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.add_contatto_btn = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\Agenda.xaml"
            this.add_contatto_btn.Click += new System.Windows.RoutedEventHandler(this.add_contatto_btn_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

