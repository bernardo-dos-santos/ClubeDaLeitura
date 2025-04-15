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

        public RepositorioReserva(RepositorioEmprestimo repositorioEmprestimo)
        {
            this.repositorioEmprestimo = repositorioEmprestimo;
        }

        public void RegistrarReserva()
        {

        }

        
    }
}
