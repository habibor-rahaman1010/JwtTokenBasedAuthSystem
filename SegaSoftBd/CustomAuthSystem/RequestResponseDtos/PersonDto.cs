namespace CustomAuthSystem.RequestResponseDtos
{
    public class PersonDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<string> PhoneNumbers { get; set; }
        public List<string> Addresses { get; set; }
        public string Nationality { get; set; } = string.Empty;
        public string CreatedDate { get; set; } = string.Empty;
        public string LastModifiedDate { get; set; } = string.Empty;

        public PersonDto()
        {
            PhoneNumbers = new List<string>();
            Addresses = new List<string>();
        }
    }
}
