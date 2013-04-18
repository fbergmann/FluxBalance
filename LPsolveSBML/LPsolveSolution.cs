using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LPsolveSBML
{
    [Serializable]
    public class LPsolveSolution
    {
        public double[] Solution { get; set; }

        public List<string> Names { get; set; }

        public double this[string id]
        {
            get
            {
                return Solution[Names.IndexOf(id)];
            }
        }

        public double this[int index]
        {
            get{
                return Solution[index];
            }
        }


        /// <summary>
        /// Initializes a new instance of the LPsolveSolution class.
        /// </summary>
        /// <param name="solution"></param>
        /// <param name="names"></param>
        public LPsolveSolution(double[] solution, List<string> names)
        {
            Solution = solution;
            Names = names;
        }

        public LPsolveSolution()
        {
        }

        public void WriteTo(TextWriter textWriter)
        {
            if (Solution == null || Names == null) return;

            for (var i = 0; i < Solution.Length; i++)
            {
                textWriter.WriteLine(String.Format("{0} = {1}", Names[i], Solution[i]));
            }
        }

        public string ToCSV()
        {
            return ToCSV(",");
        }

        public string ToCSV(string delimiter)
        {
            var builder = new StringBuilder();

            builder.AppendLine(String.Format("Flux{0}Value", delimiter));
            for (var i = 0; i < Solution.Length; i++)
            {
                builder.AppendLine(String.Format("{0}{1}{2}", Names[i], delimiter, Solution[i]));
            }
            return builder.ToString();
        }
    }
}