using System;
using System.Xml;

namespace LPsolveSBML
{
    [Serializable]
    public struct LPsolveObjective
    {
        private readonly string _sbmlId;
        private readonly double _Value;

        public LPsolveObjective(XmlElement item)
        {
            _sbmlId = item.GetAttribute("reaction", FluxBalance.STR_FBA_NAMESPACE);
            _Value = Convert.ToDouble(item.GetAttribute("coefficient", FluxBalance.STR_FBA_NAMESPACE));
        }

        public LPsolveObjective(string sbmlId, double value)
        {
            _sbmlId = sbmlId;
            _Value = value;
        }

        public void WriteTo(XmlWriter writer)
        {
            writer.WriteStartElement("fba", "fluxObjective", FluxBalance.STR_FBA_NAMESPACE);
            writer.WriteAttributeString("reaction", FluxBalance.STR_FBA_NAMESPACE, _sbmlId);
            writer.WriteAttributeString("coefficient", FluxBalance.STR_FBA_NAMESPACE, _Value.ToString());
            writer.WriteEndElement();
        }

        public string Id
        {
            get { return _sbmlId; }
        }

        public double Value
        {
            get { return _Value; }
        }
    }
}