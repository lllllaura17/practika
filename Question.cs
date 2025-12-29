namespace practika.Models
{
    public class Question
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int LectureId { get; set; }
        public int SlideIndex { get; set; }
        public string QuestionType { get; set; } = string.Empty; // "multiple_choice", "open_text", "poll"
        public string QuestionText { get; set; } = string.Empty;
        public List<string> Options { get; set; } = new List<string>();
        public int? CorrectAnswerIndex { get; set; }
        public string Explanation { get; set; } = string.Empty;
        public int DurationSeconds { get; set; } = 30;
        public string Status { get; set; } = string.Empty;
    }
}