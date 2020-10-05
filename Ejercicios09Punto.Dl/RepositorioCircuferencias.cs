using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ejercicios09Punto4.Bl;

namespace Ejercicios09Punto.Dl
{
    public class RepositorioCircuferencias
    {
        public List<Circunferencia> ListaCircunferencias { get; set; } = new List<Circunferencia>();
        public bool EstaModificado { get; set; } = false;
        private readonly string _archivo = Environment.CurrentDirectory + @"\Circunferencias.Csv";
        public RepositorioCircuferencias()
        {
            LeerDatosDelArchivo();
        }

        private void LeerDatosDelArchivo()
        {
            if (!File.Exists(_archivo))
            {
                return;
            }
            StreamReader lector=new StreamReader(_archivo);
            while (!lector.EndOfStream)
            {
                string linea = lector.ReadLine();
                Circunferencia circunferencia = ConstruirCircunferencia(linea);
                ListaCircunferencias.Add(circunferencia);
            }
            lector.Close();
        }

        private Circunferencia ConstruirCircunferencia(string linea)
        {
            return new Circunferencia
            {
                Radio = int.Parse(linea)
            };
        }

        public void GuardarDatosEnArchivo()
        {
            StreamWriter escritor = new StreamWriter(_archivo);
            foreach (var circunferencia in ListaCircunferencias)
            {
                string linea = ConstruirLinea(circunferencia);
                escritor.WriteLine(linea);
            }
            escritor.Close();
        }
        private string ConstruirLinea(Circunferencia circunferencia)
        {
            return $"{circunferencia.Radio}";
        }

        public bool ExisteCircunferencia(Circunferencia circunferencia)
        {
            return ListaCircunferencias.Contains(circunferencia);
        }
        public void Agregar(Circunferencia circunferencia)
        {
            ListaCircunferencias.Add(circunferencia);
            EstaModificado = true;
        }

        public void Borrar(Circunferencia circunferencia)
        {
            ListaCircunferencias.Remove(circunferencia);
            EstaModificado = true;

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

        public List<Circunferencia> GetListaOrdenada()
        {
            return ListaCircunferencias.OrderBy(c => c.Radio).ToList();
        }

        public List<Circunferencia> GetListaFiltrada(int valorRadio)
        {
            return ListaCircunferencias.Where(c => c.Radio >= valorRadio).ToList();
        }
    }
}
