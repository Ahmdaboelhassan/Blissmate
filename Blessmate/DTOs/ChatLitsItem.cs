using System.ComponentModel.DataAnnotations;

namespace Blessmate;

public class ChatLitsItem
{   
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhotoUrl { get; set; }
    public string LastMessage { get; set; }
    public DateTime? SendIn { get; set; }
}
