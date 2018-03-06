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
    public partial class Graphs_Form : Form
    {
        public Graphs_Form()
        {
            InitializeComponent();

            MainForm.Seri1 = chart1.Series.Add("Accx");
            MainForm.Seri1.ChartType = SeriesChartType.FastLine;
            MainForm.Seri1.BorderWidth = 2;

            MainForm.Seri2 = chart2.Series.Add("AccY");
            MainForm.Seri2.ChartType = SeriesChartType.FastLine;
            MainForm.Seri2.BorderWidth = 2;

            MainForm.Seri3 = chart3.Series.Add("AccZ");
            MainForm.Seri3.ChartType = SeriesChartType.FastLine;
            MainForm.Seri3.BorderWidth = 2;

            MainForm.Seri4 = chart4.Series.Add("GyroX");
            MainForm.Seri4.ChartType = SeriesChartType.FastLine;
            MainForm.Seri4.BorderWidth = 2;

            MainForm.Seri5 = chart5.Series.Add("GyroY");
            MainForm.Seri5.ChartType = SeriesChartType.FastLine;
            MainForm.Seri5.BorderWidth = 2;

            MainForm.Seri6 = chart6.Series.Add("GyroZ");
            MainForm.Seri6.ChartType = SeriesChartType.FastLine;
            MainForm.Seri6.BorderWidth = 2;


            MainForm.Seri7 = chart7.Series.Add("Roll");
            MainForm.Seri7.ChartType = SeriesChartType.FastLine;
            MainForm.Seri7.BorderWidth = 2;

            MainForm.Seri8 = chart8.Series.Add("Pitch");
            MainForm.Seri8.ChartType = SeriesChartType.FastLine;
            MainForm.Seri8.BorderWidth = 2;

            MainForm.Seri9 = chart9.Series.Add("Yaw");
            MainForm.Seri9.ChartType = SeriesChartType.FastLine;
            MainForm.Seri9.BorderWidth = 2;


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MainForm.minValue = MainForm.sample_num - 80;
            MainForm.maxValue = MainForm.sample_num + 20;
            MainForm.sample_num++;
            chart1.ChartAreas[0].AxisX.Minimum = MainForm.minValue;
            chart1.ChartAreas[0].AxisX.Maximum = MainForm.maxValue;
            MainForm.Seri1.Points.AddXY(MainForm.sample_num, MainForm.imu_data.accX);
           
            chart2.ChartAreas[0].AxisX.Minimum = MainForm.minValue;
            chart2.ChartAreas[0].AxisX.Maximum = MainForm.maxValue;
            MainForm.Seri2.Points.AddXY(MainForm.sample_num, MainForm.imu_data.accY);
            
            chart3.ChartAreas[0].AxisX.Minimum = MainForm.minValue;
            chart3.ChartAreas[0].AxisX.Maximum = MainForm.maxValue;
            MainForm.Seri3.Points.AddXY(MainForm.sample_num, MainForm.imu_data.accZ);

            chart4.ChartAreas[0].AxisX.Minimum = MainForm.minValue;
            chart4.ChartAreas[0].AxisX.Maximum = MainForm.maxValue;
            MainForm.Seri4.Points.AddXY(MainForm.sample_num, MainForm.imu_data.gyroX);

            chart5.ChartAreas[0].AxisX.Minimum = MainForm.minValue;
            chart5.ChartAreas[0].AxisX.Maximum = MainForm.maxValue;
            MainForm.Seri5.Points.AddXY(MainForm.sample_num, MainForm.imu_data.gyroY);

            chart6.ChartAreas[0].AxisX.Minimum = MainForm.minValue;
            chart6.ChartAreas[0].AxisX.Maximum = MainForm.maxValue;
            MainForm.Seri6.Points.AddXY(MainForm.sample_num, MainForm.imu_data.gyroZ);

            chart7.ChartAreas[0].AxisX.Minimum = MainForm.minValue;
            chart7.ChartAreas[0].AxisX.Maximum = MainForm.maxValue;
            MainForm.Seri7.Points.AddXY(MainForm.sample_num, MainForm.imu_data.roll);

            chart8.ChartAreas[0].AxisX.Minimum = MainForm.minValue;
            chart8.ChartAreas[0].AxisX.Maximum = MainForm.maxValue;
            MainForm.Seri8.Points.AddXY(MainForm.sample_num, MainForm.imu_data.pitch);

            chart9.ChartAreas[0].AxisX.Minimum = MainForm.minValue;
            chart9.ChartAreas[0].AxisX.Maximum = MainForm.maxValue;
            MainForm.Seri9.Points.AddXY(MainForm.sample_num, MainForm.imu_data.yaw);





        }
    }
}