using CG_Biblioteca;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;

namespace CGWork4
{
    public class BolaPapel : Transformacao4D
    {
        public Bitmap BitMapBola { get; private set; }

        public bool PodeRotacionar { get; private set; }

        public double VelocidadeRotacao { get; private set; }

        public BolaPapel(string textura)
        {
            BitMapBola = new Bitmap(textura);

            Thread threadBolaPapel = new Thread(Rotacionar);
            threadBolaPapel.Start();
        }

        public void Rotacionar()
        {
            while(PodeRotacionar)
            {
                AtribuirRotacaoY(VelocidadeRotacao);
                Thread.Sleep(100);
            }
        }
        
        public void SetVelocidadeRotacao(double velocidade)
        {
            VelocidadeRotacao = velocidade;
        }

        public void SetPodeRotacionar(bool rotacao)
        {
            PodeRotacionar = rotacao;
        }

        public void UnlockBits(BitmapData bitMap)
        {
            BitMapBola.UnlockBits(bitMap);
        }
    }
}
