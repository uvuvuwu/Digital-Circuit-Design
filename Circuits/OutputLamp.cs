using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circuits
{
    public class OutputLamp : Gate
    {
        protected bool _voltage;

        public OutputLamp(bool voltage, int x, int y) : base(x, y)
        {
            _voltage = voltage;
            //Add the output pin to the gate
            pins.Add(new Pin(this, true, 20));
            //move the gate and the pins to the position passed in
            MoveTo(x, y);
        }

        public bool Voltage
        {
            get { return _voltage; }
            //ask what value is!!
            set { _voltage = value; }
        }

        public override void Draw(Graphics paper)
        {
            //Draw each of the pins
            foreach (Pin p in pins)
                p.Draw(paper);
            Brush brush;
            //Check if the gate has been selected
            if (selected)
            {
                brush = selectedBrush;
                paper.FillRectangle(brush, left, top, WIDTH, HEIGHT);

            }
            else
            {
                brush = normalBrush;
                paper.FillRectangle(brush, left, top, WIDTH, HEIGHT);
            }

            SolidBrush brush1 = new SolidBrush(Color.Black);

            //If Voltage is true, fill circle in yellow
            if (Voltage)
            {
                brush1.Color = Color.Yellow;
                paper.FillEllipse(brush1, left + 2, top + 2, WIDTH - 5, HEIGHT - 5);
            }
            //Else voltage is false, fill circle in black
            else
            {
                brush1.Color = Color.Black;
                paper.FillEllipse(brush1, left + 2, top + 2, WIDTH - 5, HEIGHT - 5);
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
            pins[0].Y = y + HEIGHT / 2;
        }

        public override bool Evaluate()
        {
            if (Pins[0].InputWire.FromPin.Owner.Evaluate() == true)
            {
                Voltage = true;
                return true;
            }
            else
            {
                Voltage = false;
                return false;
            }
        }

        public override Gate Clone()
        {
            OutputLamp cloneOutputLamp = new OutputLamp(_voltage, _x, _y);
            return cloneOutputLamp;
        }
    }
}
