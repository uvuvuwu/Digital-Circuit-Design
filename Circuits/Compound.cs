using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circuits
{
    public class Compound : Gate
    {
        public List<Gate> _gatesList; 

        public Compound (int x, int y) : base (x, y)
        {
            _gatesList = new List<Gate>();
        }

        /// <summary>
        /// Moving the whole compound object to another location. Is this right???
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public override void MoveTo(int x, int y)
        {
            foreach(Gate gate in _gatesList)
            {
                gate.MoveTo(x, y);
            }
        }
        
        
        public override bool Evaluate()
        {
            throw new NotImplementedException();
        }

        public override Gate Clone()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a gate to the list.
        /// </summary>
        /// <param name="gate"></param>
        public void AddGate(Gate gate)
        {
            _gatesList.Add(gate);
        }

        public override void Draw(Graphics paper)
        {
            //throw new NotImplementedException();
            //loop through the gates list to draw the gates in the compound group
            foreach (Gate gate in _gatesList)
            {
                gate.Draw(paper);

            }
        }
    }
}
