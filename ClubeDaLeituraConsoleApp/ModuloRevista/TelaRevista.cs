using ClubeDaLeituraConsoleApp.Compartilhado;
using ClubeDaLeituraConsoleApp.ModuloAmigo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeDaLeituraConsoleApp.ModuloRevista
{
    public class TelaRevista
    {
            public RepositorioRevista repositorioRevista;

            public TelaRevista(RepositorioRevista repositorioRevista)
            {
                this.repositorioRevista = repositorioRevista;
            }

            public void ExibirCabecalho()
            {
                Console.Clear();
                Console.WriteLine("---------------------------------------");
                Console.WriteLine("Menu Revistas");
                Console.WriteLine("---------------------------------------");
                Console.WriteLine();
            }
            public string MenuRevistas()
            {
                ExibirCabecalho();

                Console.WriteLine("Selecione a opção desejada:");
                Console.WriteLine("1 - Cadastrar Revista");
                Console.WriteLine("2 - Editar Revista");
                Console.WriteLine("3 - Visualizar Revistas");
                Console.WriteLine("4 - Excluir Revista");
                Console.WriteLine("5 - Retornar");
                string opcao = Console.ReadLine();

                return opcao;
            }

            public void CadastrarRevista()
            {
                ExibirCabecalho();
                Console.WriteLine("---------------------------------------");
                Console.WriteLine("Cadastrando Revista");
                Console.WriteLine("---------------------------------------");
                Console.WriteLine();
                Revista novoRevista = ObterDadosRevistas();

                repositorioRevista.Adicionar(novoRevista);
                Notificador.ExibirMensagem("O Cadastro foi realizado com sucesso!", ConsoleColor.Green);
            }

            public void EditarRevista()
            {
                ExibirCabecalho();
                Console.WriteLine("---------------------------------------");
                Console.WriteLine("Editando Revista");
                Console.WriteLine("---------------------------------------");
                Console.WriteLine();
                VisualizarRevistas(false);
                Console.WriteLine();
                Console.Write("Digite o Id da Revista que deseja editar: ");
                int IdEditar = int.Parse(Console.ReadLine()!);
                Revista revistaEditada = repositorioRevista.SelecionarPorId(IdEditar);
                if (revistaEditada == null)
                {
                    Notificador.ExibirMensagem("O Id mencionado não existe, digite novamente", ConsoleColor.Red);
                    EditarRevista();
                }

                revistaEditada = ObterDadosRevistas();
                repositorioRevista.Editar(IdEditar, revistaEditada);

                Notificador.ExibirMensagem("O registro foi editado com sucesso!", ConsoleColor.Green);
            }

            public void VisualizarRevistas(bool exibirTitulo)
            {
                if (exibirTitulo)
                    ExibirCabecalho();
                Console.WriteLine("---------------------------------------");
                Console.WriteLine("Visualizando Revistas...");
                Console.WriteLine("---------------------------------------");

                Console.WriteLine();

                Console.WriteLine(
                    "{0, -6} | {1, -20} | {2, -15} | {3, -20} | {4, -15}",
                    "Id", "Titulo", "Número de Edição", "Ano de Publicação", "Status Atual"
                );

                Revista[] RevistasCadastradas = repositorioRevista.SelecionarTodos();

                for (int i = 0; i < RevistasCadastradas.Length; i++)
                {
                    Revista r = RevistasCadastradas[i];

                    if (r == null) continue;

                    Console.WriteLine(
                "{0, -6} | {1, -20} | {2, -15} | {3, -20} | {4, -15}",
                    r.Id, r.Titulo, r.NumeroEdicao, r.AnoPublicado, r.StatusEmprestimo
                );
            }

                Console.WriteLine();

                Notificador.ExibirMensagem("Pressione ENTER para continuar...", ConsoleColor.DarkYellow);
            }

            public void ExcluirRevista()
            {
                ExibirCabecalho();
                Console.WriteLine("---------------------------------------");
                Console.WriteLine("Excluindo Revista...");
                Console.WriteLine("---------------------------------------");

                Console.WriteLine();

                VisualizarRevistas(false);
                Console.WriteLine("Digite o Id da Revista que deseja excluir");
                int idExcluir = int.Parse(Console.ReadLine());

                Revista r = repositorioRevista.SelecionarPorId(idExcluir);
                if (r == null)
                {
                    Notificador.ExibirMensagem("O Id mencionado não existe, tente novamente", ConsoleColor.Red);
                    ExcluirRevista();
                }

                repositorioRevista.Excluir(idExcluir, r);
                Notificador.ExibirMensagem("O registro foi excluído com sucesso!", ConsoleColor.Green);
            }
        public Revista ObterDadosRevistas()
        {
            Console.Write("Digite o título da revista: ");
            string? titulo = Console.ReadLine();
            Console.Write("Digite o número de Edição: ");
            string? numeroEdicao = Console.ReadLine();
            Console.Write("Digite o ano de publicação dessa revista: ");
            DateTime anoPublicado = Convert.ToDateTime(Console.ReadLine());
            Revista novaRevista = new Revista(titulo, numeroEdicao, anoPublicado);
            return novaRevista;
        }

        public void EscolherStatus()
        {

        }
        
    }
}
