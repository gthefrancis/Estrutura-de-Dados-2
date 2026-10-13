public class Item
{
    private int id;
    private string descricao;
    private double preco;

    public int Id { get => id; set => id = value; }
    public string Descricao { get => descricao; set => descricao = value; }
    public double Preco { get => preco; set => preco = value; }

    public Item(int id, string descricao, double preco)
    {
        this.id = id;
        this.descricao = descricao;
        this.preco = preco;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType()) return false;
        Item outro = (Item)obj;
        return this.id == outro.id;
    }

    public override int GetHashCode()
    {
        return id.GetHashCode();
    }
}
