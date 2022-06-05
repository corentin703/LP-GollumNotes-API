using CoverotNimorin.GollumNotes.Server.Attributes;
using CoverotNimorin.GollumNotes.Server.Contracts.Services;
using CoverotNimorin.GollumNotes.Server.Entities;
using CoverotNimorin.GollumNotes.Server.Models.Pictures;
using CoverotNimorin.GollumNotes.Server.Payloads;
using Microsoft.AspNetCore.Mvc;

namespace CoverotNimorin.GollumNotes.Server.Controllers;

[ApiController]
[CustomAuthorize]
[Route("/api/notes/{noteId}/[controller]")]
public class PicturesController : ControllerBase
{
    private readonly IPictureService _pictureService;

    public PicturesController(IPictureService pictureService)
    {
        _pictureService = pictureService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllByUserNote(string noteId)
    {
        IEnumerable<PictureResponse> pictures = await _pictureService.GetAllByUserNote(noteId);
        return Ok(new ResultPayload<IEnumerable<PictureResponse>>(pictures));
    }
    
    [HttpGet("{pictureId}")]
    public async Task<IActionResult> GetById(string noteId, string pictureId)
    {
        PictureFullResponse picture = await _pictureService.GetById(noteId, pictureId);
        return Ok(new PictureResponse(picture));
    }
    
    [HttpGet("{pictureId}/content")]
    public async Task<IActionResult> GetContentById(string noteId, string pictureId)
    {
        PictureFullResponse picture = await _pictureService.GetById(noteId, pictureId);
        return File(picture.Content, picture.ContentType);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromRoute] string noteId, [FromForm] CreatePictureRequest model)
    {
        PictureResponse pictureResponse = await _pictureService.AddPictureAsync(noteId, model);
        return Created(
            new Uri($"/api/Pictures/{noteId}/{pictureResponse.Id}"),
            new ResultPayload<PictureResponse>(pictureResponse)
        );
    }

    [HttpDelete("{pictureId}")]
    public async Task<IActionResult> Delete(string noteId, string pictureId)
    {
        await _pictureService.DeletePictureAsync(noteId, pictureId);
        return NoContent();
    }
}