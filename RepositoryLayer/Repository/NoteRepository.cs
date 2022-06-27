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
                    if(existNote.Trash == false)
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

                    result.Message = "Can't edit in Recycle Bin";
                    return result;
                }

                result.Message = "Note not available for the user";
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
                                                                     x.NoteId == noteId &&
                                                                     x.Trash == false).FirstOrDefault();
                if (existNote != null)
                {
                    if (existNote.Archive)
                    {
                        existNote.Archive = false;
                        result.Message = "Note Unarchived";
                    }
                    else
                    {
                        existNote.Archive = true;

                        if (existNote.Pin)
                        {
                            existNote.Pin = false;
                            result.Message = "Note archived and unpinned";
                        }
                        else
                            result.Message = "Note archived";
                    }

                    this._fundooContext.Update(existNote);
                    this._fundooContext.SaveChanges();

                    result.Status = true;
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

        public ResponseModel<NotesModel> TogglePin(int noteId, int userId)
        {
            try
            {
                var result = new ResponseModel<NotesModel>();
                var existNote = this._fundooContext.Notes.Where(x => x.UserId == userId &&
                                                                     x.NoteId == noteId &&
                                                                     x.Trash == false).FirstOrDefault();
                if (existNote != null)
                {
                    if (existNote.Pin)
                    {
                        existNote.Pin = false;
                        result.Message = "Note unpinned";
                    }
                    else
                    {
                        existNote.Pin = true;

                        if (existNote.Archive)
                        {
                            existNote.Archive = false;
                            result.Message = "Note pinned and unarchived";
                        }
                        else
                            result.Message = "Note pinned";
                    }                       

                    this._fundooContext.Update(existNote);
                    this._fundooContext.SaveChanges();

                    result.Status = true;
                    result.Data = existNote;
                    return result;
                }

                result.Message = "Unsuccessful to change Note Pinned Status";
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResponseModel<NotesModel> SetColor(int noteId, int userId, string noteColor)
        {
            try
            {
                var result = new ResponseModel<NotesModel>();
                var existNote = this._fundooContext.Notes.Where(x => x.UserId == userId &&
                                                                     x.NoteId == noteId &&
                                                                     x.Trash == false).FirstOrDefault();
                if (existNote != null)
                {
                    existNote.Color = noteColor;
                    this._fundooContext.Update(existNote);
                    this._fundooContext.SaveChanges();

                    result.Status = true;
                    result.Message = "Successfully changed Note Colour";
                    result.Data = existNote;

                    return result;
                }

                result.Message = "Unsuccessful to change Note Colour";
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
