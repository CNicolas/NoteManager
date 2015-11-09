using NoteManagerWCF.DTO;
using NoteManagerWCF.EDM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace NoteManagerWCF
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez Service1.svc ou Service1.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class NoteManagerService : INoteManagerService
    {
        #region SERVER PUBLIC METHODS
        /// <summary>
        /// <see cref="INoteManagerService.ConnectUser(string, string)"/>
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public UserDTO ConnectUser(string login, string password)
        {
            if (login == null || password == null)
            {
                throw new ArgumentNullException("login or password");
            }
            using (var nme = new NoteManagerEntities())
            {
                var user = from u in nme.User where u.Login == login select u;
                if (user.Count() == 1)
                {
                    var u = user.First();
                    UserDTO res = new UserDTO(u);
                    // Si le password est ok, on renvoie le UserDTO, sinon null;
                    return password == res.Password ? res : null;
                }
                else
                {
                    var users = nme.User;
                    var id = MaxIdUsers(users) + 1;
                    User res = new User();
                    res.Id = id;
                    res.Login = login;
                    res.Password = password;
                    nme.User.Add(res);
                    nme.SaveChanges();
                    return new UserDTO(res);
                }
            }
        }

        /// <summary>
        /// <see cref="INoteManagerService.SaveNote(string, string, int, DateTime, DateTime)"/>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        /// <param name="utilisateur"></param>
        /// <param name="dateCreation"></param>
        /// <param name="dateModification"></param>
        /// <returns></returns>
        public NoteDTO SaveNote(string name, string message, int utilisateur, DateTime dateCreation, DateTime dateModification)
        {
            if (name == null || message == null || dateCreation == null || dateModification == null)
            {
                throw new ArgumentNullException("CreateNote : title or message or dates failed");
            }
            using (var nme = new NoteManagerEntities())
            {
                var existingNote = from n in nme.Note where n.Name == name && n.UserId == utilisateur select n;
                Note res;
                if (existingNote.Count() > 0)
                {
                    res = existingNote.First();
                    res.Message = message;
                    res.DateModification = dateModification;
                }
                else
                {
                    var id = MaxIdNotes(nme.Note) + 1;
                    res = new Note();
                    res.Id = id;
                    res.Name = name;
                    res.Message = message;
                    res.DateCreation = dateCreation;
                    res.DateModification = dateModification;
                    res.UserId = utilisateur;
                    nme.Note.Add(res);
                }
                //res.User = user.First();
                nme.SaveChanges();

                return new NoteDTO(res);
            }
        }

        /// <summary>
        /// <see cref="INoteManagerService.UpdateNote(NoteDTO, DateTime)"/>
        /// </summary>
        /// <param name="note"></param>
        /// <param name="dateModification"></param>
        /// <returns></returns>
        public NoteDTO UpdateNote(NoteDTO note, DateTime dateModification)
        {
            Logger logger = new Logger(this.GetType());
            logger.Info("[FONTION UPDATE]");

            if (note == null || dateModification == null)
            {
                throw new ArgumentNullException("UpdateNote : note or dates failed");
            }
            using (var nme = new NoteManagerEntities())
            {
                Note res = nme.Note.Find(note.Id);
                res.Name = note.Name;
                res.Message = note.Message;
                res.DateModification = dateModification;
                nme.SaveChanges();
                logger.Info("[FONTION UPDATE] : note.NAME => ["+note.Name+"]");

                return new NoteDTO(res);
            }
        }

        /// <summary>
        /// <see cref="INoteManagerService.DeleteNote(int)"/>
        /// </summary>
        /// <param name="id"></param>
        public void DeleteNote(int id)
        {
            using (var nme = new NoteManagerEntities())
            {
                var noteToDelete = (from n in nme.Note where n.Id == id select n).FirstOrDefault();
                if (noteToDelete != null)
                {
                    nme.Note.Remove(noteToDelete);
                    nme.SaveChanges();
                }
            }
        }

        /// <summary>
        /// <see cref="INoteManagerService.ListAllUsers"/>
        /// </summary>
        /// <param name="id"></param>
        public UserDTO[] ListAllUsers()
        {
            Logger logger = new Logger(this.GetType());
            logger.Debug("debug");
            logger.Error("error");
            logger.Info("info");
            logger.Warn("warn");
            using (var nme = new NoteManagerEntities())
            {
                var allUsers = from u in nme.User select u;
                List<UserDTO> res = new List<UserDTO>();
                foreach (var u in allUsers)
                {
                    res.Add(new UserDTO(u));
                }
                return res.ToArray();
            }
        }

        /// <summary>
        /// <see cref="INoteManagerService.ListAllNotes"/>
        /// </summary>
        /// <param name="id"></param>
        public NoteDTO[] ListAllNotes()
        {
            using (var nme = new NoteManagerEntities())
            {
                var allNotes = from n in nme.Note select n;
                List<NoteDTO> res = new List<NoteDTO>();
                foreach (var n in allNotes)
                {
                    res.Add(new NoteDTO(n));
                }
                return res.ToArray();
            }
        }

        /// <summary>
        /// <see cref="INoteManagerService.ListAllNotesOfUser(int)"/>
        /// </summary>
        /// <param name="id"></param>
        public NoteDTO[] ListAllNotesOfUser(int utilisateur)
        {
            using (var nme = new NoteManagerEntities())
            {
                var allNotes = from n in nme.Note where n.UserId == utilisateur select n;
                List<NoteDTO> res = new List<NoteDTO>();
                foreach (var n in allNotes)
                {
                    res.Add(new NoteDTO(n));
                }
                return res.ToArray();
            }
        }

        /// <summary>
        /// <see cref="INoteManagerService.RechercheNote(string,int)"/>
        /// </summary>
        /// <param name="recherche"></param>
        /// <param name="id"></param>
        public NoteDTO[] RechercheNote(string recherche, int utilisateur)
        {
            Logger logger = new Logger(this.GetType());
            using (var nme = new NoteManagerEntities())
            {
                var allNotes = from n in nme.Note
                               where (n.Name.Contains(recherche) || n.Message.Contains(recherche)) && n.UserId == utilisateur
                               orderby n.DateModification descending
                               select n;
                List<NoteDTO> res = new List<NoteDTO>();
                foreach (var n in allNotes)
                {
                    res.Add(new NoteDTO(n));
                }
                logger.Info("[FONTION RECHERCHE] [MOT]=>["+recherche+"] [Nb de resultat]=>["+res.Count()+"]");

                return res.ToArray();
            }
        }
        #endregion


        #region PRIVATE METHODS
        /// <summary>
        /// Get the greatest note ID in db
        /// </summary>
        /// <param name="notes"></param>
        /// <returns></returns>
        private int MaxIdNotes(DbSet<Note> notes)
        {
            var list = from n in notes select n;

            // On prend le dernier ID
            int id = 0;
            foreach (var no in list)
            {
                id = no.Id > id ? no.Id : id;
            }
            return id;
        }
        /// <summary>
        /// Get the greatest user ID in db
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        private int MaxIdUsers(DbSet<User> users)
        {
            var list = from n in users select n;

            // On prend le dernier ID
            int id = 0;
            foreach (var us in list)
            {
                id = us.Id > id ? us.Id : id;
            }
            return id;
        }
        #endregion
    }
}
