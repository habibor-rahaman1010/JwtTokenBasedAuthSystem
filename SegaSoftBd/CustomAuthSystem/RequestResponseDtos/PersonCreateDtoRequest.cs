using System.ComponentModel.DataAnnotations;

namespace CustomAuthSystem.RequestResponseDtos
{
    public class PersonCreateDtoRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Gender { get; set; } = string.Empty;

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public List<string> PhoneNumbers { get; set; }

        [Required]
        public List<string> Addresses { get; set; }

        [Required]
        public string Nationality { get; set; } = string.Empty;

        public PersonCreateDtoRequest()
        {
            PhoneNumbers = new List<string>();
            Addresses = new List<string>();
        }
    }
}
