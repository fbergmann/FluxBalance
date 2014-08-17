using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using AutoLayout;
using LPsolveSBML;
using SBMLExtension;
using SBMLExtension.EmlRenderExtension;
using SBMLExtension.LayoutExtension;
using SBW;
using SBW.Utils;
using Text = SBMLExtension.EmlRenderExtension.Text;

namespace LPsolveSBMLUI
{
    public partial class MainForm : Form
    {
        private readonly SBWFavorites sbwFavs;
        private readonly SBWMenu sbwMenu;
        private FluxBalance _FluxBalance;
        private Hashtable reactionTextTable;

        public MainForm()
        {
            InitializeComponent();

            sbwMenu = new SBWMenu(sBWToolStripMenuItem, "FluxBalance",
                () => _FluxBalance.WriteSBML(!toolExportL2.Checked));
            sbwFavs = new SBWFavorites(() => _FluxBalance.WriteSBML(!toolExportL2.Checked), toolStrip1);
        }

        public LPsolveSolution LastResult { get; set; }
        public string SBML { get; set; }

        private static Text CreateTextLabel(string currentLabel)
        {
            var text = new Text
            {
                TheText = currentLabel,
                TextAnchor = "middle",
                FontFamily = "Arial",
                FontSize = "20",
                Stroke = "606060",
                FontWeight = "bold"
            };
            return text;
        }

        private void GenerateImageForSolution(LPsolveSolution solution)
        {
            // apply solution settings

            for (int i = 0; i < solution.Names.Count; i++)
            {
                if (!reactionTextTable.ContainsKey(solution.Names[i]))
                    continue;

                var label = (TempLabel) reactionTextTable[solution.Names[i]];
                for (int j = 0; j < label.LocalStyle.Group.Children.Count; j++)
                {
                    if (label.LocalStyle.Group.Children[j] is Text)
                    {
                        var text = (Text) label.LocalStyle.Group.Children[j];
                        double value = 0.0;
                        if (solution.Solution.Length > i)
                            value = solution.Solution[i];
                        text.TheText = String.Format("{0} = {1}", solution.Names[i], value);
                    }
                }
            }


            SetImage();
        }

        private void GenerateLayout()
        {
            // remove temporary labels

            if (reactionTextTable != null)
            {
                foreach (TempLabel label in reactionTextTable.Values)
                {
                    SBMLLayout.Instance.CurrentLayout.TextGlyphs.Remove(label.TextGlyph);
                    SBMLLayout.Instance.CurrentLayout.EmlDefault.Styles.Remove(label.LocalStyle);
                }
            }

            //
            if (SBMLLayout.Instance.hasLayout())
                SBML = SBMLLayout.Instance.writeSBML();

            if (string.IsNullOrEmpty(SBML))
                return;

            // save constraints
            List<LPsolveConstraint> constraints = controlSetup1.Constraints;
            List<LPsolveObjective> objectives = controlSetup1.Objectives;

            try
            {
                var network = new Network();
                network.LoadFromString(SBML);
                network.DoLayout();
                loadSBML(network.GetSBML());
            }
            catch
            {
                // if layout creation fails we continue without
            }

            // restore
            controlSetup1.Constraints = constraints;
            controlSetup1.Objectives = objectives;
        }

        private static double[] GetPosition(ReactionGlyph reactionGlyph)
        {
            double[] position;
            if (reactionGlyph.Curve.CurveSegments != null && reactionGlyph.Curve.CurveSegments.Count > 0)
            {
                CurveSegment curve = reactionGlyph.Curve.CurveSegments[0];
                position = curve.Start.toDoubleArray();
            }
            else
            {
                BoundingBox bounds = reactionGlyph.Bounds;
                if (!bounds.IsEmpty)
                {
                    position = bounds.Center.toDoubleArray();
                }
                else
                {
                    if (reactionGlyph.SpeciesReferences != null && reactionGlyph.SpeciesReferences.Count > 0 &&
                        reactionGlyph.SpeciesReferences[0].Curve != null)
                    {
                        CurveSegment curve = reactionGlyph.SpeciesReferences[0].Curve.CurveSegments[0];
                        //var vector = curve.End - curve.Start;
                        Point vector = curve.End - curve.Start;
                        var point = new Point(curve.Start.X + 0.5*vector.X, curve.Start.Y + 0.5*vector.Y);
                        position = point.toDoubleArray();
                    }
                    else
                    {
                        position = new[] {20.0, 20.0};
                    }
                }
            }
            return position;
        }

