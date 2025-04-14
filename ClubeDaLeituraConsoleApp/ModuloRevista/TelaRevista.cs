using ClubeDaLeituraConsoleApp.Compartilhado;
using ClubeDaLeituraConsoleApp.ModuloAmigo;
using ClubeDaLeituraConsoleApp.ModuloCaixa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ClubeDaLeituraConsoleApp.ModuloRevista
{
    public class TelaRevista
    {
        public RepositorioRevista repositorioRevista;
        public RepositorioCaixa repositorioCaixa;

        public TelaRevista(RepositorioRevista repositorioRevista, RepositorioCaixa repositorioCaixa)
        {
            this.repositorioRevista = repositorioRevista;
            this.repositorioCaixa = repositorioCaixa;
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
            string? opcao = Console.ReadLine();

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
            if (novoRevista == null) return;
            string erros = novoRevista.Validar(repositorioRevista);
            if (erros.Length > 0)
            {
                Notificador.ExibirMensagem(erros, ConsoleColor.Red);
                return;
            }
                
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
            int IdEditar = Convertor.ConverterTextoInt();
            if (IdEditar == 0) return;
            Revista revistaEditada = repositorioRevista.SelecionarPorId(IdEditar);
            
            int idAntigoCaixa = revistaEditada.IdCaixa;
            revistaEditada = ObterDadosRevistas();
            if (revistaEditada == null) return;
            revistaEditada.Validar(repositorioRevista);
            string erros = revistaEditada.Validar(repositorioRevista);
            if (erros.Length > 0)
            {
                Notificador.ExibirMensagem(erros, ConsoleColor.Red);
                return;
            }
            repositorioRevista.Editar(IdEditar, revistaEditada, idAntigoCaixa);
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
                r.Id, r.Titulo, r.NumeroEdicao, r.AnoPublicado.ToShortDateString(), r.StatusAtual, r.caixa
                );
            }

            Console.WriteLine();

            Notificador.ExibirMensagem("Pressione ENTER para continuar...", ConsoleColor.DarkYellow);
        }
        public bool VisualizarCaixas()
        {
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Visualizando Caixas...");
            Console.WriteLine("---------------------------------------");

            Console.WriteLine();

            Console.WriteLine(
                "{0, -6} | {1, -30} | {2, -20} | {3, -25}",
                "Id", "Etiqueta", "Cor", "Dias De Empréstimo"
            );

            Caixa[] CaixasCadastradas = repositorioCaixa.SelecionarTodos();
            bool caixaExiste = false;
            for (int i = 0; i < CaixasCadastradas.Length; i++)
            {
                Caixa c = CaixasCadastradas[i];

                if (c == null) continue;
                caixaExiste = true;
                Console.WriteLine(
                "{0, -6} | {1, -30} | {2, -20} | {3, -25}",
                c.Id, c.Etiqueta, c.Cor, c.DiasDeEmprestimo
                );              
            }
            if (!caixaExiste)
            {
                Notificador.ExibirMensagem("\n Nenhuma Caixa Encontrada, Crie uma caixa para registrar uma revista", ConsoleColor.Red);
                return false;
            }
            
            Console.WriteLine();

            Notificador.ExibirMensagem("Pressione ENTER para continuar...", ConsoleColor.DarkYellow);
            return true;
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
            int idExcluir = Convertor.ConverterTextoInt();
            if (idExcluir == 0) return;

            Revista r = repositorioRevista.SelecionarPorId(idExcluir);
            if (r == null)
            {
                Notificador.ExibirMensagem("O Id mencionado não existe, retornando", ConsoleColor.Red);
                return;
            } else if (r.StatusAtual != "Disponível")
            {
                Notificador.ExibirMensagem("Não é possível excluir revistas que estão emprestadas/reservadas", ConsoleColor.Red);
                return;
            }

                repositorioRevista.Excluir(idExcluir, repositorioCaixa);
            Notificador.ExibirMensagem("O registro foi excluído com sucesso!", ConsoleColor.Green);
        }
        public Revista ObterDadosRevistas()
        {
            Console.Write("Digite o título da revista: ");
            string? titulo = Console.ReadLine();
            Console.Write("Digite o número de Edição: ");
            int numeroEdicao = Convertor.ConverterTextoInt();
            if (numeroEdicao == 0) return null;
            Console.Write("Digite o ano de publicação dessa revista: ");
            if(!(DateTime.TryParse(Console.ReadLine(), out DateTime anoPublicado)))
            {
                Notificador.ExibirMensagem("Data Inválida, Retornando...", ConsoleColor.Red);
                return null;
            }
            
            if (!VisualizarCaixas())
                return null;
            Console.Write("Digite o Id da caixa da revista: ");
            int idCaixa = Convertor.ConverterTextoInt();
            if (idCaixa == 0) return null;
            Revista novaRevista = new Revista(titulo, numeroEdicao, anoPublicado, idCaixa);
            return novaRevista;
        }       
    }
}
