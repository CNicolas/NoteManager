using NoteManager.NoteManagerServiceReference;
using System;
using System.Windows;
using System.Windows.Input;

namespace NoteManager.ViewModel
{
    /// <summary>
    /// The ViewModel for the notes page
    /// </summary>
    public class NoteViewModel : ViewModel
    {
        #region CONSTANTS
        /// <summary>
        /// Error message when the note name is not set
        /// </summary>
        private const string ERROR_NAME = "Please give a name to your Note";
        #endregion


        #region FIELDS
        /// <summary>
        /// The note's name
        /// </summary>
        private string _name;
        /// <summary>
        /// The note's message
        /// </summary>
        private string _message;
        /// <summary>
        /// The note
        /// </summary>
        private NoteDTO _note;
        /// <summary>
        /// Command to save the note
        /// </summary>
        private ICommand _saveNoteCommand;
        /// <summary>
        /// Command to go to the notes list
        /// </summary>
        private ICommand _goToNotesListCommand;
        #endregion


        /// <summary>
        /// Constructor
        /// </summary>
        public NoteViewModel()
        {
            _name = "";
            _message = "";
            Context = App.Context;

            if (Context.Note != null)
            {
                Console.WriteLine("Context Note: " + Context.Note.Name);
                _name = Context.Note.Name;
                _message = Context.Note.Message;
                _note = Context.Note;
            }

            CanExecute = true;
        }
        /// <summary>
        /// Constructor based on a <see cref="NoteDTO"/>
        /// </summary>
        public NoteViewModel(NoteDTO note)
        {
            _name = "";
            _message = "";
            _note = note;
            Context = App.Context;
            CanExecute = true;
        }


        #region PROPERTIES
        /// <summary>
        /// The note's name property
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    Notify("Name");
                }
            }
        }
        /// <summary>
        /// The note's message property
        /// </summary>
        public string Message
        {
            get { return _message; }
            set
            {
                if (_message != value)
                {
                    _message = value;
                    Notify("Message");
                }
            }
        }
        /// <summary>
        /// The note property
        /// </summary>
        public NoteDTO Note
        {
            get { return _note; }
            set { _note = value; }
        }
        /// <summary>
        /// Property to save the note
        /// </summary>
        public ICommand SaveNoteCommand
        {
            get
            {
                return _saveNoteCommand ?? (_saveNoteCommand = new DefaultCommandHandler(() => SaveNote(), CanExecute));
            }
        }
        /// <summary>
        /// Property to call the navigation method to the notes list
        /// </summary>
        public ICommand GoToNotesListCommand
        {
            get
            {
                return _goToNotesListCommand ?? (_goToNotesListCommand = new DefaultCommandHandler(() => GoToNotesList(), CanExecute));
            }
        }
        #endregion


        #region METHODS
        /// <summary>
        /// Save the edited note
        /// </summary>
        public void SaveNote()
        {
            if (Name != "")
            {
                if (Note != null)
                {
                    Note.Name    = Name;
                    Note.Message = Message;
                    Console.WriteLine("Ceci est un update ! avec NOte.NAME : ["+Note.Name+"]");
                    Context.Client.UpdateNote(Note, DateTime.Now);
                }
                else
                {
                    Console.WriteLine("Ceci est un add!");
                    NoteDTO n = Context.Client.SaveNote(Name, Message, Context.Utilisateur.Id, DateTime.Now, DateTime.Now);
                    Note = n;
                }
                NavigateTo(NOTES_LIST_PAGE);
            }
            else
            {
                MessageBox.Show(ERROR_NAME);
            }
        }
        /// <summary>
        /// Change to the list notes page
        /// </summary>
        public void GoToNotesList()
        {
            NavigateTo(NOTES_LIST_PAGE);
        }
        #endregion
    }
}
