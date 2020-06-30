﻿using System;

namespace Questao3
{
    static class Program
    {
        static void Main(string[] args)
        {
            try
            {
                new World(600, 600)
                    .Run(1.0 / 60.0);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
