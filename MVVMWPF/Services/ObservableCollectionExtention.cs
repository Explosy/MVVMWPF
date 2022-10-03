using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMWPF.Services
{
    internal static class ObservableCollectionExtention
    {
        /// <summary>
        /// Метод расширения - добавляет в ObservableCollection заданную коллекцию реализующуй интерфейс IEnumerable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="insertData"></param>
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> insertData)
        {
            foreach (var item in insertData)
            {
                collection.Add(item);
            }
        }
    }
}
