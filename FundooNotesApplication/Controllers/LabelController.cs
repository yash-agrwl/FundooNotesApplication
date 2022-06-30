using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FundooNotesApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LabelController : Controller
    {
        private readonly ILabelManager manager;

        public LabelController(ILabelManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult CreateNewLabel(LabelNameModel labelData)
        {
            try
            {
                var result = this.manager.CreateNewLabel(labelData);

                if (result.Status == true)
                {
                    return this.Ok(result);
                }
                else
                {
                    return this.BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string> { Status = false, Message = ex.Message });
            }
        }
    }
}
