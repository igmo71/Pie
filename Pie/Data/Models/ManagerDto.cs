namespace Pie.Data.Models
{
    public class ManagerDto
    {
        public Guid ManagerId { get; set; }
        public string? Name { get; set; }

        public static Manager MapToManager(ManagerDto dto)
        {
            Manager manager = new()
            {
                Id = dto.ManagerId,
                Name = dto.Name
            };

            return manager;
        }

        public static ManagerDto MapFromManager(Manager manager)
        {
            ManagerDto dto = new()
            {
                ManagerId = manager.Id,
                Name = manager.Name
            };

            return dto;
        }
    }
}
