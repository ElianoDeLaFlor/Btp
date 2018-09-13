using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Btp.Models
{
    public class Recrutement
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(90,ErrorMessage ="Le texte est trop long")]
        [Display(Name ="Poste à occuper")]
        public String Post { get; set; }

        [Display(Name = "Date de publication")]
        public DateTime DatePublication { get; set; }

        [Display(Name ="Date limite")]
        public DateTime DateExpiration { get; set; }

        [StringLength(90)]
        [Display(Name ="Niveau d'étude")]
        public String Niveau { get; set; }

        [Display(Name ="Lieu de dépot")]
        [StringLength(90)]
        public string LieuDepot { get; set; }

        [Display(Name ="Type d'offre")]
        public TypeOffre Type { get; set; }

        [StringLength(7000,ErrorMessage ="Le text est trop long")]
        public string Description { get; set; }

        public virtual ICollection<Postuler> GetPostuler { get; set; }

    }
    public enum TypeOffre
    {
        Emploi=0,
        Stage=1

    }
}