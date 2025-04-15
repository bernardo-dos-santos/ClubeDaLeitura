using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeDaLeituraConsoleApp.Compartilhado
{
    public static class GeradorId
    {
        public static int IdAmigo = 0;
        public static int IdRevista = 0;
        public static int IdCaixa = 0;
        public static int IdEmprestimo = 0;
        public static int IdReserva = 0;
        public static int GerarIdAmigo()
        {
            IdAmigo++;
            return IdAmigo;
        }

        public static int GerarIdRevista()
        {
            IdRevista++;
            return IdRevista;
        }

        public static int GerarIdCaixa()
        {
            IdCaixa++;
            return IdCaixa;
        }

        public static int GerarIdEmprestimo()
        {
            IdEmprestimo++;
            return IdEmprestimo;
        }

        public static int GerarIdReserva()
        {
            IdReserva++;
            return IdReserva;
        }

    }
}
