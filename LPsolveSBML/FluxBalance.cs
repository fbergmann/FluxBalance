using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using libsbmlcs;
using libstructural;

namespace LPsolveSBML
{
  [Serializable]
  public class FluxBalance
  {

    public static FluxBalance ForFile(string fileName)
    {
      return new FluxBalance(File.ReadAllText(fileName));
    }

    public const string STR_FBA_NAMESPACE = "http://www.sbml.org/sbml/level3/version1/fba/version1";

    public bool PreferDual { get; set; }
    public bool PrintDebug { get; set; }

    public static double Infinity = 1e30;

    public FBA_Mode Mode { get; set; }

    public Dictionary<string, bool> ReversibilityMap { get; set; }

    /// <summary>
    /// Initializes a new instance of the FluxBalance class.
    /// </summary>
    public FluxBalance()
    {
      ReversibilityMap = new Dictionary<string, bool>();
      Constraints = new List<LPsolveConstraint>();
      Objectives = new List<LPsolveObjective>();
      Mode = FBA_Mode.maximize;
      PreferDual = true;
      PrintDebug = false;
    }

    /// <summary>
    /// Initializes a new instance of the FluxBalance class.
    /// </summary>
    /// <param name="sbmlContent"></param>
    public FluxBalance(string sbmlContent)
      : this()
    {
      SBML = sbmlContent;

      if (!string.IsNullOrEmpty(sbmlContent))
      {
        LoadSBML(sbmlContent);

        InitializeFBAInformation(sbmlContent);

      }

    }

    private static lpsolve_constr_types GetOperator(string fbcOperation)
    {
      switch (fbcOperation)
      {
        case "less":
        case "lessEqual":
          return lpsolve_constr_types.LE;
        case "equal":
        default:
          return lpsolve_constr_types.EQ;
        case "greaterEqual":
        case "greater":
          return lpsolve_constr_types.GE;
      }
    }

    public string ActiveObjective { get; set; }

    private void InitializeFromSBMLDocument(SBMLDocument doc)
    {
      var model = doc.getModel();
      var plugin = (FbcModelPlugin) model.getPlugin("fbc");
      if (plugin == null) return;

      var numConstraints = plugin.getNumFluxBounds();
      for (int i = 0; i < numConstraints; i++)
      {
        var constraint = plugin.getFluxBound(i);
        Constraints.Add(new LPsolveConstraint(constraint.getReaction(), GetOperator(constraint.getOperation()),
          constraint.getValue()));
      }
      var activeObjective = plugin.getActiveObjective();
      if (activeObjective == null && plugin.getNumObjectives() > 0)
        activeObjective = plugin.getObjective(0);
      if (activeObjective == null) return;
      var numObjectives = (int) activeObjective.getNumFluxObjectives();
      for (int i = 0; i < numObjectives; i++)
      {
        var objective = activeObjective.getFluxObjective(i);
        Objectives.Add(new LPsolveObjective(objective.getReaction(), objective.getCoefficient()));
      }

      if (activeObjective.getType() == "minimize" || activeObjective.getType() == "minimise") Mode = FBA_Mode.minimize;
      else Mode = FBA_Mode.maximize;
      ActiveObjective = activeObjective.getId();

      for (int i = 0; i < model.getNumReactions(); i++)
      {
        var reaction = model.getReaction(i);
        ReversibilityMap[reaction.getId()] = reaction.getReversible();
      }
    }

    private void InitializeFromFbcPackage(string sbmlContent)
    {
      var doc = libsbml.readSBMLFromString(sbmlContent);
      InitializeFromSBMLDocument(doc);

    }

    private void InitializeFromCobraAnnotation(string sbmlContent)
    {
      try
      {
        var doc = libsbml.readSBMLFromString(sbmlContent);
        var props = new ConversionProperties();
        props.addOption("convert cobra", true, "");
        if (doc.convert(props) != libsbml.LIBSBML_OPERATION_SUCCESS)
          return;
        InitializeFromSBMLDocument(doc);
      }
      catch
      {

      }
    }

