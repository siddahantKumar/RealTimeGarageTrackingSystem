using AutoMapper;
using IndusShowroomApi.Data.Services;
using IndusShowroomApi.Dtos;
using IndusShowroomApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace IndusShowroomApi.Controllers
{
    [Route("api/setup")]
    [ApiController]
    public class SetupController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ISetupService setupService;

        public SetupController(IMapper mapper, ISetupService setupService)
        {
            this.mapper = mapper;
            this.setupService = setupService;
        }

        [HttpGet("car")]
        public ActionResult<List<CarDto>> GetCars()
        {
            List<CarDto> cars = mapper.Map<List<CarDto>>(setupService.GetProducts());
            if (cars.Count > 0)
            {
                return Ok(cars);
            }
            return NoContent();
        }

        [HttpPost("car")]
        public ActionResult<bool> InsertCar(CarDto CarDto)
        {
            if (setupService.InsertProduct(mapper.Map<Product>(CarDto)))
                return Ok(true);

            return StatusCode(409, false);
        }

        [HttpPatch("car")]
        public ActionResult<bool> UpdateCar([FromBody] JsonPatchDocument<CarDto> patchedCarDto)
        {
            CarDto carDto = new CarDto();
            patchedCarDto.ApplyTo(carDto, ModelState);

            if (!TryValidateModel(carDto))
            {
                return ValidationProblem(ModelState);
            }


            if (setupService.UpdateProduct(mapper.Map<Product>(carDto)))
                return Ok(true);

            return StatusCode(409, false);
        }

        [HttpDelete("car")]
        public ActionResult<bool> DeleteCar(CarDto carDto)
        {
            if (setupService.DeleteProduct(setupService.GetProduct(carDto.CAR_ID)))
                return Ok(true);
            return StatusCode(409, false);
        }

        [HttpGet("car_category")]
        public ActionResult<List<Car_CategoryDto>> GetCar_Categories()
        {
            List<Car_CategoryDto> car_Categories = mapper.Map<List<Car_CategoryDto>>(setupService.GetProduct_Categories());
            if (car_Categories.Count > 0)
            {
                return Ok(car_Categories);
            }
            return NoContent();
        }

        [HttpPost("car_category")]
        public ActionResult<bool> InsertCar_Categories(Car_CategoryDto car_CategoryDto)
        {
            if (setupService.InsertProduct_Category(mapper.Map<Product_Category>(car_CategoryDto)))
                return Ok(true);

            return StatusCode(409, false);
        }

        [HttpPatch("car_category")]
        public ActionResult<bool> UpdateCar_Categories([FromBody] JsonPatchDocument<Car_CategoryDto> patchedCar_CategoryDto)
        {

            Car_CategoryDto car_CategoryDto = new Car_CategoryDto();
            patchedCar_CategoryDto.ApplyTo(car_CategoryDto, ModelState);

            if (!TryValidateModel(car_CategoryDto))
            {
                return ValidationProblem(ModelState);
            }


            if (setupService.UpdateProduct_Category(mapper.Map<Product_Category>(car_CategoryDto)))
                return Ok(true);

            return StatusCode(409, false);
        }

        [HttpDelete("car_category")]
        public ActionResult<bool> DeleteCar_Category(Car_CategoryDto car_CategoryDto)
        {
            if (setupService.DeleteProduct_Category(setupService.GetProduct_Category(car_CategoryDto.CAR_CAT_ID)))
                return Ok(true);
            return StatusCode(409, false);
        }

        [HttpGet("company")]
        public ActionResult<List<CompanyDto>> GetCompanies()
        {
            List<CompanyDto> companies = mapper.Map<List<CompanyDto>>(setupService.GetProduct_Brands());
            if (companies.Count > 0)
            {
                return Ok(companies);
            }
            return NoContent();
        }

        [HttpPost("company")]
        public ActionResult<bool> InsertCompany(CompanyDto car_BrandDto)
        {
            if (setupService.InsertProduct_Brand(mapper.Map<Product_Brand>(car_BrandDto)))
                return Ok(true);

            return StatusCode(409, false);
        }

        [HttpPatch("company")]
        public ActionResult<bool> UpdateCompany([FromBody] JsonPatchDocument<CompanyDto> patchedCompanyDto)
        {
            CompanyDto companyDto = new CompanyDto();
            patchedCompanyDto.ApplyTo(companyDto, ModelState);
            if (!TryValidateModel(companyDto))
            {
                return ValidationProblem(ModelState);
            }


            if (setupService.UpdateProduct_Brand(mapper.Map<Product_Brand>(companyDto)))
                return Ok(true);

            return StatusCode(409, false);
        }

        [HttpDelete("company")]
        public ActionResult<bool> DeleteCompany(CompanyDto companyDto)
        {
            if (setupService.DeleteProduct_Brand(setupService.GetProduct_Brand(companyDto.COMPANY_ID)))
                return Ok(true);
            return StatusCode(409, false);
        }

    }
}
