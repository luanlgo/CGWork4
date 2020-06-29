﻿using CG_Biblioteca;
using System;

namespace CrossCutting
{
    public static class Calculos
    {
        public static Ponto4D GerarPontosCirculo(double angulo, double raio) => GerarPontosCirculo(angulo, raio, 0, 0);

        public static Ponto4D GerarPontosCirculo(double angulo, double raio, double centerX, double centerY) => Matematica.GerarPtosCirculo(angulo, raio, centerX, centerY);

        public static double GerarPontosCirculoSimetrico(double raio) => raio * Math.Cos(Math.PI * 45 / 180.0);

        public static double GerarAngulo((int, int) c, (int, int) e) => (Math.Atan2(e.Item2 - c.Item2, (e.Item1 - c.Item1)) * 180) / Math.PI;

        public static double GerarDistancia((double, double) x, (double, double) y)
        {
            double deltaX = x.Item2 - x.Item1;
            double deltaY = y.Item2 - y.Item1;
            double deltaZ = 0;
            return Math.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ);
        }
    }
}
