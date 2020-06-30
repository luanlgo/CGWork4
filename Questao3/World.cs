using CG_Biblioteca;
using CrossCutting;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Drawing;

namespace Questao3
{
    public class World : CustomGameWindow
    {
        public int positionX, positionY, moviment;

        public World(int width, int height) : base(width, height)
        {
            positionY = 10;
            moviment = 5;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(Color.Gray);
            Camera.xmin = -300;
            Camera.xmax = 300;
            Camera.ymin = -300;
            Camera.ymax = 300;
            Camera.zmin = 0;
            Camera.zmax = 600;
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(Camera.xmin, Camera.xmax, Camera.ymin, Camera.ymax, Camera.zmin, Camera.zmax);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.MatrixMode(MatrixMode.Modelview);

            OnRenderPlanoCarteziano();
            OnRenderLixeira();

            Circulo.CriaInstancia()
                    .WithColor(Color.Black)
                    .WithSize(3)
                    .Create(10, 20, (positionX, positionY));

            this.SwapBuffers();
        }

        private void OnRenderPlanoCarteziano()
        {
            GL.Begin(PrimitiveType.Lines);
            GL.LineWidth(5);

            //vermelho
            GL.Color3(Color.Red);
            GL.Vertex2(0, 0);
            GL.Vertex2(200, 0);

            //verde
            GL.Color3(Color.Green);
            GL.LineWidth(5);
            GL.Vertex2(0, 0);
            GL.Vertex2(0, 200);

            GL.End();
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    Exit();
                    break;
                case Key.W:
                    Move(ref positionY, true);
                    break;
                case Key.A:
                    Move(ref positionX, false);
                    break;
                case Key.S:
                    Move(ref positionY, false);
                    break;
                case Key.D:
                    Move(ref positionX, true);
                    break;
                default:
                    break;
            }
        }

        private void OnRenderLixeira()
        {
            GL.Begin(PrimitiveType.Lines);
            GL.LineWidth(3);

            //vermelho
            GL.Color3(Color.White);
            GL.Vertex2(220, -220);
            GL.Vertex2(230, -250);

            GL.Vertex2(230, -250);
            GL.Vertex2(250, -250);

            GL.Vertex2(250, -250);
            GL.Vertex2(260, -220);
            
            GL.Vertex2(260, -220);
            GL.Vertex2(220, -220);
            
            GL.End();
        }

        private void Move(ref int position, bool direction)
        {
            position = direction ? position += moviment : position -= moviment;
        }
    }
}
