using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tez_v1
{
    class UAV
    {
            public Double Airspeed, PAltitude, GPSAltitude, Heading, Pitch, Roll;

            public double lat, lon, alt;

            public Point HaritaPozisyon;

            public Double MainBattery, ServBattery;

            public Image Simge;

            public UAV()
            {
                Airspeed = 0.0;
                PAltitude = 0.0;
                GPSAltitude = 0.0;
                Heading = 0.0;
                Pitch = 0.0;
                Roll = 0.0;
            }


        }
}
