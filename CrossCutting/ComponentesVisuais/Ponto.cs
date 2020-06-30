using CG_Biblioteca;
using CrossCutting.ComponentesVisuais;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace CrossCutting
{
    public class Ponto : ComponenteVisual<Ponto>, IComponenteVisual<Ponto>
    {
        public void Create(Ponto4D point)
        {
            Create((point.X.ToInt(), point.Y.ToInt()));
        }

        public void Create((int, int) point)
        {
            if (UseColor)
                GL.Color3(Color);
            else
                GL.Color3(Red, Green, Blue);

            var contexto = PontoContext.CriaInstancia();
            contexto.Begin(Size);
            GL.Vertex2(point.Item1, point.Item2);
            contexto.End();
        }

        public Ponto WithColor(Color color)
        {
            this.SetColor(color);

            return this;
        }

        public Ponto WithColor(double red, double green, double blue)
        {
            this.SetColor(red, green, blue);

            return this;
        }

        public Ponto WithSize(float size)
        {
            this.SetSize(size);

            return this;
        }
    }
}
