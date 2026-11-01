namespace MauiAppLogin;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Verifica se já existe alguém logado guardado no telemóvel/PC
        string usuario_logado = await SecureStorage.Default.GetAsync("usuario_logado");

        if (!string.IsNullOrEmpty(usuario_logado))
        {
            // Se já estiver logado, pula o login e vai direto para a página protegida
            App.Current.MainPage = new Protegida();
        }
    }
    private async void Button_Clicked(object sender, EventArgs e)
    {
		try
		{

			List<DadosUsuario> lista_usuarios = new List<DadosUsuario>()
			{
				new DadosUsuario ()
				{
					Usuario = "jose",
					Senha = "123"
					

				},
				new DadosUsuario()
				{
					Usuario = "maria",
					Senha = "321"
				}
			};

			DadosUsuario dados_digitados = new DadosUsuario()
			{
				Usuario = txt_usuario.Text,
				Senha = txt_senha.Text,
			};

			if(lista_usuarios.Any(i => (dados_digitados.Usuario == i.Usuario && dados_digitados.Senha == i.Senha) ))
			{
				await SecureStorage.Default.SetAsync("usuario_logado", dados_digitados.Usuario);

				App.Current.MainPage = new Protegida();
			} else
			{
				throw new Exception("Usuário ou senha incorretos.");
			}

		}
		catch (Exception ex)
		{
			await DisplayAlert("Ops", ex.Message, "Fechar");
		}
		}
    }
