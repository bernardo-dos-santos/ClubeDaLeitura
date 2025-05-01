using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubeDaLeituraConsoleApp.Interfaces
{
    public interface ITelaCrud : IApresentarOpcoes
    {
        string ApresentarMenu();
        void CadastrarRegistro();
        void EditarRegistro();
        void ExcluirRegistro();
        void VisualizarRegistros(bool exibirCabecalho);
    }
}
