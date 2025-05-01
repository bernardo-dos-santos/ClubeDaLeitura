using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using ClubeDaLeituraConsoleApp.Compartilhado;
using ClubeDaLeituraConsoleApp.ModuloAmigo;
using ClubeDaLeituraConsoleApp.ModuloCaixa;
using ClubeDaLeituraConsoleApp.ModuloEmprestimo;
using ClubeDaLeituraConsoleApp.ModuloReservas;
using ClubeDaLeituraConsoleApp.ModuloRevista;
using ClubeDaLeituraConsoleApp.Interfaces;

namespace ClubeDaLeituraConsoleApp.Compartilhado
{
    public class TelaPrincipal
    {
        private string opcao;
        private RepositorioCaixa repositorioCaixa;
        private RepositorioAmigo repositorioAmigo;
        private RepositorioRevista repositorioRevista;
        private RepositorioEmprestimo repositorioEmprestimo;
        private RepositorioReserva repositorioReserva;

        public TelaPrincipal()
        {
            this.repositorioCaixa = new RepositorioCaixa();
            this.repositorioAmigo = new RepositorioAmigo();
            this.repositorioRevista = new RepositorioRevista(repositorioCaixa);
            this.repositorioEmprestimo = new RepositorioEmprestimo();
            this.repositorioReserva = new RepositorioReserva(repositorioEmprestimo);
        }

        public string ApresentarMenuPrincipal()
        {
            Console.Clear();
            Console.WriteLine("Bem vindo ao");

            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Clube da Leitura");
            Console.WriteLine("---------------------------------------");

            Console.WriteLine("Escolha uma das opções abaixo: ");
            Console.WriteLine("1 - Menu Amigos");
            Console.WriteLine("2 - Menu Revistas");
            Console.WriteLine("3 - Menu Caixas");
            Console.WriteLine("4 - Menu Empréstimos");
            Console.WriteLine("5 - Menu Reservas");
            Console.WriteLine("6 - Sair");
            opcao = Console.ReadLine()!;
            return opcao;
        }

        public ITelaCrud ObterTela()
        {
            switch (opcao)
            {
                case "1"
                : return new TelaAmigo(repositorioAmigo, repositorioEmprestimo);

                case "2"
                :
                    return new TelaRevista(repositorioRevista,repositorioCaixa);

                case "3"
                :
                    return new TelaCaixa(repositorioCaixa);

                case "4"
                :
                    return new TelaEmprestimo(repositorioEmprestimo,repositorioAmigo,repositorioRevista,repositorioCaixa);

                case "5"
                :
                    return new TelaReserva(repositorioReserva,repositorioAmigo,repositorioRevista,repositorioEmprestimo,repositorioCaixa);

                case "6"
                : return null;



                default:
                    return null;
            }
        }

    }
}
