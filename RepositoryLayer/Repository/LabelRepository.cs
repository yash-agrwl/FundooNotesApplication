using CommonLayer;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using System.Linq;

namespace RepositoryLayer.Repository
{
    public class LabelRepository : ILabelRepository
    {
        private readonly FundooContext _fundooContext;

        public IConfiguration Configuration { get; }

        public LabelRepository(IConfiguration configuration, FundooContext userContext)
        {
            this.Configuration = configuration;
            this._fundooContext = userContext;
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
    }
}
