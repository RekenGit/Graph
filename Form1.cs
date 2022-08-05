using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graph
{
    public partial class Form1 : Form
    {
        List<string> lines = File.ReadAllLines("Archiwum.txt").ToList();
        string[] miesiace;
        int liczbaLini = 0;
        int liczbaMiech = 1;
        int suma = 0;
        int ZaznaczonyMiech;
        int ZaznaczonyMiech2=0;
        int ZaznaczonyMiech3=0;
        bool pRaz = true;

        public Form1()
        {
            InitializeComponent();
            liczbaLini += (from string line in lines select line).Count();
            miesiace = new string[liczbaLini];

            for (int i = 0; i < liczbaLini; i++)
            {
                string Sprawdz = "";
                int j = 0;
                while (j < liczbaMiech)
                {
                    if (pRaz)
                    {
                        liczbaMiech = 0;
                        pRaz = false;
                    }
                    if (lines[i].Substring(3, 7) != miesiace[j])
                    {
                        Sprawdz +="0";
                    }
                    else
                    {
                        Sprawdz += "1";
                    }
                    j++;
                }
                if (int.Parse(Sprawdz) == 0)
                {
                    comboBox1.Items.Add(lines[i].Substring(3, 7));
                    comboBox2.Items.Add(lines[i].Substring(3, 7));
                    comboBox3.Items.Add(lines[i].Substring(3, 7));
                    miesiace[liczbaMiech] = lines[i].Substring(3, 7);
                    liczbaMiech++;
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ZaznaczonyMiech = comboBox1.SelectedIndex;
            ZaznaczonyMiech2 = comboBox2.SelectedIndex;
            ZaznaczonyMiech3 = comboBox3.SelectedIndex;
            chart0.Series["Series1"].Points.Clear();
            chart1.Series["Series1"].Points.Clear();
            chart2.Series["1"].Points.Clear();
            chart2.Series["2"].Points.Clear();
            chart2.Series["3"].Points.Clear();
            suma = 0;

            WczytajMiech(ZaznaczonyMiech);
            chart0.Series["Series1"].LegendText = "#PERCENT";
            if (ZaznaczonyMiech2 != 0) WczytajKolejny(ZaznaczonyMiech2-1, "2");
            if (ZaznaczonyMiech3 != 0) WczytajKolejny(ZaznaczonyMiech3-1, "3");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            chart0.Series["Series1"].Points.Clear();
            chart1.Series["Series1"].Points.Clear();
            chart2.Series["1"].Points.Clear();
            chart2.Series["2"].Points.Clear();
            chart2.Series["3"].Points.Clear();
            suma = 0;
            WczytajAll();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var data = DateTime.Now;
            int tyg = (int) data.DayOfWeek;
            string tygg = "xx";

            if (tyg == 1) tygg = "po";
            if (tyg == 2) tygg = "wt";
            if (tyg == 3) tygg = "sr";
            if (tyg == 4) tygg = "cz";
            if (tyg == 5) tygg = "pi";
            if (tyg == 6) tygg = "so";
            if (tyg == 0) tygg = "ni";

            lines.Add(data.ToString("dd.MM.yyyy HH:mm ") + tygg);
            File.WriteAllLines("Archiwum.txt", lines);
            ///////
            liczbaLini = 0;
            liczbaMiech = 1;
            suma = 0;
            miesiace = null;
            pRaz = true;

            label1.Text = "";
            label2.Text = "";
            label3.Text = "";
            label4.Text = "";
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox2.Items.Add("Nic");
            comboBox3.Items.Add("Nic");
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            chart0.Series["Series1"].Points.Clear();
            chart1.Series["Series1"].Points.Clear();
            chart2.Series["1"].Points.Clear();
            chart2.Series["2"].Points.Clear();
            chart2.Series["3"].Points.Clear();

            lines = File.ReadAllLines("Archiwum.txt").ToList();
            liczbaLini += (from string line in lines select line).Count();
            miesiace = new string[liczbaLini];
            for (int i = 0; i < liczbaLini; i++)
            {
                string Sprawdz = "";
                int j = 0;
                while (j < liczbaMiech)
                {
                    if (pRaz)
                    {
                        liczbaMiech = 0;
                        pRaz = false;
                    }
                    if (lines[i].Substring(3, 7) != miesiace[j])
                    {
                        Sprawdz += "0";
                    }
                    else
                    {
                        Sprawdz += "1";
                    }
                    j++;
                }
                if (int.Parse(Sprawdz) == 0)
                {
                    comboBox1.Items.Add(lines[i].Substring(3, 7));
                    comboBox2.Items.Add(lines[i].Substring(3, 7));
                    comboBox3.Items.Add(lines[i].Substring(3, 7));
                    miesiace[liczbaMiech] = lines[i].Substring(3, 7);
                    liczbaMiech++;
                }
            }
        }

        string Dzien(int x)
        {
            if (x < 10)
            {
                return "0" + x;
            }
            else
            {
                return "" + x;
            }
        }

        void WczytajMiech(int M)
        {
            int pon = 0, wto = 0, sro = 0, czw = 0, pia = 0, sob = 0, nie = 0;
            int[] g = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };//24
            int[] d = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; //31

            for (int i = 0; i < liczbaLini; i++)
            {
                string x = lines[i];
                //string rok = x.Substring(0, 10);
                string godzina = x.Substring(11, 2);
                //string minuta = x.Substring(14, 2);
                string tydzien = x.Substring(17, 2);
                string dzien = x.Substring(0, 2);
                string miech = x.Substring(3, 7);

                if (M != -1 && miech == miesiace[M])
                {
                    for (int j = 0; j < 24; j++)
                    {
                        if (godzina == j + "" || godzina == "0" + j) g[j]++;
                    }

                    for (int j = 0; j < 31; j++)
                    {
                        if (int.Parse(dzien) == j + 1)
                        {
                            d[j]++;
                            suma++;
                        }
                    }

                    switch (tydzien)
                    {
                        case "po": pon++; break;
                        case "wt": wto++; break;
                        case "sr": sro++; break;
                        case "cz": czw++; break;
                        case "pi": pia++; break;
                        case "so": sob++; break;
                        case "ni": nie++; break;
                    }

                    chart2.Series["1"].LegendText = miech;
                    label1.Text = miech;
                    label2.Text = miech;
                }
            }
            float mx = 0;
            for (int i = 0; i < 30; i++) mx += d[i];
            label4.Text = String.Format("{0:N2}", mx /31) + " razy dziennie";
            label3.Text = suma + " w ciągu miesiąca";

            for (int i = 0; i < 31; i++)
            {
                chart2.Series["1"].Points.AddXY(Dzien(i+1), d[i]);
            }

            if (pon != 0) chart0.Series["Series1"].Points.AddXY("Pon", pon);
            if (wto != 0) chart0.Series["Series1"].Points.AddXY("Wto", wto);
            if (sro != 0) chart0.Series["Series1"].Points.AddXY("Śro", sro);
            if (czw != 0) chart0.Series["Series1"].Points.AddXY("Czw", czw);
            if (pia != 0) chart0.Series["Series1"].Points.AddXY("Pią", pia);
            if (sob != 0) chart0.Series["Series1"].Points.AddXY("Sob", sob);
            if (nie != 0) chart0.Series["Series1"].Points.AddXY("Nie", nie);

            for (int i = 0; i < 24; i++)
            {
                if (g[i] != 0)
                {
                    chart1.Series["Series1"].Points.AddXY(i + ":00", g[i]);
                }
            }
        }

        void WczytajAll()
        {
            int pon = 0, wto = 0, sro = 0, czw = 0, pia = 0, sob = 0, nie = 0;
            int[] g = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };//24
            int[] d = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; //31


            label4.Text = null;
            label3.Text = null;
            label1.Text = null;
            label2.Text = null;
            chart2.Series["1"].LegendText = null;
            for (int M = 0; M < liczbaMiech; M++)
            {
                for (int i = 0; i < liczbaLini; i++)
                {
                    string x = lines[i];
                    string godzina = x.Substring(11, 2);
                    string tydzien = x.Substring(17, 2);
                    string dzien = x.Substring(0, 2);
                    string miech = x.Substring(3, 7);

                    if (M != -1 && miech == miesiace[M])
                    {
                        for (int j = 0; j < 24; j++)
                        {
                            if (godzina == j + "" || godzina == "0" + j) g[j]++;
                        }

                        for (int j = 0; j < 31; j++)
                        {
                            if (int.Parse(dzien) == j + 1)
                            {
                                d[j]++;
                                suma++;
                            }
                        }

                        switch (tydzien)
                        {
                            case "po": pon++; break;
                            case "wt": wto++; break;
                            case "sr": sro++; break;
                            case "cz": czw++; break;
                            case "pi": pia++; break;
                            case "so": sob++; break;
                            case "ni": nie++; break;
                        }

                        chart2.Series["1"].LegendText = miech;
                        label1.Text = "Suma miesięcy";
                        label2.Text = "Suma miesięcy";
                    }
                }
                float mx = 0;
                for (int i = 0; i < 30; i++) mx += d[i];
                label4.Text = String.Format("{0:N2}", mx / (31 * (M+1))) + " razy dziennie";
                label3.Text = suma + " w ciągu "+ (M + 1) + " miesięcy";
            }
            if (pon != 0) chart0.Series["Series1"].Points.AddXY("Pon", pon);
            if (wto != 0) chart0.Series["Series1"].Points.AddXY("Wto", wto);
            if (sro != 0) chart0.Series["Series1"].Points.AddXY("Śro", sro);
            if (czw != 0) chart0.Series["Series1"].Points.AddXY("Czw", czw);
            if (pia != 0) chart0.Series["Series1"].Points.AddXY("Pią", pia);
            if (sob != 0) chart0.Series["Series1"].Points.AddXY("Sob", sob);
            if (nie != 0) chart0.Series["Series1"].Points.AddXY("Nie", nie);
            for (int i = 0; i < 24; i++)
            {
                if (g[i] != 0) chart1.Series["Series1"].Points.AddXY(i + ":00", g[i]);
            }
            for (int i = 0; i < 31; i++)
            {
                chart2.Series["1"].Points.AddXY(Dzien(i + 1), d[i]);
            }
        }

        void WczytajKolejny(int M, string ktory)
        {
            int[] d = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; //31

            for (int i = 0; i < liczbaLini; i++)
            {
                string x = lines[i];
                string dzien = x.Substring(0, 2);
                string miech = x.Substring(3, 7);

                if (M != -2 && M != -1 && miech == miesiace[M])
                {
                    for (int j = 0; j < 31; j++)
                    {
                        if (int.Parse(dzien) == j+1) d[j]++;
                    }

                    chart2.Series[ktory].LegendText = miech;
                }
            }

            for (int i = 0; i < 31; i++)
            {
                chart2.Series[ktory].Points.AddXY(Dzien(i), d[i]);
            }
        }
    }
}
