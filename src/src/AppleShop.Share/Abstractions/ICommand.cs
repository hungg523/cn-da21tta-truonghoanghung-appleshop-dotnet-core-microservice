using AppleShop.Share.Shared;
using MediatR;

namespace AppleShop.Share.Abstractions
{
    public interface ICommand : IRequest<Result<object>>
    {
    }
}