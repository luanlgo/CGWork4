using CG_Biblioteca;
using CrossCutting.ComponentesVisuais;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;

namespace CrossCutting
{
    public class Circulo : ComponenteVisual<Circulo>, IComponenteVisual<Circulo>
    {
        private const int Graus = 360;

        public void Criar(double radius, int amountPoints)
        {
            Create(radius, amountPoints, (0, 0));
        }

        public void Create(double radius, int amountPoints, (double, double) center)
        {
            if (amountPoints < 1)
                throw new ArgumentException("A quantidade de pontos não deve ser menor ou igual a zero.", nameof(amountPoints));

            int pause = Graus / amountPoints;

            if (UseColor)
                GL.Color3(Color);
            else
                GL.Color3(Red, Green, Blue);

            var contexto = CirculoContext.CriaInstancia();
            contexto.Begin(Size);

            for (double i = 0; i < Graus; i += pause)
                CreatePoint(i, radius, center);

            contexto.End();
        }

        public void CreateRadiusLine(double angle, double radius, (int, int) point)
        {
            var contexto = SegmentodeRetaContext.CriaInstancia();
            contexto.Begin(Size);

            var a = Calculos.GerarPontosCirculo(angle, radius, point.Item1, point.Item2);
            SegmentoDeLinha.CriaInstancia()
                       .WithColor(Color)
                       .Create((point.Item1, point.Item2), (a.X.ToInt(), a.Y.ToInt()));

            contexto.End();
        }

        private void CreatePoint(double ponta, double radius, (double, double) center)
        {
            var a = Calculos.GerarPontosCirculo(ponta, radius, center.Item1, center.Item2);
            GL.Vertex2(a.X, a.Y);
        }

        public static bool isInside(double radius, int amountPoints, Ponto4D check, int quem)
        {
            if (amountPoints < 1)
                throw new ArgumentException("A quantidade de pontos não deve ser menor ou igual a zero.", nameof(amountPoints));

            int pause = Graus / amountPoints;

            bool pego = false;

            for (double i = 0; i < Graus; i += pause)
            {
                if (quem == 0 && ((i >= 0 && i <= 45) || (i >= 315 && i <= 360)))
                {
                    var a = Calculos.GerarPontosCirculo(i, radius, 0, 0);

                    if (check.X < a.X && ((check.Y >= a.Y && check.Y <= 0) || (check.Y <= a.Y && check.Y >= 0)))
                    {
                        pego = true;
                    }
                }

                if (quem == 1 && (i >= 135 && i <= 220))
                {
                    var a = Calculos.GerarPontosCirculo(i, radius, 0, 0);

                    if (check.X > a.X && ((check.Y >= a.Y && check.Y <= 0) || (check.Y <= a.Y && check.Y >= 0)))
                    {
                        pego = true;
                    }
                }

                if (quem == 2 && (i >= 220 && i <= 315))
                {
                    var a = Calculos.GerarPontosCirculo(i, radius, 0, 0);

                    if (check.Y > a.Y && ((check.X >= a.X && check.X <= 0) || (check.X <= a.X && check.X >= 0)))
                    {
                        pego = true;
                    }
                }

                if (quem == 3 && (i >= 45 && i <= 135))
                {
                    var a = Calculos.GerarPontosCirculo(i, radius, 0, 0);

                    if (check.Y < a.Y && ((check.X >= a.X && check.X <= 0) || (check.X <= a.X && check.X >= 0)))
                    {
                        pego = true;
                    }
                }

            }

            return !pego;
        }

        public Circulo WithColor(Color color)
        {
            SetColor(color);

            return this;
        }

        public Circulo WithColor(double red, double green, double blue)
        {
            SetColor(red, green, blue);

            return this;
        }

        public Circulo WithSize(float size)
        {
            SetSize(size);

            return this;
        }
    }
}
