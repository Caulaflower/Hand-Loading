using RimWorld;
using RimWorld.Planet;
using System.Linq;
using UnityEngine;
using Verse;
using System;
using System.Collections.Generic;

namespace HandLoading
{
    public class ManufacturingPlant : WorldObjectComp
    {
        public bool ismanufacturingplant = true;

        public float Exp_GainPerTick
        {
            get
            {
                float result = 1f;
                return result;
            }
        }


        #region Remains of code from last implementation
        /*
        public float ManuFacturingPlantChance
        {
            get
            {
                var idfk = 0.25f;

                TechLevel tech = this.parent.Faction.def.techLevel;

                switch (tech)
                {
                    case TechLevel.Neolithic:
                        idfk = 0.75f;
                        break;
                    case TechLevel.Medieval:
                        idfk = 0.65f;
                        break;
                    case TechLevel.Industrial:
                        idfk = 0.55f;
                        break;
                    case TechLevel.Spacer:
                        idfk = 0.35f;
                        break;
                    case TechLevel.Ultra:
                        idfk = 0.25f;
                        break;
                }

                return idfk;
            }
        }
        */
        #endregion
        

        public override void Initialize(WorldObjectCompProperties props)
        {
            base.Initialize(props);

        }

        public int ticks = 0;
        public override void CompTick()
        {

            base.CompTick();
        }

        public override string GetDescriptionPart()
        {
            if (ismanufacturingplant)
            {
                return this.parent.Faction.Name + "'s ammo manufacturing plant ";
            }
            else
            {
                return base.GetDescriptionPart();
            }

        }

        public override string CompInspectStringExtra()
        {
            if (ismanufacturingplant)
            {
                return this.parent.Faction.Name + "'s ammo manufacturing plant. Ammo manufacturing capacity: " + mycapacity.ToString().Colorize(CoolColors.cool_purple);
            }
            else
            {
                return base.CompInspectStringExtra();
            }

        }

        public float myexp = 0f;

        public float mycapacity
        {
            get
            {
                float result = 1f;

                result *= (myexp / 60);

                return result;
            }
            set
            {

            }
        }

        public void DecreaseAmmo_Cap()
        {
            if (ismanufacturingplant)
            {
                var ammo_capacity = Find.World.GetComponent<Faction_Ammo_Controller>().ammo_capacity;
                var obj = this.parent;


                var varB = ammo_capacity.ToList().Find(t => t.Key == obj.Faction.def);
                //Log.Message("varB value: " + varB.Value.ToString().Colorize(Color.blue) + " for " + obj.Faction.Name + " decreased.".Colorize(Color.magenta));
                ammo_capacity.Remove(obj.Faction.def);
                ammo_capacity.Add(obj.Faction.def, (varB.Value - mycapacity));
            }

        }

        public override void PostPostRemove()
        {
            DecreaseAmmo_Cap();
            base.PostPostRemove();
        }

        public override void PostExposeData()
        {
            base.PostExposeData();
        }
    }



    public class Faction_Ammo_Controller : WorldComponent
    {
        public Dictionary<FactionDef, float> ammo_capacity = new Dictionary<FactionDef, float>();

        public Faction_Ammo_Controller(World world) : base(world)
        {

        }
        

    }

    public static class plantutil
    {
        public static float BaseCapacityPerPlant(WorldObject worldobj)
        {

            var idfk = 1f;

            TechLevel tech = worldobj.Faction.def.techLevel;

            switch (tech)
            {
                case TechLevel.Neolithic:
                    idfk = 0.35f;
                    break;
                case TechLevel.Medieval:
                    idfk = 0.40f;
                    break;
                case TechLevel.Industrial:
                    idfk = 0.85f;
                    break;
                case TechLevel.Spacer:
                    idfk = 1.1f;
                    break;
                case TechLevel.Ultra:
                    idfk = 1.30f;
                    break;
            }

            return idfk;
        }
    }

   
}
