using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tez_v1
{
    public class IMU
    {
        public double IMU_data_valid = 0, GPS_valid = 0 , counter=0;

        public double accX, accY, accZ, gyroX, gyroY, gyroZ, magX, magY, magZ;

        public double TAS, IAS, PressureAltitude, Temperature, StaticPress, DynamicPress;
         
        public double h_corrected, Theading, MHeading, Xx,Xy,Xz,Vx,Vy,Vz;

        public double Lat_Degrees, Lat_Mins, Lat_Fractions, Lat_Dir, Lon_Degrees, Lon_Mins, Lon_Fractions, Lon_Dir, gps_speed, gps_speed_fraction, 
            gps_trackangle, gps_trackangle_fraction, gps_date, gps_month, gps_year,
            gps_time_h,gps_time_m,gps_time_s,gps_alt,gps_alt_fraction;

        public double pitch, roll, yaw, pitch1, roll1, yaw1, pitch2, roll2, yaw2;
      
        public double lat=39.96144, lon=32.81520,  utc_time;

        public double ins_lat=0, ins_lon=0, ins_alt=0 ,INS_Vn, INS_Ve, INS_Vd;
    

        public void calculate_attitude()
        {
            ////pitch = ((System.Math.Atan((Convert.ToDouble((RAWaccX - BIASaccX) * 0.0013431)) / (System.Math.Sqrt((System.Math.Pow(Convert.ToDouble((RAWaccY - BIASaccY) * 0.0013431), 2)) + System.Math.Pow(Convert.ToDouble((RAWaccZ - BIASaccZ) * 0.0013431), 2))))) * 180.00) / 3.14;
            //pitch = ((System.Math.Atan((Convert.ToDouble(RAWaccX - BIASaccX)) / (System.Math.Sqrt((System.Math.Pow(Convert.ToDouble(RAWaccY - BIASaccY), 2)) + System.Math.Pow(Convert.ToDouble(RAWaccZ - BIASaccZ), 2))))) * 180.00) / 3.14;
            
            //roll = ((System.Math.Atan((Convert.ToDouble(RAWaccY - BIASaccY)) / (System.Math.Sqrt((System.Math.Pow(Convert.ToDouble(RAWaccX - BIASaccX), 2)) + System.Math.Pow(Convert.ToDouble(RAWaccZ - BIASaccZ), 2))))) * 180.00) / 3.14;
            //yaw = ((System.Math.Atan((System.Math.Sqrt((System.Math.Pow(Convert.ToDouble(RAWaccX - BIASaccX), 2)) + System.Math.Pow(Convert.ToDouble(RAWaccY - BIASaccY), 2))) / (Convert.ToDouble(RAWaccZ - BIASaccZ)))) * 180.00) / 3.14;

        }

        public void calculate_acceleration()
        {
            //accX = ((RAWaccX - BIASaccX) * MainForm.adc1.BitWeight)/600.00;
            //accY = ((RAWaccY - BIASaccY) * MainForm.adc1.BitWeight)/600.00;
            //accZ = ((RAWaccZ - BIASaccZ) * MainForm.adc1.BitWeight)/600.00;

        }

        public void calculate_attitude_rate()
        {
            //gyroX = ((RAWgyroX - BIASgyroX) * MainForm.adc1.BitWeight) / 2.00;
            //gyroY = ((RAWgyroY - BIASgyroY) * MainForm.adc1.BitWeight) / 2.00;
            //gyroZ = ((RAWgyroZ - BIASgyroZ) * MainForm.adc1.BitWeight) / 2.00;

        }

    }

    class GPS
    {
        double time, lat, lon, gps_ALT, gps_speed;
    }
}
