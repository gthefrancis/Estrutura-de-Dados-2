using System.Collections.Generic;
using System.Linq;

public class Projetos
{
    private List<Projeto> itens;

    public Projetos()
    {
        itens = new List<Projeto>();
    }

    public bool adicionar(Projeto p)
    {
        if (buscar(p) == null)
        {
            itens.Add(p);
            return true;
        }
        return false;
    }

    public bool remover(Projeto p)
    {
        Projeto existente = buscar(p);
        if (existente != null)
        {
            itens.Remove(existente);
            return true;
        }
        return false;
    }

    public Projeto buscar(Projeto p)
    {
        return itens.FirstOrDefault(x => x.Equals(p));
    }

    public List<Projeto> listar()
    {
        return itens;
    }
}
