using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Okrety
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Rectangle[,] cR;
        public Rectangle[,] pR;

        public bool ustawioneStatki = false;
        public bool czyUstawioneStatkiWGrze = false;

        Boolean[,] prostokatyKomputera;

        bool[] bb;
        int[] tab = new int[8];
        Button[] tablicaButtonow;
        int aktualnyButton = -1;
        Gra game = null;
        JakiStatek[] rozmiarStatku;
        int licznikStatkow = 0;
        PionowoCzyPoziomo pcp;

        public void malnijJednoPoleGracza(int x, int y, bool warunek)
        {
            SolidColorBrush myBrush;
            if (warunek)
                myBrush = new SolidColorBrush(Colors.Green);
            else
                myBrush = new SolidColorBrush(Colors.Red);
            pR[x, y].Fill = myBrush;
        }

        public void malnijJednoPoleKomputera(int x, int y, bool warunek)
        {
            SolidColorBrush myBrush;
                if(warunek)
                    myBrush = new SolidColorBrush(Colors.Green);
                else
                    myBrush = new SolidColorBrush(Colors.Red);
            cR[x, y].Fill = myBrush;
        }

        // DO DEBUGU
        public void wyczyscProstokaty(Boolean[,] tab, Rectangle[,] r)
        {
            SolidColorBrush myBrush = new SolidColorBrush(Colors.Blue);
            SolidColorBrush defaultBrush = new SolidColorBrush(Colors.White);
            for (int i = 0; i < 9; ++i)
                for (int j = 0; j < 9; ++j)
                    if (tab[i, j])
                        r[i, j].Fill = myBrush;
                    else
                        r[i, j].Fill = defaultBrush;
        } // END OF DEBUG

        public void changeButtonsVisible()
        {
            bool czyJakisButtonJeszczeNieobsluzony = false;
            foreach(Boolean b in bb)
            {
                if (!b)
                {
                    czyJakisButtonJeszczeNieobsluzony = true;
                    break;
                }
                else
                {
                    foreach (Button bb in tablicaButtonow)
                        bb.Visibility = System.Windows.Visibility.Hidden;
                }
            }

            if (!czyJakisButtonJeszczeNieobsluzony && aktualnyButton == -1)
                ustawioneStatki = true;

            if (czyJakisButtonJeszczeNieobsluzony && licznikStatkow != 8 && aktualnyButton != -1)
            {
                foreach (Button b in tablicaButtonow)
                    b.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                for (int i = 0; i < tab.Length; ++i)
                    if (tab[i] == 0)
                        tablicaButtonow[i].Visibility = System.Windows.Visibility.Visible;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            cR = new Rectangle[9,9] {
                {c00, c01, c02, c03, c04, c05, c06, c07, c08},
                {c10, c11, c12, c13, c14, c15, c16, c17, c18},
                {c20, c21, c22, c23, c24, c25, c26, c27, c28},
                {c30, c31, c32, c33, c34, c35, c36, c37, c38},
                {c40, c41, c42, c43, c44, c45, c46, c47, c48},
                {c50, c51, c52, c53, c54, c55, c56, c57, c58},
                {c60, c61, c62, c63, c64, c65, c66, c67, c68},
                {c70, c71, c72, c73, c74, c75, c76, c77, c78},
                {c80, c81, c82, c83, c84, c85, c86, c87, c88}
            };
            pR = new Rectangle[9, 9] {
                {p00, p01, p02, p03, p04, p05, p06, p07, p08},
                {p10, p11, p12, p13, p14, p15, p16, p17, p18},
                {p20, p21, p22, p23, p24, p25, p26, p27, p28},
                {p30, p31, p32, p33, p34, p35, p36, p37, p38},
                {p40, p41, p42, p43, p44, p45, p46, p47, p48},
                {p50, p51, p52, p53, p54, p55, p56, p57, p58},
                {p60, p61, p62, p63, p64, p65, p66, p67, p68},
                {p70, p71, p72, p73, p74, p75, p76, p77, p78},
                {p80, p81, p82, p83, p84, p85, p86, p87, p88}
            };

            tablicaButtonow = new Button[8]
            {
                button, button1, button2, button3, button4, button5, button6, button7
            };

            bb = new bool[8];

            rozmiarStatku = new JakiStatek[8];
            rozmiarStatku[0] = rozmiarStatku[1] = rozmiarStatku[2] = JakiStatek.POJEDYNCZY;
            rozmiarStatku[3] = rozmiarStatku[4] = JakiStatek.PODWOJNY;
            rozmiarStatku[5] = rozmiarStatku[6] = JakiStatek.POTROJNY;
            rozmiarStatku[7] = JakiStatek.POCZWORNY;

            pcp = new PionowoCzyPoziomo();

            prostokatyKomputera = new Boolean[9, 9];

            game = new Gra(this);
        }

        private void c00_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(0, 0);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c01_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(0, 1);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c02_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(0, 2);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c03_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(0, 3);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c04_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(0, 4);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c05_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(0, 5);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c06_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(0, 6);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c07_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(0, 7);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c08_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(0, 8);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c10_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(1, 0);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c11_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(1, 1);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c12_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(1, 2);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c13_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(1, 3);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c14_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(1, 4);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c15_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(1, 5);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c16_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(1, 6);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c17_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(1, 7);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c18_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(1, 8);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c20_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(2, 0);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c21_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(2, 1);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c22_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(2, 2);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c23_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(2, 3);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c24_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(2, 4);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c25_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(2, 5);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c26_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(2, 6);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c27_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(2, 7);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c28_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(2, 8);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c30_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(3, 0);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c31_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(3, 1);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c32_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(3, 2);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c33_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(3, 3);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c34_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(3, 4);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c35_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(3, 5);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c36_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(3, 6);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c37_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(3, 7);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c38_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(3, 8);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c40_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(4, 0);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c41_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(4, 1);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c42_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(4, 2);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c43_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(4, 3);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c44_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(4, 4);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c45_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(4, 5);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c46_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(4, 6);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c47_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(4, 7);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c48_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(4, 8);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c50_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(5, 0);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c51_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(5, 1);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c52_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(5, 2);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c53_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(5, 3);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c54_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(5, 4);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c55_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(5, 5);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c56_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(5, 6);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c57_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(5, 7);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c58_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(5, 8);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c60_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(6, 0);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c61_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(6, 1);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c62_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(6, 2);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c63_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(6, 3);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c64_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(6, 4);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c65_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(6, 5);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c66_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(6, 6);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c67_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(6, 7);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c68_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(6, 8);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c70_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(7, 0);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c71_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(7, 1);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c72_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(7, 2);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c73_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(7, 3);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c74_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(7, 4);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c75_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(7, 5);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c76_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(7, 6);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c77_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(7, 7);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c78_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(7, 8);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c80_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(8, 0);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c81_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(8, 1);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c82_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(8, 2);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c83_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(8, 3);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c84_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(8, 4);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c85_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(8, 5);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c86_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(8, 6);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c87_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(8, 7);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void c88_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(8, 8);
            if (prostokatyKomputera[poz.x, poz.y])
                return;
            if (ustawioneStatki)
                prostokatyKomputera[poz.x, poz.y] = true;
            game.Rozgrywka(poz);
        }

        private void StworzStatekPoprzezKlikanieWPlansze(Pozycja poz)
        {
            if (aktualnyButton != -1)
            {
                if (!game.statkiGracza[aktualnyButton].SprawdzCzyDookolaNieMaJuzStatku(poz, game.statkiGracza[aktualnyButton].pozycja))
                {
                    mainInfoLabel.Content = "";
                    if (game.statkiGracza[aktualnyButton].pozycja.Count == 0)
                    {
                        int wolnePolaLewo = 0, wolnePolaPrawo = 0, wolnePolaGora = 0, wolnePolaDol = 0;
                        for(int i = 1; i <= (int)rozmiarStatku[aktualnyButton]; ++i)
                        {
                            
                            if (poz.y - i >= 0 && !game.planszaGracza[poz.x, poz.y - i])
                            {
                                if (!game.statkiGracza[aktualnyButton].SprawdzCzyDookolaNieMaJuzStatku(new Pozycja(poz.x, poz.y - i), game.statkiGracza[aktualnyButton].pozycja))
                                    ++wolnePolaLewo;
                            }
                            if (poz.y + i <= 8 && !game.planszaGracza[poz.x, poz.y + i])
                            {
                                if (!game.statkiGracza[aktualnyButton].SprawdzCzyDookolaNieMaJuzStatku(new Pozycja(poz.x, poz.y + i), game.statkiGracza[aktualnyButton].pozycja))
                                    ++wolnePolaPrawo;
                            }
                            if (poz.x - i >= 0 && !game.planszaGracza[poz.x - i, poz.y])
                            {
                                if (!game.statkiGracza[aktualnyButton].SprawdzCzyDookolaNieMaJuzStatku(new Pozycja(poz.x - i, poz.y), game.statkiGracza[aktualnyButton].pozycja))
                                    ++wolnePolaGora;
                            }
                            if (poz.x + i <= 8 && !game.planszaGracza[poz.x + i, poz.y])
                            {
                                if (!game.statkiGracza[aktualnyButton].SprawdzCzyDookolaNieMaJuzStatku(new Pozycja(poz.x + i, poz.y), game.statkiGracza[aktualnyButton].pozycja))
                                    ++wolnePolaDol;
                            }
                        }
                        if ((wolnePolaLewo + wolnePolaPrawo < (int)rozmiarStatku[aktualnyButton] - 1) && (wolnePolaDol + wolnePolaGora < (int)rozmiarStatku[aktualnyButton] - 1))
                        {
                            game.statkiGracza[aktualnyButton].pozycja.Clear();
                            mainInfoLabel.Content = "Nie jesteś w stanie utworzyć\ntu całego statku\no podanym rozmiarze.\nSpróbuj gdzieś indziej.";
                            return;
                        }

                        game.statkiGracza[aktualnyButton].pozycja.Add(poz);
                        game.planszaGracza[poz.x, poz.y] = true;
                        wyczyscProstokaty(game.planszaGracza, pR);
                    }
                    else if(game.statkiGracza[aktualnyButton].pozycja.Count == 1)
                    {
                        if(Math.Abs(poz.x - game.statkiGracza[aktualnyButton].pozycja[0].x) == 1 && Math.Abs(poz.y - game.statkiGracza[aktualnyButton].pozycja[0].y) == 0)
                        {
                            int wolnePolaGora = 0, wolnePolaDol = 0;
                            for (int i = 1; i <= (int)rozmiarStatku[aktualnyButton]; ++i)
                            {
                                if (poz.x - i >= 0 && !game.planszaGracza[poz.x - i, poz.y])
                                    if (!game.statkiGracza[aktualnyButton].SprawdzCzyDookolaNieMaJuzStatku(new Pozycja(poz.x - i, poz.y), game.statkiGracza[aktualnyButton].pozycja))
                                            ++wolnePolaGora;
                                if (poz.x + i <= 8 && !game.planszaGracza[poz.x + i, poz.y])
                                    if (!game.statkiGracza[aktualnyButton].SprawdzCzyDookolaNieMaJuzStatku(new Pozycja(poz.x + i, poz.y), game.statkiGracza[aktualnyButton].pozycja))
                                            ++wolnePolaDol;
                                if (poz.x - i >= 0 && game.planszaGracza[poz.x - i, poz.y] && game.statkiGracza[aktualnyButton].pozycja[0].x == poz.x - i)
                                        ++wolnePolaGora;
                                if (poz.x + i <= 8 && game.planszaGracza[poz.x + i, poz.y] && game.statkiGracza[aktualnyButton].pozycja[0].x == poz.x + i)
                                        ++wolnePolaDol;
                            }

                            if (wolnePolaGora + wolnePolaDol < (int)rozmiarStatku[aktualnyButton] - 1)
                            {
                                mainInfoLabel.Content = "Nie jesteś w stanie utworzyć\ntu całego statku\no podanym rozmiarze.\nSpróbuj gdzieś indziej.";
                                return;
                            }

                            pcp = PionowoCzyPoziomo.PIONOWO;
                            game.statkiGracza[aktualnyButton].pozycja.Add(poz);
                            game.planszaGracza[poz.x, poz.y] = true;
                            wyczyscProstokaty(game.planszaGracza, pR);
                        }
                        else if(Math.Abs(poz.x - game.statkiGracza[aktualnyButton].pozycja[0].x) == 0 && Math.Abs(poz.y - game.statkiGracza[aktualnyButton].pozycja[0].y) == 1)
                        {

                            int wolnePolaLewo = 0, wolnePolaPrawo = 0;
                            for (int i = 1; i <= (int)rozmiarStatku[aktualnyButton]; ++i)
                            {
                                if (poz.y - i >= 0 && !game.planszaGracza[poz.x, poz.y - i])
                                    if (!game.statkiGracza[aktualnyButton].SprawdzCzyDookolaNieMaJuzStatku(new Pozycja(poz.x, poz.y - i), game.statkiGracza[aktualnyButton].pozycja))
                                        ++wolnePolaLewo;
                                if (poz.y + i <= 8 && !game.planszaGracza[poz.x, poz.y + i])
                                    if (!game.statkiGracza[aktualnyButton].SprawdzCzyDookolaNieMaJuzStatku(new Pozycja(poz.x, poz.y + i), game.statkiGracza[aktualnyButton].pozycja))
                                        ++wolnePolaPrawo;
                                if (poz.y - i >= 0 && game.planszaGracza[poz.x, poz.y - i] && game.statkiGracza[aktualnyButton].pozycja[0].y == poz.y - i)
                                        ++wolnePolaLewo;
                                if (poz.y + i <= 8 && game.planszaGracza[poz.x, poz.y + i] && game.statkiGracza[aktualnyButton].pozycja[0].y == poz.y + i)
                                        ++wolnePolaPrawo;
                            }

                            if (wolnePolaLewo + wolnePolaPrawo < (int)rozmiarStatku[aktualnyButton] - 1)
                            {
                                mainInfoLabel.Content = "Nie jesteś w stanie utworzyć\ntu całego statku\no podanym rozmiarze.\nSpróbuj gdzieś indziej.";
                                return;
                            }

                            pcp = PionowoCzyPoziomo.POZIOMO;
                            game.statkiGracza[aktualnyButton].pozycja.Add(poz);
                            game.planszaGracza[poz.x, poz.y] = true;
                            wyczyscProstokaty(game.planszaGracza, pR);
                        }
                    }
                    else
                    {
                        if(pcp == PionowoCzyPoziomo.PIONOWO)
                        {
                            if (Math.Abs(poz.y - game.statkiGracza[aktualnyButton].pozycja[0].y) != 0)
                                return;
                            bool warunek = false;
                            foreach(Pozycja p in game.statkiGracza[aktualnyButton].pozycja)
                            {
                                if (Math.Abs(poz.x - p.x) == 1)
                                    warunek = true;
                            }
                            if(warunek)
                            {

                                int wolnePolaGora = 0, wolnePolaDol = 0;
                                for (int i = 1; i <= (int)rozmiarStatku[aktualnyButton]; ++i)
                                {
                                    if (poz.x - i >= 0 && !game.planszaGracza[poz.x - i, poz.y])
                                        ++wolnePolaGora;
                                    if (poz.x + i <= 8 && !game.planszaGracza[poz.x + i, poz.y])
                                        ++wolnePolaDol;
                                }

                                game.statkiGracza[aktualnyButton].pozycja.Add(poz);
                                game.planszaGracza[poz.x, poz.y] = true;
                                wyczyscProstokaty(game.planszaGracza, pR);
                            }
                        }
                        else
                        {
                            if (Math.Abs(poz.x - game.statkiGracza[aktualnyButton].pozycja[0].x) != 0)
                                return;
                            bool warunek = false;
                            foreach (Pozycja p in game.statkiGracza[aktualnyButton].pozycja)
                            {
                                if (Math.Abs(poz.y - p.y) == 1)
                                    warunek = true;
                            }
                            if (warunek)
                            {

                                int wolnePolaLewo = 0, wolnePolaPrawo = 0;
                                for (int i = 1; i <= (int)rozmiarStatku[aktualnyButton]; ++i)
                                {
                                    if (poz.y - i >= 0 && !game.planszaGracza[poz.x, poz.y - i])
                                        ++wolnePolaLewo;
                                    if (poz.y + i <= 8 && !game.planszaGracza[poz.x, poz.y + i])
                                        ++wolnePolaPrawo;
                                }
                                game.statkiGracza[aktualnyButton].pozycja.Add(poz);
                                game.planszaGracza[poz.x, poz.y] = true;
                                wyczyscProstokaty(game.planszaGracza, pR);
                            }
                        }
                    }
                }
                if (game.statkiGracza[aktualnyButton].pozycja.Count == (int)rozmiarStatku[aktualnyButton])
                {
                    /*foreach (Pozycja p in game.statkiGracza[aktualnyButton].pozycja)
                    {
                        game.planszaGracza[p.x, p.y] = true;
                    }*/
                    aktualnyButton = -1;
                    ++licznikStatkow;
                    //wyczyscProstokaty(game.planszaGracza, pR);
                    changeButtonsVisible();
                }
            }
        }

        private void p00_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(0, 0);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p01_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(0, 1);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p02_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(0, 2);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p03_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(0, 3);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p04_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(0, 4);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p05_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(0, 5);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p06_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(0, 6);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p07_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(0, 7);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p08_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(0, 8);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p10_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(1, 0);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p11_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(1, 1);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p12_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(1, 2);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p13_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(1, 3);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p14_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(1, 4);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p15_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(1, 5);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p16_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(1, 6);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p17_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(1, 7);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p18_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(1, 8);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p20_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(2, 0);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p21_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(2, 1);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p22_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(2, 2);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p23_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(2, 3);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p24_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(2, 4);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p25_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(2, 5);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p26_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(2, 6);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p27_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(2, 7);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p28_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(2, 8);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p30_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(3, 0);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p31_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(3, 1);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p32_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(3, 2);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p33_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(3, 3);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p34_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(3, 4);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p35_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(3, 5);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p36_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(3, 6);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p37_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(3, 7);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p38_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(3, 8);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p40_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(4, 0);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p41_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(4, 1);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p42_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(4, 2);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p43_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(4, 3);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p44_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(4, 4);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p45_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(4, 5);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p46_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(4, 6);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p47_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(4, 7);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p48_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(4, 8);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p50_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(5, 0);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p51_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(5, 1);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p52_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(5, 2);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p53_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(5, 3);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p54_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(5, 4);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p55_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(5, 5);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p56_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(5, 6);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p57_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(5, 7);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p58_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(5, 8);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p60_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(6, 0);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p61_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(6, 1);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p62_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(6, 2);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p63_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(6, 3);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p64_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(6, 4);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p65_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(6, 5);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p66_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(6, 6);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p67_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(6, 7);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p68_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(6, 8);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p70_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(7, 0);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p71_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(7, 1);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p72_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(7, 2);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p73_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(7, 3);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p74_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(7, 4);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p75_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(7, 5);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p76_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(7, 6);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p77_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(7, 7);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p78_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(7, 8);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p80_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(8, 0);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p81_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(8, 1);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p82_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(8, 2);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p83_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(8, 3);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p84_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(8, 4);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p85_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(8, 5);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p86_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(8, 6);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p87_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(8, 7);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void p88_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pozycja poz = new Pozycja(8, 8);
            StworzStatekPoprzezKlikanieWPlansze(poz);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            bb[0] = true;
            aktualnyButton = 0;
            game.statkiGracza[0] = new Statek(game.planszaGracza);
            ++tab[0];
            changeButtonsVisible();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            bb[1] = true;
            aktualnyButton = 1;
            game.statkiGracza[1] = new Statek(game.planszaGracza);
            ++tab[1];
            changeButtonsVisible();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            bb[2] = true;
            aktualnyButton = 2;
            game.statkiGracza[2] = new Statek(game.planszaGracza);
            ++tab[2];
            changeButtonsVisible();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            bb[3] = true;
            aktualnyButton = 3;
            game.statkiGracza[3] = new Statek(game.planszaGracza);
            ++tab[3];
            changeButtonsVisible();
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            bb[4] = true;
            aktualnyButton = 4;
            game.statkiGracza[4] = new Statek(game.planszaGracza);
            ++tab[4];
            changeButtonsVisible();
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            bb[5] = true;
            aktualnyButton = 5;
            game.statkiGracza[5] = new Statek(game.planszaGracza);
            ++tab[5];
            changeButtonsVisible();
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            bb[6] = true;
            aktualnyButton = 6;
            game.statkiGracza[6] = new Statek(game.planszaGracza);
            ++tab[6];
            changeButtonsVisible();
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            bb[7] = true;
            aktualnyButton = 7;
            game.statkiGracza[7] = new Statek(game.planszaGracza);
            ++tab[7];
            changeButtonsVisible();
        }
    }
}
