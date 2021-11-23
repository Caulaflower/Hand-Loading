using CombatExtended;
using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace HandLoading
{
    [HarmonyPatch(typeof(PawnInventoryGenerator), "GenerateInventoryFor")]
    static class HarmonyFuckAmmos
    {
        public static float water(AmmoLink link)
        {


            //| link.ammo == null | link.ammo.statBases == null | link.projectile == null | link.projectile.projectile == null
            ////Log.Error("test1");
            if (link == null )
            {
               ////Log.Error("test1");
                return -1f;
            }
            else
            {
                ////Log.Error("test2");
                if (link.ammo == null)
                {
                    return 0f;
                }
                ////Log.Error("test3");
                if (link.ammo.statBases == null)
                {
                    return 0f;
                }
                ////Log.Error("test4");
                if (link.projectile == null)
                {
                    return 0f;
                }
                ////Log.Error("test5");
                if (link.projectile.projectile == null)
                {
                    return 0f;
                }
                ////Log.Error("test6");
            }
           




            var pear = 1f;
            var tiredness = (ProjectilePropertiesCE)link.projectile.projectile;
            var TT32 = tiredness.GetDamageAmount(1);
            if (TT32 < 1 && link.projectile.HasModExtension<BulletModExtension>())
            {
                TT32 = (int)link.projectile.GetModExtension<BulletModExtension>().FixedDamage;
            }
            pear = ((tiredness.armorPenetrationSharp * TT32) * tiredness.pelletCount / ((link.ammo?.statBases?.Find(pp => pp.stat == StatDefOf.MarketValue)?.value ?? 3f) * (link.ammo?.statBases?.Find(pp => pp.stat == StatDefOf.Mass)?.value ?? 3f)));


            return pear;
        }
        public static void Postfix(Pawn p, PawnGenerationRequest request)
        {

            if (p.Faction == null)
            {
                return;
            }
            if (p.Faction.def == null)
            {
                return;
            }

            ///float Ammo_Money = 100f;

            ///Ammo_Money *= (int)p.Faction.def.techLevel;


            Func<AmmoLink, float> waterfunc = x => water(x);

          

           

            CompInventory inv = p.TryGetComp<CompInventory>();
            
            if ((p.equipment?.Primary ?? null) != null && inv != null)
            {
                if (Find.World.GetComponent<IhaveacoldandImustsneeze>()?.Factionammos?.FindAll(
                    tt33 => tt33 != null && tt33.ammo != null && tt33.Faction != null && tt33?.Faction == p.Faction?.ToString()
                    ).Any(tt33 => (tt33.ammo.AmmoSetDefs.Contains(p.equipment.Primary?.TryGetComp<CompAmmoUser>()?.Props.ammoSet))) ?? false)
                {
                    inv.container.RemoveAll(t33 => t33 is AmmoThing);

                    var idkanmae = Find.World.GetComponent<IhaveacoldandImustsneeze>().Factionammos.FindAll(tt332 => tt332 != null && tt332.Faction != null && tt332.ammo != null && tt332.Faction == p.Faction.ToString()).FindAll(pp33 => (pp33.ammo.AmmoSetDefs.Contains(p.equipment.Primary?.TryGetComp<CompAmmoUser>()?.Props.ammoSet)));
                    List<AmmoLink> ammolinkos = new List<AmmoLink>();
                    foreach (FactionHandLoad keyvalue in idkanmae)
                    {
                        AmmoDef amdef = keyvalue.ammo;
                        if (amdef != null)
                        {
                            AmmoSetDef caliber = amdef.AmmoSetDefs.First();
                            ammolinkos.Add(caliber.ammoTypes.Find(ps => ps.ammo == amdef));
                        }
                       
                    }

                    ammolinkos.RemoveAll(t => t == null);
                    ammolinkos.RemoveAll(tt => tt.ammo == null);
                    ammolinkos.RemoveAll(ttt => ttt.projectile == null);
                    AmmoDef finalAmdef = p.equipment.Primary?.TryGetComp<CompAmmoUser>()?.Props.ammoSet.ammoTypes[1].ammo;

                    if (ammolinkos.Count > 0)
                    {
                        finalAmdef = ammolinkos.RandomElementByWeightWithFallback(waterfunc, ammolinkos.First()).ammo;

                    }
                    
                    if (finalAmdef == null)
                    {
                        finalAmdef = p.equipment.Primary.TryGetComp<CompAmmoUser>().Props.ammoSet.ammoTypes.RandomElement().ammo;
                    }
                    AmmoThing amthin = (AmmoThing)ThingMaker.MakeThing(finalAmdef);
                    var amountofammo = Math.Round(Rand.Range(p.kindDef.GetModExtension<LoadoutPropertiesExtension>().primaryMagazineCount.min, p.kindDef.GetModExtension<LoadoutPropertiesExtension>().primaryMagazineCount.max)) * p.equipment.Primary.TryGetComp<CompAmmoUser>().Props.magazineSize;
                    var varA = 1f;
                    Find.World.GetComponent<IhaveacoldandImustsneeze>().ammo_capacity.TryGetValue(p.Faction.def, out varA);
                    var varB = varA / (Find.World.worldObjects.Settlements.FindAll(pw => pw.Faction == p.Faction).Count / 2);
                    var pain = amountofammo * varB;
                    pain = amountofammo * 1f;
                    amthin.stackCount = (int)((pain) + p.equipment.Primary.TryGetComp<CompAmmoUser>().Props.magazineSize);
                    ////Log.Message(amthin.stackCount.ToString());
                    inv.container.TryAddOrTransfer(amthin, 20000);
                    p.equipment.Primary.TryGetComp<CompAmmoUser>().LoadAmmo(amthin);
                }
            }

         


        }
    }
    public class amigus : Mod
    {
        public static Settings settings;

        public amigus(ModContentPack content) : base(content)
        {

            Harmony har = new Harmony("Caulaflower.HandLoading.CustomAmmo");
            har.PatchAll();
            
        }
    }
}
