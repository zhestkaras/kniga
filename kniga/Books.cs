namespace kniga
{
    public class Books
    {
            public int Id { get; set; }
            public string tittle { get; set; }
            public int author_id { get; set; }
            public int year_published { get; set; }
            public string genre { get; set; }
            public bool is_available { get; set; }
    }
}
/*
           id
           tittle
            author_id
            year_published
            genre
            is_available
*/