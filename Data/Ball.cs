﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Ball
    {

        public double XCoordinate { get; set; }
        public double YCoordinate { get; set; }
        public double Speed { get; private set; }
        public double Radius { get; private set; }
        public double DestinationPlaneX { get; private set; }
        public double DestinationPlaneY { get; private set; }

        public Ball(double XCoordinate, double YCoordinate, double Speed, double Radius, double DestinationPlaneX, double DestinationPlaneY)
        {
            this.Radius = Radius;
            this.XCoordinate = XCoordinate;
            this.YCoordinate = YCoordinate;
            this.Speed = Speed;
            this.DestinationPlaneX = DestinationPlaneX;
            this.DestinationPlaneY = DestinationPlaneY;
        }
    }
}