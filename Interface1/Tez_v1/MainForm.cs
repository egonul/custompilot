using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace Tez_v1
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();

            /*Sistemdeki tanımlı seri portları listele*/
            string[] port = System.IO.Ports.SerialPort.GetPortNames();
            foreach (string i in port) comboBox_Portlist.Items.Add(i);
            
              //IMU verileri için değişken oluştur.
            imu_data=new IMU();
            seriBuffer = new byte[2048];
            
            adc1 = new ADC();

            //Grafik için verileri tutacak değişken oluştur.
            Seri1 = new Series();
            Seri2 = new Series();
            Seri3 = new Series();
            Seri4 = new Series();
            Seri5 = new Series();
            Seri6 = new Series();
            Seri7 = new Series();
            Seri8 = new Series();
            Seri9 = new Series();

           
        
        }
                
        //Open or Close serial port
        private void button1_Click(object sender, EventArgs e)
        {
            if (!this.serialPort_IMU.IsOpen)
            {
                this.serialPort_IMU.PortName = comboBox_Portlist.Text;
                this.serialPort_IMU.BaudRate = Convert.ToInt32(comboBox_baudrate.Text);
                this.serialPort_IMU.Open();
                this.button_OpenPort.Text = "Close";
                this.listBox_SystemMessages.Items.Add( DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + "--> " +serialPort_IMU.PortName + " Açıldı. ");
                
            }
            else
            {
                this.serialPort_IMU.Close();
                this.button_OpenPort.Text = "Open";
                this.listBox_SystemMessages.Items.Add(DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + "--> " + serialPort_IMU.PortName + " Kapatıldı.");
            }

        }

        private void comboBox_Portlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.serialPort_IMU.IsOpen)
            {
                this.serialPort_IMU.Close();
                this.listBox_SystemMessages.Items.Add(DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + "--> " + serialPort_IMU.PortName + " Kapatıldı.");
                this.serialPort_IMU.PortName = comboBox_Portlist.Text;
                this.serialPort_IMU.Open();
                this.listBox_SystemMessages.Items.Add(DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + "--> " + serialPort_IMU.PortName + " Açıldı. ");
            }
        }

        private void pictureBox_asIndicator_Paint(object sender, PaintEventArgs e)
        {
            AirSpeedIndicator.Paint_Olayi(e);
        }

        private void pictureBox_attIndicator_Paint(object sender, PaintEventArgs e)
        {
            AttitudeIndicator.Paint_Olayi(e);
        }

       
            

          
        
        private void serialPort_IMU_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {



            while (serialPort_IMU.ReadByte() != '\x41') { }
            rx_byte = Convert.ToByte(serialPort_IMU.ReadByte());
            rx_byte1 = Convert.ToByte(serialPort_IMU.ReadByte());
            rx_byte2 = Convert.ToByte(serialPort_IMU.ReadByte());

            if ( (rx_byte == '\x42') && (rx_byte1 == '\x43') && (rx_byte2 == '\x41'))
                {

                    for (int i = 0; i < 80; i++)
                    {
                        seriBuffer[i] = Convert.ToByte(serialPort_IMU.ReadByte());                

                    }

                    //Check for final header
                    //if (seriBuffer[40] == '\x0D' && seriBuffer[41] == '\x0A')
                    //{
                        for (int ii = 0; ii < 20; ii++)
                        {

                        //adc[ii] = BitConverter.ToInt16(seriBuffer, 2 * ii);
                        adc[ii] = BitConverter.ToSingle(seriBuffer, 2 * ii);

                        }

                        imu_data.accX = adc[0] ;
                        imu_data.accY = adc[1] ;
                        imu_data.accZ = adc[2] ;
                        
                        imu_data.gyroX = adc[3] ;
                        imu_data.gyroY = adc[4] ;
                        imu_data.gyroZ = adc[5] ;

                        imu_data.magX = adc[6];
                        imu_data.magY = adc[7];
                        imu_data.magZ = adc[8];

                        imu_data.Temperature = adc[9];
                        imu_data.StaticPress = adc[10];
                        imu_data.DynamicPress = adc[11];
                         
                        new_sample = 1;
                    
                    //}
                   

                }
                //else if (rx_byte == '\xFB')
                //{
                //    for (int i = 0; i < 42; i++)
                //    {
                //        seriBuffer[i] = Convert.ToByte(serialPort_IMU.ReadByte());
                //    }

                //    //Check for final header
                //    if (seriBuffer[40] == '\x0D' && seriBuffer[41] == '\x0A')
                //    {
                //        for (int ii = 0; ii < 20; ii++)
                //        {
                //            adc[ii] = BitConverter.ToInt16(seriBuffer, 2 * ii);
                //        }

                //        imu_data.Lat_Degrees = adc[0];
                //        imu_data.Lat_Mins = adc[1];
                //        imu_data.Lat_Fractions= adc[2];
                //        imu_data.Lat_Dir = adc[3];
                //        imu_data.Lon_Degrees = adc[4];
                //        imu_data.Lon_Mins = adc[5];
                //        imu_data.Lon_Fractions = adc[6];
                //        imu_data.Lon_Dir= adc[7];
                //        imu_data.gps_alt= adc[8];
                //        imu_data.gps_alt_fraction = adc[9];
                //        imu_data.gps_speed = adc[10];
                //        imu_data.gps_speed_fraction = adc[11];
                //        imu_data.gps_trackangle = adc[12];
                //        imu_data.gps_trackangle_fraction = adc[13];
                //        imu_data.gps_date = adc[14];
                //        imu_data.gps_month = adc[15];
                //        imu_data.gps_year = adc[16];
                //        imu_data.gps_time_h= adc[17];
                //        imu_data.gps_time_m = adc[18];
                //        imu_data.gps_time_s = adc[19];
                //        new_sample = 1;

                //    }
                   
                //}
           
        }

        private void timer_SeriPort_Tick(object sender, EventArgs e)
        {
            textBox1.Text = Convert.ToString(imu_data.accX);
            textBox2.Text = Convert.ToString(imu_data.accY);
            textBox3.Text = Convert.ToString(imu_data.accZ);
              
            textBox4.Text = Convert.ToString(imu_data.gyroX);
            textBox5.Text = Convert.ToString(imu_data.gyroY);
            textBox6.Text = Convert.ToString(imu_data.gyroZ);

            textBox7.Text = Convert.ToString(imu_data.magX);
            textBox8.Text = Convert.ToString(imu_data.magY);
            textBox9.Text = Convert.ToString(imu_data.magZ);

            textBox38.Text = Convert.ToString(imu_data.Temperature); //temp
            textBox11.Text = Convert.ToString(imu_data.StaticPress); //alt
            textBox10.Text = Convert.ToString(imu_data.DynamicPress); //ias

            textBox12.Text = Convert.ToString(imu_data.Lat_Degrees);
            textBox13.Text = Convert.ToString(imu_data.Lat_Mins);
            textBox14.Text = Convert.ToString(imu_data.Lat_Fractions);
            textBox18.Text = Convert.ToString(imu_data.Lat_Dir);

            textBox15.Text = Convert.ToString(imu_data.Lon_Degrees);
            textBox16.Text = Convert.ToString(imu_data.Lon_Mins);
            textBox17.Text = Convert.ToString(imu_data.Lon_Fractions);
            textBox19.Text = Convert.ToString(imu_data.Lon_Dir);

                      
            textBox34.Text = Convert.ToString(imu_data.gps_time_h+3);
            textBox53.Text = Convert.ToString(imu_data.gps_time_m);
            textBox54.Text = Convert.ToString(imu_data.gps_time_s);

            textBox22.Text = Convert.ToString(imu_data.gps_date);
            textBox23.Text = Convert.ToString(imu_data.gps_month);
            textBox24.Text = Convert.ToString(imu_data.gps_year);
            
            textBox41.Text = Convert.ToString(imu_data.gps_speed)+ "."+ Convert.ToString(imu_data.gps_speed_fraction);
            textBox25.Text = Convert.ToString(imu_data.gps_trackangle) + "." + Convert.ToString(imu_data.gps_trackangle_fraction);
    
        }

      
    }
}
