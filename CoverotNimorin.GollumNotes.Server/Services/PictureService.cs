using System.Collections.Immutable;
using CoverotNimorin.GollumNotes.Server.Contracts.Repositories.Entities;
using CoverotNimorin.GollumNotes.Server.Contracts.Services;
using CoverotNimorin.GollumNotes.Server.Entities;
using CoverotNimorin.GollumNotes.Server.Exceptions.Entities;
using CoverotNimorin.GollumNotes.Server.Models.Pictures;
using Microsoft.AspNetCore.Mvc;

namespace CoverotNimorin.GollumNotes.Server.Services;

public class PictureService : IPictureService
{
    private readonly ICurrentUserService _currentUserService;
    private readonly INoteRepository _noteRepository;
    private readonly IPictureRepository _pictureRepository;
    
    private readonly Func<Picture, PictureResponse> _convertToResponseExpression = picture => new PictureResponse(picture);
    
    public PictureService(
        ICurrentUserService currentUserService,
        INoteRepository noteRepository,
        IPictureRepository pictureRepository
    )
    {
        _currentUserService = currentUserService;
        _noteRepository = noteRepository;
        _pictureRepository = pictureRepository;
    }

    public async Task<IEnumerable<PictureResponse>> GetAllByUserNote(string noteId)
    {
        Note note = await GetNoteWithOwnerCheckAsync(noteId);
        IEnumerable<Picture> pictures = await _pictureRepository.GetAllByNoteAsync(note);
        
        return pictures
            .Select(_convertToResponseExpression)
            .ToImmutableList();
    }

    public async Task<PictureFullResponse> GetById(string noteId, string pictureId)
    {
        Picture picture = await GetPictureWithOwnerCheckAsync(noteId, pictureId);
        return new PictureFullResponse(picture);
    }

    public async Task<PictureResponse> AddPictureAsync(string noteId, [FromForm] CreatePictureRequest model)
    {
        Note note = await GetNoteWithOwnerCheckAsync(noteId);
        byte[] content = new byte[model.File.Length];

        int imageReadResult;
        using (Stream stream = model.File.OpenReadStream())
        {
            imageReadResult = await stream.ReadAsync(content, 0, content.Length);
        }
        
        if (imageReadResult < model.File.Length)
            throw new PictureProcessingException();

        Picture picture = new()
        {
            Content = content,
            ContentType = model.File.ContentType,
            NoteId = note.Id,
            CreatedAt = DateTime.UtcNow,
        };
     
        _pictureRepository.Add(picture);
        await _pictureRepository.SaveChangesAsync();

        return new PictureResponse(picture);
    }

    public async Task DeletePictureAsync(string noteId, string pictureId)
    {
        Picture picture = await GetPictureWithOwnerCheckAsync(noteId, pictureId);
        _pictureRepository.Delete(picture.Id);
        await _pictureRepository.SaveChangesAsync();
    }
    
    private async Task<Note> GetNoteWithOwnerCheckAsync(string nodeId)
    {
        string userId = _currentUserService.GetRequiredUser().Id;
        Note note = await _noteRepository.GetByIdAsync(nodeId);
        
        if (note.UserId != userId)
            throw new NoteNotOwnedByUserException();

        return note;
    }

    private async Task<Picture> GetPictureWithOwnerCheckAsync(string noteId, string pictureId)
    {
        Note note = await GetNoteWithOwnerCheckAsync(noteId);
        Picture picture = await _pictureRepository.GetByIdAsync(pictureId);

        if (picture.NoteId != note.Id)
            throw new PictureNotOwnedByNoteException();
        
        return picture;
    }
}