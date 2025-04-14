using ClubeDaLeituraConsoleApp.Compartilhado;
using ClubeDaLeituraConsoleApp.ModuloRevista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClubeDaLeituraConsoleApp.ModuloCaixa
{
    public class Caixa
    {
        public int Id;
        public string Etiqueta;
        public string Cor;
        public int DiasDeEmprestimo;
        public Revista[] revistasNaCaixa = new Revista[40];
        public int contadorRevistas = 0;
        public Caixa(string etiqueta, string cor, int diasDeEmprestimo)
        {
            Etiqueta = etiqueta;
            Cor = cor;
            DiasDeEmprestimo = diasDeEmprestimo;
        }

        public void AdicionarRevista(Revista r)
        {
            revistasNaCaixa[contadorRevistas] = r;
            contadorRevistas++;
        }
        public void RemoverRevista(Revista r)
        {
            for (int i = 0; i < revistasNaCaixa.Length; i++)
            {
                if (revistasNaCaixa[i] == null) continue;
                if (revistasNaCaixa[i].Id == r.Id)
                {
                    revistasNaCaixa[i] = null;
                    return;
                }
            
            };
        }

        public string Validar(RepositorioCaixa repositorioCaixa)
        {
            Caixa[] caixas = repositorioCaixa.caixas;
            string erros = "";
            if (string.IsNullOrWhiteSpace(Etiqueta))
                erros += "O campo 'Etiqueta' é obrigatório.\n";
            else if(Etiqueta.Length > 50)
                erros += "O campo 'Etiqueta' pode ter até 50 caracteres.\n";
                for (int i = 0; i < caixas.Length; i++)
                {
                if (caixas[i] == null) continue;
                    for (int j = 0; j < caixas.Length; j++)
                    {
                        if (caixas[j] == null) continue;
                    if (caixas[i].Etiqueta == caixas[j].Etiqueta)
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
        public bool ValidarExclusao(Caixa c)
        {
            if (c.revistasNaCaixa == null) return false;
            else
            {
                Notificador.ExibirMensagem("Não é possível excluir caixas que tem revistas", ConsoleColor.Red);
                return true;
            }
        }
    }
}
