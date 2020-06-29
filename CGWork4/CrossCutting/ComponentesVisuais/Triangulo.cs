using CrossCutting.ComponentesVisuais;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace CrossCutting
{
    public class Triangulo : ComponenteVisual<Triangulo>, IComponenteVisual<Triangulo>
    {
        public void Create((int, int) pointOne, (int, int) pointTwo, (int, int) pointThree)
        {
            var contexto = SegmentodeRetaContext.CriaInstancia();
            contexto.Begin(Size);
            CriarLinha(pointThree, pointOne);
            CriarLinha(pointOne, pointTwo);
            CriarLinha(pointTwo, pointThree);
            contexto.End();
        }

        private void CriarLinha((int, int) pointOne, (int, int) pointTwo)
        {
            var linha = SegmentoDeLinha.CriaInstancia();
            if (UseColor)
                linha.WithColor(Color);
            else
                linha.WithColor(Red, Green, Blue);
            linha.Create(pointOne, pointTwo);
        }

        public Triangulo WithColor(Color color)
        {
            this.SetColor(color);

            return this;
        }

        public Triangulo WithColor(double red, double green, double blue)
        {
            this.SetColor(red, green, blue);

            return this;
        }

        public Triangulo WithSize(float size)
        {
            this.SetSize(size);

            return this;
        }
    }
}
