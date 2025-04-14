﻿using ClubeDaLeituraConsoleApp.Compartilhado;
using ClubeDaLeituraConsoleApp.ModuloEmprestimo;
using ClubeDaLeituraConsoleApp.ModuloRevista;
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
        public RepositorioEmprestimo repositorioEmprestimo;

        public TelaAmigo(RepositorioAmigo repositorioAmigo, RepositorioEmprestimo repositorioEmprestimo)
        {
            this.repositorioAmigo = repositorioAmigo;
            this.repositorioEmprestimo = repositorioEmprestimo;
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
            string? opcao = Console.ReadLine();

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
            string erros = novoAmigo.Validar();
            if(erros.Length > 0)
            {
                Notificador.ExibirMensagem(erros, ConsoleColor.Red);
                return;
            }
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
            string? Id = Console.ReadLine();
            int IdEditar = Convertor.ConverterTextoInt(Id);
            Amigo amigoEditado = repositorioAmigo.SelecionarPorId(IdEditar);
             if (amigoEditado == null)
            {
                Notificador.ExibirMensagem("O Id mencionado não existe, retornando", ConsoleColor.Red);
                return;
            }
            
            amigoEditado = ObterDadosAmigos();
            string erros = amigoEditado.Validar();
            if (erros.Length > 0)
            {
                Notificador.ExibirMensagem(erros, ConsoleColor.Red);
                return;
            }
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
                "{0, -6} | {1, -20} | {2, -30} | {3, -20} | {4, -35}",
                "Id", "Nome", "Nome Responsavel", "Telefone", "Empréstimo - Situação Atual"
            );

            Amigo[] amigosCadastrados = repositorioAmigo.SelecionarTodos();
            Emprestimo[] emprestimosCadastrados = repositorioEmprestimo.SelecionarTodos();
            for (int i = 0; i < amigosCadastrados.Length; i++)
            {
                Amigo m = amigosCadastrados[i];
                Emprestimo e;
                string situacao = "Nenhuma", revista = "Nenhuma";
                for (int j = 0; j < emprestimosCadastrados.Length; j++)
                {
                    if (emprestimosCadastrados[j] == null) continue;
                    if (emprestimosCadastrados[j].Amigo == m)
                    {
                        e = emprestimosCadastrados[j];
                        situacao = e.Situacao;
                        revista = e.Revista.Titulo;
                    }                    
                }
                if (m == null) continue;
                

                Console.WriteLine(
                "{0, -6} | {1, -20} | {2, -30} | {3, -20} | {4, -35}",
                m.Id, m.Nome, m.NomeResponsavel, m.Telefone, $"{revista} - {situacao}");
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
            string? Id = Console.ReadLine();
            int idExcluir = Convertor.ConverterTextoInt(Id);

            Amigo a = repositorioAmigo.SelecionarPorId(idExcluir);
            if (a == null)
            {
                Notificador.ExibirMensagem("O Id mencionado não existe, retornando...", ConsoleColor.Red);
                return;
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
