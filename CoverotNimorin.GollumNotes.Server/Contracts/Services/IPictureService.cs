using CoverotNimorin.GollumNotes.Server.Entities;
using CoverotNimorin.GollumNotes.Server.Models.Pictures;

namespace CoverotNimorin.GollumNotes.Server.Contracts.Services;

public interface IPictureService
{
    Task<IEnumerable<Picture>> GetAllByUserNote(string noteId);
    Task<Picture> GetById(string noteId, string pictureId);

    Task<CreatePictureResponse> AddPictureAsync(string noteId, CreatePictureRequest model);
    Task DeletePictureAsync(string noteId, string pictureId);
}