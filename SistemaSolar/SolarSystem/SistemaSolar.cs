using CG_Biblioteca;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using SistemaSolar;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;

namespace SolarSystem
{
    public class SistemaSolar : GameWindow
    {
        int textura;

        //Camera
        private Vector3 Visao { get; set; }
        private Vector3 Target { get; set; }
        private float Longe { get; set; }
        private float Perto { get; set; }

        private Estrela Sol { get; set; }
        private Estrela Terra { get; set; }
        private Transformacao4D ReferenciaTransacao { get; set; } = new Transformacao4D();

        private double Velocidade { get; set; }
        private bool iluminacao = true;

        public SistemaSolar(int width, int height) : base(width, height)
        {
            Velocidade = 0.01;
            Sol = new Estrela("C:/Users/55479/Desktop/Furb/CG/TrabalhoFinal/CGWork4/SistemaSolar/SolarSystem/Resources/sol.jpg");
            Terra = new Estrela("C:/Users/55479/Desktop/Furb/CG/TrabalhoFinal/CGWork4/SistemaSolar/SolarSystem/Resources/terra.jpg");

            Thread terra = new Thread(Rotacionar);
            terra.Start();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(Color.Black);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);

            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);

            AplicarTexturaSol();
            AplicarTexturaTerra();
            
            // campo de visão da câmera sintética
            Visao = new Vector3(50, 50, 50);
            Target = new Vector3(0, 0, 0);
            Longe = 100.0f;
            Perto = 1.0f;

            // Matriz sol
            Sol.MatrizEstrela = Sol.AtribuirEfeito(40);
            // Atribuindo referencia da matriz do sol com a terra
            Terra.MatrizEstrela = Sol.MatrizEstrela.MultiplicarMatriz(Terra.MatrizEstrela);

            //cria terra
            Terra.MatrizEstrela = Terra.AtribuirEfeito(10, true);
            // Atribuindo referencia da matriz de referencia com a terra
            ReferenciaTransacao = Terra.MatrizEstrela.MultiplicarMatriz(ReferenciaTransacao);

            // Referencia para terra
            Transformacao4D matrizReferencia = new Transformacao4D();
            matrizReferencia.AtribuirEscala(0.3, 0.3, 0.3);
            ReferenciaTransacao = matrizReferencia.MultiplicarMatriz(ReferenciaTransacao);
        }

        private void AplicarTexturaTerra()
        {
            GL.GenTextures(2, out textura);
            GL.BindTexture(TextureTarget.Texture2D, textura);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            var bitMapTerra = Terra.BitMapTextura;
            BitmapData dataEarth = bitMapTerra.LockBits(new System.Drawing.Rectangle(0, 0, bitMapTerra.Width, bitMapTerra.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, dataEarth.Width, dataEarth.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, dataEarth.Scan0);
            bitMapTerra.UnlockBits(dataEarth);
        }

        private void AplicarTexturaSol()
        {
            GL.GenTextures(1, out textura);
            GL.BindTexture(TextureTarget.Texture2D, textura);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            var bitMapSol = Sol.BitMapTextura;
            BitmapData dataSun = bitMapSol.LockBits(new System.Drawing.Rectangle(0, 0, bitMapSol.Width, bitMapSol.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, dataSun.Width, dataSun.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, dataSun.Scan0);
            bitMapSol.UnlockBits(dataSun);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, Width / (float)Height, Perto, Longe);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            Matrix4 modelview = Matrix4.LookAt(Visao, Target, new Vector3(0, 1, 0));
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelview);

            // Sol
            DesenhaPlaneta(1);

            GL.PushMatrix();
            GL.MultMatrix(Terra.MatrizEstrela.ObterDados());
            // terra
            DesenhaPlaneta(2);

            GL.PushMatrix();
            GL.MultMatrix(ReferenciaTransacao.ObterDados());
            DesenhaPlaneta(2);
            GL.PopMatrix();

            GL.PopMatrix();

            SwapBuffers();
        }

        protected override void OnKeyDown(OpenTK.Input.KeyboardKeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    this.Exit();
                    break;
                case Key.F:
                    GL.CullFace(CullFaceMode.Front);
                    break;
                case Key.B:
                    GL.CullFace(CullFaceMode.Back);
                    break;
                case Key.Plus:
                    Velocidade += 0.01;
                    break;
                case Key.Minus:
                    if (Velocidade > 0.01) Velocidade -= 0.01;
                    break;
                case Key.L:
                    iluminacao = !iluminacao;
                    break;
                case Key.Up:
                    Visao = new Vector3(Visao.X + 2, Visao.Y + 0, Visao.Z);
                    break;
                case Key.Down:
                    Visao = new Vector3(Visao.X - 2, Visao.Y + 0, Visao.Z);
                    break;
                case Key.Left:
                    Visao = new Vector3(Visao.X, Visao.Y + 2, Visao.Z);
                    break;
                case Key.Right:
                    Visao = new Vector3(Visao.X, Visao.Y - 2, Visao.Z);
                    break;
                case Key.O:
                    Visao = new Vector3(Visao.X, Visao.Y, Visao.Z + 2);
                    break;
            }
        }

        private void DesenhaPlaneta(int textu)
        {

            if (iluminacao)
            {
                GL.Enable(EnableCap.Lighting);
                GL.Enable(EnableCap.Light0);
                GL.Enable(EnableCap.ColorMaterial);
            }
            DesenhaEsfera(6, 38, 18, textu);
            if (iluminacao)
            {
                GL.Disable(EnableCap.Lighting);
                GL.Disable(EnableCap.Light0);
            }
        }

        public void DesenhaEsfera(double radius, int slices, int stacks, int textu)
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
                    Ponto4D p1 = PegarPontos(phi, theta, radius);
                    Ponto4D p2 = PegarPontos(phi + slice, theta, radius);
                    Ponto4D p3 = PegarPontos(phi + slice, theta + stack, radius);
                    Ponto4D p4 = PegarPontos(phi, theta + stack, radius);

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

        Ponto4D PegarPontos(double phi, double theta, double radius)
        {
            double x = radius * Math.Cos(theta) * Math.Sin(phi);
            double y = radius * Math.Sin(theta) * Math.Sin(phi);
            double z = radius * Math.Cos(phi);
            return new Ponto4D(x, y, z);
        }

        public void Rotacionar()
        {
            while (true)
            {
                Terra.MatrizEstrela.AtribuirRotacaoY(Velocidades.Terra.Move(Velocidade));
                Thread.Sleep(50);
            }
        }

        private static class Velocidades
        {
            public static Velocidade Terra { get; set; }

            static Velocidades()
            {
                Terra = new Velocidade(0.1);
            }
        }
    }
}
