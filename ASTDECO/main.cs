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
            string filePath = @"C:\Users\34626\Documents\PGTA\ASTERIX\P2_ASTERIX\ASTDECO\230502-est-080001_BCN_60MN_08_09 (3).ast"; // Ruta del archivo binario;

            // Ruta de salida para CSV
            string csvPath = @"C:\Users\34626\Documents\PGTA\ASTERIX\P2_ASTERIX.csv";


            //inst clasificador
            Clasificador clasificador = new Clasificador();
            
            //DataTable
            DataTable tabla = clasificador.CrearClasi(filePath);
             // Exportar a CSV
            ExportarACSV(tabla, csvPath);
            // Mensaje de confirmación
            Console.WriteLine($"Archivo CSV generado correctamente en: {csvPath}");
            Console.WriteLine("\nPresiona cualquier tecla para salir...");
            Console.ReadKey();


            //probar tabla
            Console.WriteLine($"Número de columnas: {tabla.Columns.Count}");
            Console.WriteLine($"Número de filas: {tabla.Rows.Count}");

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
