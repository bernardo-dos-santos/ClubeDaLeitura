using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ClubeDaLeituraConsoleApp.Compartilhado
{
    public class TelaPrincipal
    {
        public void MenuPrincipal()
        {
            Console.WriteLine("Bem vindo ao");

            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Clube da Leitura");
            Console.WriteLine("---------------------------------------");

            Console.WriteLine("Escolha uma das opções abaixo: ");
            Console.WriteLine("1 - Menu Amigos");
            string? opcao = Console.ReadLine();
        }

    }
}
