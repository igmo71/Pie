using System.ComponentModel.DataAnnotations;

namespace Pie.Admin.Api.Modules.Nginx
{
    class NginxConfig
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string? BaseAddress { get; set; }

        [MaxLength(100)]
        public string? Uri { get; set; }

        public bool SecLayer { get; set; }

        public DateTime DateTime { get; set; }
    }
}
