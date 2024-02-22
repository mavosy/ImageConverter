using ImageToGrayscale.Services.Interfaces;
using System.Windows;

namespace ImageToGrayscale.Services
{
    public class MessageService : IMessageService
    {
        public void ShowMessage(string message, MessageType messageType = MessageType.None)
        {
            MessageBoxImage image;

            switch (messageType)
            {
                case MessageType.Information:
                    image = MessageBoxImage.Information;
                    break;
                case MessageType.Warning:
                    image = MessageBoxImage.Warning;
                    break;
                case MessageType.Error:
                    image = MessageBoxImage.Error;
                    break;
                default:
                    image = MessageBoxImage.None;
                    break;
            }
            MessageBox.Show(message, messageType.ToString(), MessageBoxButton.OK, image);
        }
    }
}