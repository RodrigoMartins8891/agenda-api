namespace AgendaApi2.Dtos
{
    public class ConsultaCreateDto
    {
        public string Paciente { get; set; } = string.Empty;
        public string Medico { get; set; } = string.Empty;
        public DateTime Data { get; set; }
    }
}
