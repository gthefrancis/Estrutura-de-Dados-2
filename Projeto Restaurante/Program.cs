using System;

class Program
{
    static Restaurante cozinha = new Restaurante();
    static int contadorIdPedido = 1; // Para numerar sequencialmente os pedidos

    static void Main(string[] args)
    {
        int opcao;
        do
        {
            Console.Clear();
            Console.WriteLine("=== SISTEMA DE COZINHA INDUSTRIAL ===");
            Console.WriteLine("0. Sair");
            Console.WriteLine("1. Criar novo pedido");
            Console.WriteLine("2. Adicionar item ao pedido");
            Console.WriteLine("3. Remover item do pedido");
            Console.WriteLine("4. Consultar pedido");
            Console.WriteLine("5. Cancelar pedido");
            Console.WriteLine("6. Listar todos os pedidos");
            Console.Write("Escolha uma opção: ");

            if (int.TryParse(Console.ReadLine(), out opcao))
            {
                Console.Clear();
                switch (opcao)
                {
                    case 0: Console.WriteLine("Encerrando o sistema..."); break;
                    case 1: CriarNovoPedido(); break;
                    case 2: AdicionarItemAoPedido(); break;
                    case 3: RemoverItemDoPedido(); break;
                    case 4: ConsultarPedido(); break;
                    case 5: CancelarPedido(); break;
                    case 6: ListarTodosOsPedidos(); break;
                    default: Console.WriteLine("Opção inválida!"); break;
                }
            }
            else
            {
                Console.WriteLine("Entrada inválida.");
                opcao = -1;
            }

            if (opcao != 0)
            {
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
            }

        } while (opcao != 0);
    }

    static void CriarNovoPedido()
    {
        Console.WriteLine("-- CRIAR NOVO PEDIDO --");
        Console.Write("Nome do Cliente: ");
        string cliente = Console.ReadLine();

        Pedido p = new Pedido(contadorIdPedido, cliente);
        if (cozinha.novoPedido(p))
        {
            Console.WriteLine($"\nPedido {contadorIdPedido} criado com sucesso para o(a) cliente {cliente}!");
            contadorIdPedido++; // Incrementa para o próximo pedido ser sequencial
        }
        else
        {
            Console.WriteLine("\nCapacidade máxima diária de 50 pedidos foi atingida!");
        }
    }

    static void AdicionarItemAoPedido()
    {
        Console.WriteLine("-- ADICIONAR ITEM AO PEDIDO --");
        Console.Write("Informe o ID do Pedido: ");
        int idPedido = int.Parse(Console.ReadLine());

        Pedido p = cozinha.buscarPedido(new Pedido(idPedido));
        if (p != null)
        {
            Console.Write("ID do Item: ");
            int idItem = int.Parse(Console.ReadLine());
            Console.Write("Descrição do Item (ex: Prato Feito): ");
            string desc = Console.ReadLine();
            Console.Write("Preço do Item (R$): ");
            double preco = double.Parse(Console.ReadLine());

            Item item = new Item(idItem, desc, preco);

            if (p.adicionarItem(item))
            {
                Console.WriteLine("Item adicionado com sucesso!");
            }
            else
            {
                Console.WriteLine("Erro: Este pedido já possui o limite máximo de 10 itens.");
            }
        }
        else
        {
            Console.WriteLine("Pedido não encontrado.");
        }
    }

    static void RemoverItemDoPedido()
    {
        Console.WriteLine("-- REMOVER ITEM DO PEDIDO --");
        Console.Write("Informe o ID do Pedido: ");
        int idPedido = int.Parse(Console.ReadLine());

        Pedido p = cozinha.buscarPedido(new Pedido(idPedido));
        if (p != null)
        {
            Console.Write("Informe o ID do Item a ser removido: ");
            int idItem = int.Parse(Console.ReadLine());

            Item fakeItem = new Item(idItem, "", 0); // O equals compara pelo ID

            if (p.removerItem(fakeItem))
            {
                Console.WriteLine("Item removido com sucesso!");
            }
            else
            {
                Console.WriteLine("Item não encontrado neste pedido.");
            }
        }
        else
        {
            Console.WriteLine("Pedido não encontrado.");
        }
    }

    static void ConsultarPedido()
    {
        Console.WriteLine("-- CONSULTAR PEDIDO --");
        Console.Write("Informe o ID do Pedido: ");
        int idPedido = int.Parse(Console.ReadLine());

        Pedido p = cozinha.buscarPedido(new Pedido(idPedido));
        if (p != null)
        {
            Console.WriteLine("\n" + p.dadosDoPedido());
        }
        else
        {
            Console.WriteLine("Pedido não encontrado.");
        }
    }

    static void CancelarPedido()
    {
        Console.WriteLine("-- CANCELAR PEDIDO --");
        Console.Write("Informe o ID do Pedido a ser cancelado: ");
        int idPedido = int.Parse(Console.ReadLine());

        if (cozinha.cancelarPedido(new Pedido(idPedido)))
        {
            Console.WriteLine("Pedido cancelado e removido com sucesso!");
        }
        else
        {
            Console.WriteLine("Pedido não encontrado.");
        }
    }

    static void ListarTodosOsPedidos()
    {
        Console.WriteLine("-- LISTA DE PEDIDOS DO DIA --");
        
        if (cozinha.ProxPedido == 0)
        {
            Console.WriteLine("Nenhum pedido ativo no momento.");
            return;
        }

        double somaGeral = 0;
        
        for (int i = 0; i < cozinha.ProxPedido; i++)
        {
            Pedido p = cozinha.Pedidos[i];
            double totalPedido = p.calcularTotal();
            somaGeral += totalPedido;

            Console.WriteLine($"[Pedido {p.Id}] - Cliente: {p.Cliente} | Total: R$ {totalPedido:F2}");
        }

        Console.WriteLine("----------------------------------");
        Console.WriteLine($"TOTAL ARRECADADO NO DIA: R$ {somaGeral:F2}");
        Console.WriteLine($"Qtd. de Pedidos Ativos: {cozinha.ProxPedido}");
    }
}
