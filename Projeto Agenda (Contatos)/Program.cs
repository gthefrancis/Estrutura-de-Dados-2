using System;

class Program
{
    static Contatos minhaAgenda = new Contatos();

    static void Main(string[] args)
    {
        int opcao;
        do
        {
            Console.Clear();
            Console.WriteLine("=== AGENDA DE CONTATOS ===");
            Console.WriteLine("0. Sair");
            Console.WriteLine("1. Adicionar contato");
            Console.WriteLine("2. Pesquisar contato");
            Console.WriteLine("3. Alterar contato");
            Console.WriteLine("4. Remover contato");
            Console.WriteLine("5. Listar contatos");
            Console.Write("Escolha uma opção: ");
            
            if (int.TryParse(Console.ReadLine(), out opcao))
            {
                switch (opcao)
                {
                    case 0:
                        Console.WriteLine("Encerrando programa...");
                        break;
                    case 1:
                        AdicionarContato();
                        break;
                    case 2:
                        PesquisarContato();
                        break;
                    case 3:
                        AlterarContato();
                        break;
                    case 4:
                        RemoverContato();
                        break;
                    case 5:
                        ListarContatos();
                        break;
                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Entrada inválida!");
            }

            if (opcao != 0)
            {
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
            }

        } while (opcao != 0);
    }

    static Contato LerDadosContato(string email)
    {
        Console.Write("Nome: ");
        string nome = Console.ReadLine();

        Console.Write("Data de Nascimento (Dia): ");
        int dia = int.Parse(Console.ReadLine());
        Console.Write("Data de Nascimento (Mês): ");
        int mes = int.Parse(Console.ReadLine());
        Console.Write("Data de Nascimento (Ano): ");
        int ano = int.Parse(Console.ReadLine());

        Data dtNasc = new Data(dia, mes, ano);
        Contato c = new Contato(email, nome, dtNasc);

        string addTel;
        do
        {
            Console.Write("\nDeseja adicionar um telefone? (S/N): ");
            addTel = Console.ReadLine().ToUpper();

            if (addTel == "S")
            {
                Console.Write("Tipo (Ex: Celular, Trabalho): ");
                string tipo = Console.ReadLine();
                Console.Write("Número: ");
                string numero = Console.ReadLine();
                Console.Write("É o telefone principal? (S/N): ");
                bool principal = Console.ReadLine().ToUpper() == "S";

                c.adicionarTelefone(new Telefone(tipo, numero, principal));
            }
        } while (addTel == "S");

        return c;
    }

    static void AdicionarContato()
    {
        Console.WriteLine("\n-- ADICIONAR CONTATO --");
        Console.Write("Email (identificador único): ");
        string email = Console.ReadLine();

        Contato temp = new Contato(email);
        if (minhaAgenda.pesquisar(temp) != null)
        {
            Console.WriteLine("Já existe um contato com este email!");
            return;
        }

        Contato novo = LerDadosContato(email);
        if (minhaAgenda.adicionar(novo))
        {
            Console.WriteLine("\nContato adicionado com sucesso!");
        }
    }

    static void PesquisarContato()
    {
        Console.WriteLine("\n-- PESQUISAR CONTATO --");
        Console.Write("Informe o Email do contato: ");
        string email = Console.ReadLine();

        Contato cBusca = new Contato(email);
        Contato encontrado = minhaAgenda.pesquisar(cBusca);

        if (encontrado != null)
        {
            Console.WriteLine("\nContato Encontrado:");
            Console.WriteLine(encontrado.ToString());
            Console.WriteLine("Telefones cadastrados:");
            foreach (var t in encontrado.Telefones)
            {
                Console.WriteLine($" - [{t.Tipo}] {t.Numero} {(t.Principal ? "(Principal)" : "")}");
            }
        }
        else
        {
            Console.WriteLine("Contato não encontrado.");
        }
    }

    static void AlterarContato()
    {
        Console.WriteLine("\n-- ALTERAR CONTATO --");
        Console.Write("Informe o Email do contato a ser alterado: ");
        string email = Console.ReadLine();

        Contato existente = minhaAgenda.pesquisar(new Contato(email));
        if (existente != null)
        {
            Console.WriteLine($"\nAlterando dados de: {existente.Nome}");
            Contato atualizado = LerDadosContato(email);

            if (minhaAgenda.alterar(atualizado))
            {
                Console.WriteLine("\nContato alterado com sucesso!");
            }
        }
        else
        {
            Console.WriteLine("Contato não encontrado para alteração.");
        }
    }

    static void RemoverContato()
    {
        Console.WriteLine("\n-- REMOVER CONTATO --");
        Console.Write("Informe o Email do contato a ser removido: ");
        string email = Console.ReadLine();

        if (minhaAgenda.remover(new Contato(email)))
        {
            Console.WriteLine("Contato removido com sucesso!");
        }
        else
        {
            Console.WriteLine("Contato não encontrado.");
        }
    }

    static void ListarContatos()
    {
        Console.WriteLine("\n-- LISTA DE CONTATOS --");
        if (minhaAgenda.Agenda.Count == 0)
        {
            Console.WriteLine("A agenda está vazia.");
        }
        else
        {
            foreach (Contato c in minhaAgenda.Agenda)
            {
                Console.WriteLine(c.ToString());
            }
            Console.WriteLine($"\nTotal de contatos: {minhaAgenda.Agenda.Count}");
        }
    }
}
