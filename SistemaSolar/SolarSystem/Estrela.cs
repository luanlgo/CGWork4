using CG_Biblioteca;
using System.Drawing;

namespace SistemaSolar
{
    public class Estrela
    {
        public readonly Bitmap BitMapTextura;
        public Transformacao4D MatrizEstrela;

        public Estrela(string path)
        {
            BitMapTextura = new Bitmap(path);
            MatrizEstrela = new Transformacao4D();
        }

        public Transformacao4D AtribuirEfeito(double transacao, bool escala = false)
        {
            Transformacao4D matrizNova = new Transformacao4D();
            if (escala) matrizNova.AtribuirEscala(1.5, 1.5, 1.5);
            matrizNova.AtribuirTranslacao(transacao, 0, 0);
            return matrizNova.MultiplicarMatriz(MatrizEstrela);
        }
    }
}
