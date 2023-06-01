namespace Pie.Data.Models
{
    public class PartnerDto
    {
        public Guid? PartnerId { get; set; }
        public string? Name { get; set; }

        public static Partner? MapToPartner(PartnerDto dto)
        {
            if (dto.PartnerId == null)
                return default;

            Partner partner = new()
            {
                Id = (Guid)dto.PartnerId,
                Name = dto.Name
            };

            return partner;
        }
    }
}
