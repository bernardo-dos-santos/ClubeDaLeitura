using ClubeDaLeituraConsoleApp.Compartilhado;
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
    public class Amigo : EntidadeBase<Amigo>
    {
        public string Nome { get; set; }
        public string NomeResponsavel { get; set; }
        public string Telefone { get; set; }
        public bool emprestimo { get; set; }
        public string ListaNegra { get; set; } = "Não";

        public Amigo(string nome, string nomeResponsavel, string telefone)
        {
            Nome = nome;
            NomeResponsavel = nomeResponsavel;
            Telefone = telefone;
        }

        public override void AtualizarRegistro(Amigo registroEditado)
        {
            Nome = registroEditado.Nome;
            NomeResponsavel = registroEditado.NomeResponsavel;
            Telefone = registroEditado.Telefone;
        }

        public override string Validar()
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
        public void ValidarListaNegra(Emprestimo e)
        {
            bool listaNegra = false;
            
            if (e.Situacao == "Atrasado") listaNegra = true;
            if (listaNegra == true) ListaNegra = "Sim";
            else ListaNegra = "Não";
        }


    }
}
