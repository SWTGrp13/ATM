using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace TransponderReceiverUser.Calculations
{
    public class Calculate
    {
        public double CourseCalc(double y1, double y2, double x1, double x2)
        {
            double y = y2 - y1;
            double x = x2 - x1;
            double radius = Math.Atan2(y, x);
            double degree = radius * (180.0 / Math.PI);
            degree = (degree > 0.0 ? degree : 360.0 + degree);
            return degree;
        }
    }
}

