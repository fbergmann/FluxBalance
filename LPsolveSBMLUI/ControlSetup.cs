using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using LPsolveSBML;

namespace LPsolveSBMLUI
{
    public partial class ControlSetup : UserControl
    {
        public event EventHandler LoadClicked;
        protected virtual void OnLoadClicked()
        {
            if (LoadClicked != null)
                LoadClicked(this, EventArgs.Empty);
        }
        public event EventHandler LayoutClicked;
        protected virtual void OnLayoutClicked()
        {
            if (LayoutClicked != null)
                LayoutClicked(this, EventArgs.Empty);
        }

        public event EventHandler RunClicked;
        protected virtual void OnRunClicked()
        {
            if (RunClicked != null)
                RunClicked(this, EventArgs.Empty);
        }


        public ControlSetup()
        {
            InitializeComponent();
            OP.DataSource = Operations;

            dataGridView1.DataError += new DataGridViewDataErrorEventHandler(DataError);
        }

        void DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("wtf");
        }

        private void cmdLoad_Click(object sender, EventArgs e)
        {
            OnLoadClicked();
        }

        private void cmdRun_Click(object sender, EventArgs e)
        {
            OnRunClicked();                        
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<LPsolveConstraint> Constraints
        {
            get
            {
                return GetConstraints();
            }
            set
            {
                for (int i = dataGridView1.Rows.Count - 1; i >= 0; i--)
                    if (!dataGridView1.Rows[i].IsNewRow)
                    dataGridView1.Rows.RemoveAt(i);
                                
                for (int i = 0; i < value.Count; i++)
                {
                    dataGridView1.Rows.Add(value[i].Id, value[i].OperatorString, value[i].Value);
                }
            }
        }

        private List<LPsolveConstraint> GetConstraints()
        {
            List<LPsolveConstraint> result = new List<LPsolveConstraint>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string id = (string)row.Cells[0].Value;
                if (string.IsNullOrEmpty(id)) continue;
                result.Add(new LPsolveConstraint(id, GetOp(row.Cells[1].Value), Convert.ToDouble(row.Cells[2].Value)));
            }

            return result;
        }

        private lpsolve_constr_types GetOp(object opString)
        {
            if (opString is string)
            {
                string op = opString as string;
                switch (op)
                {
                    case "<=":
                        return lpsolve_constr_types.LE;
                    case ">=":
                        return lpsolve_constr_types.GE;
                    case "=":
                    default:
                        return lpsolve_constr_types.EQ;
                }
            }
            return lpsolve_constr_types.EQ;
        }


        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<LPsolveObjective> Objectives
        {
            get { return GetObjectives(); }
            set
            {

                for (int i = dataGridView2.Rows.Count - 1; i >= 0; i--)
                    if (!dataGridView2.Rows[i].IsNewRow)
                        dataGridView2.Rows.RemoveAt(i);

                
                for (int i = 0; i < value.Count; i++)
                {
                    dataGridView2.Rows.Add(value[i].Id, value[i].Value);
                }

            }
        }

        private List<LPsolveObjective> GetObjectives()
        {
            List<LPsolveObjective> result = new List<LPsolveObjective>();

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                string id = (string)row.Cells[0].Value;
                if (string.IsNullOrEmpty(id)) continue;
                result.Add(new LPsolveObjective(id, Convert.ToDouble(row.Cells[1].Value)));
            }

            return result;
        }


        public void InitializeFromReactionNames(List<string> reactionNames)
        {
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();

            this.ObjectiveId.DataSource = reactionNames;
            this.ID.DataSource = reactionNames;
        }


        static List<string> Operations = new List<string>(new string[] { "=", ">=", "<="} );

        private void cmdLayout_Click(object sender, EventArgs e)
        {
            OnLayoutClicked();  
        }

        public bool Maximize
        {
            get
            {
                return radMaximize.Checked;
            }
            set
            {
            	radMaximize.Checked = value;
            }
        }

        public bool Minimize
        {
            get
            {
                return radMinimize.Checked;
            }
            set
            {
            	radMinimize.Checked = value;
            }
        }
    }
}
