using Microsoft.EntityFrameworkCore;
using NexoraAPI.DTOs.Skills;
using NexoraAPI.Models;
using NexoraAPI.Services.Interfaces;

namespace NexoraAPI.Services.Implementations;

public class SkillService : ISkillService
{
    private readonly AppDbContext _context;
    private readonly INotificationService _notificationService;

    public SkillService(AppDbContext context, INotificationService notificationService)
    {
        _context = context;
        _notificationService = notificationService;
    }

    public async Task<List<StudentSkillDto>> GetSkillsAsync(int userId)
    {
        return await _context.StudentSkills
            .Where(s => s.UserId == userId)
            .OrderByDescending(s => s.AddedAt)
            .Select(s => new StudentSkillDto
            {
                Id = s.Id,
                SkillName = s.SkillName,
                TargetLevel = s.TargetLevel,
                AddedAt = s.AddedAt
            })
            .ToListAsync();
    }

    public async Task<StudentSkillDto?> AddSkillAsync(int userId, UpdateSkillDto dto)
    {
        // Prevent duplicate skill names per user (case-insensitive)
        bool exists = await _context.StudentSkills
            .AnyAsync(s => s.UserId == userId &&
                           s.SkillName.ToLower() == dto.SkillName.ToLower());
        if (exists) return null;

        // Check if this is the user's first skill
        bool isFirstSkill = !await _context.StudentSkills.AnyAsync(s => s.UserId == userId);

        var skill = new StudentSkill
        {
            UserId = userId,
            SkillName = dto.SkillName.Trim(),
            TargetLevel = dto.TargetLevel,
            AddedAt = DateTime.Now
        };

        _context.StudentSkills.Add(skill);
        await _context.SaveChangesAsync();

        // --- Motivational notification on the user's very first skill ---
        if (isFirstSkill)
        {
            await _notificationService.SendNotificationAsync(
                userId: userId,
                title: "💪 You started your skill journey!",
                message: $"Great start! You added \"{skill.SkillName}\" as your first skill. Keep building your profile to get better course recommendations.",
                type: "SkillMilestone"
            );
        }

        return new StudentSkillDto
        {
            Id = skill.Id,
            SkillName = skill.SkillName,
            TargetLevel = skill.TargetLevel,
            AddedAt = skill.AddedAt
        };
    }

    public async Task<bool> UpdateSkillAsync(int userId, int skillId, UpdateSkillDto dto)
    {
        var skill = await _context.StudentSkills
            .FirstOrDefaultAsync(s => s.Id == skillId && s.UserId == userId);

        if (skill == null) return false;

        bool leveledUpToAdvanced = skill.TargetLevel != "Advanced" && dto.TargetLevel == "Advanced";

        skill.SkillName = dto.SkillName.Trim();
        skill.TargetLevel = dto.TargetLevel;

        await _context.SaveChangesAsync();

        // --- Celebrate reaching Advanced level ---
        if (leveledUpToAdvanced)
        {
            await _notificationService.SendNotificationAsync(
                userId: userId,
                title: "🏆 Advanced Level Unlocked!",
                message: $"You set \"{skill.SkillName}\" to Advanced. Amazing progress — you're on your way to mastery!",
                type: "SkillMilestone"
            );
        }

        return true;
    }

    public async Task<bool> DeleteSkillAsync(int userId, int skillId)
    {
        var skill = await _context.StudentSkills
            .FirstOrDefaultAsync(s => s.Id == skillId && s.UserId == userId);

        if (skill == null) return false;

        _context.StudentSkills.Remove(skill);
        await _context.SaveChangesAsync();
        return true;
    }
}
