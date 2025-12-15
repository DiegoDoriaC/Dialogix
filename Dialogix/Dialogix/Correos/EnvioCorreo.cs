using Dialogix.Domain.Common;
using Essalud.Domain;
using System.Net.Mail;
using System.Net.Mime;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;


namespace Dialogix.Correos
{
    public class EnvioCorreo
    {
        private byte[] GenerarPdfCita(EstadoConversacion estado, CitaMedica cita, string especialidad, string medico)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Document doc = new Document(PageSize.A4, 40, 40, 40, 40);
                PdfWriter writer = PdfWriter.GetInstance(doc, ms);

                doc.Open();

                string rutaLogo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot/Imagenes/Logo_EsSalud.png");

                if (File.Exists(rutaLogo))
                {
                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(rutaLogo);
                    logo.ScaleAbsolute(120, 45);
                    logo.Alignment = Element.ALIGN_CENTER;
                    doc.Add(logo);
                }


                Paragraph title = new Paragraph("Confirmación de Cita Médica\n\n",
                    FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, BaseColor.BLACK));
                title.Alignment = Element.ALIGN_CENTER;
                doc.Add(title);

                Paragraph subtitulo = new Paragraph("EsSalud – Sistema Virtual\n\n",
                    FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.GRAY));
                subtitulo.Alignment = Element.ALIGN_CENTER;
                doc.Add(subtitulo);

                doc.Add(new Paragraph(" ", FontFactory.GetFont(FontFactory.HELVETICA, 4)));
                PdfPTable linea = new PdfPTable(1);
                linea.WidthPercentage = 100;
                PdfPCell lc = new PdfPCell(new Phrase(""))
                {
                    BorderWidthBottom = 1,
                    BorderWidthTop = 0,
                    BorderWidthLeft = 0,
                    BorderWidthRight = 0,
                    BorderColorBottom = new BaseColor(200, 200, 200)
                };
                linea.AddCell(lc);
                doc.Add(linea);
                doc.Add(new Paragraph("\n"));

                Paragraph cuerpo = new Paragraph(
                    $"Estimado(a) {estado.NombrePaciente},\n\n" +
                    "Le enviamos el comprobante de la cita que ha sido registrada exitosamente en nuestro sistema.\n" +
                    "También puede consultar información adicional mediante el chat del sistema.\n\n",
                    FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.BLACK)
                );
                cuerpo.Alignment = Element.ALIGN_LEFT;
                doc.Add(cuerpo);

                PdfPTable tabla = new PdfPTable(4);
                tabla.WidthPercentage = 100;
                tabla.SetWidths(new float[] { 25f, 25f, 25f, 25f });

                BaseColor azul = new BaseColor(42, 55, 106);
                BaseColor grisClaro = new BaseColor(250, 250, 250);

                Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.WHITE);
                Font cellFont = FontFactory.GetFont(FontFactory.HELVETICA, 11, BaseColor.BLACK);

                string[] headers = { "Paciente", "Especialidad", "Médico", "Horario" };
                foreach (string h in headers)
                {
                    PdfPCell header = new PdfPCell(new Phrase(h, headerFont))
                    {
                        BackgroundColor = azul,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        Padding = 8
                    };
                    tabla.AddCell(header);
                }

                tabla.AddCell(new PdfPCell(new Phrase(estado.NombrePaciente, cellFont)) { BackgroundColor = grisClaro, Padding = 8 });
                tabla.AddCell(new PdfPCell(new Phrase(especialidad, cellFont)) { BackgroundColor = grisClaro, Padding = 8 });
                tabla.AddCell(new PdfPCell(new Phrase(medico, cellFont)) { BackgroundColor = grisClaro, Padding = 8 });
                tabla.AddCell(new PdfPCell(new Phrase(cita.FechaCita.ToString("dd/MM/yyyy hh:mm tt"), cellFont)) { BackgroundColor = grisClaro, Padding = 8 });

                doc.Add(tabla);


                Paragraph mensajeFinal = new Paragraph(
                    "\nSi usted no generó esta cita, por favor comuníquese al número: 01-5467432\n\n",
                    FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.BLACK)
                );
                doc.Add(mensajeFinal);

                Paragraph footer = new Paragraph("© 2025 EsSalud – Sistema Virtual de Atención",
                    FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.GRAY));
                footer.Alignment = Element.ALIGN_CENTER;
                doc.Add(footer);

                doc.Close();
                return ms.ToArray();
            }
        }



        public bool EnviarNotificacionRegistroCita(EstadoConversacion estadoConversa, CitaMedica cita, string especialidad, string medico)
        {
            if (string.IsNullOrWhiteSpace(estadoConversa.CorreoPaciente) || !estadoConversa.CorreoPaciente.Contains("@"))
                throw new Exception("El paciente no cuenta con un correo registrado");

            MailMessage m = new MailMessage();
            m.To.Add(estadoConversa.CorreoPaciente);
            m.From = new MailAddress("diegodoriac01@gmail.com");
            m.Subject = "GENERACION DE CITA MEDICA ESSALUD";
            m.Priority = MailPriority.Normal;

            string xEmail = @"
<html>
<head>
<meta charset='UTF-8'>
<title>Confirmación de Cita</title>
</head>

<body style='font-family: Arial, sans-serif; background-color:#f4f6f9; padding:20px;'>

<div style='max-width:800px; margin:auto; background:white; border-radius:12px; padding:25px; box-shadow:0 3px 12px rgba(0,0,0,0.1);'>

    <!-- LOGO -->
    <div style='text-align:center; margin-bottom:20px;'>
        <img src='cid:logoEssalud' alt='EsSalud Logo' style='width:130px; height:auto;' />
    </div>

    <!-- TÍTULO -->
    <h2 style='color:#1f3b7f; text-align:center; margin-bottom:6px; font-size:20px;'>
        Confirmación de Cita Médica
    </h2>

    <p style='text-align:center; color:#666; margin-top:0; font-size:13px;'>
        EsSalud – Sistema Virtual
    </p>

    <hr style='margin:22px 0; border:0; border-top:1px solid #e1e1e1;' />

    <!-- MENSAJE PRINCIPAL -->
    <p style='font-size:15px; color:#333; line-height:1.6;'>
        Estimado(a) <b>" + estadoConversa.NombrePaciente + @"</b>,<br><br>
        Le enviamos el comprobante de la cita que ha sido registrada exitosamente en nuestro sistema.
        También puede consultar información adicional mediante el chat del sistema.
    </p>

    <!-- SUBTÍTULO -->
    <h3 style='color:#1f3b7f; margin-top:25px; font-size:16px;'>
        Detalle de la cita
    </h3>

    <!-- TABLA -->
    <table cellpadding='10' cellspacing='0' width='100%'
           style='border-collapse:collapse; table-layout:fixed; margin-top:10px; border-radius:8px; overflow:hidden;'>
    
        <tr style='background-color:#2A376A; color:white; text-align:left; font-size:14px;'>
            <th style='width:25%;'>Paciente</th>
            <th style='width:25%;'>Especialidad</th>
            <th style='width:25%;'>Médico</th>
            <th style='width:25%;'>Horario</th>
        </tr>

        <tr style='background:#fafafa; font-size:14px; color:#333; border-bottom:1px solid #eee;'>
            <td style='word-break:break-word;'>" + estadoConversa.NombrePaciente + @"</td>
            <td style='word-break:break-word;'>" + especialidad + @"</td>
            <td style='word-break:break-word;'>" + medico + @"</td>
            <td style='word-break:break-word;'>" + cita.FechaCita.ToString("dd/MM/yyyy hh:mm tt") + @"</td>
        </tr>
    </table>

    <!-- MENSAJE FINAL -->
    <p style='margin-top:28px; font-size:14px; color:#444; line-height:1.6;'>
        Si usted no generó esta cita, por favor comuníquese al número:<br>
        <b>01-5467432</b>
    </p>

    <!-- PIE DE PÁGINA -->
    <div style='text-align:center; margin-top:35px;'>
        <p style='color:#aaa; font-size:12px;'>
            © 2025 EsSalud – Sistema Virtual de Atención
        </p>
    </div>

</div>

<!-- ESPACIO FINAL PARA EVITAR RECORTES EN GMAIL -->
<div style='height:40px'></div>

</body>
</html>";


            var htmlView = AlternateView.CreateAlternateViewFromString(xEmail, null, MediaTypeNames.Text.Html);

            string rutaLogo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Imagenes/Logo_EsSalud.png");
            if (!File.Exists(rutaLogo))
                throw new Exception("No se encontró el logo en la ruta: " + rutaLogo);


            LinkedResource logo = new LinkedResource(rutaLogo, MediaTypeNames.Image.Png);
            logo.ContentId = "logoEssalud";
            logo.TransferEncoding = TransferEncoding.Base64;

            htmlView.LinkedResources.Add(logo);

            m.AlternateViews.Add(htmlView);

            byte[] pdfBytes = GenerarPdfCita(estadoConversa, cita, especialidad, medico);
            Attachment adjuntoPdf = new Attachment(new MemoryStream(pdfBytes), "Comprobante_Cita.pdf", MediaTypeNames.Application.Pdf);
            m.Attachments.Add(adjuntoPdf);

            SmtpClient client = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential("diegodoriac01@gmail.com", "qkpp lqug uyqp iqtq"),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Timeout = 600000
            };

            try
            {
                client.Send(m);
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                adjuntoPdf.Dispose();
                m.Dispose();
            }
        }
    }
}