namespace Blessmate.Models
{
    public class Message
    {
        public string Id {get; set;}
        public string Content { get; set; }
        public DateTime? SendIn { get; set; }
        public int SenderId { get; set; }
        public int ReciverId { get; set; }
        public ApplicationUser Sender { get; set; }
        public ApplicationUser Reciver { get; set; }

    }
}