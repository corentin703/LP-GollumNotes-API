using CoverotNimorin.GollumChat.Server.Attributes;
using CoverotNimorin.GollumChat.Server.Contracts.Services;
using CoverotNimorin.GollumChat.Server.Entities;
using CoverotNimorin.GollumChat.Server.Models.Pictures;
using CoverotNimorin.GollumChat.Server.Payloads;
using Microsoft.AspNetCore.Mvc;

namespace CoverotNimorin.GollumChat.Server.Controllers;

[ApiController]
[CustomAuthorize]
[Route("/api/[controller]/{noteId}")]
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
        IEnumerable<Picture> pictures = await _pictureService.GetAllByUserNote(noteId);
        return Ok(new ResultPayload<IEnumerable<Picture>>(pictures));
    }
    
    [HttpGet("{pictureId}")]
    public async Task<IActionResult> GetById(string noteId, string pictureId)
    {
        Picture picture = await _pictureService.GetById(noteId, pictureId);
        return File(picture.Content, picture.ContentType);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromRoute] string noteId, [FromForm] CreatePictureRequest model)
    {
        CreatePictureResponse pictureResponse = await _pictureService.AddPictureAsync(noteId, model);
        return Created(
            new Uri($"/api/Pictures/{noteId}/{pictureResponse.Id}"),
            new ResultPayload<CreatePictureResponse>(pictureResponse)
        );
    }

    [HttpDelete("{pictureId}")]
    public async Task<IActionResult> Delete(string noteId, string pictureId)
    {
        await _pictureService.DeletePictureAsync(noteId, pictureId);
        return NoContent();
    }
}