using ClubeDaLeituraConsoleApp.Compartilhado;
using ClubeDaLeituraConsoleApp.ModuloRevista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeDaLeituraConsoleApp.ModuloEmprestimo
{
    public class RepositorioEmprestimo
    {
        public Emprestimo[] emprestimos = new Emprestimo[100];
        public int contadorEmprestimo = 0;

        public void Cadastrar(Emprestimo e, Revista r)
        {
            e.Id = GeradorId.GerarIdEmprestimo();
            e.Situacao = e.situacoes[0];
            e.Amigo.emprestimo = true;
            r.Emprestar(r);
            emprestimos[contadorEmprestimo] = e;
            contadorEmprestimo++;
        }

        public Emprestimo SelecionarPorId(int id)
        {
            for (int i = 0; i < emprestimos.Length; i++)
            {
                Emprestimo e = emprestimos[i];
                if (e == null) continue;

                if (e.Id == id)
                {
                    return e;
                }
            }
            return null;
        }

        public Emprestimo[] SelecionarTodos()
        {
            return emprestimos;
        }
    }
}
