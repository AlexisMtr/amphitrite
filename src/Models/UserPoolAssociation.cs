namespace Amphitrite.Models
{
    public class UserPoolAssociation
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Pool Pool { get; set; }
    }
}
