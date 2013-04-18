using System;
using System.IO;
using System.Runtime.InteropServices;

namespace LPsolveSBML
{
    public enum lpsolve_constr_types
    {
        LE = 1,
        EQ = 3,
        GE = 2,
        FR = 0,
    }

    public enum lpsolve_scales
    {
        SCALE_EXTREME = 1,
        SCALE_RANGE = 2,
        SCALE_MEAN = 3,
        SCALE_GEOMETRIC = 4,
        SCALE_CURTISREID = 7,
        SCALE_QUADRATIC = 8,
        SCALE_LOGARITHMIC = 16,
        SCALE_USERWEIGHT = 31,
        SCALE_POWER2 = 32,
        SCALE_EQUILIBRATE = 64,
        SCALE_INTEGERS = 128,
        SCALE_DYNUPDATE = 256,
        SCALE_ROWSONLY = 512,
        SCALE_COLSONLY = 1024,
    }

    public enum lpsolve_improves
    {
        IMPROVE_NONE = 0,
        IMPROVE_SOLUTION = 1,
        IMPROVE_DUALFEAS = 2,
        IMPROVE_THETAGAP = 4,
        IMPROVE_BBSIMPLEX = 8,
        IMPROVE_DEFAULT = (IMPROVE_DUALFEAS + IMPROVE_THETAGAP),
        IMPROVE_INVERSE = (IMPROVE_SOLUTION + IMPROVE_THETAGAP)
    }

    public enum lpsolve_piv_rules
    {
        PRICER_FIRSTINDEX = 0,
        PRICER_DANTZIG = 1,
        PRICER_DEVEX = 2,
        PRICER_STEEPESTEDGE = 3,
        PRICE_PRIMALFALLBACK = 4,
        PRICE_MULTIPLE = 8,
        PRICE_PARTIAL = 16,
        PRICE_ADAPTIVE = 32,
        PRICE_HYBRID = 64,
        PRICE_RANDOMIZE = 128,
        PRICE_AUTOPARTIALCOLS = 256,
        PRICE_AUTOPARTIALROWS = 512,
        PRICE_LOOPLEFT = 1024,
        PRICE_LOOPALTERNATE = 2048,
        PRICE_AUTOPARTIAL = PRICE_AUTOPARTIALCOLS + PRICE_AUTOPARTIALROWS,
    }

    public enum lpsolve_presolve
    {
        PRESOLVE_NONE = 0,
        PRESOLVE_ROWS = 1,
        PRESOLVE_COLS = 2,
        PRESOLVE_LINDEP = 4,
        PRESOLVE_SOS = 32,
        PRESOLVE_REDUCEMIP = 64,
        PRESOLVE_KNAPSACK = 128,
        PRESOLVE_ELIMEQ2 = 256,
        PRESOLVE_IMPLIEDFREE = 512,
        PRESOLVE_REDUCEGCD = 1024,
        PRESOLVE_PROBEFIX = 2048,
        PRESOLVE_PROBEREDUCE = 4096,
        PRESOLVE_ROWDOMINATE = 8192,
        PRESOLVE_COLDOMINATE = 16384,
        PRESOLVE_MERGEROWS = 32768,
        PRESOLVE_IMPLIEDSLK = 65536,
        PRESOLVE_COLFIXDUAL = 131072,
        PRESOLVE_BOUNDS = 262144,
        PRESOLVE_DUALS = 524288,
        PRESOLVE_SENSDUALS = 1048576
    }

    public enum lpsolve_anti_degen
    {
        ANTIDEGEN_NONE = 0,
        ANTIDEGEN_FIXEDVARS = 1,
        ANTIDEGEN_COLUMNCHECK = 2,
        ANTIDEGEN_STALLING = 4,
        ANTIDEGEN_NUMFAILURE = 8,
        ANTIDEGEN_LOSTFEAS = 16,
        ANTIDEGEN_INFEASIBLE = 32,
        ANTIDEGEN_DYNAMIC = 64,
        ANTIDEGEN_DURINGBB = 128,
        ANTIDEGEN_RHSPERTURB = 256,
        ANTIDEGEN_BOUNDFLIP = 512
    }

    public enum lpsolve_basiscrash
    {
        CRASH_NOTHING = 0,
        CRASH_MOSTFEASIBLE = 2,
    }

    public enum lpsolve_simplextypes
    {
        SIMPLEX_PRIMAL_PRIMAL = 5,
        SIMPLEX_DUAL_PRIMAL = 6,
        SIMPLEX_PRIMAL_DUAL = 9,
        SIMPLEX_DUAL_DUAL = 10,
    }

