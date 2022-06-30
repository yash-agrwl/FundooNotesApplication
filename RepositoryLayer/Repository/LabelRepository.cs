using CommonLayer;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryLayer.Repository
{
    public class LabelRepository : ILabelRepository
    {
        private readonly FundooContext _fundooContext;

        public IConfiguration Configuration { get; }

        public LabelRepository(IConfiguration configuration, FundooContext _fundooContext)
        {
            this.Configuration = configuration;
            this._fundooContext = _fundooContext;
        }

        public ResponseModel<LabelNameModel> CreateNewLabel(LabelNameModel labelData)
        {
            var result = new ResponseModel<LabelNameModel>();
            var existUser = this._fundooContext.Users.Where(x => x.UserID == labelData.UserId).SingleOrDefault();

            if (existUser != null)
            {
                var existLabel = this._fundooContext.LabelNames.Where(x => x.UserId == labelData.UserId &&
                                                                           x.LabelName == labelData.LabelName)
                                                                            .SingleOrDefault();
                if (existLabel == null)
                {
                    this._fundooContext.Add(labelData);
                    this._fundooContext.SaveChanges();

                    result.Status = true;
                    result.Message = "Label Created Successfully";
                    result.Data = labelData;
                    return result;
                }

                result.Message = "Label Already Exists";
                return result;
            }

            result.Message = "User doesn't exist";
            return result;
        }

        public ResponseModel<List<string>> GetAllLabel(int userId)
        {
            var result = new ResponseModel<List<string>>();
            var existUser = this._fundooContext.Users.Where(x => x.UserID == userId).SingleOrDefault();

            if (existUser != null)
            {
                var labelList = (from label in this._fundooContext.LabelNames
                                 where label.UserId == userId select label.LabelName).ToList();

                if (labelList.Count > 0)
                {
                    result.Status = true;
                    result.Message = $"{labelList.Count} Labels Successfully retrieved";
                    result.Data = labelList;
                    return result;
                }

                result.Message = "No Label exist for this user";
                return result;
            }

            result.Message = "User doesn't exist";
            return result;
        }

        public ResponseModel<LabelNameModel> DeleteLabel(int userId, string labelName)
        {
            var result = new ResponseModel<LabelNameModel>();
            var existLabel = this._fundooContext.LabelNames.Where(x => x.LabelName == labelName &&
                                                                       x.UserId == userId).SingleOrDefault();

            if (existLabel != null)
            {
                var labelNotes = (from note in this._fundooContext.LabelNotes
                                  where note.LabelNames == existLabel select note).ToList();

                foreach (var note in labelNotes)
                {
                    this._fundooContext.Remove(note);
                }

                this._fundooContext.Remove(existLabel);
                this._fundooContext.SaveChanges();

                result.Status = true;
                result.Message = "Label Deleted Successfully";
                result.Data = existLabel;
                return result;
            }

            result.Message = "Label doesn't exist";
            return result;
        }
    }
}
