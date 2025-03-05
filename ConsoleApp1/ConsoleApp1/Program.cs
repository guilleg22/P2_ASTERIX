using System;
using System.IO;
namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string filePath = @"C:\Users\34626\Documents\PGTA\ASTERIX\P2_ASTERIX\ConsoleApp1\230502-est-080001_BCN_60MN_08_09.ast"; // Ruta del archivo binario

            if (File.Exists(filePath))
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                using (BinaryReader br = new BinaryReader(fs))
                {
                    while (fs.Position < fs.Length)
                    {
                        byte data = br.ReadByte(); // Leer byte a byte
                        Console.Write($"{data:X2} "); // Mostrar en formato hexadecimal
                    }
                }
            }
            else
            {
                Console.WriteLine("El archivo no existe.");
            }
            
        }
    }
}


