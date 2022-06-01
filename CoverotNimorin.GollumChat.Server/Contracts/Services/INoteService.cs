using CoverotNimorin.GollumChat.Server.Entities;
using CoverotNimorin.GollumChat.Server.Models.Notes;

namespace CoverotNimorin.GollumChat.Server.Contracts.Services;

public interface INoteService
{
    Task<IEnumerable<Note>> GetAllByCurrentUser();
    Task<Note> GetById(string id);

    Task<CreateNoteResponse> AddNoteAsync(CreateNoteRequest model);
    Task UpdateNoteAsync(UpdateNoteRequest model);
    Task DeleteNoteAsync(string id);
}