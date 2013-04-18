using System;
using System.Xml;

namespace LPsolveSBML
{
    [Serializable]
    public struct LPsolveConstraint
    {
        private readonly string _sbmlId;
        private readonly lpsolve_constr_types _Operator;
        private readonly double _value;

        private static lpsolve_constr_types GetOperator(string operation)
        {
            switch (operation)
            {
                case "less":
                case "lessEqual":
                    return lpsolve_constr_types.LE;
                case "equal":
                    return lpsolve_constr_types.EQ;
                case "greater":
                case "greaterEqual":
                default:
                    return lpsolve_constr_types.GE;
            }
        }

        public LPsolveConstraint(XmlElement item)
        {
            _sbmlId = item.GetAttribute("reaction", FluxBalance.STR_FBA_NAMESPACE);
            _value = Convert.ToDouble(item.GetAttribute("value", FluxBalance.STR_FBA_NAMESPACE));
            _Operator = GetOperator(item.GetAttribute("operation", FluxBalance.STR_FBA_NAMESPACE));
        }

        public LPsolveConstraint(string sbmlId, lpsolve_constr_types sOperator, double value)
        {
            _sbmlId = sbmlId;
            _Operator = sOperator;
            _value = value;
        }

        private string GetOperationString(lpsolve_constr_types type)
        {
            switch (type)
            {
                case lpsolve_constr_types.LE:
                    return "less";
                case lpsolve_constr_types.EQ:
                    return "equal";
                case lpsolve_constr_types.GE:
                default:
                    return "greater";
            }
        }

        public void WriteTo(XmlWriter writer)
        {
            writer.WriteStartElement("fba", "constraint", FluxBalance.STR_FBA_NAMESPACE);
            writer.WriteAttributeString("reaction", FluxBalance.STR_FBA_NAMESPACE, _sbmlId);
            writer.WriteAttributeString("operation", FluxBalance.STR_FBA_NAMESPACE, GetOperationString(_Operator));
            writer.WriteAttributeString("value", FluxBalance.STR_FBA_NAMESPACE, _value.ToString());
            writer.WriteEndElement();
        }

        public string Id
        {
            get { return _sbmlId; }
        }

        public lpsolve_constr_types Operator
        {
            get { return _Operator; }
        }

        public string OperatorString
        {
            get
            {
                switch (_Operator)
                {
                    default:
                    case lpsolve_constr_types.EQ:
                        return "=";
                    case lpsolve_constr_types.GE:
                        return ">=";
                    case lpsolve_constr_types.LE:
                        return "<=";
                }
            }
        }

        public double Value
        {
            get { return _value; }
        }
    }
}
