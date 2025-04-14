using ClubeDaLeituraConsoleApp.Compartilhado;
using ClubeDaLeituraConsoleApp.ModuloRevista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeDaLeituraConsoleApp.ModuloCaixa
{
    public class RepositorioCaixa
    {
        public Caixa[] caixas = new Caixa[100];
        public int contadorCaixas = 0;

        public void Adicionar(Caixa caixa)
        {
            caixas[contadorCaixas] = caixa;
            caixas[contadorCaixas].Id = GeradorId.GerarIdCaixa();
            contadorCaixas++;
        }

        public void Editar(int idCaixa, Caixa caixa)
        {
            for (int i = 0; i < caixas.Length; i++)
            {
                if (caixas[i] == null) continue;

                if (caixas[i].Id == idCaixa)
                {
                    caixas[i].Etiqueta = caixa.Etiqueta;
                    caixas[i].Cor = caixa.Cor;
                    caixas[i].DiasDeEmprestimo = caixa.DiasDeEmprestimo;
                }
            }
        }

        public void Excluir(int idCaixa)
        {
            for (int i = 0; i < caixas.Length; i++)
            {
                if (caixas[i] == null) continue;

                if (caixas[i].Id == idCaixa)
                {
                    caixas[i] = null;
                }
            }
        }
        public Caixa SelecionarPorId(int idCaixa)
        {
            for (int i = 0; i < caixas.Length; i++)
            {
                Caixa c = caixas[i];

                if (c == null)
                    continue;

                else if (c.Id == idCaixa)
                    return c;
            }

            return null;
        }

        public Caixa[] SelecionarTodos()
        {
            return caixas;
        }
    }
}
