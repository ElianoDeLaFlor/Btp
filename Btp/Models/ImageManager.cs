using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Btp.Models
{
    public class ImageManager
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(120,ErrorMessage ="Le chemin vers le fichier est trop long")]
        public String Chemin { get; set; }

        [StringLength(256,ErrorMessage ="La description est trop longue")]
        public String Description { get; set; }

        [StringLength(50,ErrorMessage ="Le tire est trop long")]
        public String Title { get; set; }
    }
}