    private void InitializeFBAInformation(string sbmlContent)
    {

      try
      {
        if (sbmlContent.Contains("fbc:required"))
        {
          InitializeFromFbcPackage(sbmlContent);
        }
        else
          InitializeFromFBAAnnotation(sbmlContent);

        if (Constraints.Count == 0)
        {
          InitializeFromCobraAnnotation(sbmlContent);
        }
      }
      catch
      {

      }

    }

    private void InitializeFromFBAAnnotation(string sbmlContent)
    {
      ActiveObjective = "OBJF";
      var doc = new XmlDocument();
      doc.LoadXml(sbmlContent);

      if (doc.DocumentElement == null) return;
      var list = doc.DocumentElement.GetElementsByTagName("fluxBalance", STR_FBA_NAMESPACE);
      if (list.Count == 0) return;

      var fbaRoot = (XmlElement) list[0];
      var constraintList = fbaRoot.GetElementsByTagName("constraint", STR_FBA_NAMESPACE);
      if (constraintList.Count > 0)
      {
        foreach (var item in constraintList)
        {
          Constraints.Add(new LPsolveConstraint((XmlElement) item));
        }
      }

      var objectiveList = fbaRoot.GetElementsByTagName("objective", STR_FBA_NAMESPACE);
      if (objectiveList.Count > 0)
      {
        var fbaObjective = (XmlElement) objectiveList[0];
        var fluxObjective = fbaObjective.GetElementsByTagName("fluxObjective", STR_FBA_NAMESPACE);
        if (fluxObjective.Count > 0)
        {
          foreach (XmlNode item in fluxObjective)
          {
            Objectives.Add(new LPsolveObjective((XmlElement) item));
          }
        }

        var type = fbaObjective.GetAttribute("type", STR_FBA_NAMESPACE);
        Mode = type == "minimize" ? FBA_Mode.minimize : FBA_Mode.maximize;
      }


      list = doc.DocumentElement.GetElementsByTagName("reaction", doc.DocumentElement.NamespaceURI);
      if (objectiveList.Count > 0)
      {
        foreach (XmlNode xmlNode in list)
        {
          var element = (XmlElement) xmlNode;
          ReversibilityMap[element.GetAttribute("id")] = Convert.ToBoolean(element.GetAttribute("reversible"));
        }
      }
    }


    public List<LPsolveConstraint> Constraints { get; set; }

    public List<LPsolveObjective> Objectives { get; set; }


    public List<string> ReactionNames { get; set; }

    public List<string> SpeciesNames { get; set; }

    private double[][] _stoichiometry;

    public string SBML { get; set; }

    private string _outputFileName = Path.Combine(Environment.GetEnvironmentVariable("TEMP"), "lpsolve.txt");

    public string OutputFileName
    {
      get { return _outputFileName; }
      set { _outputFileName = value; }
    }

    public string getSBML()
    {
      return SBML;
    }

    public void LoadSBML(string sbmlContent)
    {
      // load into struct analysis tool
      LoadSBMLContentIntoStructAnalysis(sbmlContent);

      // construct matrices for lp solve
      string[] speciesNames;
      string[] reactionNames;
      _stoichiometry = StructAnalysis.GetStoichiometryMatrix(out speciesNames, out reactionNames);
      ReactionNames = new List<string>(reactionNames);
      SpeciesNames = new List<string>(speciesNames);
      IsEmpty = (_stoichiometry.Length == 0 || reactionNames.Length == 0 || speciesNames.Length == 0);
    }

    public bool IsEmpty { get; set; }

    public void AddContraint(LPsolveConstraint constraint)
    {
      Constraints.Add(constraint);
    }

    public void AddObjective(LPsolveObjective objective)
    {
      Objectives.Add(objective);
    }


