using ClubeDaLeituraConsoleApp.Compartilhado;
using ClubeDaLeituraConsoleApp.ModuloEmprestimo;
using ClubeDaLeituraConsoleApp.ModuloRevista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeDaLeituraConsoleApp.ModuloAmigo
{
    public class RepositorioAmigo
    {
        public Amigo[] amigos = new Amigo[100];
        public int contadorAmigos = 0;

        public void Cadastrar(Amigo amigo)
        {
            amigo.Id = GeradorId.GerarIdAmigo();
            amigos[contadorAmigos] = amigo;
            contadorAmigos++;
        }

        public void Editar(int idAmigo, Amigo amigo)
        {
            for (int i = 0; i < amigos.Length; i++)
            {
                if (amigos[i] == null) continue;

                if (amigos[i].Id == idAmigo)
                {
                    amigos[i].Nome = amigo.Nome;
                    amigos[i].NomeResponsavel = amigo.NomeResponsavel;
                    amigos[i].Telefone = amigo.Telefone;
                }
            }
        }
        public void Excluir(int idAmigo)
        {
            for (int i = 0; i < amigos.Length; i++)
            {
                if (amigos[i] == null) continue;

                if (amigos[i].Id == idAmigo)
                {
                    amigos[i] = null;
                }
            }
        }
        public Amigo SelecionarPorId(int idAmigos)
        {
            for (int i = 0; i < amigos.Length; i++)
            {
                Amigo a = amigos[i];

                if (a == null)
                    continue;

                else if (a.Id == idAmigos)
                    return a;
            }

            return null;
        }

        
        

        public Amigo[] SelecionarTodos()
        {
            return amigos;
        }



    }
}
