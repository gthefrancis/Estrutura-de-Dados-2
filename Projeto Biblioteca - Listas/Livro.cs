using System;
using System.Collections.Generic;
using System.Linq;

public class Livro
{
    private int isbn;
    private string titulo;
    private string autor;
    private string editora;
    private List<Exemplar> exemplares;

    public int Isbn { get => isbn; set => isbn = value; }
    public string Titulo { get => titulo; set => titulo = value; }
    public string Autor { get => autor; set => autor = value; }
    public string Editora { get => editora; set => editora = value; }
    public List<Exemplar> Exemplares { get => exemplares; }

    public Livro(int isbn, string titulo, string autor, string editora)
    {
        this.isbn = isbn;
        this.titulo = titulo;
        this.autor = autor;
        this.editora = editora;
        this.exemplares = new List<Exemplar>();
    }

    // Construtor vazio útil para realizar pesquisas (instanciando apenas com o ISBN)
    public Livro(int isbn)
    {
        this.isbn = isbn;
    }

    public void adicionarExemplar(Exemplar exemplar)
    {
        exemplares.Add(exemplar);
    }

    public int qtdeExemplares()
    {
        return exemplares.Count;
    }

    public int qtdeDisponiveis()
    {
        return exemplares.Count(e => e.disponivel());
    }

    public int qtdeEmprestimos()
    {
        return exemplares.Sum(e => e.qtdeEmprestimos());
    }

    public double percDisponibilidade()
    {
        if (qtdeExemplares() == 0) return 0.0;
        return ((double)qtdeDisponiveis() / qtdeExemplares()) * 100.0;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType()) return false;
        Livro outro = (Livro)obj;
        return this.isbn == outro.isbn;
    }

    public override int GetHashCode()
    {
        return isbn.GetHashCode();
    }
}
