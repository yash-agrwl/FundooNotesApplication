using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FundooNotesApplication.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CollaboratorController : Controller
    {
        private readonly ICollaboratorManager manager;

        public CollaboratorController(ICollaboratorManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult AddCollaborator(CollaboratorModel collab, int userId)
        {
            try
            {
                var result = this.manager.AddCollaborator(collab, userId);

                if (result != null)
                {
                    return this.Ok(result);
                }
                
                return this.BadRequest(result);
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string> { Status = false, Message = ex.Message });
            }
        }
    }
}
