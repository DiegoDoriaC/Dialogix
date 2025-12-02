using Essalud.Application.Feature.Interfaces;
using Essalud.Infraestructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogix.Application.Features.Services
{
    public class HorariosMedicoService : IHorariosMedicoService
    {
        private readonly IHorarioMedicoRepository _horarioMedicoRepository;

        public HorariosMedicoService(IHorarioMedicoRepository horarioMedicoRepository)
        {
            _horarioMedicoRepository = horarioMedicoRepository;
        }

        public List<TimeOnly> GenerarHorariosDisponibles_TimeOnly(TimeOnly horaInicio, TimeOnly horaFin, List<TimeOnly> horariosOcupados, int intervaloMinutos = 30)
        {
            var horarios = new List<TimeOnly>();

            for (var current = horaInicio; current < horaFin; current = current.AddMinutes(intervaloMinutos))
            {
                var existe = horariosOcupados.Any(h => h.Hour == current.Hour && h.Minute == current.Minute);
                if (!existe)
                    horarios.Add(current);
            }
            return horarios;
        }

        public async Task<List<string>> ObtenerHorariosDisponibles(int idMedico, DateTime fecha)
        {
            string diaSemana = fecha.ToString("dddd", new CultureInfo("es-ES"));

            var horario = await _horarioMedicoRepository.ListarHorariosMedico(idMedico, diaSemana);

            if (horario.IdHorarioMedico == 0)
                return new List<string>(); 

            var horaInicio = horario.HoraInicio;
            var horaFin = horario.HoraFin;

            var ocupados = await _horarioMedicoRepository.ListarHorariosOcupados(idMedico, fecha);
            var ocupadosTimeSpan = ocupados.Select(o => o.HoraCita).ToList();
            var disponibles = GenerarHorariosDisponibles_TimeOnly(horaInicio, horaFin, ocupadosTimeSpan);

            return disponibles.Select(h => h.ToString("hh:mm tt")).ToList();
        }

        public async Task<List<DateTime>> ObtenerHorariosDisponiblesFuturos(int idMedico, int semanas = 4)
        {
            var resultados = new List<DateTime>();
            var fechaActual = DateTime.Today;

            for (int i = 0; i < semanas * 7; i++)
            {
                var fecha = fechaActual.AddDays(i);
                string diaSemana = fecha.ToString("dddd", new CultureInfo("es-ES")).ToLower();

                var horario = await _horarioMedicoRepository.ListarHorariosMedico(idMedico, diaSemana);

                if (horario.IdHorarioMedico == 0)
                    continue;

                TimeOnly horaInicio = horario.HoraInicio;
                TimeOnly horaFin = horario.HoraFin;

                var ocupados = await _horarioMedicoRepository.ListarHorariosOcupados(idMedico, fecha);
                var ocupadosTimeOnly = ocupados.Select(o => o.HoraCita).ToList();

                var disponibles = GenerarHorariosDisponibles_TimeOnly(
                    horaInicio, horaFin, ocupadosTimeOnly);

                foreach (var hora in disponibles)
                {
                    var fechaCompleta = fecha.Date
                        .AddHours(hora.Hour)
                        .AddMinutes(hora.Minute);

                    resultados.Add(fechaCompleta);
                }
            }
            return resultados.Take(6).ToList();
        }

    }
}
