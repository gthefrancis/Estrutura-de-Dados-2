using System;

public class Tarefa
{
    private int id;
    private string titulo;
    private string descricao;
    private int prioridade; // 1=alta, 2=média, 3=baixa
    private string status; // "Aberta", "Fechada", "Cancelada"
    private DateTime dataCriacao;
    private DateTime? dataConclusao; // Usando nullable para quando não estiver concluída

    public int Id { get => id; set => id = value; }
    public string Titulo { get => titulo; set => titulo = value; }
    public string Descricao { get => descricao; set => descricao = value; }
    public int Prioridade { get => prioridade; set => prioridade = value; }
    public string Status { get => status; private set => status = value; }
    public DateTime DataCriacao { get => dataCriacao; }
    public DateTime? DataConclusao { get => dataConclusao; }

    // Construtor para nova tarefa
    public Tarefa(int id, string titulo, string descricao, int prioridade)
    {
        this.id = id;
        this.titulo = titulo;
        this.descricao = descricao;
        this.prioridade = prioridade;
        this.status = "Aberta";
        this.dataCriacao = DateTime.Now;
        this.dataConclusao = null;
    }

    // Construtor usado apenas para busca/pesquisa por ID
    public Tarefa(int id)
    {
        this.id = id;
    }

    public void concluir()
    {
        this.status = "Fechada";
        this.dataConclusao = DateTime.Now;
    }

    public void cancelar()
    {
        this.status = "Cancelada";
        this.dataConclusao = null;
    }

    public void reabrir()
    {
        this.status = "Aberta";
        this.dataConclusao = null;
    }

    public string ObterPrioridadeDesc()
    {
        return prioridade == 1 ? "Alta" : prioridade == 2 ? "Média" : "Baixa";
    }

    public override string ToString()
    {
        string dataFim = dataConclusao.HasValue ? dataConclusao.Value.ToString("dd/MM/yyyy") : "-";
        return $"[ID: {id}] {titulo} | Prioridade: {ObterPrioridadeDesc()} | Status: {status} | Criada: {dataCriacao:dd/MM/yyyy} | Fim: {dataFim}";
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType()) return false;
        return id == ((Tarefa)obj).id;
    }

    public override int GetHashCode()
    {
        return id.GetHashCode();
    }
}
