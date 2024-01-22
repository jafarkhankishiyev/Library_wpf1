using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_wpf.DB
{
    public class DB
    {
        protected static (string, string, string) TailorDB(string table, string columns, string insertParameters, string deleteColAndParam)
        {
            string readquery = $"SELECT {columns} FROM {table};";
            string addquery = $"INSERT INTO {table} ({columns}) VALUES ({insertParameters});";
            string deletequery = $"DELETE FROM {table} WHERE {deleteColAndParam};";
            return (readquery, addquery, deletequery);
        }
    }
}
