﻿using System;

namespace CrossCutting
{
    public static class Extencoes
    {
        public static int ToInt(this object value)
        {
            try
            {
                return Convert.ToInt32(value);
            }
            catch
            {
                return 0;
            }
        }
    }
}
