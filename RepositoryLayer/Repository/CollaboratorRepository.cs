using CommonLayer;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using System;
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
                    var existUser = this._fundooContext.Users.Where(x => x.UserID == userId).FirstOrDefault();

                    var existCollab = this._fundooContext.Collaborators.Where(x => x.SharedEmail == collab.SharedEmail)
                                                                       .FirstOrDefault();

                    if (existCollab == null && collab.SharedEmail != existUser.Email)
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
    }
}
