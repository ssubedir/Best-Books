using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreLIB
{
    public class BookCatalog
    {
        public DataSet GetBookInfo()
        {
            DALBookCatalog bookCatalog = new DALBookCatalog();
            return bookCatalog.GetBookInfo();
        }

        public DataSet SearchBook(string key)
        {
            DALBookCatalog bookCatalog = new DALBookCatalog();
            return bookCatalog.GetSearchBookInfo(key);

        }
        public DataSet load_Item(string isbn)
        {
            DALBookCatalog bookCatalog = new DALBookCatalog();
            return bookCatalog.loadItem(isbn);

        }
        public DataSet load_History(int id)
        {
            DALBookCatalog bookCatalog = new DALBookCatalog();
            return bookCatalog.loadHistory(id);

        }

    }
}
