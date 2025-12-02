namespace Dialogix.ChatBot.Interfaces
{
    public interface IFlujoAgendarCita
    {
        Task<string> ListarEspecialidades();
        Task<string> ListarDoctoresSegunEspecialidad(string input);
        Task<string> ListarHorariosDisponiblesPorDoctor(string input);
        string ResumenCita(string prompt);
        Task<string> ConfirmaCita(string prompt);
    }
}