    public void LoadConstraintsFromFame(string fileName)
    {
      Constraints.Clear();

      var lines = File.ReadAllLines(fileName);
      foreach (string line in lines)
      {
        var words = line.Split(new[] {'\t', ' '}, StringSplitOptions.RemoveEmptyEntries);
        if (words.Length != 3) continue;

        double val;
        if (!double.TryParse(words[2], out val)) continue;

        if (words[1] == "lower")
        {
          Constraints.Add(new LPsolveConstraint(words[0], lpsolve_constr_types.GE, val));
        }
        else
        {
          Constraints.Add(new LPsolveConstraint(words[0], lpsolve_constr_types.LE, val));
        }

      }
    }


    public double ObjectiveValue { get; set; }

    public LPsolveSolution Maximize()
    {
      // pass matrices to lp solve
      int numColumns;
      int lp;
      double[] numArray;
      PassMatricesToLpSolve(out numColumns, out lp, out numArray);

      LPsolve.set_obj_fn(lp, ref numArray[0]);
      LPsolve.set_maxim(lp);


      return SolveIt(lp);
    }


    public string CombineWithModel(string sNode)
    {
      var annotationDocument = new XmlDocument();
      var sbmlDocument = new XmlDocument();

      annotationDocument.LoadXml(sNode);
      sbmlDocument.LoadXml(ConvertToL2(SBML));
      if (sbmlDocument.DocumentElement == null)
        return sNode;

      var oList = annotationDocument.GetElementsByTagName("fluxBalance", STR_FBA_NAMESPACE);
      var fbaAnnotation = (XmlElement) oList[0];


      oList = sbmlDocument.GetElementsByTagName("fluxBalance", STR_FBA_NAMESPACE);
      if (oList.Count > 0)
      {
        var bAttached = false;
        for (var i = oList.Count; i > 0; --i)
        {
          var oElement = (XmlElement) oList[i - 1];
          var oParent = oElement.ParentNode;
          if (oParent == null) continue;
          oParent.RemoveChild(oElement);
          var oNew = sbmlDocument.ImportNode(fbaAnnotation, true);
          oParent.AppendChild(oNew);
          bAttached = true;
        }
        if (!bAttached)
        {
          var oParent = oList[0].ParentNode;
          var oNew = sbmlDocument.ImportNode(fbaAnnotation, true);
          if (oParent != null)
            oParent.InsertBefore(oNew, oParent.FirstChild);
        }
      }
      else
      {
        // no layout information present so far ... 
        oList = sbmlDocument.GetElementsByTagName("model");
        if (oList.Count > 0)
        {
          var firstModel = (XmlElement) oList[0];

          var firstChild = firstModel.FirstChild;
          if (firstChild.Name == "notes")
            firstChild = firstModel.ChildNodes[1];

          if (firstChild.Name == "annotation" || firstChild.Name == "annotations")
          {
            var newAnnotation = sbmlDocument.ImportNode(fbaAnnotation, true);
            firstChild.AppendChild(newAnnotation);
          }
          else
          {
            var oAnnotation = sbmlDocument.CreateElement("annotation");

            var newAnnotation = sbmlDocument.ImportNode(fbaAnnotation, true);
            oAnnotation.AppendChild(newAnnotation);
            firstModel.InsertBefore(oAnnotation, firstChild); //.AppendChild(oAnnotation);
          }
        }
      }

      // save the model
      var oBuilder = new StringBuilder();

      var oSettings = new XmlWriterSettings
      {
        Encoding = Encoding.UTF8,
        Indent = true,
        IndentChars = ("\t"),
        ConformanceLevel = ConformanceLevel.Document
      };
      //oSettings.NormalizeNewLines = true;

      var oWriter = XmlWriter.Create(oBuilder, oSettings);
      oWriter.WriteStartDocument();
      oWriter.WriteComment("Created on: " + DateTime.Now);
      sbmlDocument.DocumentElement.WriteTo(oWriter);
      oWriter.WriteEndDocument();

      oWriter.Flush();
      oWriter.Close();
      return oBuilder.ToString().Replace("utf-16", "utf-8");
    }

