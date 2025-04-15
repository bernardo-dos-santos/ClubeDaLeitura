using ClubeDaLeituraConsoleApp.Compartilhado;
using ClubeDaLeituraConsoleApp.ModuloAmigo;
using ClubeDaLeituraConsoleApp.ModuloEmprestimo;
using ClubeDaLeituraConsoleApp.ModuloRevista;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeDaLeituraConsoleApp.ModuloReservas
{
    public class RepositorioReserva
    {
        public RepositorioEmprestimo repositorioEmprestimo;
        public Reserva[] reservas = new Reserva[100];
        public int contadorReservas = 0;
        public RepositorioReserva(RepositorioEmprestimo repositorioEmprestimo)
        {
            this.repositorioEmprestimo = repositorioEmprestimo;
        }

        public void Registrar(Reserva r)
        {
            r.Id = GeradorId.GerarIdReserva();
            r.revista.Reservar();
            r.amigo.emprestimo = true;
            reservas[contadorReservas] = r;
            contadorReservas++;
        }

        public void Excluir(int Id)
        {
            for (int i = 0; i < reservas.Length; i++)
            {
                if (reservas[i] == null) continue;

                if (reservas[i].Id == Id)
                {
                    if (reservas[i].revista.StatusAtual == "Reservada")
                    {
                        reservas[i].revista.Devolver();
                        reservas[i].amigo.emprestimo = false;
                    }
                    
                    reservas[i] = null;
                }
            }

        }

        public Reserva SelecionarPorId(int Id)
        {
            Reserva r;
            for (int i = 0; i < reservas.Length; i++)
            {
                r = reservas[i];
                if (r == null) continue;
                if(r.Id == Id)
                {
                    return r;
                }
            }
            return null;
        } 
        
        public Reserva[] SelecionarTodos()
        {
            return reservas;
        }
    }
}
