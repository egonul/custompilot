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
            
            /*Göstergeleri yarat */
            AirSpeedIndicator = new asIndicator(pictureBox_asIndicator);
            AttitudeIndicator = new attIndicator(pictureBox_attIndicator);

            //IMU verileri için değişken oluştur.
            imu_data=new IMU();
            seriBuffer = new byte[2048];
            
            adc1 = new ADC();

            if (textBox48.Text != null && textBox48.Text.Length != 0)
            {
                adc1.AVdd = Convert.ToSingle(textBox48.Text);
                adc1.BitWeight = (adc1.AVdd * 1000.00) / 4095.00;
                this.listBox_SystemMessages.Items.Add(DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + "--> " + "Bit Weight:" + Convert.ToString(adc1.BitWeight));
            } 

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
            header2 = Convert.ToByte(serialPort_IMU.ReadByte());
            header3 = Convert.ToByte(serialPort_IMU.ReadByte());
            header4 = Convert.ToByte(serialPort_IMU.ReadByte());

            if ((header2 == '\x42') && (header3 == '\x43') && (header4 == '\x41'))
            {

                for (int i = 0; i < 20; i++)
                    {
                        seriBuffer[i] = Convert.ToByte(serialPort_IMU.ReadByte());                

                    }

                    //Check for final header
                    //if (seriBuffer[100] == '\x0D' && seriBuffer[101] == '\x0A')
                    //{
                        for (int ii = 0; ii < 4; ii++)
                        {

                          if (ii == 0)
                          {
                            temp = BitConverter.ToDouble(seriBuffer, 4 * ii);
                          } else
                        { 
                        adc[ii] = BitConverter.ToSingle(seriBuffer, (4 * ii)+4);
                         }
                }

                        imu_data.accX = temp ;
                        imu_data.accY = adc[1] ;
                        imu_data.accZ = adc[2] ;
                        
                        imu_data.gyroX = adc[3] ;
                        //imu_data.gyroY = adc[4] ;
                        //imu_data.gyroZ = adc[5] ;

                        //imu_data.magX = adc[6];
                        //imu_data.magY = adc[7];
                        //imu_data.magZ = adc[8];

                        //imu_data.Temperature = adc[9];
                        //imu_data.StaticPress = adc[10];
                        //imu_data.DynamicPress = adc[11];
                        
                        //imu_data.roll = adc[12] ;
                        //imu_data.pitch = adc[13] ;
                        //imu_data.yaw = adc[14] ;

                        //imu_data.h_corrected = adc[15];

                        //imu_data.PressureAltitude = adc[16];
                        //imu_data.TAS = adc[17];
                        //imu_data.Theading = adc[18];

                        //imu_data.IMU_data_valid = adc[19];
                        //imu_data.counter=adc[20];
                        //imu_data.GPS_valid = adc[21];

                        //imu_data.gps_date = adc[22];
                        //imu_data.gps_month = adc[23];
                        //imu_data.gps_year = adc[24];
                        //imu_data.gps_time_h = adc[25];
                        //imu_data.gps_time_m = adc[26];
                        //imu_data.gps_time_s = adc[27];

                        //imu_data.ins_lat = BitConverter.ToDouble(seriBuffer, 56); //56/2=28

                        //imu_data.ins_lon = BitConverter.ToDouble(seriBuffer, 64);
                 
                        //imu_data.gps_speed = adc[36];
                        //imu_data.gps_speed_fraction = adc[37];
                        //imu_data.gps_trackangle = adc[38];
                        //imu_data.gps_trackangle_fraction = adc[39];

                        //imu_data.INS_Vn = adc[40]/100.00;
                        //imu_data.INS_Ve = adc[41]/ 100.00;
                        //imu_data.INS_Vd = adc[42]/ 100.00;

                        //imu_data.ins_alt = BitConverter.ToDouble(seriBuffer, 86);

                        new_sample = 1;
                    
           //         }
           //         else
           //         {
                        
           //         }

                   
                                        

           //     }
           //}

           //else
           //{
           //    while (serialPort_IMU.ReadByte() != '\xFF') ;
           //    if (serialPort_IMU.ReadByte() == '\xFA')
           //    {

           //        for (int i = 0; i < 26; i++)
           //        {
           //            seriBuffer[i] = Convert.ToByte(serialPort_IMU.ReadByte());
                     
           //        }

           //        //Check for final header
           //        if (seriBuffer[24] == '\x0D' && seriBuffer[25] == '\x0A')
           //        {
           //            for (int ii = 0; ii < 12; ii++)
           //            {
           //                adc[ii] = BitConverter.ToInt16(seriBuffer, 2 * ii);
           //            }

           //        }

           //        imu_data.accX = adc[0];
           //        imu_data.accY = adc[1];
           //        imu_data.accZ = adc[2];
           //        imu_data.gyroX = adc[3];
           //        imu_data.gyroY = adc[4];
           //        imu_data.gyroZ = adc[5];
           //        imu_data.magX = adc[6];
           //        imu_data.magY = adc[7];
           //        imu_data.magZ = adc[8];
           //        imu_data.Temperature= adc[9];
           //        imu_data.PressureAltitude = adc[10];
           //        imu_data.TAS = adc[11];
           //        imu_data.roll = 0;
           //        imu_data.pitch = 0;
           //        imu_data.yaw = 0;

           //        new_sample = 1;

                

           //    }
           }


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
            textBox11.Text = Convert.ToString(imu_data.PressureAltitude); //alt
            textBox10.Text = Convert.ToString(imu_data.TAS); //ias

            textBox16.Text = Convert.ToString(imu_data.pitch);
            textBox17.Text = Convert.ToString(imu_data.roll);
            textBox18.Text = Convert.ToString(imu_data.yaw);

           


            textBox41.Text = Convert.ToString(imu_data.gps_speed)+ "."+ Convert.ToString(imu_data.gps_speed_fraction);
           
         
            textBox30.Text = Convert.ToString(imu_data.ins_lat);
            textBox26.Text = Convert.ToString(imu_data.ins_lon);
       
            textBox33.Text = Convert.ToString(imu_data.INS_Vn);
            textBox20.Text = Convert.ToString(imu_data.INS_Ve);
            textBox21.Text = Convert.ToString(imu_data.INS_Vd);

            textBox29.Text = Convert.ToString(imu_data.counter);

            if (imu_data.IMU_data_valid == 1) label16.ForeColor = Color.LawnGreen;
            else label16.ForeColor = Color.Red;

            if (imu_data.GPS_valid== 1) label30.ForeColor = Color.LawnGreen;
            else label16.ForeColor = Color.Red;
                               
            AttitudeIndicator.pitch = Convert.ToSingle(imu_data.pitch);
            AttitudeIndicator.roll = Convert.ToSingle(imu_data.roll);
            AttitudeIndicator.Refresh();

            AirSpeedIndicator.airspeed = imu_data.TAS;
            AirSpeedIndicator.Refresh();

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form Graphs_Form1 = new Graphs_Form();
            Graphs_Form1.Show();
        }

           private void buttonShowGE_Click(object sender, EventArgs e)
        {
            
        }

           private void button2_Click(object sender, EventArgs e)
           {
               Form Sensor1 = new Sensor();
               Sensor1.Show();
           }

           
           
          

         

    }
}
