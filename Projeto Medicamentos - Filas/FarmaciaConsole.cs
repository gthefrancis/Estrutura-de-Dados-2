using System;
using System.Collections.Generic;
using System.Linq;

namespace FarmaciaConsole
{
    class Lote
    {
        private int id;
        private int qtde;
        private DateTime venc;

        public int Id { get => id; set => id = value; }
        public int Qtde { get => qtde; set => qtde = value; }
        public DateTime Venc { get => venc; set => venc = value; }

        public Lote() { }

        public Lote(int id, int qtde, DateTime venc)
        {
            this.id = id;
            this.qtde = qtde;
            this.venc = venc;
        }

        public override string ToString()
        {
            return $"{id}-{qtde}-{venc:dd/MM/yyyy}";
        }
    }

    class Medicamento
    {
        private int id;
        private string nome;
        private string laboratorio;
        private Queue<Lote> lotes;

        public int Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Laboratorio { get => laboratorio; set => laboratorio = value; }
        public Queue<Lote> Lotes { get => lotes; set => lotes = value; }

        public Medicamento()
        {
            this.lotes = new Queue<Lote>();
        }

        public Medicamento(int id, string nome, string laboratorio)
        {
            this.id = id;
            this.nome = nome;
            this.laboratorio = laboratorio;
            this.lotes = new Queue<Lote>();
        }

        public int QtdeDisponivel()
        {
            int total = 0;
            foreach (var lote in lotes)
            {
                total += lote.Qtde;
            }
            return total;
        }

        public void Comprar(Lote lote)
        {
            lotes.Enqueue(lote);
        }

        public bool Vender(int qtdeDesejada)
        {
            if (QtdeDisponivel() < qtdeDesejada)
            {
                return false; // Saldo insuficiente
            }

            int restanteParaVender = qtdeDesejada;

            while (restanteParaVender > 0)
            {
                Lote loteMaisAntigo = lotes.Peek();

                if (loteMaisAntigo.Qtde <= restanteParaVender)
                {
                    restanteParaVender -= loteMaisAntigo.Qtde;
                    lotes.Dequeue(); // Lote zerado, sai da fila
                }
                else
                {
                    loteMaisAntigo.Qtde -= restanteParaVender;
                    restanteParaVender = 0;
                }
            }

            return true;
        }

        public override string ToString()
        {
            return $"{id}-{nome}-{laboratorio}-{QtdeDisponivel()}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            
            Medicamento outro = (Medicamento)obj;
            return this.id == outro.id;
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }
    }

    class Medicamentos
    {
        private List<Medicamento> listaMedicamentos;

        public List<Medicamento> ListaMedicamentos { get => listaMedicamentos; }

        public Medicamentos()
        {
            listaMedicamentos = new List<Medicamento>();
        }

        public void Adicionar(Medicamento medicamento)
        {
            listaMedicamentos.Add(medicamento);
        }

        public bool Deletar(Medicamento medicamento)
        {
            Medicamento medEncontrado = Pesquisar(medicamento);
            
            if (medEncontrado.Id != 0 && medEncontrado.QtdeDisponivel() == 0)
            {
                listaMedicamentos.Remove(medEncontrado);
                return true;
            }
            return false;
        }

