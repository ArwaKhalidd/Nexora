
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
        Task<UserProfileDto?> GetProfileAsync(int userId);

        Task<bool> UpdateProfileAsync(int userId, UpdateAcademicProfileDto dto);
    }
}