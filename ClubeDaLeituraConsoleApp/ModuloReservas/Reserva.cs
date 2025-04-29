using ClubeDaLeituraConsoleApp.Compartilhado;
using ClubeDaLeituraConsoleApp.ModuloAmigo;
using ClubeDaLeituraConsoleApp.ModuloRevista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeDaLeituraConsoleApp.ModuloReservas
{
    public class Reserva : EntidadeBase<Reserva>
    {
        public Amigo amigo { get; set; }
        public Revista revista { get; set; }
        public DateTime DataReserva { get; set; }
        public string[] Status { get; set; } = new string[] { "Reservado", "Concluído" };
        public string StatusAtual { get; set; }

        public Reserva(Amigo amigo, Revista revista)
        {
            this.amigo = amigo;
            this.revista = revista;
            DataReserva = DateTime.Now;
            StatusAtual = Status[0];
        }

        public override void AtualizarRegistro(Reserva registroEditado)
        {
            throw new NotImplementedException();
        }

        public override string Validar()
        {
            throw new NotImplementedException();
        }
    }
}
