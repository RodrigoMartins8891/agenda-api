namespace AgendaApi2.Models
{
    // Consulta.cs
public class Consulta
{
    public int Id { get; set; }
    public string Paciente { get; set; } = string.Empty;
    public string Medico { get; set; } = string.Empty;
    public DateTime Data { get; set; }

    public int UsuarioId { get; set; }
    public Usuario? Usuario { get; set; } 
}

}
