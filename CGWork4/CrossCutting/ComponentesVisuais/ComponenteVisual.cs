using System.Drawing;

namespace CrossCutting
{
    public abstract class ComponenteVisual<T> where T : new()
    {
        protected bool UseColor { get; set; }
        public Color Color { get; protected set; }
        public float Size { get; protected set; }
        public double Red { get; protected set; }
        public double Green { get; protected set; }
        public double Blue { get; protected set; }

        public static T CriaInstancia()
        {
            return new T();
        }

        protected virtual ComponenteVisual<T> SetColor(Color color)
        {
            Color = color;
            UseColor = true;
            return this;
        }

        protected virtual ComponenteVisual<T> SetColor(double red, double green, double blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
            UseColor = false;
            return this;
        }

        protected virtual ComponenteVisual<T> SetSize(float size)
        {
            Size = size;
            return this;
        }
    }
}
