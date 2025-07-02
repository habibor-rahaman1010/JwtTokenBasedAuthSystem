namespace CustomAuthSystem.DomainEntities
{
    public class Person : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<string> PhoneNumbers { get; set; }
        public List<string> Addresses { get; set; }
        public string Nationality { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }


        public Person()
        {
            PhoneNumbers = new List<string>();
            Addresses = new List<string>();
        }
    }
}
