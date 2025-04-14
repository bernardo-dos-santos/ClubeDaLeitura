using ClubeDaLeituraConsoleApp.ModuloEmprestimo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
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
        public string ListaNegra = "Não";

        public Amigo(string nome, string nomeResponsavel, string telefone)
        {
            Nome = nome;
            NomeResponsavel = nomeResponsavel;
            Telefone = telefone;
        }

        public string Validar()
        {
            string erros = "";

            if (string.IsNullOrWhiteSpace(Nome))
                erros += "O campo 'Nome' é obrigatório.\n";

            else if (Nome.Length < 3 || Nome.Length > 100)
                erros += "O campo 'Nome' precisa conter mínimo 3 e máximo de 100 caracteres.\n";

            if (string.IsNullOrWhiteSpace(NomeResponsavel))
                erros += "O campo 'Nome responsável' é obrigatório.\n";

            else if (NomeResponsavel.Length < 3 || NomeResponsavel.Length > 100)
                erros += "O campo 'NomeResponsavel' precisa conter mínimo 3 e máximo de 100 caracteres.\n";

            if (string.IsNullOrWhiteSpace(Telefone))
                erros += "O campo 'Telefone' é obrigatório.\n";

            else if (Regex.Replace(Telefone, "[\\(\\)\\- ]", "").Length != 10 && Regex.Replace(Telefone, "[\\(\\)\\- ]", "").Length != 11)
                erros += "O campo 'Telefone' deve seguir o formato 00 0000-0000 ou 00 00000-0000.";

            return erros;
        }
        public string ValidarListaNegra(Emprestimo e, Amigo m)
        {
            bool listaNegra = false;
            if (e.Amigo == m)
            {
                if (e.Situacao == "Atrasado") listaNegra = true;
            }
            if (listaNegra == true) ListaNegra = "Sim";
            else ListaNegra = "Não";
                return ListaNegra;
        }


    }
}
