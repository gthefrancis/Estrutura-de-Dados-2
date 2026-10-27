using System;
using System.Collections.Generic;

namespace CentralAtendimento
{
    public class Senha
    {
        private int id;
        private DateTime dataGerac;
        private DateTime horaGerac;
        private DateTime dataAtend;
        private DateTime horaAtend;

        public int Id { get => id; set => id = value; }
        public DateTime DataAtend { get => dataAtend; set => dataAtend = value; }
        public DateTime HoraAtend { get => horaAtend; set => horaAtend = value; }

        public Senha(int id)
        {
            this.id = id;
            this.dataGerac = DateTime.Now.Date;
            this.horaGerac = DateTime.Now;
        }

        public string DadosParciais()
        {
            return $"{id} - {dataGerac:dd/MM/yyyy} - {horaGerac:HH:mm:ss}";
        }

        public string DadosCompletos()
        {
            return $"{id} - {dataGerac:dd/MM/yyyy} - {horaGerac:HH:mm:ss} - {dataAtend:dd/MM/yyyy} - {horaAtend:HH:mm:ss}";
        }
    }

    public class Senhas
    {
        private int proximoAtendimento;
        private Queue<Senha> filaSenhas;

        public Queue<Senha> FilaSenhas { get => filaSenhas; }

        public Senhas()
        {
            filaSenhas = new Queue<Senha>();
            proximoAtendimento = 1;
        }

        public void Gerar()
        {
            filaSenhas.Enqueue(new Senha(proximoAtendimento));
            proximoAtendimento++;
        }
    }

    public class Guiche
    {
        private int id;
        private Queue<Senha> atendimentos;

        public int Id { get => id; }
        public Queue<Senha> Atendimentos { get => atendimentos; }

        public Guiche()
        {
            this.id = 0;
            this.atendimentos = new Queue<Senha>();
        }

        public Guiche(int id)
        {
            this.id = id;
            this.atendimentos = new Queue<Senha>();
        }

        public bool Chamar(Queue<Senha> filaSenhas)
        {
            if (filaSenhas.Count > 0)
            {
                Senha s = filaSenhas.Dequeue();
                s.DataAtend = DateTime.Now.Date;
                s.HoraAtend = DateTime.Now;
                atendimentos.Enqueue(s);
                return true;
            }
            return false;
        }
    }

    public class Guiches
    {
        private List<Guiche> listaGuiches;

        public List<Guiche> ListaGuiches { get => listaGuiches; }

        public Guiches()
        {
            listaGuiches = new List<Guiche>();
        }

        public void Adicionar(Guiche guiche)
        {
            listaGuiches.Add(guiche);
        }
    }
}
