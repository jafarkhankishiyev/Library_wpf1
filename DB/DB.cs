using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_wpf.DB
{
    public class DB
    {
        protected static (string, string, string) TailorDB(string table, string addColumns, string readColumns, string insertParameters, string deleteColAndParam)
        {
            string readquery = $"SELECT {readColumns} FROM {table};";
            string addquery = $"INSERT INTO {table} ({addColumns}) VALUES ({insertParameters});";
            string deletequery = $"DELETE FROM {table} WHERE {deleteColAndParam};";
            return (readquery, addquery, deletequery);
        }
    }
}
