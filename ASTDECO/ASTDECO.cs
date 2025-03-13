using System;
using System.IO;
using System.Data;
using System.Collections.Generic;

namespace ASTDECO
{
    public class Clasificador
    {
        public DataTable CrearClasi(string Path)
        {
            // Crear DataTable
            DataTable dataTable = new DataTable();

            // Lista de nombres de columnas
            List<string> columnNames = new List<string>
            {
                "N", // Numero de registro

                //010
                "SAC", "SIC",

                //140
                "TIME",

                //LLA
                "LAT", "LON", "H",

                //020
                "TYP_020", "SIM_020", "RDP_020", "SPI_020", "RAB_020",
                "TST_020", "ERR_020", "XPP_020", "ME_020", "MI_020",
                "FOEFRI_020", "ADSB_EP_020", "ADSB_VAL_020",
                "SCN_EP_020", "SCN_VAL_020", "PAI_EP_020", "PAI_VAL_020",

                //040
                "RHO", "THETA",

                //070
                "V_070", "G_070", "L_070", "Mode_3A",

                //090
                "V_090", "G_090", "Flight_Level",

                //Mode C Corrected
                "ModeC_Co",

                //130
                "SRL_130", "SRR_130", "SAM_130", "PRL_130", "PAM_130",
                "RPD_130", "APD_130",

                //220
                "Target_Address", //TA en la tabla

                //240
                "Target_Identification", //TI en la tabla

                // 250 -Selected Vertical Intention BDS 4
                "Mode_S", "MCP_ALT", "FMS_ALT",
                "BP", "VNAV", "ALTHOLD", "APP",
                "TARGETALT_SOURCE",

                // 250 -Track and Turn Report BDS 5 
                "RA", "TTA", "GS",
                "TAR", "TAS",

                //250 -Heading and Speed Report BDS 6 
                "HDG", "IAS",
                "MACH", "BAR", "IVV",

                //161
                "Track_Number",

                //042
                "X_Component", "Y_Component",

                //200
                "Ground_Speed_kt", "Heading",

                //170
                "CNF_170", "RAD_170", "DOU_170", "MAH_170", "CDM_170",
                "TRE_170", "GHO_170", "SUP_170", "TCC_170",

                //110
                "HEIGHT",

                //230
                "COM_230", "STAT_230", "SI_230", "MSSC_230", "ARC_230",
                "AIC_230", "B1A_230", "B1B_230"
            };

            //Agregar columnas
            foreach (string columnName in columnNames)
            {
                dataTable.Columns.Add(columnName, typeof(string));
            }

            //Leer el archivo .ast
            using (FileStream fs = new FileStream(Path, FileMode.Open, FileAccess.Read))
            using (BinaryReader br = new BinaryReader(fs))
            {
                int NUM = 1;

                while (br.BaseStream.Position != br.BaseStream.Length)
                {
                    //primer byte CAT del 0 a 255
                    byte nextByte = br.ReadByte(); 

                    //Siguients 2 bytes LONGITUD 
                    byte[] nextTwoBytes = br.ReadBytes(2);
                    int combinedValue = (nextTwoBytes[0] << 8) | nextTwoBytes[1]; //dejamos 8 ceros de espacio a la derecha para la suma con el otro byte
                    int length = combinedValue - 3; //Restamos 3 bytes (cat + longitud)

                    //Leer resto
                    List<byte> completeMessage = new List<byte>(br.ReadBytes(length)); //array de bytes usando la longitud esperada

                    //Leer FSPEC
                    LeerFSPEC(completeMessage);

                    //info prueba
                    Console.WriteLine($"Mensaje {NUM}: Categoría = {nextByte}, Longitud = {combinedValue}");
                    NUM++;
                }
            }

            return dataTable; 
        }
        public void LeerFSPEC(List<byte> message)
        //FSPEC se encuentra justo después de los primeros bytes (CAT48 Y LONGITUD).
        //Hay que leer bytes uno por uno hasta que un byte FSPEC tenga el bit más alto (el bit 8) en 0.
        {
            Console.WriteLine("FSPEC en binario:");
            int index=0;
            byte fspecByte; //se almecena cada byte del FSPEC
            do
            {
                fspecByte = message[index];
                string binaryString = Convert.ToString(fspecByte, 2).PadLeft(8, '0');// Convierte el byte a su representación binaria, y se asegura que tenga 8 bits
                Console.WriteLine(binaryString);
                index++;

            } 
            while ((fspecByte & 0x80) != 0);//La operacion AND (&) 0x80 (hexadecimal) es 10000000 en binario. // Si el bit más alto (MSB) es 1, significa que hay otro byte FSPEC y sigue leyendo
                                             //Si el bit mas alto es 0, significa que es el último byte FSPEC y se deteiene la lectura.
        }
    }
}