        private void InitializeLayout(string sbmlContent, FluxBalance fluxBalance)
        {
            // load Layout
            SBMLLayout.Instance.loadSBML(sbmlContent);

            if (!SBMLLayout.Instance.hasLayout())
            {
                GenerateLayout();
                return;
            }


            reactionTextTable = new Hashtable();

            int reactionCount = 0;
            // foreach reaction add a reaction label
            foreach (ReactionGlyph reactionGlyph in SBMLLayout.Instance.CurrentLayout.ReactionGlyphs)
            {
                var dimension = new[] {100.0, 100.0};
                double[] position = GetPosition(reactionGlyph);
                position[0] = position[0] - 50;

                string currentLabel = reactionGlyph.Name;


                Text text2 = CreateTextLabel(currentLabel);
                text2.Stroke = "FFFFFF";
                text2.FontSize = "22";
                text2.TextAnchor = "middle";

                Text text = CreateTextLabel(currentLabel);
                text.TextAnchor = "middle";

                var style = new LocalStyle();
                style.Group.Children.Add(text2);
                style.Group.Children.Add(text);
                style.IdList = "fb_label_" + reactionCount;

                SBMLLayout.Instance.CurrentLayout.EmlDefault.Styles.Add(style);

                SBMLLayout.Instance.addTextGlyph("fb_label_" + reactionCount, currentLabel, currentLabel, position,
                    dimension);

                TextGlyph textGlyph = SBMLLayout.Instance.CurrentLayout.findTextGlyphById("fb_label_" + reactionCount);

                reactionTextTable[currentLabel] = new TempLabel(textGlyph, text, style);


                reactionCount++;
            }
        }

        public void LoadSBMLFile(string fileName)
        {
            string sbmlContent = File.ReadAllText(fileName);
            string name = new FileInfo(fileName).Name;
            loadSBML(sbmlContent);
            Text = String.Format("Flux Balance - [{0}]", name);
        }

        private void OnAboutClicked(object sender, EventArgs e)
        {
            using (var dialog = new AboutBox1())
            {
                dialog.ShowDialog();
            }
        }

        private void OnCopyToClipboard(object sender, EventArgs e)
        {
            if (LastResult != null)
            {
                try
                {
                    Clipboard.SetText(LastResult.ToCSV("\t"), TextDataFormat.CommaSeparatedValue);
                    Clipboard.SetText(LastResult.ToCSV("\t"), TextDataFormat.Text);
                }
                catch (Exception)
                {
                    //
                }
            }
        }

        private void OnDragDrop(object sender, DragEventArgs e)
        {
            try
            {
                var filenames = (string[]) e.Data.GetData(DataFormats.FileDrop);
                var oInfo = new FileInfo(filenames[0]);
                if (oInfo.Extension.ToLower() == ".xml" || oInfo.Extension.ToLower() == ".sbml")
                {
                    LoadSBMLFile(filenames[0]);
                }
            }
            catch (Exception)
            {
            }
        }