    public static string ConvertToL2(string model)
    {
      try
      {

        var doc = libsbml.readSBMLFromString(model);
        if (doc.isPackageEnabled("fbc"))
          doc.enablePackageInternal(doc.getNamespaces().getURI("fbc"), "fbc", false);
        var properties = new ConversionProperties(new SBMLNamespaces(2, 4));
        properties.addOption("strict", false);
        properties.addOption("setLevelAndVersion", true);
        properties.addOption("ignorePackages", true);
        doc.convert(properties);
        return libsbml.writeSBMLToString(doc);
      }
      catch// (Exception)
      {
        return model;
      }
    }
  

  public string WriteAsAnnotation()
    {
      var annotation = GetAnnotation();
      return CombineWithModel(annotation);
    }

    private static string ToFbcString(lpsolve_constr_types op)
    {
      switch (op)
      {
        case lpsolve_constr_types.LE:
          return "lessEqual";
        case lpsolve_constr_types.EQ:
        default:
          return "equal";
        case lpsolve_constr_types.GE:
          return "greaterEqual";
      }

    }



    public string WriteAsCobraAnnotation()
    {
      var doc = libsbml.readSBMLFromString(SBML);
      var model = doc.getModel();
      if (doc.getLevel() < 3)
      {
        var properties = new ConversionProperties(new SBMLNamespaces(3, 1));
        properties.addOption("strict", false);
        properties.addOption("setLevelAndVersion", true);
        properties.addOption("ignorePackages", true);
        doc.convert(properties);
      }
      doc.enablePackage(FbcExtension.getXmlnsL3V1V1(), "fbc", true);
      var plugin = (FbcModelPlugin)model.getPlugin("fbc");
      if (plugin == null)
      {

        throw new Exception("Could not save using Fbc. Please check that your model contains no errors!");
      }
      plugin.getListOfFluxBounds().clear();
      plugin.getListOfGeneAssociations().clear();
      plugin.getListOfObjectives().clear();

      foreach (var constraint in Constraints)
      {
        var bound = plugin.createFluxBound();
        bound.setReaction(constraint.Id);
        bound.setOperation(ToFbcString(constraint.Operator));
        bound.setValue(constraint.Value);
      }

      var active = plugin.createObjective();
      active.setId("objective1");
      active.setType(Mode == FBA_Mode.maximize ? "maximize" : "minimize");


      foreach (var objective in Objectives)
      {
        var current = active.createFluxObjective();
        current.setReaction(objective.Id);
        current.setCoefficient(objective.Value);
      }

      plugin.setActiveObjectiveId("objective1");

      // convert to COBRA
      var props = new ConversionProperties();
      props.addOption("convert fbc to cobra", true, "Convert FBC model to Cobra model");
      doc.convert(props);

      return libsbml.writeSBMLToString(doc);
    }

    private string WriteUsingFBC()
    {
      var doc = libsbml.readSBMLFromString(SBML);
      var model = doc.getModel();
      if (doc.getLevel() < 3)
      {
        var properties = new ConversionProperties(new SBMLNamespaces(3, 1));
        properties.addOption("strict", false);
        properties.addOption("setLevelAndVersion", true);
        properties.addOption("ignorePackages", true);
        doc.convert(properties);
      }
      doc.enablePackage(FbcExtension.getXmlnsL3V1V1(), "fbc", true);
      var plugin = (FbcModelPlugin)model.getPlugin("fbc");
      if (plugin == null)
      {

        throw new Exception("Could not save using Fbc. Please check that your model contains no errors!");
      }
      plugin.getListOfFluxBounds().clear();
      plugin.getListOfGeneAssociations().clear();
      plugin.getListOfObjectives().clear();

      foreach (var constraint in Constraints)
      {
        var bound = plugin.createFluxBound();
        bound.setReaction(constraint.Id);
        bound.setOperation(ToFbcString(constraint.Operator));
        bound.setValue(constraint.Value);
      }

      var active = plugin.createObjective();
      active.setId("objective1");
      active.setType(Mode == FBA_Mode.maximize ? "maximize" : "minimize");


      foreach (var objective in Objectives)
      {
        var current = active.createFluxObjective();
        current.setReaction(objective.Id);
        current.setCoefficient(objective.Value);
      }

      plugin.setActiveObjectiveId("objective1");

      return libsbml.writeSBMLToString(doc);
    }

