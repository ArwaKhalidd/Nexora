
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
        Task<AcademicProfileDto?> GetProfileAsync(int studentId);

        Task<bool> UpdateProfileAsync(int studentId, UpdateAcademicProfileDto dto);
    }
}