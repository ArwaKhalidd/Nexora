namespace NexoraAPI.DTOs.AcademicProfile
{
    public class AcademicProfileDto
    {
        public int StudentId { get; set; }

        public string? Gender { get; set; }

        public string? HighestEducation { get; set; }

        public string? AgeBand { get; set; }

        public string? Region { get; set; }

        public string? ImdBand { get; set; }

        public int? StudiedCredits { get; set; }

        public int? NumOfPrevAttempts { get; set; }

        public string? Disability { get; set; }

        public string? FinalResult { get; set; }
    }
}