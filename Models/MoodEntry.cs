using System;
using System.ComponentModel.DataAnnotations;

namespace RobotAPI.Data
{
    public class MoodEntry
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Emoji { get; set; }

        [Required]
        public string Text { get; set; }

        public DateTime Tidpunkt { get; set; }  // Tidpunkt när posten skapades
    }
}
