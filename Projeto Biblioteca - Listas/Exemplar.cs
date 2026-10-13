using System;
using System.Collections.Generic;
using System.Linq;

public class Exemplar
{
    private int tombo;
    private List<Emprestimo> emprestimos;

    public int Tombo { get => tombo; set => tombo = value; }
    public List<Emprestimo> Emprestimos { get => emprestimos; }

    public Exemplar(int tombo)
    {
        this.tombo = tombo;
        this.emprestimos = new List<Emprestimo>();
    }

    public bool disponivel()
    {
        // Se não tem empréstimos ou se o último empréstimo tem data de devolução preenchida
        if (emprestimos.Count == 0) return true;
        
        Emprestimo ultimo = emprestimos.Last();
        return ultimo.DtDevolucao != DateTime.MinValue;
    }

    public bool emprestar()
    {
        if (disponivel())
        {
            emprestimos.Add(new Emprestimo());
            return true;
        }
        return false;
    }

    public bool devolver()
    {
        if (!disponivel())
        {
            Emprestimo ultimo = emprestimos.Last();
            ultimo.DtDevolucao = DateTime.Now;
            return true;
        }
        return false;
    }

    public int qtdeEmprestimos()
    {
        return emprestimos.Count;
    }
}
