namespace practika.Models
{
    public class QuestionResponse
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string StudentToken { get; set; } = string.Empty;
        public int? SelectedOptionIndex { get; set; }
        public string TextAnswer { get; set; } = string.Empty;
        public int ResponseTimeMs { get; set; }
        public DateTime RespondedAt { get; set; } = DateTime.UtcNow;
        public bool IsCorrect { get; set; }
    }
}