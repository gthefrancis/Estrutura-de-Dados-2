using System;

namespace ProjetoVendedores
{
    internal class Program
    {
        static Vendedores vendedores = new Vendedores(10);

        static void Main(string[] args)
        {
            int opcao;

            do
            {
                Console.Clear();

                Console.WriteLine("======================================");
                Console.WriteLine("        SISTEMA DE VENDEDORES");
                Console.WriteLine("======================================");
                Console.WriteLine("1 - Cadastrar vendedor");
                Console.WriteLine("2 - Consultar vendedor");
                Console.WriteLine("3 - Excluir vendedor");
                Console.WriteLine("4 - Registrar venda");
                Console.WriteLine("5 - Listar vendedores");
                Console.WriteLine("0 - Sair");
                Console.WriteLine("======================================");

                opcao = LerInteiro("Digite uma opção: ");

                Console.Clear();

                switch (opcao)
                {
                    case 1:
                        CadastrarVendedor();
                        break;

                    case 2:
                        ConsultarVendedor();
                        break;

                    case 3:
                        ExcluirVendedor();
                        break;

                    case 4:
                        RegistrarVenda();
                        break;

                    case 5:
                        ListarVendedores();
                        break;

                    case 0:
                        Console.WriteLine("Programa encerrado.");
                        break;

                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }

                if (opcao != 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("Pressione ENTER para continuar...");
                    Console.ReadLine();
                }

            } while (opcao != 0);
        }

        // ==========================================
        // 1 - CADASTRAR VENDEDOR
        // ==========================================

        static void CadastrarVendedor()
        {
            Console.WriteLine("======================================");
            Console.WriteLine("       CADASTRAR VENDEDOR");
            Console.WriteLine("======================================");

            if (vendedores.Quantidade >= 10)
            {
                Console.WriteLine("Limite máximo de 10 vendedores atingido.");
                return;
            }

            int id = LerInteiro("ID do vendedor: ");

            Vendedor vendedorExistente =
                vendedores.searchVendedor(new Vendedor(id, "", 0));

            if (vendedorExistente != null)
            {
                Console.WriteLine("Já existe um vendedor com esse ID.");
                return;
            }

            Console.Write("Nome do vendedor: ");
            string nome = Console.ReadLine();

            double percComissao =
                LerDouble("Percentual de comissão (%): ");

            if (percComissao < 0)
            {
                Console.WriteLine("Percentual de comissão inválido.");
                return;
            }

            Vendedor vendedor =
                new Vendedor(id, nome, percComissao);

            if (vendedores.addVendedor(vendedor))
            {
                Console.WriteLine();
                Console.WriteLine("Vendedor cadastrado com sucesso!");
            }
            else
            {
                Console.WriteLine("Não foi possível cadastrar o vendedor.");
            }
        }

        // ==========================================
        // 2 - CONSULTAR VENDEDOR
        // ==========================================

