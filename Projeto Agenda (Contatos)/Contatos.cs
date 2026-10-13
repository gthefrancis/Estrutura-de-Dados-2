using System.Collections.Generic;
using System.Linq;

public class Contatos
{
    private readonly List<Contato> agenda;

    public List<Contato> Agenda { get => agenda; }

    public Contatos()
    {
        agenda = new List<Contato>();
    }

    public bool adicionar(Contato c)
    {
        if (!agenda.Contains(c))
        {
            agenda.Add(c);
            return true;
        }
        return false;
    }

    public Contato pesquisar(Contato c)
    {
        return agenda.FirstOrDefault(x => x.Equals(c));
    }

    public bool alterar(Contato c)
    {
        Contato existente = pesquisar(c);
        if (existente != null)
        {
            existente.Nome = c.Nome;
            existente.DtNasc = c.DtNasc;
            existente.Telefones.Clear();
            foreach (var tel in c.Telefones)
            {
                existente.adicionarTelefone(tel);
            }
            return true;
        }
        return false;
    }

    public bool remover(Contato c)
    {
        Contato existente = pesquisar(c);
        if (existente != null)
        {
            agenda.Remove(existente);
            return true;
        }
        return false;
    }
}
