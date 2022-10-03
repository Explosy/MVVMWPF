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
    /// Интерфейс парсинга файла в коллекцию.
    /// </summary>
    interface IParser
    {
        IEnumerable<T> GetData<T>(string filePath);
        void SetupFileDialog(OpenFileDialog openFileDialog);
    }

    class JsonParser : IParser
    {
        /// <summary>
        /// Метод реализующий интерфейс IParser. Преобразует файл Json в коллекцию
        /// </summary>
        /// <param name="filePath">Путь к выбранному файлу</param>
        /// <returns>Возвращает интерфейсную ссылку IEnumerable</returns>
        public IEnumerable<T> GetData<T>(string filePath)
        {
            IEnumerable<T> data;
            string jsonstring;
            using (StreamReader streamReader = new StreamReader(filePath))
            {
                jsonstring = streamReader.ReadToEnd();
            }
            data = JsonConvert.DeserializeObject<IEnumerable<T>>(jsonstring);
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
