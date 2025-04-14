using ClubeDaLeituraConsoleApp.ModuloAmigo;
using ClubeDaLeituraConsoleApp.ModuloCaixa;
using ClubeDaLeituraConsoleApp.ModuloRevista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeDaLeituraConsoleApp.ModuloEmprestimo
{
    public class Emprestimo
    {
        public int Id;
        public Amigo Amigo;
        public Revista Revista;
        public DateTime DataEmprestimo;
        public DateTime DataDevolucao;
        public string Situacao;
        public string[] situacoes = new string[] { "Aberto", "Concluído", "Atrasado" , "Reservado"};

        public Emprestimo(Amigo amigo, Revista revista, DateTime dataEmprestimo)
        {
            Amigo = amigo;
            Revista = revista;
            DataEmprestimo = dataEmprestimo;
        }

        public void RegistrarDevolução( Revista r)
        {
            r.Devolver(r);

        }

        public DateTime ObterDataDevolucao(int idCaixa, RepositorioCaixa c)
        {
            int dias = c.caixas[idCaixa - 1].DiasDeEmprestimo;
            DataDevolucao = DataEmprestimo.AddDays(dias);
            return DataDevolucao;
        }
    }
}
