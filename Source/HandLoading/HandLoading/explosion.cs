using CombatExtended;
using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Verse;

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
                    //Log.Message(damchange.ToString());
                    //Log.Message("teszt zwei");
                    //AmmoExplodyBitsOrSomethingIdk testrn = ;
                    this.parent.HitPoints -= (int)Math.Round(damchange * 20);
                    if (this.parent.HitPoints < 0)
                    {
                        if (Rand.Chance(0.25f))
                        {
                            GenExplosionCE.DoExplosion(pawn.Position, Find.CurrentMap, Rand.Range(1, 2), DamageDefOf.Burn, this.parent, 20, Rand.Range(8.25f, 20.5f), SoundDefOf.Thunder_OnMap);
                            this.parent.Kill(null);
                        }
                    }

                }

            }
            base.Notify_UsedWeapon(pawn);

        }
        public bool dodl = true;
        public override void Notify_Equipped(Pawn pawn)
        {

            if (pawn.Faction != Faction.OfPlayer && dodl)
            {
                if (pawn.ParentHolder is World)
                {
                    Log.Message("worlt");
                    return;
                }
                if (pawn.Faction == Faction.OfAncients && pawn.Faction == Faction.OfAncientsHostile)
                {
                    Log.Message("ancients");
                    return;
                }
                if ((Find.World.GetComponent<IhaveacoldandImustsneeze>().Factionammos?.ToList().FindAll(tt => tt.Key == pawn.Faction.ToString())?.Count ?? 0) < HandeLoading.settings.factionammos)
                {
                    Log.Message(HandeLoading.settings.factionammos.ToString());
                    //Log.Error("adobe");
                    if (this.parent.TryGetComp<CompAmmoUser>()?.Props.ammoSet.ammoTypes.Any(tt33 => tt33.ammo.ammoClass.defName == "FullMetalJacket") ?? false)
                    {

                        CreateAmmosThenGiveThem(pawn);
                    }
                    else if (this.parent.TryGetComp<CompAmmoUser>()?.Props.ammoSet.ammoTypes.Any(tt33 => tt33.ammo.ammoClass.defName == "GrenadeHE" | tt33.ammo.ammoClass.defName == "RocketFrag") ?? false)
                    {

                        CreateAmmosThenGiveThem(pawn, true);
                    }
                }

                else
                {



                    Log.Message("adobe");

                }
                dodl = false;
            }






            base.Notify_Equipped(pawn);
        }
        public int s;

        public float checktechlvls(float input, float input2)
        {
            float result = input / input2;
            if (result > 1)
            {
                result = 0.5f;
            }
            Log.Message("weight: " + result + " source tech level(powder) " + input.ToString() + " source thech level(faction) " + input2.ToString());
            return result;
        }
        public float checktechlvls2(float input, float input2)
        {
            float result = input / input2;
            if (result > 1)
            {
                result = 0.45f;
            }
            Log.Message("weight: " + result + " source tech level(projectile type) " + input.ToString() + " source thech level(faction) " + input2.ToString());
            return result;
        }
        public float chancemult(ThingDef thingdef, Pawn pawn)
        {
            var result = 1f;
            var Mass = thingdef.statBases.Find(tt33 => tt33.stat == StatDefOf.Mass).value;
            var PenBonus = thingdef.statBases.Find(tt33 => tt33.stat == StatDefOf.StuffPower_Armor_Sharp).value;
            var Cost  = thingdef.statBases.Find(tt33 => tt33.stat == StatDefOf.MarketValue).value;
            result = (Mass + PenBonus) / ((Cost) / (Math.Max(1f, (float)pawn.Faction.def.techLevel / 2)));
            Log.Message("Chance weight: " + result.ToString() + " for material: " + thingdef.label);
            return result;
        }
        public float charge(Pawn pawn)
        {
            float result = 1f;
            if(pawn.Faction.def.techLevel <= TechLevel.Medieval)
            {
                result = Rand.Range(1.5f, 4.5f);
            }
            if(pawn.Faction.def.techLevel >= TechLevel.Industrial)
            {
                result = Rand.Range(1.3f, 2.1f);
            }
            return result;
        }
        public void CreateAmmosThenGiveThem(Pawn pawn, bool isFrag = false)
        {
            CompAmmoUser comammp = this.parent.TryGetComp<CompAmmoUser>();
         
            if (pawn.Faction != Faction.OfPlayer && comammp != null)
            {
                AmmoDef Adef = new AmmoDef { };
                ThingDef Prjdef = new ThingDef { };
                AmmoDef amder = comammp.Props.ammoSet.ammoTypes.First().ammo;
                ThingDef projder = comammp.Props.ammoSet.ammoTypes.First().projectile;
                Log.Message(projder.label);
                FieldInfo[] amogus = typeof(AmmoDef).GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

                foreach (FieldInfo info in amogus)
                {
                    info.SetValue(Adef, info.GetValue(amder));
                }
                FieldInfo[] amoguss = typeof(ThingDef).GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

                foreach (FieldInfo info in amoguss)
                {
                    info.SetValue(Prjdef, info.GetValue(projder));
                }
                Prjdef.projectile = new ProjectilePropertiesCE { };
                FieldInfo[] projectus = typeof(ProjectilePropertiesCE).GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

                ProjectilePropertiesCE amo = (ProjectilePropertiesCE)projder.projectile;
                float amoguys = amo.GetDamageAmount(1);
                foreach (FieldInfo info in projectus)
                {
                    // Log.Message(info.Name);

                    info.SetValue(Prjdef.projectile, info.GetValue(amo));
                }


                ///Armor pen calculation and shit
              
                List<ThingDef> powders = DefDatabase<ThingDef>.AllDefs.ToList().FindAll(tt => tt.comps.Any(k => k is PowderCompProps));
                Func<ThingDef, float> funcer = flt => checktechlvls((float)flt.techLevel, (float)pawn.Faction.def.techLevel);

                Func<RecipeShapeDef, float> shapefunc = flt => checktechlvls2((float)flt.techlevel, (float)pawn.Faction.def.techLevel);

                ThingDef powder = powders.RandomElementByWeight(funcer);
                var dar = charge(pawn);
                float powderpower = powder.statBases.Find(tt33 => tt33.stat == AmmoClassesDefOf.PowderPower).value;
                Func<ThingDef, float> funcermat = flt => chancemult(flt, pawn);
                List<ThingDef> liszt = DefDatabase<ThingDef>.AllDefs.ToList().FindAll(ptrd => ptrd.stuffProps?.categories.Contains(StuffCategoryDefOf.Metallic) ?? false);
                liszt.RemoveAll(tt33 => tt33.defName == "Silver" | tt33.defName == "Gold");
                ThingDef projmat = liszt.RandomElementByWeight(funcermat);
                Log.Message("projmat: " + projmat.label);
                float matmultpen = projmat.statBases.Find(tt33 => tt33.stat == StatDefOf.StuffPower_Armor_Sharp).value;
                float basepen = amo.armorPenetrationSharp;
                ProjectilePropertiesCE cemogus = (ProjectilePropertiesCE)Prjdef.projectile;
                RecipeShapeDef shapeDef = DefDatabase<RecipeShapeDef>.AllDefs.ToList().FindAll(tt33 => tt33.ismulti == false && tt33.isfrag == false).RandomElementByWeight(shapefunc);
                if (isFrag)
                {
                    shapeDef = shapdefof.frag;
                }
                cemogus.armorPenetrationSharp = basepen + (matmultpen * powderpower * dar) * shapeDef.penmult;
                ///naming
                Adef.label = pawn.Faction.ToString() + " hand loaded " + projmat.label + " " + shapeDef.label + " " + amder.AmmoSetDefs.First().label;
                Prjdef.label = pawn.Faction.ToString() + " hand loaded " + projmat.label + " " + shapeDef.label + " " + amder.AmmoSetDefs.First().label;
                Log.Message(Adef.label + " armor pen: " + cemogus.armorPenetrationSharp);
                // Adef.forceDebugSpawnable = true;
                Prjdef.modExtensions = new List<DefModExtension>();

                float calcul = (shapeDef.damagemult * amoguys * dar);
                Log.Message(calcul.ToString());
                Prjdef.modExtensions.Add(new BulletModExtension { FixedDamage = calcul });
                Adef.thingClass = typeof(AmmoThing);
                Prjdef.thingClass = typeof(HandLoading.Bullet);
                DefDatabase<ThingDef>.AllDefs.AddItem(Adef);

                comammp.Props.ammoSet.ammoTypes.Add(new AmmoLink { ammo = Adef, projectile = Prjdef });

                Adef.defName = "a" + Rand.Range(0, 25678) + "add" + Rand.Chance(0.50f).ToString() + "add" + Rand.Range(0, 25678) + Rand.Chance(0.50f).ToString();

                Prjdef.defName = "a" + Rand.Range(0, 25678) + "add" + Rand.Chance(0.50f).ToString() + "add" + Rand.Range(0, 25678) + Rand.Chance(0.50f).ToString();

                Adef.ammoClass = new AmmoCategoryDef { label = Adef.label, labelShort = Adef.label, description = calcul.ToString(), defName = Rand.Range(0, 25678) + "add" + Rand.Chance(0.50f).ToString() };

                Adef.generateCommonality = 0f;

                Adef.ammoClass.description = "actual damage is: " + Prjdef.GetModExtension<BulletModExtension>().FixedDamage.ToString();

                Adef.description = "Powder used: " + powder.label + " Charge amount " + dar.ToString() + " projectile design :" + shapeDef.label + " damage: " + calcul.ToString();

                if (Find.World.GetComponent<IhaveacoldandImustsneeze>().Factionammos != null)
                {

                    Find.World.GetComponent<IhaveacoldandImustsneeze>().Factionammos.Add(new KeyValuePair<string, AmmoDef>(key: pawn.Faction.ToString(), value: Adef));
                }
                else
                {

                    Find.World.GetComponent<IhaveacoldandImustsneeze>().Factionammos = new List<KeyValuePair<string, AmmoDef>>();
                    Find.World.GetComponent<IhaveacoldandImustsneeze>().Factionammos.Add(new KeyValuePair<string, AmmoDef>(key: pawn.Faction.ToString(), value: Adef));
                }
                if (isFrag)
                {
                    CompProperties_Fragments frags = new CompProperties_Fragments();
                    FieldInfo[] fragfields = typeof(CompProperties_Fragments).GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                    foreach (FieldInfo info in fragfields)
                    {
                        info.SetValue(frags, info.GetValue(Prjdef.comps.Find(tt33 => tt33 is CompProperties_Fragments)));
                    }
                    makefrags(projmat, powder);
                    if (Prjdef.comps.Any(tt33 => tt33 is CompProperties_Fragments))
                    {
                        Prjdef.comps.RemoveAll(tt32 => tt32 is CompProperties_Fragments);
                    }
                    Prjdef.comps.Add(frags);
                    foreach (ThingDefCountClass thingdef in frags.fragments)
                    {
                        thingdef.thingDef = fragc1;
                    }

                }
                var athink = (dar * Rand.Range(1, 3) * powder.statBases.Find(Poof => Poof.stat.defName == "PowderPower").value) / 10;
                Adef.comps.Add(new damagercproprs { dammpoints = athink });
                Find.World.GetComponent<SaverComp>().SaveAmmodef(projectile: Prjdef, ammo: Adef, ammoset: Adef.AmmoSetDefs.First(), ammoCategory: Adef.ammoClass);
            }
        }
        public AmmoDef fragc1;

        public void makefrags(ThingDef material, ThingDef powder)
        {
            fragc1 = new AmmoDef { };
            FieldInfo[] fields = typeof(AmmoDef).GetFields(
                       BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            foreach (FieldInfo dd in fields)
            {

                dd.SetValue(fragc1, dd.GetValue(AmmoClassesDefOf.Fragment_Large));
            }
            fragc1.projectile = new ProjectilePropertiesCE();
            FieldInfo[] fieldsproj = typeof(ProjectileProperties).GetFields(
                       BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            foreach (FieldInfo dd in fieldsproj)
            {
                Log.Message(dd.Name);
                dd.SetValue(fragc1.projectile, dd.GetValue(AmmoClassesDefOf.Fragment_Large.projectile));
            }
            float powderpower = powder.statBases.Find(oo => oo.stat.defName == "PowderPower").value;
            Log.Message(powderpower.ToString());
            ProjectilePropertiesCE atak = (ProjectilePropertiesCE)fragc1.projectile;
            atak.armorPenetrationSharp = 1 * material.statBases.Find(pp => pp.stat == StatDefOf.StuffPower_Armor_Sharp).value * powderpower * material.statBases.Find(pp => pp.stat == StatDefOf.Mass).value * Rand.Range(2f, 4.5f);
            atak.armorPenetrationBlunt = 20 * material.statBases.Find(pp => pp.stat == StatDefOf.Mass).value * powderpower;
            Log.Message(atak.armorPenetrationSharp.ToString() + "ap sharp");
            Log.Message(atak.armorPenetrationBlunt.ToString() + "ap blunt");
            fragc1.label = material + " fragments";
            fragc1.defName = "truten" + Rand.Range(0, 670).ToString() + "trzmiel" + Rand.Range(0, 670).ToString() + "bak";
           
            Find.World.GetComponent<SaverComp>().SaveAmmodef(projectile: fragc1, amifrag: true);
        }


    }
    public class AmmoExplodyBitsOrSomethingIdk : ThingComp
    {
        public float dampoints => Props.dammpoints;
        int test = 0;

        public override void PostPostMake()
        {
            base.PostPostMake();
            if (this.parent?.def?.tickerType != TickerType.Normal)
            {
                Log.Message("ammo had no ticker. correcting");
                this.parent.def.tickerType = TickerType.Normal;
                Log.Message("corrected");
            }
        }
        public override void CompTick()
        {
            if (test == 0)
            {

                test++;

            }

            if (this.ParentHolder is Pawn_InventoryTracker | this.ParentHolder is CompInventory)
            {
                Pawn_InventoryTracker inbentor = this.ParentHolder as Pawn_InventoryTracker;

                if (inbentor.innerContainer.Any(O => O.def == this.parent.def && O != this.parent))
                {
                    inbentor.innerContainer.ToList().Find(O => O.def == this.parent.def && O != this.parent).stackCount += this.parent.stackCount;

                    this.parent.Destroy();
                }
            }
            if (!(this.ParentHolder is Pawn_InventoryTracker) && !(this.ParentHolder is Map))
            {

                Log.Message(this.ParentHolder.ToString());
            }
            base.CompTick();
        }


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
    [DefOf]
    public class shapdefof : DefOf
    {
        public static RecipeShapeDef frag;
    }
}
