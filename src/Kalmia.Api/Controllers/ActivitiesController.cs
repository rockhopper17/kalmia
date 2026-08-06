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

    // GET /api/activitieds
    [HttpGet]
    public async Task<ActionResult<List<ActivityDto>>> GetAll()
    {
        var a = await _srvc.GetAllAsync();
        return Ok(a);
    }

    // GET /api/activities/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ActivityDto>> GetById(int id)
    {
        var a = await _srvc.GetByIdAsync(id);
        return a is null ? NotFound() : Ok(a);
    }

    // POST /api/activities
    [HttpPost]
    public async Task<ActionResult<ActivityDto>> Create(ActivityDto dto)
    {
        var a = await _srvc.AddAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = a.Id }, a);
    }

    // PUT /api/activities/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ActivityDto dto)
    {
        var b = await _srvc.UpdateAsync(id, dto);
        return b ? NoContent() : NotFound();
    }

    // DELETE /api/activities/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var b = await _srvc.DeleteAsync(id);
        return b ? NoContent() : NotFound();
    }
}