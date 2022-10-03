using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;
using MVVMWPF.Model;
using Newtonsoft.Json;

namespace MVVMWPF.Services
{
    /// <summary>
    /// Интерфейс сохранения коллекции в файл.
    /// </summary>
    interface ISaver
    {
        /// <summary>
        /// Метод для сохранения объекта Book в файл.
        /// </summary>
        /// <param name="data">Коллекция для сохранения</param>
        /// <param name="filePath">Путь сохранения файла</param>
        void Save(IEnumerable<Book> data, string filePath);
        /// <summary>
        /// Метод настройки диалогового окна настройки
        /// </summary>
        /// <param name="saveFileDialog"></param>
        void SetupFileDialog(SaveFileDialog saveFileDialog);
    }
    
    class JsonSaver : ISaver
    {
        /// <summary>
        /// Метод реализующий интерфейс ISaver. Сохраняет коллекцию в Json файл
        /// </summary>
        /// <param name="data">Коллекция для сохранения</param>
        /// <param name="filePath">Путь сохранения файла</param>
        public void Save(IEnumerable<Book> data, string filePath)
        {
            string jsonString = JsonConvert.SerializeObject(data, Formatting.Indented);
            using (StreamWriter streamWriter = new StreamWriter(filePath, false))
            {
                streamWriter.Write(jsonString);
            }
        }
        /// <summary>
        /// Метод для настройки диалогового окна сохранения
        /// </summary>
        public void SetupFileDialog(SaveFileDialog saveFileDialog)
        {
            saveFileDialog.DefaultExt = "json";
            saveFileDialog.FileName = "save.json";
            saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
        }
    }
}
