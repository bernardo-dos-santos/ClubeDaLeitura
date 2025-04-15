using ClubeDaLeituraConsoleApp.ModuloAmigo;
using ClubeDaLeituraConsoleApp.ModuloRevista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeDaLeituraConsoleApp.ModuloReservas
{
    public class Reserva
    {
        public int Id;
        public Amigo amigo;
        public Revista revista;
        public DateTime DataReserva;
        public string[] Status = new string[] { "Reservado", "Concluído" };
        public string StatusAtual;

        public Reserva(Amigo amigo, Revista revista)
        {
            this.amigo = amigo;
            this.revista = revista;
            DataReserva = DateTime.Now;
            StatusAtual = Status[0];
        }
    }
}
