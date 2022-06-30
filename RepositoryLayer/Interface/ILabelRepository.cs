using CommonLayer;
using Microsoft.Extensions.Configuration;

namespace RepositoryLayer.Interface
{
    public interface ILabelRepository
    {
        IConfiguration Configuration { get; }

        ResponseModel<LabelNameModel> CreateNewLabel(LabelNameModel labelData);
    }
}