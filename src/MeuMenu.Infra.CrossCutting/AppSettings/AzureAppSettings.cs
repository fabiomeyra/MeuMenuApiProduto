namespace MeuMenu.Infra.CrossCutting.AppSettings;

public class AzureAppSettings : BaseAppSettings
{
    private string? _armazenamentoImagens;
    private string? _nomePastaImagens;
    public string? ArmazenamentoImagens
    {
        get => RetornaValorDescriptografado(_armazenamentoImagens);
        set => _armazenamentoImagens = value;
    }

    public string? NomePastaImagens
    {
        get => RetornaValorDescriptografado(_nomePastaImagens);
        set => _nomePastaImagens = value;
    }
}