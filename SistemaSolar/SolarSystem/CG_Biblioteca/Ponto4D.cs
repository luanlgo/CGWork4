/**
  Autor: Dalton Solano dos Reis
**/

namespace CG_Biblioteca
{
    /// <summary>
    /// Classe que define um ponto no espaço 3D com a coordenada homogênea (w) da Transformação Geometrica
    /// </summary>
    public class Ponto4D
    {
        /// <summary>
        /// Instância um ponto 3D com a coordenada homogênea w
        /// </summary>
        /// <param name="x">coordenada eixo x</param>
        /// <param name="y">coordenada eixo y</param>
        /// <param name="z">coordenada eixo z</param>
        /// <param name="w">coordenada espaço homogêneo</param>
        public Ponto4D(double x = 0.0, double y = 0.0, double z = 0.0, double w = 1.0)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }
        public Ponto4D(Ponto4D ponto)
        {
            this.X = ponto.X;
            this.Y = ponto.Y;
            this.Z = ponto.Z;
        }
        // Operator overloaded
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ponto1"></param>
        /// <param name="ponto2"></param>
        /// <returns> Retorna a soma dos dois pontos.</returns>
        /// <example>
        /// <code>
        /// pto = pto1 + pto2;
        /// </code>
        /// </example>
        public static Ponto4D operator +(Ponto4D ponto1, Ponto4D ponto2) => new Ponto4D(ponto1.X + ponto2.X, ponto1.Y + ponto2.Y, ponto1.Z + ponto2.Z);

        /// <summary>
        /// Obter e atribuir a coordenada x
        /// </summary>
        /// <value>coordenada x</value>
        public double X { get; set; }
        /// <summary>
        /// Obter e atribuir a coordenada y
        /// </summary>
        /// <value>coordeanda y</value>
        public double Y { get; set; }
        /// <summary>
        /// Obter e atribuir a coordenada z
        /// </summary>
        /// <value>coordeanda z</value>
        public double Z { get; set; }
        /// <summary>
        /// Obter e atribuir a coordenada homogênea w
        /// </summary>
        /// <value>coordeanda w</value>
        public double W { get; private set; }
    }
}