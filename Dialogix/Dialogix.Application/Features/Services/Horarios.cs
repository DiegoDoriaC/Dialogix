using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dialogix.Application.Features.Services
{
    public class Horarios
    {
        public List<TimeSpan> GenerarHorariosDisponibles(TimeSpan horaInicio, TimeSpan horaFin, List<TimeSpan> horariosOcupados, int intervaloMinutos = 30)
        {
            var horarios = new List<TimeSpan>();

            // Generar horarios cada 30 min entre inicio y fin
            for (var hora = horaInicio; hora <= horaFin; hora = hora.Add(TimeSpan.FromMinutes(intervaloMinutos)))
            {
                // Si ese horario no está en la lista de ocupados
                if (!horariosOcupados.Contains(hora))
                {
                    horarios.Add(hora);
                }
            }

            return horarios;
        }

        public async Task<List<string>> ObtenerHorariosDisponibles(int idMedico, DateTime fecha)
        {
            string diaSemana = fecha.ToString("dddd", new CultureInfo("es-ES")); // Lunes, Martes...

            // 1. Obtener horario del médico
            var horario = await _db.HorarioMedico
                .FirstOrDefaultAsync(h => h.IdMedico == idMedico && h.DiaSemana == diaSemana);

            if (horario == null)
                return new List<string>(); // No trabaja ese día

            var horaInicio = horario.HoraInicio;
            var horaFin = horario.HoraFin;

            // 2. Obtener horarios ocupados
            var ocupados = await _db.CitaMedica
                .Where(c => c.IdMedico == idMedico && c.FechaCita == fecha)
                .Select(c => c.HoraCita)
                .ToListAsync();

            // 3. Convertir Time a TimeSpan
            var ocupadosTimeSpan = ocupados.Select(o => o.TimeOfDay).ToList();

            // 4. Generar horarios disponibles
            var disponibles = GenerarHorariosDisponibles(horaInicio, horaFin, ocupadosTimeSpan);

            // 5. Convertir a string bonito para tu chatbot
            return disponibles.Select(h => h.ToString(@"hh\:mm")).ToList();
        }

    }
}
