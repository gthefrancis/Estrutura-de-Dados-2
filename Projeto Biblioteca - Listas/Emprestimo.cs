using System;

public class Emprestimo
{
    private DateTime dtEmprestimo;
    private DateTime dtDevolucao;

    public DateTime DtEmprestimo { get => dtEmprestimo; set => dtEmprestimo = value; }
    
    // Se dtDevolucao for DateTime.MinValue, significa que o livro ainda não foi devolvido
    public DateTime DtDevolucao { get => dtDevolucao; set => dtDevolucao = value; }

    public Emprestimo()
    {
        dtEmprestimo = DateTime.Now;
        dtDevolucao = DateTime.MinValue; // Representa estado "em aberto"
    }
}
