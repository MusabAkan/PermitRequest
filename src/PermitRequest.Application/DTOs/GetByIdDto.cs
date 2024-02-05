namespace PermitRequest.Application.DTOs
{
    public record GetByIdDto(int skip, int take, Guid userId);
}
