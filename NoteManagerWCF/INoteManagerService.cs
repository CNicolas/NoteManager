using NoteManagerWCF.DTO;
using System;
using System.ServiceModel;

namespace NoteManagerWCF
{
    /// <summary>
    /// Server contract (interface)
    /// </summary>
    [ServiceContract]
    public interface INoteManagerService
    {
        /// <summary>
        /// Connect a user with his login and password
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns>The UserDTO</returns>
        [OperationContract]
        UserDTO ConnectUser(string login, string password);

        /// <summary>
        /// Save a note into the database
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        /// <param name="userid"></param>
        /// <param name="dateCreation"></param>
        /// <param name="dateModification"></param>
        /// <returns></returns>
        [OperationContract]
        NoteDTO SaveNote(string name, string message, int userid, DateTime dateCreation, DateTime dateModification);

        /// <summary>
        /// Update a note into the database
        /// </summary>
        /// <param name="note"></param>
        /// <param name="dateModification"></param>
        /// <returns></returns>
        [OperationContract]
        NoteDTO UpdateNote(NoteDTO note, DateTime dateModification);
        
        /// <summary>
        /// Delete a note from the database
        /// </summary>
        /// <param name="id"></param>
        [OperationContract]
        void DeleteNote(int id);

        /// <summary>
        /// List all users
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        UserDTO[] ListAllUsers();

        /// <summary>
        /// List all notes
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        NoteDTO[] ListAllNotes();

        /// <summary>
        /// List all notes of a user
        /// </summary>
        /// <param name="utilisateur"></param>
        /// <returns></returns>
        [OperationContract]
        NoteDTO[] ListAllNotesOfUser(int utilisateur);

        /// <summary>
        /// List all notes of a user
        /// </summary>
        /// <param name="recherche"></param>
        /// <returns></returns>
        [OperationContract]
        NoteDTO[] RechercheNote(string recherche, int utilisateur);
    }
}
