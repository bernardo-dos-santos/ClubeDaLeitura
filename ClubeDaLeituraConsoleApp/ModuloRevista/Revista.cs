using ClubeDaLeituraConsoleApp.Compartilhado;
using ClubeDaLeituraConsoleApp.ModuloCaixa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace ClubeDaLeituraConsoleApp.ModuloRevista
{
    public class Revista : EntidadeBase<Revista>
    {

        public string Titulo { get; set; }
        public int NumeroEdicao { get; set; }
        public DateTime AnoPublicado { get; set; }
        public string[] StatusEmprestimo { get; set; } = new string[] { "Emprestada", "Disponível", "Reservada" };
        public string StatusAtual { get; set; }
        public int IdCaixa { get; set; }
        public string caixa { get; set; }

        RepositorioRevista repositorioRevista;

        public Revista(string titulo, int numeroEdicao, DateTime anoPublicado, int idcaixa, RepositorioRevista repositorioRevista)
        {
            Titulo = titulo;
            NumeroEdicao = numeroEdicao;
            AnoPublicado = anoPublicado;
            IdCaixa = idcaixa;
            this.repositorioRevista = repositorioRevista;
        }

        public void Emprestar()
        {
            StatusAtual = StatusEmprestimo[0];
        }
        public void Devolver()
        {
            StatusAtual = StatusEmprestimo[1];
        }
        public void Reservar()
        {
            StatusAtual = StatusEmprestimo[2];
        }
        public override string Validar()
        {
            List<Revista> revistas = repositorioRevista.SelecionarRegistros();
            string erros = "";
            if (string.IsNullOrWhiteSpace(Titulo))
                erros += "O campo 'Titulo' é obrigatório.\n";
            else if (Titulo.Length > 50 || Titulo.Length < 2)
                erros += "O campo 'Título' pode ter entre 100 e 2 caracteres.\n";

            if (NumeroEdicao < 0 || NumeroEdicao == null)
                erros += "O campo 'Numero da edição' é obrigatório e não pode ser negativo.\n";
            foreach (var revista1 in revistas)
            {
                foreach (var revista2 in revistas)
                {
                    if (revista1.Titulo == revista2.Titulo && revista1.NumeroEdicao == revista2.NumeroEdicao && revista1 != revista2)
                        erros += "Os Campos 'Título' e 'Número de Edição não podem repetir juntos'.\n";
                }
            }    
            return erros;
        }

        public override void AtualizarRegistro(Revista registroEditado)
        {
            Titulo = registroEditado.Titulo;
            NumeroEdicao = registroEditado.NumeroEdicao;
            AnoPublicado = registroEditado.AnoPublicado;
            IdCaixa = registroEditado.IdCaixa;
        }
    }
}
