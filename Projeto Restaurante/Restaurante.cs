public class Restaurante
{
    private int proxPedido;
    private Pedido[] pedidos;

    // Propriedade para expor o número de pedidos ativos (útil na listagem)
    public int ProxPedido { get => proxPedido; }
    public Pedido[] Pedidos { get => pedidos; }

    public Restaurante()
    {
        this.proxPedido = 0;
        this.pedidos = new Pedido[50]; // Capacidade máxima de 50 pedidos diários
    }

    public bool novoPedido(Pedido pedido)
    {
        if (proxPedido < pedidos.Length)
        {
            pedidos[proxPedido] = pedido;
            proxPedido++;
            return true;
        }
        return false; // Retorna false se o limite diário de 50 pedidos foi atingido
    }

    public Pedido buscarPedido(Pedido pedido)
    {
        for (int i = 0; i < proxPedido; i++)
        {
            if (pedidos[i] != null && pedidos[i].Equals(pedido))
            {
                return pedidos[i];
            }
        }
        return null;
    }

    public bool cancelarPedido(Pedido pedido)
    {
        for (int i = 0; i < proxPedido; i++)
        {
            if (pedidos[i] != null && pedidos[i].Equals(pedido))
            {
                // Para não deixar "buracos" no array, deslocamos os elementos à esquerda
                for (int j = i; j < proxPedido - 1; j++)
                {
                    pedidos[j] = pedidos[j + 1];
                }
                pedidos[proxPedido - 1] = null;
                proxPedido--;
                return true;
            }
        }
        return false;
    }
}
