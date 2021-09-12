using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse.AI;
using Verse.Sound;
using HarmonyLib;
using HarmonyMod;
using Verse;
using System.IO;
using UnityEngine;
using System.Reflection;
using CombatExtended;
using RimWorld.Planet;
using System.Xml;
using RimWorld.BaseGen;

namespace HandLoading
{
  public class exploder : ThingComp
    {
        public bool checl;
        public float faketicker;
        public CompAmmoUser compammo;
        public override void Notify_UsedWeapon(Pawn pawn)
        {
            compammo = this.parent.TryGetComp<CompAmmoUser>();
            if (compammo != null)
            {
                
                float damchange = 0.1f;
                if (compammo.CurrentAmmo.comps.Any(oof => oof is damagercproprs) && faketicker != 10)
                {
                    damagercproprs damagercproprs = compammo.CurrentAmmo.comps.Find(oof2 => oof2 is damagercproprs) as damagercproprs;
                    
                    faketicker++;
                    damchange = damagercproprs.dammpoints;
                    Log.Message(damchange.ToString());
                    Log.Message("teszt");
                    //AmmoExplodyBitsOrSomethingIdk testrn = ;
                   
                }
                if (compammo.CurrentAmmo.comps.Any(oof => oof is damagercproprs) && faketicker == 10)
                {
                    damagercproprs damagercproprs = compammo.CurrentAmmo.comps.Find(oof2 => oof2 is damagercproprs) as damagercproprs;
                    damchange = damagercproprs.dammpoints;
                    faketicker = 0;
                    Log.Message(damchange.ToString());
                    Log.Message("teszt zwei");
                    //AmmoExplodyBitsOrSomethingIdk testrn = ;
                    this.parent.HitPoints -= (int)Math.Round(damchange * 20);
                    if(this.parent.HitPoints > 0)
                    {
                        if (Rand.Chance(0.25f))
                        {
                            GenExplosionCE.DoExplosion(this.parent.Position, Find.CurrentMap, Rand.Range(1, 2), DamageDefOf.Burn, this.parent, 20, Rand.Range(8.25f, 20.5f), SoundDefOf.Vomit);
                            this.parent.Kill(null);
                        }
                    }

                }

            }
            base.Notify_UsedWeapon(pawn);

        }
        public override void Notify_Equipped(Pawn pawn)
        {
           
            CompAmmoUser comammp = this.parent.TryGetComp<CompAmmoUser>();
            //Log.Message(this.ParentHolder.ToString(), true);
            //&& comammp.Props.ammoSet.ammoTypes.Any(LL => LL.ammo.ammoClass.defName)
            if (pawn.Faction != Faction.OfPlayer && comammp != null)
            {
                Log.Message("ghjk");
                Log.Message(this.ParentHolder.ToString());
                if((!(this.ParentHolder is Map)) && comammp.Props.ammoSet.ammoTypes.Any(PP => PP.ammo.ammoClass.defName == "FullMetalJacket") && !(pawn is WorldPawns))
                {
                    Log.Message("ghjk2");
                    if (Rand.Chance(1f))
                    {
                        Log.Message("ghjk3");
                        FieldInfo[] fields = typeof(ThingDef).GetFields(
                        BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                        FieldInfo[] fields2 = typeof(AmmoDef).GetFields(
                        BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                        ThingDef projectile = new ThingDef();
                        AmmoDef ammo = new AmmoDef();
                        foreach (FieldInfo fi in fields)
                        {
                            fi.SetValue(projectile, fi.GetValue(DefDatabase<ThingDef>.AllDefs.ToList().Find(P => P.defName == "Bullet_556x45mmNATO_FMJ")));
                        }
                        foreach (FieldInfo fi in fields2)
                        {
                            fi.SetValue(ammo, fi.GetValue(DefDatabase<ThingDef>.AllDefs.ToList().Find(P => P.defName == "Ammo_556x45mmNATO_FMJ")));
                        }
                       
                        projectile.defName = "fghjk" + Rand.Range(0, 69) + Rand.Bool.ToString() + Rand.Range(0.123456f, 69f) + "d";
                        comammp.Props.ammoSet.ammoTypes.Add(new AmmoLink { ammo = ammo, projectile = projectile });
                        ammo.graphic = comammp.CurrentAmmo.graphic;
                        ammo.graphicData = comammp.CurrentAmmo.graphicData;
                        ProjectilePropertiesCE propsce = new ProjectilePropertiesCE();
                        FieldInfo[] fields3 = typeof(ProjectileProperties).GetFields(
                        BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                        foreach (FieldInfo fi in fields3)
                        {
                            fi.SetValue(propsce, fi.GetValue(DefDatabase<ThingDef>.AllDefs.ToList().Find(P => P.defName == "Bullet_556x45mmNATO_FMJ").projectile));
                        }
                        
                        calculutils.CalAP(calculutils.materials().RandomElement(), projectile, calculutils.powders().RandomElement(), ref propsce.armorPenetrationSharp);
                        projectile.projectile = propsce;
                        ammo.label = "hand loaded " + comammp.Props.ammoSet.label;
                        Log.Message(propsce.armorPenetrationSharp + this.parent.Label);
                        Thing shring = ThingMaker.MakeThing(ammo);
                        shring.stackCount = 240;
                       


                    }
                }
            }
            base.Notify_Equipped(pawn);
        }

    }
    public class AmmoExplodyBitsOrSomethingIdk : ThingComp
    {
        public float dampoints => Props.dammpoints;

       
        public damagercproprs Props => (damagercproprs)this.props;
    }
    public class damagercproprs : CompProperties
    {
        public float dammpoints = 0.1f;
       
        public damagercproprs()
        {
            this.compClass = typeof(AmmoExplodyBitsOrSomethingIdk);
        }

        public damagercproprs(Type compClass) : base(compClass)
        {
            this.compClass = compClass;
        }
    }
}
