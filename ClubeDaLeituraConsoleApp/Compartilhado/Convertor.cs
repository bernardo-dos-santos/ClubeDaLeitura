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
            if (!int.TryParse(Id, out IdEditar))
            {
                
                Notificador.ExibirMensagem("O número mencionado não existe, retornando", ConsoleColor.Red);
                return 0;
            } else
            {
                if (IdEditar == 0)
                {
                    Notificador.ExibirMensagem("O número 0 não é correto nesse escopo", ConsoleColor.Red);
                }
            }
                return IdEditar;
        }
    }
}
