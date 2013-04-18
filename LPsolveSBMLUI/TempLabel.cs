using System;
using System.Collections;
using System.Windows.Forms;
using System.IO;
using LPsolveSBML;
using SBMLExtension;
using AutoLayout;

namespace LPsolveSBMLUI
{
    public class TempLabel
    {
        private SBMLExtension.LayoutExtension.TextGlyph _TextGlyph;
        public SBMLExtension.LayoutExtension.TextGlyph TextGlyph
        {
            get { return _TextGlyph; }
            set { _TextGlyph = value; }
        }

        private SBMLExtension.EmlRenderExtension.Text _TextStyle;
        public SBMLExtension.EmlRenderExtension.Text TextStyle
        {
            get { return _TextStyle; }
            set { _TextStyle = value; }
        }

        private SBMLExtension.EmlRenderExtension.LocalStyle _LocalStyle;
        public SBMLExtension.EmlRenderExtension.LocalStyle LocalStyle
        {
            get { return _LocalStyle; }
            set { _LocalStyle = value; }
        }

        public TempLabel()
        {

        }

        /// <summary>
        /// Initializes a new instance of the TempLabel class.
        /// </summary>
        /// <param name="textGlyph"></param>
        /// <param name="textStyle"></param>
        /// <param name="localStyle"></param>
        public TempLabel(SBMLExtension.LayoutExtension.TextGlyph textGlyph, SBMLExtension.EmlRenderExtension.Text textStyle, SBMLExtension.EmlRenderExtension.LocalStyle localStyle)
        {
            _TextGlyph = textGlyph;
            _TextStyle = textStyle;
            _LocalStyle = localStyle;
        }
    }
}
