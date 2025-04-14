using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeDaLeituraConsoleApp.Compartilhado
{
    public static class Convertor
    {
        public static int ConverterTextoInt(string? s)
        {
            if (int.TryParse(s, out int resultado))
            {
                return resultado;
            }
            else return 0;

        }
    }
}
