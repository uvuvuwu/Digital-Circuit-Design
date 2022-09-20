using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circuits
{
    public class InputSource : Gate
    {
        protected bool _voltage;

        public InputSource(bool voltage, int x, int y) : base (x, y)
        {
            _voltage = voltage;
            //Add the output pin to the gate
            pins.Add(new Pin(this, false, 20));
            //move the gate and the pins to the position passed in
            MoveTo(x, y);
        }

        /// <summary>
        /// Gets and sets the Voltage
        /// </summary>
        public bool Voltage
        { 
            get { return _voltage; } 
            set { _voltage = value; }
        }

        /// <summary>
        /// Draws the gate and the pins, different colours if selected
        /// </summary>
        /// <param name="paper"></param>
        public override void Draw(Graphics paper)
        {
            Brush brush;
            //Draw each of the pins
            foreach (Pin p in pins)
                p.Draw(paper);

            //Check if the gate has been selected
            if (selected)
            {
                brush = selectedBrush;
                paper.FillRectangle(brush, left, top, WIDTH - 10, HEIGHT - 10);

            }
            else
            {
                brush = normalBrush;
                paper.FillRectangle(brush, left, top, WIDTH - 10, HEIGHT - 10);
            }
            
            
            SolidBrush brush1 = new SolidBrush(Color.Black);
            
            //If Voltage is true, fill circle in yellow
            if(Voltage)
            {
                brush1.Color = Color.Yellow;
                paper.FillEllipse(brush1, left + 2, top + 2, WIDTH - 15, HEIGHT - 15);
            }
            //Else voltage is false, fill circle in black
            else
            {
                brush1.Color = Color.Black;
                paper.FillEllipse(brush1, left + 2, top + 2, WIDTH - 15, HEIGHT - 15);
            }

            
        }

        /// <summary>
        /// to move the entire gate + pins to another position when selected.
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
            //must move the pins too
            pins[0].X = x + WIDTH - GAP / 2;
            pins[0].Y = y + (HEIGHT - 10) / 2;
        }

        /// <summary>
        /// 
        /// </summary>
        public override bool Evaluate()
        {
            return Voltage;
        }

        /// <summary>
        /// Checks if the mouse is on the circle
        /// </summary>
        /// <param name="mouseX">X position of mouse</param>
        /// <param name="mouseY">Y position of mouse</param>
        /// <returns></returns>
        public bool IsMouseOnCircle(int mouseX, int mouseY)
        {
            int centreX = left + (WIDTH - 5) / 2;
            int centreY = top + (HEIGHT - 5) / 2;
            int radius = (WIDTH - 5) / 2;

            //Calculates the distance of the mouse position to the radius
            int distance = (int)Math.Sqrt(Math.Pow((mouseX - centreX), 2) + Math.Pow((mouseY - centreY), 2));

            //if the distance is smaller than the radius, then the mouse is inside the circle
            if (distance < radius)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override Gate Clone()
        {
            InputSource cloneInputSource = new InputSource(_voltage, _x, _y); 
            return cloneInputSource;
        }

    }
}