        static void ConsultarVendedor()
        {
            Console.WriteLine("======================================");
            Console.WriteLine("       CONSULTAR VENDEDOR");
            Console.WriteLine("======================================");

            int id = LerInteiro("Digite o ID do vendedor: ");

            Vendedor vendedor =
                vendedores.searchVendedor(
                    new Vendedor(id, "", 0)
                );

            if (vendedor == null)
            {
                Console.WriteLine("Vendedor não encontrado.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("ID: " + vendedor.Id);
            Console.WriteLine("Nome: " + vendedor.Nome);
            Console.WriteLine(
                "Valor total das vendas: R$ " +
                vendedor.valorVendas().ToString("F2")
            );

            Console.WriteLine(
                "Valor da comissão: R$ " +
                vendedor.valorComissao().ToString("F2")
            );

            Console.WriteLine(
                "Valor médio das vendas diárias: R$ " +
                vendedor.valorMedioVendas().ToString("F2")
            );
        }

        // ==========================================
        // 3 - EXCLUIR VENDEDOR
        // ==========================================

        static void ExcluirVendedor()
        {
            Console.WriteLine("======================================");
            Console.WriteLine("        EXCLUIR VENDEDOR");
            Console.WriteLine("======================================");

            int id = LerInteiro("Digite o ID do vendedor: ");

            Vendedor vendedor =
                vendedores.searchVendedor(
                    new Vendedor(id, "", 0)
                );

            if (vendedor == null)
            {
                Console.WriteLine("Vendedor não encontrado.");
                return;
            }

            if (vendedor.possuiVendas())
            {
                Console.WriteLine(
                    "Não é possível excluir este vendedor."
                );

                Console.WriteLine(
                    "O vendedor possui vendas registradas."
                );

                return;
            }

            if (vendedores.delVendedor(vendedor))
            {
                Console.WriteLine(
                    "Vendedor excluído com sucesso!"
                );
            }
            else
            {
                Console.WriteLine(
                    "Não foi possível excluir o vendedor."
                );
            }
        }

        // ==========================================
        // 4 - REGISTRAR VENDA
        // ==========================================

        static void RegistrarVenda()
        {
            Console.WriteLine("======================================");
            Console.WriteLine("          REGISTRAR VENDA");
            Console.WriteLine("======================================");

            int id = LerInteiro("ID do vendedor: ");

            Vendedor vendedor =
                vendedores.searchVendedor(
                    new Vendedor(id, "", 0)
                );

            if (vendedor == null)
            {
                Console.WriteLine("Vendedor não encontrado.");
                return;
            }

            int dia = LerInteiro("Dia da venda (1 a 31): ");

            if (dia < 1 || dia > 31)
            {
                Console.WriteLine("Dia inválido.");
                return;
            }

            int qtde =
                LerInteiro("Quantidade de vendas: ");

            if (qtde <= 0)
            {
                Console.WriteLine("A quantidade deve ser maior que zero.");
                return;
            }

            double valor =
                LerDouble("Valor total das vendas: R$ ");

            if (valor < 0)
            {
                Console.WriteLine("Valor inválido.");
                return;
            }

            Venda venda = new Venda(qtde, valor);

            vendedor.registrarVenda(dia, venda);

            Console.WriteLine();
            Console.WriteLine("Venda registrada com sucesso!");
        }

        // ==========================================
        // 5 - LISTAR VENDEDORES
        // ==========================================

        static void ListarVendedores()
        {
            Console.WriteLine("======================================");
            Console.WriteLine("         LISTA DE VENDEDORES");
            Console.WriteLine("======================================");

            if (vendedores.Quantidade == 0)
            {
                Console.WriteLine("Nenhum vendedor cadastrado.");
                return;
            }

            for (int i = 0; i < vendedores.Quantidade; i++)
            {
                Vendedor vendedor =
                    vendedores.getVendedor(i);

                Console.WriteLine();
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("ID: " + vendedor.Id);
                Console.WriteLine("Nome: " + vendedor.Nome);

                Console.WriteLine(
                    "Valor total das vendas: R$ " +
                    vendedor.valorVendas().ToString("F2")
                );

                Console.WriteLine(
                    "Valor da comissão: R$ " +
                    vendedor.valorComissao().ToString("F2")
                );
            }

            Console.WriteLine();
            Console.WriteLine("======================================");
            Console.WriteLine("              TOTAIS");
            Console.WriteLine("======================================");

            Console.WriteLine(
                "Total de vendas: R$ " +
                vendedores.valorVendas().ToString("F2")
            );

            Console.WriteLine(
                "Total de comissões: R$ " +
                vendedores.valorComissao().ToString("F2")
            );
        }

        // ==========================================
        // MÉTODOS AUXILIARES
        // ==========================================

        static int LerInteiro(string mensagem)
        {
            int valor;

            while (true)
            {
                Console.Write(mensagem);

                if (int.TryParse(Console.ReadLine(), out valor))
                {
                    return valor;
                }

                Console.WriteLine(
                    "Digite um número inteiro válido."
                );
            }
        }

        static double LerDouble(string mensagem)
        {
            double valor;

            while (true)
            {
                Console.Write(mensagem);

                if (double.TryParse(Console.ReadLine(), out valor))
                {
                    return valor;
                }

                Console.WriteLine(
                    "Digite um valor numérico válido."
                );
            }
        }
    }
}
