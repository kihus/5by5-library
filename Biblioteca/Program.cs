using Library;
using System.Collections.Generic;


Console.Write("passe o caminho que deseja arquivar os livros: ");
string directoryPath = Console.ReadLine() ?? "";
if (directoryPath == "")
{
    Console.WriteLine("Cloque um diretorio valido");
    return;
}



string archivePath = "bd_library.txt";


if (!Directory.Exists(directoryPath))
{
    Directory.CreateDirectory(directoryPath);
    Console.WriteLine("Diretorio criado com sucesso");
    Console.ReadKey();
}

var completePath = Path.Combine(directoryPath, archivePath);

if (!File.Exists(completePath))
{
    using (StreamWriter sw = File.CreateText(completePath))
    {
        sw.Write("Arquivos criado");
        Console.WriteLine("Arquivo criado com sucesso");
        Console.ReadKey();
    }
}


List<Book> books = [];

var book = new Book();

var cont = 0;


StreamReader reader = new StreamReader(completePath);
using (reader)
{
    GenerateId genera = new GenerateId();
    string[] read = File.ReadAllLines(completePath ?? "");

    foreach (var item in read)
    {
        string[] livros = item.Split(',');
        if (livros.Length >= 3)
        {
            var id = int.Parse(livros[0]);
            var titulo = livros[1];
            var autor = livros[2];
            var categoria = livros[3];
            books.Add(new Book(id, titulo, autor, categoria));
            genera.GetId();
            cont++;
        }
    }
    reader.Close();
}

var working = true;

do
{

    Console.Clear();
    MainMenu();
    Console.Write("-> ");
    var option = Console.ReadLine();

    switch (option)
    {
        case "1":
            book.AddBook(books);
            StreamWriter sw = new StreamWriter(completePath);
            foreach (var item in books.OrderBy(x => x.GetTitle()))
            {
                sw.WriteLine(item);
                Console.WriteLine("Livro Adicionado");
            }
            sw.Close();
            cont++;
            break;
        case "2":
            SearchBook();

            break;
        case "3":
            Console.Write("Digite o titulo do livro que deseja atualizar: ");
            var title = Console.ReadLine();
            book.UpdateBook(title, books).ExibirLivro();

            break;
        case "4":
            Console.Write("Escreva o titulo do livro que vai ser removido: ");
            var titlee = Console.ReadLine() ?? "";
            book.RemoveBook(titlee, books);
            break;
        case "0":
            Console.WriteLine("Obrigado por utilizar o programa.");
            working = false;
            break;
        default:
            Console.WriteLine("Comando não encontrado");
            break;
    }
    Console.ReadKey();
    StreamReader streamReader = new StreamReader(completePath);
    using (streamReader)
    {
        string[] read = File.ReadAllLines(completePath ?? "");

        foreach (var item in read)
        {
            string[] livros = item.Split(',');
            var id = livros[0];
            var titulo = livros[1];
            var autor = livros[2];
            var categoria = livros[3];

            Console.WriteLine($"Id: {id}, Titulo: {titulo}, Autor: {autor}, Categoria: {categoria}");
        }
        streamReader.Close();
    }


} while (working);


void MainMenu()
{
    Console.WriteLine("+----------------------------+");
    Console.WriteLine("|   === Menu Principal ===   |");
    Console.WriteLine("|-----+----------------------|");
    Console.WriteLine("|  1  |  Adicionar Livro     |");
    Console.WriteLine("|-----+----------------------|");
    Console.WriteLine("|  2  |  Buscar Livro        |");
    Console.WriteLine("|-----+----------------------|");
    Console.WriteLine("|  3  |  Atualizar Livro     |");
    Console.WriteLine("|-----+----------------------|");
    Console.WriteLine("|  4  |  Remover Livro       |");
    Console.WriteLine("|-----+----------------------|");
    Console.WriteLine("|  0  |  Sair                |");
    Console.WriteLine("+-----+----------------------+");
    Console.WriteLine("Quadntidades de livros: " + cont);
    Console.WriteLine();
}

string SearchBookMenu()
{
    Console.WriteLine("+----------------------------+");
    Console.WriteLine("|   === Buscar Livros ===    |");
    Console.WriteLine("|-----+----------------------|");
    Console.WriteLine("|  1  |  Buscar p/ Titulo    |");
    Console.WriteLine("|-----+----------------------|");
    Console.WriteLine("|  2  |  Buscar p/ Autor     |");
    Console.WriteLine("|-----+----------------------|");
    Console.WriteLine("|  3  |  Buscar p/ Id        |");
    Console.WriteLine("|-----+----------------------|");
    Console.WriteLine("|  4  |  Listar todos        |");
    Console.WriteLine("|-----+----------------------|");
    Console.WriteLine("|  0  |  Sair                |");
    Console.WriteLine("+-----+----------------------+");
    Console.WriteLine();
    Console.Write("Escreva o numéro da opção corretamente: ");

    return Console.ReadLine();
}

void SearchBook()
{
    var working = true;
    do
    {
        Console.Clear();
        var opt = SearchBookMenu();
        switch (opt)
        {
            case "1":
                Console.Write("Escreva o titulo do livro: ");
                var title = Console.ReadLine();
                var clientBook = book.SearchByTitle(title, books);

                if (clientBook is null)
                {
                    Console.WriteLine("Livro não encontrado");
                    return;
                }

                foreach (var item in clientBook)
                {
                    item.ExibirLivro();
                }

                break;
            case "2":
                Console.Write("Escreva o nome do autor do livro: ");
                var author = Console.ReadLine();
                var authorBook = book.SearchByAuthor(author, books);

                if (authorBook is null)
                {
                    Console.WriteLine("Livro não encontrado");
                    return;
                }

                foreach (var item in authorBook)
                {
                    item.ExibirLivro();
                }

                break;
            case "3":
                Console.Write("Escreva o id do livro: ");
                var id = int.Parse(Console.ReadLine());
                var idBook = book.SearchById(id, books);

                if (idBook is null)
                {
                    Console.WriteLine("Livro não encontrado");
                    return;
                }

                idBook.ExibirLivro();
                break;
            case "4":
                book.ListBook(books);
                break;
            case "0":
                Console.WriteLine("Obrigado por usar o meu programa");
                working = false;
                break;
            default:
                Console.WriteLine("Comando não encontrado");
                break;
        }

        Console.ReadKey();
    } while (working);

}