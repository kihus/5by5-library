namespace Library
{
    internal class Book
    {
        private int _id {  get; init; }
        private string _title { get; set; }
        private string _author { get; set; }
        private string _category { get; set; }
        private GenerateId _generateId { get; set; }

        public Book()
        {

        }
        public Book(int id, string title, string author, string category)
        {
            _id = id;
            _title = title;
            _author = author;
            _category = category;
            
        }

        public string GetTitle()
        {
            return _title;
        }

        public List<Book> AddBook(List<Book> books)
        {
            _generateId = new GenerateId();
            Console.Write("Digite o titulo do livro: ");
            var titulo = Console.ReadLine();

            Console.Write("Digite o nome do Autor: ");
            var autor = Console.ReadLine();

            Console.Write("Digite a categoria do livro: ");
            var categoria = Console.ReadLine();

            books.Add(new Book(_generateId.GetId(), titulo, autor, categoria));

            return books;
        }

        public List<Book> AllBooks(List<Book> books, string path)
        {
            StreamReader reader = new StreamReader(path);
            using (reader)
            {

                string[] read = File.ReadAllLines(path ?? "");

                if (read.Length <= 0)
                {

                    reader.Close();
                    return null;
                }
                if (read != null)
                {
                    foreach (var item in read)
                    {
                        string[] livros = item.Split(',');
                        var id = int.Parse(livros[0]);
                        var titulo = livros[1];
                        var autor = livros[2];
                        var categoria = livros[3];

                        books.Add(new Book(id, titulo, autor, categoria));
                    }
                }

                reader.Close();
            }
            return books;
        }
        public void ListBook(List<Book> books)
        {
            foreach (var item in books.OrderBy(x => x._title))
            {
                item.ExibirLivro();
            }
        }
        public List<Book> SearchByTitle(string title, List<Book> books)
        {
            var book = books.Where(x => x._title.Contains(title));
            if(book is null)
            {
                return null;
            }

            return [.. book];
        }
        public List<Book> SearchByAuthor(string author, List<Book> books)
        {
            var book = books.Where(x => x._author.Contains(author));
            if (book is null)
            {
                return null;
            }

            return [.. book];
        }

        public Book SearchById(int id, List<Book> books)
        {
            
            var idBook = books.FirstOrDefault(x => x._id == id);
            if (idBook is null)
            {
                return null;
            }

            return idBook;
        }
        public Book UpdateBook(string title, List<Book> book)
        {
            var bookCustomer = book.FirstOrDefault(x => x._title == title);

            bookCustomer.ExibirLivro();

            Console.WriteLine("Digite o titulo que vai ser alterado");
            var newTitle = Console.ReadLine();

            bookCustomer._title = newTitle;


            Console.WriteLine("Alteração concluida");
            return bookCustomer;
        }
        public void RemoveBook(string title, List<Book> book)
        {
            var bookCustomer = book.FirstOrDefault(x => x._title == title);
            if( bookCustomer is null)
            {
                Console.WriteLine("Livro não existe");
                return;
            }
            book.Remove(bookCustomer);

            Console.WriteLine($"O livro {bookCustomer._title} foi removido com sucesso!");
        }
        public void ExibirLivro()
        {
            
            Console.WriteLine($"\n[Id]: {_id}");
            Console.WriteLine($"[Titulo]: {_title}");
            Console.WriteLine($"[Autor]: {_author}");
            Console.WriteLine($"[Categoria]: {_category}\n");
        }

        public override string ToString()
        {
            return $"{_id},{_title},{_author},{_category}";

        }
    }
}
