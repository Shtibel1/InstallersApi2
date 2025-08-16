using DAL.Entities;

namespace DAL.Abstracts
{
    public class ServiceProvider
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
        public Guid IdentityId { get; set; }

        public List<Company> Companies { get; set; } = new List<Company>();
    }
}
