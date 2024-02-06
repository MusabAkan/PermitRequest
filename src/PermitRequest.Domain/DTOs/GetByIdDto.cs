namespace PermitRequest.Domain.DTOs
{
    public record GetByIdDto(int skip, int take, Guid userId);
}
