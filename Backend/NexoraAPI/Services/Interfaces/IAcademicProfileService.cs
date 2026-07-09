
// namespace NexoraAPI.Services.Interfaces
// {
//     public interface IAcademicProfileService
//     {

//     }
// }

using NexoraAPI.DTOs.AcademicProfile;

namespace NexoraAPI.Services.Interfaces
{
    public interface IAcademicProfileService
    {
        Task<AcademicProfileDto?> GetProfileAsync(int userId);

        Task<bool> UpdateProfileAsync(int userId, AcademicProfileDto dto);
    }
}