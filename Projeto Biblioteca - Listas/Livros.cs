using System.Collections.Generic;
using System.Linq;

public class Livros
{
    private List<Livro> acervo;

    public List<Livro> Acervo { get => acervo; }

    public Livros()
    {
        acervo = new List<Livro>();
    }

    public void adicionar(Livro livro)
    {
        if (pesquisar(livro) == null)
        {
            acervo.Add(livro);
        }
    }

    public Livro pesquisar(Livro livro)
    {
        return acervo.FirstOrDefault(l => l.Equals(livro));
    }
}
