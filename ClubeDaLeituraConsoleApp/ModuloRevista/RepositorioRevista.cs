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
        public void Adicionar(Revista revista)
        {
            revistas[contadorRevistas] = revista;
            revistas[contadorRevistas].StatusEmprestimo = "Disponível";
            revistas[contadorRevistas].Id = GeradorId.GerarIdRevista();
            
        }

        public void Editar(int idRevista, Revista revista)
        {
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

        public void Excluir(int idRevista)
        {
            for (int i = 0; i < revistas.Length; i++)
            {
                if (revistas[i] == null) continue;

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
