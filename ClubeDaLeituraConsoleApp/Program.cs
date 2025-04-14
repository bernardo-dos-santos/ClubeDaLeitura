using ClubeDaLeituraConsoleApp.Compartilhado;
using ClubeDaLeituraConsoleApp.ModuloAmigo;
using ClubeDaLeituraConsoleApp.ModuloCaixa;
using ClubeDaLeituraConsoleApp.ModuloEmprestimo;
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
            
            
            TelaPrincipal telaPrincipal = new TelaPrincipal();
            TelaRevista telaRevista = new TelaRevista(repositorioRevista, repositorioCaixa);
            TelaAmigo telaAmigo = new TelaAmigo(repositorioAmigo, repositorioEmprestimo);
            TelaCaixa telaCaixa = new TelaCaixa(repositorioCaixa);
            TelaEmprestimo telaEmprestimo = new TelaEmprestimo(repositorioEmprestimo, repositorioAmigo, repositorioRevista);

            while(true)
            {
                string opcaoPrincipal = telaPrincipal.MenuPrincipal();

                if(opcaoPrincipal == "1")
                {
                    string opcaoAmigo = telaAmigo.MenuAmigo();
                    switch (opcaoAmigo)
                    {
                        case "1": telaAmigo.CadastrarAmigo();
                            break;
                        
                        case "2": telaAmigo.EditarAmigo();
                            break;
                        
                        case "3": telaAmigo.VisualizarAmigos(true);
                            break;
                        
                        case "4": telaAmigo.ExcluirAmigo();
                            break;

                        case "5":
                            break;
                        default: Notificador.ExibirMensagem("Opção Inválida, tente novamente", ConsoleColor.Red);
                            break;
                    }
                } else if (opcaoPrincipal == "2")
                {
                    string opcaoRevista = telaRevista.MenuRevistas();
                    switch (opcaoRevista)
                    {
                        case "1":
                            telaRevista.CadastrarRevista();
                            break;

                        case "2":
                            telaRevista.EditarRevista();
                            break;

                        case "3":
                            telaRevista.VisualizarRevistas(true);
                            break;

                        case "4":
                            telaRevista.ExcluirRevista();
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
                    string opcaoCaixa = telaCaixa.MenuCaixas();
                    switch (opcaoCaixa)
                    {
                        case "1":
                            telaCaixa.CadastrarCaixa();
                            break;

                        case "2":
                            telaCaixa.EditarCaixa();
                            break;

                        case "3":
                            telaCaixa.VisualizarCaixas(true);
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
                } else if (opcaoPrincipal == "4")
                {
                    string opcaoCaixa = telaEmprestimo.MenuEmprestimos();
                    switch (opcaoCaixa)
                    {
                        case "1":
                            telaEmprestimo.RegistrarEmprestimo();
                            break;

                        case "2":
                            telaEmprestimo.RegistrarDevolucao();
                            break;

                        case "3":
                            telaEmprestimo.VisualizarEmprestimos(true);
                            break;

                        case "4":
                            telaEmprestimo.RegistrarReserva();
                            break;
                        case "5":
                            break;
                        default:
                            Notificador.ExibirMensagem("Opção Inválida, tente novamente", ConsoleColor.Red);
                            break;
                    }
                } else if (opcaoPrincipal == "5")
                {
                    break;
                } else
                {
                    Notificador.ExibirMensagem("Opção Inválida, tente novamente", ConsoleColor.Red);
                }

            }
        }
    }
}
