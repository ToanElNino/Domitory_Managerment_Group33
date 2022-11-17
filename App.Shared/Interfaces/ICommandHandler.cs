using App.Shared.Commands;

namespace App.Shared.Interfaces
{
    public interface ICommandHandler<in T>  where T : CommandBase
    {
        Result Handle(T command);
    }
}