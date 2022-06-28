using CommonLayer;
using Microsoft.Extensions.Configuration;

namespace BusinessLayer.Interface
{
    public interface ICollaboratorManager
    {
        IConfiguration Configuration { get; }

        ResponseModel<CollaboratorModel> AddCollaborator(CollaboratorModel collab, int userId);
    }
}