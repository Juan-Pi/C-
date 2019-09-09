using System;
using System.Xml;

namespace Pl_Generar_Word
{
    [Serializable()]
    public class XmlHelper
    {

        public XmlNode addNode(ref XmlDocument document, ref XmlNode parent, ref string name, ref string value)
        {
            XmlNode element = (XmlNode)document.CreateElement(name);
            if (value != null)
                element.InnerText = value;
            parent.AppendChild(element);
            return element;
        }

        public XmlNode addNode(XmlDocument document, XmlNode parent, string name)
        {
            ref XmlDocument local1 = ref document;
            ref XmlNode local2 = ref parent;
            ref string local3 = ref name;
            string str = (string)null;
            ref string local4 = ref str;
            return this.addNode(ref local1, ref local2, ref local3, ref local4);
        }

        public XmlAttribute AddAttribute(XmlDocument document, XmlNode node, string name, string value)
        {
            XmlAttribute attribute = document.CreateAttribute(name);
            attribute.Value = value;
            node.Attributes.Append(attribute);
            return attribute;
        }

    }
}
