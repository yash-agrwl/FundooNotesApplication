using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;

namespace BusinessLayer.Manager
{
    public class LabelManager : ILabelManager
    {
        private readonly ILabelRepository repository;

        public IConfiguration Configuration { get; }

        public LabelManager(ILabelRepository repository, IConfiguration configuration)
        {
            this.repository = repository;
            this.Configuration = configuration;
        }

        public ResponseModel<LabelNameModel> CreateNewLabel(LabelNameModel labelData)
        {
            try
            {
                return this.repository.CreateNewLabel(labelData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
