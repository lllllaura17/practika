using practika.Models;

namespace practika.Services
{
   public interface IQuestionService
    {
        Question CreateQuestion(int lectureId, Question question);
        QuestionStatistics GetQuestionStats(int questionId);
        QuestionResponse SubmitResponse(int questionId, QuestionResponse response);
        List<Question> GetLectureQuestions(int lectureId); 
        Question? GetQuestionById(int questionId);
        Question? ActivateQuestion(int questionId);
        Question? CompleteQuestion(int questionId);
    }
}