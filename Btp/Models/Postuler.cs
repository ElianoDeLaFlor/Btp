using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Btp.Models
{
	public class Postuler
	{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "N° de l'offre")]
        public int RecrutementId { get; set; }

        [StringLength(90, ErrorMessage = "Le texte est trop long")]
        [Display(Name = "Poste à occuper")]
        [Required(AllowEmptyStrings = false,ErrorMessage = "Entrez le poste à occuper")]
        public String PostOccupe { get; set; }

        [MaxLength(90, ErrorMessage = "Le nom est trop long")]
        [MinLength(3,ErrorMessage="Le nom est trop court")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Entrez le nom")]
        public String Nom { get; set; }


        [MaxLength(120, ErrorMessage = "Le prénom est trop long")]
        [MinLength(3, ErrorMessage = "Le prénom est trop court")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Entrez le prénom")]
        public String Prenom { get; set; }

        [Display(Name ="Votre cv")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Selectionnez votre CV")]
        public String CheminCv { get; set; }

        [Display(Name = "Votre lettre de motivation")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Selectionnez votre Lettre de motivation")]
        public String Lettre { get; set; }

        [Display(Name = "Vos attestations")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Selectionnez au moins une de vos attestations")]
        public String Attestation { get; set; }


        [Display(Name ="Date d'envoi")]
        public DateTime PostTime { get; set; }

        [Display(Name ="N° de l'offre")]
        [NotMapped]
        String TypePost {
            get {
                if (RecrutementId == 0)
                    return "Spontané";
                else
                    return RecrutementId.ToString();
            }
        }
        
    }
}