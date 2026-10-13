using System.Collections.Generic;
using System.Linq;

public class Projeto
{
    private int id;
    private string nome;
    private List<Tarefa> tarefas;

    public int Id { get => id; set => id = value; }
    public string Nome { get => nome; set => nome = value; }
    public List<Tarefa> Tarefas { get => tarefas; }

    public Projeto(int id, string nome)
    {
        this.id = id;
        this.nome = nome;
        this.tarefas = new List<Tarefa>();
    }

    public Projeto(int id)
    {
        this.id = id;
    }

    public void adicionarTarefa(Tarefa t)
    {
        if (buscarTarefa(t) == null)
        {
            tarefas.Add(t);
        }
    }

    public bool removerTarefa(Tarefa t)
    {
        Tarefa existente = buscarTarefa(t);
        if (existente != null)
        {
            tarefas.Remove(existente);
            return true;
        }
        return false;
    }

    public Tarefa buscarTarefa(Tarefa t)
    {
        return tarefas.FirstOrDefault(x => x.Equals(t));
    }

    public List<Tarefa> tarefasPorStatus(string s)
    {
        return tarefas.Where(x => x.Status.Equals(s, System.StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public List<Tarefa> tarefasPorPrioridade(int p)
    {
        return tarefas.Where(x => x.Prioridade == p).ToList();
    }

    public int totalAbertas()
    {
        return tarefasPorStatus("Aberta").Count;
    }

    public int totalFechadas()
    {
        return tarefasPorStatus("Fechada").Count;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType()) return false;
        return id == ((Projeto)obj).id;
    }

    public override int GetHashCode()
    {
        return id.GetHashCode();
    }
}
