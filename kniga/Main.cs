using System.Collections.ObjectModel;
using kniga;
namespace kniga
{
    internal class Main : BaseVM
    {
        private Author selectedClient;
        private ObservableCollection<Author> author = new();

        public ObservableCollection<Author> Authors
        {
            get => author;
            set
            {
                author = value;
                Signal();
            }
        }
        public Author SelectedAuthor
        {
            get => selectedAuthor;
            set
            {
                selectedAuthor = value;
                Signal();
            }
        }
        public Command UpdateAuthor { get; set; }
        public Command RemoveAuthor { get; set; }
        public Command AddAuthor { get; set; }

        public Main()
        {
            SelectAll();

            UpdateAuthor = new Command(() =>
            {
                if (AuthorDB.GetDb().Update(SelectedAuthor))
                    MessageBox.Show("Успешно");
            }, () => SelectedAuthor != null);

            RemoveAuthor = new Command(() =>
            {
                AuthorDB.GetDb().Remove(SelectedAuthor);
                SelectAll();
            }, () => SelectedAuthor != null);

            AddAuthor = new Command(() =>
            {
                new WinAddAuthor().ShowDialog();
                SelectAll();
            }, () => true);
        }

        private void SelectAll()
        {
            Author = new ObservableCollection<Author>(AuthorDB.GetDb().SelectAll());
        }

    }
}