using System.ComponentModel;

namespace Pie.Data.Models.Out
{
    public class QueueOut : Queue
    {
        // Отгрузить До:

        [DefaultValue(0)]
        public int Days { get; set; }

        [DefaultValue(0)]
        public int Hours { get; set; }

        [DefaultValue(0)]
        public int Minutes { get; set; }

        public TimeOnly ConcreteTime { get; set; } = new TimeOnly();
    }
}
