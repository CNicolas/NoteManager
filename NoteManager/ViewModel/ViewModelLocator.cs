namespace NoteManager.ViewModel
{
    /// <summary>
    /// MVVM Locator for ViewModels
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Get a ConnectionViewModel
        /// </summary>
        public ConnectionViewModel Connection
        {
            get
            {
                return new ConnectionViewModel();
            }
        }
        /// <summary>
        /// Get a NotesListViewModel
        /// </summary>
        public NotesListViewModel NotesList
        {
            get
            {
                return new NotesListViewModel();
            }
        }
        /// <summary>
        /// Get a NoteViewModel
        /// </summary>
        public NoteViewModel Note
        {
            get
            {
                return new NoteViewModel();
            }
        }
    }
}
