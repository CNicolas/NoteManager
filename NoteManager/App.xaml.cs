using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace NoteManager
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static ContextNoteManager _context;
        private static MainWindow _mainWindow;

        public App()
        {
            InitializeComponent();
            _context = new ContextNoteManager();
        }

        public static ContextNoteManager Context
        {
            get { return _context; }
            set { _context = value; }
        }

        public static MainWindow Window
        {
            get { return _mainWindow; }
            set { _mainWindow = value; }
        }
    }
}
