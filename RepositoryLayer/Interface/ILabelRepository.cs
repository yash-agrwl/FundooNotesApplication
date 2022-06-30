using CommonLayer;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace RepositoryLayer.Interface
{
    public interface ILabelRepository
    {
        IConfiguration Configuration { get; }

        ResponseModel<LabelNameModel> CreateNewLabel(LabelNameModel labelData);

        ResponseModel<List<string>> GetAllLabel(int userId);

        ResponseModel<LabelNameModel> DeleteLabel(int userId, string labelName);

        ResponseModel<LabelNoteModel> AddNoteToLabel(string labelName, int noteId, int userId);
    }
}