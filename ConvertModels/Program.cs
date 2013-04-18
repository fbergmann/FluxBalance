using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using SBMLSupport;
using libsbmlcs;
using LPsolveSBML;

namespace ConvertModels
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("need SBML file");
                Environment.Exit(-1);
            }

            string fileName = args[0];
            string sbmlContent = File.ReadAllText(fileName);

            sbmlContent = ConvertSBML(sbmlContent);
            File.WriteAllText(fileName + ".converted.xml", sbmlContent);

        }
        private static string ConvertSBML(string sbmlContent)
        {
            FluxBalance fluxBalance = new FluxBalance();
            fluxBalance.Mode = FBA_Mode.maximize;

            // read SBML 
            NOM.loadSBML(sbmlContent);

            // fix boundary flags
            var model = NOM.SbmlModel;
            for (int i = 0; i < model.getNumSpecies(); i++)
            {
                Species species = model.getSpecies(i);
                if (species.getId().EndsWith("_b"))
                    species.setBoundaryCondition(true);
            }

            // find objective
            for (int i = 0; i < model.getNumReactions(); i++)
            {
                Reaction reaction = model.getReaction(i);
                if (reaction.isSetKineticLaw())
                {
                    KineticLaw law = reaction.getKineticLaw();
                    var parameter = law.getParameter("OBJECTIVE_COEFFICIENT");
                    if (parameter == null || parameter.getValue() != 1.0)
                        continue;
                    fluxBalance.Objectives.Add(new LPsolveObjective(reaction.getId(), 1.0));
                    reaction.unsetKineticLaw();
                }
            }

            // build constraints
            for (int i = 0; i < model.getNumReactions(); i++)
            {
                Reaction reaction = model.getReaction(i);
                if (reaction.isSetKineticLaw())
                {
                    KineticLaw law = reaction.getKineticLaw();
                    var lowerBound = law.getParameter("LOWER_BOUND");
                    var upperBound = law.getParameter("UPPER_BOUND");

                    if (lowerBound == null || upperBound == null) continue;

                    fluxBalance.Constraints.Add(new LPsolveConstraint(reaction.getId(), lpsolve_constr_types.LE, lowerBound.getValue()));
                    fluxBalance.Constraints.Add(new LPsolveConstraint(reaction.getId(), lpsolve_constr_types.GE, upperBound.getValue()));

                    reaction.unsetKineticLaw();
                }
            }


            string newSBML = libsbml.writeSBMLToString(NOM.SbmlDocument);

            fluxBalance.SBML = newSBML;

            return fluxBalance.WriteSBML();

        }
    }
}
