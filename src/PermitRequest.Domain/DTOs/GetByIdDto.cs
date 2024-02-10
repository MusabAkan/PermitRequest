namespace PermitRequest.Domain.DTOs
{
    public record GetByIdRequestDto(int skip, int take, Guid userId);
}
