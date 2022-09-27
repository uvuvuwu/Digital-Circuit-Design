using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Circuits
{
    /// <summary>
    /// The main GUI for the COMP104 digital circuits editor.
    /// This has a toolbar, containing buttons called buttonAnd, buttonOr, etc.
    /// The contents of the circuit are drawn directly onto the form.
    /// 
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// The (x,y) mouse position of the last MouseDown event.
        /// </summary>
        protected int startX, startY;

        /// <summary>
        /// If this is non-null, we are inserting a wire by
        /// dragging the mouse from startPin to some output Pin.
        /// </summary>
        protected Pin startPin = null;

        /// <summary>
        /// The (x,y) position of the current gate, just before we started dragging it.
        /// </summary>
        protected int currentX, currentY;

        /// <summary>
        /// The set of gates in the circuit
        /// </summary>
        protected List<Gate> gatesList = new List<Gate>();

        /// <summary>
        /// The set of connector wires in the circuit
        /// </summary>
        protected List<Wire> wiresList = new List<Wire>();

        /// <summary>
        /// The currently selected gate, or null if no gate is selected.
        /// </summary>
        protected Gate current = null;

        /// <summary>
        /// The new gate that is about to be inserted into the circuit
        /// </summary>
        protected Gate newGate = null;

        /// <summary>
        /// New compound gate 
        /// </summary>
        protected Compound newCompound = null;

        protected Form1 newForm = null;

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        /// <summary>
        /// Handles all events when a mouse is clicked in the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (current != null)
            {
                //If current gate is a compound gate
                if(current is Compound)
                {
                    Compound c = (Compound)current;
                    //Set select for all gates in the compound gate's gateslist to false
                    foreach (Gate gate in c._gatesList)
                    {
                        gate.Selected = false;
                    }
                }
                current.Selected = false;
                current = null;
                this.Invalidate();
            }
            // See if we are inserting a new gate
            if (newGate != null)
            {
                newGate.MoveTo(e.X, e.Y);
                gatesList.Add(newGate);
                newGate = null;
                this.Invalidate();
            }
            else
            {
                // search for the first gate under the mouse position
                foreach (Gate g in gatesList)
                {
                    if (g.IsMouseOn(e.X, e.Y))
                    {
                        g.Selected = true;
                        if(g is Compound c)
                        {
                            foreach (Gate gate in c._gatesList)
                            {
                                gate.Selected = true;
                            }

                            //when you click on an inputsource in a compound it changes colour and T/F
                            for (int i = 0; i < c._gatesList.Count; i++)
                            {
                                if(c._gatesList[i] is InputSource)
                                {
                                    if (((InputSource)c._gatesList[i]).IsMouseOnCircle(e.X, e.Y))
                                    {
                                        if (((InputSource)c._gatesList[i]).Voltage == false)
                                        {
                                            ((InputSource)c._gatesList[i]).Voltage = true;
                                        }
                                        else if (((InputSource)c._gatesList[i]).Voltage == true)
                                        {
                                            ((InputSource)c._gatesList[i]).Voltage = false;
                                        }
                                    }
                                }
                            }
                        }
                        //Check if adding gate to a new compound gate
                        if (newForm != null && newForm.newCompound != null)
                        {
                            newForm.newCompound.AddGate(g);
                        }
                        current = g;
                        this.Invalidate();

                        //Check if the gate clicked on is InputSource
                        if (g is InputSource)
                        {
                            //If the mouse is on the circle, set voltage to opposite voltage, true to false, false to true
                            if (((InputSource)g).IsMouseOnCircle(e.X, e.Y))
                            {
                                if(((InputSource)g).Voltage == false)
                                {
                                    ((InputSource)g).Voltage = true;
                                }
                                else if(((InputSource)g).Voltage == true)
                                {
                                    ((InputSource)g).Voltage = false;
                                }
                            }
                        }

                        this.Invalidate();
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Handles events while the mouse button is pressed down.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (current == null)
            {
                // try to start adding a wire
                startPin = findPin(e.X, e.Y);
            }
            else if (current.IsMouseOn(e.X, e.Y))
            {
                // start dragging the current object around
                startX = e.X;
                startY = e.Y;
                currentX = current.Left;
                currentY = current.Top;
            }
        }

        /// <summary>
        /// Handles all events when the mouse is moving.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (startPin != null)
            {
                Console.WriteLine("wire from " + startPin + " to " + e.X + "," + e.Y);
                currentX = e.X;
                currentY = e.Y;
                this.Invalidate();  // this will draw the line
            }
            else if (startX >= 0 && startY >= 0 && current != null)
            {
                Console.WriteLine("mouse move to " + e.X + "," + e.Y);  
                current.MoveTo(currentX + (e.X - startX), currentY + (e.Y - startY));
                this.Invalidate();
            }
            else if (newGate != null)
            {
                currentX = e.X;
                currentY = e.Y;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Handles all events when the mouse button is released.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (startPin != null)
            {
                // see if we can insert a wire
                Pin endPin = findPin(e.X, e.Y);
                if (endPin != null)
                {
                    Console.WriteLine("Trying to connect " + startPin + " to " + endPin);
                    Pin input, output;
                    if (startPin.IsOutput)
                    {
                        input = endPin;
                        output = startPin;
                    }
                    else
                    {
                        input = startPin;
                        output = endPin;
                    }
                    if (input.IsInput && output.IsOutput)
                    {
                        if (input.InputWire == null)
                        {
                            Wire newWire = new Wire(output, input);
                            input.InputWire = newWire;
                            wiresList.Add(newWire);
                        }
                        else
                        {
                            MessageBox.Show("That input is already used.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error: you must connect an output pin to an input pin.");
                    }
                }
                startPin = null;
                this.Invalidate();
            }
            // We have finished moving/dragging
            startX = -1;
            startY = -1;
            currentX = 0;
            currentY = 0;
        }

        /// <summary>
        /// This will create a new AndGate.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonAnd_Click(object sender, EventArgs e)
        {
            newGate = new AndGate(0, 0);
        }

        /// <summary>
        /// This will create a new OrGate.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            newGate = new OrGate(0, 0);
        }

        /// <summary>
        /// This will create a new NotGate.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            newGate = new NotGate(0, 0);
        }

        /// <summary>
        /// Creates a new Input icon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonInput_Click(object sender, EventArgs e)
        {
            newGate = new InputSource(false, 0, 0);
        }

        /// <summary>
        /// Creates a new Output lamp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonOutput_Click(object sender, EventArgs e)
        {
            newGate = new OutputLamp(false, 0, 0);
        }

        /// <summary>
        /// Calls Evaluate method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonEvaluate_Click(object sender, EventArgs e)
        {
            foreach(Gate gate in gatesList)
            {
                if(gate is OutputLamp)
                {
                    gate.Evaluate();
                }
                this.Invalidate();
                
            }
        }

        /// <summary>
        /// Copy button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            //If a gate is selected
            if(current != null)
            {
                //Clone the currently selected gate
                newGate = current.Clone();
                if(current is Compound)
                {
                    Compound compound = (Compound)current;
                    Compound compoundCopy = (Compound)newGate;
                    wiresList.AddRange(compound.CopyWires(compoundCopy));
                }
            }
        }

        /// <summary>
        /// Starts a group/compound gate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            newForm = new Form1();
            newForm.newCompound = new Compound(0, 0);
        }

        /// <summary>
        /// Ends making of a compound gate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonEndGroup_Click(object sender, EventArgs e)
        {
            //If newForm is not null, end making compound gate
            if (newForm != null)
            {
                //Add new compound gate to gates list
                gatesList.Add(newForm.newCompound);
                //Set new form new compound to null
                newForm.newCompound = null;
                current = gatesList[gatesList.Count - 1];
                //Remove the gates in gates list that are now instead in the compound
                Compound c = current as Compound;
                foreach (Gate g in c._gatesList)
                {
                    gatesList.Remove(g);
                }
            }
            //Else do nothing
            else
            {

            }
        }



        /// <summary>
        /// Finds the pin that is close to (x,y), or returns
        /// null if there are no pins close to the position.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Pin findPin(int x, int y)
        {
            foreach (Gate g in gatesList)
            {
                foreach (Pin p in g.Pins)
                {
                    if (p.isMouseOn(x, y))
                        return p;
                }
            }
            return null;
        }

        /// <summary>
        /// Redraws all the graphics for the current circuit.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //Draw all of the gates
            foreach (Gate g in gatesList)
            {
                g.Draw(e.Graphics);
            }
            //Draw all of the wires
            foreach (Wire w in wiresList)
            {
                w.Draw(e.Graphics);
            }

            if (startPin != null)
            {
                e.Graphics.DrawLine(Pens.White,
                    startPin.X, startPin.Y,
                    currentX, currentY);
            }
            if (newGate != null)
            {
                // show the gate that we are dragging into the circuit
                newGate.MoveTo(currentX, currentY);
                newGate.Draw(e.Graphics);
            }
        }

        
    }
}
