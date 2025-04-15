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
    public class Revista
    {
        public int Id;
        public string Titulo;
        public int NumeroEdicao;
        public DateTime AnoPublicado;
        public string[] StatusEmprestimo = new string[] { "Emprestada", "Disponível", "Reservada" };
        public string StatusAtual;
        public int IdCaixa;
        public string caixa;

        public Revista(string titulo, int numeroEdicao, DateTime anoPublicado, int idcaixa)
        {
            Titulo = titulo;
            NumeroEdicao = numeroEdicao;
            AnoPublicado = anoPublicado;
            IdCaixa = idcaixa;
        }

        public void Emprestar(Revista revista)
        {
            revista.StatusAtual = StatusEmprestimo[0];
        }
        public void Devolver(Revista revista)
        {
            revista.StatusAtual = StatusEmprestimo[1];
        }
        public void Reservar(Revista revista)
        {
            revista.StatusAtual = StatusEmprestimo[2];
        }
        public string Validar(RepositorioRevista repositorioRevista)
        {
            Revista[] revistas = repositorioRevista.revistas;
            string erros = "";
            if (string.IsNullOrWhiteSpace(Titulo))
                erros += "O campo 'Titulo' é obrigatório.\n";
            else if (Titulo.Length > 50 || Titulo.Length < 2)
                erros += "O campo 'Título' pode ter entre 100 e 2 caracteres.\n";

            if (NumeroEdicao < 0 || NumeroEdicao == null)
                erros += "O campo 'Numero da edição' é obrigatório e não pode ser negativo.\n";
            for (int i = 0; i < revistas.Length; i++)
            {
                if (revistas[i] == null) continue;
                for (int j = 1; j < revistas.Length - 1; j++)
                {
                    if (revistas[j] == null) continue;
                    if (revistas[i].Titulo == revistas[j].Titulo && revistas[i].NumeroEdicao == revistas[j].NumeroEdicao)
                        erros += "Os Campos 'Título' e 'Número de Edição não podem repetir juntos'.\n";
                }
            }
            return erros;
        }
    }
}
