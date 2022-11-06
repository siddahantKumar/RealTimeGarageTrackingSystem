using AutoMapper;
using IndusShowroomApi.Data.Interfaces;
using IndusShowroomApi.Dtos;
using IndusShowroomApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace IndusShowroomApi.Controllers
{
    [ApiController]
    [Route("api/coa")]
    public class Charts_Of_AccountsController : ControllerBase
    {
        private readonly ICharts_Of_AccountsRepo repository;
        private readonly IMapper mapper;

        public Charts_Of_AccountsController(ICharts_Of_AccountsRepo repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet("assetsaccounts")]
        public ActionResult GetAssetsAccounts()
        {
            try
            {
                List<Charts_Of_Accounts> charts_Of_Accounts = new List<Charts_Of_Accounts>();
                foreach (var item in repository.GetCharts_Of_Accounts())
                {
                    if (item.ACC_ID == 11 || item.ACC_ID == 12)
                    {
                        charts_Of_Accounts.Add(item);
                    }
                }

                return Ok(mapper.Map<List<Charts_Of_AccountsDto>>(charts_Of_Accounts));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet]
        public ActionResult<List<Charts_Of_Accounts>> GetCharts_Of_Accounts()
        {
            try
            {
                List<Charts_Of_Accounts> charts_Of_Accounts = repository.GetCharts_Of_Accounts();
                if (charts_Of_Accounts.Count > 0)
                {
                    return Ok(charts_Of_Accounts);
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public ActionResult InsertChart_Of_Accounts(Charts_Of_Accounts charts_Of_Accounts)
        {
            repository.InsertChart_Of_Accounts(charts_Of_Accounts);
            if (!repository.SaveChanges())
            {
                return StatusCode(309, charts_Of_Accounts);
            }
            return Ok(charts_Of_Accounts);
        }

    }
}
