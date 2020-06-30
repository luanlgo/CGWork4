using OpenTK.Graphics.OpenGL;

namespace CrossCutting
{
    public class PontoContext : ComponentContext<PontoContext>
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
