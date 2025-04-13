using ClubeDaLeituraConsoleApp.ModuloRevista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
