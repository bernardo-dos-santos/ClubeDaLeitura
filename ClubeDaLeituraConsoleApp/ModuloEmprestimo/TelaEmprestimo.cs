using ClubeDaLeituraConsoleApp.Compartilhado;
using ClubeDaLeituraConsoleApp.ModuloAmigo;
using ClubeDaLeituraConsoleApp.ModuloCaixa;
using ClubeDaLeituraConsoleApp.ModuloRevista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeDaLeituraConsoleApp.ModuloEmprestimo
{
    public class TelaEmprestimo
    {
        public RepositorioEmprestimo repositorioEmprestimo;
        public RepositorioAmigo repositorioAmigo;
        public RepositorioRevista repositorioRevista;

        public TelaEmprestimo(RepositorioEmprestimo repositorioEmprestimo, RepositorioAmigo repositorioAmigo, RepositorioRevista repositorioRevista)
        {
            this.repositorioEmprestimo = repositorioEmprestimo;
            this.repositorioAmigo = repositorioAmigo;
            this.repositorioRevista = repositorioRevista;
        }
        public void ExibirCabecalho()
        {
            Console.Clear();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Menu Empréstimos");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine();
        }
        public string MenuEmprestimos()
        {
            ExibirCabecalho();

            Console.WriteLine("Selecione a opção desejada:");
            Console.WriteLine("1 - Registrar Empréstimo");
            Console.WriteLine("2 - Regitrar Devolução");
            Console.WriteLine("3 - Visualizar Empréstimos");
            Console.WriteLine("4 - Retornar");
            string? opcao = Console.ReadLine();

            return opcao;
        }
        public void RegistrarEmprestimo()
        {
            ExibirCabecalho();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Registrando Empréstimo");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine();
            Emprestimo novoEmprestimo = ObterDadosEmprestimo();
            repositorioEmprestimo.Cadastrar(novoEmprestimo, novoEmprestimo.Revista);
       
        }
        public void RegistrarDevolucao()
        {
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Registrando Devolução");
            Console.WriteLine("---------------------------------------");

            VisualizarEmprestimos(false);

            Console.Write("Digite o Id do Empréstimo que deseja devolver: ");
            int idDevolucao = int.Parse(Console.ReadLine()!);
            Emprestimo e = repositorioEmprestimo.SelecionarPorId(idDevolucao);
            Revista r = e.Revista;
            e.Situacao = e.situacoes[1];
            e.Amigo.emprestimo = false;
            e.Revista.Devolver(r);

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
                r.Id, r.Titulo, r.NumeroEdicao, r.AnoPublicado.ToShortDateString(), r.StatusEmprestimo, r.caixa
                );
            }

            Console.WriteLine();

            Notificador.ExibirMensagem("Pressione ENTER para continuar...", ConsoleColor.DarkYellow);
        }

        public void VisualizarEmprestimos(bool titulo)
        {
            if (titulo) ExibirCabecalho();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Visualizando Empréstimos");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine(
                "{0, -6} | {1, -20} | {2, -20} | {3, -15} | {4, -15}",
                "Id", "Amigo", "Revista", "Situação", "Dia da Entrega"
            );
            for (int i = 0; i < repositorioEmprestimo.emprestimos.Length; i++)
            {
                Emprestimo e = repositorioEmprestimo.emprestimos[i];

                if (e == null) continue;
                
                if (e.Situacao == "Concluída" && titulo)
                {
                    Console.WriteLine(
                        "{0, -6} | {1, -20} | {2, -20} | {3, -15} | {4, -15}",
                        e.Id, e.Amigo.Nome, e.Revista.Titulo, e.Situacao, "Entregue", ConsoleColor.Green
                    );
                } else if (e.Situacao == "Aberta")
                {
                    Console.WriteLine(
                        "{0, -6} | {1, -20} | {2, -20} | {3, -15} | {4, -15}",
                        e.Id, e.Amigo.Nome, e.Revista.Titulo, e.Situacao, e.ObterDataDevolucao(e.Revista.IdCaixa, repositorioRevista.repositorioCaixa), ConsoleColor.DarkYellow
                    );
                } else
                {
                    Console.WriteLine(
                        "{0, -6} | {1, -20} | {2, -20} | {3, -15} | {4, -15}",
                        e.Id, e.Amigo.Nome, e.Revista.Titulo, e.Situacao, $"Atrasado", ConsoleColor.Red
                    );
                }

            }

        }
        public Emprestimo ObterDadosEmprestimo()
        {
            VisualizarAmigos();
            Console.WriteLine();
            Console.Write("Digite o Id do Amigo que deseja fazer um empréstimo");
            int idAmigo = int.Parse(Console.ReadLine());

            Console.WriteLine();
            VisualizarRevistas();
            Console.WriteLine();
            Console.Write("Digite o Id da Revista que seu amigo deseja: ");
            int idRevista = int.Parse(Console.ReadLine());

            Amigo amigo = repositorioAmigo.SelecionarPorId(idAmigo);
            Revista revista = repositorioRevista.SelecionarPorId(idRevista);
            Emprestimo novoEmprestimo = new Emprestimo(amigo, revista, DateTime.Now);
            return novoEmprestimo;
        }
    }
}
