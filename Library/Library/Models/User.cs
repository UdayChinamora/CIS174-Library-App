using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
	public class User : IdentityUser
	{
		[NotMapped]
		public List<string> RoleNames { get; set; }
	}
}
