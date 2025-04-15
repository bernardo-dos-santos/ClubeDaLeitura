using ClubeDaLeituraConsoleApp.Compartilhado;
using ClubeDaLeituraConsoleApp.ModuloAmigo;
using ClubeDaLeituraConsoleApp.ModuloEmprestimo;
using ClubeDaLeituraConsoleApp.ModuloRevista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeDaLeituraConsoleApp.ModuloReservas
{
    public class TelaReserva
    {
        public RepositorioReserva repositorioReserva;
        public RepositorioAmigo repositorioAmigo;
        public RepositorioRevista repositorioRevista;
        public RepositorioEmprestimo repositorioEmprestimo;

        public TelaReserva(RepositorioReserva repositorioReserva, RepositorioAmigo repositorioAmigo, RepositorioRevista repositorioRevista, RepositorioEmprestimo repositorioEmprestimo)
        {
            this.repositorioReserva = repositorioReserva;
            this.repositorioAmigo = repositorioAmigo;
            this.repositorioRevista = repositorioRevista;
            this.repositorioEmprestimo = repositorioEmprestimo;
        }
        public void ExibirCabecalho()
        {
            Console.Clear();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Menu Reservas");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine();
        }
        public string MenuReservas()
        {
            ExibirCabecalho();

            Console.WriteLine("Selecione a opção desejada:");
            Console.WriteLine("1 - Registrar Reserva");
            Console.WriteLine("2 - Visualizar Reservas");            
            Console.WriteLine("3 - Cancelar Reserva");
            Console.WriteLine("4 - Retirar Reserva");
            Console.WriteLine("5 - Retornar");
            string? opcao = Console.ReadLine();

            return opcao;
        }
        public void RegistrarReserva()
        {
            ExibirCabecalho();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Registrando Reserva");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine();
            Reserva novaReserva = ObterDadosReserva();

            if (novaReserva == null) return;
            if(novaReserva.amigo == null || novaReserva.revista == null)
            {                    
                Notificador.ExibirMensagem("Id Inválido, Retornando...", ConsoleColor.Red);
                return;
            }
            if (novaReserva.amigo.emprestimo == true || novaReserva.amigo.ListaNegra == "Sim" || novaReserva.revista.StatusAtual != "Disponível")
            {
                Notificador.ExibirMensagem("Não é possível adicionar reservas a esse amigo ou revista", ConsoleColor.Red);
                return;
            }
            repositorioReserva.Registrar(novaReserva);
            Notificador.ExibirMensagem("O registro foi realizado com sucesso!", ConsoleColor.Green);
        }
        public void CancelarReserva()
        {
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Registrando Devolução");
            Console.WriteLine("---------------------------------------");

            VisualizarReservas(false);

            Console.Write("Digite o Id da Reserva que deseja cancelar: ");
            int idCancelar = Convertor.ConverterTextoInt();
            if (idCancelar == 0) return;
            Reserva r = repositorioReserva.SelecionarPorId(idCancelar);
            if (r == null)
            {
                Notificador.ExibirMensagem("Id Inválido, Retornando...", ConsoleColor.Red);
                return;
            }
            if (r == null) Notificador.ExibirMensagem("Id Inválido, Retornando...", ConsoleColor.Red);
            repositorioReserva.Excluir(idCancelar);
            
            Notificador.ExibirMensagem("O registro foi realizado com sucesso!", ConsoleColor.Green);
        }
        public void RetirarReserva()
        {
            VisualizarReservas(false);

            Console.Write("Digite o Id da reserva que você deseja retirar: ");
            int idRetirar = Convertor.ConverterTextoInt();
            if (idRetirar == 0) return;
            Reserva r = repositorioReserva.SelecionarPorId(idRetirar);
            if (r == null)
            {
                Notificador.ExibirMensagem("Id Inválido, Retornando...", ConsoleColor.Red);
                return;
            }
            r.StatusAtual = r.Status[1];
            
            Emprestimo emprestimo = new Emprestimo(r.amigo, r.revista, DateTime.Now);
            repositorioEmprestimo.Cadastrar(emprestimo);
            Notificador.ExibirMensagem("A reserva foi alterada para um empréstimo, confira no Menu Empréstimos!", ConsoleColor.Green);
        }
        public void VisualizarAmigos()
        {
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Visualizando Amigos...");
            Console.WriteLine("---------------------------------------");

            Console.WriteLine();
            Console.WriteLine(
                "{0, -6} | {1, -20} | {2, -30} | {3, -20}",
                "Id", "Nome", "Nome Responsavel", "Telefone"
            );

            Amigo[] amigosCadastrados = repositorioAmigo.SelecionarTodos();

            for (int i = 0; i < amigosCadastrados.Length; i++)
            {
                Amigo m = amigosCadastrados[i];

                if (m == null) continue;

                Console.WriteLine(
                "{0, -6} | {1, -20} | {2, -30} | {3, -20}",
                m.Id, m.Nome, m.NomeResponsavel, m.Telefone
                );
            }

            Console.WriteLine();

            Notificador.ExibirMensagem("Pressione ENTER para continuar...", ConsoleColor.DarkYellow);
        }
        public void VisualizarRevistas()
        {
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Visualizando Revistas...");
            Console.WriteLine("---------------------------------------");

            Console.WriteLine();

            Console.WriteLine(
            "{0, -6} | {1, -20} | {2, -15} | {3, -20} | {4, -15} | {5, -20}",
                "Id", "Titulo", "Número de Edição", "Ano de Publicação", "Status Atual", "Caixa"
            );

            Revista[] RevistasCadastradas = repositorioRevista.SelecionarTodos();

            for (int i = 0; i < RevistasCadastradas.Length; i++)
            {
                Revista r = RevistasCadastradas[i];

                if (r == null) continue;

                Console.WriteLine(
                "{0, -6} | {1, -20} | {2, -16} | {3, -20} | {4, -15} | {5, -20}",
                r.Id, r.Titulo, r.NumeroEdicao, r.AnoPublicado.ToShortDateString(), r.StatusAtual, r.caixa
                );
            }

            Console.WriteLine();

            Notificador.ExibirMensagem("Pressione ENTER para continuar...", ConsoleColor.DarkYellow);
        }
        public void VisualizarReservas(bool titulo)
        {
            if (titulo) ExibirCabecalho();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Visualizando Reservas");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine(
                "{0, -6} | {1, -20} | {2, -20} | {3, -15} | {4, -15}",
                "Id", "Amigo", "Revista", "Situação", "Dia da Reserva"
            );
            for (int i = 0; i < repositorioReserva.reservas.Length; i++)
            {
                Reserva r = repositorioReserva.reservas[i];

                if (r == null) continue;
                
                Console.WriteLine(
                "{0, -6} | {1, -20} | {2, -20} | {3, -15} | {4, -15}",
                r.Id, r.amigo.Nome, r.revista.Titulo, r.StatusAtual, r.DataReserva
                );
            }
            Notificador.ExibirMensagem("Pressione ENTER para continuar...", ConsoleColor.DarkYellow);
        }
        public Reserva ObterDadosReserva()
        {
            VisualizarAmigos();
            Console.WriteLine();
            Console.Write("Digite o Id do Amigo que deseja:  ");
            int IdAmigo = Convertor.ConverterTextoInt();
            if (IdAmigo == 0) return null;

            Console.WriteLine();
            VisualizarRevistas();
            Console.WriteLine();
            Console.Write("Digite o Id da Revista que seu amigo deseja reservar: ");
            int idRevista = Convertor.ConverterTextoInt();
            if (idRevista == 0) return null;

            Amigo amigo = repositorioAmigo.SelecionarPorId(IdAmigo);
            Revista revista = repositorioRevista.SelecionarPorId(idRevista);
            
            Reserva novaReserva = new Reserva(amigo, revista);
            return novaReserva;
        }

    }
}
