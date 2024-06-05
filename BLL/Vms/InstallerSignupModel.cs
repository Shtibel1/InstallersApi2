namespace BLL.Models
{
    public class CreateServiceProviderVm
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public List<Guid> Categories { get; set; }
        public Guid? IdentityId { get; set; }
    }
}
