using Shared.Contracts.Interfaces;

namespace IdentityContext.Contracts.Commands.Register
{
    public record RegisterRequest : IResultRequest<Guid>
    {

    }
}
