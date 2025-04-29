using ClubeDaLeituraConsoleApp.Compartilhado;
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
    public class Emprestimo : EntidadeBase<Emprestimo>
    {
        RepositorioCaixa repositorioCaixa;
        public Amigo Amigo { get; set; }
        public Revista Revista { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolucao 
        { 
            get 
            {
                int IdCaixa = Revista.IdCaixa;
                int dias = repositorioCaixa.SelecionarRegistroPorId(IdCaixa).DiasDeEmprestimo;
                return DataEmprestimo.AddDays(dias);
            } 
        }
        public string Situacao { get; set; }
        public string[] situacoes { get; set; } = new string[] {"Aberto", "Concluído", "Atrasado"};
        public decimal ValorMulta { get; set; }
        public bool TemMulta { get; set; }

        public Emprestimo(Amigo amigo, Revista revista, DateTime dataEmprestimo, RepositorioCaixa repositorioCaixa)
        {
            Amigo = amigo;
            Revista = revista;
            DataEmprestimo = dataEmprestimo;
            this.repositorioCaixa = repositorioCaixa;
        }

        public override void AtualizarRegistro(Emprestimo registroEditado)
        {
            throw new NotImplementedException();
        }

        public override string Validar()
        {
            throw new NotImplementedException();
        }
    }
}
