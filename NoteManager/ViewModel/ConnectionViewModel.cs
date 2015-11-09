using System.Windows;
using System.Windows.Input;

namespace NoteManager.ViewModel
{
    /// <summary>
    /// The ViewModel for the connection page
    /// </summary>
    public class ConnectionViewModel : ViewModel
    {
        #region CONSTANTS
        /// <summary>
        /// The error message to show when password is not correct
        /// </summary>
        private const string ERROR_PASSWORD = "Le mot de passe est erroné !";
        #endregion


        #region FIELDS
        /// <summary>
        /// The login
        /// </summary>
        private string _login;
        /// <summary>
        /// The password
        /// </summary>
        private string _password;
        /// <summary>
        /// The connection command
        /// </summary>
        private ICommand _connectionCommand;
        #endregion


        /// <summary>
        /// Constructor
        /// </summary>
        public ConnectionViewModel()
        {
            _login = "";
            _password = "";
            Context = App.Context;
            CanExecute = true;
        }


        #region PROPERTIES
        /// <summary>
        /// Get the Frame name
        /// </summary>
        public string FrameName
        {
            get { return FRAME_NAME; }
        }
        /// <summary>
        /// Property to get/set the login
        /// </summary>
        public string Login
        {
            get { return _login; }
            set
            {
                if (_login != value)
                {
                    _login = value;
                    Notify("Login");
                }
            }
        }
        /// <summary>
        /// Property for the password
        /// </summary>
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    Notify("Password");
                }
            }
        }
        /// <summary>
        /// Property to call the connection command
        /// </summary>
        public ICommand ConnectionCommand
        {
            get
            {
                return _connectionCommand ?? (_connectionCommand = new DefaultCommandHandler(() => Connection(), CanExecute));
            }
        }
        #endregion


        #region METHODS
        /// <summary>
        /// Server call to connect the user
        /// </summary>
        public void Connection()
        {
            Context.Utilisateur = Context.Client.ConnectUser(Login, Password);
            if (Context.Utilisateur == null)
            {
                MessageBox.Show(ERROR_PASSWORD);
            }
            else
            {
                NavigateTo(NOTES_LIST_PAGE);
            }
        }
        #endregion
    }
}
