using ClubeDaLeituraConsoleApp.Compartilhado;
using ClubeDaLeituraConsoleApp.Interfaces;

namespace ClubeDaLeituraConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            TelaPrincipal telaPrincipal = new TelaPrincipal();
            while (true)
            {
                string opcaoEscolhida = telaPrincipal.ApresentarMenuPrincipal();
                ITelaCrud telaSelecionada = telaPrincipal.ObterTela();

                string? opcao = telaSelecionada.ApresentarMenu();
                telaSelecionada.ApresentarOpcoes(opcao);
            }
        }
    }
}
