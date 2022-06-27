using CommonLayer;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface INoteRepository
    {
        IConfiguration Configuration { get; }

        ResponseModel<NotesModel> CreateNote(NotesModel noteData);

        Task<ResponseModel<NotesModel>> EditNotes(NotesEditModel noteData);

        ResponseModel<NotesModel> ToggleArchive(int noteId, int userId);

        ResponseModel<NotesModel> TogglePin(int noteId, int userId);

        ResponseModel<NotesModel> SetColor(int noteId, int userId, string noteColor);

        ResponseModel<List<NotesModel>> GetNotes(int userId);
    }
}