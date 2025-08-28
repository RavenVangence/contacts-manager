using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace GeniusContactManager.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters")]
        [RegularExpression(@"^[a-zA-Z\s'.-]+$", ErrorMessage = "Name can only contain letters, spaces, hyphens, apostrophes, and dots")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Surname is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Surname must be between 2 and 50 characters")]
        [RegularExpression(@"^[a-zA-Z\s'.-]+$", ErrorMessage = "Surname can only contain letters, spaces, hyphens, apostrophes, and dots")]
        public string Surname { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Phone number must be between 10 and 15 characters")]
        [RegularExpression(@"^[\d\s\-\+\(\)]+$", ErrorMessage = "Phone number can only contain digits, spaces, hyphens, plus signs, and parentheses")]
        public string PhoneNumber { get; set; } = string.Empty;

        public bool Used { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? ModifiedDate { get; set; }

        // Display properties for UI
        public string FullName => $"{Name} {Surname}";

        public string DisplayText => $"{FullName} - {PhoneNumber}";

        // Validation method for custom business logic
        public List<string> ValidateContact()
        {
            var errors = new List<string>();

            // Validate Name
            if (string.IsNullOrWhiteSpace(Name))
                errors.Add("Name is required");
            else if (Name.Trim().Length < 2)
                errors.Add("Name must be at least 2 characters long");
            else if (Name.Trim().Length > 50)
                errors.Add("Name cannot exceed 50 characters");
            else if (!Regex.IsMatch(Name.Trim(), @"^[a-zA-Z\s'.-]+$"))
                errors.Add("Name can only contain letters, spaces, hyphens, apostrophes, and dots");

            // Validate Surname
            if (string.IsNullOrWhiteSpace(Surname))
                errors.Add("Surname is required");
            else if (Surname.Trim().Length < 2)
                errors.Add("Surname must be at least 2 characters long");
            else if (Surname.Trim().Length > 50)
                errors.Add("Surname cannot exceed 50 characters");
            else if (!Regex.IsMatch(Surname.Trim(), @"^[a-zA-Z\s'.-]+$"))
                errors.Add("Surname can only contain letters, spaces, hyphens, apostrophes, and dots");

            // Validate Phone Number (now required)
            if (string.IsNullOrWhiteSpace(PhoneNumber))
            {
                errors.Add("Phone number is required");
            }
            else
            {
                var cleanPhone = PhoneNumber.Trim();
                if (cleanPhone.Length < 10)
                    errors.Add("Phone number must be at least 10 characters long");
                else if (cleanPhone.Length > 15)
                    errors.Add("Phone number cannot exceed 15 characters");
                else if (!Regex.IsMatch(cleanPhone, @"^[\d\s\-\+\(\)]+$"))
                    errors.Add("Phone number can only contain digits, spaces, hyphens, plus signs, and parentheses");

                // Check if phone number has at least 10 digits
                var digitCount = Regex.Matches(cleanPhone, @"\d").Count;
                if (digitCount < 10)
                    errors.Add("Phone number must contain at least 10 digits");
                else if (digitCount > 15)
                    errors.Add("Phone number cannot contain more than 15 digits");
            }

            return errors;
        }
    }
}
