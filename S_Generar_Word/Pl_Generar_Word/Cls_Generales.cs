using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Pl_Generar_Word
{
    [Serializable()]
    public class Cls_Generales
    {

        public static void validaInt(ref object objDestino, object valor)
        {
            if (!Versioned.IsNumeric(RuntimeHelpers.GetObjectValue(valor)))
                return;
            objDestino = (object)Conversions.ToInteger(valor);
        }

        public static string validaDBnullValue(object valor)
        {
            if (Cls_Generales.isnullItemdtt(RuntimeHelpers.GetObjectValue(valor)) || valor == DBNull.Value)
                return "";
            return valor.ToString();
        }

        public static bool isnullItemdtt(object Itemdtt)
        {
            return Information.IsDBNull(RuntimeHelpers.GetObjectValue(Itemdtt)) || Itemdtt == null;
        }

        public static object isnull(object item, object valorRetorno)
        {
            if (Information.IsDBNull(RuntimeHelpers.GetObjectValue(item)) || item == null)
                return valorRetorno;
            return item;
        }

        public static string rellenaCerosIzquierda(string Valor, int largo)
        {
            while (Strings.Len(Valor) < largo && (uint)Strings.Len(Conversions.ToDouble(Valor) < (double)largo) > 0U)
                Valor = "0" + Valor;
            return Valor;
        }

        public static string GuidSinLlaves(string guidString)
        {
            return guidString.Replace("{", "").Replace("}", "");
        }

    }
}
