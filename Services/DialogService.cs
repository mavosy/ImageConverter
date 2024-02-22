using ImageToGrayscale.Services.Interfaces;
using Microsoft.Win32;

namespace ImageToGrayscale.Services
{
    class DialogService : IDialogService
    {
        public string OpenFileDialog(string filter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog { Filter = filter };
            return openFileDialog.ShowDialog() == true ? openFileDialog.FileName : null;

        }

        public string SaveFileDialog(string filter)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog { Filter = filter };
            return saveFileDialog.ShowDialog() == true ? saveFileDialog.FileName : null;
        }
    }
}
