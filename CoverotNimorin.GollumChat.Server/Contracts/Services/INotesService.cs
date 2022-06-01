using CoverotNimorin.GollumChat.Server.Entities;
using CoverotNimorin.GollumChat.Server.Models.Notes;

namespace CoverotNimorin.GollumChat.Server.Contracts.Services;

public interface INotesService
{
    Task<IEnumerable<Note>> GetAll(User user);
    Task<Note> GetById(string id, User user);

    Task<CreateNoteResponse> AddNoteAsync(CreateNoteRequest model, User user);
    Task UpdateNoteAsync(UpdateNoteRequest model, User user);
    Task DeleteNoteAsync(string id, User user);
}