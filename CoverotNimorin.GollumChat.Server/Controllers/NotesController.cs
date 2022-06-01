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
public class NotesController : ApiControllerBase
{
    private readonly INotesService _notesService;

    public NotesController(INotesService notesService)
    {
        _notesService = notesService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        IEnumerable<Note> notes = await _notesService.GetAll(CurrentUser!);
        return Ok(new ResultPayload<IEnumerable<Note>>(notes));
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        Note note = await _notesService.GetById(id, CurrentUser!);
        return Ok(new ResultPayload<Note>(note));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateNoteRequest model)
    {
        CreateNoteResponse noteResponse = await _notesService.AddNoteAsync(model, CurrentUser!);
        return Created(
            new Uri($"api/Notes/{noteResponse.Id}"),
            new ResultPayload<CreateNoteResponse>(noteResponse)
        );
    }
    
    [HttpPut]
    public async Task<IActionResult> Update(UpdateNoteRequest model)
    {
        await _notesService.UpdateNoteAsync(model, CurrentUser!);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _notesService.DeleteNoteAsync(id, CurrentUser!);
        return NoContent();
    }
}