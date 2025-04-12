using ClubeDaLeituraConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        public void ExibirCabecalho()
        {
            Console.Clear();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Menu Amigos");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine();
        }
        public string MenuAmigo()
        {
            ExibirCabecalho();

            Console.WriteLine("Selecione a opção desejada:");
            Console.WriteLine("1 - Cadastrar Amigo");
            Console.WriteLine("2 - Editar Amigo");
            Console.WriteLine("3 - Visualizar Amigos");
            Console.WriteLine("4 - Excluir Amigos");
            Console.WriteLine("5 - Retornar");
            string opcao = Console.ReadLine();

            return opcao;
        }

        public void CadastrarAmigo()
        {
            ExibirCabecalho();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Cadastrando Amigo");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine();
            Amigo novoAmigo = ObterDadosAmigos();

            repositorioAmigo.Cadastrar(novoAmigo);
            Notificador.ExibirMensagem("O Cadastro foi realizado com sucesso!", ConsoleColor.Green);
        }

        public void EditarAmigo()
        {
            ExibirCabecalho();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Editando Amigo");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine();
            VisualizarAmigos(false);
            Console.WriteLine();
            Console.Write("Digite o Id do amigo que deseja editar: ");
            int IdEditar = int.Parse(Console.ReadLine()!);
            Amigo amigoEditado = repositorioAmigo.SelecionarPorId(IdEditar);
             if (amigoEditado == null)
            {
                Notificador.ExibirMensagem("O Id mencionado não existe, digite novamente", ConsoleColor.Red);
                EditarAmigo();
            }
            
            amigoEditado = ObterDadosAmigos();
            repositorioAmigo.Editar(IdEditar, amigoEditado);

            Notificador.ExibirMensagem("O registro foi editado com sucesso!", ConsoleColor.Green);
        }

        public void VisualizarAmigos(bool exibirTitulo)
        {
            if (exibirTitulo)
                ExibirCabecalho();
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

        public void ExcluirAmigo()
        {
            ExibirCabecalho();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Excluindo Amigo...");
            Console.WriteLine("---------------------------------------");

            Console.WriteLine();

            VisualizarAmigos(false);
            Console.WriteLine("Digite o Id do amigo que deseja excluir");
            int idExcluir = int.Parse(Console.ReadLine());

            Amigo a = repositorioAmigo.SelecionarPorId(idExcluir);
            if (a == null)
            {
                Notificador.ExibirMensagem("O Id mencionado não existe, tente novmente", ConsoleColor.Red);
                ExcluirAmigo();
            }

            repositorioAmigo.Excluir(idExcluir, a);
            Notificador.ExibirMensagem("O registro foi excluído com sucesso!", ConsoleColor.Green);
        }
        public Amigo ObterDadosAmigos()
        {
            Console.Write("Digite o nome do amigo: ");
            string? nome = Console.ReadLine();
            Console.Write("Digite o nome do responsável do amigo: ");
            string? nomeResponsavel = Console.ReadLine();
            Console.Write("Digite o telefone do amigo: ");
            string? telefone = Console.ReadLine();
            Amigo novoAmigo = new Amigo(nome, nomeResponsavel, telefone);
            return novoAmigo;
        }
    }
}
