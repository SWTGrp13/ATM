﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace TransponderReceiverUser.Calculations
{
    public class Calculate
    {
        public double FindAngle(double x1, double y1)
        {
            return 0;
        }

        public double FindAngle(double x1, double y1, double x2, double y2)
        {
            var y = y2 - y1;
            var x = x2 - x1;
            var radius = Math.Atan2(y, x);
            var degree = radius * (180.0 / Math.PI);
            degree = (degree > 0.0 ? degree : 360.0 + degree);
                if (degree == 360)
                    return 0.0;
            return degree;
        }

        public double CalculateCourse(double x, double y)
        {
            


            return 0;
        }

    }
}