    public string WriteSBML(bool useFbc = true)
    {
      if (useFbc)
        return WriteUsingFBC();
      return WriteAsAnnotation();
    }

    public void WriteToFile(string fileName)
    {
      File.WriteAllText(fileName, WriteSBML());
    }

    public void WriteLPToFile(string fileName)
    {
      int numColumns;
      int lp;
      double[] numArray;
      PassMatricesToLpSolve(out numColumns, out lp, out numArray);

      LPsolve.set_obj_fn(lp, ref numArray[0]);
      if (Mode == FBA_Mode.minimize)
        LPsolve.set_minim(lp);
      else
        LPsolve.set_maxim(lp);

      LPsolve.write_lp(lp, fileName);
      LPsolve.delete_lp(lp);
    }


    private void WriteTo(XmlWriter writer)
    {
      writer.WriteStartElement("fba", "fluxBalance", STR_FBA_NAMESPACE);

      if (Constraints != null && Constraints.Count > 0)
      {
        writer.WriteStartElement("fba", "listOfConstraints", STR_FBA_NAMESPACE);
        foreach (var constraint in Constraints)
        {
          constraint.WriteTo(writer);
        }
        writer.WriteEndElement();
      }

      if (Objectives != null)
      {
        writer.WriteStartElement("fba", "listOfObjectives", STR_FBA_NAMESPACE);
        writer.WriteAttributeString("activeObjective", STR_FBA_NAMESPACE, "obj1");

        writer.WriteStartElement("fba", "objective", STR_FBA_NAMESPACE);
        writer.WriteAttributeString("id", "obj1");
        writer.WriteAttributeString("type", STR_FBA_NAMESPACE,
                                    (Mode == FBA_Mode.maximize ? "maximize" : "minimize"));
        writer.WriteStartElement("fba", "listOfFluxes", STR_FBA_NAMESPACE);
        foreach (var objective in Objectives)
        {
          objective.WriteTo(writer);
        }
        writer.WriteEndElement();

        writer.WriteEndElement();

        writer.WriteEndElement();
      }

      writer.WriteEndElement();
    }

    private string GetAnnotation()
    {
      var stringBuilder = new StringBuilder();
      var settings = new XmlWriterSettings { Indent = true, Encoding = Encoding.UTF8 };
      var writer = XmlWriter.Create(stringBuilder, settings);
      WriteTo(writer);
      writer.Flush();
      writer.Close();
      return stringBuilder.ToString();
    }

    public LPsolveSolution Solve()
    {
      switch (Mode)
      {
        case FBA_Mode.minimize:
          return Minimize();
        case FBA_Mode.maximize:
        default:
          return Maximize();
      }
    }

    public lpsolve_return LastResult { get; set; }