        private void OnDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var sFilenames = (string[]) e.Data.GetData(DataFormats.FileDrop);
                var oInfo = new FileInfo(sFilenames[0]);
                if (oInfo.Extension.ToLower() == ".xml" || oInfo.Extension.ToLower() == ".sbml")
                {
                    e.Effect = DragDropEffects.Copy;
                    return;
                }
            }
            e.Effect = DragDropEffects.None;
        }

        private void OnExitClicked(object sender, EventArgs e)
        {
            Close();
        }

        private void OnGenerateLayoutClicked(object sender, EventArgs e)
        {
            GenerateLayout();
        }

        private void OnImportConstraintsClicked(object sender, EventArgs e)
        {
            if (_FluxBalance == null)
            {
                MessageBox.Show("Load an SBML file first.", "No file loaded", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (
                var dialog = new OpenFileDialog
                {
                    Title = "Import F.A.M.E constraints",
                    Filter = "Text files|*.txt|All files|*.*"
                })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _FluxBalance.LoadConstraintsFromFame(dialog.FileName);
                    controlSetup1.Constraints = _FluxBalance.Constraints;
                }
            }
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            LowLevel.SBWConnect();
            if (SBWLowLevel.isConnected())
            {
                sbwMenu.UpdateSBWMenu();
                sbwFavs.Update();
                SBWExporter.SetupImport(mnuImport, doAnalysis);
            }
            else
            {
                // ignore SBW not present
                sBWToolStripMenuItem.Visible = false;
                mnuImport.Visible = false;
                while (toolStrip1.Items.Count > 5)
                    toolStrip1.Items.RemoveAt(5);
            }
        }

        private void OnLoadClicked(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void OnPicturePaint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            double[] dimensions = SBMLLayout.Instance.getDimensions();
            var width = (float) dimensions[0];
            // ReSharper disable CompareOfFloatsByEqualityOperator
            if (width == 0f) width = 1;
            var height = (float) dimensions[1];
            if (height == 0f) height = 1;
            // ReSharper restore CompareOfFloatsByEqualityOperator
            float scale = Math.Min(pictureBox1.Width/width, pictureBox1.Height/height);
            SBMLLayout.Instance.CurrentLayout.drawLayout(e.Graphics, scale);
        }

        private void OnPrintClicked(object sender, EventArgs e)
        {
            PrintImage();
        }

        private void OnPrintPage(object sender, PrintPageEventArgs e)
        {
            double[] dimensions = SBMLLayout.Instance.getDimensions();
            float scale = Math.Min(e.PageBounds.Width/(float) dimensions[0], e.PageBounds.Height/(float) dimensions[1]);
            SBMLLayout.Instance.CurrentLayout.drawLayout(e.Graphics, scale);
        }

        private void OnRunClicked(object sender, EventArgs e)
        {
            if (_FluxBalance == null) return;
            _FluxBalance.Constraints = controlSetup1.Constraints;
            _FluxBalance.Objectives = controlSetup1.Objectives;
            LPsolveSolution solution = controlSetup1.Maximize ? _FluxBalance.Solve() : _FluxBalance.Minimize();

            LastResult = solution;

            GenerateImageForSolution(solution);

            dataGridView1.Rows.Clear();
            for (int i = 0; i < LastResult.Names.Count; i++)
            {
                dataGridView1.Rows.Add(LastResult.Names[i], LastResult.Solution[i]);
            }

            lblResult.Text = string.Format("Value for last objective is: {0}", _FluxBalance.ObjectiveValue);
            lblStatus.Text = _FluxBalance.GetStatus();

            if (_FluxBalance.LastResultHadError)
            {
                MessageBox.Show(this,
                    "The last run was not successful, the result was: " + _FluxBalance.GetStatus(),
                    "Solution not found",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void OnSaveClicked(object sender, EventArgs e)
        {
            try
            {
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    _FluxBalance.Constraints = controlSetup1.Constraints;
                    _FluxBalance.Objectives = controlSetup1.Objectives;
                    _FluxBalance.Mode = (controlSetup1.Maximize ? FBA_Mode.maximize : FBA_Mode.minimize);
                    string fileName = saveDialog.FileName;
                    if (fileName.EndsWith(".lp"))
                        _FluxBalance.WriteLPToFile(fileName);
                    else
                    {
                        if (saveDialog.FilterIndex == 2)
                            File.WriteAllText(fileName, _FluxBalance.WriteAsAnnotation());
                        else if (saveDialog.FilterIndex == 3)
                            File.WriteAllText(fileName, _FluxBalance.WriteAsCobraAnnotation());
                        else
                            _FluxBalance.WriteToFile(fileName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Could not save file due to:  " + ex.Message, "Could not save",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void OpenFile()
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    LoadSBMLFile(openFileDialog1.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    string.Format("A fatal error occured trying to load the model, the error message is: {0}",
                        ex.Message),
                    "Could not load model.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void PrintImage()
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void SetImage()
        {
            pictureBox1.Refresh();
        }

        public void doAnalysis(string sbmlContent)
        {
            Invoke(new MethodInvoker(() => loadSBML(sbmlContent)));
        }

        public void loadSBML(string sbmlContent)
        {
            SBML = Encoding.ASCII.GetString(Encoding.UTF8.GetBytes(sbmlContent));


            reactionTextTable = new Hashtable();

            _FluxBalance = new FluxBalance(SBML);

            if (_FluxBalance.IsEmpty)
            {
                MessageBox.Show(this,
                    "The model you have loaded has an empty Stoichiometry Matrix. As such the model is not suited for Flux Balance Analysis. Please ensure that you loaded a model that uses SBML non-boundary species and reactions.",
                    "Model not suitable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            bool doLayout = true;
            if (_FluxBalance.SpeciesNames.Count > 100 || _FluxBalance.ReactionNames.Count > 100)
            {
                if (MessageBox.Show(this,
                    string.Format(
                        "The model you have loaded has {0} species and {1} reactions. Would you still like to generate an image for it? The image is unlikely to provide much information.",
                        _FluxBalance.SpeciesNames.Count, _FluxBalance.ReactionNames.Count),
                    "Generate Layout?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    doLayout = false;
            }

            if (doLayout)
                InitializeLayout(SBML, _FluxBalance);

            if (SBMLLayout.Instance.hasLayout())
            {
                splitContainer2.Panel1Collapsed = false;
                SetImage();
            }
            else
            {
                splitContainer2.Panel1Collapsed = true;
            }

            controlSetup1.InitializeFromReactionNames(_FluxBalance.ReactionNames);

            controlSetup1.Maximize = _FluxBalance.Mode == FBA_Mode.maximize;
            controlSetup1.Minimize = _FluxBalance.Mode == FBA_Mode.minimize;

            controlSetup1.Constraints = _FluxBalance.Constraints;
            controlSetup1.Objectives = _FluxBalance.Objectives;
        }
    }
}