namespace App.Shared.Messages
{
    public interface IMessage<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }
    }
}
