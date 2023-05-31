using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abdulrhmaan.News.SQlServer;

public class User : IdentityUser
{
    public override required string Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string City { get; set; }

}

