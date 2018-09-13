using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Btp.Models
{
	public class ModelDBContext:DbContext
	{
        public ModelDBContext():base("BTPdb") { }
        public DbSet<Users> Usersinfo { get; set; }
        public DbSet<ImageManager> ImageManagerinfo { get; set; }
        public DbSet<Recrutement> Recrutementinfo { get; set; }
        public DbSet<Postuler> Postulerinfo { get; set; }
    }
}