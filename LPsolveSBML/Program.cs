using System;
using System.Collections.Generic;
using System.IO;

namespace LPsolveSBML
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Need one argument, the SBML file to solve");
                Environment.Exit(-1);
            }

            var sbmlContent = File.ReadAllText(args[0]);
            var fluxBalance = new FluxBalance();

            fluxBalance.LoadSBML(sbmlContent);

            fluxBalance.Constraints = new List<LPsolveConstraint>(new[]
            {
                new LPsolveConstraint("J1",
                                      lpsolve_constr_types.EQ,
                                      10.0),
                new LPsolveConstraint("J5",
                                      lpsolve_constr_types.EQ,
                                      6.0),
                new LPsolveConstraint("J3",
                                      lpsolve_constr_types.GE,
                                      1.0)
            });

            fluxBalance.Objectives = new List<LPsolveObjective>(new[]
            {
                new LPsolveObjective("J9", 0.5),
                new LPsolveObjective("J8", 0.75)
            });

            var result = fluxBalance.Solve();

            result.WriteTo(Console.Out);


            Console.WriteLine("(any key to continue)");
            Console.ReadKey();
        }
    }
}