namespace SolarSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var sistemaSolar = new SistemaSolar(800, 800))
			{
				sistemaSolar.Run(1.0 / 60.0);
			}
		}
    }
}
