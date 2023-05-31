using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abdulrhmaan.News.SQlServer;

    public class Category
    {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string UserId { get; set; }
    public virtual User USer { get; set; }
    public DateTime InsertedAt { get; set; }

}
