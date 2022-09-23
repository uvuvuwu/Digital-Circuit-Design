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
        public int topmost;
        public int leftmost;

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
                gate.MoveTo(x + (gate.Left - Left), y + (gate.Top - Top));
            }

            ////Going through gates list, checking the top most position and left most position, adding them to variables
            //for (int i = 0; i < _gatesList.Count - 1; i++)
            //{
            //    if (_gatesList[i].Left >= _gatesList[i + 1].Left)
            //    {
            //        leftmost = _gatesList[i].Left;
            //    }
            //    if (_gatesList[i].Top >= _gatesList[i + 1].Top)
            //    {
            //        topmost = _gatesList[i].Top;
            //    }
            //}

            //int xPos = 0;
            //int yPos = 0;
            //int xPos1 = 0;
            //int yPos1 = 0;
            ////For every gate in the compound, save its current x position, y position
            ////When mouse moves, x + x of new position, y + y of new position
            //for (int j = 0; j < _gatesList.Count; j++)
            //{
            //    //Get the difference between mouse position and left of compound
            //    xPos = x - leftmost;
            //    //Get the difference between mouse position and top of compound
            //    yPos = y - topmost;
            //    xPos1 = xPos + x;
            //    yPos1 = yPos + y;
            //    left = xPos1;
            //    top = yPos1;
            //    _gatesList[j].Left = xPos1;
            //    _gatesList[j].Top = yPos1;
            //}

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
            Left = _gatesList[0].Left;
            Top = _gatesList[0].Top;

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
