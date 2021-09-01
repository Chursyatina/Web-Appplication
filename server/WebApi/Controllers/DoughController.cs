namespace WebApi.Controllers
{
    using System.Collections.Generic;
    using Application.DTO.Request;
    using Application.DTO.Response;
    using Application.Interfaces;
    using Application.Validation;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Swashbuckle.AspNetCore.Annotations;

    [ApiController]
    [Route("api/doughs")]
    public class DoughController : Controller
    {
        private readonly ILogger<DoughController> _logger;
        private readonly IDoughService _doughService;
        private readonly DoughValidator _doughValidator;

        public DoughController(ILogger<DoughController> logger, IDoughService doughService)
        {
            _logger = logger;
            _doughService = doughService;
            _doughValidator = new DoughValidator(_doughService);
        }

        [HttpGet]
        [SwaggerResponse(200, "Returns all doughs")]
        [SwaggerResponse(400, "Bad request with message of an error.")]
        public ActionResult<List<DoughDto>> GetAll()
        {
            return Ok(_doughService.GetAll());
        }

        [HttpGet("{id}")]
        [SwaggerResponse(200, "Returns dough by input id")]
        [SwaggerResponse(400, "Bad request with message of an error.")]
        [SwaggerResponse(404, "Not found")]
        public ActionResult<DoughDto> Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }

            var existingDough = _doughService.GetById(id);
            if (existingDough == null)
            {
                return NotFound();
            }

            return Ok(existingDough);
        }

        [HttpPost]
        [SwaggerResponse(201, "Inserts new dough in database")]
        [SwaggerResponse(400, "Bad request with message of an error.")]
        public ActionResult<DoughDto> Insert([FromBody] DoughCreateRequestDto dough)
        {
            ValidationResult validationResult = _doughValidator.Validate(dough);
            if (!validationResult.IsValid)
            {
                return BadRequest(new JsonResult(validationResult.ErrorMessage) { StatusCode = 400, });
            }

            DoughDto returnedDto = _doughService.Insert(dough);
            return Created("api/doughs/" + returnedDto.Id.ToString(), returnedDto);
        }

        [HttpPut("{id}")]
        [SwaggerResponse(200, "Updates existing dough in database")]
        [SwaggerResponse(400, "Bad request with message of an error.")]
        [SwaggerResponse(404, "Not found")]
        public ActionResult<DoughDto> Update([FromRoute] int id, [FromBody] DoughUpdateRequestDto dough)
        {
            var existingDough = _doughService.GetById(id);
            if (existingDough == null)
            {
                return NotFound();
            }

            ValidationResult validationResult = _doughValidator.Validate(dough, id);
            if (!validationResult.IsValid)
            {
                return BadRequest(new JsonResult(validationResult.ErrorMessage) { StatusCode = 400, });
            }

            return Ok(_doughService.Update(id, dough));
        }

        [HttpPatch("{id}")]
        [SwaggerResponse(200, "Updates existing dough in database")]
        [SwaggerResponse(400, "Bad request with message of an error.")]
        [SwaggerResponse(404, "Not found")]
        public ActionResult<DoughDto> Patch([FromRoute] int id, [FromBody] DoughPatchRequestDto dough)
        {
            var existingDough = _doughService.GetById(id);
            if (existingDough == null)
            {
                return NotFound();
            }

            ValidationResult validationResult = _doughValidator.Validate(dough, id);
            if (!validationResult.IsValid)
            {
                return BadRequest(new JsonResult(validationResult.ErrorMessage) { StatusCode = 400, });
            }

            return Ok(_doughService.Patch(id, dough));
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(204, "Deletes existing dough from database(by changing flag IsDeleted)")]
        [SwaggerResponse(400, "Bad request with message of an error.")]
        [SwaggerResponse(404, "Not found")]
        public ActionResult Delete([FromRoute] int id)
        {
            var existingDough = _doughService.GetById(id);
            if (existingDough == null)
            {
                return NotFound();
            }

            _doughService.Delete(id);
            return NoContent();
        }
    }
}