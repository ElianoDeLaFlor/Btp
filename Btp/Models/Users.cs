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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID {get;set;}
        [Display(Name ="Nom")]
        [RegularExpression("[a-zA-Zéè_-]*", ErrorMessage = "Le Nom ne doit pas contenir que des chiffres")]
        [Required(AllowEmptyStrings =false,ErrorMessage ="Le nom est requis")]
        [StringLength(20,ErrorMessage ="La longueur du nom doit être comprise entre 3-20 caractères",MinimumLength =3)]
        public string Name { get; set; }



        [StringLength(30, ErrorMessage = "La longueur du prenom doit être comprise entre 3-30 caractères", MinimumLength = 3)]
        [Display(Name="Prénom")]
        [RegularExpression("[a-zA-Zéè_-]*",ErrorMessage ="Le prénom ne doit pas contenir que des chiffres")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Le prénom est requis")]
        public string LastName { get; set; }



        [Display(Name="Nom d'utilisateur")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Le nom d'utilisateur est requis")]
        [MinLength(4,ErrorMessage ="Le nom d'utilisateur doit être au moins de 4 caractères")]
        [StringLength(15,ErrorMessage ="Le nom d'utilisateur doit être au maximun de 15 caractères")]
        public string Login { get; set; }




        [Display(Name ="Rôle")]
        [Required]
        public Role UserRole { get; set; }




        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Le mot de passe est requis")]
        [Display(Name = "Mot de passe")]
        [MinLength(4,ErrorMessage ="Le mot de passe doit être de 4 caractères au minimum")]
        [RegularExpression("[a-zA-Z0-9]*$", ErrorMessage = "Le format du mot de passe est incorrect")]
        [StringLength(256, ErrorMessage = "Le mot de passe est trop long")]
        public string Password { get; set; }
        



        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le mot de passe")]
        [Compare("Password", ErrorMessage = "La confirmation est incorrecte")]
        [NotMapped]
        [StringLength(256,ErrorMessage ="Le mot de passe est trop long")]
        [MinLength(4,ErrorMessage ="Le mot de passe doit être au moins de 4 caractères")]
        public string ConfirmPassword { get; set; }
    }

    public enum Role
    {
        Administrateur=0,
        Sous_Administrateur=1,
        Cv_Administrateur=2
    }
}