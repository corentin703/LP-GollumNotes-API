using CoverotNimorin.GollumChat.Server.Entities;
using CoverotNimorin.GollumChat.Server.Models.Pictures;

namespace CoverotNimorin.GollumChat.Server.Contracts.Services;

public interface IPictureService
{
    Task<IEnumerable<Picture>> GetAllByUserNote(string noteId);
    Task<Picture> GetById(string noteId, string pictureId);

    Task<CreatePictureResponse> AddPictureAsync(CreatePictureRequest model);
    Task DeletePictureAsync(string noteId, string pictureId);
}