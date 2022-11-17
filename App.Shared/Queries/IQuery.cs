using MediatR;

namespace Object.Application.Queries
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {

    }
}