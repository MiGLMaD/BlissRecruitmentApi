using bliss.recruitment.api.Data;
using bliss.recruitment.api.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace bliss.recruitment.api.Controllers
{
    [ApiController]
    [SwaggerTag("Bliss Recruitment API is a simple API allowing consumers to view polls and vote in them.")]
    public class QuestionsController : Controller
    {
        private readonly QuestionsContext _context;

        public QuestionsController(QuestionsContext context)
        {
            _context = context;
        }

        [HttpGet("health")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(GenericResponse))]
        [SwaggerOperation(summary: "Get Health Status")]
        public IActionResult GetHealth()
        {
            try
            {
                if (_context.Questions.Count() == 0)
                    throw new Exception();

                return Ok(new GenericResponse { Status = "OK" });
            }
            catch (Exception)
            {
                return StatusCode(503, new GenericResponse { Status = "Service Unavailable. Please try again later." });
            }
        }

        [HttpGet("questions")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Question[]))]
        [SwaggerOperation(summary: "List All Questions")]
        public IActionResult GetQuestions([FromQuery(Name = "limit")][Required] int limit, [FromQuery(Name = "offset")][Required] int offset, [FromQuery(Name = "filter")] string filter = null)
        {
            var query = from q in _context.Questions
                        where string.IsNullOrEmpty(filter) || q.Name.ToLower().Contains(filter.ToLower())
                        select new Question
                        {
                            id = q.Id,
                            question = q.Name,
                            image_url = q.Image_Url,
                            thumb_url = q.Thumb_Url,
                            published_at = q.Published_At,
                            choices = (List<Choice>)(from c in _context.Choices
                                                     where c.QuestionId == q.Id 
                                                     select new Choice
                                                       {
                                                           choice = c.Name,
                                                           votes = c.Votes
                                                       })
                        };

            if (offset > 0)
                query = query.Skip((int)offset);
            if (limit > 0)
                query = query.Take((int)limit);

            return Ok(query.ToList());
        }

        [HttpGet("questions/{question_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Question))]
        [SwaggerOperation(summary: "Retrieve a Question")]
        public IActionResult GetQuestions([FromRoute] int question_id)
        {
            var query = from q in _context.Questions
                        where q.Id == question_id
                        select new Question
                        {
                            id = q.Id,
                            question = q.Name,
                            image_url = q.Image_Url,
                            thumb_url = q.Thumb_Url,
                            published_at = q.Published_At,
                            choices = (List<Choice>)(from c in _context.Choices
                                                     where c.QuestionId == q.Id
                                                     select new Choice
                                                     {
                                                         choice = c.Name,
                                                         votes = c.Votes
                                                     })
                        };

            return Ok(query.FirstOrDefault());
        }

        [HttpPost("questions")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Question))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GenericResponse))]
        [SwaggerOperation(summary: "Create a New Question", description: "You may create your own question using this action. It takes a JSON object containing a question and a collection of answers in the form of choices.")]
        public IActionResult PostQuestions([FromBody] PostQuestionRequest question)
        {
            if (!ModelState.IsValid)
                return BadRequest(new GenericResponse { Status = "Bad Request. All fields are mandatory." });

            var q = new Model.Data.Question() { 
                Name = question.question,
                Image_Url = question.image_url,
                Thumb_Url = question.thumb_url,
                Published_At = DateTime.Now,
            };
            _context.Questions.Add(q);
            _context.SaveChanges();
            foreach (var choice in question.choices)
            {
                var c = new Model.Data.Choice()
                {
                    Name = choice,
                    QuestionId = q.Id
                };
                _context.Choices.Add(c);
            }
            _context.SaveChanges();

            return new CreatedResult($"/questions/{q.Id}", GetQuestions(q.Id));
        }

        [HttpPut("questions/{question_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Question))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GenericResponse))]
        [SwaggerOperation(summary: "Update a Question", description: "You may update a question using this action. It takes a JSON object containing a question and a collection of answers in the form of choices.")]
        public IActionResult PutQuestions([FromRoute] int question_id, [FromBody] Question question)
        {
            if (!ModelState.IsValid)
                return BadRequest(new GenericResponse { Status = "Bad Request. All fields are mandatory." });

            var q = _context.Questions.FirstOrDefault(q => q.Id == question_id);
            q.Name = question.question;
            q.Image_Url = question.image_url;
            q.Thumb_Url = question.thumb_url;
            _context.SaveChanges();
            foreach (var choice in question.choices)
            {
                var c = _context.Choices.FirstOrDefault(c => c.QuestionId == q.Id && c.Name == choice.choice);
                if (c != null)
                {
                    c.Votes = choice.votes;
                }
            }
            _context.SaveChanges();

            return new CreatedResult($"/questions/{q.Id}", GetQuestions(q.Id));
        }

        [HttpPost("share")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GenericResponse))]
        [SwaggerOperation(summary: "Share")]
        public IActionResult PostShare([FromQuery(Name = "destination_email")][Required] string destinationEmail, [FromQuery(Name = "content_url")][Required] string contentUrl)
        {
            if (string.IsNullOrEmpty(destinationEmail) || string.IsNullOrEmpty(contentUrl))
                return BadRequest(new GenericResponse { Status = "Bad Request. Either destination_email not valid or empty content_url" });

            return Ok(new GenericResponse { Status = "OK" });
        }
    }
}
