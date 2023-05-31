using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abdulrhmaan.News.SQlServer;

public class Item
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string UserId { get; set; }
    public virtual User User { get; set; }
    public int CategoryId { get; set; }
    public virtual Category Category { get; set; }
    public DateTime InsertedAt { get; set; }
    public string? Status { get; set; }
    public bool? IsDeleted { get; set; }


}


