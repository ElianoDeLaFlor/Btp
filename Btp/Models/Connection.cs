using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Btp.Models
{
    public class Connection
    {
        [Display(Name = "Nom d'utilisateur")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Entrez le Nom d'utilisateur")]
        [NotMapped]
        public string UserLogin { get; set; }

        [Display(Name = "Mot de passe")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Entrez le Mot de passe")]
        [NotMapped]
        public string Password { get; set; }
    }
}