using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;
using UnityEngine;


namespace HandLoading
{
    public enum NamingType : byte
    {
        namer_mk,

        namer_m,
        
        namer_randletter,

        namer_number_letter_number

    }

    public static class NamerUtil
    {
        public static NamingType GetNamingForPawn(Pawn pawn)
        {
            var idk = NamingType.namer_mk;

            Faction myfaction = pawn.Faction;


            if (Find.World.GetComponent<IhaveacoldandImustsneeze>().factionnamings != null)
            {
                if (Find.World.GetComponent<IhaveacoldandImustsneeze>().factionnamings.Count > 0)
                {
                    var imtired = Find.World.GetComponent<IhaveacoldandImustsneeze>().factionnamings.Any(p => p.Key == pawn.Faction.def);
                    if (imtired)
                    {
                        idk = Find.World.GetComponent<IhaveacoldandImustsneeze>().factionnamings.ToList().Find(p => p.Key == pawn.Faction.def).Value;
                    }
                    else
                    {
                        idk = (NamingType)Rand.Range(0, 4);
                        Find.World.GetComponent<IhaveacoldandImustsneeze>().factionnamings.Add(myfaction.def, idk);
                    }
                }
                else
                {
                    idk = (NamingType)Rand.Range(0, 4);
                    Find.World.GetComponent<IhaveacoldandImustsneeze>().factionnamings.Add(myfaction.def, idk);
                }
            }
           


            return idk;


        }

        public static string testpan(Pawn pawn)
        {
            var sleep = "";

            int HLFactionCount = 1;

            NamingType socks = GetNamingForPawn(pawn);

            if (socks == NamingType.namer_m)
            {
                sleep += "M" + Math.Abs(HLFactionCount - ((int)(Rand.Range(-10f, 10f)) ) );
                
            }
            if (socks == NamingType.namer_mk)
            {
                sleep += "mk" + Math.Abs(HLFactionCount - ((int)(Rand.Range(-10f, 10f))));

            }
            if (socks == NamingType.namer_randletter)
            {
                var idk = "abcdefghijklmnoprstuvxyz";

                

                sleep += idk.RandomElement().ToString().CapitalizeFirst() + Math.Abs(HLFactionCount - ((int)(Rand.Range(-10f, 10f))));

            }
            if (socks == NamingType.namer_number_letter_number)
            {
                var idk = "abcdefghijklmnoprstuvxyz";

               

                sleep = Rand.Range(0, 9).ToString() + idk.RandomElement().ToString().CapitalizeFirst() + HLFactionCount.ToString();
            }

            if (socks == NamingType.namer_mk | socks == NamingType.namer_m)
            {
                if (Rand.Chance(0.30f))
                {
                    sleep += "A1";
                }
            }


            sleep.CapitalizeFirst();
            //Log.Message(socks.ToString().Colorize(CoolColors.cool_purple));
            return sleep;
        }
    }
    public static class CoolColors
    {
        public static Color cool_purple
        {
            get
            {
                return new Color(r: 255, g: 0, b:255);
            }
        }
        public static Color cool_kremowy
        {
            get
            {
                return new Color(r: 255, g: 229, b: 180);
            }
        }
        public static Color cool_orange
        {
            get
            {
                return new Color(r: 100, g: 55, b: 0);
            }
        }
    }
}
