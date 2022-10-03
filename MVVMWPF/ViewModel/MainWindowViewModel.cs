using MVVMWPF.Model;
using MVVMWPF.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace MVVMWPF.ViewModel
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        private Book selectedBook;
        public ObservableCollection<Book> Books { get; set; }
        
        public Book SelectedBook
        {
            get { return selectedBook; }
            set
            {
                selectedBook = value;
                OnPropertyChanged("SelectedBook");
            }
        }

        private FileSaver fileSaver;
        private FileParser fileParser;
        public MainWindowViewModel()
        {
            fileSaver = new FileSaver() { SaveStrategy = new JsonSaver() };
            fileParser = new FileParser() { ParseStrategy = new JsonParser() };

            Books = new ObservableCollection<Book>();

            #region Тестовые данные
            //{
            //    new Book {Title="WPF Unleashed", Author="Adam Natan", Year=2012 },
            //    new Book {Title="F# for Machine Learning", Author="Sudipta Mukherjee", Year=2016 },
            //    new Book {Title="F# for Fun and Profit", Author="Scott Wlaschin", Year=2015 },
            //    new Book {Title="Learning C# by Developing Games with Unity 3D", Author="Terry Norton", Year=2013 }
            //};
            #endregion
        }
        #region Command
        #region AddCommand
        private BaseCommand addCommand;
        public BaseCommand AddCommand
        {
            get
            {
                return addCommand ??
                (addCommand = new BaseCommand(obj =>
                {
                    Book Book = new Book();
                    Books.Insert(0, Book);
                    SelectedBook = Book;
                }));
            }
        }
        #endregion AddCommand
        #region DeleteCommand
        private BaseCommand delCommand;
        public BaseCommand DeleteCommand
        {
            get
            {
                if (delCommand != null)
                    return delCommand;
                else
                {
                    Action<object> Execute = o =>
                    {
                        Book b = (Book)o;
                        Books.Remove(b);
                    };
                    Func<object, bool> CanExecute = o => Books.Count > 0;
                    delCommand = new BaseCommand(Execute, CanExecute);
                    return delCommand;
                }
            }
        }
        #endregion
        #region SaveCommand
        private BaseCommand saveCommand;
        public BaseCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                (saveCommand = new BaseCommand(obj =>
                {
                    fileSaver.Save(Books);
                }));
            }
        }
        #endregion
        #region LoadCommand
        private BaseCommand loadCommand;
        public BaseCommand LoadCommand
        {
            get
            {
                return loadCommand ??
                (loadCommand = new BaseCommand(obj =>
                {
                    Books.Clear();
                    Books.AddRange(fileParser.GetData());
                }));
            }
        }
        #endregion
        #endregion

        #region IPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion
    }
}
