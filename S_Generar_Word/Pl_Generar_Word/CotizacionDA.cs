using EntidadesCRM;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Pl_Generar_Word
{
    public class CotizacionDA
    {

        /// <summary>
        /// Verificar si el NumeroIdentificacion ya Existe
        /// </summary>
        /// <param name="cliente">Mandamos la clase con los datos dentro</param>
        /// <param name="servicio">Conexion del CRM</param>
        /// <returns></returns>
        public string BuscarPlantillaCotizacion(string nombrePlantilla,IOrganizationService servicio)
        {
            try
            {
                string guidPlantilla = "";
                zthFetch busquedaFetch = new zthFetch("documenttemplate", ref servicio);
                busquedaFetch.AgregarCampoRetorno("documenttemplate", "documenttemplateid", (zthFetch.TipoRetorno.Key));//devuelvo el vendedor
                busquedaFetch.AgregarFiltroPlano("documenttemplate", (zthFetch.TipoFiltro.and), "name", (zthFetch.TipoComparacionFiltro.Igual), nombrePlantilla);

                DataTable dataTable2 = busquedaFetch.GeneraTblconFetchResult();
                if (dataTable2.Rows.Count > 0)
                {
                    guidPlantilla = Cls_Generales.validaDBnullValue(RuntimeHelpers.GetObjectValue(dataTable2.Rows[0]["documenttemplateid"]));
                    return guidPlantilla;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public string GenerarWord(string guidQuoteId, string guidPlantilla, IOrganizationService servicio,ref string error) {

            try
            {
                OrganizationRequest req = new OrganizationRequest("SetWordTemplate");
                req["Target"] = new EntityReference(Quote.EntityLogicalName, new Guid(guidQuoteId));
                req["SelectedTemplate"] = new EntityReference(DocumentTemplate.EntityLogicalName, new Guid(guidPlantilla));

                servicio.Execute(req);
                return "1";
            }
            catch (Exception ex)
            {
                error = "error: " + ex.Message.ToString();
                return "0";
            }

        }




    }
}
