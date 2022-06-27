using CommonLayer;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<ResponseModel<NotesModel>> EditNotes(NotesEditModel noteData)
        {
            try
            {
                var result = new ResponseModel<NotesModel>();
                var existNote = this._fundooContext.Notes.Where(x => x.UserId == noteData.UserId && 
                                                                     x.NoteId == noteData.NoteId).FirstOrDefault();
                result.Data = existNote;

                if (existNote != null)
                {
                    if (noteData.Body != null)
                        existNote.Body = noteData.Body;
                    if (noteData.Title != null)
                        existNote.Title = noteData.Title;

                    this._fundooContext.Update(existNote);
                    await this._fundooContext.SaveChangesAsync();

                    result.Status = true;
                    result.Message = "Note Successfully Edited";

                    return result;
                }

                result.Message = "Unsuccessful to edit Note";
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResponseModel<NotesModel> ToggleArchive(int noteId, int userId)
        {
            try
            {
                var result = new ResponseModel<NotesModel>();
                var existNote = this._fundooContext.Notes.Where(x => x.UserId == userId &&
                                                                     x.NoteId == noteId).FirstOrDefault();
                if (existNote != null)
                {
                    if (existNote.Archive)
                        existNote.Archive = false;
                    else
                    {
                        if (existNote.Pin)
                            existNote.Pin = false;
                        existNote.Archive = true;
                    }
                    this._fundooContext.Update(existNote);
                    this._fundooContext.SaveChanges();

                    result.Status = true;
                    result.Message = "Successfully changed Note Archive Status";
                    result.Data = existNote;

                    return result;
                }

                result.Message = "Unsuccessful to change Note Archive Status";
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
