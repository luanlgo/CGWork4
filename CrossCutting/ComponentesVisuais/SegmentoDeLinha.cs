using CrossCutting.ComponentesVisuais;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace CrossCutting
{
    public class SegmentoDeLinha : ComponenteVisual<SegmentoDeLinha>, IComponenteVisual<SegmentoDeLinha>
    {
        public void Create((double, double) pointOne, (double, double) pointTwo)
        {
            Create((pointOne.Item1.ToInt(), pointOne.Item2.ToInt()), (pointTwo.Item1.ToInt(), pointTwo.Item2.ToInt()));
        }

        public void Create((int, int) pointOne, (int, int) pointTwo)
        {
            if (UseColor)
                GL.Color3(Color);
            else
                GL.Color3(Red, Green, Blue);
            GL.LineWidth(Size);
            GL.Vertex2(pointOne.Item1, pointOne.Item2);
            GL.Vertex2(pointTwo.Item1, pointTwo.Item2);
        }

        public SegmentoDeLinha WithColor(Color color)
        {
            this.SetColor(color);

            return this;
        }

        public SegmentoDeLinha WithColor(double red, double green, double blue)
        {
            this.SetColor(red, green, blue);

            return this;
        }

        public SegmentoDeLinha WithSize(float size)
        {
            this.SetSize(size);

            return this;
        }
    }
}
