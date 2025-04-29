using ClubeDaLeituraConsoleApp.Compartilhado;
using ClubeDaLeituraConsoleApp.ModuloAmigo;
using ClubeDaLeituraConsoleApp.ModuloCaixa;
using ClubeDaLeituraConsoleApp.ModuloEmprestimo;
using ClubeDaLeituraConsoleApp.ModuloReservas;
using ClubeDaLeituraConsoleApp.ModuloRevista;

namespace ClubeDaLeituraConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RepositorioCaixa repositorioCaixa = new RepositorioCaixa();
            RepositorioAmigo repositorioAmigo = new RepositorioAmigo();
            RepositorioRevista repositorioRevista = new RepositorioRevista(repositorioCaixa);
            RepositorioEmprestimo repositorioEmprestimo = new RepositorioEmprestimo();
            RepositorioReserva repositorioReserva = new RepositorioReserva(repositorioEmprestimo);
            
            TelaPrincipal telaPrincipal = new TelaPrincipal();
            TelaRevista telaRevista = new TelaRevista(repositorioRevista, repositorioCaixa);
            TelaAmigo telaAmigo = new TelaAmigo(repositorioAmigo, repositorioEmprestimo);
            TelaCaixa telaCaixa = new TelaCaixa(repositorioCaixa);
            TelaEmprestimo telaEmprestimo = new TelaEmprestimo(repositorioEmprestimo, repositorioAmigo, repositorioRevista, repositorioCaixa);
            TelaReserva telaReserva = new TelaReserva(repositorioReserva, repositorioAmigo, repositorioRevista, repositorioEmprestimo, repositorioCaixa);

            while(true)
            {
                string opcaoPrincipal = telaPrincipal.MenuPrincipal();

                if (opcaoPrincipal == "1")
                {
                    string opcaoAmigo = telaAmigo.ApresentarMenu();
                    switch (opcaoAmigo)
                    {
                        case "1":
                            telaAmigo.CadastrarRegistro();
                            break;

                        case "2":
                            telaAmigo.EditarRegistro();
                            break;

                        case "3":
                            telaAmigo.VisualizarRegistros(true);
                            break;

                        case "4":
                            telaAmigo.ExcluirRegistro();
                            break;

                        case "5":
                            break;
                        default:
                            Notificador.ExibirMensagem("Opção Inválida, tente novamente", ConsoleColor.Red);
                            break;
                    }
                }
                else if (opcaoPrincipal == "2")
                {
                    string opcaoRevista = telaRevista.ApresentarMenu();
                    switch (opcaoRevista)
                    {
                        case "1":
                            telaRevista.CadastrarRegistro();
                            break;

                        case "2":
                            telaRevista.EditarRegistro();
                            break;

                        case "3":
                            telaRevista.VisualizarRegistros(true);
                            break;

                        case "4":
                            telaRevista.ExcluirRegistro();
                            break;

                        case "5":
                            break;
                        default:
                            Notificador.ExibirMensagem("Opção Inválida, tente novamente", ConsoleColor.Red);
                            break;
                    }
                }
                else if (opcaoPrincipal == "3")
                {
                    string opcaoCaixa = telaCaixa.ApresentarMenu();
                    switch (opcaoCaixa)
                    {
                        case "1":
                            telaCaixa.CadastrarRegistro();
                            break;

                        case "2":
                            telaCaixa.EditarRegistro();
                            break;

                        case "3":
                            telaCaixa.VisualizarRegistros(true);
                            break;

                        case "4":
                            telaCaixa.ExcluirCaixa();
                            break;

                        case "5":
                            break;
                        default:
                            Notificador.ExibirMensagem("Opção Inválida, tente novamente", ConsoleColor.Red);
                            break;
                    }
                }
                else if (opcaoPrincipal == "4")
                {
                    string opcaoEmprestimo = telaEmprestimo.ApresentarMenu();
                    switch (opcaoEmprestimo)
                    {
                        case "1":
                            telaEmprestimo.CadastrarRegistro();
                            break;
                        case "2":
                            telaEmprestimo.RegistrarDevolucao();
                            break;
                        case "3":
                            telaEmprestimo.VisualizarRegistros(true);
                            break;
                        case "4":
                            telaEmprestimo.VisualizarMultas(true);
                            break;
                        case "5":
                            telaEmprestimo.RegistrarPagamento();
                            break;
                        case "6":
                            break;
                        default:
                            Notificador.ExibirMensagem("Opção Inválida, tente novamente", ConsoleColor.Red);
                            break;
                    }
                }
                else if (opcaoPrincipal == "5")
                {
                    string opcaoCaixa = telaReserva.ApresentarMenu();
                    switch (opcaoCaixa)
                    {
                        case "1":
                            telaReserva.RegistrarReserva();
                            break;

                        case "2":
                            telaReserva.VisualizarRegistros(true);
                            break;

                        case "3":
                            telaReserva.CancelarReserva();
                            break;

                        case "4":
                            telaReserva.RetirarReserva();
                            break;

                        case "5":
                            break;
                        default:
                            Notificador.ExibirMensagem("Opção Inválida, tente novamente", ConsoleColor.Red);
                            break;
                    }
                }
                else if (opcaoPrincipal == "6") break;
                else
                {
                    Notificador.ExibirMensagem("Opção Inválida, tente novamente", ConsoleColor.Red);
                }

            }
        }
    }
}
