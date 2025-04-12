using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeDaLeituraConsoleApp.Compartilhado
{
    public class GeradorId
    {
        public int IdAmigo = 0;

        public int GerarIdAmigo()
        {
            IdAmigo++;
            return IdAmigo;
        }

    }
}
