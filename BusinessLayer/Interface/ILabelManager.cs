using CommonLayer;
using Microsoft.Extensions.Configuration;

namespace BusinessLayer.Interface
{
    public interface ILabelManager
    {
        IConfiguration Configuration { get; }

        ResponseModel<LabelNameModel> CreateNewLabel(LabelNameModel labelData);
    }
}