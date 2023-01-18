namespace WebAPI.Models;

public class iwebDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string UsuariosCollectionName { get; set; } = null!;

    public string MessagesCollectionName { get; set; } = null!;

    public string AparcamientosCollectionName { get; set; } = null!;

    public string ImagenCollectionName { get; set; } = null!;

    public string LogsCollectionName { get; set; } = null!;

}