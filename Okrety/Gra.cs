using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okrety
{
    class Gra
    {
        static Random rand = new Random();
        public Statek[] statkiGracza;
        public Statek[] statkiKomputera;

        public Boolean[,] planszaGracza;
        public Boolean[,] planszaKomputera;

        public Boolean[,] planszaGraczaKopia;
        private Boolean[,] planszaKomputeraKopia;

        private List<Pozycja>[] statkiK;
        private List<Pozycja>[] statkiG;

        MainWindow mw;

        bool koniec = false;

        public Gra(MainWindow mw)
        {
            this.mw = mw;
            planszaGracza = new Boolean[9, 9];
            planszaKomputera = new Boolean[9, 9];
            statkiGracza = new Statek[8];
            statkiKomputera = new Statek[8];
            statkiKomputera[0] = new Statek(JakiStatek.POJEDYNCZY, planszaKomputera);
            statkiKomputera[1] = new Statek(JakiStatek.POJEDYNCZY, planszaKomputera);
            statkiKomputera[2] = new Statek(JakiStatek.POJEDYNCZY, planszaKomputera);
            statkiKomputera[3] = new Statek(JakiStatek.PODWOJNY, planszaKomputera);
            statkiKomputera[4] = new Statek(JakiStatek.PODWOJNY, planszaKomputera);
            statkiKomputera[5] = new Statek(JakiStatek.POTROJNY, planszaKomputera);
            statkiKomputera[6] = new Statek(JakiStatek.POTROJNY, planszaKomputera);
            statkiKomputera[7] = new Statek(JakiStatek.POCZWORNY, planszaKomputera);

            statkiK = new List<Pozycja>[8];
            statkiG = new List<Pozycja>[8];

            for (int i = 0; i < 8; ++i)
            {
                statkiK[i] = new List<Pozycja>();
                statkiG[i] = new List<Pozycja>();
                statkiK[i] = statkiKomputera[i].pozycja;
            }

            planszaGraczaKopia = new Boolean[9, 9];
            planszaKomputeraKopia = new Boolean[9, 9];

            mw.wyczyscProstokaty(planszaKomputera, mw.cR);

        }

        public void ustawStatkiGracza()
        {
            for(int i = 0; i < 8; ++i)
                statkiG[i] = statkiGracza[i].pozycja;
        }

        public bool TuraGracza(Pozycja poz)
        {
            mw.mainInfoLabel.Content = "Twoja kolej!";

            planszaKomputeraKopia[poz.x, poz.y] = true;

            foreach (List<Pozycja> p in statkiK)
            {
                for (int i = 0; i < p.Count; ++i)
                    if (p[i].x == poz.x && p[i].y == poz.y)
                    {
                        mw.malnijJednoPoleKomputera(poz.x, poz.y, true);
                        mw.mainInfoLabel.Content = "Gratulacje, trafiłeś statek.";
                        p.Remove(p[i]);
                        if (p.Count == 0)
                            mw.mainInfoLabel.Content = "Trafiony zatopiony.";
                        if (CzyKoniec())
                        {
                            koniec = true;
                            return false;
                        }
                        return true;
                    }
            }
            mw.malnijJednoPoleKomputera(poz.x, poz.y, false);
            mw.mainInfoLabel.Content = "Pudło.";
            return false;
        }

        public bool TuraPC()
        {
            int wylosowanePoleX;
            int wylosowanePoleY;
            do
            {
                wylosowanePoleX = rand.Next(9);
                wylosowanePoleY = rand.Next(9);
            } while (planszaGraczaKopia[wylosowanePoleX, wylosowanePoleY]);

            planszaGraczaKopia[wylosowanePoleX, wylosowanePoleY] = true;

            foreach(List<Pozycja> p in statkiG)
            {
                for(int i = 0; i < p.Count; ++i)
                    if(p[i].x == wylosowanePoleX && p[i].y == wylosowanePoleY)
                    {
                        mw.malnijJednoPoleGracza(wylosowanePoleX, wylosowanePoleY, true);
                        mw.mainInfoLabel.Content = "Komputer trafił statek.";
                        p.Remove(p[i]);
                        if (p.Count == 0)
                            mw.mainInfoLabel.Content = "Komputer trafił\ni zatopił statek.";
                        if (CzyKoniec())
                        {
                            koniec = true;
                            return false;
                        }
                        return true;
                    }
            }
            mw.malnijJednoPoleGracza(wylosowanePoleX, wylosowanePoleY, false);
            mw.mainInfoLabel.Content = "Pudło komputera.";
            return false;
        }

        public void Rozgrywka(Pozycja poz)
        {
            if (koniec)
                return;
            if (mw.ustawioneStatki)
            {
                if (!mw.czyUstawioneStatkiWGrze)
                {
                    ustawStatkiGracza();
                    mw.czyUstawioneStatkiWGrze = true;
                }
                if (TuraGracza(poz))
                    return;
                if (koniec)
                    return;
                while (TuraPC()) ;
            }
        }

        public bool CzyKoniec()
        {
            bool czyWygralGracz = true;
            bool czyWygralKomputer = true;
            foreach(List<Pozycja> p in statkiK)
            {
                if (p.Count != 0)
                    czyWygralGracz = false;
            }

            if(czyWygralGracz)
            {
                mw.mainInfoLabel.Content = "GRATULACJE!!!\nWYGRANA!!!";
                return true;
            }

            foreach(List<Pozycja> p in statkiG)
            {
                if (p.Count != 0)
                    czyWygralKomputer = false;
            }

            if(czyWygralKomputer)
            {
                mw.mainInfoLabel.Content = "Niestety.\nPRZEGRANA.";
                return true;
            }

            return false;
        }

    }
}
