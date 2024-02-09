using Ardalis.SharedKernel;

namespace PermitRequest.Domain.Entities.Base
{
    public abstract class BaseEntity : EntityBase
    {
        public new Guid Id { get; set; }
        /*Böyle yapmamızın sebeni Evet EntityBase de Guid ayarlanabiliryor(Örneğin EntityBase<Guid>) Fakat IDomainEventDispatcher sadece EntityBase istiyor.
         Ardalis.SharedKernel kütüphanesinde inceleyebiliriz. Kaynak https://github.com/ardalis/Ardalis.SharedKernel Buyüzden ID new keywordü ile eziyorum
         */

    }
}
