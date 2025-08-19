using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using RobotAPI.Data;       

namespace Robot.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoodEntriesController : ControllerBase
    {
        private readonly RobotsDbContext _context;

        public MoodEntriesController(RobotsDbContext context)
        {
            _context = context;
        }

        // GET: api/moodentries
        // Hämtar alla sparade humörposter, sorterade med senaste först
        [HttpGet]
        public async Task<ActionResult<List<MoodEntry>>> GetMoodEntries()
        {
            var moods = await _context.MoodEntries
                .AsNoTracking()
                .OrderByDescending(m => m.Tidpunkt)
                .ToListAsync();

            if (moods.Count == 0)
            {
                return NotFound("Inga humörposter sparade än.");
            }

            return Ok(moods);
        }

        // POST: api/moodentries
        // Sparar en ny humörpost
        [HttpPost]
        public async Task<ActionResult<MoodEntry>> PostMoodEntry(MoodEntry moodEntry)
        {
            if (moodEntry == null || string.IsNullOrWhiteSpace(moodEntry.Emoji))
            {
                return BadRequest("Humörposten är ogiltig.");
            }

            // Sätt tidpunkt för posten
            moodEntry.Tidpunkt = DateTime.UtcNow;

            _context.MoodEntries.Add(moodEntry);
            await _context.SaveChangesAsync();

            // Returnera skapad post med 201-status
            return CreatedAtAction(nameof(GetMoodEntries), new { id = moodEntry.Id }, moodEntry);
        }
    }
}
