namespace ImageToGrayscale.Services.Interfaces
{
    public interface IDialogService
    {
        string OpenFileDialog(string filters);
        string SaveFileDialog(string filters);
    }
}