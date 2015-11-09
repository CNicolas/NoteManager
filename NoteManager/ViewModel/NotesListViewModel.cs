using NoteManager.NoteManagerServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NoteManager.ViewModel
{
    /// <summary>
    /// The ViewModel for the notes list page
    /// </summary>
    public class NotesListViewModel : ViewModel
    {
        #region FIELDS
        /// <summary>
        /// Notes list of the user
        /// </summary>
        private List<NoteDTO> _notesList;
        private NoteDTO _selectedNote;

        private string _recherche;
        /// <summary>
        /// Command to go to the page where to create a note
        /// </summary>
        private ICommand _newNoteCommand;
        /// <summary>
        /// Command to delete a note
        /// </summary>
        private ICommand _deleteNoteCommand;
        /// <summary>
        /// Command to quit the application
        /// </summary>
        private ICommand _quitCommand;

        private ICommand _rechercheCommand;
        #endregion


        /// <summary>
        /// Constructor
        /// </summary>
        public NotesListViewModel()
        {
            _notesList = new List<NoteDTO>();
            Context = App.Context;
            Context.Note = null;
            GetNotesList(Context.Utilisateur.Id);
            CanExecute = true;
        }


        #region PROPERTIES
        /// <summary>
        /// Property to get/set the list of notes
        /// </summary>
        public List<NoteDTO> NotesList
        {
            get
            {
                return _notesList;
            }
            set
            {
                if (_notesList != value)
                {
                    _notesList = value;
                    Notify("NotesList");
                }
            }
        }
        public NoteDTO SelectedNote
        {
            get
            {
                return _selectedNote;
            }
            set
            {
                _selectedNote = value;
                Context.Note = value;
                Console.WriteLine("Le Selected : " + Context.Note.Name);
                Notify("SelectedNote");
                NewNote();
            }
        }

        public string Recherche
        {
            get
            {
                return _recherche;
            }
            set
            {
                _recherche = value;
            }
        }
        /// <summary>
        /// Property for calling the <see cref="_newNoteCommand"/>
        /// </summary>
        public ICommand NewNoteCommand
        {
            get
            {
                return _newNoteCommand ?? (_newNoteCommand = new DefaultCommandHandler(() => NewNote(), CanExecute));
            }
        }
        /// <summary>
        /// Property for calling the <see cref="_deleteNoteCommand"/>
        /// </summary>
        public ICommand DeleteNoteCommand
        {
            get
            {
                return _deleteNoteCommand ?? (_deleteNoteCommand = new DefaultTypedCommandHandler<NoteDTO>((note) => DeleteNote(note), CanExecute));
            }
        }

        /// <summary>
        /// Property for calling the <see cref="_quitCommand"/>
        /// </summary>
        public ICommand QuitCommand
        {
            get
            {
                return _quitCommand ?? (_quitCommand = new DefaultCommandHandler(() => Quit(), CanExecute));
            }
        }

        public ICommand RechercheCommand
        {
            get
            {
                return _rechercheCommand ?? (_rechercheCommand = new DefaultCommandHandler(() => RechercheNote(), CanExecute));
            }
        }
        #endregion


        #region METHODS
        /// <summary>
        /// Server call fetching notes
        /// </summary>
        /// <param name="user"></param>
        /// <returns>The list of notes</returns>
        private List<NoteDTO> GetNotesList(int user)
        {
            NoteDTO[] tmp = Context.Client.ListAllNotesOfUser(user);
            NotesList = new List<NoteDTO>(tmp);
            return NotesList;
        }
        /// <summary>
        /// Go to the page for creating a note
        /// </summary>
        private void NewNote()
        {
            NavigateTo(NOTE_PAGE);
        }
        /// <summary>
        /// Server call to delete a note
        /// </summary>
        /// <param name="obj"></param>
        private void DeleteNote(object obj)
        {
            NoteDTO note = obj as NoteDTO;
            if (note != null)
            {
                MessageBox.Show("Suppression de la note " + note.Name);
                Context.Client.DeleteNote(note.Id);
                NotesList = (from n in NotesList where n.Name != note.Name select n).ToList();
            }
        }

        private void RechercheNote()
        {
            Console.WriteLine("[Recherche] => " + Recherche);
            if (Recherche != "")
            {
                NoteDTO[] tmp = Context.Client.RechercheNote(Recherche, Context.Utilisateur.Id);
                NotesList = new List<NoteDTO>(tmp);
            }
            else
                GetNotesList(Context.Utilisateur.Id);

        }

        /// <summary>
        /// Quit the application
        /// </summary>
        private void Quit()
        {
            Application.Current.Shutdown();
        }
        #endregion
    }
}
