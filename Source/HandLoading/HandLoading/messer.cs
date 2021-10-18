using CombatExtended;
using HarmonyLib;
using RimWorld;
using System;
using System.Linq;
using Verse;

namespace HandLoading
{
    [HarmonyPatch(typeof(PawnInventoryGenerator), "GenerateInventoryFor")]
    static class HarmonyFuckAmmos
    {
        public static void Postfix(Pawn p, PawnGenerationRequest request)
        {
            CompInventory inv = p.TryGetComp<CompInventory>();
            Log.Message(p?.equipment?.Primary?.Label ?? "noequipment");
            if ((p.equipment?.Primary ?? null) != null && inv != null)
            {
                Log.Message("good morning");
                if (Find.World.GetComponent<IhaveacoldandImustsneeze>().Factionammos.FindAll(tt33 => tt33.Key == p.Faction.ToString()).Any(tt33 => (tt33.Value.AmmoSetDefs.Contains(p.equipment.Primary?.TryGetComp<CompAmmoUser>()?.Props.ammoSet))))
                {
                    inv.container.Clear();
                    AmmoThing amthin = (AmmoThing)ThingMaker.MakeThing(Find.World.GetComponent<IhaveacoldandImustsneeze>().Factionammos.FindAll(tt33 => tt33.Key == p.Faction.ToString()).FindAll(pp33 => (pp33.Value.AmmoSetDefs.Contains(p.equipment.Primary?.TryGetComp<CompAmmoUser>()?.Props.ammoSet))).RandomElement().Value);
                    amthin.stackCount = ((int)Math.Round(Rand.Range(p.kindDef.GetModExtension<LoadoutPropertiesExtension>().primaryMagazineCount.min, p.kindDef.GetModExtension<LoadoutPropertiesExtension>().primaryMagazineCount.max)) * p.equipment.Primary.TryGetComp<CompAmmoUser>().Props.magazineSize);
                    inv.container.TryAddOrTransfer(amthin, 20000);
                    Log.Message("es is working");
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
