namespace practika.Models
{
    public class Lecture
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string InstructorId { get; set; } = string.Empty;
        public bool IsActive { get; set; } = false;
        public string AccessCode { get; set; } = string.Empty;
        public string QrCodeUrl { get; set; } = string.Empty;
    }
}