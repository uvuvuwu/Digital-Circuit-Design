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
            //For every gate in the compound, save its current x position, y position
            //When mouse moves, x + x of new position, y + y of new position
            for (int j = 0; j < _gatesList.Count; j++)
            {
                int xPos = x + _gatesList[j].Left;
                int yPos = y + _gatesList[j].Top;
                _gatesList[j].MoveTo(xPos, yPos);
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
            }
        }

        /// <summary>
        /// Overrides the IsMouseOn method and checks each gate in the list
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>either true or false</returns>
        public override bool IsMouseOn(int x, int y)
        {
            //FOR each gate in the list, check if the mouse is on it and return a value
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
