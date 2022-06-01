using CoverotNimorin.GollumChat.Server.Attributes;
using CoverotNimorin.GollumChat.Server.Contracts.Services;
using CoverotNimorin.GollumChat.Server.Entities;
using CoverotNimorin.GollumChat.Server.Models.Notes;
using CoverotNimorin.GollumChat.Server.Payloads;
using Microsoft.AspNetCore.Mvc;

namespace CoverotNimorin.GollumChat.Server.Controllers;

[ApiController]
[CustomAuthorize]
[Route("/api/[controller]")]
public class NotesController : ControllerBase
{
    private readonly INoteService _noteService;

    public NotesController(INoteService noteService)
    {
        _noteService = noteService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllByUser()
    {
        IEnumerable<Note> notes = await _noteService.GetAllByCurrentUser();
        return Ok(new ResultPayload<IEnumerable<Note>>(notes));
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        Note note = await _noteService.GetById(id);
        return Ok(new ResultPayload<Note>(note));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateNoteRequest model)
    {
        CreateNoteResponse noteResponse = await _noteService.AddNoteAsync(model);
        return Created(
            new Uri($"/api/Notes/{noteResponse.Id}"),
            new ResultPayload<CreateNoteResponse>(noteResponse)
        );
    }
    
    [HttpPut]
    public async Task<IActionResult> Update(UpdateNoteRequest model)
    {
        await _noteService.UpdateNoteAsync(model);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _noteService.DeleteNoteAsync(id);
        return NoContent();
    }
}