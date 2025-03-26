using System.Data.Common;
using System.Windows;

namespace kniga
{
    internal class BooksDB
    {
        DbConnection connection;

        private BooksDB(DbConnection db)
        {
            this.connection = db;
        }

        public bool Insert(Books book)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {                                                                                                      //// bool ?? its true
                MySqlCommand cmd = connection.CreateCommand("insert into `books` Values (0, @tittle, 0, 0, @genre, @is_available);select LAST_INSERT_ID();");

                cmd.Parameters.Add(new MySqlParameter("tittle", book.tittle));
                cmd.Parameters.Add(new MySqlParameter("genre", book.genre));


                try
                {
                   
                    int id = (int)(ulong)cmd.ExecuteScalar();
                    if (id > 0)
                    {
                        MessageBox.Show(id.ToString());                      
                        book.Id = id;
                       //??? book.author_id = author_id;
                        result = true;
                    }
                    else
                    {
                        MessageBox.Show("Запись не добавлена");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return result;
        }

        internal List<Books> SelectAll()
        {
            List<Books> book = new List<Books>();
            if (connection == null)
                return book;

            if (connection.OpenConnection())
            {
                var command = connection.CreateCommand("select `id`, ` tittle`, ` author_id`, `year_published`, `genre`, `is_available` from `books` ");
                try
                {
                  
                    MySqlDataReader dr = command.ExecuteReader();
                  
                    {
                        int id = dr.GetInt32(0);
                        string tittle = string.Empty;
                        string genre = string.Empty;
                        int author_id = dr.GetString("author_id");
                        int year_published = dr.GetString("year_published");
                        bool is_available  = dr.GetString("is_available");

                        book.Add(new Books
                        {
                            Id = id,
                            tittle = tittle,
                            author_id = author_id,
                            year_published = year_published,
                            genre = genre,
                            is_available = is_available

                        });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return book;
        }

        internal bool Update(Books edit)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"update `books` set ` tittle`=@tittle, ` author_id`=@author_id, ` year_published`=@year_published, ` genre`=@genre, ` is_available`=@is_available where `id` = {edit.Id}");
                mc.Parameters.Add(new MySqlParameter("tittle",edit.tittle));
                mc.Parameters.Add(new MySqlParameter("genre", edit.genre));

                try
                {
                    mc.ExecuteNonQuery();
                    result = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return result;
        }


        internal bool Remove(Books remove)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"delete from `books` where `id` = {remove.Id}");
                try
                {
                    mc.ExecuteNonQuery();
                    result = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("точно точно удалить??");
                }
            }
            connection.CloseConnection();
            return result;
        }

        static BooksDB db;
        public static BooksDB GetDb()
        {
            if (db == null)
                db = new BooksDB(DbConnection.GetDbConnection());
            return db;
        }
    }
}

