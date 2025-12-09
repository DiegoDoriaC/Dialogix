-- Consultas en página de Administrador
-- Login
USE Dialogix
SELECT * FROM Usuarios

SELECT * FROM Convesaciones

SELECT * FROM MetricaUso ORDER BY Fecha DESC;

SELECT * FROM Mensajes


-- Total de citas médicas
USE Essalud
SELECT COUNT(*) AS TotalCitas
FROM CitaMedica;

-- Total de citas agendadas
USE Essalud
EXEC pr_total_citas_agendadas;


-- Preguntas Frecuentes
USE Dialogix
SELECT * FROM PreguntasFrecuentes

-- Metricas
USE Dialogix
SELECT * FROM MetricaUso


-- Consultas en página de Chatbot

USE Essalud
--Paciente sin citas 
-- Ejemplo Prueba: 70023419
SELECT 
    P.Id,
    P.DNI,
    P.Nombre,
    P.Sexo,
    P.FechaNacimiento,
    P.Telefono,
    P.Correo
FROM Paciente P
LEFT JOIN CitaMedica C 
    ON C.IdPaciente = P.Id
WHERE C.Id IS NULL;

----Visualizar detalle del paciente despues de Agregar Cita y Cancelar Cita
-- Al cancelar se modifica el 
SELECT 
    C.Id AS IdCita,
    C.FechaCita,
    C.HoraCita,
    C.Motivo,
    C.Estado,

    P.Id AS IdPaciente,
    P.Nombre AS NombrePaciente,
    P.DNI,

    M.Id AS IdMedico,
    M.Nombre AS NombreMedico,
    M.Especialidad
FROM CitaMedica C
INNER JOIN Paciente P ON P.Id = C.IdPaciente
LEFT JOIN Medico M ON M.Id = C.IdMedico
WHERE P.DNI = '73120001';


----Consultar Estado de Resultados
-- Ejemplo Prueba: 72690310

SELECT DISTINCT p.Id, p.Nombre, p.DNI, c.Id AS IdCita, c.FechaCita, c.HoraCita,
       r.IdResultadosClinicos, r.TipoExamen, r.Valor, r.FechaResultado, r.Observaciones
FROM Paciente p
INNER JOIN CitaMedica c ON p.Id = c.IdPaciente
INNER JOIN ResultadosClinicos r ON c.Id = r.IdCitaMedica
WHERE p.DNI = '72690310';

----Consultar Historial de citas (las 3 últimas)
-- Ejemplo Prueba: 72690310
SELECT TOP 3
       c.Id AS IdCita,
       c.FechaCita,
       c.HoraCita,
       c.Motivo,
       c.Estado,
       m.Nombre AS NombreMedico,
       m.Especialidad
FROM Paciente p
INNER JOIN CitaMedica c ON p.Id = c.IdPaciente
INNER JOIN Medico m ON c.IdMedico = m.Id
WHERE p.DNI = '72690310'
ORDER BY c.FechaCita DESC, c.HoraCita DESC;

-- Preguntas de ejemplo para la IA
-- cuantas citas se pueden registrar al dia?
-- Que documentos necesito para atenderme en EsSalud?
-- Puedo afiliar a mis familiares?

















SELECT DISTINCT p.Id, p.Nombre, p.DNI
FROM Paciente p
INNER JOIN CitaMedica c ON p.Id = c.IdPaciente
INNER JOIN ResultadosClinicos r ON c.Id = r.IdCitaMedica;