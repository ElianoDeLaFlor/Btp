using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Btp.Models
{
	public class ModelDBContext:DbContext
	{
        public ModelDBContext() { }
        public DbSet<Users> Usersinfo { get; set; }

	}
}