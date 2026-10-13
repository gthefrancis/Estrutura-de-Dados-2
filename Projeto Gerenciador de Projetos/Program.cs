using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static Projetos gestaoProjetos = new Projetos();

    static void Main(string[] args)
    {
        int opcao;
        do
        {
            Console.Clear();
            Console.WriteLine("=== SISTEMA DE GESTÃO DE PROJETOS ===");
            Console.WriteLine("0. Sair");
            Console.WriteLine("1. Adicionar projeto");
            Console.WriteLine("2. Pesquisar projeto (mostrar tarefas por status e totais)");
            Console.WriteLine("3. Remover projeto (apenas se sem tarefas)");
            Console.WriteLine("4. Adicionar tarefa em projeto");
            Console.WriteLine("5. Concluir tarefa");
            Console.WriteLine("6. Cancelar tarefa");
            Console.WriteLine("7. Reabrir tarefa");
            Console.WriteLine("8. Listar tarefas de um projeto");
            Console.WriteLine("9. Filtrar tarefas por status ou prioridade em um projeto");
            Console.WriteLine("10. Filtrar tarefas por status/prioridade em todos os projetos");
            Console.WriteLine("11. Resumo geral");
            Console.Write("Escolha uma opção: ");

            if (int.TryParse(Console.ReadLine(), out opcao))
            {
                Console.Clear();
                switch (opcao)
                {
                    case 0: Console.WriteLine("Encerrando..."); break;
                    case 1: AdicionarProjeto(); break;
                    case 2: PesquisarProjeto(); break;
                    case 3: RemoverProjeto(); break;
                    case 4: AdicionarTarefa(); break;
                    case 5: MudarStatusTarefa("concluir"); break;
                    case 6: MudarStatusTarefa("cancelar"); break;
                    case 7: MudarStatusTarefa("reabrir"); break;
                    case 8: ListarTarefasProjeto(); break;
                    case 9: FiltrarTarefasEmUmProjeto(); break;
                    case 10: FiltrarTarefasTodosProjetos(); break;
                    case 11: ResumoGeral(); break;
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
                Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
                Console.ReadKey();
            }

        } while (opcao != 0);
    }

    static void AdicionarProjeto()
    {
        Console.WriteLine("-- ADICIONAR PROJETO --");
        Console.Write("ID do Projeto: ");
        int id = int.Parse(Console.ReadLine());
        Console.Write("Nome do Projeto: ");
        string nome = Console.ReadLine();

        if (gestaoProjetos.adicionar(new Projeto(id, nome)))
            Console.WriteLine("Projeto adicionado com sucesso!");
        else
            Console.WriteLine("Erro: Já existe um projeto com este ID.");
    }

    static void PesquisarProjeto()
    {
        Console.WriteLine("-- PESQUISAR PROJETO --");
        Console.Write("ID do Projeto: ");
        Projeto p = gestaoProjetos.buscar(new Projeto(int.Parse(Console.ReadLine())));

        if (p != null)
        {
            Console.WriteLine($"\nProjeto Encontrado: {p.Nome} (ID: {p.Id})");
            Console.WriteLine($"Total de Tarefas Abertas: {p.totalAbertas()}");
            Console.WriteLine($"Total de Tarefas Fechadas: {p.totalFechadas()}");
            Console.WriteLine($"Total de Tarefas Canceladas: {p.tarefasPorStatus("Cancelada").Count}");

            Console.WriteLine("\n-- Tarefas --");
            if (p.Tarefas.Count == 0) Console.WriteLine("Nenhuma tarefa neste projeto.");
            foreach (var t in p.Tarefas.OrderBy(x => x.Status))
            {
                Console.WriteLine(t.ToString());
            }
        }
        else Console.WriteLine("Projeto não encontrado.");
    }

    static void RemoverProjeto()
    {
        Console.WriteLine("-- REMOVER PROJETO --");
        Console.Write("ID do Projeto: ");
        Projeto p = gestaoProjetos.buscar(new Projeto(int.Parse(Console.ReadLine())));

        if (p != null)
        {
            if (p.Tarefas.Count == 0)
            {
                gestaoProjetos.remover(p);
                Console.WriteLine("Projeto removido com sucesso.");
            }
            else
            {
                Console.WriteLine($"Erro: O projeto não pode ser removido porque possui {p.Tarefas.Count} tarefa(s).");
            }
        }
        else Console.WriteLine("Projeto não encontrado.");
    }

    static void AdicionarTarefa()
    {
        Console.WriteLine("-- ADICIONAR TAREFA --");
        Console.Write("ID do Projeto destino: ");
        Projeto p = gestaoProjetos.buscar(new Projeto(int.Parse(Console.ReadLine())));

        if (p != null)
        {
            Console.Write("ID da Tarefa: ");
            int idTask = int.Parse(Console.ReadLine());

            if (p.buscarTarefa(new Tarefa(idTask)) != null)
            {
                Console.WriteLine("Erro: ID de tarefa já existente neste projeto.");
                return;
            }

            Console.Write("Título: ");
            string titulo = Console.ReadLine();
            Console.Write("Descrição: ");
            string desc = Console.ReadLine();
            Console.Write("Prioridade (1=Alta, 2=Média, 3=Baixa): ");
            int prio = int.Parse(Console.ReadLine());

            p.adicionarTarefa(new Tarefa(idTask, titulo, desc, prio));
            Console.WriteLine("Tarefa adicionada com sucesso!");
        }
        else Console.WriteLine("Projeto não encontrado.");
    }

    static void MudarStatusTarefa(string acao)
    {
        Console.WriteLine($"-- {acao.ToUpper()} TAREFA --");
        Console.Write("ID do Projeto: ");
        Projeto p = gestaoProjetos.buscar(new Projeto(int.Parse(Console.ReadLine())));

        if (p != null)
        {
            Console.Write("ID da Tarefa: ");
            Tarefa t = p.buscarTarefa(new Tarefa(int.Parse(Console.ReadLine())));

            if (t != null)
            {
                if (acao == "concluir") t.concluir();
                else if (acao == "cancelar") t.cancelar();
                else if (acao == "reabrir") t.reabrir();

                Console.WriteLine($"Tarefa atualizada com sucesso! Novo status: {t.Status}");
            }
            else Console.WriteLine("Tarefa não encontrada.");
        }
        else Console.WriteLine("Projeto não encontrado.");
    }

    static void ListarTarefasProjeto()
    {
        Console.WriteLine("-- LISTAR TAREFAS DE UM PROJETO --");
        Console.Write("ID do Projeto: ");
        Projeto p = gestaoProjetos.buscar(new Projeto(int.Parse(Console.ReadLine())));

        if (p != null)
        {
            if (p.Tarefas.Count == 0) Console.WriteLine("Nenhuma tarefa cadastrada.");
            foreach (var t in p.Tarefas) Console.WriteLine(t.ToString());
        }
        else Console.WriteLine("Projeto não encontrado.");
    }

    static void FiltrarTarefasEmUmProjeto()
    {
        Console.WriteLine("-- FILTRAR TAREFAS (UM PROJETO) --");
        Console.Write("ID do Projeto: ");
        Projeto p = gestaoProjetos.buscar(new Projeto(int.Parse(Console.ReadLine())));

        if (p != null)
        {
            Console.Write("Filtrar por (1) Status ou (2) Prioridade? ");
            int tipo = int.Parse(Console.ReadLine());

            List<Tarefa> resultado = new List<Tarefa>();

            if (tipo == 1)
            {
                Console.Write("Digite o Status (Aberta, Fechada, Cancelada): ");
                resultado = p.tarefasPorStatus(Console.ReadLine());
            }
            else if (tipo == 2)
            {
                Console.Write("Digite a Prioridade (1=Alta, 2=Média, 3=Baixa): ");
                resultado = p.tarefasPorPrioridade(int.Parse(Console.ReadLine()));
            }

            Console.WriteLine($"\nEncontradas: {resultado.Count}");
            foreach (var t in resultado) Console.WriteLine(t.ToString());
        }
        else Console.WriteLine("Projeto não encontrado.");
    }

    static void FiltrarTarefasTodosProjetos()
    {
        Console.WriteLine("-- FILTRAR TAREFAS (TODOS OS PROJETOS) --");
        Console.Write("Filtrar por (1) Status ou (2) Prioridade? ");
        int tipo = int.Parse(Console.ReadLine());

        string filtroStatus = "";
        int filtroPrio = 0;

        if (tipo == 1)
        {
            Console.Write("Digite o Status (Aberta, Fechada, Cancelada): ");
            filtroStatus = Console.ReadLine();
        }
        else if (tipo == 2)
        {
            Console.Write("Digite a Prioridade (1=Alta, 2=Média, 3=Baixa): ");
            filtroPrio = int.Parse(Console.ReadLine());
        }

        foreach (var p in gestaoProjetos.listar())
        {
            var filtradas = tipo == 1 ? p.tarefasPorStatus(filtroStatus) : p.tarefasPorPrioridade(filtroPrio);
            if (filtradas.Count > 0)
            {
                Console.WriteLine($"\nProjeto: {p.Nome} (ID {p.Id})");
                foreach (var t in filtradas) Console.WriteLine("  " + t.ToString());
            }
        }
    }

    static void ResumoGeral()
    {
        Console.WriteLine("-- RESUMO GERAL DO SISTEMA --");
        var projetos = gestaoProjetos.listar();
        
        int qtdProjetos = projetos.Count;
        int abertas = 0, fechadas = 0, canceladas = 0;

        foreach (var p in projetos)
        {
            abertas += p.totalAbertas();
            fechadas += p.totalFechadas();
            canceladas += p.tarefasPorStatus("Cancelada").Count;
        }

        Console.WriteLine($"Total de Projetos: {qtdProjetos}");
        Console.WriteLine($"Tarefas Abertas: {abertas}");
        Console.WriteLine($"Tarefas Fechadas: {fechadas}");
        Console.WriteLine($"Tarefas Canceladas: {canceladas}");

        double percConcluidas = 0;
        if (abertas > 0)
        {
            // Percentual de concluídas em relação às abertas (Fechadas / Abertas * 100)
            // Se o entendimento for "Fechadas em relação ao total ativo", a fórmula seria fechadas / (abertas + fechadas)
            // Calculando concluídas sobre abertas conforme texto literal:
            percConcluidas = ((double)fechadas / abertas) * 100;
            Console.WriteLine($"% Concluídas em relação às Abertas: {percConcluidas:F2}%");
        }
        else if (fechadas > 0)
        {
             Console.WriteLine("% Concluídas em relação às Abertas: 100% (Não há tarefas abertas)");
        }
        else
        {
            Console.WriteLine("% Concluídas em relação às Abertas: 0%");
        }
    }
}
