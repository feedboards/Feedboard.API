namespace Feedboard.Contracts.DTOs.Response;

public class DeleteDto
{
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsSoftDeleted { get; set; }
}
