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
    public class TelaRevista : TelaBase<Revista>
    {
        public RepositorioRevista repositorioRevista;
        public RepositorioCaixa repositorioCaixa;

        public TelaRevista(RepositorioRevista repositorioRevista, RepositorioCaixa repositorioCaixa) : base("Revista", repositorioRevista)
        {
            this.repositorioRevista = repositorioRevista;
            this.repositorioCaixa = repositorioCaixa;
        }
        public override void CadastrarRegistro()
        {
            ExibirCabecalho();
            Console.WriteLine();

            Console.WriteLine($"Cadastrando {nomeEntidade}...");
            Console.WriteLine("--------------------------------------------");

            Console.WriteLine();

            Revista novoRegistro = ObterDados();
            if (novoRegistro == null) return;
            string erros = novoRegistro.Validar();

            if (erros.Length > 0)
            {
                Notificador.ExibirMensagem(erros, ConsoleColor.Red);
                CadastrarRegistro();
                return;
            }

            repositorioRevista.CadastrarRevista(novoRegistro);
            Notificador.ExibirMensagem("O Cadastro foi realizado com sucesso", ConsoleColor.Green);

        }
        public override void EditarRegistro()
        {
            ExibirCabecalho();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Editando Revista");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine();
            VisualizarRegistros(false);
            Console.WriteLine();
            Console.Write("Digite o Id da Revista que deseja editar: ");
            int IdEditar = Convertor.ConverterTextoInt();
            if (IdEditar == 0) return;
            Revista revistaEditada = repositorioRevista.SelecionarRegistroPorId(IdEditar);
            if (revistaEditada == null)
            {
                Notificador.ExibirMensagem("Id Inválido, Retornando...", ConsoleColor.Red);
                return;
            }

            int idAntigoCaixa = revistaEditada.IdCaixa;
            revistaEditada = ObterDados();
            if (revistaEditada == null) return;
            string erros = revistaEditada.Validar();
            if (erros.Length > 0)
            {
                Notificador.ExibirMensagem(erros, ConsoleColor.Red);
                return;
            }
            repositorioRevista.Editar(IdEditar, revistaEditada, idAntigoCaixa);
            Notificador.ExibirMensagem("O registro foi editado com sucesso!", ConsoleColor.Green);
        }

        public override void VisualizarRegistros(bool exibirTitulo)
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

            List<Revista> revistas = repositorioRevista.SelecionarRegistros();

            foreach (var r in revistas)
            {
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

            List<Caixa> caixas = repositorioCaixa.SelecionarRegistros();
            bool caixaExiste = false;
            foreach (var c in caixas)
            {
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
        public override void ExcluirRegistro()
        {
            ExibirCabecalho();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Excluindo Revista");
            Console.WriteLine("---------------------------------------");

            Console.WriteLine();

            VisualizarRegistros(false);
            Console.WriteLine("Digite o Id da Revista que deseja excluir");
            int idExcluir = Convertor.ConverterTextoInt();
            if (idExcluir == 0) return;

            Revista r = repositorioRevista.SelecionarRegistroPorId(idExcluir);
            if (r == null)
            {
                Notificador.ExibirMensagem("O Id mencionado não existe, retornando", ConsoleColor.Red);
                return;
            } else if (r.StatusAtual != "Disponível")
            {
                Notificador.ExibirMensagem("Não é possível excluir revistas que estão emprestadas/reservadas", ConsoleColor.Red);
                return;
            }

            bool conseguiuExcluir = repositorioRevista.Excluir(idExcluir, repositorioCaixa);

            if(!conseguiuExcluir)
            {
                Notificador.ExibirMensagem("Houve um erro durante a exclusão, tente novamente", ConsoleColor.Red);
                return;
            }
            Notificador.ExibirMensagem("O registro foi excluído com sucesso!", ConsoleColor.Green);
        }
        public override Revista ObterDados()
        {
            Console.Write("Digite o título da revista: ");
            string? titulo = Console.ReadLine();
            Console.Write("Digite o número de Edição: ");
            int numeroEdicao = Convertor.ConverterTextoInt();
            if (numeroEdicao == 0) return null;
            Console.Write("Digite a data de publicação dessa revista (dd/mm/aaaa): ");
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
            Revista novaRevista = new Revista(titulo, numeroEdicao, anoPublicado, idCaixa, repositorioRevista);
            return novaRevista;
        }       
    }
}
