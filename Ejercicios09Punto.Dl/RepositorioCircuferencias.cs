using System.Collections.Generic;
using Ejercicios09Punto4.Bl;

namespace Ejercicios09Punto.Dl
{
    public class RepositorioCircuferencias
    {
        public List<Circunferencia> ListaCircunferencias { get; set; } = new List<Circunferencia>();

        public RepositorioCircuferencias()
        {
            var circ1 = new Circunferencia(12);
            var circ2 = new Circunferencia(20);
            var circ3 = new Circunferencia(2);
            var circ4 = new Circunferencia(11);
            var circ5 = new Circunferencia(29);
            Agregar(circ1);
            Agregar(circ2);
            Agregar(circ3);
            Agregar(circ4);
            Agregar(circ5);
        }

        public bool ExisteCircunferencia(Circunferencia circunferencia)
        {
            return ListaCircunferencias.Contains(circunferencia);
        }
        public void Agregar(Circunferencia circunferencia)
        {
            ListaCircunferencias.Add(circunferencia);
        }

        public void Borrar(Circunferencia circunferencia)
        {

        }

        public List<Circunferencia> GetLista()
        {
            return ListaCircunferencias;
        }

        //public Circunferencia GetCircunferencia()
        //{

        //}

        public int GetCantidad()
        {
            return ListaCircunferencias.Count;
        }
    }
}
