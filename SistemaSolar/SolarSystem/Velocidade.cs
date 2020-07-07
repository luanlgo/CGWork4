namespace SolarSystem
{
    public class Velocidade
    {
        public double DefaultValue { get; private set; }
        public double CurrentValue { get; set; }

        public Velocidade(double defaultValue)
        {
            this.DefaultValue = defaultValue;
        }

        public double Move(double velocidade)
        {
            return this.CurrentValue += velocidade;
        }
    }
}
