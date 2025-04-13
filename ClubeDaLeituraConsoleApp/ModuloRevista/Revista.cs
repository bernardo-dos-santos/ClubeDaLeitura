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
        public string StatusEmprestimo;
        public int IdCaixa;

        public Revista(string titulo, string numeroEdicao, DateTime anoPublicado, int idcaixa)
        {
            Titulo = titulo;
            NumeroEdicao = numeroEdicao;
            AnoPublicado = anoPublicado;
            IdCaixa = idcaixa;
        }

        public void Emprestar(Revista revista)
        {
            revista.StatusEmprestimo = "Emprestada";
        }
        public void Devolver(Revista revista)
        {
            revista.StatusEmprestimo = "Disponível";
        }
        public void Reservar(Revista revista)
        {
            revista.StatusEmprestimo = "Reservada";
        }
    }
}
