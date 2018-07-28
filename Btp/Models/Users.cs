using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Btp.Models
{
    public class Users
    {

        public int ID {get;set;}
        [Display(Name ="Nom")]
        [StringLength(20,ErrorMessage ="La longueur du nom doit être compris entre 3-20 caractères",MinimumLength =3)]
        public string Name { get; set; }

        [StringLength(30, ErrorMessage = "La longueur du nom doit être compris entre 3-30 caractères", MinimumLength = 3)]
        [Display(Name="Prénom")]
        [RegularExpression("[a-zA-Zéè_-]*")]
        public string LastName { get; set; }

        [Display(Name ="Mot de passe")]
        public string Password { get; set; }

        [Display(Name="Nom d'utilisateur")]
        public string Login { get; set; }

        public Role role { get; set; }
    }

    public enum Role
    {
        Admin=0,
        SubAdmin=1,
        CvAdmin=2
    }
}