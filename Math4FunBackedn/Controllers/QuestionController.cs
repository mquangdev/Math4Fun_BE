﻿using Math4FunBackedn.DTO;
using Math4FunBackedn.Entities;
using Math4FunBackedn.Repositories.LessonRepo;
using Math4FunBackedn.Repositories.QuestionRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Math4FunBackedn.Controllers
{
    [Authorize]
    [Route("Question")]
    public class QuestionController : Controller
    {
        private IQuestionRepository _questionRepository;
        public QuestionController(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddQuestion([FromBody] AddQuestionDTO iAdd)
        {
            try
            {
                return Ok(await _questionRepository.Add(iAdd));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        [HttpGet("Detail")]
        public async Task<IActionResult> DetailQuestion(Guid questionId)
        {
            try
            {
                return Ok(await _questionRepository.DetailQuestion(questionId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        [HttpDelete("Remove")]
        public async Task<IActionResult> RemoveQuestion(Guid questionId)
        {
            try
            {
                return Ok(await _questionRepository.Remove(questionId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        [HttpPost("Update")]
        public async Task<IActionResult> UpdateQuestion([FromBody] UpdateQuestionDTO iUpdate)
        {
            try
            {
                return Ok(await _questionRepository.Update(iUpdate));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        [HttpPost("AddQuestionToDb")] 
        public async Task<IActionResult> AddQuestionToDb([FromBody] AddQuestionToDbDTO[] iQuestionList)
        {
            try
            {

                return Ok(await _questionRepository.AddQuestionToDb(iQuestionList));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
