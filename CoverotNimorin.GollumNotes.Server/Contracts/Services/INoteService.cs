using CoverotNimorin.GollumNotes.Server.Entities;
using CoverotNimorin.GollumNotes.Server.Models.Notes;

namespace CoverotNimorin.GollumNotes.Server.Contracts.Services;

public interface INoteService
{
    Task<IEnumerable<Note>> GetAllByCurrentUser();
    Task<Note> GetById(string id);

    Task<CreateNoteResponse> AddNoteAsync(CreateNoteRequest model);
    Task UpdateNoteAsync(UpdateNoteRequest model);
    Task DeleteNoteAsync(string id);
}