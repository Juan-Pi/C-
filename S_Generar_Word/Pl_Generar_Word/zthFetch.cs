using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Pl_Generar_Word
{
    [Serializable()]
    public class zthFetch
    {


            private string _XmlBase;
            private string _NombreEntidadPrincipalBuscar;
            private ArrayList _ListaCamposRetorno;
            private ArrayList _ListaCamposRetornoOpcionales;
            private ArrayList _ListaTiposCamposRetorno;
            private ArrayList _ListaEntidadesAsociadas;
            private ArrayList _ListaFiltrosenResultadoFrom;
            private ArrayList _ListaFiltrosenResultadoTo;
            private ArrayList _ListaFiltrosenResultadoTiposfiltros;
            private XmlHelper XmlHelper;
            private IOrganizationService _ServicioCRM;
            private bool _TieneAgregate;
            private string _SeparadorMilesCaracter;
            private int _ValorDefectoNumeros;
            private bool _ValorDefectoNumerosSeteado;
            private List<zthFetch.structEntidades> _NombreEntidades;

            public zthFetch(string NombreEntidadPrincipalBuscar, ref IOrganizationService ServicioCRM)
            {
                this._XmlBase = "";
                this._NombreEntidadPrincipalBuscar = "";
                this._ListaCamposRetorno = new ArrayList();
                this._ListaCamposRetornoOpcionales = new ArrayList();
                this._ListaTiposCamposRetorno = new ArrayList();
                this._ListaEntidadesAsociadas = new ArrayList();
                this._ListaFiltrosenResultadoFrom = new ArrayList();
                this._ListaFiltrosenResultadoTo = new ArrayList();
                this._ListaFiltrosenResultadoTiposfiltros = new ArrayList();
                this.XmlHelper = new XmlHelper();
                this._TieneAgregate = false;
                this._SeparadorMilesCaracter = "";
                this._ValorDefectoNumeros = 0;
                this._ValorDefectoNumerosSeteado = false;
                this._NombreEntidades = new List<zthFetch.structEntidades>();
                this._ServicioCRM = ServicioCRM;
                this._XmlBase = "";
                this._ListaCamposRetorno = new ArrayList();
                this._ListaCamposRetornoOpcionales = new ArrayList();
                this._ListaTiposCamposRetorno = new ArrayList();
                this._ListaEntidadesAsociadas = new ArrayList();
                this._ListaFiltrosenResultadoFrom = new ArrayList();
                this._ListaFiltrosenResultadoTo = new ArrayList();
                this._ListaFiltrosenResultadoTiposfiltros = new ArrayList();
                this._NombreEntidadPrincipalBuscar = NombreEntidadPrincipalBuscar;
                this._TieneAgregate = false;
                this.XmlHelper = new XmlHelper();
                this.MYCreaXMLbase();
            }

            private string ResultadoFetchXML
            {
                get
                {
                    return this._XmlBase;
                }
            }

            public void AgregarValorDefectoNumeros(int ValorDefectoNumeros)
            {
                this._ValorDefectoNumeros = ValorDefectoNumeros;
                this._ValorDefectoNumerosSeteado = true;
            }

            public void AgregarCampoRetorno(string NombreEntidad, string NombreCampo, zthFetch.TipoRetorno Tipo)
            {
                this.MYAgregarCampoRetorno(NombreEntidad.ToLower(), NombreCampo.ToLower(), Tipo, NombreCampo.ToLower());
            }

            public void AgregarCampoRetorno(string NombreEntidad, string NombreCampo, zthFetch.TipoRetorno Tipo, string NombreOpcionalColumna)
            {
                this.MYAgregarCampoRetorno(NombreEntidad.ToLower(), NombreCampo.ToLower(), Tipo, NombreOpcionalColumna.ToLower());
            }

            public void AgregarCantidadRegistrosDevolver_puedesermenorque5000(int Cantidad)
            {
                this.MYAgregarCantidadRegistrosDevolver(Cantidad);
            }

            public void AgregarEntidadLinkJoin(string NombreEntidadFrom, string NombreEntidadTo, zthFetch.TipoRelacionEntidadLink TipoRelacionEntidad, string NombreCampoFrom, string NombreCampoTo, string NombreAlias = "")
            {
                this.MyAgregarEntidadLinkJoin(NombreEntidadFrom.ToLower(), NombreEntidadTo.ToLower(), TipoRelacionEntidad, NombreCampoFrom.ToLower(), NombreCampoTo.ToLower(), NombreAlias.ToLower());
            }

            public void AgregarFiltrosPlanosAnidados(string NombreEntidad, zthFetch.TipoFiltro TipoFiltroPadre, zthFetch.TipoFiltro[] TiposFiltrosAnidados, string[] NombresAtributos, zthFetch.TipoComparacionFiltro[] TiposComparacion, string[] ValoresAtributos)
            {
                string[] strArray = NombresAtributos;
                int index = 0;
                while (index < strArray.Length)
                {
                    strArray[index].ToLower();
                    checked { ++index; }
                }
                this.MyAgregarFiltrosPlanosAnidados(this._XmlBase, NombreEntidad.ToLower(), TipoFiltroPadre, TiposFiltrosAnidados, NombresAtributos, TiposComparacion, ValoresAtributos);
            }

            public void AgregarFiltroPlano(string NombreEntidad, zthFetch.TipoFiltro TipoFiltro, string NombreAtributo, zthFetch.TipoComparacionFiltro TipoComparacion, string ValorAtributo)
            {
                this.MyAgregarFiltroPlano(this._XmlBase, NombreEntidad.ToLower(), TipoFiltro, NombreAtributo.ToLower(), TipoComparacion, ValorAtributo.ToLower());
            }

            public void AgregarFiltroPlano(string NombreEntidad, zthFetch.TipoFiltro TipoFiltro, string NombreAtributo, zthFetch.TipoComparacionFiltro TipoComparacion)
            {
                this.MyAgregarFiltroPlano(this._XmlBase, NombreEntidad.ToLower(), TipoFiltro, NombreAtributo.ToLower(), TipoComparacion, "");
            }

            public void AgregarOrdenResultadoEntidad(string NombreEntidad, string NombreCampo, zthFetch.TipoOrdenCampoResultadoEntidad TipoOrden)
            {
                this.MyAgregarOrdenResultadoEntidad(NombreEntidad, NombreCampo, TipoOrden);
            }

            public void AgregarDistinctResultado(bool addDistinct)
            {
                this.MyAgregarDistinctResultado(addDistinct);
            }

            public string DevuelveXML()
            {
                return this._XmlBase;
            }

            public DataTable GeneraTblconFetchResult(bool ObtenerResultadoCompletodeCRM = false)
            {
                return this.MyObtieneResultadodeCRM(ObtenerResultadoCompletodeCRM);
            }

            private void MYCreaXMLbase()
            {
                this._XmlBase = "<fetch version=\"1.0\" output-format=\"xml-platform\" mapping=\"logical\">" + "<entity name=\"" + this._NombreEntidadPrincipalBuscar + "\">" + "</entity></fetch>";
            }

            private void MYAgregarCountCampo(string NombreEntidad, string NombreCampo, string NombreOpcionalColumna, bool agregarDistinct)
            {
                XmlDocument documentCargadoXml = new XmlDocument();
                documentCargadoXml.LoadXml(this._XmlBase);
                if (!this._TieneAgregate)
                {
                    XmlNode node = documentCargadoXml.SelectSingleNode("/fetch");
                    if (node != null)
                    {
                        this.XmlHelper.AddAttribute(documentCargadoXml, node, "aggregate", "true");
                        this._XmlBase = documentCargadoXml.InnerXml;
                    }
                    this._TieneAgregate = true;
                }
                XmlNode parent = this.MyDevuelveNodoEntidad(NombreEntidad, ref documentCargadoXml);
                if (parent == null || parent.SelectSingleNode("/attribute[@name='" + NombreCampo + "']") != null)
                    return;
                XmlNode node1 = this.XmlHelper.addNode(documentCargadoXml, parent, "attribute");
                this.XmlHelper.AddAttribute(documentCargadoXml, node1, "name", NombreCampo);
                this.XmlHelper.AddAttribute(documentCargadoXml, node1, "aggregate", "countcolumn");
                this.XmlHelper.AddAttribute(documentCargadoXml, node1, "alias", NombreOpcionalColumna);
                if (agregarDistinct)
                    this.XmlHelper.AddAttribute(documentCargadoXml, node1, "distinct", "true");
                this._XmlBase = documentCargadoXml.InnerXml;
                if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(NombreEntidad, this._NombreEntidadPrincipalBuscar, false) != 0)
                    NombreCampo = parent.Attributes["to"].Value.ToString() + "." + NombreCampo;
                this._ListaCamposRetorno.Add((object)(NombreOpcionalColumna + "/" + NombreEntidad));
                try
                {
                    foreach (object retornoOpcionale in this._ListaCamposRetornoOpcionales)
                    {
                        if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Conversions.ToString(retornoOpcionale), NombreOpcionalColumna, false) == 0)
                            NombreOpcionalColumna = NombreOpcionalColumna + "_" + DateTime.Now.Ticks.ToString();
                    }
                }
                finally
                {
                    IEnumerator enumerator = null;
                    if (enumerator is IDisposable)
                        (enumerator as IDisposable).Dispose();
                }
                this._ListaCamposRetornoOpcionales.Add((object)(NombreOpcionalColumna + "/" + NombreEntidad));
                this._ListaTiposCamposRetorno.Add((object)zthFetch.TipoRetorno.String);
            }

            private void MYAgregarCampoRetorno(string NombreAliasEntidad, string NombreCampo, zthFetch.TipoRetorno Tipo, string NombreOpcionalColumna)
            {
                string str1 = "";
                string str2 = "";
                List<zthFetch.structEntidades>.Enumerator enumerator1 = new List<structEntidades>.Enumerator();
                if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(NombreAliasEntidad, this._NombreEntidadPrincipalBuscar, false) == 0)
                {
                    str1 = this._NombreEntidadPrincipalBuscar;
                    str2 = this._NombreEntidadPrincipalBuscar;
                }
                else
                {
                    try
                    {
                        enumerator1 = this._NombreEntidades.GetEnumerator();
                        while (enumerator1.MoveNext())
                        {
                            zthFetch.structEntidades current = enumerator1.Current;
                            if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(current.NombreAlias, NombreAliasEntidad, false) == 0)
                            {
                                str1 = current.Nombre;
                                str2 = current.NombreAlias;
                            }
                        }
                    }
                    finally
                    {
                        enumerator1.Dispose();
                    }
                }
                XmlDocument documentCargadoXml = new XmlDocument();
                documentCargadoXml.LoadXml(this._XmlBase);
                XmlNode parent = this.MyDevuelveNodoEntidad(str1, ref documentCargadoXml);
                if (parent == null || parent.SelectSingleNode("/attribute[@name='" + NombreCampo + "']") != null)
                    return;
                XmlNode node = this.XmlHelper.addNode(documentCargadoXml, parent, "attribute");
                this.XmlHelper.AddAttribute(documentCargadoXml, node, "name", NombreCampo);
                this._XmlBase = documentCargadoXml.InnerXml;
                if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str1, this._NombreEntidadPrincipalBuscar, false) != 0)
                    NombreCampo = str2 + "." + NombreCampo;
                this._ListaCamposRetorno.Add((object)(NombreCampo + "/" + str1));
                try
                {
                    foreach (object retornoOpcionale in this._ListaCamposRetornoOpcionales)
                    {
                        if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Conversions.ToString(retornoOpcionale), NombreOpcionalColumna, false) == 0)
                            NombreOpcionalColumna = NombreOpcionalColumna + "_" + DateTime.Now.Ticks.ToString();
                    }
                }
                finally
                {
                    IEnumerator enumerator2 = null;
                    if (enumerator2 is IDisposable)
                        (enumerator2 as IDisposable).Dispose();
                }
                this._ListaCamposRetornoOpcionales.Add((object)(NombreOpcionalColumna + "/" + str1));
                this._ListaTiposCamposRetorno.Add((object)Tipo);
                if (Tipo != zthFetch.TipoRetorno.CrmMoney)
                    return;
                this.MYAgregarCampoSinRetorno(str1, "transactioncurrencyid");
            }

            private void MYAgregarCampoSinRetorno(string NombreEntidad, string NombreCampo)
            {
                XmlDocument documentCargadoXml = new XmlDocument();
                documentCargadoXml.LoadXml(this._XmlBase);
                XmlNode parent = this.MyDevuelveNodoEntidad(NombreEntidad, ref documentCargadoXml);
                if (parent == null || parent.SelectSingleNode("/attribute[@name='" + NombreCampo + "']") != null)
                    return;
                XmlNode node = this.XmlHelper.addNode(documentCargadoXml, parent, "attribute");
                this.XmlHelper.AddAttribute(documentCargadoXml, node, "name", NombreCampo);
                this._XmlBase = documentCargadoXml.InnerXml;
            }

            private void MYAgregarCantidadRegistrosDevolver(int Cantidad)
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(this._XmlBase);
                XmlNode node = document.SelectSingleNode("/fetch");
                if (node == null)
                    return;
                this.XmlHelper.AddAttribute(document, node, "count", Cantidad.ToString());
                this._XmlBase = document.InnerXml;
            }

            private void MYAgregarDevuelveTodoslosRegistros(bool addFullResult)
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(this._XmlBase);
                XmlNode node = document.SelectSingleNode("/fetch");
                if (node == null || !addFullResult)
                    return;
                this.XmlHelper.AddAttribute(document, node, "distinct", "true");
                this._XmlBase = document.InnerXml;
            }

            private void MyAgregarDistinctResultado(bool addDistinct)
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(this._XmlBase);
                XmlNode node = document.SelectSingleNode("/fetch");
                if (node == null)
                    return;
                if (addDistinct)
                {
                    this.XmlHelper.AddAttribute(document, node, "distinct", "true");
                    this._XmlBase = document.InnerXml;
                }
                else
                {
                    this.XmlHelper.AddAttribute(document, node, "distinct", "false");
                    this._XmlBase = document.InnerXml;
                }
            }

            private DataTable MyObtieneResultadodeCRM(bool ObtenerResultadoCompletodeCRM)
            {
                DataTable dataTable1;
                if (ObtenerResultadoCompletodeCRM)
                {
                    bool flag = false;
                    int num = 1;
                    string Left = "";
                    XmlDocument xmlDocument1 = new XmlDocument();
                    DataTable dataTable2 = (DataTable)null;
                    while (!flag)
                    {
                        XmlDocument xmlDocument2 = new XmlDocument();
                        xmlDocument2.LoadXml(this.ResultadoFetchXML);
                        XmlNode xmlNode = xmlDocument2.SelectSingleNode("/fetch");
                        if (xmlNode.Attributes["page"] == null)
                        {
                            XmlAttribute attribute = xmlDocument2.CreateAttribute("page");
                            attribute.Value = num.ToString();
                            xmlNode.Attributes.Append(attribute);
                        }
                        else
                            xmlNode.Attributes["page"].Value = num.ToString();
                        if (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(Left, "", false) != 0)
                        {
                            if (xmlNode.Attributes["paging-cookie"] == null)
                            {
                                XmlAttribute attribute = xmlDocument2.CreateAttribute("paging-cookie");
                                attribute.Value = Left;
                                xmlNode.Attributes.Append(attribute);
                            }
                            else
                                xmlNode.Attributes["paging-cookie"].Value = Left;
                        }
                        EntityCollection Resultado = this._ServicioCRM.RetrieveMultiple((QueryBase)new FetchExpression(xmlDocument2.InnerXml.ToString()));
                        if (dataTable2 == null)
                            dataTable2 = this.MyGeneraTbl(Resultado);
                        else
                            dataTable2.Merge(this.MyGeneraTbl(Resultado));
                        Left = Resultado.PagingCookie;
                        if (!Resultado.MoreRecords)
                            flag = true;
                        else
                            checked { ++num; }
                    }
                    dataTable1 = dataTable2;
                }
                else
                    dataTable1 = this.MyGeneraTbl(this._ServicioCRM.RetrieveMultiple((QueryBase)new FetchExpression(this.ResultadoFetchXML)));
                return dataTable1;
            }

            private DataTable MyGeneraTbl(EntityCollection Resultado)
            {
                int count = this._ListaCamposRetorno.Count;
                DataTable dataTable = new DataTable();
                int num1 = checked(count - 1);
                int index1 = 0;
                while (index1 <= num1)
                {
                    DataColumn column = new DataColumn();
                    zthFetch.TipoRetorno integer = (zthFetch.TipoRetorno)Conversions.ToInteger(this._ListaTiposCamposRetorno[index1]);
                    string str = this._ListaCamposRetornoOpcionales[index1].ToString().Split('/')[0];
                    column.ColumnName = str;
                    switch (integer)
                    {
                        case zthFetch.TipoRetorno.CrmBoolean:
                            column.DataType = typeof(bool);
                            break;
                        case zthFetch.TipoRetorno.CrmDateTime:
                            column.DataType = typeof(DateTime);
                            break;
                        case zthFetch.TipoRetorno.CrmDecimal:
                            column.DataType = typeof(Decimal);
                            break;
                        case zthFetch.TipoRetorno.CrmFloat:
                            column.DataType = typeof(double);
                            break;
                        case zthFetch.TipoRetorno.CrmMoney:
                            column.DataType = typeof(Money);
                            break;
                        case zthFetch.TipoRetorno.CrmNumber:
                            column.DataType = typeof(int);
                            break;
                        case zthFetch.TipoRetorno.LookupName:
                            column.DataType = typeof(string);
                            break;
                        case zthFetch.TipoRetorno.LookupValue:
                            column.DataType = typeof(Guid);
                            break;
                        case zthFetch.TipoRetorno.String:
                            column.DataType = typeof(string);
                            break;
                        case zthFetch.TipoRetorno.PicklistName:
                            column.DataType = typeof(string);
                            break;
                        case zthFetch.TipoRetorno.PicklistValue:
                            column.DataType = typeof(int);
                            break;
                        case zthFetch.TipoRetorno.Key:
                            column.DataType = typeof(Guid);
                            break;
                        case zthFetch.TipoRetorno.StatusValue:
                            column.DataType = typeof(int);
                            break;
                        case zthFetch.TipoRetorno.StatusName:
                            column.DataType = typeof(string);
                            break;
                        case zthFetch.TipoRetorno.StateValue:
                            column.DataType = typeof(int);
                            break;
                        case zthFetch.TipoRetorno.StateName:
                            column.DataType = typeof(string);
                            break;
                        default:
                            column.DataType = typeof(string);
                            break;
                    }
                    dataTable.Columns.Add(column);
                    checked { ++index1; }
                }
                IEnumerator<Entity> enumerator = null;
                try
                {
                    enumerator = Resultado.Entities.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        Entity current = enumerator.Current;
                        DataRow row = dataTable.NewRow();
                        int num2 = checked(count - 1);
                        int index2 = 0;
                        while (index2 <= num2)
                        {
                            string str1 = this._ListaCamposRetorno[index2].ToString().Split('/')[1];
                            string nombreAtributo = this._ListaCamposRetorno[index2].ToString().Split('/')[0];
                            string index3 = this._ListaCamposRetornoOpcionales[index2].ToString().Split('/')[0];
                            zthFetch.TipoRetorno integer1 = (zthFetch.TipoRetorno)Conversions.ToInteger(this._ListaTiposCamposRetorno[index2]);
                            if (current.Attributes.Keys.Contains(nombreAtributo) && !Information.IsNothing(RuntimeHelpers.GetObjectValue(current[nombreAtributo])))
                            {
                                RuntimeHelpers.GetObjectValue(new object());
                                object Instance = Microsoft.VisualBasic.CompilerServices.Operators.CompareString(current[nombreAtributo].GetType().Name.ToUpper(), "AliasedValue".ToUpper(), false) != 0 ? RuntimeHelpers.GetObjectValue(current[nombreAtributo]) : RuntimeHelpers.GetObjectValue(((AliasedValue)current[nombreAtributo]).Value);
                                switch (integer1)
                                {
                                    case zthFetch.TipoRetorno.CrmBoolean:
                                        bool boolean = Conversions.ToBoolean(Instance);
                                        row[index3] = (object)boolean;
                                        break;
                                    case zthFetch.TipoRetorno.CrmDateTime:
                                        DateTime date = Conversions.ToDate(Instance);
                                        row[index3] = (object)date;
                                        break;
                                    case zthFetch.TipoRetorno.CrmDecimal:
                                        Decimal num3 = Conversions.ToDecimal(Instance);
                                        row[index3] = (object)num3;
                                        break;
                                    case zthFetch.TipoRetorno.CrmFloat:
                                        double num4 = Conversions.ToDouble(Instance);
                                        row[index3] = (object)num4;
                                        break;
                                    case zthFetch.TipoRetorno.CrmMoney:
                                        Money money = (Money)Instance;
                                        row[index3] = (object)money;
                                        break;
                                    case zthFetch.TipoRetorno.CrmNumber:
                                        int integer2 = Conversions.ToInteger(Instance);
                                        row[index3] = (object)integer2;
                                        break;
                                    case zthFetch.TipoRetorno.LookupName:
                                        EntityReference entityReference1 = (EntityReference)Instance;
                                        row[index3] = RuntimeHelpers.GetObjectValue(zthFetch.isnull((object)entityReference1.Name, (object)null));
                                        break;
                                    case zthFetch.TipoRetorno.LookupValue:
                                        EntityReference entityReference2 = (EntityReference)Instance;
                                        row[index3] = RuntimeHelpers.GetObjectValue(zthFetch.isnull((object)entityReference2.Id, (object)null));
                                        break;
                                    case zthFetch.TipoRetorno.String:
                                        string str2 = Conversions.ToString(Instance);
                                        row[index3] = (object)str2;
                                        break;
                                    case zthFetch.TipoRetorno.PicklistName:
                                        OptionSetValue optionSetValue1 = (OptionSetValue)Instance;
                                        row[index3] = (object)this.GetOptionSetValueLabel(current.LogicalName, nombreAtributo, optionSetValue1.Value);
                                        break;
                                    case zthFetch.TipoRetorno.PicklistValue:
                                        OptionSetValue optionSetValue2 = (OptionSetValue)Instance;
                                        row[index3] = (object)optionSetValue2.Value;
                                        break;
                                    case zthFetch.TipoRetorno.Key:
                                        object obj = Instance;
                                        Guid guid = obj != null ? (Guid)obj : new Guid();
                                        row[index3] = (object)guid;
                                        break;
                                    case zthFetch.TipoRetorno.StatusValue:
                                        int integer3 = Conversions.ToInteger(NewLateBinding.LateGet(Instance, (Type)null, "Value", new object[0], (string[])null, (Type[])null, (bool[])null));
                                        row[index3] = (object)integer3;
                                        break;
                                    case zthFetch.TipoRetorno.StatusName:
                                        int integer4 = Conversions.ToInteger(NewLateBinding.LateGet(Instance, (Type)null, "Value", new object[0], (string[])null, (Type[])null, (bool[])null));
                                        row[index3] = (object)this.StatusValueLabel(current.LogicalName, nombreAtributo, integer4);
                                        break;
                                    case zthFetch.TipoRetorno.StateValue:
                                        int integer5 = Conversions.ToInteger(NewLateBinding.LateGet(Instance, (Type)null, "Value", new object[0], (string[])null, (Type[])null, (bool[])null));
                                        row[index3] = (object)integer5;
                                        break;
                                    case zthFetch.TipoRetorno.StateName:
                                        int integer6 = Conversions.ToInteger(NewLateBinding.LateGet(Instance, (Type)null, "Value", new object[0], (string[])null, (Type[])null, (bool[])null));
                                        row[index3] = (object)this.StateValueLabel(current.LogicalName, nombreAtributo, integer6);
                                        break;
                                }
                            }
                            checked { ++index2; }
                        }
                        dataTable.Rows.Add(row);
                    }
                }
                finally
                {
                    enumerator?.Dispose();
                }
                return dataTable;
            }

            private void MyAgregarEntidadLinkJoin(string NombreEntidadFrom, string NombreEntidadTo, zthFetch.TipoRelacionEntidadLink TipoRelacionEntidad, string NombreCampoFrom, string NombreCampoTo, string NombreEntidadFromALIAS = "")
            {
                string str = Microsoft.VisualBasic.CompilerServices.Operators.CompareString(NombreEntidadFromALIAS, "", false) != 0 ? NombreEntidadFromALIAS : NombreEntidadTo;
                XmlDocument documentCargadoXml = new XmlDocument();
                documentCargadoXml.LoadXml(this._XmlBase);
                XmlNode parent = this.MyDevuelveNodoEntidad(NombreEntidadFrom, ref documentCargadoXml);
                if (parent != null)
                {
                    XmlNode node = this.XmlHelper.addNode(documentCargadoXml, parent, "link-entity");
                    this.XmlHelper.AddAttribute(documentCargadoXml, node, "name", NombreEntidadTo);
                    this.XmlHelper.AddAttribute(documentCargadoXml, node, "from", NombreCampoTo);
                    this.XmlHelper.AddAttribute(documentCargadoXml, node, "to", NombreCampoFrom);
                    this.XmlHelper.AddAttribute(documentCargadoXml, node, "alias", str);
                    switch (TipoRelacionEntidad)
                    {
                        case zthFetch.TipoRelacionEntidadLink.InnerJoin:
                            this._ListaEntidadesAsociadas.Add((object)node);
                            break;
                        case zthFetch.TipoRelacionEntidadLink.OuterJoin:
                            this.XmlHelper.AddAttribute(documentCargadoXml, node, "link-type", "outer");
                            this._ListaEntidadesAsociadas.Add((object)node);
                            break;
                        default:
                            this._ListaEntidadesAsociadas.Add((object)node);
                            break;
                    }
                    this._NombreEntidades.Add(new zthFetch.structEntidades()
                    {
                        Nombre = NombreEntidadTo,
                        NombreAlias = str
                    });
                }
                this._XmlBase = documentCargadoXml.InnerXml;
            }

            private XmlNode MyDevuelveNodoEntidad(string NombreNodoBuscar, ref XmlDocument documentCargadoXml)
            {
                int count = this._ListaEntidadesAsociadas.Count;
                XmlNode xmlNode = documentCargadoXml.SelectSingleNode("/fetch/entity[@name='" + NombreNodoBuscar + "']");
                string str = "/link-entity";
                int num1 = checked(count - 1);
                int num2 = 0;
                while (num2 <= num1)
                {
                    if (xmlNode == null)
                    {
                        xmlNode = documentCargadoXml.SelectSingleNode("/fetch/entity" + str + "[@name='" + NombreNodoBuscar + "']");
                        str += "/link-entity";
                    }
                    else
                        num2 = checked(count + 1);
                    checked { ++num2; }
                }
                return xmlNode == null ? (XmlNode)null : xmlNode;
            }

            private void MyAgregarFiltroPlano(string xmlOrigen, string NombreEntidad, zthFetch.TipoFiltro TipoFiltro, string NombreAtributo, zthFetch.TipoComparacionFiltro TipoComparacion, string ValorAtributo)
            {
                XmlDocument documentCargadoXml = new XmlDocument();
                documentCargadoXml.LoadXml(this._XmlBase);
                XmlNode parent = this.MyDevuelveNodoEntidad(NombreEntidad, ref documentCargadoXml);
                if (parent != null)
                {
                    XmlNode xmlNode = this.XmlHelper.addNode(documentCargadoXml, parent, "filter");
                    this.XmlHelper.AddAttribute(documentCargadoXml, xmlNode, "type", TipoFiltro.ToString());
                    XmlNode node = this.XmlHelper.addNode(documentCargadoXml, xmlNode, "condition");
                    this.XmlHelper.AddAttribute(documentCargadoXml, node, "attribute", NombreAtributo);
                    switch (TipoComparacion)
                    {
                        case zthFetch.TipoComparacionFiltro.Igual:
                            this.XmlHelper.AddAttribute(documentCargadoXml, node, "operator", "eq");
                            this.XmlHelper.AddAttribute(documentCargadoXml, node, "value", ValorAtributo.ToString());
                            break;
                        case zthFetch.TipoComparacionFiltro.Contiene:
                            this.XmlHelper.AddAttribute(documentCargadoXml, node, "operator", "like");
                            this.XmlHelper.AddAttribute(documentCargadoXml, node, "value", "%" + ValorAtributo.ToString() + "%");
                            break;
                        case zthFetch.TipoComparacionFiltro.NOContiene:
                            this.XmlHelper.AddAttribute(documentCargadoXml, node, "operator", "not-like");
                            this.XmlHelper.AddAttribute(documentCargadoXml, node, "value", "%" + ValorAtributo.ToString() + "%");
                            break;
                        case zthFetch.TipoComparacionFiltro.MayorQue:
                            this.XmlHelper.AddAttribute(documentCargadoXml, node, "operator", "gt");
                            this.XmlHelper.AddAttribute(documentCargadoXml, node, "value", ValorAtributo.ToString());
                            break;
                        case zthFetch.TipoComparacionFiltro.MenorQue:
                            this.XmlHelper.AddAttribute(documentCargadoXml, node, "operator", "lt");
                            this.XmlHelper.AddAttribute(documentCargadoXml, node, "value", ValorAtributo.ToString());
                            break;
                        case zthFetch.TipoComparacionFiltro.MayorIgualQue:
                            this.XmlHelper.AddAttribute(documentCargadoXml, node, "operator", "ge");
                            this.XmlHelper.AddAttribute(documentCargadoXml, node, "value", ValorAtributo.ToString());
                            break;
                        case zthFetch.TipoComparacionFiltro.MenorIgualQue:
                            this.XmlHelper.AddAttribute(documentCargadoXml, node, "operator", "le");
                            this.XmlHelper.AddAttribute(documentCargadoXml, node, "value", ValorAtributo.ToString());
                            break;
                        case zthFetch.TipoComparacionFiltro.PoseeDatos:
                            this.XmlHelper.AddAttribute(documentCargadoXml, node, "operator", "not-null");
                            break;
                        case zthFetch.TipoComparacionFiltro.NOPoseeDatos:
                            this.XmlHelper.AddAttribute(documentCargadoXml, node, "operator", "null");
                            break;
                        case zthFetch.TipoComparacionFiltro.NOIgual:
                            this.XmlHelper.AddAttribute(documentCargadoXml, node, "operator", "ne");
                            this.XmlHelper.AddAttribute(documentCargadoXml, node, "value", ValorAtributo.ToString());
                            break;
                        case zthFetch.TipoComparacionFiltro.FechaMayorIgualQue:
                            this.XmlHelper.AddAttribute(documentCargadoXml, node, "operator", "on-or-after");
                            this.XmlHelper.AddAttribute(documentCargadoXml, node, "value", ValorAtributo.ToString());
                            break;
                        case zthFetch.TipoComparacionFiltro.FechaMenorIgualQue:
                            this.XmlHelper.AddAttribute(documentCargadoXml, node, "operator", "on-or-before");
                            this.XmlHelper.AddAttribute(documentCargadoXml, node, "value", ValorAtributo.ToString());
                            break;
                        case zthFetch.TipoComparacionFiltro.FechaIgualQue:
                            this.XmlHelper.AddAttribute(documentCargadoXml, node, "operator", "on");
                            this.XmlHelper.AddAttribute(documentCargadoXml, node, "value", ValorAtributo.ToString());
                            break;
                        default:
                            this.XmlHelper.AddAttribute(documentCargadoXml, node, "operator", "eq");
                            this.XmlHelper.AddAttribute(documentCargadoXml, node, "value", ValorAtributo.ToString());
                            break;
                    }
                }
                this._XmlBase = documentCargadoXml.InnerXml;
            }

            private void MyAgregarFiltrosPlanosAnidados(string xmlOrigen, string NombreEntidad, zthFetch.TipoFiltro TipoFiltroPadre, zthFetch.TipoFiltro[] TiposFiltrosAnidados, string[] NombresAtributos, zthFetch.TipoComparacionFiltro[] TiposComparacion, string[] ValoresAtributos)
            {
                XmlDocument documentCargadoXml = new XmlDocument();
                documentCargadoXml.LoadXml(this._XmlBase);
                XmlNode parent = this.MyDevuelveNodoEntidad(NombreEntidad, ref documentCargadoXml);
                if (parent != null)
                {
                    int length = NombresAtributos.Length;
                    XmlNode xmlNode1 = this.XmlHelper.addNode(documentCargadoXml, parent, "filter");
                    this.XmlHelper.AddAttribute(documentCargadoXml, xmlNode1, "type", TipoFiltroPadre.ToString());
                    XmlNode node1 = this.XmlHelper.addNode(documentCargadoXml, xmlNode1, "filter");
                    this.XmlHelper.AddAttribute(documentCargadoXml, node1, "type", TiposFiltrosAnidados[0].ToString());
                    int num1 = 1;
                    int num2 = checked(length - num1);
                    int index = 0;
                    while (index <= num2)
                    {
                        XmlNode xmlNode2 = node1;
                        if (index > 0 && Microsoft.VisualBasic.CompilerServices.Operators.CompareString(TiposFiltrosAnidados[index].ToString(), TiposFiltrosAnidados[checked(index - 1)].ToString(), false) != 0)
                        {
                            xmlNode2 = this.XmlHelper.addNode(documentCargadoXml, xmlNode1, "filter");
                            this.XmlHelper.AddAttribute(documentCargadoXml, xmlNode2, "type", TiposFiltrosAnidados[index].ToString());
                            node1 = xmlNode2;
                        }
                        XmlNode node2 = this.XmlHelper.addNode(documentCargadoXml, xmlNode2, "condition");
                        this.XmlHelper.AddAttribute(documentCargadoXml, node2, "attribute", NombresAtributos[index].ToString());
                        switch (TiposComparacion[index])
                        {
                            case zthFetch.TipoComparacionFiltro.Igual:
                                this.XmlHelper.AddAttribute(documentCargadoXml, node2, "operator", "eq");
                                this.XmlHelper.AddAttribute(documentCargadoXml, node2, "value", ValoresAtributos[index].ToString());
                                break;
                            case zthFetch.TipoComparacionFiltro.Contiene:
                                this.XmlHelper.AddAttribute(documentCargadoXml, node2, "operator", "like");
                                this.XmlHelper.AddAttribute(documentCargadoXml, node2, "value", "%" + ValoresAtributos[index].ToString() + "%");
                                break;
                            case zthFetch.TipoComparacionFiltro.NOContiene:
                                this.XmlHelper.AddAttribute(documentCargadoXml, node2, "operator", "not-like");
                                this.XmlHelper.AddAttribute(documentCargadoXml, node2, "value", "%" + ValoresAtributos[index].ToString() + "%");
                                break;
                            case zthFetch.TipoComparacionFiltro.MayorQue:
                                this.XmlHelper.AddAttribute(documentCargadoXml, node2, "operator", "gt");
                                this.XmlHelper.AddAttribute(documentCargadoXml, node2, "value", ValoresAtributos[index].ToString());
                                break;
                            case zthFetch.TipoComparacionFiltro.MenorQue:
                                this.XmlHelper.AddAttribute(documentCargadoXml, node2, "operator", "lt");
                                this.XmlHelper.AddAttribute(documentCargadoXml, node2, "value", ValoresAtributos[index].ToString());
                                break;
                            case zthFetch.TipoComparacionFiltro.MayorIgualQue:
                                this.XmlHelper.AddAttribute(documentCargadoXml, node2, "operator", "ge");
                                this.XmlHelper.AddAttribute(documentCargadoXml, node2, "value", ValoresAtributos[index].ToString());
                                break;
                            case zthFetch.TipoComparacionFiltro.MenorIgualQue:
                                this.XmlHelper.AddAttribute(documentCargadoXml, node2, "operator", "le");
                                this.XmlHelper.AddAttribute(documentCargadoXml, node2, "value", ValoresAtributos[index].ToString());
                                break;
                            case zthFetch.TipoComparacionFiltro.PoseeDatos:
                                this.XmlHelper.AddAttribute(documentCargadoXml, node2, "operator", "not-null");
                                break;
                            case zthFetch.TipoComparacionFiltro.NOPoseeDatos:
                                this.XmlHelper.AddAttribute(documentCargadoXml, node2, "operator", "null");
                                break;
                            case zthFetch.TipoComparacionFiltro.NOIgual:
                                this.XmlHelper.AddAttribute(documentCargadoXml, node2, "operator", "ne");
                                this.XmlHelper.AddAttribute(documentCargadoXml, node2, "value", ValoresAtributos[index].ToString());
                                break;
                            case zthFetch.TipoComparacionFiltro.FechaMayorIgualQue:
                                this.XmlHelper.AddAttribute(documentCargadoXml, node2, "operator", "on-or-after");
                                this.XmlHelper.AddAttribute(documentCargadoXml, node2, "value", ValoresAtributos[index].ToString());
                                break;
                            case zthFetch.TipoComparacionFiltro.FechaMenorIgualQue:
                                this.XmlHelper.AddAttribute(documentCargadoXml, node2, "operator", "on-or-before");
                                this.XmlHelper.AddAttribute(documentCargadoXml, node2, "value", ValoresAtributos[index].ToString());
                                break;
                            case zthFetch.TipoComparacionFiltro.FechaIgualQue:
                                this.XmlHelper.AddAttribute(documentCargadoXml, node2, "operator", "on");
                                this.XmlHelper.AddAttribute(documentCargadoXml, node2, "value", ValoresAtributos[index].ToString());
                                break;
                            default:
                                this.XmlHelper.AddAttribute(documentCargadoXml, node2, "operator", "eq");
                                this.XmlHelper.AddAttribute(documentCargadoXml, node2, "value", ValoresAtributos[index].ToString());
                                break;
                        }
                        checked { ++index; }
                    }
                }
                this._XmlBase = documentCargadoXml.InnerXml;
            }

            private void MyAgregarFiltroLookUp(string xmlOrigen, string NombreEntidad, zthFetch.TipoFiltro TipoFiltro, string NombreAtributo, string LookupNameValor, string LookupTypeValor, string LookupGuidValor)
            {
                XmlDocument documentCargadoXml = new XmlDocument();
                documentCargadoXml.LoadXml(this._XmlBase);
                XmlNode parent = this.MyDevuelveNodoEntidad(NombreEntidad, ref documentCargadoXml);
                if (parent == null)
                    return;
                LookupGuidValor = LookupGuidValor.Replace("{", "");
                LookupGuidValor = LookupGuidValor.Replace("}", "");
                XmlNode xmlNode = this.XmlHelper.addNode(documentCargadoXml, parent, "filter");
                this.XmlHelper.AddAttribute(documentCargadoXml, xmlNode, "type", TipoFiltro.ToString());
                XmlNode node = this.XmlHelper.addNode(documentCargadoXml, xmlNode, "condition");
                this.XmlHelper.AddAttribute(documentCargadoXml, node, "attribute", NombreAtributo);
                this.XmlHelper.AddAttribute(documentCargadoXml, node, "operator", "eq");
                this.XmlHelper.AddAttribute(documentCargadoXml, node, "uiname", LookupNameValor);
                this.XmlHelper.AddAttribute(documentCargadoXml, node, "uitype", LookupTypeValor);
                this.XmlHelper.AddAttribute(documentCargadoXml, node, "value", "{" + LookupGuidValor.ToUpper() + "}");
                this._XmlBase = documentCargadoXml.InnerXml;
            }

            private void MyAgregarFiltroLookUpAnidado(string xmlOrigen, string NombreEntidad, zthFetch.TipoFiltro TipoFiltro, string NombreAtributo, string LookupTypeValor, string[] LookupNameValores, string[] LookupGuidValores)
            {
                XmlDocument documentCargadoXml = new XmlDocument();
                documentCargadoXml.LoadXml(this._XmlBase);
                XmlNode parent = this.MyDevuelveNodoEntidad(NombreEntidad, ref documentCargadoXml);
                if (parent == null)
                    return;
                XmlNode xmlNode = this.XmlHelper.addNode(documentCargadoXml, parent, "filter");
                this.XmlHelper.AddAttribute(documentCargadoXml, xmlNode, "type", TipoFiltro.ToString());
                int num = checked(LookupGuidValores.Length - 1);
                int index = 0;
                while (index <= num)
                {
                    LookupGuidValores[index] = LookupGuidValores[index].Replace("{", "");
                    LookupGuidValores[index] = LookupGuidValores[index].Replace("}", "");
                    XmlNode node = this.XmlHelper.addNode(documentCargadoXml, xmlNode, "condition");
                    this.XmlHelper.AddAttribute(documentCargadoXml, node, "attribute", NombreAtributo);
                    this.XmlHelper.AddAttribute(documentCargadoXml, node, "operator", "eq");
                    this.XmlHelper.AddAttribute(documentCargadoXml, node, "uiname", LookupNameValores[index].ToString());
                    this.XmlHelper.AddAttribute(documentCargadoXml, node, "uitype", LookupTypeValor);
                    this.XmlHelper.AddAttribute(documentCargadoXml, node, "value", "{" + LookupGuidValores[index].ToString().ToUpper() + "}");
                    checked { ++index; }
                }
                this._XmlBase = documentCargadoXml.InnerXml;
            }

            private void MyAgregarOrdenResultadoEntidad(string NombreEntidad, string NombreCampo, zthFetch.TipoOrdenCampoResultadoEntidad TipoOrden)
            {
                XmlDocument documentCargadoXml = new XmlDocument();
                documentCargadoXml.LoadXml(this._XmlBase);
                XmlNode parent = this.MyDevuelveNodoEntidad(NombreEntidad, ref documentCargadoXml);
                if (parent == null)
                    return;
                XmlNode node = this.XmlHelper.addNode(documentCargadoXml, parent, "order");
                this.XmlHelper.AddAttribute(documentCargadoXml, node, "attribute", NombreCampo);
                if (TipoOrden == zthFetch.TipoOrdenCampoResultadoEntidad.Descendiente)
                    this.XmlHelper.AddAttribute(documentCargadoXml, node, "descending", "true");
                this._XmlBase = documentCargadoXml.InnerXml;
            }

            private string devuelvecrmdatetime(DateTime fecha, zthFetch.TipoFormatoFechaCRM formato, bool esFechahasta = false)
            {
                string str1;
                if (esFechahasta && formato >= zthFetch.TipoFormatoFechaCRM.F1 && formato <= zthFetch.TipoFormatoFechaCRM.F5)
                {
                    switch (formato)
                    {
                        case zthFetch.TipoFormatoFechaCRM.F1:
                            string format1 = "{0}-{1}-{2}T23:59";
                            string str2 = fecha.Day.ToString();
                            int num1 = fecha.Month;
                            string str3 = num1.ToString();
                            num1 = fecha.Year;
                            string str4 = num1.ToString();
                            str1 = string.Format(format1, (object)str2, (object)str3, (object)str4);
                            break;
                        case zthFetch.TipoFormatoFechaCRM.F2:
                            string format2 = "{0}-{1}-{2}T23:59";
                            string str5 = fecha.Year.ToString();
                            int num2 = fecha.Month;
                            string str6 = num2.ToString();
                            num2 = fecha.Day;
                            string str7 = num2.ToString();
                            str1 = string.Format(format2, (object)str5, (object)str6, (object)str7);
                            break;
                        case zthFetch.TipoFormatoFechaCRM.F3:
                            string format3 = "{0}/{1}/{2}T23:59";
                            string str8 = fecha.Day.ToString();
                            int num3 = fecha.Month;
                            string str9 = num3.ToString();
                            num3 = fecha.Year;
                            string str10 = num3.ToString();
                            str1 = string.Format(format3, (object)str8, (object)str9, (object)str10);
                            break;
                        case zthFetch.TipoFormatoFechaCRM.F4:
                            string format4 = "{0}/{1}/{2}T23:59";
                            string str11 = fecha.Year.ToString();
                            int num4 = fecha.Month;
                            string str12 = num4.ToString();
                            num4 = fecha.Day;
                            string str13 = num4.ToString();
                            str1 = string.Format(format4, (object)str11, (object)str12, (object)str13);
                            break;
                        default:
                            string format5 = "{0}-{1}-{2}T23:59";
                            string str14 = fecha.Year.ToString();
                            int num5 = fecha.Month;
                            string str15 = num5.ToString();
                            num5 = fecha.Day;
                            string str16 = num5.ToString();
                            str1 = string.Format(format5, (object)str14, (object)str15, (object)str16);
                            break;
                    }
                }
                else
                {
                    switch (formato)
                    {
                        case zthFetch.TipoFormatoFechaCRM.F1:
                            string format6 = "{0}-{1}-{2}T00:00";
                            string str17 = fecha.Day.ToString();
                            int num6 = fecha.Month;
                            string str18 = num6.ToString();
                            num6 = fecha.Year;
                            string str19 = num6.ToString();
                            str1 = string.Format(format6, (object)str17, (object)str18, (object)str19);
                            break;
                        case zthFetch.TipoFormatoFechaCRM.F2:
                            string format7 = "{0}-{1}-{2}T00:00";
                            string str20 = fecha.Year.ToString();
                            int num7 = fecha.Month;
                            string str21 = num7.ToString();
                            num7 = fecha.Day;
                            string str22 = num7.ToString();
                            str1 = string.Format(format7, (object)str20, (object)str21, (object)str22);
                            break;
                        case zthFetch.TipoFormatoFechaCRM.F3:
                            string format8 = "{0}/{1}/{2}T00:00";
                            string str23 = fecha.Day.ToString();
                            int num8 = fecha.Month;
                            string str24 = num8.ToString();
                            num8 = fecha.Year;
                            string str25 = num8.ToString();
                            str1 = string.Format(format8, (object)str23, (object)str24, (object)str25);
                            break;
                        case zthFetch.TipoFormatoFechaCRM.F4:
                            string format9 = "{0}/{1}/{2}T00:00";
                            string str26 = fecha.Year.ToString();
                            int num9 = fecha.Month;
                            string str27 = num9.ToString();
                            num9 = fecha.Day;
                            string str28 = num9.ToString();
                            str1 = string.Format(format9, (object)str26, (object)str27, (object)str28);
                            break;
                        case zthFetch.TipoFormatoFechaCRM.F5:
                            string format10 = "{0}-{1}-{2}T{3}:{4}";
                            object[] objArray1 = new object[5]
                            {
              (object) fecha.Day.ToString(),
              null,
              null,
              null,
              null
                            };
                            int index1 = 1;
                            int num10 = fecha.Month;
                            string str29 = num10.ToString();
                            objArray1[index1] = (object)str29;
                            int index2 = 2;
                            num10 = fecha.Year;
                            string str30 = num10.ToString();
                            objArray1[index2] = (object)str30;
                            int index3 = 3;
                            num10 = fecha.Hour;
                            string str31 = num10.ToString();
                            objArray1[index3] = (object)str31;
                            int index4 = 4;
                            num10 = fecha.Minute;
                            string str32 = num10.ToString();
                            objArray1[index4] = (object)str32;
                            str1 = string.Format(format10, objArray1);
                            break;
                        case zthFetch.TipoFormatoFechaCRM.F6:
                            string format11 = "{0}-{1}-{2}T{3}:{4}";
                            object[] objArray2 = new object[5]
                            {
              (object) fecha.Year.ToString(),
              null,
              null,
              null,
              null
                            };
                            int index5 = 1;
                            int num11 = fecha.Month;
                            string str33 = num11.ToString();
                            objArray2[index5] = (object)str33;
                            int index6 = 2;
                            num11 = fecha.Day;
                            string str34 = num11.ToString();
                            objArray2[index6] = (object)str34;
                            int index7 = 3;
                            num11 = fecha.Hour;
                            string str35 = num11.ToString();
                            objArray2[index7] = (object)str35;
                            int index8 = 4;
                            num11 = fecha.Minute;
                            string str36 = num11.ToString();
                            objArray2[index8] = (object)str36;
                            str1 = string.Format(format11, objArray2);
                            break;
                        case zthFetch.TipoFormatoFechaCRM.F7:
                            string format12 = "{0}/{1}/{2}T{3}:{4}";
                            object[] objArray3 = new object[5]
                            {
              (object) fecha.Day.ToString(),
              null,
              null,
              null,
              null
                            };
                            int index9 = 1;
                            int num12 = fecha.Month;
                            string str37 = num12.ToString();
                            objArray3[index9] = (object)str37;
                            int index10 = 2;
                            num12 = fecha.Year;
                            string str38 = num12.ToString();
                            objArray3[index10] = (object)str38;
                            int index11 = 3;
                            num12 = fecha.Hour;
                            string str39 = num12.ToString();
                            objArray3[index11] = (object)str39;
                            int index12 = 4;
                            num12 = fecha.Minute;
                            string str40 = num12.ToString();
                            objArray3[index12] = (object)str40;
                            str1 = string.Format(format12, objArray3);
                            break;
                        case zthFetch.TipoFormatoFechaCRM.F8:
                            string format13 = "{0}/{1}/{2}T{3}:{4}";
                            object[] objArray4 = new object[5]
                            {
              (object) fecha.Year.ToString(),
              null,
              null,
              null,
              null
                            };
                            int index13 = 1;
                            int num13 = fecha.Month;
                            string str41 = num13.ToString();
                            objArray4[index13] = (object)str41;
                            int index14 = 2;
                            num13 = fecha.Day;
                            string str42 = num13.ToString();
                            objArray4[index14] = (object)str42;
                            int index15 = 3;
                            num13 = fecha.Hour;
                            string str43 = num13.ToString();
                            objArray4[index15] = (object)str43;
                            int index16 = 4;
                            num13 = fecha.Minute;
                            string str44 = num13.ToString();
                            objArray4[index16] = (object)str44;
                            str1 = string.Format(format13, objArray4);
                            break;
                        default:
                            string format14 = "{0}-{1}-{2}T00:00";
                            string str45 = fecha.Year.ToString();
                            int num14 = fecha.Month;
                            string str46 = num14.ToString();
                            num14 = fecha.Day;
                            string str47 = num14.ToString();
                            str1 = string.Format(format14, (object)str45, (object)str46, (object)str47);
                            break;
                    }
                }
                return str1;
            }

            public static object isnull(object item, object valorRetorno)
            {
                return !Information.IsDBNull(RuntimeHelpers.GetObjectValue(item)) ? (!Information.IsNothing(RuntimeHelpers.GetObjectValue(item)) ? item : valorRetorno) : valorRetorno;
            }

            private string GetOptionSetValueLabel(string nombreEntidad, string nombreAtributo, int valorPicklist)
            {
                OptionMetadata[] array = ((EnumAttributeMetadata)((RetrieveAttributeResponse)this._ServicioCRM.Execute((OrganizationRequest)new RetrieveAttributeRequest()
                {
                    EntityLogicalName = nombreEntidad,
                    LogicalName = nombreAtributo,
                    RetrieveAsIfPublished = true
                })).AttributeMetadata).OptionSet.Options.ToArray();
                string str = "";
                OptionMetadata[] optionMetadataArray = array;
                int index = 0;
                while (index < optionMetadataArray.Length)
                {
                    OptionMetadata optionMetadata = optionMetadataArray[index];
                    int? nullable1 = optionMetadata.Value;
                    bool? nullable2;
                    bool? nullable3;
                    if (!nullable1.HasValue)
                    {
                        nullable2 = new bool?();
                        nullable3 = nullable2;
                    }
                    else
                        nullable3 = new bool?(nullable1.GetValueOrDefault() == valorPicklist);
                    nullable2 = nullable3;
                    if (nullable2.GetValueOrDefault())
                        str = optionMetadata.Label.UserLocalizedLabel.Label;
                    checked { ++index; }
                }
                return str;
            }

            private string StatusValueLabel(string nombreEntidad, string nombreAtributo, int valorStatus)
            {
                StatusAttributeMetadata attributeMetadata = (StatusAttributeMetadata)((RetrieveAttributeResponse)this._ServicioCRM.Execute((OrganizationRequest)new RetrieveAttributeRequest()
                {
                    EntityLogicalName = nombreEntidad,
                    LogicalName = nombreAtributo,
                    RetrieveAsIfPublished = true
                })).AttributeMetadata;
                string str = "";
                IEnumerator<OptionMetadata> enumerator = null;
                try
                {
                    enumerator = attributeMetadata.OptionSet.Options.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        StatusOptionMetadata current = (StatusOptionMetadata)enumerator.Current;
                        int? nullable1 = current.Value;
                        bool? nullable2;
                        bool? nullable3;
                        if (!nullable1.HasValue)
                        {
                            nullable2 = new bool?();
                            nullable3 = nullable2;
                        }
                        else
                            nullable3 = new bool?(nullable1.GetValueOrDefault() == valorStatus);
                        nullable2 = nullable3;
                        if (nullable2.GetValueOrDefault())
                            str = current.Label.UserLocalizedLabel.Label;
                    }
                }
                finally
                {
                    enumerator?.Dispose();
                }
                return str;
            }

            private string StateValueLabel(string nombreEntidad, string nombreAtributo, int valorState)
            {
                StateAttributeMetadata attributeMetadata = (StateAttributeMetadata)((RetrieveAttributeResponse)this._ServicioCRM.Execute((OrganizationRequest)new RetrieveAttributeRequest()
                {
                    EntityLogicalName = nombreEntidad,
                    LogicalName = nombreAtributo,
                    RetrieveAsIfPublished = true
                })).AttributeMetadata;
                string str = "";
                IEnumerator<OptionMetadata> enumerator = null;
                try
                {

                    enumerator = attributeMetadata.OptionSet.Options.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        StateOptionMetadata current = (StateOptionMetadata)enumerator.Current;
                        int? nullable1 = current.Value;
                        bool? nullable2;
                        bool? nullable3;
                        if (!nullable1.HasValue)
                        {
                            nullable2 = new bool?();
                            nullable3 = nullable2;
                        }
                        else
                            nullable3 = new bool?(nullable1.GetValueOrDefault() == valorState);
                        nullable2 = nullable3;
                        if (nullable2.GetValueOrDefault())
                            str = current.Label.UserLocalizedLabel.Label;
                    }
                }
                finally
                {
                    enumerator?.Dispose();
                }
                return str;
            }

            public enum TipoRetorno
            {
                CrmBoolean = 101, // 0x00000065
                CrmDateTime = 202, // 0x000000CA
                CrmDecimal = 301, // 0x0000012D
                CrmFloat = 401, // 0x00000191
                CrmMoney = 501, // 0x000001F5
                CrmNumber = 601, // 0x00000259
                LookupName = 701, // 0x000002BD
                LookupValue = 702, // 0x000002BE
                String = 801, // 0x00000321
                PicklistName = 901, // 0x00000385
                PicklistValue = 903, // 0x00000387
                Key = 1000, // 0x000003E8
                StatusValue = 1101, // 0x0000044D
                StatusName = 1102, // 0x0000044E
                StateValue = 1201, // 0x000004B1
                StateName = 1202, // 0x000004B2
            }

            public enum TipoComparacionFiltro
            {
                Igual = 1,
                Contiene = 2,
                NOContiene = 3,
                MayorQue = 4,
                MenorQue = 5,
                MayorIgualQue = 6,
                MenorIgualQue = 7,
                PoseeDatos = 8,
                NOPoseeDatos = 9,
                NOIgual = 10, // 0x0000000A
                FechaMayorIgualQue = 13, // 0x0000000D
                FechaMenorIgualQue = 14, // 0x0000000E
                FechaIgualQue = 15, // 0x0000000F
            }

            public enum TipoComparacionFiltroFecha
            {
                FechaIgual = 1,
                FechaNOIgual = 2,
                FechaMayorQue = 3,
                FechaMenorQue = 4,
                FechaMayorIgualQue = 5,
                FechaMenorIgualQue = 6,
            }

            public enum TipoComparacionFiltroResultado
            {
                Igual = 1,
                Distinto = 2,
            }

            public enum TipoFiltro
            {
                and = 1,
                or = 2,
            }

            public enum TipoRelacionEntidadLink
            {
                InnerJoin = 1,
                OuterJoin = 2,
            }

            public enum TipoOrdenCampoResultadoEntidad
            {
                Descendiente = 1,
                Ascendente = 2,
            }

            public struct structEntidades
            {
                public string Nombre;
                public string NombreAlias;
            }

            public enum TipoFormatoFechaCRM
            {
                F1 = 1,
                F2 = 2,
                F3 = 3,
                F4 = 4,
                F5 = 5,
                F6 = 6,
                F7 = 7,
                F8 = 8,
            }
        

}
}
