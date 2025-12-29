using Microsoft.AspNetCore.Mvc;
using practika.Models;
using practika.Services;

namespace practika.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionsController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        // GET: api/questions/{id}/stats
        [HttpGet("{id}/stats")]
        public IActionResult GetQuestionStats(int id)
        {
            try
            {
                var stats = _questionService.GetQuestionStats(id);
                return Ok(stats);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // POST: api/questions/{id}/respond
        [HttpPost("{id}/respond")]
        public IActionResult SubmitResponse(int id, [FromBody] QuestionResponse response)
        {
            try
            {
                response.QuestionId = id;
                var createdResponse = _questionService.SubmitResponse(id, response);
                return Ok(new
                {
                    status = "success",
                    message = "Ответ принят",
                    correctAnswer = createdResponse.IsCorrect ? "Правильно" : "Неправильно",
                    explanation = createdResponse.IsCorrect ? "Ваш ответ верный!" : "Попробуйте еще раз"
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/questions/lecture/{lectureId}
        [HttpGet("lecture/{lectureId}")]
        public IActionResult GetLectureQuestions(int lectureId)
        {
            var questions = _questionService.GetLectureQuestions(lectureId);
            return Ok(questions);
        }

        // PUT: api/questions/{id}/activate
        [HttpPut("{id}/activate")]
        public IActionResult ActivateQuestion(int id)
        {
            var question = _questionService.ActivateQuestion(id);
            if (question == null)
                return NotFound();

            return Ok(question);
        }

        // PUT: api/questions/{id}/complete
        [HttpPut("{id}/complete")]
        public IActionResult CompleteQuestion(int id)
        {
            var question = _questionService.CompleteQuestion(id);
            if (question == null)
                return NotFound();

            return Ok(question);
        }
    }
}