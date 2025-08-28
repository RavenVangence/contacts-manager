using System.ComponentModel.DataAnnotations;

namespace GeniusContactManager.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Surname { get; set; } = string.Empty;

        [StringLength(15)]
        public string? PhoneNumber { get; set; }

        public bool Used { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? ModifiedDate { get; set; }

        // Display properties for UI
        public string FullName => $"{Name} {Surname}";

        public string DisplayText => $"{FullName} - {PhoneNumber}";
    }
}
