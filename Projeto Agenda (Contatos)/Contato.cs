using System;
using System.Collections.Generic;
using System.Linq;

public class Contato
{
    private string email;
    private string nome;
    private Data dtNasc;
    private List<Telefone> telefones;

    public string Email { get => email; set => email = value; }
    public string Nome { get => nome; set => nome = value; }
    public Data DtNasc { get => dtNasc; set => dtNasc = value; }
    public List<Telefone> Telefones { get => telefones; }

    public Contato()
    {
        telefones = new List<Telefone>();
    }

    public Contato(string email) : this()
    {
        Email = email;
    }

    public Contato(string email, string nome, Data dtNasc) : this()
    {
        Email = email;
        Nome = nome;
        DtNasc = dtNasc;
    }

    public int getIdade()
    {
        DateTime hoje = DateTime.Today;
        int idade = hoje.Year - dtNasc.Ano;
        if (hoje.Month < dtNasc.Mes || (hoje.Month == dtNasc.Mes && hoje.Day < dtNasc.Dia))
        {
            idade--;
        }
        return idade;
    }

    public void adicionarTelefone(Telefone t)
    {
        telefones.Add(t);
    }

    public string getTelefonePrincipal()
    {
        Telefone telPrin = telefones.FirstOrDefault(t => t.Principal);
        return telPrin != null ? telPrin.Numero : "Sem telefone principal";
    }

    public override string ToString()
    {
        return $"Nome: {Nome} | Email: {Email} | Idade: {getIdade()} anos | Nasc: {DtNasc} | Tel Principal: {getTelefonePrincipal()}";
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType()) return false;
        Contato c = (Contato)obj;
        return Email.Equals(c.Email, StringComparison.OrdinalIgnoreCase); 
    }

    public override int GetHashCode()
    {
        return Email.GetHashCode();
    }
}
