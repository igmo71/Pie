using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pie.Data.Models.In
{
    public class DocInProduct : DocProduct
    {
        public Guid DocId { get; set; }
        public DocIn? Doc { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Причину изменения нужно указать")]
        public Guid? ChangeReasonId { get; set; }
        
        [NotMapped]
        public ChangeReasonIn? ChangeReason { get; set; }
    }
}