    private LPsolveSolution SolveIt(int lp)
    {
      LPsolve.write_lp(lp, OutputFileName + ".lp");
      LPsolve.delete_lp(lp);
      lp = LPsolve.read_LP(OutputFileName + ".lp", 0, "");


      LastResult = LPsolve.solve(lp);
      ObjectiveValue = LastResult == lpsolve_return.INFEASIBLE ? double.NaN :
          LPsolve.get_objective(lp);

      if (LastResult == lpsolve_return.INFEASIBLE)
      {
        LPsolve.delete_lp(lp);
        lp = LPsolve.read_LP(OutputFileName + ".lp", 0, "");
        LPsolve.set_preferdual(lp, PreferDual);
        //LPsolve.set_simplextype(lp, lpsolve_simplextypes.SIMPLEX_DUAL_DUAL);
        LPsolve.set_basiscrash(lp, lpsolve_basiscrash.CRASH_MOSTFEASIBLE);
        LPsolve.set_presolve(lp, lpsolve_presolve.PRESOLVE_ROWS | lpsolve_presolve.PRESOLVE_COLS, LPsolve.get_presolveloops(lp));
        LastResult = LPsolve.solve(lp);
        ObjectiveValue = LastResult == lpsolve_return.INFEASIBLE ? double.NaN :
            LPsolve.get_objective(lp);
      }

      if (LastResult == lpsolve_return.INFEASIBLE)
      {
        LPsolve.delete_lp(lp);
        lp = LPsolve.read_LP(OutputFileName + ".lp", 0, "");
        LPsolve.set_simplextype(lp, lpsolve_simplextypes.SIMPLEX_DUAL_PRIMAL);
        LPsolve.set_scaling(lp, lpsolve_scales.SCALE_EQUILIBRATE | lpsolve_scales.SCALE_INTEGERS);
        LPsolve.set_pivoting(lp, lpsolve_piv_rules.PRICER_STEEPESTEDGE | lpsolve_piv_rules.PRICE_ADAPTIVE);
        LPsolve.set_anti_degen(lp, lpsolve_anti_degen.ANTIDEGEN_FIXEDVARS | lpsolve_anti_degen.ANTIDEGEN_STALLING);
        LPsolve.set_basiscrash(lp, lpsolve_basiscrash.CRASH_NOTHING);
        LPsolve.set_presolve(lp, lpsolve_presolve.PRESOLVE_NONE, LPsolve.get_presolveloops(lp));
        LPsolve.set_improve(lp, lpsolve_improves.IMPROVE_DUALFEAS | lpsolve_improves.IMPROVE_THETAGAP);
        LastResult = LPsolve.solve(lp);
        ObjectiveValue = LastResult == lpsolve_return.INFEASIBLE ? double.NaN :
            LPsolve.get_objective(lp);
      }


      if (OutputFileName != "" && PrintDebug)
      {
        LPsolve.print_str(lp,
                          string.Concat(new object[] { LastResult, ": ", LPsolve.get_objective(lp), "\n" }));
        LPsolve.print_lp(lp);
        LPsolve.print_objective(lp);
        LPsolve.print_solution(lp, 1);
        LPsolve.print_constraints(lp, 1);
        LPsolve.set_outputfile(lp, null);
      }

      int numColumns = LPsolve.get_Ncolumns(lp);
      var numArray2 = new double[numColumns];

      LPsolve.get_variables(lp, ref numArray2[0]);
      var solution = new double[numColumns];
      var solutionNames = new List<string>();

      for (var i = 0; i < numColumns; i++)
      {
        solution[i] = numArray2[i];
        string name = LPsolve.get_col_name(lp, i + 1);
        solutionNames.Add(name);
      }

      LPsolve.delete_lp(lp);

      // generate results
      LastSolution = new LPsolveSolution(solution, solutionNames);
      return LastSolution;
    }
    public LPsolveSolution Minimize()
    {
      // pass matrices to lp solve
      int numColumns;
      int lp;
      double[] numArray;
      PassMatricesToLpSolve(out numColumns, out lp, out numArray);

      LPsolve.set_obj_fn(lp, ref numArray[0]);
      LPsolve.set_minim(lp);

      return SolveIt(lp);
    }

    public LPsolveSolution LastSolution { get; set; }

    public bool LastResultHadError
    {
      get { return LastResult != lpsolve_return.OPTIMAL && LastResult != lpsolve_return.SUBOPTIMAL && LastResult != lpsolve_return.PRESOLVED; }
    }

    public string GetStatus()
    {

      switch (LastResult)
      {
        case lpsolve_return.NOMEMORY:
          return "Out of memory";
        case lpsolve_return.OPTIMAL:
          return "An optimal solution was obtained";
        case lpsolve_return.SUBOPTIMAL:
          return "The model is sub-optimal";
        case lpsolve_return.INFEASIBLE:
          return "The model is infeasible";
        case lpsolve_return.UNBOUNDED:
          return "The model is unbounded";
        case lpsolve_return.DEGENERATE:
          return "The model is degenerative";
        case lpsolve_return.NUMFAILURE:
          return "Numerical failure encountered";
        default:
        case lpsolve_return.USERABORT:
        case lpsolve_return.TIMEOUT:
        case lpsolve_return.PRESOLVED:
        case lpsolve_return.PROCFAIL:
        case lpsolve_return.PROCBREAK:
        case lpsolve_return.FEASFOUND:
        case lpsolve_return.NOFEASFOUND:
          return "";
      }
    }

