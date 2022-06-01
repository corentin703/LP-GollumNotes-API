using System.Collections.Immutable;
using CoverotNimorin.GollumChat.Server.Contracts.Repositories.Entities;
using CoverotNimorin.GollumChat.Server.Contracts.Services;
using CoverotNimorin.GollumChat.Server.Entities;
using CoverotNimorin.GollumChat.Server.Exceptions;
using CoverotNimorin.GollumChat.Server.Models.Notes;

namespace CoverotNimorin.GollumChat.Server.Services;

public class NoteService : INoteService
{
    private readonly INoteRepository _noteRepository;
    private readonly ICurrentUserService _currentUserService;
    
    private readonly Func<Note, Note> _convertToResponseExpression = note => new Note()
    {
        Id = note.Id,
        Title = note.Title,
        Content = note.Content,
        CreatedAt = note.CreatedAt.ToLocalTime(),
        LastModifiedAt = note.LastModifiedAt?.ToLocalTime(),
    };

    public NoteService(INoteRepository noteRepository, ICurrentUserService currentUserService)
    {
        _noteRepository = noteRepository;
        _currentUserService = currentUserService;
    }

    public async Task<IEnumerable<Note>> GetAllByCurrentUser()
    {
        string userId = _currentUserService.GetRequiredUser().Id;
        
        IEnumerable<Note> notes = await _noteRepository.GetAllByUserAsync(userId);
        notes = notes
            .Select(_convertToResponseExpression)
            .ToImmutableList();

        return notes;
    }

    public async Task<Note> GetById(string id)
    {
        Note note = await GetNoteWithOwnerCheck(id);
        return _convertToResponseExpression(note);
    }

    public async Task<CreateNoteResponse> AddNoteAsync(CreateNoteRequest model)
    {
        string userId = _currentUserService.GetRequiredUser().Id;
        
        Note note = new Note()
        {
            Title = model.Title,
            Content = model.Content,
            UserId = userId,
            CreatedAt = DateTime.UtcNow,
        };
        
        _noteRepository.Add(note);
        await _noteRepository.SaveChangesAsync();

        return new CreateNoteResponse(note);
    }

    public async Task UpdateNoteAsync(UpdateNoteRequest model)
    {
        Note existingNote = await GetNoteWithOwnerCheck(model.Id);

        if (!string.IsNullOrEmpty(model.Title))
            existingNote.Title = model.Title;
        
        if (!string.IsNullOrEmpty(model.Content))
            existingNote.Content = model.Content;
        
        existingNote.LastModifiedAt = DateTime.UtcNow;
        
        _noteRepository.Update(existingNote);
        await _noteRepository.SaveChangesAsync();
    }

    public async Task DeleteNoteAsync(string id)
    {
        Note existingNote = await GetNoteWithOwnerCheck(id);

        _noteRepository.Delete(existingNote.Id);
        await _noteRepository.SaveChangesAsync();
    }

    private async Task<Note> GetNoteWithOwnerCheck(string id)
    {
        string userId = _currentUserService.GetRequiredUser().Id;
        Note note = await _noteRepository.GetByIdAsync(id);
        
        if (note.UserId != userId)
            throw new NoteNotOwnedByUserException();

        return note;
    }
}