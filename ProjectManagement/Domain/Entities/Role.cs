namespace Domain.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string RoleDescription { get; set; }
        
        public int AssignId { get; set; }
        public Assign Assign { get; set; }
    }
}