using System;
using System.Collections.Generic;
using System.Linq;

public class Usuario
{
    private int id;
    private string nome;
    private List<Ambiente> ambientes = new();

    // Propriedades para permitir leitura externa sem violar os métodos da UML
    public int Id { get => id; set => id = value; }
    public string Nome { get => nome; set => nome = value; }
    public List<Ambiente> Ambientes { get => ambientes; }

    public bool concederPermissao(Ambiente ambiente)
    {
        // Em vez de Contains(), compara o Id manualmente
        if (!ambientes.Any(a => a.Id == ambiente.Id))
        {
            ambientes.Add(ambiente);
            return true;
        }
        return false;
    }

    public bool revogarPermissao(Ambiente ambiente)
    {
        // Acha a referência original na lista usando o Id do objeto que chegou
        var ambienteReal = ambientes.FirstOrDefault(a => a.Id == ambiente.Id);

        if (ambienteReal != null)
        {
            return ambientes.Remove(ambienteReal);
        }
        return false;
    }
}

public class Ambiente
{
    private int id;
    private string nome;
    private Queue<Log> logs = new();

    public int Id { get => id; set => id = value; }
    public string Nome { get => nome; set => nome = value; }
    public Queue<Log> Logs { get => logs; }

    public void registrarLog(Log log)
    {
        if (logs.Count >= 100)
        {
            logs.Dequeue(); // Tira o mais velho
        }
        logs.Enqueue(log); // Coloca o novo
    }
}

public class Log
{
    private DateTime dtAcesso;
    private Usuario usuario;
    private bool tipoAcesso;

    public DateTime DtAcesso { get => dtAcesso; set => dtAcesso = value; }
    public Usuario Usuario { get => usuario; set => usuario = value; }
    public bool TipoAcesso { get => tipoAcesso; set => tipoAcesso = value; }
}

public class Cadastro
{
    private List<Usuario> usuarios = new();
    private List<Ambiente> ambientes = new();

    public void adicionarUsuario(Usuario usuario)
    {
        if (!usuarios.Any(u => u.Id == usuario.Id))
        {
            usuarios.Add(usuario);
        }
    }

    public bool removerUsuario(Usuario usuario)
    {
        // Reutiliza a pesquisa para achar o objeto verdadeiro na memória
        var usuarioReal = pesquisarUsuario(usuario);
        if (usuarioReal != null)
        {
            return usuarios.Remove(usuarioReal);
        }
        return false;
    }

    public Usuario pesquisarUsuario(Usuario usuario)
    {
        // Compara direto pelo Id em vez de depender do objeto inteiro
        return usuarios.FirstOrDefault(u => u.Id == usuario.Id);
    }

    public void adicionarAmbiente(Ambiente ambiente)
    {
        if (!ambientes.Any(a => a.Id == ambiente.Id))
        {
            ambientes.Add(ambiente);
        }
    }

    public bool removerAmbiente(Ambiente ambiente)
    {
        var ambienteReal = pesquisarAmbiente(ambiente);
        if (ambienteReal != null)
        {
            return ambientes.Remove(ambienteReal);
        }
        return false;
    }

    public Ambiente pesquisarAmbiente(Ambiente ambiente)
    {
        return ambientes.FirstOrDefault(a => a.Id == ambiente.Id);
    }

    public void upload() { }
    public void download() { }
}
