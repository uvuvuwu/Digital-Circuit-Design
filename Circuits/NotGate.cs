using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Circuits
{
    public class NotGate : Gate
    {
        public NotGate(int x, int y) : base(x, y)
        {
            //Add the two input pins to the gate
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
                paper.DrawImage(Properties.Resources.NotGateAllRed, Left, Top);
            }
            else
            {
                paper.DrawImage(Properties.Resources.NotGate, Left, Top);
            }
        }

        public override void MoveTo(int x, int y)
        {
            //Debugging message
            Console.WriteLine("pins = " + pins.Count);
            //Set the position of the gate to the values passed in
            left = x;
            top = y;
            // must move the pins too
            pins[0].X = x - GAP;
            pins[0].Y = y + 50 / 2;
            //pins[1].X = x - GAP;
            //pins[1].Y = y + HEIGHT - GAP;
            pins[1].X = x + WIDTH + GAP + 10;
            pins[1].Y = y + 50 / 2;
        }

        public override void Evaluate()
        {
        }
    }
}
