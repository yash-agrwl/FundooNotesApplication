using CommonLayer;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface INoteRepository
    {
        IConfiguration Configuration { get; }

        ResponseModel<NotesModel> CreateNote(NotesModel noteData);

        Task<ResponseModel<NotesModel>> EditNotes(NotesEditModel noteData);
    }
}