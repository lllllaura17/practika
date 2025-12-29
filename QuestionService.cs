using practika.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace practika.Services
{
    public class QuestionService : IQuestionService
    {
        private static List<Question> _questions = new List<Question>();
        private static List<QuestionResponse> _responses = new List<QuestionResponse>();
        private static List<Lecture> _lectures = new List<Lecture>
        {
            new Lecture { Id = 1, Title = "Введение в программирование", InstructorId = "teacher1", IsActive = true, AccessCode = "ABC123" },
            new Lecture { Id = 2, Title = "Основы алгоритмов", InstructorId = "teacher2", IsActive = false, AccessCode = "DEF456" }
        };

        public Question CreateQuestion(int lectureId, Question question)
        {
            var lecture = _lectures.FirstOrDefault(l => l.Id == lectureId);
            if (lecture == null)
                throw new ArgumentException("Лекция не найдена");

            question.Id = _questions.Count > 0 ? _questions.Max(q => q.Id) + 1 : 1;
            question.LectureId = lectureId;
            question.CreatedAt = DateTime.UtcNow;
            question.Status = "draft";

            _questions.Add(question);
            return question;
        }

        public QuestionStatistics GetQuestionStats(int questionId)
        {
            var question = _questions.FirstOrDefault(q => q.Id == questionId);
            if (question == null)
                throw new ArgumentException("Вопрос не найден");

            var questionResponses = _responses.Where(r => r.QuestionId == questionId).ToList();
            var totalResponses = questionResponses.Count;

            var stats = new QuestionStatistics
            {
                QuestionId = questionId,
                Status = question.Status,
                TotalResponses = totalResponses,
                CorrectAnswer = question.CorrectAnswerIndex,
                StudentCount = 25, // Примерное количество студентов
                TimeLeft = Math.Max(0, question.DurationSeconds - (int)(DateTime.UtcNow - question.CreatedAt).TotalSeconds)
            };

            // Рассчет статистики для вопросов с вариантами ответов
            if (question.Options.Any() && question.CorrectAnswerIndex.HasValue)
            {
                var results = new Dictionary<int, int>();
                for (int i = 0; i < question.Options.Count; i++)
                {
                    results[i] = questionResponses.Count(r => r.SelectedOptionIndex == i);
                }
                stats.Results = results;

                var correctResponses = questionResponses.Count(r => r.IsCorrect);
                stats.CorrectPercentage = totalResponses > 0 ? (correctResponses * 100.0 / totalResponses) : 0;

                if (totalResponses > 0)
                {
                    stats.AverageResponseTimeSeconds = questionResponses.Average(r => r.ResponseTimeMs / 1000.0);
                }
            }

            return stats;
        }

        public QuestionResponse SubmitResponse(int questionId, QuestionResponse response)
        {
            var question = _questions.FirstOrDefault(q => q.Id == questionId);
            if (question == null || question.Status != "active")
                throw new ArgumentException("Вопрос неактивен или не найден");

            response.Id = _responses.Count > 0 ? _responses.Max(r => r.Id) + 1 : 1;
            response.QuestionId = questionId;
            response.RespondedAt = DateTime.UtcNow;

            // Проверка правильности ответа
            if (question.CorrectAnswerIndex.HasValue && response.SelectedOptionIndex.HasValue)
            {
                response.IsCorrect = response.SelectedOptionIndex == question.CorrectAnswerIndex;
            }

            _responses.Add(response);
            return response;
        }

        // ОДИН метод GetQuestionById (с обработкой исключений)
        public Question? GetQuestionById(int questionId)
        {
            try
            {
                return _questions.FirstOrDefault(q => q.Id == questionId);
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                Console.WriteLine($"Ошибка при получении вопроса: {ex.Message}");
                return null;
            }
        }

        // ДОБАВЛЯЕМ отсутствующий метод GetLectureQuestions
        public List<Question> GetLectureQuestions(int lectureId)
        {
            try
            {
                return _questions.Where(q => q.LectureId == lectureId).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении вопросов лекции: {ex.Message}");
                return new List<Question>();
            }
        }

        public Question? ActivateQuestion(int questionId)
        {
            var question = _questions.FirstOrDefault(q => q.Id == questionId);
            if (question != null)
            {
                question.Status = "active";
                question.CreatedAt = DateTime.UtcNow; // Сброс таймера
            }
            return question;
        }

        public Question? CompleteQuestion(int questionId)
        {
            var question = _questions.FirstOrDefault(q => q.Id == questionId);
            if (question != null)
            {
                question.Status = "completed";
            }
            return question;
        }
    }
}