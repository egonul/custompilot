using System.Windows.Forms.DataVisualization.Charting;

namespace Tez_v1
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// 
        byte[] seriBuffer ;
        byte header2, header3, header4;

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
            this.textBox21 = new System.Windows.Forms.TextBox();
            this.textBox20 = new System.Windows.Forms.TextBox();
            this.textBox33 = new System.Windows.Forms.TextBox();
            this.textBox26 = new System.Windows.Forms.TextBox();
            this.textBox30 = new System.Windows.Forms.TextBox();
            this.textBox18 = new System.Windows.Forms.TextBox();
            this.textBox17 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox16 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox38 = new System.Windows.Forms.TextBox();
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
            this.label11 = new System.Windows.Forms.Label();
            this.label_pressureALT = new System.Windows.Forms.Label();
            this.label_airspeed = new System.Windows.Forms.Label();
            this.label_magZ = new System.Windows.Forms.Label();
            this.label_magY = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label_magX = new System.Windows.Forms.Label();
            this.label_gyroZ = new System.Windows.Forms.Label();
            this.label_gyroY = new System.Windows.Forms.Label();
            this.label_gyroX = new System.Windows.Forms.Label();
            this.label_accZ = new System.Windows.Forms.Label();
            this.label_accY = new System.Windows.Forms.Label();
            this.label_accX = new System.Windows.Forms.Label();
            this.serialPort_IMU = new System.IO.Ports.SerialPort(this.components);
            this.panel_serialport = new System.Windows.Forms.Panel();
            this.checkBoxIMUData = new System.Windows.Forms.CheckBox();
            this.buttonShowGE = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button_OpenPort = new System.Windows.Forms.Button();
            this.comboBox_baudrate = new System.Windows.Forms.ComboBox();
            this.comboBox_Portlist = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.listBox_SystemMessages = new System.Windows.Forms.ListBox();
            this.pictureBox_attIndicator = new System.Windows.Forms.PictureBox();
            this.pictureBox_asIndicator = new System.Windows.Forms.PictureBox();
            this.timer_SeriPort = new System.Windows.Forms.Timer(this.components);
            this.panel4 = new System.Windows.Forms.Panel();
            this.label20 = new System.Windows.Forms.Label();
            this.textBox48 = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.textBox29 = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.textBox27 = new System.Windows.Forms.TextBox();
            this.textBox28 = new System.Windows.Forms.TextBox();
            this.textBox31 = new System.Windows.Forms.TextBox();
            this.textBox32 = new System.Windows.Forms.TextBox();
            this.textBox35 = new System.Windows.Forms.TextBox();
            this.textBox36 = new System.Windows.Forms.TextBox();
            this.textBox37 = new System.Windows.Forms.TextBox();
            this.textBox39 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.panel_numericaldata.SuspendLayout();
            this.panel_serialport.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_attIndicator)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_asIndicator)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_numericaldata
            // 
            this.panel_numericaldata.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel_numericaldata.Controls.Add(this.textBox21);
            this.panel_numericaldata.Controls.Add(this.textBox20);
            this.panel_numericaldata.Controls.Add(this.textBox33);
            this.panel_numericaldata.Controls.Add(this.textBox26);
            this.panel_numericaldata.Controls.Add(this.textBox30);
            this.panel_numericaldata.Controls.Add(this.textBox18);
            this.panel_numericaldata.Controls.Add(this.textBox17);
            this.panel_numericaldata.Controls.Add(this.label8);
            this.panel_numericaldata.Controls.Add(this.textBox16);
            this.panel_numericaldata.Controls.Add(this.label7);
            this.panel_numericaldata.Controls.Add(this.label12);
            this.panel_numericaldata.Controls.Add(this.label18);
            this.panel_numericaldata.Controls.Add(this.label17);
            this.panel_numericaldata.Controls.Add(this.label28);
            this.panel_numericaldata.Controls.Add(this.label27);
            this.panel_numericaldata.Controls.Add(this.label26);
            this.panel_numericaldata.Controls.Add(this.label9);
            this.panel_numericaldata.Controls.Add(this.label34);
            this.panel_numericaldata.Controls.Add(this.label36);
            this.panel_numericaldata.Controls.Add(this.label33);
            this.panel_numericaldata.Controls.Add(this.label35);
            this.panel_numericaldata.Controls.Add(this.label25);
            this.panel_numericaldata.Controls.Add(this.label32);
            this.panel_numericaldata.Controls.Add(this.label13);
            this.panel_numericaldata.Controls.Add(this.label1);
            this.panel_numericaldata.Controls.Add(this.textBox38);
            this.panel_numericaldata.Controls.Add(this.textBox11);
            this.panel_numericaldata.Controls.Add(this.textBox41);
            this.panel_numericaldata.Controls.Add(this.textBox10);
            this.panel_numericaldata.Controls.Add(this.textBox9);
            this.panel_numericaldata.Controls.Add(this.textBox8);
            this.panel_numericaldata.Controls.Add(this.textBox7);
            this.panel_numericaldata.Controls.Add(this.textBox6);
            this.panel_numericaldata.Controls.Add(this.textBox5);
            this.panel_numericaldata.Controls.Add(this.textBox4);
            this.panel_numericaldata.Controls.Add(this.textBox36);
            this.panel_numericaldata.Controls.Add(this.textBox31);
            this.panel_numericaldata.Controls.Add(this.textBox3);
            this.panel_numericaldata.Controls.Add(this.textBox39);
            this.panel_numericaldata.Controls.Add(this.textBox35);
            this.panel_numericaldata.Controls.Add(this.textBox28);
            this.panel_numericaldata.Controls.Add(this.textBox2);
            this.panel_numericaldata.Controls.Add(this.textBox37);
            this.panel_numericaldata.Controls.Add(this.textBox32);
            this.panel_numericaldata.Controls.Add(this.textBox27);
            this.panel_numericaldata.Controls.Add(this.textBox1);
            this.panel_numericaldata.Controls.Add(this.label21);
            this.panel_numericaldata.Controls.Add(this.label22);
            this.panel_numericaldata.Controls.Add(this.label_column2);
            this.panel_numericaldata.Controls.Add(this.label11);
            this.panel_numericaldata.Controls.Add(this.label_pressureALT);
            this.panel_numericaldata.Controls.Add(this.label_airspeed);
            this.panel_numericaldata.Controls.Add(this.label_magZ);
            this.panel_numericaldata.Controls.Add(this.label_magY);
            this.panel_numericaldata.Controls.Add(this.label10);
            this.panel_numericaldata.Controls.Add(this.label_magX);
            this.panel_numericaldata.Controls.Add(this.label_gyroZ);
            this.panel_numericaldata.Controls.Add(this.label_gyroY);
            this.panel_numericaldata.Controls.Add(this.label_gyroX);
            this.panel_numericaldata.Controls.Add(this.label_accZ);
            this.panel_numericaldata.Controls.Add(this.label_accY);
            this.panel_numericaldata.Controls.Add(this.label_accX);
            this.panel_numericaldata.Location = new System.Drawing.Point(229, 12);
            this.panel_numericaldata.Name = "panel_numericaldata";
            this.panel_numericaldata.Size = new System.Drawing.Size(503, 418);
            this.panel_numericaldata.TabIndex = 0;
            // 
            // textBox21
            // 
            this.textBox21.Location = new System.Drawing.Point(369, 170);
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new System.Drawing.Size(75, 20);
            this.textBox21.TabIndex = 1;
            // 
            // textBox20
            // 
            this.textBox20.Location = new System.Drawing.Point(369, 147);
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new System.Drawing.Size(75, 20);
            this.textBox20.TabIndex = 1;
            // 
            // textBox33
            // 
            this.textBox33.Location = new System.Drawing.Point(369, 124);
            this.textBox33.Name = "textBox33";
            this.textBox33.Size = new System.Drawing.Size(75, 20);
            this.textBox33.TabIndex = 1;
            // 
            // textBox26
            // 
            this.textBox26.Location = new System.Drawing.Point(247, 147);
            this.textBox26.Name = "textBox26";
            this.textBox26.Size = new System.Drawing.Size(77, 20);
            this.textBox26.TabIndex = 1;
            // 
            // textBox30
            // 
            this.textBox30.Location = new System.Drawing.Point(246, 125);
            this.textBox30.Name = "textBox30";
            this.textBox30.Size = new System.Drawing.Size(77, 20);
            this.textBox30.TabIndex = 1;
            // 
            // textBox18
            // 
            this.textBox18.Location = new System.Drawing.Point(403, 84);
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new System.Drawing.Size(60, 20);
            this.textBox18.TabIndex = 1;
            // 
            // textBox17
            // 
            this.textBox17.Location = new System.Drawing.Point(403, 59);
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new System.Drawing.Size(60, 20);
            this.textBox17.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label8.Location = new System.Drawing.Point(342, 177);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(20, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Vd";
            // 
            // textBox16
            // 
            this.textBox16.Location = new System.Drawing.Point(403, 35);
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new System.Drawing.Size(60, 20);
            this.textBox16.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label7.Location = new System.Drawing.Point(342, 154);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(20, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Ve";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label12.Location = new System.Drawing.Point(198, 154);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(51, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "Longitute";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label18.Location = new System.Drawing.Point(342, 131);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(20, 13);
            this.label18.TabIndex = 0;
            this.label18.Text = "Vn";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label17.Location = new System.Drawing.Point(197, 132);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(45, 13);
            this.label17.TabIndex = 0;
            this.label17.Text = "Latitude";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label28.Location = new System.Drawing.Point(368, 90);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(28, 13);
            this.label28.TabIndex = 0;
            this.label28.Text = "Yaw";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(366, 64);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(25, 13);
            this.label27.TabIndex = 0;
            this.label27.Text = "Roll";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(366, 41);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(31, 13);
            this.label26.TabIndex = 0;
            this.label26.Text = "Pitch";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label9.Location = new System.Drawing.Point(183, 7);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "PIXHAWK DATA";
            // 
            // textBox38
            // 
            this.textBox38.Location = new System.Drawing.Point(109, 170);
            this.textBox38.Name = "textBox38";
            this.textBox38.Size = new System.Drawing.Size(69, 20);
            this.textBox38.TabIndex = 3;
            // 
            // textBox11
            // 
            this.textBox11.Location = new System.Drawing.Point(109, 147);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(69, 20);
            this.textBox11.TabIndex = 3;
            // 
            // textBox41
            // 
            this.textBox41.Location = new System.Drawing.Point(115, 196);
            this.textBox41.Name = "textBox41";
            this.textBox41.Size = new System.Drawing.Size(50, 20);
            this.textBox41.TabIndex = 1;
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(109, 124);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(69, 20);
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
            this.label21.Location = new System.Drawing.Point(293, 19);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(51, 13);
            this.label21.TabIndex = 2;
            this.label21.Text = "Magnetic";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(153, 21);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(101, 13);
            this.label22.TabIndex = 2;
            this.label22.Text = "Turn Rate(deg/sec)";
            // 
            // label_column2
            // 
            this.label_column2.AutoSize = true;
            this.label_column2.Location = new System.Drawing.Point(34, 21);
            this.label_column2.Name = "label_column2";
            this.label_column2.Size = new System.Drawing.Size(96, 13);
            this.label_column2.TabIndex = 2;
            this.label_column2.Text = "Acceleration(m/s2)";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 177);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(98, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "Temperature(Cdeg)";
            // 
            // label_pressureALT
            // 
            this.label_pressureALT.AutoSize = true;
            this.label_pressureALT.Location = new System.Drawing.Point(10, 154);
            this.label_pressureALT.Name = "label_pressureALT";
            this.label_pressureALT.Size = new System.Drawing.Size(72, 13);
            this.label_pressureALT.TabIndex = 1;
            this.label_pressureALT.Text = "P. Altitude (m)";
            // 
            // label_airspeed
            // 
            this.label_airspeed.AutoSize = true;
            this.label_airspeed.Location = new System.Drawing.Point(10, 131);
            this.label_airspeed.Name = "label_airspeed";
            this.label_airspeed.Size = new System.Drawing.Size(79, 13);
            this.label_airspeed.TabIndex = 1;
            this.label_airspeed.Text = "Airspeed(km/h)";
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
            this.label10.Location = new System.Drawing.Point(10, 202);
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
            // label_gyroZ
            // 
            this.label_gyroZ.AutoSize = true;
            this.label_gyroZ.Location = new System.Drawing.Point(125, 87);
            this.label_gyroZ.Name = "label_gyroZ";
            this.label_gyroZ.Size = new System.Drawing.Size(40, 13);
            this.label_gyroZ.TabIndex = 1;
            this.label_gyroZ.Text = "gyroZ :";
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
            this.panel_serialport.Controls.Add(this.checkBoxIMUData);
            this.panel_serialport.Controls.Add(this.buttonShowGE);
            this.panel_serialport.Controls.Add(this.label3);
            this.panel_serialport.Controls.Add(this.button1);
            this.panel_serialport.Controls.Add(this.label2);
            this.panel_serialport.Controls.Add(this.button_OpenPort);
            this.panel_serialport.Controls.Add(this.comboBox_baudrate);
            this.panel_serialport.Controls.Add(this.comboBox_Portlist);
            this.panel_serialport.Location = new System.Drawing.Point(29, 12);
            this.panel_serialport.Name = "panel_serialport";
            this.panel_serialport.Size = new System.Drawing.Size(181, 143);
            this.panel_serialport.TabIndex = 1;
            // 
            // checkBoxIMUData
            // 
            this.checkBoxIMUData.AutoSize = true;
            this.checkBoxIMUData.Location = new System.Drawing.Point(90, 110);
            this.checkBoxIMUData.Name = "checkBoxIMUData";
            this.checkBoxIMUData.Size = new System.Drawing.Size(96, 17);
            this.checkBoxIMUData.TabIndex = 17;
            this.checkBoxIMUData.Text = "Only IMU Data";
            this.checkBoxIMUData.UseVisualStyleBackColor = true;
            // 
            // buttonShowGE
            // 
            this.buttonShowGE.Location = new System.Drawing.Point(6, 108);
            this.buttonShowGE.Name = "buttonShowGE";
            this.buttonShowGE.Size = new System.Drawing.Size(75, 23);
            this.buttonShowGE.TabIndex = 10;
            this.buttonShowGE.Text = "Show MAP";
            this.buttonShowGE.UseVisualStyleBackColor = true;
            this.buttonShowGE.Click += new System.EventHandler(this.buttonShowGE_Click);
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
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 82);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Show Graph";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
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
            this.panel2.Location = new System.Drawing.Point(29, 216);
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
            this.listBox_SystemMessages.Size = new System.Drawing.Size(162, 251);
            this.listBox_SystemMessages.TabIndex = 0;
            // 
            // pictureBox_attIndicator
            // 
            this.pictureBox_attIndicator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox_attIndicator.Location = new System.Drawing.Point(261, 436);
            this.pictureBox_attIndicator.Name = "pictureBox_attIndicator";
            this.pictureBox_attIndicator.Size = new System.Drawing.Size(154, 144);
            this.pictureBox_attIndicator.TabIndex = 5;
            this.pictureBox_attIndicator.TabStop = false;
            this.pictureBox_attIndicator.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_attIndicator_Paint);
            // 
            // pictureBox_asIndicator
            // 
            this.pictureBox_asIndicator.Location = new System.Drawing.Point(448, 437);
            this.pictureBox_asIndicator.Name = "pictureBox_asIndicator";
            this.pictureBox_asIndicator.Size = new System.Drawing.Size(150, 143);
            this.pictureBox_asIndicator.TabIndex = 4;
            this.pictureBox_asIndicator.TabStop = false;
            this.pictureBox_asIndicator.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_asIndicator_Paint);
            // 
            // timer_SeriPort
            // 
            this.timer_SeriPort.Enabled = true;
            this.timer_SeriPort.Interval = 50;
            this.timer_SeriPort.Tick += new System.EventHandler(this.timer_SeriPort_Tick);
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.label20);
            this.panel4.Controls.Add(this.textBox48);
            this.panel4.Controls.Add(this.label19);
            this.panel4.Location = new System.Drawing.Point(29, 441);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(181, 139);
            this.panel4.TabIndex = 10;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(16, 33);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(49, 13);
            this.label20.TabIndex = 3;
            this.label20.Text = "AVdd (V)";
            // 
            // textBox48
            // 
            this.textBox48.Location = new System.Drawing.Point(67, 29);
            this.textBox48.Name = "textBox48";
            this.textBox48.Size = new System.Drawing.Size(50, 20);
            this.textBox48.TabIndex = 1;
            this.textBox48.Text = "3.3";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label19.Location = new System.Drawing.Point(36, 9);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(91, 13);
            this.label19.TabIndex = 0;
            this.label19.Text = "PARAMETERS";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label16.ForeColor = System.Drawing.Color.Red;
            this.label16.Location = new System.Drawing.Point(61, 193);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(51, 13);
            this.label16.TabIndex = 1;
            this.label16.Text = "IMU OK";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label30.ForeColor = System.Drawing.Color.Red;
            this.label30.Location = new System.Drawing.Point(125, 193);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(53, 13);
            this.label30.TabIndex = 1;
            this.label30.Text = "GPS OK";
            // 
            // textBox29
            // 
            this.textBox29.Location = new System.Drawing.Point(112, 161);
            this.textBox29.Name = "textBox29";
            this.textBox29.Size = new System.Drawing.Size(85, 20);
            this.textBox29.TabIndex = 11;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(34, 168);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(70, 13);
            this.label31.TabIndex = 1;
            this.label31.Text = "Syst. Counter";
            // 
            // textBox27
            // 
            this.textBox27.Location = new System.Drawing.Point(66, 237);
            this.textBox27.Name = "textBox27";
            this.textBox27.Size = new System.Drawing.Size(59, 20);
            this.textBox27.TabIndex = 3;
            // 
            // textBox28
            // 
            this.textBox28.Location = new System.Drawing.Point(66, 262);
            this.textBox28.Name = "textBox28";
            this.textBox28.Size = new System.Drawing.Size(59, 20);
            this.textBox28.TabIndex = 3;
            // 
            // textBox31
            // 
            this.textBox31.Location = new System.Drawing.Point(66, 288);
            this.textBox31.Name = "textBox31";
            this.textBox31.Size = new System.Drawing.Size(59, 20);
            this.textBox31.TabIndex = 3;
            // 
            // textBox32
            // 
            this.textBox32.Location = new System.Drawing.Point(185, 237);
            this.textBox32.Name = "textBox32";
            this.textBox32.Size = new System.Drawing.Size(59, 20);
            this.textBox32.TabIndex = 3;
            // 
            // textBox35
            // 
            this.textBox35.Location = new System.Drawing.Point(185, 262);
            this.textBox35.Name = "textBox35";
            this.textBox35.Size = new System.Drawing.Size(59, 20);
            this.textBox35.TabIndex = 3;
            // 
            // textBox36
            // 
            this.textBox36.Location = new System.Drawing.Point(185, 288);
            this.textBox36.Name = "textBox36";
            this.textBox36.Size = new System.Drawing.Size(59, 20);
            this.textBox36.TabIndex = 3;
            // 
            // textBox37
            // 
            this.textBox37.Location = new System.Drawing.Point(317, 237);
            this.textBox37.Name = "textBox37";
            this.textBox37.Size = new System.Drawing.Size(59, 20);
            this.textBox37.TabIndex = 3;
            // 
            // textBox39
            // 
            this.textBox39.Location = new System.Drawing.Point(317, 262);
            this.textBox39.Name = "textBox39";
            this.textBox39.Size = new System.Drawing.Size(59, 20);
            this.textBox39.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 244);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "RC Ch1:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(13, 269);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(47, 13);
            this.label13.TabIndex = 0;
            this.label13.Text = "RC Ch2:";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(13, 295);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(47, 13);
            this.label25.TabIndex = 0;
            this.label25.Text = "RC Ch3:";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(134, 244);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(47, 13);
            this.label32.TabIndex = 0;
            this.label32.Text = "RC Ch4:";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(134, 269);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(47, 13);
            this.label33.TabIndex = 0;
            this.label33.Text = "RC Ch5:";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(134, 295);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(47, 13);
            this.label34.TabIndex = 0;
            this.label34.Text = "RC Ch6:";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(264, 244);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(47, 13);
            this.label35.TabIndex = 0;
            this.label35.Text = "RC Ch7:";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(264, 269);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(47, 13);
            this.label36.TabIndex = 0;
            this.label36.Text = "RC Ch8:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 626);
            this.Controls.Add(this.textBox29);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.pictureBox_attIndicator);
            this.Controls.Add(this.pictureBox_asIndicator);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel_serialport);
            this.Controls.Add(this.panel_numericaldata);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.label16);
            this.Name = "MainForm";
            this.Text = "Inertial Navigation System---Ersin GÖNÜL";
            this.panel_numericaldata.ResumeLayout(false);
            this.panel_numericaldata.PerformLayout();
            this.panel_serialport.ResumeLayout(false);
            this.panel_serialport.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_attIndicator)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_asIndicator)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox listBox_SystemMessages;
        private System.Windows.Forms.PictureBox pictureBox_attIndicator;
        private System.Windows.Forms.PictureBox pictureBox_asIndicator;
        private System.Windows.Forms.Timer timer_SeriPort;
        private System.Windows.Forms.TextBox textBox38;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox41;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox textBox48;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Button buttonShowGE;
        private System.Windows.Forms.CheckBox checkBoxIMUData;
        private System.Windows.Forms.TextBox textBox18;
        private System.Windows.Forms.TextBox textBox17;
        private System.Windows.Forms.TextBox textBox16;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox textBox33;
        private System.Windows.Forms.TextBox textBox30;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox textBox21;
        private System.Windows.Forms.TextBox textBox20;
        private System.Windows.Forms.TextBox textBox26;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox textBox29;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox textBox36;
        private System.Windows.Forms.TextBox textBox31;
        private System.Windows.Forms.TextBox textBox39;
        private System.Windows.Forms.TextBox textBox35;
        private System.Windows.Forms.TextBox textBox28;
        private System.Windows.Forms.TextBox textBox37;
        private System.Windows.Forms.TextBox textBox32;
        private System.Windows.Forms.TextBox textBox27;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label25;
    }
}

