using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeDaLeituraConsoleApp.ModuloRevista
{
    public class Revista
    {
        public int Id;
        public string Titulo;
        public string NumeroEdicao;
        public DateTime AnoPublicado;
        public string[] StatusEmprestimo = new string[] { "Emprestada", "Disponível", "Reservada" };
        public string StatusAtual;
        public int IdCaixa;
        public string caixa;

        public Revista(string titulo, string numeroEdicao, DateTime anoPublicado, int idcaixa)
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
    }
}
