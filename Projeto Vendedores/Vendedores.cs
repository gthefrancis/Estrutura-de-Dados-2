using ProjetoVendedores;
using System;

namespace ProjetoVendedores
{
    public class Vendedores
    {
        private Vendedor[] osVendedores;
        private int max;
        private int qtde;

        public Vendedores(int max)
        {
            this.max = max;
            this.qtde = 0;
            this.osVendedores = new Vendedor[max];
        }

        public bool addVendedor(Vendedor v)
        {
            if (qtde >= max)
            {
                return false;
            }

            osVendedores[qtde] = v;
            qtde++;

            return true;
        }

        public bool delVendedor(Vendedor v)
        {
            int posicao = -1;

            for (int i = 0; i < qtde; i++)
            {
                if (osVendedores[i].Id == v.Id)
                {
                    posicao = i;
                    break;
                }
            }

            if (posicao == -1)
            {
                return false;
            }

            if (osVendedores[posicao].possuiVendas())
            {
                return false;
            }

            for (int i = posicao; i < qtde - 1; i++)
            {
                osVendedores[i] = osVendedores[i + 1];
            }

            osVendedores[qtde - 1] = null;
            qtde--;

            return true;
        }

        public Vendedor searchVendedor(Vendedor v)
        {
            for (int i = 0; i < qtde; i++)
            {
                if (osVendedores[i].Id == v.Id)
                {
                    return osVendedores[i];
                }
            }

            return null;
        }

        public double valorVendas()
        {
            double total = 0;

            for (int i = 0; i < qtde; i++)
            {
                total += osVendedores[i].valorVendas();
            }

            return total;
        }

        public double valorComissao()
        {
            double total = 0;

            for (int i = 0; i < qtde; i++)
            {
                total += osVendedores[i].valorComissao();
            }

            return total;
        }

        public int Quantidade
        {
            get { return qtde; }
        }

        public Vendedor getVendedor(int posicao)
        {
            if (posicao >= 0 && posicao < qtde)
            {
                return osVendedores[posicao];
            }

            return null;
        }
    }
}
