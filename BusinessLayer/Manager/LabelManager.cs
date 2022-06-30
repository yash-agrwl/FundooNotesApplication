using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Manager
{
    public class LabelManager : ILabelManager
    {
        private readonly ILabelRepository _repository;

        public IConfiguration Configuration { get; }

        public LabelManager(ILabelRepository repository, IConfiguration configuration)
        {
            this._repository = repository;
            this.Configuration = configuration;
        }

        public ResponseModel<LabelNameModel> CreateNewLabel(LabelNameModel labelData)
        {
            try
            {
                return this._repository.CreateNewLabel(labelData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResponseModel<List<string>> GetAllLabel(int userId)
        {
            try
            {
                return this._repository.GetAllLabel(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResponseModel<LabelNameModel> DeleteLabel(int userId, string labelName)
        {
            try
            {
                return this._repository.DeleteLabel(userId, labelName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
