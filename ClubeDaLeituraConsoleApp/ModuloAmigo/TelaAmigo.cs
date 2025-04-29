using ClubeDaLeituraConsoleApp.Compartilhado;
using ClubeDaLeituraConsoleApp.ModuloEmprestimo;
using ClubeDaLeituraConsoleApp.ModuloRevista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ClubeDaLeituraConsoleApp.ModuloAmigo
{
    public class TelaAmigo : TelaBase<Amigo>
    {
        public RepositorioAmigo repositorioAmigo;
        public RepositorioEmprestimo repositorioEmprestimo;

        public TelaAmigo(RepositorioAmigo repositorioAmigo, RepositorioEmprestimo repositorioEmprestimo) : base("Amigo", repositorioAmigo)
        {
            this.repositorioAmigo = repositorioAmigo;
            this.repositorioEmprestimo = repositorioEmprestimo;
        }
        public override void VisualizarRegistros(bool exibirTitulo)
        {
            if (exibirTitulo)
                ExibirCabecalho();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Visualizando Amigos...");
            Console.WriteLine("---------------------------------------");

            Console.WriteLine();

            Console.WriteLine(
                "{0, -6} | {1, -15} | {2, -20} | {3, -20} | {4, -28} | {5, -15}",
                "Id", "Nome", "Nome Responsavel", "Telefone", "Empréstimo - Situação Atual", "Lista Negra"
            );

            List<Amigo> registros = repositorioAmigo.SelecionarRegistros();
            List<Emprestimo> emprestimos = repositorioEmprestimo.SelecionarRegistros();
            foreach (var m in registros)
            {
                string situacao = "Nenhuma", revista = "Nenhuma";
                foreach (var p in emprestimos)
                {
                    if(p.Amigo == m)
                    {
                        situacao = p.Situacao;
                        revista = p.Revista.Titulo;
                    }
                }

                Console.WriteLine(
                "{0, -6} | {1, -15} | {2, -20} | {3, -20} | {4, -28} | {5, -15}",
                m.Id, m.Nome, m.NomeResponsavel, m.Telefone, $"{revista} - {situacao}", m.ListaNegra);
            }

            Console.WriteLine();

            Notificador.ExibirMensagem("Pressione ENTER para continuar...", ConsoleColor.DarkYellow);
        }

        public override void ExcluirRegistro()
        {
            ExibirCabecalho();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Excluindo Amigo...");
            Console.WriteLine("---------------------------------------");

            Console.WriteLine();

            VisualizarRegistros(false);
            Console.WriteLine("Digite o Id do amigo que deseja excluir");
            int Id = Convertor.ConverterTextoInt();
            if (Id == 0) return;
            Amigo a = repositorioAmigo.SelecionarRegistroPorId(Id);
            if (!(ValidarExclusao(a)))
            {
                Notificador.ExibirMensagem("Não é permitido excluir amigos que tenham empréstimos", ConsoleColor.Red);
                return;
            }
            bool conseguiuExcluir = repositorioAmigo.ExcluirRegistro(Id, a);
            if (!conseguiuExcluir)
            {
                Notificador.ExibirMensagem("Ocorreu um erro, o registro não foi excluído", ConsoleColor.Red);
                return;
            }                      
            Notificador.ExibirMensagem("O registro foi excluído com sucesso!", ConsoleColor.Green);
        }
        public override Amigo ObterDados()
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

        public bool ValidarExclusao(Amigo a)
        {
            List<Emprestimo> registros = repositorioEmprestimo.SelecionarRegistros();
            foreach (var e in registros)
            {
                if (a == e.Amigo)
                {
                    if (e.Revista.StatusAtual != "Disponível") return false;
                }
            }
                

            return true;
        }
    }
}
