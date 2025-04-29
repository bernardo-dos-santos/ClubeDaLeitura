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
    public class RepositorioReserva : RepositorioBase<Reserva>
    {
        public RepositorioEmprestimo repositorioEmprestimo;
        public RepositorioReserva(RepositorioEmprestimo repositorioEmprestimo)
        {
            this.repositorioEmprestimo = repositorioEmprestimo;
        }

        public void Registrar(Reserva r)
        {
            r.revista.Reservar();
            r.amigo.emprestimo = true;
            CadastrarRegistro(r);
        }

        public void Excluir(int Id)
        {
            List<Reserva> reservas = SelecionarRegistros();
            foreach (var r in reservas)
            {
                if (r.Id == Id)
                {
                    if (r.revista.StatusAtual == "Reservada")
                    {
                        r.revista.Devolver();
                        r.amigo.emprestimo = false;
                    }
                }
            }
            reservas.Remove(SelecionarRegistroPorId(Id));
        }
    }
}
