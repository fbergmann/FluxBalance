using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using LPsolveSBML;
using SBW;

namespace LPsolveSBMLUI
{
  static class Program
  {

    private static void RunTest(string[] args)
    {
      if (args.Length < 4)
      {
        Console.Error.WriteLine("Arguments for testrunner: -r <testcases dir> <case number> <output dir>");
        return;
      }

      var casesDir = args[1];
      var caseNumber = args[2];
      var outputDir = args[3];

      var sbmlFiles = Directory.GetFiles(Path.Combine(casesDir, caseNumber), "*.xml");

      var file = sbmlFiles.First(s => !s.Contains("sedml"));


      var fbModel = FluxBalance.ForFile(file);
      var result = fbModel.Solve();
      var current = fbModel.ObjectiveValue;

      var outputFile = Path.Combine(outputDir, caseNumber + ".csv");
      var sb = new StringBuilder();
      foreach (var item in result.Names)
      {
        sb.Append(item + ", ");
      }
      sb.Append(fbModel.ActiveObjective);
      sb.AppendLine();

      foreach (var item in result.Solution)
      {
        sb.Append(item + ", ");
      }
      sb.Append(current);
      sb.AppendLine();

      File.WriteAllText(outputFile, sb.ToString());
    }


    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {

      string fileName = null;
      foreach (string arg in args)
      {
        if (arg == "-r" || arg == "--run-test")
        {
          RunTest(args);
          return;
        }
        if (File.Exists(arg))
          fileName = arg;

      }

      var culture = System.Globalization.CultureInfo.CreateSpecificCulture("en");
      culture.NumberFormat.NumberDecimalSeparator = ".";
      Thread.CurrentThread.CurrentCulture = culture;
      try
      {
        Application.CurrentCulture = culture;
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);


        formInstance = new MainForm();
        object instance = formInstance;

        var module = new ModuleImplementation(
            "FluxBalance", "Flux Balance Analsysis", LowLevel.ModuleManagementType.UniqueModule,
            "Module performing Flux Balance Analysis on SBML Models");
        module.addService("analysis", "Flux Balance Analysis", "/Analysis", "Module performing Flux Balance Analysis on SBML Models", ref instance);

        module.ModuleShutdown += module_ModuleShutdown;

        module.EnableServices(args);

        if (fileName != null)
          formInstance.LoadSBMLFile(fileName);

        Application.Run(formInstance);
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
      }
    }

    static void module_ModuleShutdown(object sender, EventArgs e)
    {
      Environment.Exit(0);
    }

    static MainForm formInstance;
  }
}
