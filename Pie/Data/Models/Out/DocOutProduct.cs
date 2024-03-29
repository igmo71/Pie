﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pie.Data.Models.Out
{
    public class DocOutProduct : DocProduct
    {
        public Guid DocId { get; set; }
        public DocOut? Doc { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Причину изменения нужно указать")]
        public Guid? ChangeReasonId { get; set; }
    }
}
