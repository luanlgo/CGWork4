using CG_Biblioteca;
using CGWork4;
using CrossCutting;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;

namespace Questao3
{
    public class World : CustomGameWindow
    {
        public int positionX, positionY, moviment;

        private readonly BolaPapel BolaPapel = new BolaPapel("C:/Users/55479/Desktop/Furb/CG/CGWork4/Resources/sun.bmp");


        int texture;

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

            CriarBola();
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

            //OnRenderPlanoCarteziano();

            //OnRenderLixeira();
            OnLoadBitMapBola();
            GL.PushMatrix();
            GL.MultMatrix(BolaPapel.ObterDados());

            DesenharBola(8); //Bolina

            this.SwapBuffers();
        }

        private void DesenharBola(int v)
        {
            DesenharEsfera(15, 38, 18, v);
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
                    MoveBola(ref positionY, true);
                    break;
                case Key.A:
                    MoveBola(ref positionX, false);
                    break;
                case Key.S:
                    MoveBola(ref positionY, false);
                    break;
                case Key.D:
                    MoveBola(ref positionX, true);
                    break;
                default:
                    break;
            }
        }

/*
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
        */

        private void CriarBola()
        {
            /*
            Transformacao4D bolaNovaPapel = new Transformacao4D();
            bolaNovaPapel.AtribuirEscala(0.3, 0.3, 0.3);
            bolaNovaPapel.AtribuirTranslacao(20, 0, 0);
            BolaPapel = bolaNovaPapel.MultiplicarMatriz(BolaPapel);
            */
        }

        private void OnLoadBitMapBola()
        {
            GL.GenTextures(1, out texture);
            GL.BindTexture(TextureTarget.Texture2D, texture);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            var bitMapBola = BolaPapel.BitMapBola;
            BitmapData dataMapBola = bitMapBola.LockBits(new Rectangle(0, 0, bitMapBola.Width, bitMapBola.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, dataMapBola.Width, dataMapBola.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, dataMapBola.Scan0);
            BolaPapel.UnlockBits(dataMapBola);
        }

        private void MoveBola(ref int position, bool direction)
        {
            position = direction ? position += moviment : position -= moviment;
        }

        public void DesenharEsfera(double radius, int slices, int stacks, int textu)
        {
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, textu);
            GL.Color3(Color.White);
            GL.Begin(PrimitiveType.Quads);
            double stack = (2 * Math.PI) / stacks;
            double slice = (2 * Math.PI) / slices;
            for (double theta = 0; theta < 2 * Math.PI; theta += stack)
            {
                for (double phi = 0; phi < 2 * Math.PI; phi += slice)
                {
                    Ponto4D p1 = GetPoints(phi, theta, radius);
                    Ponto4D p2 = GetPoints(phi + slice, theta, radius);
                    Ponto4D p3 = GetPoints(phi + slice, theta + stack, radius);
                    Ponto4D p4 = GetPoints(phi, theta + stack, radius);

                    double s0 = theta / (2 * Math.PI);
                    double s1 = (theta + stack) / (2 * Math.PI);
                    double t0 = phi / (2 * Math.PI);
                    double t1 = (phi + slice) / (2 * Math.PI);

                    GL.TexCoord2(s0, t0);
                    GL.Vertex3(p1.X, p1.Y, p1.Z);

                    GL.TexCoord2(s1, t0);
                    GL.Vertex3(p2.X, p2.Y, p2.Z);

                    GL.TexCoord2(s1, t1);
                    GL.Vertex3(p3.X, p3.Y, p3.Z);

                    GL.TexCoord2(s0, t1);
                    GL.Vertex3(p4.X, p4.Y, p4.Z);

                }
            }
            GL.End();
            GL.Disable(EnableCap.Texture2D);
        }

        Ponto4D GetPoints(double phi, double theta, double radius)
        {
            double x = radius * Math.Cos(theta) * Math.Sin(phi);
            double y = radius * Math.Sin(theta) * Math.Sin(phi);
            double z = radius * Math.Cos(phi);
            return new Ponto4D(x, y, z);
        }
    }
}
