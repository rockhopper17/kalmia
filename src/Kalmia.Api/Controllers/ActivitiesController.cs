using Kalmia.Core.Common;
using Kalmia.Core.Entities;
using Kalmia.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kalmia.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ActivitiesController :  ControllerBase
{
    private readonly IActivityService _srvc;
    public ActivitiesController(IActivityService srvc) => _srvc = srvc;

    // GET /api/activities
    [HttpGet]
    public async Task<ActionResult<List<ActivityDto>>> GetAll()
    {
        var result = await _srvc.GetAllAsync();
        return Ok(result.Value);
    }

    // GET /api/activities/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ActivityDto>> GetById(int id)
    {
        var result = await _srvc.GetByIdAsync(id);
        return result.ErrorType == ResultErrorType.NotFound ? NotFound() : Ok(result.Value);
    }

    // POST /api/activities
    [HttpPost]
    public async Task<ActionResult<ActivityDto>> Create(ActivityDto dto)
    {
        var result = await _srvc.AddAsync(dto);
        if (!result.IsSuccess) return BadRequest(result.Errors);
        return CreatedAtAction(nameof(GetById), new { id = result.Value!.Id }, result.Value);
    }

    // PUT /api/activities/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ActivityDto dto)
    {
        var result = await _srvc.UpdateAsync(id, dto);
        if (result.ErrorType == ResultErrorType.NotFound) return NotFound();
        if (!result.IsSuccess) return BadRequest(result.Errors);
        return Ok(result.Value);
    }

    // DELETE /api/activities/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _srvc.DeleteAsync(id);
        return result.ErrorType == ResultErrorType.NotFound ? NotFound() : NoContent();
    }
}