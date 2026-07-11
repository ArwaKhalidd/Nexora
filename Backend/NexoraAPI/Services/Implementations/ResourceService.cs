using Microsoft.EntityFrameworkCore;
using NexoraAPI.Models;

namespace NexoraAPI.Services
{
    public class ResourceService
    {
        private readonly AppDbContext _context;

        public ResourceService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Recommendation>> GetResourcesBySubject(string subject)
        {
            return await _context.Recommendations
                .Where(r => r.SubjectName == subject)
                .ToListAsync();
        }

        public async Task<List<Recommendation>> GetAllRecommendations()
        {
            return await _context.Recommendations.ToListAsync();
        }
    }
}