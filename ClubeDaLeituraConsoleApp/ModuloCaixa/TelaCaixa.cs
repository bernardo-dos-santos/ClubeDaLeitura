﻿using ClubeDaLeituraConsoleApp.Compartilhado;
using ClubeDaLeituraConsoleApp.ModuloRevista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeDaLeituraConsoleApp.ModuloCaixa
{
    public class TelaCaixa
    {

        public RepositorioCaixa repositorioCaixa;

        public TelaCaixa(RepositorioCaixa repositorioCaixa)
        {
            this.repositorioCaixa = repositorioCaixa;
        }

        public void ExibirCabecalho()
        {
            Console.Clear();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Menu Caixas");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine();
        }
        public string MenuCaixas()
        {
            ExibirCabecalho();

            Console.WriteLine("Selecione a opção desejada:");
            Console.WriteLine("1 - Cadastrar Caixa");
            Console.WriteLine("2 - Editar Caixa");
            Console.WriteLine("3 - Visualizar Caixas");
            Console.WriteLine("4 - Excluir Caixa");
            Console.WriteLine("5 - Retornar");
            string opcao = Console.ReadLine();

            return opcao;
        }

        public void CadastrarCaixa()
        {
            ExibirCabecalho();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Cadastrando Caixa");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine();
            Caixa novoCaixa = ObterDadosCaixas();

            repositorioCaixa.Adicionar(novoCaixa);
            Notificador.ExibirMensagem("O Cadastro foi realizado com sucesso!", ConsoleColor.Green);
        }

        public void EditarCaixa()
        {
            ExibirCabecalho();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Editando Caixa");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine();
            VisualizarCaixas(false);
            Console.WriteLine();
            Console.Write("Digite o Id da Caixa que deseja editar: ");
            int IdEditar = int.Parse(Console.ReadLine()!);
            Caixa caixaEditada = repositorioCaixa.SelecionarPorId(IdEditar);
            if (caixaEditada == null)
            {
                Notificador.ExibirMensagem("O Id mencionado não existe, digite novamente", ConsoleColor.Red);
                EditarCaixa();
            }

            caixaEditada = ObterDadosCaixas();
            repositorioCaixa.Editar(IdEditar, caixaEditada);

            Notificador.ExibirMensagem("O registro foi editado com sucesso!", ConsoleColor.Green);
        }
        public void VisualizarCaixas(bool ehTitulo)
        {
            if (ehTitulo)
                ExibirCabecalho();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Visualizando Caixas...");
            Console.WriteLine("---------------------------------------");

            Console.WriteLine();

            Console.WriteLine(
                "{0, -6} | {1, -30} | {2, -20} | {3, -25}",
                "Id", "Etiqueta", "Cor", "Dias De Empréstimo"
            );

            Caixa[] CaixasCadastradas = repositorioCaixa.SelecionarTodos();
            for (int i = 0; i < CaixasCadastradas.Length; i++)
            {
                Caixa c = CaixasCadastradas[i];

                if (c == null) continue;
                Console.WriteLine(
                "{0, -6} | {1, -30} | {2, -20} | {3, -25}",
                c.Id, c.Etiqueta, c.Cor, c.DiasDeEmprestimo
                );
            }
            Console.WriteLine();

            Notificador.ExibirMensagem("Pressione ENTER para continuar...", ConsoleColor.DarkYellow);
        }
        public void ExcluirCaixa()
        {
            ExibirCabecalho();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Excluindo Caixa...");
            Console.WriteLine("---------------------------------------");

            Console.WriteLine();

            VisualizarCaixas(false);
            Console.WriteLine("Digite o Id da Caixa que deseja excluir");
            int idExcluir = int.Parse(Console.ReadLine());

            Caixa c = repositorioCaixa.SelecionarPorId(idExcluir);
            if (c == null)
            {
                Notificador.ExibirMensagem("O Id mencionado não existe, tente novamente", ConsoleColor.Red);
                ExcluirCaixa();
            }
            repositorioCaixa.Excluir(idExcluir);
            Notificador.ExibirMensagem("O registro foi excluído com sucesso!", ConsoleColor.Green);
        }
        public Caixa ObterDadosCaixas()
        {
            Console.Write("Digite a etiqueta da caixa: ");
            string? etiqueta = Console.ReadLine();
            Console.Write("Digite a cor da caixa: ");
            string? cor = Console.ReadLine();
            Console.Write("Digite qual o prazo para devoluções nessa caixa (em dias): ");
            string? dias = Console.ReadLine();
            int diasDeEmprestimo = Convertor.ConverterTextoInt(dias);
            Caixa novaCaixa = new Caixa(etiqueta, cor, diasDeEmprestimo);
            return novaCaixa;
        }
    }
}
