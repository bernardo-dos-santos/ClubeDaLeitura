using ClubeDaLeituraConsoleApp.Compartilhado;
using ClubeDaLeituraConsoleApp.ModuloRevista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeDaLeituraConsoleApp.ModuloEmprestimo
{
    public class RepositorioEmprestimo : RepositorioBase<Emprestimo>
    {

        public void Cadastrar(Emprestimo e)
        {
            e.Situacao = e.situacoes[0];
            e.Amigo.emprestimo = true;
            e.Revista.Emprestar();
            CadastrarRegistro(e);
        }
        public void RegistrarMultas()
        {
            string situacao = "Atrasado";
            foreach (var e in SelecionarRegistros())
            {
                if (e.Situacao == situacao)
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
