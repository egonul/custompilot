using System.Windows.Forms.DataVisualization.Charting;
//using EARTHLib;
namespace Tez_v1
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// 
        byte[] seriBuffer ;

        byte rx_byte;
        byte rx_byte1;
        byte rx_byte2;
        byte rx_byte3;
        byte[] deneme = {0x79,0x58,0xA8,0x35,0xCD,0x0F, 0x40,0x40};

        float[] adc = new float[50];
        string gps_data;
        int GPGSA_Enable = 0, GPGGA_Enable=0, GPVTG_Enable=0;
        
        public static IMU imu_data;
        public static ADC adc1;
        asIndicator AirSpeedIndicator;
        attIndicator AttitudeIndicator;
        
        string okunan = "";
        string okunan2 = "";
        string[] GPGGA = { "" };
        string[] GPGSA = { "" };
        string[] GPVTG = { "" };
        string[] GPGSV1 = { "" };
        string[] GPGSV2 = { "" };
        string[] GPGSV3 = { "" };
        int GPindex = 0;
        int adet = 0;
        string[] veri = { "" };
        string[] GPSayirici = { "\x000D\x000A" };
        //char[] ayirici ={ '\x000A' };
        char[] virgul = { ',' };

        int sayac = 0;
        
        public static Series Seri1, Seri2, Seri3, Seri4, Seri5, Seri6, Seri7, Seri8, Seri9;
        public static int sample_num=0, minValue, maxValue;

        int new_sample = 0;

        string[] ddmm={""};
        string[] dddmm={""};
        double lat;
        
        //------------------------------------------//

       
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel_numericaldata = new System.Windows.Forms.Panel();
            this.textBoxRoll = new System.Windows.Forms.TextBox();
            this.textBox25 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox38 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.textBox41 = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label_column2 = new System.Windows.Forms.Label();
            this.textBox17 = new System.Windows.Forms.TextBox();
            this.textBox19 = new System.Windows.Forms.TextBox();
            this.textBox18 = new System.Windows.Forms.TextBox();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.textBox24 = new System.Windows.Forms.TextBox();
            this.textBox16 = new System.Windows.Forms.TextBox();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.textBox23 = new System.Windows.Forms.TextBox();
            this.textBox54 = new System.Windows.Forms.TextBox();
            this.textBox53 = new System.Windows.Forms.TextBox();
            this.textBox15 = new System.Windows.Forms.TextBox();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.textBox22 = new System.Windows.Forms.TextBox();
            this.textBox34 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label_pressureALT = new System.Windows.Forms.Label();
            this.label_airspeed = new System.Windows.Forms.Label();
            this.label_magZ = new System.Windows.Forms.Label();
            this.label_magY = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label_magX = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label_gyroZ = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label_gyroY = new System.Windows.Forms.Label();
            this.label_gyroX = new System.Windows.Forms.Label();
            this.label_accZ = new System.Windows.Forms.Label();
            this.label_accY = new System.Windows.Forms.Label();
            this.label_accX = new System.Windows.Forms.Label();
            this.serialPort_IMU = new System.IO.Ports.SerialPort(this.components);
            this.panel_serialport = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button_OpenPort = new System.Windows.Forms.Button();
            this.comboBox_baudrate = new System.Windows.Forms.ComboBox();
            this.comboBox_Portlist = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.listBox_SystemMessages = new System.Windows.Forms.ListBox();
            this.timer_SeriPort = new System.Windows.Forms.Timer(this.components);
            this.textBox20 = new System.Windows.Forms.TextBox();
            this.textBox21 = new System.Windows.Forms.TextBox();
            this.panel_numericaldata.SuspendLayout();
            this.panel_serialport.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_numericaldata
            // 
            this.panel_numericaldata.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel_numericaldata.Controls.Add(this.textBox21);
            this.panel_numericaldata.Controls.Add(this.textBox20);
            this.panel_numericaldata.Controls.Add(this.textBoxRoll);
            this.panel_numericaldata.Controls.Add(this.textBox25);
            this.panel_numericaldata.Controls.Add(this.label15);
            this.panel_numericaldata.Controls.Add(this.label23);
            this.panel_numericaldata.Controls.Add(this.label9);
            this.panel_numericaldata.Controls.Add(this.textBox38);
            this.panel_numericaldata.Controls.Add(this.label14);
            this.panel_numericaldata.Controls.Add(this.textBox11);
            this.panel_numericaldata.Controls.Add(this.textBox41);
            this.panel_numericaldata.Controls.Add(this.textBox10);
            this.panel_numericaldata.Controls.Add(this.textBox9);
            this.panel_numericaldata.Controls.Add(this.textBox8);
            this.panel_numericaldata.Controls.Add(this.textBox7);
            this.panel_numericaldata.Controls.Add(this.textBox6);
            this.panel_numericaldata.Controls.Add(this.textBox5);
            this.panel_numericaldata.Controls.Add(this.textBox4);
            this.panel_numericaldata.Controls.Add(this.textBox3);
            this.panel_numericaldata.Controls.Add(this.textBox2);
            this.panel_numericaldata.Controls.Add(this.textBox1);
            this.panel_numericaldata.Controls.Add(this.label21);
            this.panel_numericaldata.Controls.Add(this.label22);
            this.panel_numericaldata.Controls.Add(this.label_column2);
            this.panel_numericaldata.Controls.Add(this.textBox17);
            this.panel_numericaldata.Controls.Add(this.textBox19);
            this.panel_numericaldata.Controls.Add(this.textBox18);
            this.panel_numericaldata.Controls.Add(this.textBox14);
            this.panel_numericaldata.Controls.Add(this.textBox24);
            this.panel_numericaldata.Controls.Add(this.textBox16);
            this.panel_numericaldata.Controls.Add(this.textBox13);
            this.panel_numericaldata.Controls.Add(this.textBox23);
            this.panel_numericaldata.Controls.Add(this.textBox54);
            this.panel_numericaldata.Controls.Add(this.textBox53);
            this.panel_numericaldata.Controls.Add(this.textBox15);
            this.panel_numericaldata.Controls.Add(this.textBox12);
            this.panel_numericaldata.Controls.Add(this.textBox22);
            this.panel_numericaldata.Controls.Add(this.textBox34);
            this.panel_numericaldata.Controls.Add(this.label11);
            this.panel_numericaldata.Controls.Add(this.label_pressureALT);
            this.panel_numericaldata.Controls.Add(this.label_airspeed);
            this.panel_numericaldata.Controls.Add(this.label_magZ);
            this.panel_numericaldata.Controls.Add(this.label_magY);
            this.panel_numericaldata.Controls.Add(this.label10);
            this.panel_numericaldata.Controls.Add(this.label_magX);
            this.panel_numericaldata.Controls.Add(this.label6);
            this.panel_numericaldata.Controls.Add(this.label1);
            this.panel_numericaldata.Controls.Add(this.label29);
            this.panel_numericaldata.Controls.Add(this.label_gyroZ);
            this.panel_numericaldata.Controls.Add(this.label5);
            this.panel_numericaldata.Controls.Add(this.label_gyroY);
            this.panel_numericaldata.Controls.Add(this.label_gyroX);
            this.panel_numericaldata.Controls.Add(this.label_accZ);
            this.panel_numericaldata.Controls.Add(this.label_accY);
            this.panel_numericaldata.Controls.Add(this.label_accX);
            this.panel_numericaldata.Location = new System.Drawing.Point(229, 12);
            this.panel_numericaldata.Name = "panel_numericaldata";
            this.panel_numericaldata.Size = new System.Drawing.Size(403, 422);
            this.panel_numericaldata.TabIndex = 0;
            // 
            // textBoxRoll
            // 
            this.textBoxRoll.Location = new System.Drawing.Point(55, 131);
            this.textBoxRoll.Name = "textBoxRoll";
            this.textBoxRoll.Size = new System.Drawing.Size(58, 20);
            this.textBoxRoll.TabIndex = 4;
            // 
            // textBox25
            // 
            this.textBox25.Location = new System.Drawing.Point(341, 336);
            this.textBox25.Name = "textBox25";
            this.textBox25.Size = new System.Drawing.Size(50, 20);
            this.textBox25.TabIndex = 1;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label15.Location = new System.Drawing.Point(164, 292);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(69, 13);
            this.label15.TabIndex = 2;
            this.label15.Text = "GPS DATA";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label23.Location = new System.Drawing.Point(164, 228);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(65, 13);
            this.label23.TabIndex = 2;
            this.label23.Text = "AIR DATA";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label9.Location = new System.Drawing.Point(163, 6);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "PIXHAWK DATA";
            // 
            // textBox38
            // 
            this.textBox38.Location = new System.Drawing.Point(289, 258);
            this.textBox38.Name = "textBox38";
            this.textBox38.Size = new System.Drawing.Size(59, 20);
            this.textBox38.TabIndex = 3;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(228, 340);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(100, 13);
            this.label14.TabIndex = 0;
            this.label14.Text = "Ground Track (deg)";
            // 
            // textBox11
            // 
            this.textBox11.Location = new System.Drawing.Point(170, 258);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(59, 20);
            this.textBox11.TabIndex = 3;
            // 
            // textBox41
            // 
            this.textBox41.Location = new System.Drawing.Point(341, 310);
            this.textBox41.Name = "textBox41";
            this.textBox41.Size = new System.Drawing.Size(50, 20);
            this.textBox41.TabIndex = 1;
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(55, 258);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(61, 20);
            this.textBox10.TabIndex = 3;
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(288, 87);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(59, 20);
            this.textBox9.TabIndex = 3;
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(288, 61);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(59, 20);
            this.textBox8.TabIndex = 3;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(289, 35);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(59, 20);
            this.textBox7.TabIndex = 3;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(169, 84);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(59, 20);
            this.textBox6.TabIndex = 3;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(169, 59);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(59, 20);
            this.textBox5.TabIndex = 3;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(169, 33);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(59, 20);
            this.textBox4.TabIndex = 3;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(54, 87);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(59, 20);
            this.textBox3.TabIndex = 3;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(54, 61);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(59, 20);
            this.textBox2.TabIndex = 3;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(54, 36);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(59, 20);
            this.textBox1.TabIndex = 3;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(286, 19);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(75, 13);
            this.label21.TabIndex = 2;
            this.label21.Text = "Magnetometer";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(168, 19);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(58, 13);
            this.label22.TabIndex = 2;
            this.label22.Text = "Gyroscope";
            // 
            // label_column2
            // 
            this.label_column2.AutoSize = true;
            this.label_column2.Location = new System.Drawing.Point(34, 21);
            this.label_column2.Name = "label_column2";
            this.label_column2.Size = new System.Drawing.Size(75, 13);
            this.label_column2.TabIndex = 2;
            this.label_column2.Text = "Accelerometer";
            // 
            // textBox17
            // 
            this.textBox17.Location = new System.Drawing.Point(188, 390);
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new System.Drawing.Size(42, 20);
            this.textBox17.TabIndex = 1;
            // 
            // textBox19
            // 
            this.textBox19.Location = new System.Drawing.Point(236, 390);
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new System.Drawing.Size(25, 20);
            this.textBox19.TabIndex = 1;
            // 
            // textBox18
            // 
            this.textBox18.Location = new System.Drawing.Point(236, 364);
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new System.Drawing.Size(25, 20);
            this.textBox18.TabIndex = 1;
            // 
            // textBox14
            // 
            this.textBox14.Location = new System.Drawing.Point(188, 365);
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new System.Drawing.Size(42, 20);
            this.textBox14.TabIndex = 1;
            // 
            // textBox24
            // 
            this.textBox24.Location = new System.Drawing.Point(180, 336);
            this.textBox24.Name = "textBox24";
            this.textBox24.Size = new System.Drawing.Size(25, 20);
            this.textBox24.TabIndex = 1;
            // 
            // textBox16
            // 
            this.textBox16.Location = new System.Drawing.Point(149, 390);
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new System.Drawing.Size(33, 20);
            this.textBox16.TabIndex = 1;
            // 
            // textBox13
            // 
            this.textBox13.Location = new System.Drawing.Point(149, 365);
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new System.Drawing.Size(33, 20);
            this.textBox13.TabIndex = 1;
            // 
            // textBox23
            // 
            this.textBox23.Location = new System.Drawing.Point(149, 336);
            this.textBox23.Name = "textBox23";
            this.textBox23.Size = new System.Drawing.Size(25, 20);
            this.textBox23.TabIndex = 1;
            // 
            // textBox54
            // 
            this.textBox54.Location = new System.Drawing.Point(180, 313);
            this.textBox54.Name = "textBox54";
            this.textBox54.Size = new System.Drawing.Size(25, 20);
            this.textBox54.TabIndex = 1;
            // 
            // textBox53
            // 
            this.textBox53.Location = new System.Drawing.Point(149, 313);
            this.textBox53.Name = "textBox53";
            this.textBox53.Size = new System.Drawing.Size(25, 20);
            this.textBox53.TabIndex = 1;
            // 
            // textBox15
            // 
            this.textBox15.Location = new System.Drawing.Point(118, 390);
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new System.Drawing.Size(25, 20);
            this.textBox15.TabIndex = 1;
            // 
            // textBox12
            // 
            this.textBox12.Location = new System.Drawing.Point(118, 365);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(25, 20);
            this.textBox12.TabIndex = 1;
            // 
            // textBox22
            // 
            this.textBox22.Location = new System.Drawing.Point(118, 336);
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new System.Drawing.Size(25, 20);
            this.textBox22.TabIndex = 1;
            // 
            // textBox34
            // 
            this.textBox34.Location = new System.Drawing.Point(118, 313);
            this.textBox34.Name = "textBox34";
            this.textBox34.Size = new System.Drawing.Size(25, 20);
            this.textBox34.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(265, 245);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(103, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "Temperature Sensor";
            // 
            // label_pressureALT
            // 
            this.label_pressureALT.AutoSize = true;
            this.label_pressureALT.Location = new System.Drawing.Point(158, 245);
            this.label_pressureALT.Name = "label_pressureALT";
            this.label_pressureALT.Size = new System.Drawing.Size(83, 13);
            this.label_pressureALT.TabIndex = 1;
            this.label_pressureALT.Text = "Static P. Sensor";
            // 
            // label_airspeed
            // 
            this.label_airspeed.AutoSize = true;
            this.label_airspeed.Location = new System.Drawing.Point(35, 242);
            this.label_airspeed.Name = "label_airspeed";
            this.label_airspeed.Size = new System.Drawing.Size(97, 13);
            this.label_airspeed.TabIndex = 1;
            this.label_airspeed.Text = "Dynamic P. Sensor";
            // 
            // label_magZ
            // 
            this.label_magZ.AutoSize = true;
            this.label_magZ.Location = new System.Drawing.Point(244, 90);
            this.label_magZ.Name = "label_magZ";
            this.label_magZ.Size = new System.Drawing.Size(40, 13);
            this.label_magZ.TabIndex = 1;
            this.label_magZ.Text = "magZ :";
            // 
            // label_magY
            // 
            this.label_magY.AutoSize = true;
            this.label_magY.Location = new System.Drawing.Point(244, 64);
            this.label_magY.Name = "label_magY";
            this.label_magY.Size = new System.Drawing.Size(40, 13);
            this.label_magY.TabIndex = 1;
            this.label_magY.Text = "magY :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(228, 317);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(107, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Ground Speed(km/h)";
            // 
            // label_magX
            // 
            this.label_magX.AutoSize = true;
            this.label_magX.Location = new System.Drawing.Point(244, 38);
            this.label_magX.Name = "label_magX";
            this.label_magX.Size = new System.Drawing.Size(40, 13);
            this.label_magX.TabIndex = 1;
            this.label_magX.Text = "magX :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 397);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Longitude";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 373);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Latitude";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(11, 343);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(80, 13);
            this.label29.TabIndex = 0;
            this.label29.Text = "Date(dd-mm-yy)";
            // 
            // label_gyroZ
            // 
            this.label_gyroZ.AutoSize = true;
            this.label_gyroZ.Location = new System.Drawing.Point(125, 87);
            this.label_gyroZ.Name = "label_gyroZ";
            this.label_gyroZ.Size = new System.Drawing.Size(40, 13);
            this.label_gyroZ.TabIndex = 1;
            this.label_gyroZ.Text = "gyroZ :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 320);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "UTC Time(h-min-sec)";
            // 
            // label_gyroY
            // 
            this.label_gyroY.AutoSize = true;
            this.label_gyroY.Location = new System.Drawing.Point(125, 62);
            this.label_gyroY.Name = "label_gyroY";
            this.label_gyroY.Size = new System.Drawing.Size(40, 13);
            this.label_gyroY.TabIndex = 1;
            this.label_gyroY.Text = "gyroY :";
            // 
            // label_gyroX
            // 
            this.label_gyroX.AutoSize = true;
            this.label_gyroX.Location = new System.Drawing.Point(125, 36);
            this.label_gyroX.Name = "label_gyroX";
            this.label_gyroX.Size = new System.Drawing.Size(40, 13);
            this.label_gyroX.TabIndex = 0;
            this.label_gyroX.Text = "gyroX :";
            // 
            // label_accZ
            // 
            this.label_accZ.AutoSize = true;
            this.label_accZ.Location = new System.Drawing.Point(10, 90);
            this.label_accZ.Name = "label_accZ";
            this.label_accZ.Size = new System.Drawing.Size(38, 13);
            this.label_accZ.TabIndex = 0;
            this.label_accZ.Text = "accZ :";
            // 
            // label_accY
            // 
            this.label_accY.AutoSize = true;
            this.label_accY.Location = new System.Drawing.Point(10, 64);
            this.label_accY.Name = "label_accY";
            this.label_accY.Size = new System.Drawing.Size(38, 13);
            this.label_accY.TabIndex = 0;
            this.label_accY.Text = "accY :";
            // 
            // label_accX
            // 
            this.label_accX.AutoSize = true;
            this.label_accX.Location = new System.Drawing.Point(10, 39);
            this.label_accX.Name = "label_accX";
            this.label_accX.Size = new System.Drawing.Size(38, 13);
            this.label_accX.TabIndex = 0;
            this.label_accX.Text = "accX :";
            // 
            // serialPort_IMU
            // 
            this.serialPort_IMU.BaudRate = 115200;
            this.serialPort_IMU.PortName = "COM4";
            this.serialPort_IMU.ReceivedBytesThreshold = 1000;
            this.serialPort_IMU.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort_IMU_DataReceived);
            // 
            // panel_serialport
            // 
            this.panel_serialport.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel_serialport.Controls.Add(this.label3);
            this.panel_serialport.Controls.Add(this.label2);
            this.panel_serialport.Controls.Add(this.button_OpenPort);
            this.panel_serialport.Controls.Add(this.comboBox_baudrate);
            this.panel_serialport.Controls.Add(this.comboBox_Portlist);
            this.panel_serialport.Location = new System.Drawing.Point(29, 12);
            this.panel_serialport.Name = "panel_serialport";
            this.panel_serialport.Size = new System.Drawing.Size(181, 83);
            this.panel_serialport.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(86, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "BaudRate";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "COM Ports";
            // 
            // button_OpenPort
            // 
            this.button_OpenPort.Location = new System.Drawing.Point(6, 46);
            this.button_OpenPort.Name = "button_OpenPort";
            this.button_OpenPort.Size = new System.Drawing.Size(160, 30);
            this.button_OpenPort.TabIndex = 1;
            this.button_OpenPort.Text = "Open Port";
            this.button_OpenPort.UseVisualStyleBackColor = true;
            this.button_OpenPort.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox_baudrate
            // 
            this.comboBox_baudrate.FormattingEnabled = true;
            this.comboBox_baudrate.Items.AddRange(new object[] {
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.comboBox_baudrate.Location = new System.Drawing.Point(89, 22);
            this.comboBox_baudrate.Name = "comboBox_baudrate";
            this.comboBox_baudrate.Size = new System.Drawing.Size(77, 21);
            this.comboBox_baudrate.TabIndex = 0;
            this.comboBox_baudrate.Text = "115200";
            // 
            // comboBox_Portlist
            // 
            this.comboBox_Portlist.FormattingEnabled = true;
            this.comboBox_Portlist.Location = new System.Drawing.Point(6, 23);
            this.comboBox_Portlist.Name = "comboBox_Portlist";
            this.comboBox_Portlist.Size = new System.Drawing.Size(77, 21);
            this.comboBox_Portlist.TabIndex = 0;
            this.comboBox_Portlist.Text = "COM1";
            this.comboBox_Portlist.SelectedIndexChanged += new System.EventHandler(this.comboBox_Portlist_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.listBox_SystemMessages);
            this.panel2.Location = new System.Drawing.Point(29, 104);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(181, 219);
            this.panel2.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(26, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "SYSTEM MESSAGES";
            // 
            // listBox_SystemMessages
            // 
            this.listBox_SystemMessages.FormattingEnabled = true;
            this.listBox_SystemMessages.Location = new System.Drawing.Point(8, 19);
            this.listBox_SystemMessages.Name = "listBox_SystemMessages";
            this.listBox_SystemMessages.Size = new System.Drawing.Size(162, 186);
            this.listBox_SystemMessages.TabIndex = 0;
            // 
            // timer_SeriPort
            // 
            this.timer_SeriPort.Enabled = true;
            this.timer_SeriPort.Interval = 50;
            this.timer_SeriPort.Tick += new System.EventHandler(this.timer_SeriPort_Tick);
            // 
            // textBox20
            // 
            this.textBox20.Location = new System.Drawing.Point(55, 157);
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new System.Drawing.Size(58, 20);
            this.textBox20.TabIndex = 4;
            // 
            // textBox21
            // 
            this.textBox21.Location = new System.Drawing.Point(55, 183);
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new System.Drawing.Size(58, 20);
            this.textBox21.TabIndex = 4;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 535);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel_serialport);
            this.Controls.Add(this.panel_numericaldata);
            this.Name = "MainForm";
            this.Text = "Inertial Measurement Unit Raw Sensor Outputs";
            this.panel_numericaldata.ResumeLayout(false);
            this.panel_numericaldata.PerformLayout();
            this.panel_serialport.ResumeLayout(false);
            this.panel_serialport.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_numericaldata;
        private System.Windows.Forms.Label label_magY;
        private System.Windows.Forms.Label label_magX;
        private System.Windows.Forms.Label label_gyroZ;
        private System.Windows.Forms.Label label_gyroY;
        private System.Windows.Forms.Label label_gyroX;
        private System.Windows.Forms.Label label_accZ;
        private System.Windows.Forms.Label label_accY;
        private System.Windows.Forms.Label label_accX;
        private System.Windows.Forms.Label label_magZ;
        private System.Windows.Forms.Label label_column2;
        private System.Windows.Forms.Label label_airspeed;
        private System.Windows.Forms.Label label_pressureALT;
        private System.IO.Ports.SerialPort serialPort_IMU;
        private System.Windows.Forms.Panel panel_serialport;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_OpenPort;
        private System.Windows.Forms.ComboBox comboBox_Portlist;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox_baudrate;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox34;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox listBox_SystemMessages;
        private System.Windows.Forms.Timer timer_SeriPort;
        private System.Windows.Forms.TextBox textBox38;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBox41;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox textBox54;
        private System.Windows.Forms.TextBox textBox53;
        private System.Windows.Forms.TextBox textBox24;
        private System.Windows.Forms.TextBox textBox23;
        private System.Windows.Forms.TextBox textBox22;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox textBox25;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox textBox17;
        private System.Windows.Forms.TextBox textBox19;
        private System.Windows.Forms.TextBox textBox18;
        private System.Windows.Forms.TextBox textBox14;
        private System.Windows.Forms.TextBox textBox16;
        private System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.TextBox textBox15;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxRoll;
        private System.Windows.Forms.TextBox textBox21;
        private System.Windows.Forms.TextBox textBox20;
    }
}

