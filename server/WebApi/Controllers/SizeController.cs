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
    [Route("api/sizes")]
    public class SizeController : Controller
    {
        private readonly ILogger<SizeController> _logger;
        private readonly ISizeService _sizeService;
        private readonly SizeValidator _sizeValidator;

        public SizeController(ILogger<SizeController> logger, ISizeService sizeService)
        {
            _logger = logger;
            _sizeService = sizeService;
            _sizeValidator = new SizeValidator(_sizeService);
        }

        [HttpGet]
        [SwaggerResponse(200, "Returns all Sizes")]
        [SwaggerResponse(400, "Bad request with message of an error.")]
        public ActionResult<List<SizeDto>> GetAll()
        {
            return Ok(_sizeService.GetAll());
        }

        [HttpGet("{id}")]
        [SwaggerResponse(200, "Returns size by input id")]
        [SwaggerResponse(400, "Bad request with message of an error.")]
        [SwaggerResponse(404, "Not found")]
        public ActionResult<SizeDto> Get(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingSize = _sizeService.GetById(id);
            if (existingSize == null)
            {
                return NotFound();
            }

            return Ok(existingSize);
        }

        [HttpPost]
        [SwaggerResponse(201, "Inserts new size in database")]
        [SwaggerResponse(400, "Bad request with message of an error.")]
        public ActionResult<SizeDto> Insert([FromBody] SizeCreateRequestDto size)
        {
            ValidationResult validationResult = _sizeValidator.Validate(size);
            if (!validationResult.IsValid)
            {
                return BadRequest(new JsonResult(validationResult.ErrorMessage) { StatusCode = 400, });
            }

            SizeDto returnedDto = _sizeService.Insert(size);
            return Created("api/sizes/" + returnedDto.Id.ToString(), returnedDto);
        }

        [HttpPut("{id}")]
        [SwaggerResponse(200, "Updates existing size in database")]
        [SwaggerResponse(400, "Bad request with message of an error.")]
        [SwaggerResponse(404, "Not found")]
        public ActionResult<SizeDto> Update([FromRoute] string id, [FromBody] SizeUpdateRequestDto size)
        {
            var existingSize = _sizeService.GetById(id);
            if (existingSize == null)
            {
                return NotFound();
            }

            ValidationResult validationResult = _sizeValidator.Validate(size, id);
            if (!validationResult.IsValid)
            {
                return BadRequest(new JsonResult(validationResult.ErrorMessage) { StatusCode = 400, });
            }

            return Ok(_sizeService.Update(id, size));
        }

        [HttpPatch("{id}")]
        [SwaggerResponse(200, "Updates existing size in database")]
        [SwaggerResponse(400, "Bad request with message of an error.")]
        [SwaggerResponse(404, "Not found")]
        public ActionResult<SizeDto> Patch([FromRoute] string id, [FromBody] SizePatchRequestDto size)
        {
            var existingSize = _sizeService.GetById(id);
            if (existingSize == null)
            {
                return NotFound();
            }

            ValidationResult validationResult = _sizeValidator.Validate(size, id);
            if (!validationResult.IsValid)
            {
                return BadRequest(new JsonResult(validationResult.ErrorMessage) { StatusCode = 400, });
            }

            return Ok(_sizeService.Patch(id, size));
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(204, "Deletes existing size from database(by changing flag IsDeleted)")]
        [SwaggerResponse(400, "Bad request with message of an error.")]
        [SwaggerResponse(404, "Not found")]
        public ActionResult Delete([FromRoute] string id)
        {
            var existingSize = _sizeService.GetById(id);
            if (existingSize == null)
            {
                return NotFound();
            }

            _sizeService.Delete(id);
            return NoContent();
        }
    }
}