using Microsoft.AspNetCore.Mvc;
using practika.Models;
using practika.Services;

namespace practika.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LecturesController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public LecturesController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        // POST: api/lectures/{lectureId}/questions
        [HttpPost("{lectureId}/questions")]
        public IActionResult CreateQuestion(int lectureId, [FromBody] Question question)
        {
            try
            {
                var createdQuestion = _questionService.CreateQuestion(lectureId, question);

                // Имитация расчета статистики (в реальности будет рассчитываться позже)
                var stats = new
                {
                    questionId = createdQuestion.Id,
                    status = createdQuestion.Status,
                    studentCount = 25,
                    results = new Dictionary<int, int>(),
                    correctAnswerPercentage = 0.0,
                    timeLeft = createdQuestion.DurationSeconds
                };

                return CreatedAtAction(
                    nameof(QuestionsController.GetQuestionStats),
                    "Questions",
                    new { id = createdQuestion.Id },
                    new { question = createdQuestion, statistics = stats }
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/lectures
        [HttpGet]
        public IActionResult GetLectures()
        {
            // Временные данные для примера
            var lectures = new List<Lecture>
            {
                new Lecture { Id = 1, Title = "Введение в программирование", IsActive = true },
                new Lecture { Id = 2, Title = "Основы алгоритмов", IsActive = false }
            };

            return Ok(lectures);
        }

        // GET: api/lectures/{id}
        [HttpGet("{id}")]
        public IActionResult GetLecture(int id)
        {
            // Временные данные для примера
            if (id == 1)
            {
                var lecture = new Lecture
                {
                    Id = 1,
                    Title = "Введение в программирование",
                    Description = "Основные понятия программирования",
                    IsActive = true,
                    AccessCode = "ABC123",
                    QrCodeUrl = "https://api.qrserver.com/v1/create-qr-code/?size=150x150&data=lecture-1-ABC123"
                };
                return Ok(lecture);
            }

            return NotFound();
        }
    }
}