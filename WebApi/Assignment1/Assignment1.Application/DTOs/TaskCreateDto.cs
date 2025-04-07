using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1.Application.DTOs
{
    public class TaskCreateDto
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
    }
}