    public enum lpsolve_BBstrategies
    {
        NODE_FIRSTSELECT = 0,
        NODE_GAPSELECT = 1,
        NODE_RANGESELECT = 2,
        NODE_FRACTIONSELECT = 3,
        NODE_PSEUDOCOSTSELECT = 4,
        NODE_PSEUDONONINTSELECT = 5,
        NODE_PSEUDORATIOSELECT = 6,
        NODE_USERSELECT = 7,
        NODE_WEIGHTREVERSEMODE = 8,
        NODE_BRANCHREVERSEMODE = 16,
        NODE_GREEDYMODE = 32,
        NODE_PSEUDOCOSTMODE = 64,
        NODE_DEPTHFIRSTMODE = 128,
        NODE_RANDOMIZEMODE = 256,
        NODE_GUBMODE = 512,
        NODE_DYNAMICMODE = 1024,
        NODE_RESTARTMODE = 2048,
        NODE_BREADTHFIRSTMODE = 4096,
        NODE_AUTOORDER = 8192,
        NODE_RCOSTFIXING = 16384,
        NODE_STRONGINIT = 32768
    }

    public enum lpsolve_return
    {
        NOMEMORY = -2,
        OPTIMAL = 0,
        SUBOPTIMAL = 1,
        INFEASIBLE = 2,
        UNBOUNDED = 3,
        DEGENERATE = 4,
        NUMFAILURE = 5,
        USERABORT = 6,
        TIMEOUT = 7,
        PRESOLVED = 9,
        PROCFAIL = 10,
        PROCBREAK = 11,
        FEASFOUND = 12,
        NOFEASFOUND = 13,
    }

    public enum lpsolve_branch
    {
        BRANCH_CEILING = 0,
        BRANCH_FLOOR = 1,
        BRANCH_AUTOMATIC = 2,
    }

    public enum lpsolve_msgmask
    {
        MSG_PRESOLVE = 1,
        MSG_LPFEASIBLE = 8,
        MSG_LPOPTIMAL = 16,
        MSG_MILPEQUAL = 32,
        MSG_MILPFEASIBLE = 128,
        MSG_MILPBETTER = 512,
    }


