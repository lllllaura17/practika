namespace practika.Models
{
    public class QuestionStatistics
    {
        public int QuestionId { get; set; }
        public string Status { get; set; } = string.Empty;
        public int TotalResponses { get; set; }
        public Dictionary<int, int> Results { get; set; } = new Dictionary<int, int>();
        public int? CorrectAnswer { get; set; }
        public double CorrectPercentage { get; set; }
        public double AverageResponseTimeSeconds { get; set; }
        public int StudentCount { get; set; }
        public int TimeLeft { get; set; }
    }
}