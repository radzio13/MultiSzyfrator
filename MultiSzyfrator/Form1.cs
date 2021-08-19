using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace MultiSzyfrator
{
    public partial class Form1 : Form
    {
        MultiSzyfrator ms = new MultiSzyfrator();
        public Form1()
        {
            InitializeComponent();
            //TrueRotor.generuj_i_wypiszRotor();
        }

        private void bSzyfruj_Click(object sender, EventArgs e)
        {
            ms.setMetoda((MetodaID)  Enum.Parse(typeof(MetodaID), cbMetoda.Text));
            ms.setKlucz(tbKSzyfrowania.Text);
            tbTZaszyfrowany.Text = ms.szyfruj(tbTOtwarty.Text);
        }

        private void bOdszyfruj_Click(object sender, EventArgs e)
        {
            ms.setMetoda((MetodaID)Enum.Parse(typeof(MetodaID), cbMetoda.Text));
            ms.setKlucz(tbKOdszyfrowania.Text);
            tbTOdszyfrowany.Text = ms.deszyfruj(tbTZaszyfrowany.Text);
        }

        private void cbMetoda_SelectedIndexChanged(object sender, EventArgs e)
        {
            string wybranaMetoda = (string)cbMetoda.SelectedItem;
            if (wybranaMetoda == "SprawdzeniePESEL")
            {
                tbKSzyfrowania.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                tbKOdszyfrowania.Visible = false;
                bSzyfruj.Text = "Generuj 11 cyfre";
                bOdszyfruj.Text = "Sprawdź poprawność";
                label1.Text = "10 cyfr PESEL:";
                label2.Text = "PESEL cały:";
                label3.Text = "Poprawny?";
                tbTOtwarty.Text = "8401139741";
                bSzyfruj.AutoSize = false;
            }
            else if (wybranaMetoda == "SprawdzenieIBAN")
            {
                tbKSzyfrowania.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                tbKOdszyfrowania.Visible = false;
                bSzyfruj.Text = "Generuj cały IBAN";
                bOdszyfruj.Text = "Sprawdź poprawność";
                label1.Text = "IBAN z nieznanymi cyframi kontrolnymi:";
                label2.Text = "IBAN cały:";
                label3.Text = "Poprawny?";
                tbTOtwarty.Text = "PL 00 1140 2004 0000 1234 5678 9012";
                bSzyfruj.AutoSize = false;
            }
            else if (wybranaMetoda == "KodHamminga")
            {
                tbKSzyfrowania.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                tbKOdszyfrowania.Visible = false;
                bSzyfruj.Text = "Generuj Kody Parzystości";
                bOdszyfruj.Text = "Weryfikuj dane";
                label1.Text = "Data:";
                label2.Text = "Received word:";
                label3.Text = "Corrected word";
                tbTOtwarty.Text = "0101";
                bSzyfruj.AutoSize = true;
            }
            else
            {
                tbKSzyfrowania.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                tbKOdszyfrowania.Visible = true;
                bSzyfruj.Text = "Szyfruj!";
                bOdszyfruj.Text = "Odszyfruj!";
                label1.Text = "Tekst otwarty:";
                label2.Text = "Tekst zaszyfrowany:";
                label3.Text = "Tekst odszyfrowany:";
                bSzyfruj.AutoSize = false;
                if (wybranaMetoda == "PlayFair")
                {
                    tbTOtwarty.Text = "Uniwersytett";
                }
                else if (wybranaMetoda == "SzyfrVigenere")
                {
                    tbKSzyfrowania.Text = "BARAN";
                    tbTOtwarty.Text = "ABERACJA";
                    tbKOdszyfrowania.Text = "BARAN";
                }
                else if (wybranaMetoda == "SzyfrHomofoniczny")
                {
                    tbKSzyfrowania.Text = "KZ(5bN8#4T_W*/c\\@6+[QVg-mMP'ke^X2OnLjH\"E$Aa,YJlS:fC9)D0di1`7BG?IU;R<3%hF.>&=]!";
                    tbKOdszyfrowania.Text = "KZ(5bN8#4T_W*/c\\@6+[QVg-mMP'ke^X2OnLjH\"E$Aa,YJlS:fC9)D0di1`7BG?IU;R<3%hF.>&=]!";
                    tbTOtwarty.Text = "ABERACJA";
                }
                else if (wybranaMetoda == "RSA")
                {
                    tbKSzyfrowania.Text = "7, 589";
                    tbKOdszyfrowania.Text = "463, 589";
                    tbTOtwarty.Text = "Ala ma kota";
                }
                else
                {
                    tbTOtwarty.Text = "Najlepsze kasztany rosna na placu Pigalle";
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;

                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
            }


            string wiadomosc;
            wiadomosc = fileContent;
            //wiadomosc = Regex.Replace(wiadomosc, " ", "");
            wiadomosc = wiadomosc.ToUpper();
            //Console.WriteLine(wiadomosc);

            char[] litery = new char[35];
            litery[0] = 'A';
            litery[1] = 'Ą';
            litery[2] = 'B';
            litery[3] = 'C';
            litery[4] = 'Ć';
            litery[5] = 'D';
            litery[6] = 'E';
            litery[7] = 'Ę';
            litery[8] = 'F';
            litery[9] = 'G';
            litery[10] = 'H';
            litery[11] = 'I';
            litery[12] = 'J';
            litery[13] = 'K';
            litery[14] = 'L';
            litery[15] = 'Ł';
            litery[16] = 'M';
            litery[17] = 'N';
            litery[18] = 'Ń';
            litery[19] = 'O';
            litery[20] = 'Ó';
            litery[21] = 'P';
            litery[22] = 'Q';
            litery[23] = 'R';
            litery[24] = 'S';
            litery[25] = 'Ś';
            litery[26] = 'T';
            litery[27] = 'U';
            litery[28] = 'V';
            litery[29] = 'W';
            litery[30] = 'X';
            litery[31] = 'Y';
            litery[32] = 'Z';
            litery[33] = 'Ź';
            litery[34] = 'Ż';

            int[] liczba_liter = new int[35];

            foreach (char cc in wiadomosc)
            {
                for (int i = 0; i < 35; i++)
                {
                    if (cc == litery[i])
                    {
                        liczba_liter[i]++;
                    }
                }

            }
            int suma_wszystkich_liter = 0;
            for (int i = 0; i < 35; i++)
            {
                Console.WriteLine(litery[i] + " " + liczba_liter[i]);
                suma_wszystkich_liter += liczba_liter[i];
            }

            double[] czestosc_wg_korpusjp = new double[35];
            czestosc_wg_korpusjp[0] = 8.91;
            czestosc_wg_korpusjp[1] = 0.99;
            czestosc_wg_korpusjp[2] = 1.47;
            czestosc_wg_korpusjp[3] = 3.96;
            czestosc_wg_korpusjp[4] = 0.40;
            czestosc_wg_korpusjp[5] = 3.25;
            czestosc_wg_korpusjp[6] = 7.66;
            czestosc_wg_korpusjp[7] = 1.11;
            czestosc_wg_korpusjp[8] = 0.30;
            czestosc_wg_korpusjp[9] = 1.42;
            czestosc_wg_korpusjp[10] = 1.08;
            czestosc_wg_korpusjp[11] = 8.21;
            czestosc_wg_korpusjp[12] = 2.28;
            czestosc_wg_korpusjp[13] = 3.51;
            czestosc_wg_korpusjp[14] = 2.10;
            czestosc_wg_korpusjp[15] = 1.82;
            czestosc_wg_korpusjp[16] = 2.80;
            czestosc_wg_korpusjp[17] = 5.52;
            czestosc_wg_korpusjp[18] = 0.20;
            czestosc_wg_korpusjp[19] = 7.75;
            czestosc_wg_korpusjp[20] = 0.85;
            czestosc_wg_korpusjp[21] = 3.13;
            czestosc_wg_korpusjp[22] = 0.14;
            czestosc_wg_korpusjp[23] = 4.69;
            czestosc_wg_korpusjp[24] = 4.32;
            czestosc_wg_korpusjp[25] = 0.66;
            czestosc_wg_korpusjp[26] = 3.98;
            czestosc_wg_korpusjp[27] = 2.50;
            czestosc_wg_korpusjp[28] = 0.04;
            czestosc_wg_korpusjp[29] = 4.65;
            czestosc_wg_korpusjp[30] = 0.02;
            czestosc_wg_korpusjp[31] = 3.76;
            czestosc_wg_korpusjp[32] = 5.64;
            czestosc_wg_korpusjp[33] = 0.06;
            czestosc_wg_korpusjp[34] = 0.83;

            for (int i = 0; i < 35; i++)
            {
                chart1.Series[0].Points.AddXY(litery[i].ToString(), (Convert.ToDouble(liczba_liter[i]) / suma_wszystkich_liter) * 100);
                chart1.Series[1].Points.AddXY(litery[i].ToString(), czestosc_wg_korpusjp[i]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;

                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
            }

            string wiadomosc;
            wiadomosc = fileContent;
            //wiadomosc = Regex.Replace(wiadomosc, " ", "");
            wiadomosc = wiadomosc.ToUpper();
            Console.WriteLine(wiadomosc);

            char[] litery = new char[35];
            litery[0] = 'A';
            litery[1] = 'Ą';
            litery[2] = 'B';
            litery[3] = 'C';
            litery[4] = 'Ć';
            litery[5] = 'D';
            litery[6] = 'E';
            litery[7] = 'Ę';
            litery[8] = 'F';
            litery[9] = 'G';
            litery[10] = 'H';
            litery[11] = 'I';
            litery[12] = 'J';
            litery[13] = 'K';
            litery[14] = 'L';
            litery[15] = 'Ł';
            litery[16] = 'M';
            litery[17] = 'N';
            litery[18] = 'Ń';
            litery[19] = 'O';
            litery[20] = 'Ó';
            litery[21] = 'P';
            litery[22] = 'Q';
            litery[23] = 'R';
            litery[24] = 'S';
            litery[25] = 'Ś';
            litery[26] = 'T';
            litery[27] = 'U';
            litery[28] = 'V';
            litery[29] = 'W';
            litery[30] = 'X';
            litery[31] = 'Y';
            litery[32] = 'Z';
            litery[33] = 'Ź';
            litery[34] = 'Ż';

            int[] liczba_liter = new int[35];

            foreach (char cc in wiadomosc)
            {
                for (int i = 0; i < 35; i++)
                {
                    if (cc == litery[i])
                    {
                        liczba_liter[i]++;
                    }
                }

            }
            int suma_wszystkich_liter = 0;
            for (int i = 0; i < 35; i++)
            {
                Console.WriteLine(litery[i] + " " + liczba_liter[i]);
                suma_wszystkich_liter += liczba_liter[i];
            }

            double[] czestosc_wg_korpusjp = new double[35];
            czestosc_wg_korpusjp[0] = 8.91;
            czestosc_wg_korpusjp[1] = 0.99;
            czestosc_wg_korpusjp[2] = 1.47;
            czestosc_wg_korpusjp[3] = 3.96;
            czestosc_wg_korpusjp[4] = 0.40;
            czestosc_wg_korpusjp[5] = 3.25;
            czestosc_wg_korpusjp[6] = 7.66;
            czestosc_wg_korpusjp[7] = 1.11;
            czestosc_wg_korpusjp[8] = 0.30;
            czestosc_wg_korpusjp[9] = 1.42;
            czestosc_wg_korpusjp[10] = 1.08;
            czestosc_wg_korpusjp[11] = 8.21;
            czestosc_wg_korpusjp[12] = 2.28;
            czestosc_wg_korpusjp[13] = 3.51;
            czestosc_wg_korpusjp[14] = 2.10;
            czestosc_wg_korpusjp[15] = 1.82;
            czestosc_wg_korpusjp[16] = 2.80;
            czestosc_wg_korpusjp[17] = 5.52;
            czestosc_wg_korpusjp[18] = 0.20;
            czestosc_wg_korpusjp[19] = 7.75;
            czestosc_wg_korpusjp[20] = 0.85;
            czestosc_wg_korpusjp[21] = 3.13;
            czestosc_wg_korpusjp[22] = 0.14;
            czestosc_wg_korpusjp[23] = 4.69;
            czestosc_wg_korpusjp[24] = 4.32;
            czestosc_wg_korpusjp[25] = 0.66;
            czestosc_wg_korpusjp[26] = 3.98;
            czestosc_wg_korpusjp[27] = 2.50;
            czestosc_wg_korpusjp[28] = 0.04;
            czestosc_wg_korpusjp[29] = 4.65;
            czestosc_wg_korpusjp[30] = 0.02;
            czestosc_wg_korpusjp[31] = 3.76;
            czestosc_wg_korpusjp[32] = 5.64;
            czestosc_wg_korpusjp[33] = 0.06;
            czestosc_wg_korpusjp[34] = 0.83;

            for (int i = 0; i < 35; i++)
            {
                chart1.Series[0].Points.AddXY(litery[i].ToString(), (Convert.ToDouble(liczba_liter[i]) / suma_wszystkich_liter) * 100);
                chart1.Series[1].Points.AddXY(litery[i].ToString(), czestosc_wg_korpusjp[i]);
            }

        }
    }

    enum MetodaID {Cezar, Midnight, MacierzProstokatna, Enigma, Rotor, TrueRotor, PlayFair, SprawdzeniePESEL, 
        SprawdzenieIBAN, SzyfrVigenere, SzyfrHomofoniczny, KodHamminga, RSA};

    class MultiSzyfrator
    {
        private MetodaSzyfrowania metoda;
        public static List<string> klucz = new List<string>();

        public string szyfruj(string ss)
        {
            return metoda.szyfruj(ss);
        }

        public string deszyfruj(string ss)
        {
            return metoda.deszyfruj(ss);
        }

        public void setMetoda(MetodaID id)
        {
            switch (id)
	            {
		    case MetodaID.Cezar:
                    metoda = new Cezar();
             break;
            case MetodaID.Midnight:
                    metoda = new Midnight();
             break;
            case MetodaID.MacierzProstokatna:
                    metoda =  new MacierzProstokatna();
             break;
            case MetodaID.Enigma:
                    metoda = new Enigma();
             break;
            case MetodaID.Rotor:
                    metoda = new Rotor();
             break;
            case MetodaID.TrueRotor:
                metoda = new TrueRotor();
             break;
             case MetodaID.PlayFair:
                    metoda = new PlayFair();
            break;
            case MetodaID.SprawdzeniePESEL:
                metoda = new SprawdzeniePESEL();
            break;
            case MetodaID.SprawdzenieIBAN:
                metoda = new SprawdzenieIBAN();
            break;
            case MetodaID.SzyfrVigenere:
                    metoda = new SzyfrVigenere();
            break;
            case MetodaID.SzyfrHomofoniczny:
                metoda = new SzyfrHomofoniczny();
            break;
            case MetodaID.KodHamminga:
                    metoda = new KodHamminga();
            break;
            case MetodaID.RSA:
                    metoda = new RSA();
             break;
                default:
             throw new Exception("Próba ustawienia nieznanej metody.");
             break;
	            }
        }

        public void setKlucz(params string[] kk)
        {
            klucz.Clear();
            foreach (string item in kk)
	            {                
                    klucz.Add(item);
            	}
        }
    }

    interface MetodaSzyfrowania
    {
        
        string szyfruj(string ss);
        string deszyfruj (string ss);
    }

    class Cezar: MetodaSzyfrowania
    {

        string MetodaSzyfrowania.szyfruj(string ss)
        {
            ss = Regex.Replace(ss, " ", "Q"); 
            ss = ss.ToUpper();            

            string wynik = "";            
            foreach (char ch in ss)
            {
                int nn = ch - 'A';
                nn = (nn + 3) % 26;
                wynik += (char) (nn+'A');
            }
            return wynik;
        }

        string MetodaSzyfrowania.deszyfruj(string ss)
        {
            string wynik = "";           
            foreach (char ch in ss)
            {
                int nn = ch - 'A';
                nn = (nn - 3 + 26) % 26;
                wynik += (char)(nn + 'A');
            }
            wynik = Regex.Replace(wynik, "Q", " "); 
            return wynik;
        }
    }
    class Midnight : MetodaSzyfrowania
    {

        string MetodaSzyfrowania.szyfruj(string ss)
        {
            ss = Regex.Replace(ss, " ", "Q");
            ss = ss.ToUpper();
            String sKlucz = MultiSzyfrator.klucz[0];
            int iKlucz = int.Parse(sKlucz);

            string wynik = "";
            foreach (char ch in ss)
            {
                int nn = ch - 'A';
                nn = (nn + iKlucz) % 26;
                wynik += (char)(nn + 'A');
            }
            return wynik;
        }

        string MetodaSzyfrowania.deszyfruj(string ss)
        {
            String sKlucz = MultiSzyfrator.klucz[0];
            int iKlucz = int.Parse(sKlucz);
            string wynik = "";
            foreach (char ch in ss)
            {
                int nn = ch - 'A';
                nn = (nn - iKlucz + 26) % 26;
                wynik += (char)(nn + 'A');
            }
            wynik = Regex.Replace(wynik, "Q", " ");
            return wynik;
        }
    }
    class MacierzProstokatna : MetodaSzyfrowania
    {

        string MetodaSzyfrowania.szyfruj(string ss)
        {
            throw new NotImplementedException();
        }

        string MetodaSzyfrowania.deszyfruj(string ss)
        {
            throw new NotImplementedException();
        }
    }
    class Enigma : MetodaSzyfrowania
    {

        string MetodaSzyfrowania.szyfruj(string ss)
        {
            throw new NotImplementedException();
        }

        string MetodaSzyfrowania.deszyfruj(string ss)
        {
            throw new NotImplementedException();
        }
    }

    class Rotor : MetodaSzyfrowania
    {
        
        string MetodaSzyfrowania.szyfruj(string ss)
        {

            ss = Regex.Replace(ss, " ", "Q");
            ss = ss.ToUpper();
            String sKlucz = MultiSzyfrator.klucz[0];
            int iKlucz = int.Parse(sKlucz);

            string wynik = "";
            foreach (char ch in ss)
            {
                int nn = ch - 'A';
                nn = (nn + iKlucz) % 26;
                wynik += (char)(nn + 'A');
                iKlucz = (iKlucz + 1) % 26;
            }
            return wynik;
        }

        string MetodaSzyfrowania.deszyfruj(string ss)
        {
            String sKlucz = MultiSzyfrator.klucz[0];
            int iKlucz = int.Parse(sKlucz);
            string wynik = "";
            foreach (char ch in ss)
            {
                int nn = ch - 'A';
                nn = (nn - iKlucz + 26) % 26;
                wynik += (char)(nn + 'A');
                iKlucz = (iKlucz+1) % 26;
            }
            wynik = Regex.Replace(wynik, "Q", " ");
            return wynik;
        }
    }

    //--------------------------------------------------------------------------------------------------------------
    //--------------------------------------------------------------------------------------------------------------

    //Domanski Radoslaw
    class TrueRotor : MetodaSzyfrowania
    {
        
        public static void generuj_i_wypiszRotor()
        {
            Random rand = new Random();
            List<char> lRotor = new List<char>();
            for (char znak = 'A'; znak <= 'Z'; znak++)
            {
                lRotor.Insert(rand.Next(lRotor.Count()), znak);
            }

            foreach (char cc in lRotor)
            {
                Console.WriteLine(cc);
            }
        }

        string MetodaSzyfrowania.szyfruj(string ss)
        {

            ss = Regex.Replace(ss, " ", "Q");
            ss = ss.ToUpper();
            String sKlucz = MultiSzyfrator.klucz[0];
            Console.WriteLine("sKlucz: " + sKlucz);
            //QPWDMTJNIBXHYOLZGFSECVURKA
            char[] rotorek = new char[26];
            int index = 0;
            
            foreach (char cc in sKlucz)
            {
                //Console.WriteLine(cc);
                rotorek[index] = cc;           
                Console.WriteLine("Rotorek: " + rotorek[index]);
                index++;
            }

            int iKlucz = 3;
            string wynik = "";

            foreach (char ch in ss)
            {
                int nn = ch - 'A';
                int ind = (nn + iKlucz) % 26;
                int nn2 = rotorek[ind] - 'A';
                nn = (nn2) % 26;
                wynik += (char)(nn + 'A');
                iKlucz = (iKlucz + 1) % 26;
            }

            //string wynik = "";
            return wynik;
        }

        string MetodaSzyfrowania.deszyfruj(string ss)
        {
            String sKlucz = MultiSzyfrator.klucz[0];
            int iKlucz = 3;
            string wynik = "";
            char[] rotorek = new char[26];
            int index = 0;
            //QPWDMTJNIBXHYOLZGFSECVURKA

            foreach (char cc in sKlucz)
            {
                //Console.WriteLine(cc);
                rotorek[index] = cc;
                Console.WriteLine("Rotorek: " + rotorek[index]);
                index++;
            }
            int index_litery = 0;
            
            foreach (char ch in ss)
            {
                int index_temp = 0;
                foreach (char ch2 in rotorek)
                {

                    if (ch == ch2)
                    {
                        index_litery = index_temp;
                        Console.WriteLine("ch: " + ch + ", ch2: " + ch2);
                        Console.WriteLine("index_litery: " + index_litery);
                    }
                    index_temp++;
                }
                index_litery = (index_litery - iKlucz + 26) % 26;
                wynik += (char)(index_litery + 'A');
                iKlucz = (iKlucz + 1) % 26;
            }
            wynik = Regex.Replace(wynik, "Q", " ");
            return wynik;
        }
    }


    //--------------------------------------------------------------------------------------------------------------
    //--------------------------------------------------------------------------------------------------------------

    class PlayFair : MetodaSzyfrowania
    {

        string playfair_szyfruj(char m1, char m2, char [,] klucz)
        {
            string wynik = "";
            int x1 = 0, y1 = 0, x2 = 0, y2 = 0;
            
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (m1 == klucz[i,j])
                    {
                        x1 = i;
                        y1 = j;
                    }
                    else if (m2 == klucz[i,j])
                    {
                        x2 = i;
                        y2 = j;
                    }
                }
            }
            if (x1 == x2)
            {
                y1 = (y1 + 1) % 5;
                y2 = (y2 + 1) % 5;
                Console.WriteLine(klucz[x1, y1] + " " + klucz[x2, y2]);
                wynik += klucz[x1, y1];
                wynik += klucz[x2, y2];
            }
            else if (y1 == y2)
            {
                x1 = (x1 + 1) % 5;
                x2 = (x2 + 1) % 5;
                Console.WriteLine(klucz[x1, y1] + " " + klucz[x2, y2]);
                wynik += klucz[x1, y1];
                wynik += klucz[x2, y2];
            }
            else
            {
                Console.WriteLine(klucz[x1, y2] + " " + klucz[x2, y1]);
                wynik += klucz[x1, y2]; 
                wynik += klucz[x2, y1];
            }
            return wynik;
        }
        string MetodaSzyfrowania.szyfruj(string ss)
        {

            ss = Regex.Replace(ss, " ", "Q");
            ss = ss.ToUpper();
            String sKlucz = MultiSzyfrator.klucz[0];
            int dlugosc = ss.Length+1;
            char[] slowo = new char[dlugosc];

            if (sKlucz.Length != 25)
            {
                Console.WriteLine("ERROR! KLUCZ MUSI ZAWIERAĆ 25 LITER!!!!!!!!");
            }

            Console.WriteLine("sKlucz: " + sKlucz);
            //ZMAPWSFUHBTCIROGVNYDXQEKL

            char[,] klucz = new char[5,5];
            char[] klucz1 = new char[25];

            //PRZEPISANIE DO TABLICY DWUWYMIAROWEJ
            int index = 0;
            foreach (char cc in sKlucz)
            {
                klucz1[index] = cc;
                index++;
            }

            index = 0;

            for(int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    klucz[i,j] = klucz1[index];
                    index++;
                }
            }
 
            //WYPISZ
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Console.Write(klucz[i,j]);
                }
                Console.WriteLine();
            }

            string wynik = "";
            int index2 = 0;
            foreach (char ch in ss)
            {
                slowo[index2] = ch;
                index2++;
            }
            Console.WriteLine("SLOWO: ");
            for(int i=0; i<slowo.Length; i++)
            {
                Console.Write(slowo[i]);
            }
            Console.WriteLine();

            for (int i = 0; i < ss.Length; i++)
            {
                if (slowo[i] == 'J')
                {
                    slowo[i] = 'I';
                }
                if (slowo[i + 1] == '\0')
                {
                    wynik += playfair_szyfruj(slowo[i], 'X', klucz);
                }
                else
                {
                    if (slowo[i + 1] == 'J')
                    {
                        slowo[i + 1] = 'I';
                    }
                    if (slowo[i] == slowo[i + 1])
                    {
                        wynik += playfair_szyfruj(slowo[i], 'X', klucz);
                    }
                    else
                    {
                        wynik += playfair_szyfruj(slowo[i], slowo[i + 1], klucz);
                        i++;
                    }
                }
            }

            return wynik;
        }



        string playfair_deszyfruj(char m1, char m2, char[,] klucz)
        {
            string wynik = "";
            int x1 = 0, y1 = 0, x2 = 0, y2 = 0;

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (m1 == klucz[i, j])
                    {
                        x1 = i;
                        y1 = j;
                    }
                    else if (m2 == klucz[i, j])
                    {
                        x2 = i;
                        y2 = j;
                    }
                }
            }
            if (x1 == x2)
            {
                y1 = ((y1 - 1) + 5) % 5;
                y2 = ((y2 - 1) + 5) % 5;
                Console.WriteLine(klucz[x1, y1] + " " + klucz[x2, y2]);
                wynik += klucz[x1, y1];
                wynik += klucz[x2, y2];
            }
            else if (y1 == y2)
            {
                x1 = ((x1 - 1) + 5) % 5;
                x2 = ((x2 - 1) + 5) % 5;
                Console.WriteLine(klucz[x1, y1] + " " + klucz[x2, y2]);
                wynik += klucz[x1, y1];
                wynik += klucz[x2, y2];
            }
            else
            {
                Console.WriteLine(klucz[x1, y2] + " " + klucz[x2, y1]);
                wynik += klucz[x1, y2];
                wynik += klucz[x2, y1];
            }
            return wynik;
        }

        string MetodaSzyfrowania.deszyfruj(string ss)
        {
           
            String sKlucz = MultiSzyfrator.klucz[0];
            int dlugosc = ss.Length + 1;
            char[] slowo = new char[dlugosc];

            if (sKlucz.Length != 25)
            {
                Console.WriteLine("ERROR! KLUCZ MUSI ZAWIERAĆ 25 LITER");
            }
            Console.WriteLine("sKlucz: " + sKlucz);
            //ZMAPWSFUHBTCIROGVNYDXQEKL

            char[,] klucz = new char[5, 5];
            char[] klucz1 = new char[25];

            //PRZEPISANIE DO TABLICY DWUWYMIAROWEJ
            int index = 0;
            foreach (char cc in sKlucz)
            {
                klucz1[index] = cc;
                index++;
            }

            index = 0;

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    klucz[i, j] = klucz1[index];
                    index++;
                }
            }

            //WYPISZ
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Console.Write(klucz[i, j]);
                }
                Console.WriteLine();
            }

            string wynik = "";
            int index2 = 0;
            foreach (char ch in ss)
            {
                slowo[index2] = ch;
                index2++;
            }
            Console.WriteLine("SLOWO: ");
            for (int i = 0; i < slowo.Length; i++)
            {
                Console.Write(slowo[i]);
            }
            Console.WriteLine();

            for (int i = 0; i < ss.Length; i++)
            {
                wynik += playfair_deszyfruj(slowo[i], slowo[i + 1], klucz);
                i++;
            }
            wynik = Regex.Replace(wynik, "Q", " ");
            wynik = Regex.Replace(wynik, "X", ""); //usuwamy X - opcjonalnie, mozna to wykomentować 
            return wynik;
        }
    }


    //--------------------------------------------------------------------------------------------------------------
    //--------------------------------------------------------------------------------------------------------------



    class SprawdzeniePESEL : MetodaSzyfrowania
    {

        string MetodaSzyfrowania.szyfruj(string ss)
        {
            string wynik = "";
            int suma = 0, reszta = 0, roznica = 0;
            if (ss.Length != 10)
            {
                wynik = "ERROR! PODAJ 10 CYFR!!!";
            }
            else
            {
                suma = int.Parse(ss[0].ToString()) * 1 + int.Parse(ss[1].ToString()) * 3 + int.Parse(ss[2].ToString()) * 7 + int.Parse(ss[3].ToString()) * 9 +
                    int.Parse(ss[4].ToString()) * 1 + int.Parse(ss[5].ToString()) * 3 + int.Parse(ss[6].ToString()) * 7 + int.Parse(ss[7].ToString()) * 9 +
                    int.Parse(ss[8].ToString()) * 1 + int.Parse(ss[9].ToString()) * 3;

                Console.WriteLine(suma);
                reszta = suma % 10;
                Console.WriteLine(reszta);
                roznica = 10 - reszta;
                wynik = ss + roznica.ToString();
            }
            return wynik;
        }

        string MetodaSzyfrowania.deszyfruj(string ss)
        {
            string wynik = "";
            int suma = 0, reszta = 0;
            if (ss.Length != 11)
            {
                wynik = "ERROR!!!";
            }
            else
            {
                suma = int.Parse(ss[0].ToString()) * 1 + int.Parse(ss[1].ToString()) * 3 + int.Parse(ss[2].ToString()) * 7 + int.Parse(ss[3].ToString()) * 9 +
                int.Parse(ss[4].ToString()) * 1 + int.Parse(ss[5].ToString()) * 3 + int.Parse(ss[6].ToString()) * 7 + int.Parse(ss[7].ToString()) * 9 +
                int.Parse(ss[8].ToString()) * 1 + int.Parse(ss[9].ToString()) * 3 + int.Parse(ss[10].ToString()) * 1;

                Console.WriteLine(suma);
                reszta = suma % 10;
                if (reszta == 0)
                    wynik = "PESEL jest poprawny.";
                else
                    wynik = "PESEL jest niepoprawny!";
            }
            return wynik;
        }
    }




    //--------------------------------------------------------------------------------------------------------------
    //--------------------------------------------------------------------------------------------------------------



    class SprawdzenieIBAN : MetodaSzyfrowania
    {

        string MetodaSzyfrowania.szyfruj(string ss)
        {
            ss = Regex.Replace(ss, " ", "");
            string wynik = "";
            string kraj_numerycznie = "";
            string sam_numer = "";
            string numer_odwrocony = "";
            int reszta = 0;
            if (ss.Length != 28)
            {
                wynik = "ERROR! NUMER NIE ZAWIERA 28 ZNAKÓW!!!";
            }
            else
            {
                for (int i = 4; i < ss.Length; i++)
                {
                    sam_numer += ss[i].ToString();
                }

                if(ss[0].ToString() == "P" && ss[1].ToString() == "L")
                {
                    kraj_numerycznie = "2521";
                }

                numer_odwrocony += sam_numer + kraj_numerycznie + ss[2].ToString() + ss[3].ToString();
                reszta = (int.Parse(numer_odwrocony[0].ToString() + numer_odwrocony[1].ToString() + numer_odwrocony[2].ToString())) % 97;
                Console.WriteLine(reszta);

                for (int i = 3; i < numer_odwrocony.Length; i++)
                {
                    reszta = (int.Parse(reszta.ToString() + numer_odwrocony[i].ToString())) % 97;
                    Console.WriteLine(reszta);
                }
                String cyfry_kontrolne = "";
                if (reszta != 1)
                {
                    reszta = 98 - reszta;
                    if (reszta < 10)
                    {
                        cyfry_kontrolne = "0" + reszta.ToString();
                    }
                    else
                    {
                        cyfry_kontrolne = reszta.ToString();
                    }
                }
                else
                {
                    cyfry_kontrolne = ss[2].ToString() + ss[3].ToString();
                }
                
                String numer_ze_spacjami = "";
                for (int i = 0; i < sam_numer.Length; i++)
                {
                    if (i==3 || i== 7 || i == 11 || i == 15 || i == 19 || i == 23 )
                        numer_ze_spacjami += sam_numer[i] + " ";
                    else
                        numer_ze_spacjami += sam_numer[i];
                }
                wynik = ss[0].ToString() + ss[1].ToString() + " " + cyfry_kontrolne + " " + numer_ze_spacjami;
            }
            return wynik;
        }

        string MetodaSzyfrowania.deszyfruj(string ss)
        {
            ss = Regex.Replace(ss, " ", "");
            string wynik = "";
            string kraj_numerycznie = "";
            string sam_numer = "";
            string numer_odwrocony = "";
            int reszta = 0;
            if (ss.Length != 28)
            {
                wynik = "ERROR! NUMER NIE ZAWIERA 28 ZNAKÓW!!!";
            }
            else
            {
                for (int i = 4; i < ss.Length; i++)
                {
                    sam_numer += ss[i].ToString();
                }

                if (ss[0].ToString() == "P" && ss[1].ToString() == "L")
                {
                    kraj_numerycznie = "2521";
                }

                numer_odwrocony += sam_numer + kraj_numerycznie + ss[2].ToString() + ss[3].ToString();
                reszta = (int.Parse(numer_odwrocony[0].ToString() + numer_odwrocony[1].ToString() + numer_odwrocony[2].ToString())) % 97;
                Console.WriteLine(reszta);
                
                for (int i = 3; i < numer_odwrocony.Length; i++)
                {
                    reszta = (int.Parse(reszta.ToString() + numer_odwrocony[i].ToString())) % 97;
                    Console.WriteLine(reszta);
                }

                if (reszta == 1)
                {
                    wynik = "IBAN jest poprawny";
                }
                else
                {
                    wynik = "IBAN nie jest poprawny";
                }
            }
            return wynik;
        }
    }





    //--------------------------------------------------------------------------------------------------------------
    //--------------------------------------------------------------------------------------------------------------



    class SzyfrVigenere : MetodaSzyfrowania
    {

        string MetodaSzyfrowania.szyfruj(string ss)
        {

            ss = Regex.Replace(ss, " ", "");
            ss = ss.ToUpper();
            String sKlucz = MultiSzyfrator.klucz[0];
            Console.WriteLine("sKlucz: " + sKlucz);
            string wynik = "";

            if (ss.Length == 0 || sKlucz.Length == 0)
            {
                wynik = "ERROR! NIE PODAŁEŚ SŁOWA LUB KLUCZA!!!";
            }
            else
            {
                char[] sKlucz_tab = new char[sKlucz.Length + 1];
                sKlucz_tab[0] = 'X'; //byle jaki znak, bo i tak zaczynamy od indeksu 1
                int index = 1;

                foreach (char cc in sKlucz)
                {
                    sKlucz_tab[index] = cc;
                    Console.WriteLine("sKlucz_tab: " + sKlucz_tab[index]);
                    index++;
                }

                int indexx = 1;

                foreach (char ch in ss)
                {
                    int nn = ch - 'A' + 1;
                    int nn2 = sKlucz_tab[indexx] - 'A' + 1;
                    int ind = (nn + nn2) % 27;
                    indexx++;
                    if (indexx > sKlucz.Length)
                    {
                        indexx = 1;
                    }
                    wynik += (char)(ind + 'A' - 1);
                }
            }
            return wynik;
        }




        string MetodaSzyfrowania.deszyfruj(string ss)
        {
            ss = Regex.Replace(ss, " ", "");
            ss = ss.ToUpper();
            String sKlucz = MultiSzyfrator.klucz[0];
            Console.WriteLine("sKlucz: " + sKlucz);
            string wynik = "";

            if (ss.Length == 0 || sKlucz.Length == 0)
            {
                wynik = "ERROR! NIE PODAŁEŚ SŁOWA LUB KLUCZA!!!";
            }
            else
            {
                char[] sKlucz_tab = new char[sKlucz.Length + 1];
                sKlucz_tab[0] = 'X'; //byle jaki znak, bo i tak zaczynamy od indeksu 1
                int index = 1;

                foreach (char cc in sKlucz)
                {
                    sKlucz_tab[index] = cc;
                    Console.WriteLine("sKlucz_tab: " + sKlucz_tab[index]);
                    index++;
                }

                int indexx = 1;

                foreach (char ch in ss)
                {
                    int nn = ch - 'A' + 1;
                    int nn2 = sKlucz_tab[indexx] - 'A' + 1;
                    int ind = (nn - nn2 + 27) % 27;
                    indexx++;
                    if (indexx > sKlucz.Length)
                    {
                        indexx = 1;
                    }
                    wynik += (char)(ind + 'A' - 1);
                }
            }
            return wynik;
        }
    }




    //--------------------------------------------------------------------------------------------------------------
    //--------------------------------------------------------------------------------------------------------------



    class SzyfrHomofoniczny : MetodaSzyfrowania
    {

        public static void generuj_i_wypisz_klucz_Homofony()
        {
            Random rand = new Random();
            List<char> klucz_Homofony = new List<char>();
            char poczatek = (char)33;
            char koniec = (char)110;
            //char[] klucz = new char[78];
            for (char znak = poczatek; znak <= koniec; znak++)
            {
                klucz_Homofony.Insert(rand.Next(klucz_Homofony.Count()), znak);
            }

            //for(int i = 0; i < 78; i++)
            //{
            //    klucz[i] = klucz_Homofony[i];
           // }

            Console.WriteLine("\nKLUCZ DO HOMOFONOW \n");
            int x = 0;
            foreach (char cc in klucz_Homofony)
            {
                Console.WriteLine(x + " " + cc);
                x++;
            }
            // &eV)h+#;5BMA."J1[TC3FfPWL$:^?\k%Sa`Q<2bDNXU9Inj0ZG'6>*d@OcEHgKY(8/]_m,-i7=l4R!
        }

        string MetodaSzyfrowania.szyfruj(string ss)
        {
            //generuj_i_wypisz_klucz_Homofony();
            ss = Regex.Replace(ss, " ", "");
            ss = ss.ToUpper();
            String sKlucz = MultiSzyfrator.klucz[0];
            //Console.WriteLine("sKlucz: " + sKlucz);
            string wynik = "";
            /*
             LITERA - PRZYPISANE JEJ ZNAKI W KLUCZU SZYFROWANIA (indeks)
             A - 0,1,2
             B - 3,4,5
             C - 6,7,8
            D - 9,10,11
            E - 12,13,14
            F - 15,16,17
            G - 18,19,20
            H - 21,22,23
            I - 24,25,26
            J - 27,28,29
            K - 30,31,32
            L - 33,34,35
            M - 36,37,38
            N - 39,40,41
            O - 42,43,44
            P - 45,46,47
            Q - 48,49,50
            R - 51,52,53
            S - 54,55,56
            T - 57,58,59
            U - 60,61,62
            V - 63,64,65
            W - 66,67,68
            X - 69,70,71
            Y - 72,73,74
            Z - 75,76,77
             */

            if (ss.Length == 0 || sKlucz.Length == 0)
            {
                wynik = "ERROR! NIE PODAŁEŚ SŁOWA LUB KLUCZA!!!";
            }
            else
            {
                char[] sKlucz_tab = new char[sKlucz.Length];
                int index = 0;

                foreach (char cc in sKlucz)
                {
                    sKlucz_tab[index] = cc;
                 //   Console.WriteLine("sKlucz_tab: " + sKlucz_tab[index]);
                    index++;
                }

                Random rand = new Random();

                foreach (char ch in ss)
                {
                    int nn = ch - 65;
                    int nn2 = nn * 3 + rand.Next(3);
                    wynik += sKlucz_tab[nn2];
                }
            }
            return wynik;
        }




        string MetodaSzyfrowania.deszyfruj(string ss)
        {
            ss = Regex.Replace(ss, " ", "");
            String sKlucz = MultiSzyfrator.klucz[0];
            //Console.WriteLine("sKlucz: " + sKlucz);
            string wynik = "";
            Random rand = new Random();
            List<char> klucz_Homofony = new List<char>();

            if (ss.Length == 0 || sKlucz.Length == 0)
            {
                wynik = "ERROR! NIE PODAŁEŚ SŁOWA LUB KLUCZA!!!";
            }
            else
            {
                //char[] sKlucz_tab = new char[sKlucz.Length];
                int index = 0;

                foreach (char cc in sKlucz)
                {
                    //sKlucz_tab[index] = cc;
                    klucz_Homofony.Add(cc);
                   // Console.WriteLine("sKlucz_tab: " + sKlucz_tab[index]);
                    index++;
                }

                int indexx = 0;

                foreach (char ch in ss)
                {
                    indexx = klucz_Homofony.IndexOf(ch);
                    int nn = indexx / 3;
                    int nn2 = nn + 65; ;
                    wynik += (char)nn2;
                }
            }
            return wynik;
        }
    }






    //--------------------------------------------------------------------------------------------------------------
    //--------------------------------------------------------------------------------------------------------------



    class KodHamminga : MetodaSzyfrowania
    {

        string MetodaSzyfrowania.szyfruj(string ss)
        {
            
            ss = Regex.Replace(ss, " ", "");
            ss = ss.ToUpper();
            string wynik = "";
            
            if (ss.Length == 0 )
            {
                wynik = "ERROR! NIE PODAŁEŚ LICZBY!!!";
            }
            else if (ss.Length == 4)
            {
                int[] wpisana_liczba = new int[ss.Length];
                int index = 0;

                foreach (char cc in ss)
                {
                    wpisana_liczba[index] = Int32.Parse(cc.ToString());
                   // Console.WriteLine("Wpisana liczba: " + wpisana_liczba[index]);
                    index++;
                }
                int parzystosc1, parzystosc2, parzystosc3;
                parzystosc1 = (wpisana_liczba[0] + wpisana_liczba[1] + wpisana_liczba[3]) % 2;
                parzystosc2 = (wpisana_liczba[0] + wpisana_liczba[2] + wpisana_liczba[3]) % 2;
                parzystosc3 = (wpisana_liczba[1] + wpisana_liczba[2] + wpisana_liczba[3]) % 2;
                Console.WriteLine("parzystosc1 = " + parzystosc1);
                Console.WriteLine("parzystosc2 = " + parzystosc2);
                Console.WriteLine("parzystosc3 = " + parzystosc3);

                //wprowadzamy zakłócenie wiadomości - zamieniamy bit
                if (wpisana_liczba[2] == 0)
                {
                    wpisana_liczba[2] = 1;
                }
                else
                {
                    wpisana_liczba[2] = 0;
                }

                wynik = parzystosc1.ToString() + parzystosc2.ToString() + wpisana_liczba[0].ToString() + parzystosc3.ToString() + wpisana_liczba[1].ToString() +
                    wpisana_liczba[2].ToString() + wpisana_liczba[3].ToString();

            }

            else if (ss.Length == 8)
            {
                int[] wpisana_liczba = new int[ss.Length];
                int index = 0;

                foreach (char cc in ss)
                {
                    wpisana_liczba[index] = Int32.Parse(cc.ToString());
                    // Console.WriteLine("Wpisana liczba: " + wpisana_liczba[index]);
                    index++;
                }
                int parzystosc1, parzystosc2, parzystosc3, parzystosc4;
                parzystosc1 = (wpisana_liczba[0] + wpisana_liczba[1] + wpisana_liczba[3] + wpisana_liczba[4] + wpisana_liczba[6]) % 2;
                parzystosc2 = (wpisana_liczba[0] + wpisana_liczba[2] + wpisana_liczba[3] + wpisana_liczba[5] + wpisana_liczba[6]) % 2;
                parzystosc3 = (wpisana_liczba[1] + wpisana_liczba[2] + wpisana_liczba[3] + wpisana_liczba[7]) % 2;
                parzystosc4 = (wpisana_liczba[4] + wpisana_liczba[5] + wpisana_liczba[6] + wpisana_liczba[7]) % 2;
                Console.WriteLine("parzystosc1 = " + parzystosc1);
                Console.WriteLine("parzystosc2 = " + parzystosc2);
                Console.WriteLine("parzystosc3 = " + parzystosc3);
                Console.WriteLine("parzystosc4 = " + parzystosc4);

                //wprowadzamy zakłócenie wiadomości - zamieniamy bit
                if (wpisana_liczba[2] == 0)
                {
                    wpisana_liczba[2] = 1;
                }
                else
                {
                    wpisana_liczba[2] = 0;
                }

                wynik = parzystosc1.ToString() + parzystosc2.ToString() + wpisana_liczba[0].ToString() + parzystosc3.ToString() + wpisana_liczba[1].ToString() +
                    wpisana_liczba[2].ToString() + wpisana_liczba[3].ToString() + parzystosc4.ToString() + wpisana_liczba[4].ToString() +
                    wpisana_liczba[5].ToString() + wpisana_liczba[6].ToString() + wpisana_liczba[7].ToString();

            }
            else
            {
                wynik = "ERROR! PODANY CIĄG MA ZŁĄ DŁUGOŚĆ (MUSI MIEĆ 4 lub 8)!";
            }
            return wynik;
        }




        string MetodaSzyfrowania.deszyfruj(string ss)
        {
            ss = Regex.Replace(ss, " ", "");
            string wynik = "";

            if (ss.Length == 0)
            {
                wynik = "ERROR! NIE PODAŁEŚ LICZBY!!!";
            }
            else if (ss.Length == 7)
            {
                int[] wpisana_liczba = new int[ss.Length];
                int index = 0;

                foreach (char cc in ss)
                {
                    wpisana_liczba[index] = Int32.Parse(cc.ToString());
                    //Console.WriteLine("Wpisana liczba: " + wpisana_liczba[index]);
                    index++;
                }

                int parzystosc1, parzystosc2, parzystosc3;
                parzystosc1 = (wpisana_liczba[0] + wpisana_liczba[2] + wpisana_liczba[4] + wpisana_liczba[6]) % 2;
                parzystosc2 = (wpisana_liczba[1] + wpisana_liczba[2] + wpisana_liczba[5] + wpisana_liczba[6]) % 2;
                parzystosc3 = (wpisana_liczba[3] + wpisana_liczba[4] + wpisana_liczba[5] + wpisana_liczba[6]) % 2;
                Console.WriteLine("parzystosc1 = " + parzystosc1);
                Console.WriteLine("parzystosc2 = " + parzystosc2);
                Console.WriteLine("parzystosc3 = " + parzystosc3);

                int error_check = (int)(parzystosc1 * Math.Pow(2, 0) + parzystosc2 * Math.Pow(2, 1) + parzystosc3 * Math.Pow(2, 2));
                Console.WriteLine("Error check = " + error_check);

                if(error_check == 0)
                {
                    wynik = ss;
                }
                else
                {
                    if (wpisana_liczba[error_check - 1] == 0) //error_check okresla pozycję bitu do zmiany. -1 bo zaczynamy od 0, nie od 1.
                    {
                        wpisana_liczba[error_check - 1] = 1;
                    }
                    else
                    {
                        wpisana_liczba[error_check - 1] = 0;
                    }
                    Console.WriteLine("Nastąpiła korekta");
                    wynik = wpisana_liczba[0].ToString() + wpisana_liczba[1].ToString() + wpisana_liczba[2].ToString() + wpisana_liczba[3].ToString() + wpisana_liczba[4].ToString() +
                        wpisana_liczba[5].ToString() + wpisana_liczba[6].ToString();
                }


            }


            else if (ss.Length == 12)
            {
                int[] wpisana_liczba = new int[ss.Length];
                int index = 0;

                foreach (char cc in ss)
                {
                    wpisana_liczba[index] = Int32.Parse(cc.ToString());
                    //Console.WriteLine("Wpisana liczba: " + wpisana_liczba[index]);
                    index++;
                }

                int parzystosc1, parzystosc2, parzystosc3, parzystosc4;
                parzystosc1 = (wpisana_liczba[0] + wpisana_liczba[2] + wpisana_liczba[4] + wpisana_liczba[6] + wpisana_liczba[8] + wpisana_liczba[10]) % 2;
                parzystosc2 = (wpisana_liczba[1] + wpisana_liczba[2] + wpisana_liczba[5] + wpisana_liczba[6] + wpisana_liczba[9] + wpisana_liczba[10]) % 2;
                parzystosc3 = (wpisana_liczba[3] + wpisana_liczba[4] + wpisana_liczba[5] + wpisana_liczba[6] + wpisana_liczba[11]) % 2;
                parzystosc4 = (wpisana_liczba[7] + wpisana_liczba[8] + wpisana_liczba[9] + wpisana_liczba[10] + wpisana_liczba[11]) % 2;
                Console.WriteLine("parzystosc1 = " + parzystosc1);
                Console.WriteLine("parzystosc2 = " + parzystosc2);
                Console.WriteLine("parzystosc3 = " + parzystosc3);
                Console.WriteLine("parzystosc4 = " + parzystosc4);

                int error_check = (int)(parzystosc1 * Math.Pow(2, 0) + parzystosc2 * Math.Pow(2, 1) + parzystosc3 * Math.Pow(2, 2) + parzystosc4 * Math.Pow(2, 3));
                Console.WriteLine("Error check = " + error_check);

                if (error_check == 0)
                {
                    wynik = ss;
                }
                else
                {
                    if (wpisana_liczba[error_check - 1] == 0)
                    {
                        wpisana_liczba[error_check - 1] = 1;
                    }
                    else
                    {
                        wpisana_liczba[error_check - 1] = 0;
                    }
                    Console.WriteLine("Nastąpiła korekta");
                    wynik = wpisana_liczba[0].ToString() + wpisana_liczba[1].ToString() + wpisana_liczba[2].ToString() + wpisana_liczba[3].ToString() + wpisana_liczba[4].ToString() +
                        wpisana_liczba[5].ToString() + wpisana_liczba[6].ToString() + wpisana_liczba[7].ToString() + wpisana_liczba[8].ToString() + wpisana_liczba[9].ToString() 
                        + wpisana_liczba[10].ToString() + wpisana_liczba[11].ToString();
                }


            }
            else
            {
                wynik = "ERROR! PODANY CIĄG MA ZŁĄ DŁUGOŚĆ (MUSI MIEĆ 7 lub 12)!";
            }
            return wynik;
        }
    }









    //--------------------------------------------------------------------------------------------------------------
    //--------------------------------------------------------------------------------------------------------------





    class RSA : MetodaSzyfrowania
    {
        string MetodaSzyfrowania.szyfruj(string ss)
        {

            //ss = Regex.Replace(ss, " ", "");
            //ss = ss.ToUpper();
            String sKlucz = MultiSzyfrator.klucz[0];
            sKlucz = Regex.Replace(sKlucz, " ", "");

            string wynik = "";

            if (ss.Length == 0 || sKlucz.Length == 0)
            {
                wynik = "ERROR! NIE PODAŁEŚ SŁOWA LUB KLUCZA!!!";
            }
            else
            {
                string[] sKlucz_split = sKlucz.Split(',');
                int[] klucze = new int[2];
                klucze[0] = int.Parse(sKlucz_split[0]);
                klucze[1] = int.Parse(sKlucz_split[1]);

                foreach (char ch in ss)
                {
                    int nn = ch;
                    Console.WriteLine(nn);
                    int nn2 = pot_mod(nn, klucze[0], klucze[1]);
                    wynik += nn2.ToString() + ", ";
                }
            }
            return wynik;
        }




        string MetodaSzyfrowania.deszyfruj(string ss)
        {
            ss = Regex.Replace(ss, " ", "");
            //ss = ss.ToUpper();
            String sKlucz = MultiSzyfrator.klucz[0];
            sKlucz = Regex.Replace(sKlucz, " ", "");

            string wynik = "";

            if (ss.Length == 0 || sKlucz.Length == 0)
            {
                wynik = "ERROR! NIE PODAŁEŚ SŁOWA LUB KLUCZA!!!";
            }
            else
            {
                string[] ss_split = ss.Split(',');
                int[] wiad_zaszyfr = new int[ss_split.Length];

                for (int i = 0; i < ss_split.Length - 1; i++)
                {
                    wiad_zaszyfr[i] = int.Parse(ss_split[i]);
                    Console.WriteLine("wiad " + wiad_zaszyfr[i]);
                }

                string[] sKlucz_split = sKlucz.Split(',');
                int[] klucze = new int[2];
                klucze[0] = int.Parse(sKlucz_split[0]);
                klucze[1] = int.Parse(sKlucz_split[1]);
                // Console.WriteLine("klucze0 " + klucze[0]);
                // Console.WriteLine("klucze1 " + klucze[1]);

                for (int i = 0; i < wiad_zaszyfr.Length; i++)
                {
                    int nn2 = pot_mod(wiad_zaszyfr[i], klucze[0], klucze[1]);
                    wynik += (char)nn2;
                }

            }
            return wynik;
        }
        int pot_mod(int a, int w, int n)
        {
            int pot, wyn, q;

            pot = a; wyn = 1;
            for (q = w; q > 0; q /= 2)
            {
                if (q % 2 != 0) wyn = (wyn * pot) % n;
                pot = (pot * pot) % n; 
            }
            return wyn;
        }
    }


}

