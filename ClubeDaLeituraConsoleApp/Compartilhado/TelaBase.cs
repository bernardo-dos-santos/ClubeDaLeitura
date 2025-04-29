namespace ClubeDaLeituraConsoleApp.Compartilhado
{

	public abstract class TelaBase<T> where T : EntidadeBase<T>
	{
		protected string nomeEntidade;
		private RepositorioBase<T> repositorio;

		protected TelaBase(string nomeEntidade, RepositorioBase<T> repositorio)
		{
			this.nomeEntidade = nomeEntidade;
			this.repositorio = repositorio;
		}

		public void ExibirCabecalho()
		{
			Console.Clear();
			Console.WriteLine("--------------------------------------------");
			Console.WriteLine($"Menu de {nomeEntidade}s");
			Console.WriteLine("--------------------------------------------");
		}

		public virtual string ApresentarMenu()
		{
			ExibirCabecalho();

			Console.WriteLine();

			Console.WriteLine($"1 - Cadastrar {nomeEntidade}");
			Console.WriteLine($"2 - Editar {nomeEntidade}");
			Console.WriteLine($"3 - Visualizar {nomeEntidade}");
			Console.WriteLine($"4 - Excluir {nomeEntidade}s");

			Console.WriteLine("5 - Voltar");

			Console.WriteLine();

			Console.Write("Escolha uma das opções: ");
			string operacaoEscolhida = Console.ReadLine()!;

			return operacaoEscolhida;
		}

		public virtual void CadastrarRegistro()
		{
			ExibirCabecalho();
            Console.WriteLine();

            Console.WriteLine($"Cadastrando {nomeEntidade}...");
            Console.WriteLine("--------------------------------------------");

            Console.WriteLine();

			T novoRegistro = ObterDados();
			if (novoRegistro == null) return;
			string erros = novoRegistro.Validar();

			if(erros.Length > 0)
			{
				Notificador.ExibirMensagem(erros, ConsoleColor.Red);
				CadastrarRegistro();
				return;
			}

			repositorio.CadastrarRegistro(novoRegistro);
			Notificador.ExibirMensagem("O Cadastro foi realizado com sucesso", ConsoleColor.Green);

        }

		public virtual void EditarRegistro()
		{
			ExibirCabecalho();
            Console.WriteLine();

            Console.WriteLine($"Editando {nomeEntidade}");
            Console.WriteLine("--------------------------------------------");

			VisualizarRegistros(false);

            Console.Write("Digite o Id do registro que deseja selecionar: ");
			int id = Convertor.ConverterTextoInt();
			if (id == 0) return;
            Console.WriteLine();
			T novoRegistro = ObterDados();
			if (novoRegistro == null) return;
			string erros = novoRegistro.Validar();

			if(erros.Length > 0)
			{
				Notificador.ExibirMensagem(erros, ConsoleColor.Red);
				EditarRegistro();
				return;
			}
            bool conseguiuEditar = repositorio.EditarRegistro(id, novoRegistro);

            if (!conseguiuEditar)
            {
                Notificador.ExibirMensagem("Houve um erro durante a edição do registro...", ConsoleColor.Red);

                return;
            }

            Notificador.ExibirMensagem("O registro foi editado com sucesso!", ConsoleColor.Green);
        }
        

		public virtual void ExcluirRegistro()
		{
            ExibirCabecalho();
            Console.WriteLine();

            Console.WriteLine($"Excluindo {nomeEntidade}");
            Console.WriteLine("--------------------------------------------");

            VisualizarRegistros(false);

            Console.Write("Digite o Id do registro que deseja selecionar: ");
            int id = Convertor.ConverterTextoInt();
            if (id == 0) return;
            Console.WriteLine();
			T registroExcluir = repositorio.SelecionarRegistroPorId(id);
            bool conseguiuExcluir = repositorio.ExcluirRegistro(id, registroExcluir);

            if (!conseguiuExcluir)
            {
                Notificador.ExibirMensagem("Houve um erro durante a edição do registro...", ConsoleColor.Red);

				return;
            }

            Notificador.ExibirMensagem("O registro foi editado com sucesso!", ConsoleColor.Green);
        }

		public abstract void VisualizarRegistros(bool Titulo);

		public abstract T ObterDados();
	}




}