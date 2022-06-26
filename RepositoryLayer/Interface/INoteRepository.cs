using CommonLayer;
using Microsoft.Extensions.Configuration;

namespace RepositoryLayer.Interface
{
    public interface INoteRepository
    {
        IConfiguration Configuration { get; }

        ResponseModel<NotesModel> CreateNote(NotesModel noteData);
    }
}