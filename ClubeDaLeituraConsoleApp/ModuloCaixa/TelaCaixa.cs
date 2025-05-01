using ClubeDaLeituraConsoleApp.Compartilhado;
using ClubeDaLeituraConsoleApp.Interfaces;
using ClubeDaLeituraConsoleApp.ModuloRevista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeDaLeituraConsoleApp.ModuloCaixa
{
    public class TelaCaixa : TelaBase<Caixa>, IApresentarOpcoes, ITelaCrud
    {

        public RepositorioCaixa repositorioCaixa;

        public TelaCaixa(RepositorioCaixa repositorioCaixa) : base("Caixa", repositorioCaixa)
        {
            this.repositorioCaixa = repositorioCaixa;
        }
        public override void VisualizarRegistros(bool ehTitulo)
        {
            if (ehTitulo)
                ExibirCabecalho();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Visualizando Caixas...");
            Console.WriteLine("---------------------------------------");

            Console.WriteLine();

            Console.WriteLine(
                "{0, -6} | {1, -30} | {2, -20} | {3, -25} | {4, -15}",
                "Id", "Etiqueta", "Cor", "Dias De Empréstimo", "Quant. Revistas"
            );

            List<Caixa> caixas = repositorioCaixa.SelecionarRegistros();
            
            foreach (var c in caixas)
            {
                Console.WriteLine(
                "{0, -6} | {1, -30} | {2, -20} | {3, -25} | {4, -15}",
                c.Id, c.Etiqueta, c.Cor, c.DiasDeEmprestimo, c.revistasNaCaixa.Count(item => item != null)
                );
            }        
            Console.WriteLine();

            Notificador.ExibirMensagem("Pressione ENTER para continuar...", ConsoleColor.DarkYellow);
        }
        public void ApresentarOpcoes(string opcao)
        {
            switch (opcao)
            {
                case "1": CadastrarRegistro(); break;
                case "2": EditarRegistro(); break;
                case "3": VisualizarRegistros(true); break;
                case "4": ExcluirCaixa(); break;
                case "5": break;

                default: break;
            }
        }
        public void ExcluirCaixa()
        {
            ExibirCabecalho();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Excluindo Caixa...");
            Console.WriteLine("---------------------------------------");

            Console.WriteLine();

            VisualizarRegistros(false);
            Console.WriteLine("Digite o Id da Caixa que deseja excluir");
            int Id = Convertor.ConverterTextoInt();
            if (Id == 0) return;
            Caixa c = repositorioCaixa.SelecionarRegistroPorId(Id);
            if (c == null)
            {
                Notificador.ExibirMensagem("Id Inválido, Retornando...", ConsoleColor.Red);
                return;
            }
            if (c.ValidarExclusao())
                return;
            repositorioCaixa.ExcluirRegistro(Id, c);
            Notificador.ExibirMensagem("O registro foi excluído com sucesso!", ConsoleColor.Green);
        }
        public override Caixa ObterDados()
        {
            Console.Write("Digite a etiqueta da caixa: ");
            string? etiqueta = Console.ReadLine();
            Console.Write("Digite a cor da caixa: ");
            string? cor = Console.ReadLine();
            Console.Write("Digite qual o prazo para devoluções nessa caixa (em dias): ");
            int diasDeEmprestimo = Convertor.ConverterTextoInt();
            if (diasDeEmprestimo == 0) return null;
            Caixa novaCaixa = new Caixa(etiqueta, cor, diasDeEmprestimo, repositorioCaixa);
            return novaCaixa;
        }
    }
}
