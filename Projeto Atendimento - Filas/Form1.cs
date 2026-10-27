using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CentralAtendimento
{
    public partial class Form1 : Form
    {
        private Senhas senhas;
        private Guiches guiches;

        // Controles de UI
        private Button btnGerar;
        private Button btnListarSenhas;
        private ListBox lstSenhas;
        
        private Button btnAdicionarGuiche;
        private Label lblQtdeGuiches;
        
        private Label lblNumGuiche;
        private TextBox txtNumGuiche;
        private Button btnChamar;
        private Button btnListarAtendimentos;
        private ListBox lstAtendimentos;

        public Form1()
        {
            InitializeComponent();
            ConfigurarInterface();
            
            // Instancia os controladores principais
            senhas = new Senhas();
            guiches = new Guiches();
            AtualizarQtdeGuiches();
        }

        private void ConfigurarInterface()
        {
            this.Text = "Central de Atendimento";
            this.Size = new Size(600, 450);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Painel Senhas
            btnGerar = new Button() { Text = "Gerar", Location = new Point(20, 20), Width = 100 };
            btnGerar.Click += BtnGerar_Click;

            btnListarSenhas = new Button() { Text = "Listar Senhas", Location = new Point(20, 50), Width = 100 };
            btnListarSenhas.Click += BtnListarSenhas_Click;

            lstSenhas = new ListBox() { Location = new Point(130, 20), Size = new Size(200, 150) };

            // Painel Guichês
            btnAdicionarGuiche = new Button() { Text = "Adicionar", Location = new Point(350, 20), Width = 100 };
            btnAdicionarGuiche.Click += BtnAdicionarGuiche_Click;

            lblQtdeGuiches = new Label() { Location = new Point(460, 25), AutoSize = true };

            lblNumGuiche = new Label() { Text = "Guichê:", Location = new Point(350, 60), Width = 50 };
            txtNumGuiche = new TextBox() { Location = new Point(400, 57), Width = 50 };
            
            btnChamar = new Button() { Text = "Chamar", Location = new Point(460, 55), Width = 100 };
            btnChamar.Click += BtnChamar_Click;

            btnListarAtendimentos = new Button() { Text = "Listar Atendimentos", Location = new Point(350, 90), Width = 210 };
            btnListarAtendimentos.Click += BtnListarAtendimentos_Click;

            lstAtendimentos = new ListBox() { Location = new Point(130, 190), Size = new Size(430, 150) };

            // Adiciona controles ao formulário
            this.Controls.Add(btnGerar);
            this.Controls.Add(btnListarSenhas);
            this.Controls.Add(lstSenhas);
            this.Controls.Add(btnAdicionarGuiche);
            this.Controls.Add(lblQtdeGuiches);
            this.Controls.Add(lblNumGuiche);
            this.Controls.Add(txtNumGuiche);
            this.Controls.Add(btnChamar);
            this.Controls.Add(btnListarAtendimentos);
            this.Controls.Add(lstAtendimentos);
        }

        private void BtnGerar_Click(object sender, EventArgs e)
        {
            senhas.Gerar();
            AtualizarListaSenhas();
        }

        private void BtnListarSenhas_Click(object sender, EventArgs e)
        {
            AtualizarListaSenhas();
        }

        private void BtnAdicionarGuiche_Click(object sender, EventArgs e)
        {
            int novoId = guiches.ListaGuiches.Count + 1;
            guiches.Adicionar(new Guiche(novoId));
            AtualizarQtdeGuiches();
        }

        private void BtnChamar_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtNumGuiche.Text, out int idGuiche))
            {
                Guiche guiche = guiches.ListaGuiches.FirstOrDefault(g => g.Id == idGuiche);
                if (guiche != null)
                {
                    if (guiche.Chamar(senhas.FilaSenhas))
                    {
                        AtualizarListaSenhas(); // Remove a senha chamada da tela principal
                        ListarAtendimentos(guiche); // Exibe os atendimentos do guichê atual
                    }
                    else
                    {
                        MessageBox.Show("Não há senhas na fila de espera.");
                    }
                }
                else
                {
                    MessageBox.Show("Guichê não encontrado.");
                }
            }
            else
            {
                MessageBox.Show("Informe um número válido para o guichê.");
            }
        }

        private void BtnListarAtendimentos_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtNumGuiche.Text, out int idGuiche))
            {
                Guiche guiche = guiches.ListaGuiches.FirstOrDefault(g => g.Id == idGuiche);
                if (guiche != null)
                {
                    ListarAtendimentos(guiche);
                }
                else
                {
                    MessageBox.Show("Guichê não encontrado.");
                }
            }
            else
            {
                MessageBox.Show("Informe um número válido para o guichê.");
            }
        }

        private void AtualizarListaSenhas()
        {
            lstSenhas.Items.Clear();
            foreach (var senha in senhas.FilaSenhas)
            {
                lstSenhas.Items.Add(senha.DadosParciais());
            }
        }

        private void ListarAtendimentos(Guiche guiche)
        {
            lstAtendimentos.Items.Clear();
            foreach (var atendimento in guiche.Atendimentos)
            {
                lstAtendimentos.Items.Add(atendimento.DadosCompletos());
            }
        }

        private void AtualizarQtdeGuiches()
        {
            lblQtdeGuiches.Text = $"Guichês: {guiches.ListaGuiches.Count}";
        }
    }
}
