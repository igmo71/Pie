namespace Pie.Data.Models
{
    public class PartnerDto
    {
        public Guid PartnerId { get; set; }
        public string? Name { get; set; }

        public static Partner MapToPartner(PartnerDto dto)
        {
            Partner partner = new()
            {
                Id = dto.PartnerId,
                Name = dto.Name
            };

            return partner;
        }

        public static PartnerDto MapFromPartner(Partner partner)
        {
            PartnerDto dto = new()
            {
                PartnerId = partner.Id,
                Name = partner.Name
            };

            return dto;
        }
    }
}
