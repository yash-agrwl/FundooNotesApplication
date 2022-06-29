using CommonLayer;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryLayer.Repository
{
    public class CollaboratorRepository : ICollaboratorRepository
    {
        private readonly FundooContext _fundooContext;

        public IConfiguration Configuration { get; }

        public CollaboratorRepository(IConfiguration configuration, FundooContext fundooContext)
        {
            this.Configuration = configuration;
            this._fundooContext = fundooContext;
        }

        public ResponseModel<CollaboratorModel> AddCollaborator(CollaboratorModel collab, int userId)
        {
            try
            {
                var result = new ResponseModel<CollaboratorModel>();
                var existNote = this._fundooContext.Notes.Where(x => x.NoteId == collab.NoteId &&
                                                                     x.UserId == userId).SingleOrDefault();
                if (existNote != null)
                {
                    var loggedUser = this._fundooContext.Users.Where(x => x.UserID == userId).FirstOrDefault();

                    var existCollab = this._fundooContext.Collaborators.Where(x => x.SharedEmail == collab.SharedEmail)
                                                                       .FirstOrDefault();

                    if (existCollab == null && collab.SharedEmail != loggedUser.Email)
                    {
                        this._fundooContext.Collaborators.Add(collab);
                        this._fundooContext.SaveChanges();

                        result.Status = true;
                        result.Message = "Collaborator added Successfully";
                        result.Data = collab;
                        return result;
                    }

                    result.Message = "This email already exist";
                    return result;
                }

                result.Message = "Note not available";
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResponseModel<List<string>> GetCollaborator(int noteId, int userId)
        {
            try
            {
                var result = new ResponseModel<List<string>>();
                var existNote = this._fundooContext.Notes.Where(x => x.NoteId == noteId &&
                                                                     x.UserId == userId).SingleOrDefault();
                if (existNote != null)
                {
                    var collabMails = (from collab in this._fundooContext.Collaborators
                                       where collab.NoteId == noteId
                                       select collab.SharedEmail).ToList();

                    if (collabMails.Count > 0)
                    {
                        result.Status = true;
                        result.Message = $"{collabMails.Count} Collaborator retrieved Successfully";
                        result.Data = collabMails;
                        return result;
                    }

                    result.Message = "No collaborator available for this note";
                    return result;
                }

                result.Message = "Note not available";
                return result;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResponseModel<CollaboratorModel> DeleteCollaborator(int noteId, int userId, string collabMail)
        {
            try
            {
                var result = new ResponseModel<CollaboratorModel>();
                var existNote = this._fundooContext.Notes.Where(x => x.NoteId == noteId &&
                                                                     x.UserId == userId).SingleOrDefault();
                if (existNote != null)
                {
                    var collab = this._fundooContext.Collaborators.Where(x => x.NoteId == noteId &&
                                                                         x.SharedEmail == collabMail).SingleOrDefault();
                    if (collab != null)
                    {
                        this._fundooContext.Collaborators.Remove(collab);
                        this._fundooContext.SaveChanges();

                        result.Status = true;
                        result.Message = "Collaborator deleted Successfully";
                        result.Data = collab;
                        return result;
                    }

                    result.Message = "No collaborator found with this email";
                    return result;
                }

                result.Message = "Note not available";
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
