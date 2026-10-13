using System;
using System.Windows.Forms;

namespace AgendaContatosApp
{
    public partial class Form1 : Form
    {
        private Contatos minhaAgenda;

        public Form1()
        {
            InitializeComponent();
            minhaAgenda = new Contatos();
        }

        private Contato LerContatoDaTela()
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("O email é obrigatório para identificar o contato.");
                return null;
            }

            try
            {
                int dia = int.Parse(txtDia.Text);
                int mes = int.Parse(txtMes.Text);
                int ano = int.Parse(txtAno.Text);

                Data dtNasc = new Data(dia, mes, ano);
                Contato c = new Contato(txtEmail.Text, txtNome.Text, dtNasc);

                // Adiciona o telefone digitado na tela, se houver
                if (!string.IsNullOrWhiteSpace(txtTelNumero.Text))
                {
                    Telefone t = new Telefone(txtTelTipo.Text, txtTelNumero.Text, chkPrincipal.Checked);
                    c.adicionarTelefone(t);
                }

                return c;
            }
            catch (Exception)
            {
                MessageBox.Show("Verifique se os campos de data (Dia, Mês, Ano) contêm apenas números válidos.");
                return null;
            }
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            Contato c = LerContatoDaTela();
            if (c != null)
            {
                if (minhaAgenda.adicionar(c))
                    MessageBox.Show("Contato adicionado com sucesso!");
                else
                    MessageBox.Show("Já existe um contato com este email.");
                
                LimparCampos();
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            // Para pesquisar, precisamos apenas do Email preenchido na tela (usado no Equals)
            Contato cBusca = new Contato { Email = txtEmail.Text };
            Contato encontrado = minhaAgenda.pesquisar(cBusca);

            if (encontrado != null)
            {
                txtNome.Text = encontrado.Nome;
                txtDia.Text = encontrado.DtNasc.Dia.ToString();
                txtMes.Text = encontrado.DtNasc.Mes.ToString();
                txtAno.Text = encontrado.DtNasc.Ano.ToString();
                
                lstContatos.Items.Clear();
                lstContatos.Items.Add(encontrado.ToString());
            }
            else
            {
                MessageBox.Show("Contato não encontrado.");
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            Contato c = LerContatoDaTela();
            if (c != null)
            {
                if (minhaAgenda.alterar(c))
                    MessageBox.Show("Contato alterado com sucesso!");
                else
                    MessageBox.Show("Contato não encontrado para alteração.");
            }
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            Contato c = new Contato { Email = txtEmail.Text };
            if (minhaAgenda.remover(c))
            {
                MessageBox.Show("Contato removido com sucesso!");
                LimparCampos();
            }
            else
            {
                MessageBox.Show("Contato não encontrado.");
            }
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            lstContatos.Items.Clear();
            if (minhaAgenda.Agenda.Count == 0)
            {
                lstContatos.Items.Add("A agenda está vazia.");
            }
            else
            {
                foreach (Contato c in minhaAgenda.Agenda)
                {
                    lstContatos.Items.Add(c.ToString());
                }
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LimparCampos()
        {
            txtEmail.Clear();
            txtNome.Clear();
            txtDia.Clear();
            txtMes.Clear();
            txtAno.Clear();
            txtTelNumero.Clear();
            txtTelTipo.Clear();
            chkPrincipal.Checked = false;
        }
    }
}
