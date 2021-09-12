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
using CombatExtended;
using RimWorld.Planet;
using System.Xml;

namespace HandLoading
{
    public class othersomethingsets : ModSettings
    {


        public bool Open;

        public bool Silly;




        public override void ExposeData()
        {


            Scribe_Values.Look<bool>(ref Open, "Open");
            base.ExposeData();
        }
    }
    public class HandeLoading : Mod
    {
        public static othersomethingsets settings;


        public HandeLoading(ModContentPack content) : base(content)
        {


            settings = GetSettings<othersomethingsets>();


        }
        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);
            listingStandard.Gap(60);
            listingStandard.CheckboxLabeled("Create seconday mod folder used as container for hand loading saving" , ref settings.Open, "dew it");
            listingStandard.TextEntry("IF YOU CHOOSE TO DO IT REMEMBER TO ADD THE CONTAINER TO YOUR MODLIST");
            listingStandard.TextEntry("container is created after first ammo is created with the settings on");
            listingStandard.Gap(60);
            listingStandard.CheckboxLabeled("Allow all guns to be rebareled to any caliber", ref settings.Silly, "");
            listingStandard.Gap(120);
            if(Find.CurrentMap != null)
            {
                if (listingStandard.ButtonText("Remove all ammos"))
                {
                   
                   
                    Log.Message("2");
                    List<ThingDef> filestoyeet = new List<ThingDef>();
                    //Log.Error(ThingDefOf.Door.fileName);
                    //filestoyeet.Add(ThingDefOf.AncientBed);
                    string workingDirectory = Environment.CurrentDirectory;
                    string actualpathtomod = Directory.GetParent(workingDirectory).Parent.FullName + "/workshop/content/294100/2548930569";
                  
                   
                    if (Directory.Exists(Directory.GetParent(workingDirectory).Parent.FullName + "/common/Rimworld/Mods/Hand-Loading"))
                    {

                        actualpathtomod = Directory.GetParent(workingDirectory).Parent.FullName + "/common/Rimworld/Mods/Hand-Loading";
                    }
                    if (Directory.Exists(Directory.GetParent(workingDirectory).Parent.FullName + "/common/Rimworld/Mods/2548930569"))
                    {


                        actualpathtomod = Directory.GetParent(workingDirectory).Parent.FullName + "/common/Rimworld/Mods/2548930569";
                    }
                    if (Directory.Exists(Directory.GetParent(workingDirectory).Parent.FullName + "/workshop/content/294100/2548930569"))
                    {

                        actualpathtomod = Directory.GetParent(workingDirectory).Parent.FullName + "/workshop/content/294100/2548930569";
                    }
                    if (Directory.Exists(Directory.GetParent(workingDirectory).Parent.FullName + "/common/Rimworld/Mods/HLContainer"))
                    {

                        actualpathtomod = Directory.GetParent(workingDirectory).Parent.FullName + "/common/Rimworld/Mods/HLContainer";
                    }
                    string[] stuff = Directory.GetDirectories(actualpathtomod + "/");
                    foreach(string s in stuff)
                    {
                        string[] vs = Directory.GetDirectories(s + "/");
                        vs.ToList().RemoveAll(OO => OO.Contains("Textures"));
                        foreach(string fathouse in vs)
                        {
                            //Log.Message(fathouse);
                            string[] again = Directory.GetFiles(fathouse + "/");
                            foreach(string cos in again)
                            {
                                Log.Message(cos);
                                File.Delete(cos);
                            }
                        }
                        //Log.Message(s);
                    }
                    Log.Message(stuff.ToString());
                    Log.Message("3");
                    List<Thing> tings = Find.CurrentMap.listerThings.AllThings.FindAll(OL => OL.def.Verbs.Count > 0).ToList();
                    Log.Message("11");
                    foreach (Pawn pwan in Find.CurrentMap.mapPawns.FreeColonists)
                    {
                        if ((pwan?.equipment?.Primary ?? null) != null)
                        {
                            if (pwan.equipment.Primary.TryGetComp<CompAmmoUser>() != null)
                            {
                                tings.Add(pwan.equipment.Primary);
                            }
                        }
                    }
                    Log.Message("1");
                    if (tings.Count > 0)
                    {
                        foreach (ThingWithComps thingWith in tings)
                        {
                            if (thingWith.TryGetComp<CompAmmoUser>() == null)
                            {
                                return;
                            }
                            else
                            {
                                CompAmmoUser amuser = thingWith.TryGetComp<CompAmmoUser>();
                                if (amuser.CurrentAmmo.modContentPack == Verse.LoadedModManager.RunningMods.ToList().Find(L => L.Name == "Hand loads [Beta]"))
                                {
                                    CompAmmoUser amser = new CompAmmoUser { props = thingWith.def.comps.Find(P => P is CompProperties_AmmoUser) };

                                    thingWith.AllComps.Remove(amuser);
                                    thingWith.AllComps.Add(amser);
                                }
                            }
                        }
                    }
                }
            }
           