    private void PassMatricesToLpSolve(out int numColumns, out int lp, out double[] numArray)
    {
      var numRows = _stoichiometry.Length;
      numColumns = numRows == 0 ? 0 : _stoichiometry[0].Length;

      // get a new lp instance
      lp = LPsolve.make_lp(0, numColumns);

      LPsolve.set_outputfile(lp, OutputFileName);

      // set reaction names
      for (var i = 0; i < numColumns; i++)
      {
        LPsolve.set_col_name(lp, i + 1, ReactionNames[i]);
      }

      // set up constraints from stoichiometry
      numArray = new double[numColumns + 1];
      for (var i = 0; i < numRows; i++)
      {
        for (var j = 0; j < numColumns; j++)
        {
          numArray[j + 1] = _stoichiometry[i][j];
        }
        LPsolve.add_constraint(lp, ref numArray[0], lpsolve_constr_types.EQ, 0.0);
      }

      //// set up custom constraints
      //foreach (var constraint in Constraints)
      //{
      //    var index = ReactionNames.IndexOf(constraint.Id);

      //    if (index == -1) continue; // oups ... probably should fail reaction not found

      //    numArray = new double[numColumns + 1];
      //    if (numArray.Length > index + 1)
      //        numArray[index + 1] = 1.0;

      //    LPsolve.add_constraint(lp, ref numArray[0], constraint.Operator, constraint.Value);
      //}
      var infty = Infinity;
      var map = new Dictionary<string, double[]>();

      try
      {
        foreach (string reactionName in ReactionNames)
        {
          if (!map.ContainsKey(reactionName))
          {
            var reversible = ReversibilityMap != null && ReversibilityMap.ContainsKey(reactionName) ? ReversibilityMap[reactionName] : true;
            map[reactionName] = new double[2] { reversible ? -infty : 0, infty };
          }
        }

        foreach (var constraint in Constraints)
        {

          switch (constraint.Operator)
          {
            case lpsolve_constr_types.LE:
              map[constraint.Id][1] = constraint.Value;
              break;
            case lpsolve_constr_types.GE:
              map[constraint.Id][0] = constraint.Value;
              break;
            case lpsolve_constr_types.EQ:
              map[constraint.Id][0] = constraint.Value;
              map[constraint.Id][1] = constraint.Value;
              break;
            default:
              continue;
          }
        }
      }
      catch
      {

      }

      // set up custom constraints
      foreach (var constraint in map.Keys)
      {
        var index = ReactionNames.IndexOf(constraint);
        if (index == -1) continue;
        //Debug.WriteLine(string.Format("{0} = [{1} ... {2}]", constraint, map[constraint][0], map[constraint][1]));
        LPsolve.set_bounds(lp, index + 1, map[constraint][0], map[constraint][1]);
      }

      numArray = new double[numColumns + 1];
      // set up objective function
      foreach (var objective in Objectives)
      {
        var index = ReactionNames.IndexOf(objective.Id);

        if (index == -1) continue; // oups ... probably should fail reaction not found

        numArray[index + 1] = objective.Value;
      }


      if (File.Exists(DefaultParameterFile))
      {
        LPsolve.read_params(lp, DefaultParameterFile, "-h Default");
      }

    }

    public static string DefaultParameterFile
    {

      get
      {
        try
        {
          var location = new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName;
          return Path.Combine(location, "lp_parameters.txt");
        }
        catch
        {
          return "lp_parameters.txt";
        }
      }
    }

    private void LoadSBMLContentIntoStructAnalysis(string sbmlContent)
    {
      var ascii = Encoding.ASCII.GetString(Encoding.UTF8.GetBytes(sbmlContent));
      StructAnalysis.LoadSBML(ascii);
      StructAnalysis.AnalyzeWithQR();
      SBML = ascii;
    }
  }
}