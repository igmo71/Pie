using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Pie.Data.Models.Out
{
    public class QueueOut : Queue
    {
        // Отгрузить До:

        [DefaultValue(0)]
        [Display(Name = "Дни")]
        public int Days { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Часы")]
        public int Hours { get; set; }

        [DefaultValue(0)]

        [Display(Name = "Минуты")]
        public int Minutes { get; set; }

        [Display(Name = "Конкретное время")]
        public TimeOnly ConcreteTime { get; set; } = new TimeOnly();
    }
}
