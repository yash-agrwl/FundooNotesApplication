using CommonLayer;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using System;
using System.Linq;

namespace RepositoryLayer.Repository
{
    public class NoteRepository : INoteRepository
    {
        private readonly FundooContext _fundooContext;

        public IConfiguration Configuration { get; }

        public NoteRepository(IConfiguration configuration, FundooContext fundooContext)
        {
            Configuration = configuration;
            this._fundooContext = fundooContext;
        }

        public ResponseModel<NotesModel> CreateNote(NotesModel noteData)
        {
            try
            {
                var result = new ResponseModel<NotesModel>();
                var validUser = this._fundooContext.Users.Where(x => x.UserID == noteData.UserId).FirstOrDefault();
                if (validUser != null)
                {
                    if (noteData.Body != null || noteData.Title != null)
                    {
                        // Add the data to the data base using fundooContext.
                        this._fundooContext.Add(noteData);
                        // Save the change in data base.
                        this._fundooContext.SaveChanges();

                        result.Status = true;
                        result.Message = "Note created Successfully";
                        result.Data = noteData;

                        return result;
                    }

                    result.Message = "Note cannot be Empty";
                    return result;
                }

                result.Message = "Unsuccessful to create Note";
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
