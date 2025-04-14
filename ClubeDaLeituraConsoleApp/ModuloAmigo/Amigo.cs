using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeDaLeituraConsoleApp.ModuloAmigo
{
    public class Amigo
    {
        public int Id;
        public string Nome;
        public string NomeResponsavel;
        public string Telefone;
        public bool emprestimo = false;

        public Amigo(string nome, string nomeResponsavel, string telefone)
        {
            Nome = nome;
            NomeResponsavel = nomeResponsavel;
            Telefone = telefone;
        }


    }
}
