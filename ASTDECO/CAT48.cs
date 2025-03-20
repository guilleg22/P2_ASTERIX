using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ASTDECO
{
    public class CAT48
    {
         //ATRIBUTOS
        public string FSPEC;
        public string SAC;
        public string SIC;
        public string time;
        public double Lat;
        public double Long;
        public double hwgs84;
        public string TYP020;
        public string SIM020;
        public string RDP020;
        public string SPI020;
        public string RAB020;
        public string TST020;
        public string ERR020;
        public string XPP020;
        public string ME020;
        public string MI020;
        public string FOEFRI_020;
        public double RHO;
        public double THETA;
        public string V070;
        public string G070;











        

        public void ComputeFSPEC(string[] Fspecs)
        {
            int i = 3;//empezamos en tres
            bool encontrado = true;
            while(encontrado == true)
            {
                string octeto = Convert.ToString(Convert.ToInt32(Fspecs[i],16),2).PadLeft(8,'0');
                this.FSPEC = this.FSPEC + octeto;

                if(octeto.Substring(7,1) == "1")
                    i++;

                else
                    encontrado = false;
            }
        }



        //I048/140
        public void ComputeTimeOfDay(string octetoTimeOfDay)
        {
            int seg = int.Parse(octetoTimeOfDay,System.Globalization.NumberStyles.HexNumber);
            double segundos = Convert.ToSingle(seg)/128;
            TimeSpan time = TimeSpan.FromSeconds(segundos);
            this.time = time.ToString(@"hh\:mm\:ss\:fff");

        }

     }




























}