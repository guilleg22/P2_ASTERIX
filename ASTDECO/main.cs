using System;
using System.Data;
using System.IO;
using ASTDECO;

namespace AST_DECO
{
    class Program
    {
        static void Main(string[] args)
        {
            // Ruta del archivo de entrada
            string filePath = @"C:\Users\jpask\OneDrive\Escriptori\PGTA\Proyecto2\CodigoProyecto\P2_ASTERIX\ASTDECO\230502-est-080001_BCN_60MN_08_09.ast";

            // Ruta de salida para CSV
            string csvPath = @"C:\Users\jpask\OneDrive\Escriptori\PGTA\Proyecto2\CodigoProyecto\P2_ASTERIX\ASTDECO\output.csv";

            // Instanciar clasificador
            Clasificador clasificador = new Clasificador();

            // Crear DataTable
            DataTable tabla = clasificador.CrearClasi(filePath);

            // Exportar a CSV
            ExportarACSV(tabla, csvPath);

            // Mensaje de confirmación
            Console.WriteLine($"Archivo CSV generado correctamente en: {csvPath}");
            Console.WriteLine("\nPresiona cualquier tecla para salir...");
            Console.ReadKey();
        }
        
        // Función para exportar DataTable a CSV
        static void ExportarACSV(DataTable tabla, string ruta)
        {
            using (StreamWriter sw = new StreamWriter(ruta))
            {
                // Escribir encabezados
                sw.WriteLine(string.Join(",", tabla.Columns.Cast<DataColumn>().Select(col => col.ColumnName)));

                // Escribir filas
                foreach (DataRow row in tabla.Rows)
                {
                    sw.WriteLine(string.Join(",", row.ItemArray));
                }
            }
        }
    }
}