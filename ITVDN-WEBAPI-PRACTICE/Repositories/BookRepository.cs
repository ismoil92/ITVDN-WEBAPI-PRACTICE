using ITVDN_WEBAPI_PRACTICE.Context;
using ITVDN_WEBAPI_PRACTICE.Models;
using Microsoft.EntityFrameworkCore;

namespace ITVDN_WEBAPI_PRACTICE.Repositories
{
    public class BookRepository : IRepository<Book>
    {

        #region FIELDS
        private BookContext db;
        private bool disposed = false;
        #endregion

        #region CONSTUCTOR
        public BookRepository(BookContext db) => this.db = db;
        #endregion

        #region METHODS

        /// <summary>
        /// Метод получение коллекция объекта типа Book
        /// </summary>
        /// <returns>Возвращает коллекция объекта типа Book</returns>
        public IEnumerable<Book> GetAllBooks() => db.Books;


        /// <summary>
        /// Метод получение объекта типа Book по id
        /// </summary>
        /// <param name="id">id объекта типа Book</param>
        /// <returns>Возвращает объект типа Book</returns>
        public Book GetBookById(int id)
        {
           var book = db.Books.FirstOrDefault(x => x.Id == id);
            if (book == null)
                return null!;
            return book;

        }

        /// <summary>
        /// Создание объекта типа Book, а также добавление в таблицу Books из базы данных
        /// </summary>
        /// <param name="book">Объект типа Book</param>

        public void CreateBook(Book book)
        {
            db.Books.Add(book);
            db.SaveChanges();
        }

        /// <summary>
        /// Метод, изменние объъекта типа Book, а также сохранение в таблицу Books из базы данных
        /// </summary>
        /// <param name="book">Объект типа Book</param>
        public void UpdateBook(Book book)
        {
            db.Entry(book).State =EntityState.Modified;
            db.SaveChanges();
        }


        /// <summary>
        /// Метод удаление объекта типа Book, а также изменение в таблицы Books из базы данных
        /// </summary>
        /// <param name="id">id объекта типа Book</param>
        public void DeleteBook(int id)
        {
            var book = db.Books.FirstOrDefault(x => x.Id == id);
            if(book != null)
            {
                db.Books.Remove(book);
                db.SaveChanges();
            }

        }

        /// <summary>
        /// Виртуальный метод, для удаление контекст базы данных
        /// </summary>
        /// <param name="disposing">Для вызова в контексте базы данных метод,Dispose в интерфейсе IDisposable</param>
        protected virtual void Dispose(bool disposing)
        {
            if(!this.disposed)
            {
                if(disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// Метод для вызова виртуального метода Dispose, а также вызов сборщика мусора (Garbage Collector)
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
