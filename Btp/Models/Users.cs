using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Btp.Models
{
    public class Users
    {

        public int ID {get;set;}
        [Display(Name ="Nom")]
        [Required(AllowEmptyStrings =false,ErrorMessage ="Le nom est requis")]
        [StringLength(20,ErrorMessage ="La longueur du nom doit être comprise entre 3-20 caractères",MinimumLength =3)]
        public string Name { get; set; }

        [StringLength(30, ErrorMessage = "La longueur du prenom doit être comprise entre 3-30 caractères", MinimumLength = 3)]
        [Display(Name="Prénom")]
        [RegularExpression("[a-zA-Zéè_-]*")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Le prénom est requis")]
        public string LastName { get; set; }

        [Display(Name="Nom d'utilisateur")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Le nom d'utilisateur est requis")]
        public string Login { get; set; }

        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Le mot de passe est requis")]
        [Display(Name = "Mot de passe")]
        [StringLength(10, ErrorMessage = "La longueur du mot de passe doit être comprise entre 4-10 caractères", MinimumLength = 4)]
        public string Password { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le mot de passe")]
        [Compare("Password",ErrorMessage ="La confirmation est incorrecte")]
        public string ConfirmPassword { get; set; }

        [Display(Name ="Rôle")]
        public Role UserRole { get; set; }
    }

    public enum Role
    {
        Admin=0,
        SubAdmin=1,
        CvAdmin=2
    }
}