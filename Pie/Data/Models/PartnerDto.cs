namespace Pie.Data.Models
{
    public class PartnerDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }

        public static Partner MapToPartner(PartnerDto dto)
        {
            Partner partner = new()
            {
                Id = dto.Id,
                Name = dto.Name
            };

            return partner;
        }

        public static PartnerDto MapFromPartner(Partner partner)
        {
            PartnerDto dto = new()
            {
                Id = partner.Id,
                Name = partner.Name
            };

            return dto;
        }
    }
}
