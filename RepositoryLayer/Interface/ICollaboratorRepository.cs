using CommonLayer;
using Microsoft.Extensions.Configuration;

namespace RepositoryLayer.Interface
{
    public interface ICollaboratorRepository
    {
        IConfiguration Configuration { get; }

        ResponseModel<CollaboratorModel> AddCollaborator(CollaboratorModel collab, int userId);
    }
}