using kniga;
namespace kniga
{
    internal class WinAddAuthor : BaseVM
    {
        private Author newAuthor = new();

        public Author NewAuthor
        {
            get => newAuthor;
            set
            {
                newAuthor = value;
                Signal();
            }
        }

        public Command InsertAuthor { get; set; }
        public WinAddAuthor()
        {
            InsertAuthor = new Command(() =>
            {
                AuthorDB.GetDb().Insert(NewAuthor);
                close?.Invoke();
            },
                () =>
                !string.IsNullOrEmpty(newAuthor.FirstName) &&
                !string.IsNullOrEmpty(newAuthor.LastName));
        }
        Action close;
        internal void SetClose(Action close)
        {
            this.close = close;
        }
    }
}