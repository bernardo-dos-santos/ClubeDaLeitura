using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeDaLeituraConsoleApp.ModuloAmigo
{
    public class TelaAmigo
    {
        public RepositorioAmigo repositorioAmigo;

        public TelaAmigo(RepositorioAmigo repositorioAmigo)
        {
            this.repositorioAmigo = repositorioAmigo;
        }

        public string MenuAmigo()
        {
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Menu Amigos");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Selecione a opção desejada:");
            Console.WriteLine("1 - Cadastrar Amigo");
            Console.WriteLine("2 - Editar Amigo");
            Console.WriteLine("3 - Visualizar Amigos");
            Console.WriteLine("4 - Excluir Amigos");
            string opcao = Console.ReadLine();

            return opcao;
        }

        public void CadastrarAmigo()
        {
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Menu Amigos");
            Console.WriteLine("---------------------------------------");


        }

    }
}
