public class Telefone
{
    private string tipo;
    private string numero;
    private bool principal;

    public string Tipo { get => tipo; set => tipo = value; }
    public string Numero { get => numero; set => numero = value; }
    public bool Principal { get => principal; set => principal = value; }

    public Telefone(string tipo, string numero, bool principal)
    {
        Tipo = tipo;
        Numero = numero;
        Principal = principal;
    }
}
