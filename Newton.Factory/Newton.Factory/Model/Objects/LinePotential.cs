using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Factory.Model.Objects
{
    public class LinePotential
    {
        private Line line;

        public Line Line
        {
            get { return line; }
            set { line = value; }
        }

        private Rate currentOutput;

        public Rate CurrentOutput
        {
            get { return currentOutput; }
            set { currentOutput = value; }
        }
    }
}
