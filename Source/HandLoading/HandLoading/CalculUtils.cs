using System;
using System.Collections.Generic;
using System.Linq;
using CombatExtended;
using RimWorld;
using UnityEngine;
using Verse;
using HarmonyLib;
using HarmonyMod;

namespace HandLoading
{
    public static class calculutils
    {
		public static List<string> shapes()
		{
			List<string> amogus = new List<string>();
			amogus.Add("Hollow point"); 
			return amogus;
		}
		public static List<ThingDef> materials()
		{
			List<ThingDef> amogus = new List<ThingDef>();
			amogus = DefDatabase<ThingDef>.AllDefs.ToList().FindAll(L => L.stuffProps?.categories?.Contains(StuffCategoryDefOf.Metallic) is true);
			return amogus;
		}
		public static ThingDef material()
		{
			return calculutils.materials().RandomElement();
		}
		public static List<ThingDef> powders()
		{
			List<ThingDef> amogus = new List<ThingDef>();
			amogus = DefDatabase<ThingDef>.AllDefs.ToList().FindAll(OOPP => OOPP.comps.Any(OO => OO is PowderCompProps));
			return amogus;
		}
		public static ThingDef powder()
		{
			ThingDef result = new ThingDef();
			result = calculutils.powders().RandomElement();
			if(result.defName == "Archotechiumpowdered")
			{
				if (Rand.Chance(0.02f))
				{
					return result;
				}
				else
				{
					result = calculutils.powders().First();
				}
			}
			return result;
		}


        public static void CalAP(ThingDef material, ThingDef projbase, ThingDef propelant, ref float PenNN)
        {
			
			string ProjectileShape = shapes().RandomElement();
			float ShapeDoubleAP = 1f;
			switch (ProjectileShape)
			{
				case "Hollow point":
					ShapeDoubleAP = 0.5f;
					break;

				case "Armor piercing":
					ShapeDoubleAP = 2f;
					break;

				case "Full Metal Jacket":
					ShapeDoubleAP = 1f;
					break;

				case "Sabot":
					ShapeDoubleAP = 3.5f;
					break;

			}
			ProjectilePropertiesCE propsCE = projbase?.projectile as ProjectilePropertiesCE;
			float Penmult2 = new float { };
			Penmult2 = propelant.statBases.Find(abc => abc.stat.defName == "PowderPower")?.value ?? 0f;
			if (Penmult2 == 0f)
			{
				Log.Error("propelant most likely has no PowderPower statbase");
			}
			float hardness_material_multiplier = material.statBases.Find(Peepee => Peepee.stat == StatDefOf.StuffPower_Armor_Sharp).value;



			PenNN = (float)((propsCE?.armorPenetrationSharp ?? 1) + (ShapeDoubleAP * hardness_material_multiplier) * Penmult2 * Rand.Range(1f, 3f));
			Log.Message(projbase.ToString());
			Log.Message(PenNN.ToString());
		}
    }
}
