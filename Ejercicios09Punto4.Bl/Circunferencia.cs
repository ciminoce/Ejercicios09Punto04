using System;

namespace Ejercicios09Punto4.Bl
{
    public class Circunferencia:ICloneable
    {
        public int Radio { get; set; }

        public Circunferencia() { }
        public Circunferencia(int valorRadio)
        {
            Radio = valorRadio;
        }
        public double GetPerimetro()
        {
            return 2 * Math.PI * Radio;
        }

        public double GetArea()
        {
            return Math.PI*Math.Pow(Radio,2);
        }
        /*Sobreescribo el método Equals
         para comparar los radios de 2 circunferencias*/
        public override bool Equals(object obj)
        {
            if (obj==null || !(obj is Circunferencia))
            {
                return false;
            }

            return this.Radio == ((Circunferencia) obj).Radio;
        }

        public override int GetHashCode()
        {
            return this.Radio.GetHashCode();
        }

        public object Clone()
        {
            return this.MemberwiseClone();//hace una copia superficial del objeto
        }
    }
}
