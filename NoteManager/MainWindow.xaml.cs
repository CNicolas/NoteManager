using System.Windows;

namespace NoteManager
{
    /// <summary>
    /// The connection window of the NoteManager
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            App.Context.Window = this;
        }
    }
}
