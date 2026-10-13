using System;
using System.Text;

public class Pedido
{
    private int id;
    private string cliente;
    private Item[] itens;

    public int Id { get => id; set => id = value; }
    public string Cliente { get => cliente; set => cliente = value; }
    public Item[] Itens { get => itens; }

    public Pedido(int id, string cliente)
    {
        this.id = id;
        this.cliente = cliente;
        this.itens = new Item[10]; // Limite de 10 itens por pedido
    }

    // Construtor auxiliar para realizar a busca no Equals
    public Pedido(int id)
    {
        this.id = id;
    }

    public bool adicionarItem(Item item)
    {
        // Procura a primeira posição vazia (null) no array
        for (int i = 0; i < itens.Length; i++)
        {
            if (itens[i] == null)
            {
                itens[i] = item;
                return true;
            }
        }
        return false; // Retorna false se o array de itens já estiver cheio
    }

    public bool removerItem(Item item)
    {
        for (int i = 0; i < itens.Length; i++)
        {
            if (itens[i] != null && itens[i].Equals(item))
            {
                itens[i] = null; // Libera o espaço (seta como null)
                return true;
            }
        }
        return false;
    }

    public double calcularTotal()
    {
        double total = 0;
        for (int i = 0; i < itens.Length; i++)
        {
            if (itens[i] != null)
            {
                total += itens[i].Preco;
            }
        }
        return total;
    }

    public string dadosDoPedido()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Pedido ID: {id} | Cliente: {cliente}");
        sb.AppendLine("Itens do Pedido:");
        
        bool possuiItens = false;
        for (int i = 0; i < itens.Length; i++)
        {
            if (itens[i] != null)
            {
                sb.AppendLine($"  - [{itens[i].Id}] {itens[i].Descricao} - R$ {itens[i].Preco:F2}");
                possuiItens = true;
            }
        }

        if (!possuiItens) sb.AppendLine("  (Nenhum item adicionado)");
        
        sb.AppendLine($"Valor Total: R$ {calcularTotal():F2}");
        return sb.ToString();
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType()) return false;
        return this.id == ((Pedido)obj).id;
    }

    public override int GetHashCode()
    {
        return id.GetHashCode();
    }
}
