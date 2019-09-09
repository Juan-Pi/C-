using EntidadesCRM;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pl_Generar_Word
{
    [Serializable()]
    public class Cls_Logica : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {

            ITracingService tracingService =
            (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            IPluginExecutionContext service = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService _orgService = serviceFactory.CreateOrganizationService(service.UserId);

            // tracingService.Trace("GUID de la cotización" + "cc");
            CotizacionDA cotizacionDA = new CotizacionDA();
            string guidPlantilla = string.Empty;
            string NombrePlantilla = "Plantilla Cotización";

            try
            {
               // tracingService.Trace("GUID de la cotización" + inputParameter.Id.ToString());
                CotizacionBE cotizacionBE = new CotizacionBE();
                
                var QuoteAndId = service.InputParameters["EntidadAndId"];
                string SepararQuoteAndId = QuoteAndId.ToString();
                string[] separadas;
                separadas = SepararQuoteAndId.Split(';');

                cotizacionBE.NombreEntidad = separadas[0].ToString();
                cotizacionBE.CotizacionId = separadas[1].ToString();
                cotizacionBE.CotizacionId = cotizacionBE.CotizacionId.Replace("{", "");
                cotizacionBE.CotizacionId = cotizacionBE.CotizacionId.Replace("}", "");
                tracingService.Trace(" cotizacionBE.NombreEntidad " + cotizacionBE.NombreEntidad.ToString());
                tracingService.Trace(" cotizacionBE.CotizacionId " + cotizacionBE.CotizacionId.ToString().Trim().ToLower());
                if (cotizacionBE.NombreEntidad.Equals("quote"))
                {
                    guidPlantilla = cotizacionDA.BuscarPlantillaCotizacion(NombrePlantilla, _orgService);

                    string respGenerarWord = "";
                    if (guidPlantilla != string.Empty)
                    {
                        tracingService.Trace("guidPlantilla " + guidPlantilla.ToString());
                        string error = "";
                        respGenerarWord = cotizacionDA.GenerarWord(cotizacionBE.CotizacionId.Trim().ToLower(), guidPlantilla, _orgService, ref error);
                        tracingService.Trace("respGenerarWord " + respGenerarWord.ToString());
                        tracingService.Trace("error " + error.ToString());
                        service.OutputParameters["Resultado"] = respGenerarWord;
                        return;



                    }
                    else
                    {
                        service.OutputParameters["Resultado"] = "2";
                        return;
                    }


                }

  

            }
            catch (Exception ex)
            {
                tracingService.Trace("Error Plugin Generar Word: " + ex.Message.ToString());
                service.OutputParameters["Resultado"] = "3";
            }
        }
    }
}
