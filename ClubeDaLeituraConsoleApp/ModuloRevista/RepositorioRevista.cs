using ClubeDaLeituraConsoleApp.Compartilhado;
using ClubeDaLeituraConsoleApp.ModuloAmigo;
using ClubeDaLeituraConsoleApp.ModuloCaixa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ClubeDaLeituraConsoleApp.ModuloRevista
{
    public class RepositorioRevista : RepositorioBase<Revista>
    {
        public RepositorioCaixa repositorioCaixa;
        public RepositorioRevista(RepositorioCaixa repositorioCaixa)
        {
            this.repositorioCaixa = repositorioCaixa;
        }
        public void CadastrarRevista(Revista revista)
        {
            revista.caixa = repositorioCaixa.SelecionarRegistroPorId(revista.IdCaixa).Etiqueta;
            repositorioCaixa.SelecionarRegistroPorId(revista.IdCaixa).AdicionarRevista(revista);
            revista.StatusAtual = revista.StatusEmprestimo[1];
            CadastrarRegistro(revista);            
        }

        public bool Editar(int idRevista, Revista revista, int idAntigo)
        {
            // uso esse if pq se o Id for o mesmo, iria adicionar a mesma revista na mesma caixa 2 vezes
            // Não necessariamente o usuário vai mudar o IdCaixa
            if (!(idAntigo == revista.IdCaixa))
            {
                //Adicionando em outra Caixa
                repositorioCaixa.SelecionarRegistroPorId(revista.IdCaixa).AdicionarRevista(revista);
                revista.caixa = repositorioCaixa.SelecionarRegistroPorId(revista.IdCaixa).Etiqueta;
                //Removendo da Classe Antiga
                repositorioCaixa.SelecionarRegistroPorId(idAntigo).RemoverRevista(revista);
            }
            return EditarRegistro(idRevista, revista);
        }

        public bool Excluir(int idRevista, RepositorioCaixa repositorioCaixa)
        {

            foreach (var item in repositorioCaixa.SelecionarRegistros())
            {
                for (int i = 0; i < item.revistasNaCaixa.Count; i++)
                {
                    if (item.revistasNaCaixa[i].Id == idRevista)
                    {
                        item.RemoverRevista(SelecionarRegistroPorId(idRevista));
                        return ExcluirRegistro(idRevista, SelecionarRegistroPorId(idRevista));
                    }
                        
                }               
            }
            return false;
        }
    }
}
