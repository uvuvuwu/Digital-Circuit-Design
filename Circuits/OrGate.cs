﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Circuits
{
    public class OrGate : Gate
    {
        public OrGate(int x, int y) : base(x, y)
        {
            //Add the two input pins to the gate
            pins.Add(new Pin(this, true, 20));
            pins.Add(new Pin(this, true, 20));
            //Add the output pin to the gate
            pins.Add(new Pin(this, false, 20));
            //move the gate and the pins to the position passed in
            MoveTo(x, y);
        }

        /// <summary>
        /// Draws the gate in the normal colour or in the selected colour.
        /// </summary>
        /// <param name="paper"></param>
        public override void Draw(Graphics paper)
        {
            //Draw each of the pins
            foreach (Pin p in pins)
                p.Draw(paper);

            //Check if the gate has been selected
            if (selected)
            {
                paper.DrawImage(Properties.Resources.OrGateAllRed, Left, Top);
            }
            else
            {
                paper.DrawImage(Properties.Resources.OrGate, Left, Top);
            }
        }

        /// <summary>
        /// To move the gate to another location
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public override void MoveTo(int x, int y)
        {
            //Debugging message
            Console.WriteLine("pins = " + pins.Count);
            //Set the position of the gate to the values passed in
            left = x;
            top = y;
            // must move the pins too
            pins[0].X = x - 5;
            pins[0].Y = y + GAP + 5;
            pins[1].X = x - 5;
            pins[1].Y = y + HEIGHT - 5;
            pins[2].X = x + WIDTH + GAP + 25;
            pins[2].Y = y + HEIGHT / 2 + 5;
        }

        /// <summary>
        /// Evaluates the input wires
        /// </summary>
        /// <returns>true if one of the input wires is true</returns>
        public override bool Evaluate()
        {
            //IF either input wire is true then return true, else return false
            if (Pins[0].InputWire.FromPin.Owner.Evaluate() == true || Pins[1].InputWire.FromPin.Owner.Evaluate() == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Clones the Or gate
        /// </summary>
        /// <returns></returns>
        public override Gate Clone()
        {
            OrGate cloneOrGate = new OrGate(_x, _y);
            return cloneOrGate;
        }
    }
}
