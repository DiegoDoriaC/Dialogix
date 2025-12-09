using Dialogix.Domain.Common;
using Essalud.Domain;
using Microsoft.Extensions.Hosting.Internal;
using System.Net.Mail;

namespace Dialogix.Correos
{
    public class EnvioCorreo
    {

        public bool EnviarNotificacionRegistroCita(EstadoConversacion estadoConversa, CitaMedica cita, string especialidad, string medico)
        {
            bool respuesta = false;

            MailMessage m = new MailMessage();

            if (string.IsNullOrWhiteSpace(estadoConversa.CorreoPaciente) || !estadoConversa.CorreoPaciente.Contains("@")) 
                throw new Exception("El paciente no cuenta con un correo registrado");

            m.To.Add(estadoConversa.CorreoPaciente);

            string xEmail = "";

            // --------------------------------------------------------------------------------------------------------------------------------------------------------

            xEmail = xEmail + "<html xmlns='http://www.w3.org/1999/xhtml'>";
            xEmail = xEmail + "<head>";
            xEmail = xEmail + "<meta http-equiv='Content-Type' content='text/html; charset=utf-8' />";
            xEmail = xEmail + "<title>Untitled Document</title>";

            xEmail = xEmail + "<link href='../CSS/isa_style.css' rel='stylesheet' type='text/css' />";
            //ESTILOS
            xEmail = xEmail + "<style>";
            xEmail = xEmail + ".estilo-azul {";
            xEmail = xEmail + "background-color:#2A376A;";
            xEmail = xEmail + "color:white;";
            xEmail = xEmail + "padding:5px;";
            xEmail = xEmail + "font-family: Verdana, Arial, Helvetica, sans-serif;";
            xEmail = xEmail + "}";
            xEmail = xEmail + ".estilo-verde {";
            xEmail = xEmail + "background-color:#01A88C;";
            xEmail = xEmail + "color:white;";
            xEmail = xEmail + "padding:5px;";
            xEmail = xEmail + "font-family: Verdana, Arial, Helvetica, sans-serif;";
            xEmail = xEmail + "}";
            xEmail = xEmail + ".estilo-azulClaro {";
            xEmail = xEmail + "background-color:#4D5FA1;";
            xEmail = xEmail + "color:white;";
            xEmail = xEmail + "padding:5px;";
            xEmail = xEmail + "font-family: Verdana, Arial, Helvetica, sans-serif;";
            xEmail = xEmail + "}";
            xEmail = xEmail + "</style>";

            xEmail = xEmail + "</head>";

            xEmail = xEmail + "<body>";
            xEmail = xEmail + "<div class='m_cabecera'>";

            xEmail = xEmail + "<img src=\"cid:Pic1\">";

            xEmail = xEmail + "</div>";
            xEmail = xEmail + "<div style='display: block;width:1600px;padding-top: 20px;padding-right: 50px;padding-bottom: 20px;padding-left: 5px;margin: auto;font-weight: normal;color: #333333;font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;'> ";
            xEmail = xEmail + "<h1 style='display: block;font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;font-weight: bold;color: #333333;'>Estimado(a) : " + estadoConversa.NombrePaciente + " <br />";
            xEmail = xEmail + "</h1>";

            xEmail = xEmail + "<h2 style='display: block;font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px; color: #333333; font-weight: normal;'>Por medio del presente le enviamos el resumen de la generación de su cita:<br />";
            xEmail = xEmail + "</h2>";

            xEmail = xEmail + "<h1 style='display: block;font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;font-weight: bold;color: #333333;'>DETALLE CITA<br />";
            xEmail = xEmail + "</h1>";

            xEmail += "<table border='1' cellpadding='5' cellspacing='0' style='border-collapse:collapse; font-family:Verdana; font-size:12px; width:100%;'>";

            xEmail += "<tr>";
            xEmail += "<th class='estilo-azul'>Paciente</th>";
            xEmail += "<th class='estilo-verde'>Especialidad</th>";
            xEmail += "<th class='estilo-verde'>Medico</th>";
            xEmail += "<th  class='estilo-verde'>Horario</th>";
            xEmail += "</tr>";

            //foreach (var item in oLista)
            //{
            xEmail += "<tr>";
                
            xEmail += "<td>" + estadoConversa.NombrePaciente + "</td>";
            xEmail += "<td>" + especialidad + "</td>";
            xEmail += "<td>" + medico + "</td>";
            xEmail += "<td>" + cita.FechaCita.ToShortDateString() + " " + cita.HoraCita + "</td>";
            xEmail += "</tr>";
            //}

            xEmail += "</table>";

            // FIN TABLA

            xEmail = xEmail + "<br />";
            xEmail = xEmail + "<br />";
            xEmail = xEmail + "<br />";
            xEmail = xEmail + "<br />";

            xEmail = xEmail + "<h2 style='display: block;font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px; color: #333333; font-weight: normal;'>Si usted no generó la cita puede comunicarse al número: 01-5467432 para obtener mas informacion<br />";

            xEmail = xEmail + "<br />";
            xEmail = xEmail + "<br />";

            //xEmail = xEmail + "<img src=\"cid:Pic2\">";

            xEmail = xEmail + "</div>";
            xEmail = xEmail + "</body>";
            xEmail = xEmail + "</html>";

            // --------------------------------------------------------------------------------------------------------------------------------------------------------

            string htmlBody = xEmail.Replace("<1MA>", char.ConvertFromUtf32(39)); ;

            AlternateView avHtml = AlternateView.CreateAlternateViewFromString(htmlBody, null, System.Net.Mime.MediaTypeNames.Text.Html);

            avHtml = AlternateView.CreateAlternateViewFromString(htmlBody, null, System.Net.Mime.MediaTypeNames.Text.Plain);
            m.Body = htmlBody;
            m.IsBodyHtml = true;
            m.Priority = MailPriority.Normal;
            m.From = new MailAddress("diegodoriac01@gmail.com");

            m.Subject = "GENERACION DE CITA MEDICA ESSALUD";

            SmtpClient client = new SmtpClient();
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Timeout = 600000;
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.Credentials = new System.Net.NetworkCredential("diegodoriac01@gmail.com", "qkpp lqug uyqp iqtq");
            client.Send(m);
            respuesta = true;
            return respuesta;

        }

    }
}
