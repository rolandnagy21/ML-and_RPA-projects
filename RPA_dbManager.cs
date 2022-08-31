using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPAprojectFW
{
    public static class dbManager
    {
        
        public static void AddTelefon(Telefon t)
        {
            RPAEntities context = new RPAEntities();

            context.Telefons.Add(t);
            context.SaveChanges();
        }

        public static string GetTelefonNameById(int id)
        {
            RPAEntities context = new RPAEntities();

            var query = from Telefon in context.Telefons
                        where Telefon.Id == id
                        select Telefon;

            var result = query.FirstOrDefault();
            return result.Nev;
        }

        public static bool IsTelefonNew(Telefon t)
        {
            RPAEntities context = new RPAEntities();

            var query = from Telefon in context.Telefons
                        where Telefon.Nev == t.Nev
                        select Telefon;

            if (query.Any())
            {
                //ha van már ilyen nevű telefon a listában
                //ekkor megnézzük változott-e az állapota, dátuma, ára

                var res = query.FirstOrDefault();
                string allapot = res.Allapot.Replace(" ", "");
                if (allapot != t.Allapot) return true;
                if (res.Datum != t.Datum) return true;
                if (res.Ar != t.Ar) return true;
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
