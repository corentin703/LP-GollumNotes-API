using System.Collections.Immutable;
using CoverotNimorin.GollumChat.Server.Contracts.Repositories.Entities;
using CoverotNimorin.GollumChat.Server.Contracts.Services;
using CoverotNimorin.GollumChat.Server.Entities;
using CoverotNimorin.GollumChat.Server.Exceptions;
using CoverotNimorin.GollumChat.Server.Models.Pictures;

namespace CoverotNimorin.GollumChat.Server.Services;

public class PictureService : IPictureService
{
    private readonly ICurrentUserService _currentUserService;
    private readonly INoteRepository _noteRepository;
    private readonly IPictureRepository _pictureRepository;
    
    private readonly Func<Picture, Picture> _convertToResponseExpression = picture => new Picture()
    {
        Id = picture.Id,
        CreatedAt = picture.CreatedAt.ToLocalTime(),
    };
    
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

    public async Task<IEnumerable<Picture>> GetAllByUserNote(string noteId)
    {
        Note note = await GetNoteWithOwnerCheckAsync(noteId);
        IEnumerable<Picture> pictures = await _pictureRepository.GetAllByNoteAsync(note);
        
        return pictures.Select(_convertToResponseExpression).ToImmutableList();
    }

    public async Task<Picture> GetById(string noteId, string pictureId)
    {
        return await GetPictureWithOwnerCheckAsync(noteId, pictureId);
    }

    public async Task<CreatePictureResponse> AddPictureAsync(CreatePictureRequest model)
    {
        Note note = await GetNoteWithOwnerCheckAsync(model.NoteId);
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
            NoteId = note.Id,
            CreatedAt = DateTime.UtcNow,
        };
     
        _pictureRepository.Add(picture);
        await _pictureRepository.SaveChangesAsync();

        return new CreatePictureResponse(picture);
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
        await GetNoteWithOwnerCheckAsync(noteId);
        Picture picture = await _pictureRepository.GetByIdAsync(pictureId);
        return picture;
    }
}