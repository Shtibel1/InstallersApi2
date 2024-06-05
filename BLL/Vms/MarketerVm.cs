namespace BLL.Models
{
    public class MarketerVm
    {
        public Guid? Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
    }
}