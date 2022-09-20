using System;
using System.Collections.Generic;
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

        public void AddGate(Gate gate)
        {
            _gatesList.Add(gate);
        }
    }
}
