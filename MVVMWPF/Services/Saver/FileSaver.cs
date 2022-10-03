using Microsoft.Win32;
using MVVMWPF.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace MVVMWPF.Services
{
    /// <summary>
    /// Класс реализующий сохранение файла.
    /// </summary>
    internal class FileSaver
    {
        private SaveFileDialog saveFileDialog;
        /// <summary>
        /// Поле, содержащее текущую стратегию сохранения файла.
        /// </summary>
        public ISaver SaveStrategy { private get; set; }

        public FileSaver()
        {
            saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.CurrentDirectory;
            saveFileDialog.AddExtension = true;
        }
        /// <summary>
        /// Метод, создающий диалоговое окно сохранения, а также запускающий реализацию текущей стратегии сохранения из SaveStrategy.
        /// </summary>
        /// <param name="data"></param>
        public void Save (IEnumerable<Book> data)
        {
            SaveStrategy.SetupFileDialog(saveFileDialog);
            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                SaveStrategy.Save(data, filePath);
            }
        }
    }

    
}