            listingStandard.End();
            base.DoSettingsWindowContents(inRect);
        }
        public override string SettingsCategory()
        {
            return "Hand loading";
        }
    }
    public class SaverComp : WorldComponent
    {
        public bool myExampleBool = true;

       

        public SaverComp(World world) : base(world)
        {
        }
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<int>(ref abc, "abc");
            Log.Message(abc.ToString());

        }
        public override void FinalizeInit()
        {

            foreach (RecipeDef recdef in DefDatabase<RecipeDef>.AllDefs.ToList().FindAll(o => o.description == "Make custom ammo"))
            {
                //Log.Message(recdef.ToString());
                recdef.recipeUsers.Add(CE_ThingDefOf.AmmoBench);
            }
          
            base.FinalizeInit();


        }
        public othersomethingsets sets;
        public void SaveAmmodef(AmmoDef ammo, AmmoCategoryDef ammoCategory, ThingDef projectile, AmmoSetDef ammoset, RecipeDef recpe, float someamount, ThingDef damImtired, float re)
        {
            sets = HandeLoading.settings;
            abc++;




          
            string workingDirectory = Environment.CurrentDirectory;
            Log.Error(Directory.GetParent(workingDirectory).Parent.FullName);
            if (sets.Open && !(Directory.Exists(Directory.GetParent(workingDirectory).Parent.FullName + "/common/Rimworld/Mods/HLContainer")))
            {
                Log.Message("Creating container directories");
                Directory.CreateDirectory(Directory.GetParent(workingDirectory).Parent.FullName + "/common/Rimworld/Mods/HLContainer");
                Directory.CreateDirectory(Directory.GetParent(workingDirectory).Parent.FullName + "/common/Rimworld/Mods/HLContainer/Defs");
                Directory.CreateDirectory(Directory.GetParent(workingDirectory).Parent.FullName + "/common/Rimworld/Mods/HLContainer/Defs/ammos");
                Directory.CreateDirectory(Directory.GetParent(workingDirectory).Parent.FullName + "/common/Rimworld/Mods/HLContainer/Defs/bullets");
                Directory.CreateDirectory(Directory.GetParent(workingDirectory).Parent.FullName + "/common/Rimworld/Mods/HLContainer/Defs/categories");
                Directory.CreateDirectory(Directory.GetParent(workingDirectory).Parent.FullName + "/common/Rimworld/Mods/HLContainer/Defs/recpe");
                Directory.CreateDirectory(Directory.GetParent(workingDirectory).Parent.FullName + "/common/Rimworld/Mods/HLContainer/Patches");
                Directory.CreateDirectory(Directory.GetParent(workingDirectory).Parent.FullName + "/common/Rimworld/Mods/HLContainer/Patches/codemade");
                Directory.CreateDirectory(Directory.GetParent(workingDirectory).Parent.FullName + "/common/Rimworld/Mods/HLContainer/About");
                XmlDocument docsaver = new XmlDocument();
                docsaver.LoadXml("<ModMetaData><name>Hand Loads Saved Ammo Data Container</name><author>Caulaflower</author><packageId>Caulaflower.HLContainer.CustomAmmoContainer</packageId><supportedVersions><li>1.2</li><li>1.3</li></supportedVersions><url>Link</url><description>Container with your created ammo.</description></ModMetaData>"); //Your string here

                // Save the document to a file and auto-indent the output.
                XmlTextWriter writersaver = new XmlTextWriter(Directory.GetParent(workingDirectory).Parent.FullName + "/common/Rimworld/Mods/HLContainer/About/" + "About" + ".xml", null);
                abc++;
                writersaver.Formatting = Formatting.Indented;
                docsaver.Save(writersaver);
            }
            string actualpathtomod = Directory.GetParent(workingDirectory).Parent.FullName + "/workshop/content/294100/2548930569/Defs";
          
            if (Directory.Exists(Directory.GetParent(workingDirectory).Parent.FullName + "/common/Rimworld/Mods/Hand-Loading"))
            {
                Log.Message("saved ammo in github version files");
                actualpathtomod = Directory.GetParent(workingDirectory).Parent.FullName + "/common/Rimworld/Mods/Hand-Loading/Defs";
            }
            if (Directory.Exists(Directory.GetParent(workingDirectory).Parent.FullName + "/common/Rimworld/Mods/2548930569"))
            {
                Log.Message("saved ammo in rimpy version files");

                actualpathtomod = Directory.GetParent(workingDirectory).Parent.FullName + "/common/Rimworld/Mods/2548930569/Defs";
            }
            if (Directory.Exists(Directory.GetParent(workingDirectory).Parent.FullName + "/workshop/content/294100/2548930569/Defs"))
            {
                Log.Message("saved ammo in steam version files");
                actualpathtomod = Directory.GetParent(workingDirectory).Parent.FullName + "/workshop/content/294100/2548930569/Defs";
            }
            if (Directory.Exists(Directory.GetParent(workingDirectory).Parent.FullName + "/common/Rimworld/Mods/HLContainer"))
            {
                Log.Message("saved ammo in container files. The messages are fucked, this is definitely the actual one");
                actualpathtomod = Directory.GetParent(workingDirectory).Parent.FullName + "/common/Rimworld/Mods/HLContainer/Defs";
            }







            XmlDocument doc2 = new XmlDocument();
            doc2.LoadXml(
                "<Defs><CombatExtended.AmmoCategoryDef><defName>" + ammoCategory.defName + "</defName><label>" + ammoCategory.label + "</label><labelShort>" + ammoCategory.label+ "</labelShort><description>" + ammo.description + "</description></CombatExtended.AmmoCategoryDef></Defs>"); //Your string here

            // Save the document to a file and auto-indent the output.
            XmlTextWriter writer2 = new XmlTextWriter(actualpathtomod + "/categories/category" + abc.ToString() + ".xml", null);
            abc++;
            writer2.Formatting = Formatting.Indented;
            doc2.Save(writer2);

            // Create the XmlDocument.
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(
                "<Defs><ThingDef Class='CombatExtended.AmmoDef'><defName>"+ ammo.defName + "</defName>"
                + "<thingClass>" + ammo.thingClass + "</thingClass>"
                + "<label>" + ammo.label + "</label>"
                + "<thingCategories><li>HandsLoadedAmmo</li></thingCategories>"
                + "<ammoClass>" + ammoCategory.defName + "</ammoClass>"
                + "<comps><li Class='HandLoading.damagercproprs'><dammpoints>" + re.ToString()  + "</dammpoints></li><li><compClass>HandLoading.HaulComp</compClass></li></comps>"
                + "<graphicData>" + "<texPath>" + ammo.graphic.path +"</texPath>"
                + "<graphicClass>Graphic_StackCount</graphicClass>" + "</graphicData>"
                + "<selectable>true</selectable><alwaysHaulable>true</alwaysHaulable><stackLimit>50000</stackLimit>" +
                "<drawGUIOverlay>true</drawGUIOverlay><statBases>" + "<Bulk>" + ammo.statBases.Find(a => a.stat == CE_StatDefOf.Bulk).value.ToString() + "</Bulk>" + "<Mass>" + ammo.statBases.Find(a => a.stat == StatDefOf.Mass).value.ToString() + "</Mass>" + "</statBases></ThingDef></Defs>"); //Your string here

            // Save the document to a file and auto-indent the output.
            XmlTextWriter writer = new XmlTextWriter(actualpathtomod + "/ammos/ammo" + abc.ToString() +".xml", null);
            doc.Save(writer);
         

            ProjectilePropertiesCE projpropsCE = projectile.projectile as ProjectilePropertiesCE;
            XmlDocument doc3 = new XmlDocument();
            if(projpropsCE.secondaryDamage.Count >= 1)
            {
                doc3.LoadXml("<Defs><ThingDef><defName>" + projectile.defName + "</defName>" + "<tickerType>Normal</tickerType>" + "<graphicData>" + "<texPath>" + projectile.graphicData.texPath + "</texPath>" + "<graphicClass>Graphic_Single</graphicClass>" + "</graphicData>" + "<thingClass>CombatExtended.BulletCE</thingClass>" + "<label>" + projectile.label + "</label>" + "<projectile Class='CombatExtended.ProjectilePropertiesCE'>" + "<damageDef>Bullet</damageDef>" + "<speed>" + projpropsCE.speed.ToString() + "</speed>" + "<dropsCasings>true</dropsCasings>" + "<damageAmountBase>" + projectile.GetModExtension<BulletModExtension>().FixedDamage.ToString() + "</damageAmountBase>" + "<armorPenetrationSharp>" + projpropsCE.armorPenetrationSharp.ToString() + "</armorPenetrationSharp>" + "<armorPenetrationBlunt>" + projpropsCE.armorPenetrationBlunt.ToString() + "</armorPenetrationBlunt>" + "<secondaryDamage><li><def>" + projpropsCE.secondaryDamage.First().def + "</def><amount>" + projpropsCE.secondaryDamage.First().amount + "</amount></li></secondaryDamage></projectile>" + "</ThingDef></Defs>"); //Your string here
            }
            else
            {
                if(projectile.comps.Any(K => K is CompProperties_Fragments))
                {
                    Log.Error("test test test");
                    CompProperties_Fragments kurwa = projectile.comps.Find(G => G is CompProperties_Fragments) as CompProperties_Fragments;
                    if (kurwa.fragments.Any(c => c.thingDef == AmmoClassesDefOf.Fragment_Large))
                    {
                        Log.Message("test");
                        doc3.LoadXml("<Defs><ThingDef><defName>" + projectile.defName + "</defName>" + "<tickerType>Normal</tickerType>" + "<graphicData>" + "<texPath>" + projectile.graphicData.texPath + "</texPath>" + "<graphicClass>Graphic_Single</graphicClass>" + "</graphicData>" + "<thingClass>CombatExtended.BulletCE</thingClass>" + "<label>" + projectile.label + "</label>" + "<projectile Class='CombatExtended.ProjectilePropertiesCE'>" + "<damageDef>Bullet</damageDef>" + "<speed>" + projpropsCE.speed.ToString() + "</speed>" + "<dropsCasings>true</dropsCasings>" + "<damageAmountBase>" + projectile.GetModExtension<BulletModExtension>().FixedDamage.ToString() + "</damageAmountBase>" + "<armorPenetrationSharp>" + projpropsCE.armorPenetrationSharp.ToString() + "</armorPenetrationSharp>" + "<armorPenetrationBlunt>" + projpropsCE.armorPenetrationBlunt.ToString() + "</armorPenetrationBlunt>" + "</projectile>" + "<comps><li Class='CombatExtended.CompProperties_Fragments'><fragments>" +
                            "<Fragment_Large>" + kurwa.fragments.Find(F => F.thingDef == AmmoClassesDefOf.Fragment_Large).count.ToString() + "</Fragment_Large><Fragment_Small>" + kurwa.fragments.Find(H => H.thingDef == AmmoClassesDefOf.Fragment_Small).count + "</Fragment_Small></fragments></li></comps>" + "</ThingDef></Defs>"); //Your string here
                    }
                    else
                    {
                        doc3.LoadXml("<Defs><ThingDef><defName>" + projectile.defName + "</defName>" + "<tickerType>Normal</tickerType>" + "<graphicData>" + "<texPath>" + "Things/Ammo/Cannon/HE" + "</texPath>" + "<graphicClass>Graphic_Single</graphicClass>" + "</graphicData>" + "<thingClass>CombatExtended.BulletCE</thingClass>" + "<label>" + projectile.label + "</label>" + "<projectile Class='CombatExtended.ProjectilePropertiesCE'>" + "<damageDef>Bullet</damageDef>" + "<speed>" + projpropsCE.speed.ToString() + "</speed>" + "<dropsCasings>true</dropsCasings>" + "<damageAmountBase>" + projectile.GetModExtension<BulletModExtension>().FixedDamage.ToString() + "</damageAmountBase>" + "<armorPenetrationSharp>" + projpropsCE.armorPenetrationSharp.ToString() + "</armorPenetrationSharp>" + "<armorPenetrationBlunt>" + projpropsCE.armorPenetrationBlunt.ToString() + "</armorPenetrationBlunt>" + "</projectile>" + "<comps><li Class='CombatExtended.CompProperties_Fragments'><fragments><Fragment_Small>" + kurwa.fragments.Find(H => H.thingDef == AmmoClassesDefOf.Fragment_Small).count + "</Fragment_Small></fragments></li></comps>" + "</ThingDef></Defs>"); //Your string here
                    }
                   
                }
                else
                {
                    if(projpropsCE.pelletCount >= 2)
                    {
                        doc3.LoadXml("<Defs><ThingDef><defName>" + projectile.defName + "</defName>" + "<tickerType>Normal</tickerType>" + "<graphicData>" + "<texPath>" + "Things/Ammo/Cannon/HE" + "</texPath>" + "<graphicClass>Graphic_Single</graphicClass>" + "</graphicData>" + "<thingClass>CombatExtended.BulletCE</thingClass>" + "<label>" + projectile.label + "</label>" + "<projectile Class='CombatExtended.ProjectilePropertiesCE'><spreadMult>" + projpropsCE.spreadMult.ToString() + "</spreadMult>" + "<damageDef>Bullet</damageDef>" + "<speed>" + projpropsCE.speed.ToString() + "</speed>" + "<dropsCasings>true</dropsCasings>" + "<damageAmountBase>" + projectile.GetModExtension<BulletModExtension>().FixedDamage.ToString() + "</damageAmountBase>" + "<armorPenetrationSharp>" + projpropsCE.armorPenetrationSharp.ToString() + "</armorPenetrationSharp><pelletCount>" + projpropsCE.pelletCount.ToString() + "</pelletCount><armorPenetrationBlunt>" + projpropsCE.armorPenetrationBlunt.ToString() + "</armorPenetrationBlunt>" + "</projectile>" + "</ThingDef></Defs>"); //Your string here
                    }
                    else
                    {
                        doc3.LoadXml("<Defs><ThingDef><defName>" + projectile.defName + "</defName>" + "<tickerType>Normal</tickerType>" + "<graphicData>" + "<texPath>" + projectile.graphicData.texPath + "</texPath>" + "<graphicClass>Graphic_Single</graphicClass>" + "</graphicData>" + "<thingClass>CombatExtended.BulletCE</thingClass>" + "<label>" + projectile.label + "</label>" + "<projectile Class='CombatExtended.ProjectilePropertiesCE'>" + "<damageDef>Bullet</damageDef>" + "<speed>" + projpropsCE.speed.ToString() + "</speed>" + "<dropsCasings>true</dropsCasings>" + "<damageAmountBase>" + projectile.GetModExtension<BulletModExtension>().FixedDamage.ToString() + "</damageAmountBase>" + "<armorPenetrationSharp>" + projpropsCE.armorPenetrationSharp.ToString() + "</armorPenetrationSharp>" + "<armorPenetrationBlunt>" + projpropsCE.armorPenetrationBlunt.ToString() + "</armorPenetrationBlunt>" + "</projectile>" + "</ThingDef></Defs>"); //Your string here
                    }
                    
                }
               
            }

            // Save the document to a file and auto-indent the output.
            XmlTextWriter writer3 = new XmlTextWriter(actualpathtomod + "/bullets/projectile" + abc.ToString() + ".xml", null);
            
            writer3.Formatting = Formatting.Indented;
            doc3.Save(writer3);












            XmlDocument doc4 = new XmlDocument();
            doc4.LoadXml("<Patch><Operation Class='PatchOperationAdd'><xpath>/Defs/CombatExtended.AmmoSetDef[defName='" + ammoset.defName + "']/ammoTypes" + "</xpath><value>" + "<" + ammo.defName + ">" + projectile.defName + "</" + ammo.defName + ">" + "</value></Operation></Patch>"); //Your string here

            // Save the document to a file and auto-indent the output.
            XmlTextWriter writer4 = new XmlTextWriter(actualpathtomod + "/../Patches/codemade/patchnumber" + Rand.Range(0, 56709723).ToString() + ".xml", null);

            writer4.Formatting = Formatting.Indented;
            doc4.Save(writer4);


            XmlDocument doc5 = new XmlDocument();
            if (projpropsCE.secondaryDamage.Count >= 1)
            {
                 doc5.LoadXml("<Defs><RecipeDef ParentName=" + '"' + "AmmoRecipeBase" + '"' + "><defName>" + recpe.defName + "</defName>" + "<label>" + "Make " + ammo.label + "(" + recpe.products.First().count.ToString() + ")" + "</label>" + "<description>Make custom ammo</description>" + "<jobString>Making custom ammunition</jobString>" + "<ingredients><li><filter><thingDefs><li>" + recpe.ingredients.Find(a => a.filter.Allows(CE_ThingDefOf.FSX) | a.filter.Allows(AmmoClassesDefOf.Prometheum)).filter.AnyAllowedDef.defName + "</li>" + "</thingDefs>" + "</filter><count>" + "2" + "</count>" + "</li><li><filter><thingDefs><li>" + recpe.ingredients.First().filter.AnyAllowedDef.defName + "</li>" + "</thingDefs>" + "</filter><count>" + recpe.ingredients.First().GetBaseCount() + "</count>" + "</li>" + "<li><filter>" + "<thingDefs><li>" + recpe.ingredients.Find(a => ThingMaker.MakeThing(a.filter.AnyAllowedDef).TryGetComp<PowderComp>() != null).filter.AnyAllowedDef.defName + "</li></thingDefs></filter><count>" + someamount + "</count></li>" + "<li><filter><thingDefs><li>" + recpe.ingredients.FindAll(a => ThingMaker.MakeThing(a.filter.AnyAllowedDef).TryGetComp<PowderComp>() == null && !a.filter.Allows(CE_ThingDefOf.FSX) && !a.filter.Allows(AmmoClassesDefOf.Prometheum)).Last().filter.AnyAllowedDef.defName + "</li></thingDefs></filter><count>" + recpe.ingredients.First().GetBaseCount() + "</count></li>" + "</ingredients>" + "<fixedIngredientFilter><thingDefs><li>" + recpe.ingredients.FindAll(a => ThingMaker.MakeThing(a.filter.AnyAllowedDef).TryGetComp<PowderComp>() == null && !a.filter.Allows(CE_ThingDefOf.FSX) && !a.filter.Allows(AmmoClassesDefOf.Prometheum)).Last().filter.AnyAllowedDef.defName + "</li></thingDefs></fixedIngredientFilter><products>" + "<" + ammo.defName + ">" + recpe.products.First().count.ToString() + "</" + ammo.defName + ">" + "</products><workAmount>" + recpe.workAmount + "</workAmount></RecipeDef></Defs>"); //Your string here
            }
            else
            {
                doc5.LoadXml("<Defs><RecipeDef ParentName=" + '"' + "AmmoRecipeBase" + '"' + "><defName>" + recpe.defName + "</defName>" + "<label>" + "Make " + ammo.label + "(" + recpe.products.First().count.ToString() + ")" + "</label>" + "<description>Make custom ammo</description>" + "<jobString>Making custom ammunition</jobString>" + "<ingredients><li><filter><thingDefs><li>" + recpe.ingredients.First().filter.AnyAllowedDef.defName + "</li>" + "</thingDefs>" + "</filter><count>" + recpe.ingredients.First().GetBaseCount() + "</count>" + "</li>" + "<li><filter>" + "<thingDefs><li>" + recpe.ingredients.Find(a => ThingMaker.MakeThing(a.filter.AnyAllowedDef).TryGetComp<PowderComp>() != null).filter.AnyAllowedDef.defName + "</li></thingDefs></filter><count>" + someamount +"</count></li>" + "<li><filter><thingDefs><li>" + recpe.ingredients.FindAll(a => ThingMaker.MakeThing(a.filter.AnyAllowedDef).TryGetComp<PowderComp>() == null && !a.filter.Allows(CE_ThingDefOf.FSX) && !a.filter.Allows(AmmoClassesDefOf.Prometheum)).Last().filter.AnyAllowedDef.defName + "</li></thingDefs></filter><count>" + recpe.ingredients.First().GetBaseCount() + "</count></li>" + "</ingredients>" + "<fixedIngredientFilter><thingDefs><li>" + recpe.ingredients.FindAll(a => ThingMaker.MakeThing(a.filter.AnyAllowedDef).TryGetComp<PowderComp>() == null && !a.filter.Allows(CE_ThingDefOf.FSX) && !a.filter.Allows(AmmoClassesDefOf.Prometheum)).Last().filter.AnyAllowedDef.defName + "</li></thingDefs></fixedIngredientFilter><products>" + "<" + ammo.defName + ">" + recpe.products.First().count.ToString() + "</" + ammo.defName + ">" + "</products><workAmount>" + recpe.workAmount + "</workAmount></RecipeDef></Defs>"); //Your string here
            }
               

            // Save the document to a file and auto-indent the output.
            XmlTextWriter writer5 = new XmlTextWriter(actualpathtomod + "/recpe/recipe" + Rand.Range(0, 56709723).ToString() + ".xml", null);

            writer5.Formatting = Formatting.Indented;
            doc5.Save(writer5);
          

        }
        public void TransferDef(AmmoSetDef ammoSet)
        {
            
        }

       
        public int abc;
        public string ammolabel;
        public Type ammotype;
        public string defnameofammo;
        public IEnumerable<AmmoDef> ammodefmade;
    }
}
