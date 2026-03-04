using System.ComponentModel.DataAnnotations;

namespace VertexUniversity.Models
{
    public class Event
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public string Time { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string Organizer { get; set; } = string.Empty;
        public string ContactInfo { get; set; } = string.Empty;
        public int Capacity { get; set; }
    }

    public class RSVP
    {
        [Required(ErrorMessage = "Name is required")]
        public string StudentName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Student ID is required")]
        public string StudentId { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; } = string.Empty;
        
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }

    public class Feedback
    {
        public string EventId { get; set; } = string.Empty;
        public string StudentName { get; set; } = "Anonymous";
        
        [Range(1, 5, ErrorMessage = "Please select a rating between 1 and 5")]
        public int Rating { get; set; }
        
        [Required(ErrorMessage = "Your feedback is important, please provide a comment.")]
        [MinLength(5, ErrorMessage = "Comment is too short (min 5 characters).")]
        public string Comment { get; set; } = string.Empty;
        
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }

    public class RegistrationModel
    {
        [Required(ErrorMessage = "Full Name is required")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Student ID is required")]
        [RegularExpression(@"^VU-\d{4}-\d{4}$", ErrorMessage = "Student ID must follow format VU-YYYY-NNNN")]
        public string StudentId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Faculty/Department is required")]
        public string Department { get; set; } = string.Empty;

        [Required(ErrorMessage = "Year of Study is required")]
        public string StudyYear { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Invalid phone number")]
        public string ContactNumber { get; set; } = string.Empty;

        public string SpecialInquiries { get; set; } = string.Empty;
    }

    public class LoginModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;
    }
}
