﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Circuits
{
    public abstract class Gate
    {
        // left is the left-hand edge of the main part of the gate.
        // So the input pins are further left than left.
        protected int left;

        // top is the top of the whole gate
        protected int top;

        // width and height of the main part of the gate
        protected const int WIDTH = 40;
        protected const int HEIGHT = 40;
        // length of the connector legs sticking out left and right
        protected const int GAP = 10;

        protected Brush selectedBrush = Brushes.Red;
        protected Brush normalBrush = Brushes.LightGray;

        //Store x and y
        protected int _x;
        protected int _y;


        /// <summary>
        /// This is the list of all the pins of this gate.
        /// An AND gate always has two input pins (0 and 1)
        /// and one output pin (number 2).
        /// </summary>
        protected List<Pin> pins = new List<Pin>();
        //Has the gate been selected
        protected bool selected = false;

        

        /// <summary>
        /// Initialises the gate
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Gate(int x, int y)
        {
            _x = x;
            _y = y;
        }

        /// <summary>
        /// Gets and sets whether the gate is selected or not.
        /// </summary>
        public virtual bool Selected
        {
            get { return selected; }
            set { selected = value; }
        }

        /// <summary>
        /// Gets the left hand edge of the gate.
        /// </summary>
        public virtual int Left
        {
            get { return left; }
            set { left = value; }
        }

        /// <summary>
        /// Gets the top edge of the gate.
        /// </summary>
        public virtual int Top
        {
            get { return top; }
            set { top = value; }
        }

        /// <summary>
        /// Gets the list of pins for the gate.
        /// </summary>
        public virtual List<Pin> Pins
        {
            get { return pins; }
        }

        /// <summary>
        /// Checks if the gate has been clicked on.
        /// </summary>
        /// <param name="x">The x position of the mouse click</param>
        /// <param name="y">The y position of the mouse click</param>
        /// <returns>True if the mouse click position is inside the gate</returns>
        public virtual bool IsMouseOn(int x, int y)
        {
            if (left <= x && x < left + WIDTH
                && top <= y && y < top + HEIGHT)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Draws the gate in the normal colour or in the selected colour.
        /// </summary>
        /// <param name="paper"></param>
        public abstract void Draw(Graphics paper);

        /// <summary>
        /// Moves the gate to the position specified.
        /// </summary>
        /// <param name="x">The x position to move the gate to</param>
        /// <param name="y">The y position to move the gate to</param>
        public virtual void MoveTo(int x, int y)
        {
            //Debugging message
            Console.WriteLine("pins = " + pins.Count);
            //Set the position of the gate to the values passed in
            left = x;
            top = y;
            // must move the pins too
            pins[0].X = x - GAP;
            pins[0].Y = y + GAP;
            pins[1].X = x - GAP;
            pins[1].Y = y + HEIGHT - GAP;
            pins[2].X = x + WIDTH + GAP;
            pins[2].Y = y + HEIGHT / 2;
        }

        /// <summary>
        /// This is the abstract Evaluate method that every subclass inherits to implement each of its own Evaluations
        /// </summary>
        /// <returns></returns>
        public abstract bool Evaluate();

        /// <summary>
        /// The abstract Clone method which every gate inherits to clone the selected gate
        /// </summary>
        /// <returns></returns>
        public abstract Gate Clone();
    }
}
