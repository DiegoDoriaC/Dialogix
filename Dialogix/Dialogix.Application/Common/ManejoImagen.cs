using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogix.Application.Common
{
    public class ManejoImagen
    {
        public void GuardarImagen()
        {
            string root = Directory.GetCurrentDirectory();
            string xRuta = "Avatar//" + DateTime.Now.ToShortDateString().Replace("/", "_");

            string savePath = root + xRuta;

            //string xNuevaRuta = "";
            //if (flpRuta.FileName != "")
            //{
            //    bool xValorRuta = existeDir(savePath);
            //    if (xValorRuta == false)
            //    {
            //        System.IO.Directory.CreateDirectory(savePath);
            //    }

            //    xNuevaRuta = savePath + "/" + flpRuta.FileName.Replace(" ", "_");
            //    flpRuta.SaveAs(xNuevaRuta);
            //    hdAdjunto.Value = xNuevaRuta;

            //}

            //oto.ID = int.Parse(hdIDCotizacion.Value);
            //oto.Tincidencias = xMotivos.SelectedValue;
            //if (xMotivos.SelectedValue != "000")
            //    obo.actualizar_Informe(oto);

            //hdEmpleado.Value = dcbDirigidoA.SelectedValue.ToString();
            //hdEmpleado2.Value = txtCopiadoA.Text.Trim();
            //hdEvento.Value = "1";
            //pClientScript = "<script language=javascript> ActualizarVentanaPadre();</script>";
            //RegisterStartupScript("Prueba", pClientScript);

        }
    }
}
