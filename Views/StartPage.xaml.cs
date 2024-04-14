
using FotosClientes.Database.Repositories;
using FotosClientes.Models;


namespace FotosClientes.Views;

public partial class StartPage : ContentPage
{
	private IClienteModelRepository _repository;
    private IList<FotoClienteModel> _clientes;
    
    public StartPage()

    {
		InitializeComponent();

		//TODO - Injeção de Dependência
		_repository = new ClienteModelRepository();
        
		LoadData();
	}

	public void LoadData()
	{
		 _clientes = _repository.GetAll();
        CollectionViewClientes.ItemsSource = _clientes;
       
    }

    
    private async void deletarItem(object sender, TappedEventArgs e)
    {
        if(e.Parameter != null)
        {
            var delcliente = (FotoClienteModel)e.Parameter;
            var confirm = await DisplayAlert("Confirme Exclusão", $"Registro {delcliente.Name} - {delcliente.Ordem} será deletado da base de dados", "Ok", "Cancelar");
            
            if (confirm)
            {
                _repository.Delete(delcliente);
                LoadData();
            }

        }
        

        
       
    }

    private void adicionarCliente(object sender, EventArgs e)
    {
       Navigation.PushModalAsync(new AddEditPage());

    }

    private  void OnTapToEdit(object sender, TappedEventArgs e)
    {
       
        var fotoCliente =(FotoClienteModel)e.Parameter;
        Navigation.PushModalAsync(new AddEditPage(_repository.GetById(fotoCliente.Id)));
    }

    private void OnTextChangedFilterList(object sender, TextChangedEventArgs e)
    {
        var word = e.NewTextValue;
        CollectionViewClientes.ItemsSource = _clientes.Where(a => a.Name.ToLower().Contains(word.ToLower())).ToList();
    }
}