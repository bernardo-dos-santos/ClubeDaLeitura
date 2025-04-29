using System.ComponentModel;

namespace ClubeDaLeituraConsoleApp.Compartilhado
{

    public abstract class  RepositorioBase<T> where T : EntidadeBase<T>
    {
        
        private List<T> registros = new List<T>();
		private int contadorIds = 0;

		public virtual void CadastrarRegistro(T novoregistro)
		{
			novoregistro.Id = ++contadorIds;
			registros.Add(novoregistro);
		}

        public bool EditarRegistro(int id, T registroEditado)
        {
            foreach (T item in registros)
            {
                if(item.Id == id)
                {
					item.AtualizarRegistro(registroEditado);
					return true;
				}
            }
            return false;
        }

		public bool ExcluirRegistro(int id, T registroExcluido)
		{
			T registroSelecionado = SelecionarRegistroPorId(id);

			if (registroSelecionado != null)
			{
				registros.Remove(registroSelecionado);

				return true;
			}
			return false;
		}

		public T SelecionarRegistroPorId(int id)
		{
			foreach (T item in registros)
			{
				if (item.Id == id)
					return item;
			}
			return null;
		}

		public List<T> SelecionarRegistros()
		{
			return registros;
		}



	}




}