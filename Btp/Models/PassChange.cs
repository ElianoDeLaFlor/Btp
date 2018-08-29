using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace Btp.Models
{
    public class PassChange
    {

        [Display(Name ="Nom d'utilisateur")]
        [NotMapped]
        public string UserLogin { get; set; }


        [Display(Name ="Ancien mot de passe")]
        [DataType(DataType.Password)]
        [NotMapped]
        public string OldPass { get; set; }

        
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Le mot de passe est requis")]
        [Display(Name = "Nouveau mot de passe")]
        [MinLength(4, ErrorMessage = "Le mot de passe doit être de 4 caractères au minimum")]
        [RegularExpression("[a-zA-Z0-9]*$", ErrorMessage = "Le format du mot de passe est incorrect")]
        [StringLength(256, ErrorMessage = "Le mot de passe est trop long")]
        [NotMapped]
        public string NewPass { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le mot de passe")]
        [Compare("NewPass", ErrorMessage = "La confirmation est incorrecte")]
        [NotMapped]
        public string ConfPass { get; set; }

       

    }
}