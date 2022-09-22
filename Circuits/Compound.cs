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
            //for (int i = 0; i < _gatesList.Count - 1; i++)
            //{

            //    //Check which gate in compound gate is the most left, then set the most left to the compound gate's left
            //    if (_gatesList[i].Left < _gatesList[i + 1].Left)
            //    {
            //        left = _gatesList[i].Left;
            //    }
            //    //Check which gate in the compound gate is the most top, then set the most top to the compound gate's top
            //    if (_gatesList[i].Top < _gatesList[i + 1].Top)
            //    {
            //        top = _gatesList[i].Top;
            //    }
            //}

            //For every gate in the compound, save its current x position, y position
            //When mouse moves, x + x of new position, y + y of new position
            List<int> xGatePos = new List<int>();
            List<int> yGatePos = new List<int>();
            for (int j = 0; j < _gatesList.Count; j++)
            {
                xGatePos.Add (_gatesList[j].Left);
                yGatePos.Add (_gatesList[j].Top);
                
            }
            for (int k = 0; k < _gatesList.Count; k++)
            {
                _gatesList[k].MoveTo(x + _gatesList[k].Left, y + _gatesList[k].Top);
            }

        }


        public override bool Selected 
        {
            get { return selected; }
            set { selected = value; }
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
            //loop through the gates list to draw the gates in the compound group
            foreach (Gate gate in _gatesList)
            {
                gate.Draw(paper);

                //add toggle for selecting (different colours) so entire compound is selected
            }
        }

        public override bool IsMouseOn(int x, int y)
        {
            foreach(Gate g in _gatesList)
            {
                if (g.IsMouseOn(x, y))
                {
                    return true;
                }
            }
            return false;

        }
    }
}
