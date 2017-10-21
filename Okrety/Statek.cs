using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okrety
{
    class Statek
    {
        static Random rand = new Random();
        public static readonly int n = 8;
        int rozmiar { get; set; }
        Boolean[,] plansza;
        public List<Pozycja> pozycja;
        MainWindow mw = null;

        public Statek()
        {

        }

        public Statek(Boolean[,] plansza)
        {
            this.plansza = plansza;
            pozycja = new List<Pozycja>();
        }

        public Statek(MainWindow mw)
        {
            this.mw = mw;

        }

        public Statek(JakiStatek rozmiar, Boolean[,] plansza)
        {
            this.rozmiar = (int)rozmiar;
            this.plansza = plansza;
            pozycja = new List<Pozycja>();
            Pozycja pozycjaTmp = new Pozycja();
            PionowoCzyPoziomo pcp = new PionowoCzyPoziomo();
            do
            {
                if(pozycja.Count == 0)
                {
                    int xTmp = rand.Next(n + 1);
                    int yTmp = rand.Next(n + 1);
                    pozycjaTmp = new Pozycja(xTmp, yTmp);

                    if (!SprawdzCzyDookolaNieMaJuzStatku(pozycjaTmp, pozycja))
                    {
                        pozycja.Add(pozycjaTmp);
                        if (pozycja.Count == 1)
                        {
                            int pionowoCzyPoziomo = rand.Next(2);
                            if (pionowoCzyPoziomo == 0)
                                pcp = PionowoCzyPoziomo.PIONOWO;
                            else
                                pcp = PionowoCzyPoziomo.POZIOMO;
                        }
                    }
                }
                else
                {
                    int goraCzyDol = -1;
                    int prawoCzyLewo = -1;
                    while (pozycja.Count != this.rozmiar && pozycja.Count != 0)
                    {
                        if (pcp == PionowoCzyPoziomo.POZIOMO)
                        {
                            if(pozycjaTmp.y == 0)
                            {
                                Pozycja poz = new Pozycja(pozycjaTmp.x, pozycjaTmp.y + 1);
                                if (!SprawdzCzyDookolaNieMaJuzStatku(poz, pozycja))
                                {
                                    pozycja.Add(poz);
                                    prawoCzyLewo = 1;
                                    pozycjaTmp = poz;
                                }
                                else
                                    pozycja.Clear();
                            }
                            else if(pozycjaTmp.y == n)
                            {
                                Pozycja poz = new Pozycja(pozycjaTmp.x, pozycjaTmp.y - 1);
                                if (!SprawdzCzyDookolaNieMaJuzStatku(poz, pozycja))
                                {
                                    pozycja.Add(poz);
                                    prawoCzyLewo = 0;
                                    pozycjaTmp = poz;
                                }
                                else
                                    pozycja.Clear();
                            }
                            else
                            {
                                if(prawoCzyLewo == -1)
                                {
                                    prawoCzyLewo = rand.Next(2);
                                }
                                if(prawoCzyLewo == 0)
                                {
                                    Pozycja poz = new Pozycja(pozycjaTmp.x, pozycjaTmp.y - 1);
                                    if (!SprawdzCzyDookolaNieMaJuzStatku(poz, pozycja))
                                    {
                                        pozycja.Add(poz);
                                        pozycjaTmp = poz;
                                    }
                                    else
                                        pozycja.Clear();
                                }
                                else if(prawoCzyLewo == 1)
                                {
                                    Pozycja poz = new Pozycja(pozycjaTmp.x, pozycjaTmp.y + 1);
                                    if (!SprawdzCzyDookolaNieMaJuzStatku(poz, pozycja))
                                    {
                                        pozycja.Add(poz);
                                        pozycjaTmp = poz;
                                    }
                                    else
                                        pozycja.Clear();
                                }
                            }
                        }
                        else if (pcp == PionowoCzyPoziomo.PIONOWO)
                        {
                            if (pozycjaTmp.x == 0)
                            {
                                Pozycja poz = new Pozycja(pozycjaTmp.x + 1, pozycjaTmp.y);
                                if (!SprawdzCzyDookolaNieMaJuzStatku(poz, pozycja))
                                {
                                    pozycja.Add(poz);
                                    goraCzyDol = 0;
                                    pozycjaTmp = poz;
                                }
                                else
                                    pozycja.Clear();
                            }
                            else if (pozycjaTmp.x == n)
                            {
                                Pozycja poz = new Pozycja(pozycjaTmp.x - 1, pozycjaTmp.y);
                                if (!SprawdzCzyDookolaNieMaJuzStatku(poz, pozycja))
                                {
                                    pozycja.Add(poz);
                                    goraCzyDol = 1;
                                    pozycjaTmp = poz;
                                }
                                else
                                    pozycja.Clear();
                            }
                            else
                            {
                                if (goraCzyDol == -1)
                                {
                                    goraCzyDol = rand.Next(2);
                                }
                                if (goraCzyDol == 1)
                                {
                                    Pozycja poz = new Pozycja(pozycjaTmp.x - 1, pozycjaTmp.y);
                                    if (!SprawdzCzyDookolaNieMaJuzStatku(poz, pozycja))
                                    {
                                        pozycja.Add(poz);
                                        pozycjaTmp = poz;
                                    }
                                    else
                                        pozycja.Clear();
                                }
                                else if (goraCzyDol == 0)
                                {
                                    Pozycja poz = new Pozycja(pozycjaTmp.x + 1, pozycjaTmp.y);
                                    if (!SprawdzCzyDookolaNieMaJuzStatku(poz, pozycja))
                                    {
                                        pozycja.Add(poz);
                                        pozycjaTmp = poz;
                                    }
                                    else
                                        pozycja.Clear();
                                }
                            }
                        }
                    }
                    
                }
                
            } while (pozycja.Count != this.rozmiar);

            foreach(Pozycja p in pozycja)
            {
                plansza[p.x, p.y] = true;
            }
            
        }

        // zwraca true gdy jest, false gdy nie ma
        public Boolean SprawdzCzyDookolaNieMaJuzStatku(Pozycja p, List<Pozycja> listaAktualnychPozycji)
        {
            if (plansza[p.x, p.y] == true)
                return true;

            foreach (Pozycja pp in listaAktualnychPozycji)
                if (p.x == pp.x && p.y == pp.y)
                    return true;
            Boolean czySwojak = false;
            if (p.x > 0)
            {
                if (plansza[p.x - 1, p.y])
                {
                    foreach(Pozycja pp in listaAktualnychPozycji)
                    {
                        if (p.x - 1 == pp.x && p.y == pp.y)
                            czySwojak = true;
                    }
                    if (!czySwojak)
                        return true;
                    czySwojak = false;
                }
                if(p.y > 0)
                {
                    if (plansza[p.x - 1, p.y - 1])
                    {
                        foreach (Pozycja pp in listaAktualnychPozycji)
                        {
                            if (p.x - 1 == pp.x && p.y - 1 == pp.y)
                                czySwojak = true;
                        }
                        if (!czySwojak)
                            return true;
                        czySwojak = false;
                    }
                }
            }
            if(p.y > 0)
            {
                if (plansza[p.x, p.y - 1])
                {
                    foreach (Pozycja pp in listaAktualnychPozycji)
                    {
                        if (p.x == pp.x && p.y - 1 == pp.y)
                            czySwojak = true;
                    }
                    if (!czySwojak)
                        return true;
                    czySwojak = false;
                }
                if(p.x < n)
                {
                    if (plansza[p.x + 1, p.y - 1])
                    {
                        foreach (Pozycja pp in listaAktualnychPozycji)
                        {
                            if (p.x + 1 == pp.x && p.y - 1 == pp.y)
                                czySwojak = true;
                        }
                        if (!czySwojak)
                            return true;
                        czySwojak = false;
                    }
                }
            }
            if(p.x < n)
            {
                if (plansza[p.x + 1, p.y])
                {
                    foreach (Pozycja pp in listaAktualnychPozycji)
                    {
                        if (p.x + 1 == pp.x && p.y == pp.y)
                            czySwojak = true;
                    }
                    if (!czySwojak)
                        return true;
                    czySwojak = false;
                }
                if(p.y < n)
                {
                    if (plansza[p.x + 1, p.y + 1])
                    {
                        foreach (Pozycja pp in listaAktualnychPozycji)
                        {
                            if (p.x + 1 == pp.x && p.y + 1 == pp.y)
                                czySwojak = true;
                        }
                        if (!czySwojak)
                            return true;
                        czySwojak = false;
                    }
                }
            }
            if(p.y < n)
            {
                if (plansza[p.x, p.y + 1])
                {
                    foreach (Pozycja pp in listaAktualnychPozycji)
                    {
                        if (p.x == pp.x && p.y + 1 == pp.y)
                            czySwojak = true;
                    }
                    if (!czySwojak)
                        return true;
                    czySwojak = false;
                }
                if(p.x > 0)
                {
                    if (plansza[p.x - 1, p.y + 1])
                    {
                        foreach (Pozycja pp in listaAktualnychPozycji)
                        {
                            if (p.x - 1 == pp.x && p.y + 1 == pp.y)
                                czySwojak = true;
                        }
                        if (!czySwojak)
                            return true;
                        czySwojak = false;
                    }
                }
            }
            return false;
        }

    }
}
