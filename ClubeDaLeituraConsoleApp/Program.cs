using ClubeDaLeituraConsoleApp.Compartilhado;
using ClubeDaLeituraConsoleApp.ModuloAmigo;

namespace ClubeDaLeituraConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RepositorioAmigo repositorioAmigo = new RepositorioAmigo();
            TelaPrincipal telaPrincipal = new TelaPrincipal();
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
                }

            }
        }
    }
}
