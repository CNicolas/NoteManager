using NoteManagerWCF.EDM;
using System;
using System.Runtime.Serialization;

namespace NoteManagerWCF.DTO
{
    /// <summary>
    /// The NoteDTO object, containing the Note informations through http requests
    /// </summary>
    [DataContract]
    public class NoteDTO
    {
        #region FIELDS
        /// <summary>
        /// The note's id
        /// </summary>
        private int _id;
        /// <summary>
        /// The note's name
        /// </summary>
        private string _name;
        /// <summary>
        /// The note's message
        /// </summary>
        private string _message;
        /// <summary>
        /// The note's user id
        /// </summary>
        private int _utilisateur;
        /// <summary>
        /// The note's creation date
        /// </summary>
        private DateTime _dateCreation;
        /// <summary>
        /// The note's modification date
        /// </summary>
        private DateTime _dateModification;

        /// <summary>
        /// The note's creation date as a string
        /// </summary>
        private string _creation;
        /// <summary>
        /// The note's modification date as a string
        /// </summary>
        private string _modification;

        /// <summary>
        /// The note's creation day as a string
        /// </summary>
        private string _jourCreation;
        /// <summary>
        /// The note's modification day as a string
        /// </summary>
        private string _jourModification;
        /// <summary>
        /// The note's creation hour as a string
        /// </summary>
        private string _horaireCreation;
        /// <summary>
        /// The note's modification hour as a string
        /// </summary>
        private string _horaireModification;
        #endregion


        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        /// <param name="note">The Model object</param>
        public NoteDTO(Note note)
        {
            _id = note.Id;
            _name = note.Name;
            _message = note.Message;
            _utilisateur = note.UserId;
            _dateCreation = note.DateCreation;
            _dateModification = note.DateModification;
            initStringsDate();
        }
        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        /// <param name="name">Le titre</param>
        /// <param name="message">Le message</param>
        /// <param name="utilisateur">L'id de l'utilisateur</param>
        /// <param name="creation">La date de création</param>
        /// <param name="modification">La date de modification</param>
        public NoteDTO(string name, string message, int utilisateur, DateTime creation, DateTime modification)
        {
            _name = name;
            _message = message;
            _utilisateur = utilisateur;
            _dateCreation = creation;
            _dateModification = modification;
            initStringsDate();
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
        /// Property Name
        /// </summary>
        [DataMember]
        public string Name
        {
            get { return _name.Trim(); }
            set { _name = value; }
        }
        /// <summary>
        ///  Property Message
        /// </summary>
        [DataMember]
        public string Message
        {
            get { return _message.Trim(); }
            set { _message = value; }
        }
        /// <summary>
        /// Property user id
        /// </summary>
        [DataMember]
        public int Utilisateur
        {
            get { return _utilisateur; }
            set { _utilisateur = value; }
        }
        /// <summary>
        /// Property creation date
        /// </summary>
        [DataMember]
        public DateTime DateCreation
        {
            get { return _dateCreation; }
            set { _dateCreation = value; }
        }
        /// <summary>
        /// Property Modification date
        /// </summary>
        [DataMember]
        public DateTime DateModification
        {
            get { return _dateModification; }
            set { _dateModification = value; }
        }

        /// <summary>
        /// Property Creation string
        /// </summary>
        [DataMember]
        public string Creation
        {
            get { return _creation.Trim(); }
            set { _creation = value; }
        }
        /// <summary>
        /// Property Modification string
        /// </summary>
        [DataMember]
        public string Modification
        {
            get { return _modification.Trim(); }
            set { _modification = value; }
        }

        /// <summary>
        /// Property creation day string
        /// </summary>
        [DataMember]
        public string JourCreation
        {
            get { return _jourCreation; }
            set { _jourCreation = value; }
        }
        /// <summary>
        /// Property modification day string
        /// </summary>
        [DataMember]
        public string JourModification
        {
            get { return _jourModification; }
            set { _jourModification = value; }
        }
        /// <summary>
        /// Property creation hour string
        /// </summary>
        [DataMember]
        public string HoraireCreation
        {
            get { return _horaireCreation; }
            set { _horaireCreation = value; }
        }
        /// <summary>
        /// Property modification hour string
        /// </summary>
        [DataMember]
        public string HoraireModification
        {
            get { return _horaireModification; }
            set { _horaireModification = value; }
        }
        #endregion

        /// <summary>
        /// Init strings
        /// </summary>
        private void initStringsDate()
        {
            _jourCreation = DateCreation.Day.ToString("00") + "/" + DateCreation.Month.ToString("00") + "/" + DateCreation.Year.ToString("00");
            _jourModification = DateModification.Day.ToString("00") + "/" + DateModification.Month.ToString("00") + "/" + DateModification.Year.ToString("00");
            _horaireCreation = "" + DateCreation.Hour.ToString("00") + ":" + DateCreation.Minute.ToString("00");// + ":" + DateCreation.Second.ToString("00");
            _horaireModification = "" + DateModification.Hour.ToString("00") + ":" + DateModification.Minute.ToString("00");// + ":" + DateModification.Second.ToString("00");

            _creation = _jourCreation + " à " + _horaireCreation;
            _modification = _jourModification + " à " + _horaireModification;
        }
 
    }
}
