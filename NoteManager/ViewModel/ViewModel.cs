using NoteManager.Pages;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace NoteManager.ViewModel
{
    /// <summary>
    /// The abstract ViewModel, which contains constants and methods for children
    /// </summary>
    public abstract class ViewModel : INotifyPropertyChanged
    {
        #region CONSTANTS
        /// <summary>
        /// The page of the notes list
        /// </summary>
        protected const string NOTES_LIST_PAGE = "NotesListPage";
        /// <summary>
        /// The page of a note
        /// </summary>
        protected const string NOTE_PAGE = "NotePage";
        /// <summary>
        /// The main frame of the window
        /// </summary>
        protected const string FRAME_NAME = "MainFrame";
        #endregion


        #region FIELDS
        /// <summary>
        /// The <see cref="ContextNoteManager"/> of the application
        /// </summary>
        protected ContextNoteManager Context;
        /// <summary>
        /// A bool to execute or not the commands
        /// </summary>
        protected bool CanExecute;
        /// <summary>
        /// See <see cref="INotifyPropertyChanged"/>
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion


        #region METHODS
        /// <summary>
        /// Notify binding changes
        /// </summary>
        /// <param name="propertyName"></param>
        public void Notify(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        /// <summary>
        /// Navigate to a page
        /// </summary>
        /// <param name="page"></param>
        public void NavigateTo(string page)
        {

            Page newPage = null;
            switch (page)
            {
                case NOTES_LIST_PAGE:
                    newPage = new NotesListPage();
                    break;
                case NOTE_PAGE:
                    newPage = new NotePage();
                    break;
                default:
                    Context.Window = new MainWindow();
                    return;
            }
            Context.Window.Content = newPage;
            //Frame f = Context.Window.FindName(FRAME_NAME) as Frame;
            //if (f == null)
            //{
            //    f.Source = newPage;
            //}
        }
        #endregion
    }
}