    public class LPsolve
    {
        public const string LP_SOLVE_LIB = "lpsolve55";

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool add_column(int lp, ref double column);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool add_columnex(int lp, int count, ref double column, ref int rowno);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool add_constraint(int lp, ref double row, lpsolve_constr_types constr_type, double rh);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool add_constraintex(int lp, int count, ref double row, ref int colno,
                                                   lpsolve_constr_types constr_type, double rh);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool add_lag_con(int lp, ref double row, lpsolve_constr_types con_type, double rhs);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern int add_SOS(int lp, string name, int sostype, int priority, int count, ref int sosvars,
                                         ref double weights);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern int column_in_lp(int lp, ref double column);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern int copy_lp(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void default_basis(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool del_column(int lp, int column);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool del_constraint(int lp, int del_row);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void delete_lp(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool dualize_lp(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern lpsolve_anti_degen get_anti_degen(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool get_basis(int lp, ref int bascolumn, bool nonbasic);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern lpsolve_basiscrash get_basiscrash(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern int get_bb_depthlimit(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern lpsolve_branch get_bb_floorfirst(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern lpsolve_BBstrategies get_bb_rule(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool get_bounds_tighter(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern double get_break_at_value(int lp);

        //[DllImport(LP_SOLVE_LIB, SetLastError=true)] public static extern string get_col_name(int lp, int column);
        [DllImport(LP_SOLVE_LIB, EntryPoint = "get_col_name", SetLastError = true)]
        private static extern IntPtr get_col_name_c(int lp, int column);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool get_column(int lp, int col_nr, ref double column);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern int get_columnex(int lp, int col_nr, ref double column, ref int nzrow);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern lpsolve_constr_types get_constr_type(int lp, int row);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern double get_constr_value(int lp, int row, int count, ref double primsolution,
                                                     ref int nzindex);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool get_constraints(int lp, ref double constr);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool get_dual_solution(int lp, ref double rc);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern double get_epsb(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern double get_epsd(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern double get_epsel(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern double get_epsint(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern double get_epsperturb(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern double get_epspivot(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern lpsolve_improves get_improve(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern double get_infinite(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool get_lambda(int lp, ref double lambda);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern double get_lowbo(int lp, int column);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern int get_lp_index(int lp, int orig_index);

        //[DllImport(LP_SOLVE_LIB, SetLastError=true)] public static extern string get_lp_name(int lp);
        [DllImport(LP_SOLVE_LIB, EntryPoint = "get_lp_name", SetLastError = true)]
        private static extern IntPtr get_lp_name_c(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern int get_Lrows(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern double get_mat(int lp, int row, int column);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern int get_max_level(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern int get_maxpivot(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern double get_mip_gap(int lp, bool absolute);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern int get_Ncolumns(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern double get_negrange(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern int get_nameindex(int lp, string name, bool isrow);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern int get_nonzeros(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern int get_Norig_columns(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern int get_Norig_rows(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern int get_Nrows(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern double get_obj_bound(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern double get_objective(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern int get_orig_index(int lp, int lp_index);

        //[DllImport(LP_SOLVE_LIB, SetLastError=true)] public static extern string get_origcol_name(int lp, int column);
        [DllImport(LP_SOLVE_LIB, EntryPoint = "get_origcol_name", SetLastError = true)]
        private static extern IntPtr get_origcol_name_c(int lp, int column);

        //[DllImport(LP_SOLVE_LIB, SetLastError=true)] public static extern string get_origrow_name(int lp, int row);
        [DllImport(LP_SOLVE_LIB, EntryPoint = "get_origrow_name", SetLastError = true)]
        private static extern IntPtr get_origrow_name_c(int lp, int row);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern lpsolve_piv_rules get_pivoting(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern lpsolve_presolve get_presolve(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern int get_presolveloops(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool get_primal_solution(int lp, ref double pv);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern int get_print_sol(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool get_PseudoCosts(int lp, ref double clower, ref double cupper, ref int updatelimit);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern double get_rh(int lp, int row);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern double get_rh_range(int lp, int row);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool get_row(int lp, int row_nr, ref double row);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern int get_rowex(int lp, int row_nr, ref double row, ref int colno);

        //[DllImport(LP_SOLVE_LIB, SetLastError=true)] public static extern string get_row_name(int lp, int row);
        [DllImport(LP_SOLVE_LIB, EntryPoint = "get_row_name", SetLastError = true)]
        private static extern IntPtr get_row_name_c(int lp, int row);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern double get_scalelimit(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern lpsolve_scales get_scaling(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool get_sensitivity_obj(int lp, ref double objfrom, ref double objtill);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool get_sensitivity_objex(int lp, ref double objfrom, ref double objtill,
                                                        ref double objfromvalue, ref double objtillvalue);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool get_sensitivity_rhs(int lp, ref double duals, ref double dualsfrom,
                                                      ref double dualstill);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern lpsolve_simplextypes get_simplextype(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern int get_solutioncount(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern int get_solutionlimit(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern int get_status(int lp);

        //[DllImport(LP_SOLVE_LIB, SetLastError=true)] public static extern string get_statustext(int lp, int statuscode);
        [DllImport(LP_SOLVE_LIB, EntryPoint = "get_statustext", SetLastError = true)]
        private static extern IntPtr get_statustext_c(int lp, int statuscode);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern int get_timeout(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern long get_total_iter(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern long get_total_nodes(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern double get_upbo(int lp, int column);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern lpsolve_branch get_var_branch(int lp, int column);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern double get_var_dualresult(int lp, int index);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern double get_var_primalresult(int lp, int index);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern int get_var_priority(int lp, int column);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool get_variables(int lp, ref double var);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern int get_verbose(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern double get_working_objective(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool guess_basis(int lp, ref double guessvector, ref int basisvector);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool has_BFP(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool has_XLI(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool is_add_rowmode(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool is_anti_degen(int lp, lpsolve_scales testmask);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool is_binary(int lp, int column);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool is_break_at_first(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool is_constr_type(int lp, int row, int mask);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool is_debug(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool is_feasible(int lp, ref double values, double threshold);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool is_infinite(int lp, double value);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool is_int(int lp, int column);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool is_integerscaling(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool is_lag_trace(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool is_maxim(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool is_nativeBFP(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool is_nativeXLI(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool is_negative(int lp, int column);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool is_piv_mode(int lp, lpsolve_scales testmask);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool is_piv_rule(int lp, lpsolve_piv_rules rule);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool is_presolve(int lp, lpsolve_scales testmask);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool is_scalemode(int lp, lpsolve_scales testmask);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool is_scaletype(int lp, lpsolve_scales scaletype);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool is_semicont(int lp, int column);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool is_SOS_var(int lp, int column);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool is_trace(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool is_unbounded(int lp, int column);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool is_use_names(int lp, bool isrow);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void lp_solve_version(ref int majorversion, ref int minorversion, ref int release,
                                                   ref int build);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern int make_lp(int rows, int columns);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern int resize_lp(int lp, int rows, int columns);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void print_constraints(int lp, int columns);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool print_debugdump(int lp, string filename);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void print_duals(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void print_lp(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void print_objective(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void print_scales(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void print_solution(int lp, int columns);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void print_str(int lp, string str);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void print_tableau(int lp);

        public delegate bool ctrlcfunc(int lp, int userhandle);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void put_abortfunc(int lp, ctrlcfunc newctrlc, int ctrlchandle);

        public delegate void logfunc(int lp, int userhandle, string buf);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void put_logfunc(int lp, logfunc newlog, int loghandle);

        public delegate void msgfunc(int lp, int userhandle, lpsolve_msgmask message);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void put_msgfunc(int lp, msgfunc newmsg, int msghandle, int mask);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool read_basis(int lp, string filename, string info);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern int read_freeMPS(string filename, int verbose);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern int read_LP(string filename, int verbose, string lp_name);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern int read_MPS(string filename, int verbose);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern int read_XLI(string xliname, string modelname, string dataname, string options, int verbose);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool read_params(int lp, string filename, string options);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void reset_basis(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void reset_params(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool set_add_rowmode(int lp, bool turnon);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void set_anti_degen(int lp, lpsolve_anti_degen anti_degen);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool set_basis(int lp, ref int bascolumn, bool nonbasic);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void set_basiscrash(int lp, lpsolve_basiscrash mode);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void set_basisvar(int lp, int basisPos, int enteringCol);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void set_bb_depthlimit(int lp, int bb_maxlevel);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void set_bb_floorfirst(int lp, lpsolve_branch bb_floorfirst);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void set_bb_rule(int lp, lpsolve_BBstrategies bb_rule);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool set_BFP(int lp, string filename);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool set_binary(int lp, int column, bool must_be_bin);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool set_bounds(int lp, int column, double lower, double upper);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void set_bounds_tighter(int lp, bool tighten);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void set_break_at_first(int lp, bool break_at_first);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void set_break_at_value(int lp, double break_at_value);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool set_col_name(int lp, int column, string new_name);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool set_column(int lp, int col_no, ref double column);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool set_columnex(int lp, int col_no, int count, ref double column, ref int rowno);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool set_constr_type(int lp, int row, lpsolve_constr_types con_type);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void set_debug(int lp, bool debug);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void set_epsb(int lp, double epsb);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void set_epsd(int lp, double epsd);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void set_epsel(int lp, double epsel);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void set_epsint(int lp, double epsint);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool set_epslevel(int lp, int level);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void set_epsperturb(int lp, double epsperturb);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void set_epspivot(int lp, double epspivot);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void set_improve(int lp, lpsolve_improves improve);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void set_infinite(int lp, double infinite);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool set_int(int lp, int column, bool must_be_int);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void set_lag_trace(int lp, bool lag_trace);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool set_lowbo(int lp, int column, double value);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool set_lp_name(int lp, string lpname);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool set_mat(int lp, int row, int column, double value);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void set_maxim(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void set_maxpivot(int lp, int max_num_inv);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void set_minim(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void set_mip_gap(int lp, bool absolute, double mip_gap);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void set_negrange(int lp, double negrange);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool set_obj(int lp, int Column, double Value);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void set_obj_bound(int lp, double obj_bound);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool set_obj_fn(int lp, ref double row);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool set_obj_fnex(int lp, int count, ref double row, ref int colno);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool set_outputfile(int lp, string filename);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void set_pivoting(int lp, lpsolve_piv_rules piv_rule);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void set_preferdual(int lp, bool dodual);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void set_presolve(int lp, lpsolve_presolve do_presolve, int maxloops);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void set_print_sol(int lp, int print_sol);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool set_PseudoCosts(int lp, ref double clower, ref double cupper, ref int updatelimit);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool set_rh(int lp, int row, double value);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool set_rh_range(int lp, int row, double deltavalue);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void set_rh_vec(int lp, ref double rh);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool set_row(int lp, int row_no, ref double row);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool set_row_name(int lp, int row, string new_name);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool set_rowex(int lp, int row_no, int count, ref double row, ref int colno);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void set_scalelimit(int lp, double scalelimit);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void set_scaling(int lp, lpsolve_scales scalemode);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool set_semicont(int lp, int column, bool must_be_sc);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void set_sense(int lp, bool maximize);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void set_simplextype(int lp, lpsolve_simplextypes simplextype);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void set_solutionlimit(int lp, int limit);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void set_timeout(int lp, int sectimeout);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void set_trace(int lp, bool trace);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool set_unbounded(int lp, int column);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool set_upbo(int lp, int column, double value);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void set_use_names(int lp, bool isrow, bool use_names);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool set_var_branch(int lp, int column, lpsolve_branch branch_mode);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool set_var_weights(int lp, ref double weights);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void set_verbose(int lp, int verbose);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool set_XLI(int lp, string filename);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern lpsolve_return solve(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool str_add_column(int lp, string col_string);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool str_add_constraint(int lp, string row_string, lpsolve_constr_types constr_type,
                                                     double rh);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool str_add_lag_con(int lp, string row_string, lpsolve_constr_types con_type, double rhs);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool str_set_obj_fn(int lp, string row_string);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool str_set_rh_vec(int lp, string rh_string);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern double time_elapsed(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern void unscale(int lp);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool write_basis(int lp, string filename);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool write_freemps(int lp, string filename);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool write_lp(int lp, string filename);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool write_mps(int lp, string filename);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool write_XLI(int lp, string filename, string options, bool results);

        [DllImport(LP_SOLVE_LIB, SetLastError = true)]
        public static extern bool write_params(int lp, string filename, string options);

        public static string get_col_name(int lp, int column)
        {
            return (Marshal.PtrToStringAnsi(get_col_name_c(lp, column)));
        }

        public string get_lp_name(int lp)
        {
            return (Marshal.PtrToStringAnsi(get_lp_name_c(lp)));
        }

        public string get_origcol_name(int lp, int column)
        {
            return (Marshal.PtrToStringAnsi(get_origcol_name_c(lp, column)));
        }

        public string get_origrow_name(int lp, int row)
        {
            return (Marshal.PtrToStringAnsi(get_origrow_name_c(lp, row)));
        }

        public string get_row_name(int lp, int row)
        {
            return (Marshal.PtrToStringAnsi(get_row_name_c(lp, row)));
        }

        public string get_statustext(int lp, int statuscode)
        {
            return (Marshal.PtrToStringAnsi(get_statustext_c(lp, statuscode)));
        }

        [DllImport("kernel32", SetLastError = true)]
        private static extern int SetEnvironmentVariableA(string lpName, string lpValue);

        [DllImport("kernel32", SetLastError = true)]
        private static extern int GetEnvironmentVariableA(string lpName, string lpBuffer, int nSize);

        private static bool SetEnvironmentVariable(string Name, string val)
        {
            return SetEnvironmentVariableA(Name, val) == 0 ? false : true;
        }

        private static string GetEnvironmentVariable(string Name)
        {
            return Environment.GetEnvironmentVariable(Name);
        }


        public static bool Init()
        {
            return Init("");
        }


        private static bool bEnvChanged;

        public static bool Init(string dllPath)
        {
            string Path;
            string Buf;
            bool returnValue;

            if (dllPath != "")
            {
                dllPath = Environment.CurrentDirectory;
            }
            Buf = dllPath;
            if (Buf.Substring(Buf.Length - 1, 1) != "\\")
            {
                Buf += "\\";
            }
            Buf += LP_SOLVE_LIB;

            returnValue = File.Exists(Buf);
            if (returnValue)
            {
                if (!bEnvChanged)
                {
                    bEnvChanged = true;
                    Path = GetEnvironmentVariable("PATH");
                    var PathS = Path.ToLower() + ";";
                    PathS.ToLower();
                    if (PathS.IndexOf(dllPath.ToLower() + ";") < 0)
                    {
                        SetEnvironmentVariable("PATH", dllPath + ";" + Path);
                    }
                }
            }
            return returnValue;
        }

        //Init
    }
}