        public Medicamento Pesquisar(Medicamento medicamento)
        {
            foreach (var med in listaMedicamentos)
            {
                if (med.Equals(medicamento))
                {
                    return med;
                }
            }
            return new Medicamento(); // Retorna objeto vazio se não encontrar
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Medicamentos controleMedicamentos = new Medicamentos();
            int opcao = -1;

            while (opcao != 0)
            {
                Console.WriteLine("\n==============================");
                Console.WriteLine("    CONTROLE DE FARMÁCIA");
                Console.WriteLine("==============================");
                Console.WriteLine("0. Finalizar processo");
                Console.WriteLine("1. Cadastrar medicamento");
                Console.WriteLine("2. Consultar medicamento (Sintético)");
                Console.WriteLine("3. Consultar medicamento (Analítico)");
                Console.WriteLine("4. Comprar medicamento (Cadastrar Lote)");
                Console.WriteLine("5. Vender medicamento");
                Console.WriteLine("6. Listar medicamentos");
                Console.Write("Escolha uma opção: ");
                
                if (!int.TryParse(Console.ReadLine(), out opcao)) continue;

                switch (opcao)
                {
                    case 1: // Cadastrar medicamento
                        Console.Write("ID do medicamento: ");
                        int idCad = int.Parse(Console.ReadLine());
                        Console.Write("Nome do medicamento: ");
                        string nomeCad = Console.ReadLine();
                        Console.Write("Laboratório: ");
                        string labCad = Console.ReadLine();

                        Medicamento novoMed = new Medicamento(idCad, nomeCad, labCad);
                        if (controleMedicamentos.Pesquisar(novoMed).Id == 0)
                        {
                            controleMedicamentos.Adicionar(novoMed);
                            Console.WriteLine("Medicamento cadastrado com sucesso!");
                        }
                        else
                        {
                            Console.WriteLine("Erro: Medicamento com este ID já existe.");
                        }
                        break;

                    case 2: // Consultar medicamento (sintético)
                        Console.Write("ID do medicamento a consultar: ");
                        int idSint = int.Parse(Console.ReadLine());
                        
                        Medicamento medSint = controleMedicamentos.Pesquisar(new Medicamento { Id = idSint });
                        if (medSint.Id != 0)
                            Console.WriteLine(medSint.ToString());
                        else
                            Console.WriteLine("Medicamento não encontrado.");
                        break;

                    case 3: // Consultar medicamento (analítico)
                        Console.Write("ID do medicamento a consultar: ");
                        int idAnalitico = int.Parse(Console.ReadLine());
                        
                        Medicamento medAnalitico = controleMedicamentos.Pesquisar(new Medicamento { Id = idAnalitico });
                        if (medAnalitico.Id != 0)
                        {
                            Console.WriteLine(medAnalitico.ToString());
                            Console.WriteLine("Lotes disponíveis:");
                            foreach (var lote in medAnalitico.Lotes)
                            {
                                Console.WriteLine("  " + lote.ToString());
                            }
                        }
                        else
                        {
                            Console.WriteLine("Medicamento não encontrado.");
                        }
                        break;

                    case 4: // Comprar medicamento
                        Console.Write("ID do medicamento: ");
                        int idCompra = int.Parse(Console.ReadLine());
                        
                        Medicamento medCompra = controleMedicamentos.Pesquisar(new Medicamento { Id = idCompra });
                        if (medCompra.Id != 0)
                        {
                            Console.Write("ID do lote: ");
                            int idLote = int.Parse(Console.ReadLine());
                            Console.Write("Quantidade: ");
                            int qtdeLote = int.Parse(Console.ReadLine());
                            Console.Write("Data de vencimento (dd/mm/aaaa): ");
                            DateTime vencLote = DateTime.Parse(Console.ReadLine());

                            medCompra.Comprar(new Lote(idLote, qtdeLote, vencLote));
                            Console.WriteLine("Lote adicionado com sucesso!");
                        }
                        else
                        {
                            Console.WriteLine("Medicamento não encontrado.");
                        }
                        break;

                    case 5: // Vender medicamento
                        Console.Write("ID do medicamento: ");
                        int idVenda = int.Parse(Console.ReadLine());
                        
                        Medicamento medVenda = controleMedicamentos.Pesquisar(new Medicamento { Id = idVenda });
                        if (medVenda.Id != 0)
                        {
                            Console.Write("Quantidade a vender: ");
                            int qtdeVenda = int.Parse(Console.ReadLine());

                            if (medVenda.Vender(qtdeVenda))
                                Console.WriteLine("Venda realizada com sucesso!");
                            else
                                Console.WriteLine("Erro: Quantidade indisponível em estoque.");
                        }
                        else
                        {
                            Console.WriteLine("Medicamento não encontrado.");
                        }
                        break;

                    case 6: // Listar medicamentos
                        Console.WriteLine("Lista de Medicamentos:");
                        foreach (var m in controleMedicamentos.ListaMedicamentos)
                        {
                            Console.WriteLine(m.ToString());
                        }
                        break;

                    case 0:
                        Console.WriteLine("Saindo do sistema...");
                        break;

                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
        }
    }
}
