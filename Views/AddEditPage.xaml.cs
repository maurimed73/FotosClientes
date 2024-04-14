using FotosClientes.Models;
using FotosClientes.Database.Repositories;
using Firebase.Storage;



namespace FotosClientes.Views;

public partial class AddEditPage : ContentPage
{

    private IClienteModelRepository _repository;
    private FotoClienteModel _fotoClienteModel;

    private string lblPathCliente = "nophoto.jpg";
    private string lblPathOrdem = "nophoto.jpg";
    private string lblPathArmacao = "nophoto.jpg";


    public AddEditPage()
	{
		InitializeComponent();
        _repository = new ClienteModelRepository();
        _fotoClienteModel = new FotoClienteModel();
        ImageFotoCliente.Source = "nophoto.jpg";
        ImageFotoOrdem.Source = "nophoto.jpg";
        ImageFotoArmacao.Source = "nophoto.jpg";
	}

    public AddEditPage(FotoClienteModel fotoCliente)
    {
        _repository = new ClienteModelRepository();
        InitializeComponent();
        _fotoClienteModel = fotoCliente;
        FillFields();
    }

    private void FillFields()
    {
        Entry_FotoClienteName.Text = _fotoClienteModel.Name;
        Entry_FotoClienteOrdem.Text = _fotoClienteModel.Ordem;
        DatePicker_FotoClienteData.Date = _fotoClienteModel.DataRegistro.Date;
        ImageFotoCliente.Source = _fotoClienteModel.Foto1;
        ImageFotoOrdem.Source = _fotoClienteModel.Foto2;
        ImageFotoArmacao.Source = _fotoClienteModel.Foto3;

    }

    private void CloseModal_Clicked(object sender, EventArgs e)
    {
		Navigation.PopModalAsync();
    }

    private async void SalvarModal_Clicked(object sender, EventArgs e)
    {

        var fileResult = await FilePicker.PickAsync();
        var fileToUpload = await fileResult.OpenReadAsync();
        var firebaseStorage = await new FirebaseStorage("albumclientes-7ee16.appspot.com")
          .Child(fileResult.FileName)
          .PutAsync(fileToUpload);

        //Obter os dados
        GetDataFromForm();

        //Validar os dados
        bool valid = ValidateData();

        //Salvar os dados
        if (valid)
        {
            SaveInDatabase();
            //Fechar a tela
            Navigation.PopModalAsync();
            //DisplayAlert("Salvar", "Registro salvo com sucesso!", "Ok");
        }
        
        //Solicitar a atualização da listagem da tela anterior
        UpdateListInStartPage();
        


    }

    private void UpdateListInStartPage()
    {
        var navPage = (NavigationPage)App.Current.MainPage;
        var startPage = (StartPage)navPage.CurrentPage;
        startPage.LoadData();
    }

    private void GetDataFromForm()
    {
        _fotoClienteModel.Name = Entry_FotoClienteName.Text;
        _fotoClienteModel.Ordem = Entry_FotoClienteOrdem.Text;
        _fotoClienteModel.DataRegistro = DateTime.Now;
        _fotoClienteModel.Foto1 = lblPathCliente;
        _fotoClienteModel.Foto2 = lblPathOrdem;
        _fotoClienteModel.Foto3 = lblPathArmacao;

    }

    private bool ValidateData()
    {
        lbl_clienteName_Required.IsVisible = false;
        lbl_clienteOrdem_Required.IsVisible = false;

        bool validResult = true;
        if (string.IsNullOrWhiteSpace(_fotoClienteModel.Name))
        {
            lbl_clienteName_Required.IsVisible = true; 
            validResult = false;
        }

        if (string.IsNullOrWhiteSpace(_fotoClienteModel.Ordem))
        {
            lbl_clienteOrdem_Required.IsVisible = true;
            validResult = false;
        }

        if (_fotoClienteModel.Foto1 == "")
        {
           validResult = false;
        }

        if (_fotoClienteModel.Foto2 == "")
        {
            validResult = false;
        }

        if (_fotoClienteModel.Foto3 == "")
        {
            validResult = false;
        }
        return validResult;

    }

    private  void SaveInDatabase()
    {
       
        if (_fotoClienteModel.Id == _fotoClienteModel.Id)
            _repository.Add(_fotoClienteModel);
        
        else
            _repository.Update(_fotoClienteModel);
    }

    private async void buttonCliente_Clicked(object sender, EventArgs e)
    {
      
            string lblPath = string.Empty;
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

                if (photo != null)
                {
                    // save the file into local storage
                    string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                    using Stream sourceStream = await photo.OpenReadAsync();
                    using FileStream localFileStream = File.OpenWrite(localFilePath);

                    await sourceStream.CopyToAsync(localFileStream);
                    lblPathCliente = localFilePath;
                    ImageFotoCliente.Source = lblPathCliente;
                }

        }
    }

    private async void buttonOrdem_Clicked(object sender, EventArgs e)
    {
        string lblPath = string.Empty;
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

            if (photo != null)
            {
                // save the file into local storage
                string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                using Stream sourceStream = await photo.OpenReadAsync();
                using FileStream localFileStream = File.OpenWrite(localFilePath);

                await sourceStream.CopyToAsync(localFileStream);
                lblPathOrdem = localFilePath;
                ImageFotoOrdem.Source = lblPathOrdem;
            }

        }
    }

    private async void buttonArmacao_Clicked(object sender, EventArgs e)
    {
        string lblPath = string.Empty;
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

            if (photo != null)
            {
                // save the file into local storage
                string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                using Stream sourceStream = await photo.OpenReadAsync();
                using FileStream localFileStream = File.OpenWrite(localFilePath);

                await sourceStream.CopyToAsync(localFileStream);
                lblPathArmacao = localFilePath;
                ImageFotoArmacao.Source = lblPathArmacao;
            }

        }
    }
}