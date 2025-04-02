using System.ComponentModel.DataAnnotations;

namespace Assignment2.Model.RookieDto
{
    
    public class RookieInputDto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [MaxLength(20, ErrorMessage = "First name cannot exceed 20 characters")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "First name must be alphabetic and can contain spaces")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [MaxLength(20, ErrorMessage = "Last name cannot exceed 20 characters")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Last name must be alphabetic and can contain spaces")]
        public string LastName { get; set; }

        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [MaxLength(10, ErrorMessage = "Phone number cannot exceed 10 characters")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Phone number must be numeric")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Birth place is required")]
        [MaxLength(100, ErrorMessage = "Birth place cannot exceed 100 characters")]
        public string BirthPlace { get; set; }
        public bool IsGraduated { get; set; }
    }
}
