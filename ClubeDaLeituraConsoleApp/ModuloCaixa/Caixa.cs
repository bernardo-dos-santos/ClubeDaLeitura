using ClubeDaLeituraConsoleApp.Compartilhado;
using ClubeDaLeituraConsoleApp.ModuloRevista;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClubeDaLeituraConsoleApp.ModuloCaixa
{
    public class Caixa : EntidadeBase<Caixa>
    {
        public string Etiqueta { get; set; }
        public string Cor { get; set; }
        public int DiasDeEmprestimo { get; set; }
        public List<Revista> revistasNaCaixa { get; set; } = new List<Revista>();

        public RepositorioCaixa repositorioCaixa;

        public Caixa(string etiqueta, string cor, int diasDeEmprestimo, RepositorioCaixa repositorioCaixa)
        {
            Etiqueta = etiqueta;
            Cor = cor;
            DiasDeEmprestimo = diasDeEmprestimo;
            this.repositorioCaixa = repositorioCaixa;
        }

        public void AdicionarRevista(Revista r)
        {
            revistasNaCaixa.Add(r);
        }
        public void RemoverRevista(Revista r)
        {           
            revistasNaCaixa.Remove(r);      
        }

        public override string Validar()
        {
            List<Caixa> caixas = repositorioCaixa.SelecionarRegistros();
            string erros = "";
            if (string.IsNullOrWhiteSpace(Etiqueta))
                erros += "O campo 'Etiqueta' é obrigatório.\n";
            else if(Etiqueta.Length > 50)
                erros += "O campo 'Etiqueta' pode ter até 50 caracteres.\n";
            foreach (var item in caixas)
            {
                foreach (var item2 in caixas)
                {
                    if (item != item2 && item.Etiqueta == item2.Etiqueta)
                        erros += "O campo 'Etiqueta' precisa ser único.\n";
                }
            }

            if (string.IsNullOrWhiteSpace(Cor))
                erros += "O campo 'Cor' é obrigatório.\n";

            else if (!(ListaDeCores.Cores.Contains(Cor.ToUpper())))
                erros += "Cor Inválida.\n";

            if (DiasDeEmprestimo < 1)
                erros += "O campo 'Dias de Empréstimo' precisa ser maior que 0.\n";
            return erros;
        }
        public bool ValidarExclusao()
        {
            if (revistasNaCaixa == null) return false;
            else
            {
                Notificador.ExibirMensagem("Não é possível excluir caixas que tem revistas", ConsoleColor.Red);
                return true;
            }
        }

        public override void AtualizarRegistro(Caixa registroEditado)
        {
            Etiqueta = registroEditado.Etiqueta;
            Cor = registroEditado.Cor;
            DiasDeEmprestimo = registroEditado.DiasDeEmprestimo;
        }

        
    }
}
