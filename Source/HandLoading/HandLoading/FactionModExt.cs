using CombatExtended;
using RimWorld.Planet;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using UnityEngine;
using RimWorld;
using System.Xml;

namespace HandLoading
{
    public class FactionHandLoad : Def
    {
        public string Faction;

        public AmmoDef ammo;

       

       
    }

    public class IhaveacoldandImustsneeze : WorldComponent
    {
        public IhaveacoldandImustsneeze(World world) : base(world)
        {
            
        }

       

        public int ticks = 0;


        public List<FactionHandLoad> Factionammos = new List<FactionHandLoad>();

        public Dictionary<FactionDef, float> ammo_capacity = new Dictionary<FactionDef, float>();

        public Dictionary<FactionDef, NamingType> factionnamings = new Dictionary<FactionDef, NamingType> { };



        public override void ExposeData()
        {

            
           
            
           

            Scribe_Collections.Look(ref Factionammos, "factionammos", LookMode.Def);

            Scribe_Collections.Look(ref factionnamings, "factionamosdesign", LookMode.Def, LookMode.Value);

            Scribe_Collections.Look(ref ammo_capacity, "ammo_manufacturing_capacity", LookMode.Def, LookMode.Value);



            base.ExposeData();
        }
    }


}
