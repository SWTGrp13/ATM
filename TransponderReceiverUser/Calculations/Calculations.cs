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
  
        public double FindAngle(double x1, double y1, double x2, double y2)
        {
            var y = y2 - y1;
            var x = x2 - x1;
            //var radians = Math.Atan2(y, x);
            var radians = Math.Atan2(x, y);
            var degree = radians * (180.0 / Math.PI);
            //degree -= 90;
            if (degree < 0)
                degree += 360;
            degree = (degree > 0.0 ? degree : 360.0 + degree);
                if (degree == 360)
                    return 0.0;
            return degree;
        }
        
        

        //public double FindVelocity()
        //{

        //}

    }
}

