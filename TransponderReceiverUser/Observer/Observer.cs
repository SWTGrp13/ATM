﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TransponderReceiverUser.Observer
{
    public interface IObserver
    {
        void Update(string tag);
    }

    public class Observer : IObserver
    {
        public void AddSubject(subject plane)
        {
            plane.Attach(this);
        }

        public void Update(string tag)
        {
            foreach (var data in tag)
            {
                System.Console.WriteLine("{data}");
            }
            
        }
    }
}
