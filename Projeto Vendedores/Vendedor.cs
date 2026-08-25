using System;

namespace ProjetoVendedores
{
    public class Vendedor
    {
        private int id;
        private string nome;
        private double percComissao;
        private Venda[] asVendas;

        public Vendedor(int id, string nome, double percComissao)
        {
            this.id = id;
            this.nome = nome;
            this.percComissao = percComissao;
            this.asVendas = new Venda[31];
        }

        public int Id
        {
            get { return id; }
        }

        public string Nome
        {
            get { return nome; }
        }

        public double PercComissao
        {
            get { return percComissao; }
        }

        public void registrarVenda(int dia, Venda venda)
        {
            if (dia >= 1 && dia <= 31)
            {
                asVendas[dia - 1] = venda;
            }
        }

        public double valorVendas()
        {
            double total = 0;

            for (int i = 0; i < asVendas.Length; i++)
            {
                if (asVendas[i] != null)
                {
                    total += asVendas[i].Valor;
                }
            }

            return total;
        }

        public double valorComissao()
        {
            return valorVendas() * (percComissao / 100);
        }

        public double valorMedioVendas()
        {
            double totalMedio = 0;
            int diasComVenda = 0;

            for (int i = 0; i < asVendas.Length; i++)
            {
                if (asVendas[i] != null)
                {
                    totalMedio += asVendas[i].valorMedio();
                    diasComVenda++;
                }
            }

            if (diasComVenda == 0)
                return 0;

            return totalMedio / diasComVenda;
        }

        public bool possuiVendas()
        {
            for (int i = 0; i < asVendas.Length; i++)
            {
                if (asVendas[i] != null)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
