using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace TransponderReceiverUser.Calculations
{
    public static class Calculate
    {
        public static double FindDegree(Plane plane)
        {
            var y = plane.YPos - plane.OldYPos;
            var x = plane.XPos - plane.OldXPos;
            var radians = Math.Atan2(y, x);
            var degree = radians * (180.0 / Math.PI);
            degree =- 90;
            if (degree < 0)
                degree += 360;
            degree = (degree > 0.0 ? degree : 360.0 + degree);
            if (degree == 360)
                return 0.0;
            return Convert.ToInt32(degree);
        }
        
        

    }
}

