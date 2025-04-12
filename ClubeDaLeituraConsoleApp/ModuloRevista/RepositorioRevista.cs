using ClubeDaLeituraConsoleApp.Compartilhado;
using ClubeDaLeituraConsoleApp.ModuloAmigo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeDaLeituraConsoleApp.ModuloRevista
{
    public class RepositorioRevista
    {
        public Revista[] revistas = new Revista[100];
        public int contadorRevistas = 0;
        public void Adicionar(Revista revista)
        {
            revista.StatusEmprestimo = "Disponível";
            revistas[contadorRevistas].Titulo = revista.Titulo;
            revistas[contadorRevistas].NumeroEdicao = revista.NumeroEdicao;
            revistas[contadorRevistas].AnoPublicado = revista.AnoPublicado;
            revistas[contadorRevistas].StatusEmprestimo = revista.StatusEmprestimo;
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
                    revistas[i].StatusEmprestimo = revista.StatusEmprestimo;
                }
            }
        }

        public void Excluir(int idAmigo, Revista revista)
        {
            for (int i = 0; i < revistas.Length; i++)
            {
                if (revistas[i] == null) continue;

                if (revistas[i].Id == idAmigo)
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
