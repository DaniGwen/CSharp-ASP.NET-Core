﻿using SIS.MvcFramework;
using System;
using System.Threading.Tasks;

namespace SharedTrip
{
    public static class Program
    {
        public static async Task Main()
        {
            WebHost.Start(new Startup());
        }
    }
}