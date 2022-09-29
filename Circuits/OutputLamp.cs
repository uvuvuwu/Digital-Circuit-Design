using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        /// <summary>
        /// Gets and sets the voltage
        /// </summary>
        public bool Voltage
        {
            get { return _voltage; }
            set { _voltage = value; }
        }

        /// <summary>
        /// Draws the Output lamp and pins
        /// </summary>
        /// <param name="paper"></param>
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

        /// <summary>
        /// Moves the gate and pins to a new location
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
            pins[0].X = x - GAP;
            pins[0].Y = y + HEIGHT / 2;
        }

        /// <summary>
        /// Evaluates the input wire
        /// </summary>
        /// <returns>Voltage and bool value</returns>
        public override bool Evaluate()
        {
            //IF the input wire is true
            if (Pins[0].InputWire.FromPin.Owner.Evaluate() == true)
            {
                //voltage and value are true
                Voltage = true;
                return true;
                
            }
            else
            {
                //voltage and value are false
                Voltage = false;
                return false;
            }
            
        }

        /// <summary>
        /// Clones the Output lamp
        /// </summary>
        /// <returns></returns>
        public override Gate Clone()
        {
            OutputLamp cloneOutputLamp = new OutputLamp(_voltage, _x, _y);
            return cloneOutputLamp;
        }
    }
}
