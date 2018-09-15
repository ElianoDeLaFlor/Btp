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

        [MaxLength(90, ErrorMessage = "Le nom est trop long")]
        [MinLength(3,ErrorMessage="Le nom est trop court")]
        public String Nom { get; set; }


        [MaxLength(120, ErrorMessage = "Le prenom est trop long")]
        [MinLength(3, ErrorMessage = "Le prenom est trop court")]
        public String Prenom { get; set; }

        [Display(Name ="Votre cv")]
        public String CheminCv { get; set; }

        [Display(Name = "Votre Lettre de motivation")]
        public String Lettre { get; set; }

        [Display(Name = "Votre Attestation")]
        public String Attestation { get; set; }

        public virtual Recrutement GetRecrutement { get; set; }
        
    }
}