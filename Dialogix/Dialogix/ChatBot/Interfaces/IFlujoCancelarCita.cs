namespace Dialogix.ChatBot.Interfaces
{
    public interface IFlujoCancelarCita
    {
        Task<string> MostrarInformacionCitaMedica();
        Task<string> ConfirmarCancelarCita(string prompt);
    }
}
