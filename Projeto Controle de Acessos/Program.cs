using System;
using System.Linq;

class Program
{
    static void Main()
    {
        Cadastro cadastro = new Cadastro();
        
        // 1. O PULO DO GATO: Carrega o JSON pra memória assim que o programa abre
        cadastro.download(); 

        int opcao = -1;

        while (opcao != 0)
        {
            Console.Clear();
            Console.WriteLine("=== SISTEMA DE CONTROLE DE ACESSO ===");
            Console.WriteLine("1. Cadastrar ambiente");
            Console.WriteLine("2. Consultar ambiente");
            Console.WriteLine("3. Excluir ambiente");
            Console.WriteLine("4. Cadastrar usuário");
            Console.WriteLine("5. Consultar usuário");
            Console.WriteLine("6. Excluir usuário");
            Console.WriteLine("7. Conceder permissão de acesso");
            Console.WriteLine("8. Revogar permissão de acesso");
            Console.WriteLine("9. Registrar acesso");
            Console.WriteLine("10. Consultar logs de acesso");
            Console.WriteLine("0. Sair");
            Console.Write("Escolha uma opção: ");

            // Previne que o programa exploda se digitar letra
            if (!int.TryParse(Console.ReadLine(), out opcao)) continue;

            Console.Clear();

            switch (opcao)
            {
                case 1: 
                    Console.Write("ID do Ambiente: ");
                    int idAmb = int.Parse(Console.ReadLine());
                    Console.Write("Nome do Ambiente: ");
                    string nomeAmb = Console.ReadLine();
                    
                    cadastro.adicionarAmbiente(new Ambiente { Id = idAmb, Nome = nomeAmb });
                    Console.WriteLine("Ambiente cadastrado!");
                    break;

                case 2: 
                    Console.Write("ID do Ambiente: ");
                    var ambEncontrado = cadastro.pesquisarAmbiente(new Ambiente { Id = int.Parse(Console.ReadLine()) });
                    
                    if (ambEncontrado != null) Console.WriteLine($"Encontrado: {ambEncontrado.Nome}");
                    else Console.WriteLine("Ambiente não encontrado.");
                    break;

                case 3: 
                    Console.Write("ID do Ambiente a excluir: ");
                    bool ambRemovido = cadastro.removerAmbiente(new Ambiente { Id = int.Parse(Console.ReadLine()) });
                    Console.WriteLine(ambRemovido ? "Removido com sucesso!" : "Ambiente não encontrado.");
                    break;

                case 4: 
                    Console.Write("ID do Usuário: ");
                    int idUsu = int.Parse(Console.ReadLine());
                    Console.Write("Nome do Usuário: ");
                    string nomeUsu = Console.ReadLine();
                    
                    cadastro.adicionarUsuario(new Usuario { Id = idUsu, Nome = nomeUsu });
                    Console.WriteLine("Usuário cadastrado!");
                    break;

                case 5: 
                    Console.Write("ID do Usuário: ");
                    var usuEncontrado = cadastro.pesquisarUsuario(new Usuario { Id = int.Parse(Console.ReadLine()) });
                    
                    if (usuEncontrado != null) Console.WriteLine($"Encontrado: {usuEncontrado.Nome}");
                    else Console.WriteLine("Usuário não encontrado.");
                    break;

                case 6: 
                    Console.Write("ID do Usuário a excluir: ");
                    bool usuRemovido = cadastro.removerUsuario(new Usuario { Id = int.Parse(Console.ReadLine()) });
                    
                    // Tratando a regra de negócio da atividade
                    if (usuRemovido) 
                        Console.WriteLine("Removido com sucesso!");
                    else 
                        Console.WriteLine("FALHA: Usuário não encontrado ou possui permissões ativas (precisa revogar antes).");
                    break;

                case 7: 
                    Console.Write("ID do Usuário: ");
                    var usuPerm = cadastro.pesquisarUsuario(new Usuario { Id = int.Parse(Console.ReadLine()) });
                    Console.Write("ID do Ambiente: ");
                    var ambPerm = cadastro.pesquisarAmbiente(new Ambiente { Id = int.Parse(Console.ReadLine()) });

                    if (usuPerm != null && ambPerm != null)
                    {
                        bool concedeu = usuPerm.concederPermissao(ambPerm);
                        Console.WriteLine(concedeu ? "Permissão concedida!" : "Usuário já tem permissão.");
                    }
                    else Console.WriteLine("Usuário ou Ambiente inválido.");
                    break;

                case 8: 
                    Console.Write("ID do Usuário: ");
                    var usuRev = cadastro.pesquisarUsuario(new Usuario { Id = int.Parse(Console.ReadLine()) });
                    Console.Write("ID do Ambiente: ");
                    var ambRev = new Ambiente { Id = int.Parse(Console.ReadLine()) };

                    if (usuRev != null)
                    {
                        bool revogou = usuRev.revogarPermissao(ambRev);
                        Console.WriteLine(revogou ? "Permissão revogada!" : "Usuário não tinha permissão.");
                    }
                    else Console.WriteLine("Usuário inválido.");
                    break;

                case 9: 
                    Console.Write("ID do Usuário: ");
                    var usuAcesso = cadastro.pesquisarUsuario(new Usuario { Id = int.Parse(Console.ReadLine()) });
                    Console.Write("ID do Ambiente: ");
                    var ambAcesso = cadastro.pesquisarAmbiente(new Ambiente { Id = int.Parse(Console.ReadLine()) });
                    
                    if (usuAcesso != null && ambAcesso != null)
                    {
                        bool autorizado = usuAcesso.Ambientes.Any(a => a.Id == ambAcesso.Id);

                        var novoLog = new Log
                        {
                            DtAcesso = DateTime.Now,
                            Usuario = usuAcesso,
                            TipoAcesso = autorizado 
                        };

                        ambAcesso.registrarLog(novoLog);
                        
                        if (autorizado) Console.WriteLine("ACESSO AUTORIZADO! Log registrado.");
                        else Console.WriteLine("ACESSO NEGADO! Tentativa registrada no log.");
                    }
                    else Console.WriteLine("Usuário ou Ambiente inválido.");
                    break;

                case 10: 
                    Console.Write("ID do Ambiente: ");
                    var ambLogs = cadastro.pesquisarAmbiente(new Ambiente { Id = int.Parse(Console.ReadLine()) });

                    if (ambLogs != null)
                    {
                        Console.Write("Filtrar (1-Autorizados, 2-Negados, 3-Todos): ");
                        string filtro = Console.ReadLine();

                        var logsFiltrados = ambLogs.Logs.AsEnumerable();

                        if (filtro == "1") logsFiltrados = logsFiltrados.Where(l => l.TipoAcesso == true);
                        else if (filtro == "2") logsFiltrados = logsFiltrados.Where(l => l.TipoAcesso == false);

                        Console.WriteLine($"\n--- LOGS: {ambLogs.Nome} ---");
                        
                        if (!logsFiltrados.Any()) Console.WriteLine("Nenhum log encontrado.");
                        
                        foreach (var log in logsFiltrados)
                        {
                            string status = log.TipoAcesso ? "AUTORIZADO" : "NEGADO";
                            Console.WriteLine($"[{log.DtAcesso}] {log.Usuario.Nome} - {status}");
                        }
                    }
                    else Console.WriteLine("Ambiente inválido.");
                    break;
                    
                case 0:
                    Console.WriteLine("Salvando dados no arquivo JSON...");
                    // 2. O PULO DO GATO 2: Salva tudo de uma vez antes de fechar a tela preta
                    cadastro.upload(); 
                    Console.WriteLine("Sistema encerrado com sucesso. Pode fechar!");
                    break;

                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }

            if (opcao != 0)
            {
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }
    }
}
