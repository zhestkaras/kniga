using System.Data.Common;
using System.Windows;
using kniga;

    internal class AuthorDB
{
        DbConnection connection;

        private AuthorDB(DbConnection db)
        {
            this.connection = db;
        }

        public bool Insert(Author author)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                MySqlCommand cmd = connection.CreateCommand("insert into `author` Values (0, @first_name, @patrionymic, @last_name, @birthday);select LAST_INSERT_ID();");

                
                cmd.Parameters.Add(new MySqlParameter("first_name", author.FirstName));
            cmd.Parameters.Add(new MySqlParameter("patrionymic", author.Patrionymic));
            cmd.Parameters.Add(new MySqlParameter("last_name", author.LastName));


            try
                {
                    
                    int id = (int)(ulong)cmd.ExecuteScalar();
                    if (id > 0)
                    {
                        MessageBox.Show(id.ToString());
                   
                       author.ID = id;
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

        internal List<Author> SelectAll()
        {
            List<Author> author = new List<Author>();
            if (connection == null)
                return author;

            if (connection.OpenConnection())
            {
                var command = connection.CreateCommand("select `id`, `first_name`, `patrionymic`, `last_name`, `birthday` from `author` ");
                try
                {
                   
                    MySqlDataReader dr = command.ExecuteReader();
                   
                    while (dr.Read())
                    {
                        int id = dr.GetInt32(0);
                        string first_name = string.Empty;
                    string patrionymic = string.Empty;
                    string last_name = string.Empty;
                    DateTime birthday = DateTime.MinValue;



                    if (!dr.IsDBNull(1))
                        first_name = dr.GetString("first_name");
                        string Patrionymic = dr.GetString("patrionymic");


                    string last_name = dr.GetString("Lname");
                    string birthday = dr.GetString("Lname");
                    author.Add(new Author
                       {
                            ID = id,
                            FirstName = fname,
                            LastName = lname
                        });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            connection.CloseConnection();
            return author;
        }

        internal bool Update(Author edit)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"update `author` set `Fname`=@fname, `Lname`=@lname where `id` = {edit.ID}");
                mc.Parameters.Add(new MySqlParameter("fname", edit.FirstName));
            mc.Parameters.Add(new MySqlParameter("fname", edit.FirstName)); 
            mc.Parameters.Add(new MySqlParameter("lname", edit.LastName));

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


        internal bool Remove(Author remove)
        {
            bool result = false;
            if (connection == null)
                return result;

            if (connection.OpenConnection())
            {
                var mc = connection.CreateCommand($"delete from `author` where `id` = {remove.ID}");
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

        static AuthorDB db;
        public static AuthorDB GetDb()
        {
            if (db == null)
                db = new AuthorDB (DbConnection.GetDbConnection());
            return db;
        }
    }
}