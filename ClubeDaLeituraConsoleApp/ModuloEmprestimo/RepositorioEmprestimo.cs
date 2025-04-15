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

        public void Cadastrar(Emprestimo e)
        {
            e.Id = GeradorId.GerarIdEmprestimo();
            e.Situacao = e.situacoes[0];
            e.Amigo.emprestimo = true;
            e.Revista.Emprestar();
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

        public void RegistrarMultas()
        {
            string situacao = "Atrasado";
            for (int i = 0; i < emprestimos.Length; i++)
            {
                Emprestimo e = emprestimos[i];
                if (e == null) continue;
                if(e.Situacao == situacao)
                {
                    e.TemMulta = true;
                    int multa = (e.DataDevolucao - DateTime.Now).Days;
                    e.ValorMulta = multa * 2;
                }
            }
        }
        public void RegistrarPagamento(Emprestimo e)
        {
            e.TemMulta = false;
            e.ValorMulta = 0;
            e.Situacao = "Concluído";
        }
    }
}
