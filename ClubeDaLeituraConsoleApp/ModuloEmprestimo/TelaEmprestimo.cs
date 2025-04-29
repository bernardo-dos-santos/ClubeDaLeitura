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
    public class TelaEmprestimo : TelaBase<Emprestimo>
    {
        public RepositorioEmprestimo repositorioEmprestimo;
        public RepositorioAmigo repositorioAmigo;
        public RepositorioRevista repositorioRevista;
        public RepositorioCaixa repositorioCaixa;

        public TelaEmprestimo(RepositorioEmprestimo repositorioEmprestimo, RepositorioAmigo repositorioAmigo, RepositorioRevista repositorioRevista, RepositorioCaixa repositorioCaixa) : base("Empréstimo", repositorioEmprestimo)
        {
            this.repositorioEmprestimo = repositorioEmprestimo;
            this.repositorioAmigo = repositorioAmigo;
            this.repositorioRevista = repositorioRevista;
            this.repositorioCaixa = repositorioCaixa;
        }
        public override string ApresentarMenu()
        {
            ExibirCabecalho();

            Console.WriteLine("Selecione a opção desejada:");
            Console.WriteLine("1 - Registrar Empréstimo");
            Console.WriteLine("2 - Regitrar Devolução");
            Console.WriteLine("3 - Visualizar Empréstimos");
            Console.WriteLine("4 - Visualizar Multas");
            Console.WriteLine("5 - Registrar Pagamento");
            Console.WriteLine("6 - Retornar");
            string? opcao = Console.ReadLine();

            return opcao;
        }
        public override void CadastrarRegistro()
        {
            ExibirCabecalho();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Registrando Empréstimo");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine();
            Emprestimo novoEmprestimo = ObterDados();
            if (novoEmprestimo == null) return;
            if (novoEmprestimo.Amigo == null || novoEmprestimo.Revista == null)
            {
                Notificador.ExibirMensagem("Id Inválido, Retornando...", ConsoleColor.Red);
                return;
            }
            novoEmprestimo.Amigo.ValidarListaNegra(novoEmprestimo);
            if (novoEmprestimo.Amigo.emprestimo == true || novoEmprestimo.Amigo.ListaNegra == "Sim" || !(VerificarRevista(novoEmprestimo)))
            {
                Notificador.ExibirMensagem("Não é possível adicionar empréstimos a esse amigo ou revista", ConsoleColor.Red);
                return;
            }
                
            repositorioEmprestimo.Cadastrar(novoEmprestimo);
            Notificador.ExibirMensagem("O registro foi realizado com sucesso!", ConsoleColor.Green);
        }
        public void RegistrarDevolucao()
        {
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Registrando Devolução");
            Console.WriteLine("---------------------------------------");

            VisualizarRegistros(false);

            Console.Write("Digite o Id do Empréstimo que deseja devolver: ");
            int idDevolucao = Convertor.ConverterTextoInt();
            if (idDevolucao == 0) return;
            Emprestimo e = repositorioEmprestimo.SelecionarRegistroPorId(idDevolucao);
            if (e == null)
            {
                Notificador.ExibirMensagem("Id Inválido, Retornando...", ConsoleColor.Red);
                return;
            }
            if (e.TemMulta == true)
            {
                Notificador.ExibirMensagem("Não é possível fechar um empréstimo com multas", ConsoleColor.Red);
                return;
            }
            e.Situacao = e.situacoes[1];
            e.Amigo.emprestimo = false;
            e.Revista.Devolver();
            Notificador.ExibirMensagem("O registro foi realizado com sucesso!", ConsoleColor.Green);
        }
        public void VisualizarAmigos()
        {
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
                    if (p.Amigo == m)
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
        public override void VisualizarRegistros(bool titulo)
        {
            if (titulo) ExibirCabecalho();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Visualizando Empréstimos");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine(
                "{0, -6} | {1, -20} | {2, -20} | {3, -15} | {4, -15}",
                "Id", "Amigo", "Revista", "Situação", "Dia da Entrega"
            );
            foreach (var e in repositorioEmprestimo.SelecionarRegistros())
            {
                e.Amigo.ValidarListaNegra(e);
                if (e.Situacao == "Concluído")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(
                        "{0, -6} | {1, -20} | {2, -20} | {3, -15} | {4, -15}",
                        e.Id, e.Amigo.Nome, e.Revista.Titulo, e.Situacao, "Entregue", ConsoleColor.Green
                    );
                    Console.ResetColor();
                }
                else if (e.Situacao == "Aberto")
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(
                        "{0, -6} | {1, -20} | {2, -20} | {3, -15} | {4, -15}",
                        e.Id, e.Amigo.Nome, e.Revista.Titulo, e.Situacao, e.DataDevolucao.ToString("dd/MM/yyyy")
                    );
                    Console.ResetColor();
                }
                else if (e.Situacao == "Atrasado")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(
                        "{0, -6} | {1, -20} | {2, -20} | {3, -15} | {4, -15}",
                        e.Id, e.Amigo.Nome, e.Revista.Titulo, e.Situacao, "Atrasada"
                    );
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine
                        ("{0, -6} | {1, -20} | {2, -20} | {3, -15} | {4, -15}",
                        e.Id, e.Amigo.Nome, e.Revista.Titulo, e.Situacao, "Reservada");
                    Console.ResetColor();
                }
            }           
            Notificador.ExibirMensagem("Pressione ENTER para continuar...", ConsoleColor.DarkYellow);
        }
        public void VisualizarMultas(bool titulo)
        {
            repositorioEmprestimo.RegistrarMultas();

            if (titulo)
                ExibirCabecalho();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Visualizando Multas...");
            Console.WriteLine("---------------------------------------");

            Console.WriteLine();

            Console.WriteLine(
                "{0, -15} | {1, -30} | {2, -20} | {3, -25}",
                "Id Empréstimo", "Amigo", "Revista", "Valor Multa"
            );

            foreach (var e in repositorioEmprestimo.SelecionarRegistros())
            {
                if (e.TemMulta == true)
                {
                    Console.WriteLine(
                    "{0, -10} | {1, -30} | {2, -20} | {3, -25}",
                    e.Id, e.Amigo.Nome, e.Revista.Titulo, $"R${e.ValorMulta}"
                    );
                }
            }                         
            Console.WriteLine();

            Notificador.ExibirMensagem("Pressione ENTER para continuar...", ConsoleColor.DarkYellow);
        }
        public void RegistrarPagamento()
        {
            VisualizarMultas(false);
            Console.Write("Digite o Id do empréstimo que deseja quitar: ");
            int idMulta = Convertor.ConverterTextoInt();
            if (idMulta == 0) return;
            Emprestimo e = repositorioEmprestimo.SelecionarRegistroPorId(idMulta);
            if (e == null)
            {
                Notificador.ExibirMensagem("Id Inválido, Retornando...", ConsoleColor.Red);
                return;
            }
            repositorioEmprestimo.RegistrarPagamento(e);
            Notificador.ExibirMensagem("Obs: Se deseja registrar esse empréstimo como Concluído, vá à aba Registrar Devolução", ConsoleColor.DarkRed);
        }
        public override Emprestimo ObterDados()
        {
            VisualizarAmigos();
            Console.WriteLine();
            Console.Write("Digite o Id do Amigo que deseja:  ");
            int IdAmigo = Convertor.ConverterTextoInt();
            if (IdAmigo == 0) return null;

            Console.WriteLine();
            VisualizarRevistas();
            Console.WriteLine();
            Console.Write("Digite o Id da Revista que seu amigo deseja: ");
            int idRevista = Convertor.ConverterTextoInt();
            if (idRevista == 0) return null;

            Amigo amigo = repositorioAmigo.SelecionarRegistroPorId(IdAmigo);
            Revista revista = repositorioRevista.SelecionarRegistroPorId(idRevista);
            Emprestimo novoEmprestimo = new Emprestimo(amigo, revista, DateTime.Now, repositorioCaixa);
            return novoEmprestimo;
        }
        public bool VerificarRevista(Emprestimo e)
        {
            if (e.Revista.StatusAtual != "Disponível") return false;

            return true;
        }
        
    }
}
