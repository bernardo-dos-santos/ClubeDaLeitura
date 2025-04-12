using ClubeDaLeituraConsoleApp.Compartilhado;
using ClubeDaLeituraConsoleApp.ModuloAmigo;
using ClubeDaLeituraConsoleApp.ModuloRevista;

namespace ClubeDaLeituraConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RepositorioAmigo repositorioAmigo = new RepositorioAmigo();
            RepositorioRevista repositorioRevista = new RepositorioRevista();

            TelaPrincipal telaPrincipal = new TelaPrincipal();

            TelaRevista telaRevista = new TelaRevista(repositorioRevista);
            TelaAmigo telaAmigo = new TelaAmigo(repositorioAmigo);

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
                    string opcaoRevista = telaAmigo.MenuAmigo();
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

            }
        }
    }
}
