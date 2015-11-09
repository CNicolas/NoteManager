using NoteManager.NoteManagerServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NoteManager
{
    public class ContextNoteManager
    {
        private Window _window;
        private NoteManagerServiceClient _client;
        private UserDTO _utilisateur;
        private NoteDTO _note;

        public ContextNoteManager()
        {
            _client = new NoteManagerServiceClient();
        }

        public NoteManagerServiceClient Client
        {
            get { return _client; }
            set { _client = value; }
        }
        public UserDTO Utilisateur
        {
            get { return _utilisateur; }
            set { _utilisateur = value; }
        }
        public Window Window
        {
            get { return _window; }
            set { _window = value; }
        }

        public NoteDTO Note
        {
            get { return _note; }
            set { _note = value; }
        }
    }
}