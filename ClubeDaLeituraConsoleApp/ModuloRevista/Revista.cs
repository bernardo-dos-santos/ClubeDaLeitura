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
        public string Caixa;

        public Revista(string titulo, string numeroEdicao, DateTime anoPublicado)
        {
            Titulo = titulo;
            NumeroEdicao = numeroEdicao;
            AnoPublicado = anoPublicado;
        }
    }
}
