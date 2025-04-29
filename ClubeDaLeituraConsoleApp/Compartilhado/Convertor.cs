using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeDaLeituraConsoleApp.Compartilhado
{
    public static class Convertor
    {
        public static int ConverterTextoInt()
        {
            string? Id = Console.ReadLine();
                
            int IdEditar;
            if (!int.TryParse(Id, out IdEditar) || IdEditar < 1)
            {
                
                Notificador.ExibirMensagem("O número mencionado não existe, retornando", ConsoleColor.Red);
                return 0;
            }
                return IdEditar;
        }
    }
}
