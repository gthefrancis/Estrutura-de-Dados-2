using System;
using System.Linq;

class Program
{
    static Livros acervoDigital = new Livros();

    static void Main(string[] args)
    {
        int opcao;
        do
        {
            Console.Clear();
            Console.WriteLine("=== SISTEMA DE BIBLIOTECA ===");
            Console.WriteLine("0. Sair");
            Console.WriteLine("1. Adicionar livro");
            Console.WriteLine("2. Pesquisar livro (sintético)");
            Console.WriteLine("3. Pesquisar livro (analítico)");
            Console.WriteLine("4. Adicionar exemplar");
            Console.WriteLine("5. Registrar empréstimo");
            Console.WriteLine("6. Registrar devolução");
            Console.Write("Escolha uma opção: ");
            
            if (int.TryParse(Console.ReadLine(), out opcao))
            {
                switch (opcao)
                {
                    case 0:
                        Console.WriteLine("Saindo...");
                        break;
                    case 1:
                        AdicionarLivro();
                        break;
                    case 2:
                        PesquisarLivro(false); // Sintético
                        break;
                    case 3:
                        PesquisarLivro(true); // Analítico
                        break;
                    case 4:
                        AdicionarExemplar();
                        break;
                    case 5:
                        RegistrarEmprestimo();
                        break;
                    case 6:
                        RegistrarDevolucao();
                        break;
                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Entrada inválida.");
            }

            if (opcao != 0)
            {
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
            }

        } while (opcao != 0);
    }

    static void AdicionarLivro()
    {
        Console.WriteLine("\n-- ADICIONAR LIVRO --");
        Console.Write("ISBN: ");
        int isbn = int.Parse(Console.ReadLine());
        Console.Write("Título: ");
        string titulo = Console.ReadLine();
        Console.Write("Autor: ");
        string autor = Console.ReadLine();
        Console.Write("Editora: ");
        string editora = Console.ReadLine();

        Livro l = new Livro(isbn, titulo, autor, editora);
        if (acervoDigital.pesquisar(l) == null)
        {
            acervoDigital.adicionar(l);
            Console.WriteLine("Livro adicionado com sucesso!");
        }
        else
        {
            Console.WriteLine("Já existe um livro com este ISBN no acervo.");
        }
    }

    static void PesquisarLivro(bool analitico)
    {
        Console.WriteLine($"\n-- PESQUISA DE LIVRO ({(analitico ? "ANALÍTICO" : "SINTÉTICO")}) --");
        Console.Write("Informe o ISBN do Livro: ");
        int isbn = int.Parse(Console.ReadLine());

        Livro lBusca = new Livro(isbn);
        Livro encontrado = acervoDigital.pesquisar(lBusca);

        if (encontrado != null)
        {
            Console.WriteLine($"\nTítulo: {encontrado.Titulo} | Autor: {encontrado.Autor} | Editora: {encontrado.Editora}");
            Console.WriteLine($"Total de Exemplares: {encontrado.qtdeExemplares()}");
            Console.WriteLine($"Exemplares Disponíveis: {encontrado.qtdeDisponiveis()}");
            Console.WriteLine($"Total de Empréstimos: {encontrado.qtdeEmprestimos()}");
            Console.WriteLine($"Disponibilidade: {encontrado.percDisponibilidade():F2}%");

            if (analitico)
            {
                Console.WriteLine("\n-- DETALHES DOS EXEMPLARES --");
                if (encontrado.qtdeExemplares() == 0)
                {
                    Console.WriteLine("Nenhum exemplar cadastrado.");
                }

                foreach (Exemplar ex in encontrado.Exemplares)
                {
                    Console.WriteLine($"\n[Tombo: {ex.Tombo}] - Status: {(ex.disponivel() ? "Disponível" : "Emprestado")}");
                    Console.WriteLine($"Empréstimos registrados: {ex.qtdeEmprestimos()}");
                    
                    foreach (Emprestimo emp in ex.Emprestimos)
                    {
                        string dataDev = emp.DtDevolucao == DateTime.MinValue ? "Pendente" : emp.DtDevolucao.ToString("dd/MM/yyyy HH:mm");
                        Console.WriteLine($"   -> Retirado: {emp.DtEmprestimo.ToString("dd/MM/yyyy HH:mm")} | Devolvido: {dataDev}");
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("Livro não encontrado no acervo.");
        }
    }

    static void AdicionarExemplar()
    {
        Console.WriteLine("\n-- ADICIONAR EXEMPLAR --");
        Console.Write("ISBN do Livro: ");
        int isbn = int.Parse(Console.ReadLine());

        Livro lBusca = new Livro(isbn);
        Livro encontrado = acervoDigital.pesquisar(lBusca);

        if (encontrado != null)
        {
            Console.Write("Número de Tombo do Exemplar: ");
            int tombo = int.Parse(Console.ReadLine());

            if (encontrado.Exemplares.Any(e => e.Tombo == tombo))
            {
                Console.WriteLine("Já existe um exemplar com este tombo para este livro.");
            }
            else
            {
                Exemplar ex = new Exemplar(tombo);
                encontrado.adicionarExemplar(ex);
                Console.WriteLine("Exemplar adicionado com sucesso!");
            }
        }
        else
        {
            Console.WriteLine("Livro não encontrado.");
        }
    }

    static void RegistrarEmprestimo()
    {
        Console.WriteLine("\n-- REGISTRAR EMPRÉSTIMO --");
        Console.Write("ISBN do Livro: ");
        int isbn = int.Parse(Console.ReadLine());

        Livro lBusca = new Livro(isbn);
        Livro encontrado = acervoDigital.pesquisar(lBusca);

        if (encontrado != null)
        {
            Exemplar disponivel = encontrado.Exemplares.FirstOrDefault(e => e.disponivel());
            if (disponivel != null)
            {
                if (disponivel.emprestar())
                {
                    Console.WriteLine($"Empréstimo registrado com sucesso! (Exemplar Tombo: {disponivel.Tombo})");
                }
            }
            else
            {
                Console.WriteLine("Não há exemplares disponíveis deste livro no momento.");
            }
        }
        else
        {
            Console.WriteLine("Livro não encontrado.");
        }
    }

    static void RegistrarDevolucao()
    {
        Console.WriteLine("\n-- REGISTRAR DEVOLUÇÃO --");
        Console.Write("ISBN do Livro: ");
        int isbn = int.Parse(Console.ReadLine());

        Livro lBusca = new Livro(isbn);
        Livro encontrado = acervoDigital.pesquisar(lBusca);

        if (encontrado != null)
        {
            Console.Write("Tombo do Exemplar a devolver: ");
            int tombo = int.Parse(Console.ReadLine());

            Exemplar ex = encontrado.Exemplares.FirstOrDefault(e => e.Tombo == tombo);

            if (ex != null)
            {
                if (!ex.disponivel())
                {
                    if (ex.devolver())
                        Console.WriteLine("Devolução registrada com sucesso!");
                }
                else
                {
                    Console.WriteLine("Este exemplar já consta como disponível (não está emprestado).");
                }
            }
            else
            {
                Console.WriteLine("Exemplar não encontrado.");
            }
        }
        else
        {
            Console.WriteLine("Livro não encontrado.");
        }
    }
}
