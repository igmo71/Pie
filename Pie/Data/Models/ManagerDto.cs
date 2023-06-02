namespace Pie.Data.Models
{
    public class ManagerDto
    {
        public Guid? ManagerId { get; set; }
        public string? Name { get; set; }

        public static Manager? MapToManager(ManagerDto dto)
        {
            if (dto.ManagerId == null)
                return default;

            Manager manager = new()
            {
                Id = (Guid)dto.ManagerId,
                Name = dto.Name,
                Active = true
            };

            return manager;
        }
    }
}
