using System;
using System.Data;
using ASTDECO;
namespace AST_DECO
{
    class Program
    {
        static void Main(string[] args)
        {
            //Ruta
            string filePath = @"C:/Users/test/Downloads/230502-est-080001_BCN_60MN_08_09.ast";

            //inst clasificador
            Clasificador clasificador = new Clasificador();

            //DataTable
            DataTable tabla = clasificador.CrearClasi(filePath);

            //probar tabla
            Console.WriteLine($"Número de columnas: {tabla.Columns.Count}");
            Console.WriteLine($"Número de filas: {tabla.Rows.Count}");

        }
    }
}
