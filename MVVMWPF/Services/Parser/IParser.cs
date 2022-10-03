using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Microsoft.Win32;
using MVVMWPF.Model;
using Newtonsoft.Json;

namespace MVVMWPF.Services
{
    /// <summary>
    /// Интерфейс парсинга файла в объект DataTable.
    /// </summary>
    interface IParser
    {
        IEnumerable<Book> GetData(string filePath);
        void SetupFileDialog(OpenFileDialog openFileDialog);
    }

    class JsonParser : IParser
    {
        /// <summary>
        /// Метод реализующий интерфейс IParser. Преобразует файл Json в коллекцию
        /// </summary>
        /// <param name="filePath">Путь к выбранному файлу</param>
        /// <returns>Возвращает объект коллекции</returns>
        public IEnumerable<Book> GetData(string filePath)
        {
            IEnumerable<Book> data;
            string jsonstring;
            using (StreamReader streamReader = new StreamReader(filePath))
            {
                jsonstring = streamReader.ReadToEnd();
            }
            data = JsonConvert.DeserializeObject<IEnumerable<Book>>(jsonstring);
            return data;
        }
        /// <summary>
        /// Метод для настройки диалогового окна выбора файла
        /// </summary>
        public void SetupFileDialog(OpenFileDialog openFileDialog)
        {
            openFileDialog.FileName = "";
            openFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
        }
    }
}
