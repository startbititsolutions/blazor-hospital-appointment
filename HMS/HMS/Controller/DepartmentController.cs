﻿using HMS.Model.DatabaseModel;
using HMS.Service.Implementations;
using HMS.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace HMS.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        #region Fields
        private readonly IDepartmentService _departmentService;
        private readonly ILogService _logService;
        #endregion

        #region Constructor
        public DepartmentController(IDepartmentService departmentService, ILogService logService)
        {
            _departmentService = departmentService; 
            _logService = logService;
        }
        #endregion

        #region Method

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var val = await _departmentService.GetAll();
                return StatusCode(StatusCodes.Status200OK, val);
            }
            catch (Exception ex)
            {
                 await  _logService.Error(ex);
                return StatusCode(StatusCodes.Status500InternalServerError ,ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Department data)
        {
            try
            {
                if(data != null)
                {
                    var res = await _departmentService.Insert(data);
                    return StatusCode(StatusCodes.Status200OK, res);
                }

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                 await  _logService.Error(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        #endregion
    }
}
