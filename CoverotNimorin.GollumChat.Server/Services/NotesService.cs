using System.Collections.Immutable;
using CoverotNimorin.GollumChat.Server.Contracts.Repositories.Entities;
using CoverotNimorin.GollumChat.Server.Contracts.Services;
using CoverotNimorin.GollumChat.Server.Entities;
using CoverotNimorin.GollumChat.Server.Exceptions;
using CoverotNimorin.GollumChat.Server.Models.Notes;

namespace CoverotNimorin.GollumChat.Server.Services;

public class NotesService : INotesService
{
    private readonly INoteRepository _noteRepository;

    private readonly Func<Note, Note> _convertToResponseExpression = note => new Note()
    {
        Id = note.Id,
        Title = note.Title,
        Content = note.Content,
        CreatedAt = note.CreatedAt.ToLocalTime(),
        LastModifiedAt = note.LastModifiedAt?.ToLocalTime(),
    };

    public NotesService(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    public async Task<IEnumerable<Note>> GetAll(User user)
    {
        IEnumerable<Note> notes = await _noteRepository.GetAllByUserAsync(user.Id);
        notes = notes
            .Select(_convertToResponseExpression)
            .ToImmutableList();

        return notes;
    }

    public async Task<Note> GetById(string id, User user)
    {
        Note note = await GetNoteWithOwnerCheck(id, user);
        return _convertToResponseExpression(note);
    }

    public async Task<CreateNoteResponse> AddNoteAsync(CreateNoteRequest model, User user)
    {
        Note note = new Note()
        {
            Title = model.Title,
            Content = model.Content,
            UserId = user.Id,
            CreatedAt = DateTime.UtcNow,
        };
        
        _noteRepository.Add(note);
        await _noteRepository.SaveChangesAsync();

        return new CreateNoteResponse(note);
    }

    public async Task UpdateNoteAsync(UpdateNoteRequest model, User user)
    {
        Note existingNote = await GetNoteWithOwnerCheck(model.Id, user);

        if (!string.IsNullOrEmpty(model.Title))
            existingNote.Title = model.Title;
        
        if (!string.IsNullOrEmpty(model.Content))
            existingNote.Content = model.Content;
        
        existingNote.LastModifiedAt = DateTime.UtcNow;
        
        _noteRepository.Update(existingNote);
        await _noteRepository.SaveChangesAsync();
    }

    public async Task DeleteNoteAsync(string id, User user)
    {
        Note existingNote = await GetNoteWithOwnerCheck(id, user);

        _noteRepository.Delete(existingNote.Id);
        await _noteRepository.SaveChangesAsync();
    }

    private async Task<Note> GetNoteWithOwnerCheck(string id, User user)
    {
        Note note = await _noteRepository.GetByIdAsync(id);
        
        if (note.UserId != user.Id)
            throw new NoteNotOwnedByUserException();

        return note;
    }
}