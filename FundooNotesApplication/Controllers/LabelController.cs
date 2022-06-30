﻿using BusinessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FundooNotesApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LabelController : Controller
    {
        private readonly ILabelManager _manager;

        public LabelController(ILabelManager manager)
        {
            this._manager = manager;
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult CreateNewLabel(LabelNameModel labelData)
        {
            try
            {
                var result = this._manager.CreateNewLabel(labelData);

                if (result.Status == true)
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

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAllLabel(int userId)
        {
            try
            {
                var result = this._manager.GetAllLabel(userId);

                if (result.Status == true)
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

        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteLabel(int userId, string labelName)
        {
            try
            {
                var result = this._manager.DeleteLabel(userId, labelName);

                if (result.Status == true)
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
