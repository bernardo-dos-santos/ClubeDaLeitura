using ClubeDaLeituraConsoleApp.Compartilhado;
using ClubeDaLeituraConsoleApp.ModuloAmigo;
using ClubeDaLeituraConsoleApp.ModuloCaixa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeDaLeituraConsoleApp.ModuloRevista
{
    public class RepositorioRevista
    {
        public Revista[] revistas = new Revista[200];
        public int contadorRevistas = 0;
        public RepositorioCaixa repositorioCaixa;
        public RepositorioRevista(RepositorioCaixa repositorioCaixa)
        {
            this.repositorioCaixa = repositorioCaixa;
        }
        public void Adicionar(Revista revista)
        {
            revista.caixa = repositorioCaixa.caixas[revista.IdCaixa - 1].Etiqueta;
            repositorioCaixa.caixas[revista.IdCaixa - 1].AdicionarRevista(revista);
            revistas[contadorRevistas] = revista;
            revistas[contadorRevistas].StatusAtual = revistas[contadorRevistas].StatusEmprestimo[1];
            revistas[contadorRevistas].Id = GeradorId.GerarIdRevista();
            
        }

        public void Editar(int idRevista, Revista revista, int idAntigo)
        {
            // uso esse if pq se o Id for o mesmo, iria adicionar a mesma revista na mesma caixa 2 vezes
            // Não necessariamente o usuário vai mudar o IdCaixa
            if (!(idAntigo == revista.IdCaixa))
            {
                //Adicionando em outra Caixa
                repositorioCaixa.caixas[revista.IdCaixa -1 ].AdicionarRevista(revista);
                revista.caixa = repositorioCaixa.caixas[revista.IdCaixa - 1].Etiqueta;
                //Removendo da Classe Antiga
                repositorioCaixa.caixas[idAntigo - 1].RemoverRevista(revista);
            }
            for (int i = 0; i < revistas.Length; i++)
            {
                if (revistas[i] == null) continue;

                if (revistas[i].Id == idRevista)
                {
                    revistas[i].Titulo = revista.Titulo;
                    revistas[i].NumeroEdicao = revista.NumeroEdicao;
                    revistas[i].AnoPublicado = revista.AnoPublicado;
                    revistas[i].IdCaixa = revista.IdCaixa;
                }
            }
        }

        public void Excluir(int idRevista, RepositorioCaixa repositorioCaixa)
        {
            
            for (int i = 0; i < revistas.Length; i++)
            {
                if (revistas[i] == null) continue;
                if (repositorioCaixa.caixas[i].revistasNaCaixa[i].Id == idRevista)
                    repositorioCaixa.caixas[i].revistasNaCaixa[i] = null;

                if (revistas[i].Id == idRevista)
                {
                revistas[i] = null;
                }
            }
        }
        public Revista SelecionarPorId(int idRevista)
        {
            for (int i = 0; i < revistas.Length; i++)
            {
                Revista r = revistas[i];

                if (r == null)
                    continue;

                else if (r.Id == idRevista)
                    return r;
            }

            return null;
        }

        public Revista[] SelecionarTodos()
        {
            return revistas;
        }
    }
}
