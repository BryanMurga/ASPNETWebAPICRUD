using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using TestApi9A.models;

namespace TestApi9A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CommentController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public CommentController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var listComments = await _context.Comments.ToListAsync();
            if (listComments == null || listComments.Count == 0)
            {
                return NoContent();
            }
            return Ok(listComments);
        }

        [HttpGet("Show")]
        public async Task<IActionResult> Show(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment);
        }

        [HttpPost("Store")]
        public async Task<HttpStatusCode> Store([FromBody] Comment comment)
        {
            if (comment == null)
            {
                return HttpStatusCode.BadRequest;
            }
            _context.Add(comment);
            await _context.SaveChangesAsync();
            return HttpStatusCode.Created;
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] Comment comment)
        {
            if (comment == null)
            {
                return BadRequest(); //Error code 400
            }
            var entity = await _context.Comments.FindAsync(comment.Id);
            if (entity == null)
            {
                return NotFound(); //Error code 404
            }
            entity.Title = comment.Title;
            entity.Description = comment.Description;
            entity.Author = comment.Author;
            entity.CreatedAt = comment.CreatedAt;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("Destroy")]
        public async Task<IActionResult> Destroy(int id)
        {
            var producto = await _context.Comments.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            _context.Comments.Remove(producto);
            await _context.SaveChangesAsync();
            return Ok();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(builder =>
                builder.AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod());

            // Resto de la configuración del middleware
        }



    }


}