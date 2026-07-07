
namespace StrategicviewBack.Models.DTO
{
 public class PermissionDto
 {
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Route { get; set; }

    public bool? IsParent { get; set; }

    public int? ParentPermissionId { get; set; }

    public string? Icon { get; set; }
    public List<PermissionDto> Children { get; set; } = new List<PermissionDto>();
 }

}
