using NoteManagerWCF.EDM;
using System;
using System.Runtime.Serialization;

namespace NoteManagerWCF.DTO
{
    /// <summary>
    /// The UserDTO object, containing the Note informations through http requests
    /// </summary>
    [DataContract]
    public class UserDTO
    {
        #region FIELDS
        /// <summary>
        /// The user's id
        /// </summary>
        private int _id;
        /// <summary>
        /// The user's login
        /// </summary>
        private string _login;
        /// <summary>
        /// The user's password
        /// </summary>
        private string _password;
        /*
        /// <summary>
        /// The user's firstname
        /// </summary>
        private string _prenom;
        /// <summary>
        /// The user's lastname
        /// </summary>
        private string _nom;
        */
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="user"></param>
        public UserDTO(User user)
        {
            _id = user.Id;
            _login = user.Login;
            _password = user.Password;
            //_nom = user.Nom;
            //_prenom = user.Prenom;
        }

        #region PROPERTIES
        /// <summary>
        /// Property Id
        /// </summary>
        [DataMember]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
         /// Property Login
         /// </summary>
        [DataMember]
        public string Login
        {
            get { return _login.Trim(); }
            set { _login = value.Trim(); }
        }
        /// <summary>
        /// Property Password
        /// </summary>
        [DataMember]
        public string Password
        {
            get { return _password.Trim(); }
            set { _password = value.Trim(); }
        }
        /*
        /// <summary>
        /// Property Lastname
        /// </summary>
        [DataMember]
        public string Nom
        {
            get { return _nom; }
            set { _nom = value.Trim(); }
        }
        /// <summary>
        /// Property Firstname
        /// </summary>
        [DataMember]
        public string Prenom
        {
            get { return _prenom; }
            set { _prenom = value.Trim(); }
        }
        */
        #endregion

        #region METHODS
        public override string ToString()
        {
            return "Login cc: " + Login + ", Password : " + Password;// + ", Prénom : " + Prenom + ", Nom : " + Nom;
        }
        #endregion
    }
}