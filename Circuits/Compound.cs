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

        //A gates list for compound gate
        public List<Gate> _gatesList; 

        /// <summary>
        /// Initialises Compound gate
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Compound (int x, int y) : base (x, y)
        {
            //Makes a new gates list for the compound
            _gatesList = new List<Gate>();
        }

        /// <summary>
        /// Moving the whole compound object to another location. Is this right???
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public override void MoveTo(int x, int y)
        {
            //Foreach gate in gates list, call MoveTo method
            foreach(Gate gate in _gatesList)
            {
                //Move everything relative to the first gate in the gates list, so that every gate stays in the same relative position
                gate.MoveTo(x + (gate.Left - Left), y + (gate.Top - Top));
            }

        }

        /// <summary>
        /// This is just the normal Selected property, I was going to override to do something else with it.
        /// </summary>
        public override bool Selected 
        {
            get { return selected; }
            set { selected = value; }
        }

        /// <summary>
        /// Evaluates compound gate
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override bool Evaluate()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Clones compound gate
        /// </summary>
        /// <returns></returns>
        public override Gate Clone()
        {
            //Make a new compound
            Compound compound = new Compound(_x, _y);
            //For each gate in the current compound, clone the gates, add the gates to new compound gates list
            for(int i = 0; i < _gatesList.Count; i++)
            {
                compound._gatesList.Add(_gatesList[i].Clone());
                //Set the left and top of the new compound to be the same as the original one to keep its distances
                compound._gatesList[i].Left = _gatesList[i].Left;
                compound._gatesList[i].Top = _gatesList[i].Top;
            }

            //Trying to clone the pins and wires

            //Looping through gatesList
            for(int i = 0; i < _gatesList.Count; i++)
            {
                //Looping through pins
                for(int k = 0; k < _gatesList[i].Pins.Count; k++)
                {
                    //If pin exists, and is input wire
                    if(_gatesList[i].Pins[k].InputWire != null)
                    {
                        for(int j = 0; j < _gatesList.Count; j++)
                        {
                            //Finding the owner of the gate that the input wire came from
                            if(_gatesList[i].Pins[k].InputWire.FromPin.Owner == _gatesList[j])
                            {
                                //Loop through gatesList to find the gate that the pin belongs to in gatesList[j], use that pin to connect
                                //Put the found pin in place of 'Pin from' in the new Wire
                                for(int l = 0; l < _gatesList.Count; l++)
                                {
                                    if ()
                                    {
                                        
                                    }
                                }
                                //Make a new wire for the cloning of the compound as you can't "clone" a wire so 
                                //cloning here is done manually
                                Wire wire = new Wire(compound._gatesList[j].Pins[k], compound._gatesList[i].Pins[k]);
                            }
                        }
                    }
                }
            }
            //Return cloned compound
            return compound;
        }

        /// <summary>
        /// Adds a gate to the list.
        /// </summary>
        /// <param name="gate"></param>
        public void AddGate(Gate gate)
        {
            _gatesList.Add(gate);
        }

        /// <summary>
        /// Draws the gate
        /// </summary>
        /// <param name="paper"></param>
        public override void Draw(Graphics paper)
        {
            //Set the "Left" and "Top" to the left and top of the first gate in the gates list, for the MoveTo method
            //The draw method gets called earlier than MoveTo method, so better for the code to be here.
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
