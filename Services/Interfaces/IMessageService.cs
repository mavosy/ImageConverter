namespace ImageToGrayscale.Services.Interfaces
{
    public interface IMessageService
    {
        void ShowMessage(string message, MessageType messageType = MessageType.None);
    }
}