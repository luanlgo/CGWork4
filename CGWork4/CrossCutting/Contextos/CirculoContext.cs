using OpenTK.Graphics.OpenGL;

namespace CrossCutting
{
    public class CirculoContext : ComponentContext<CirculoContext>
    {
        public override void Begin()
        {
            Begin(1);
        }

        public override void Begin(float size)
        {
            SetType(PrimitiveType.Points);
            
            base.Begin(size);
        }
    }
}
