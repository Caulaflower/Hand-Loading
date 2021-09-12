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
	[DefOf]
	public static class AmmoClassesDefOf
	{
		public static AmmoCategoryDef MusketBall;

		public static ThingDef Prometheum;

		public static ResearchProjectDef CE_AdvancedAmmo;

		public static ThingDef IndustrialPowder2b;

		public static AmmoDef Ammo_556x45mmNATO_FMJ;

		public static ThingDef Bullet_12Gauge_Buck;

		public static ThingDef Fragment_Small;

		public static ThingDef Fragment_Large;

		public static ThingDef testshit;

		public static RecipeDef MakeAmmo_556x45mmNATO_FMJ;

		public static AmmoCategoryDef FullMetalJacket;

		public static ThingDef UnfinishedAmmo;

		public static ThingDef IndustrialPowder;

		static AmmoClassesDefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(AmmoClassesDefOf));
		}
	}

	public class HandLoadingWindow : Window
	{
		public void CalPel()
		{
			if (SelectedProjectile != null && ProjectileShape == "Buckshot")
			{
				string test = SelectedProjectile.defName.Substring(7, 2);

				if (PelCount == 0)
				{


				}
				PelCount = (int)(Math.Round(Convert.ToInt32(test) / 18.5) * 21);
				pelcal = 6.1f;
				Log.Message(test);
			}
			if (SelectedProjectile != null | ProjectileShape == "Flechette")
			{
				string test = SelectedProjectile.defName.Substring(7, 2);

				if (PelCount == 0)
				{


				}
				PelCount = (int)(Convert.ToInt32(test) / 5);
				pelcal = 10f;
				Log.Message(test);
			}
		}
		private static readonly Vector2 Test = new Vector2(100f, 140f);

		private int pint = 0;

		public int PelCount = 0;

		public int bigshrabint;

		public AmmoSetDef ammoclass_selected;

		public List<AmmoSetDef> calibers;

		public AmmoDef ammodef_selected;

		public CompProperties_Fragments kurwakurwakurwa;

		public string AmmoDefLabel;

		public float ArmorPenBluntCalculated;

		public float ArmorPenSharpCalculated;

		public float CalculatedSpeed;

		public float pelcal;

		public ThingDef casematerial;

		public float hardness_material_multiplier;

		public AmmoDef MadeAmmoDef;

		public ThingDef MadeProjectile;

		public ThingDef projectile_material;

		public string ProjectileShape;

		public ThingDef SelectedProjectile;

		public Texture sometextureidk;

		public List<ThingDef> ThingDefsToAddToDatabase;

		public float ChargeAmount = 1f;
		public override Vector2 InitialSize
		{
			get
			{
				return new Vector2(900f, 610f);
			}
		}

		public HandLoadingWindow(IntVec3 pos, Map mpa)
		{
			mpaa = mpa;
			postition = pos;
		}
		public IntVec3 postition;
		public Map mpaa;
		public void CalculateAP()
		{
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
			ProjectilePropertiesCE propsCE = SelectedProjectile?.projectile as ProjectilePropertiesCE;
			float Penmult2 = new float { };
			Penmult2 = propelant.statBases.Find(abc => abc.stat.defName == "PowderPower").value;
			if (Penmult2 == 0f)
			{
				Log.Error("propelant most likely has no PowderPower statbase");
			}
			
		
			if (ProjectileShape == "Buckshot")
			{
				ArmorPenSharpCalculated = (float)(2.25 * (hardness_material_multiplier) * (Penmult2) * this.ChargeAmount * (pelcal / 10));
			}
			else if (ProjectileShape == "Flechette")
			{
				ArmorPenSharpCalculated = (float)(3.10 * (hardness_material_multiplier) * (Penmult2) * this.ChargeAmount * (pelcal / 10));
			}
			else
			{
				ArmorPenSharpCalculated = (float)((propsCE?.armorPenetrationSharp ?? 1) + (ShapeDoubleAP * hardness_material_multiplier) * Penmult2 * this.ChargeAmount);
			}
			Log.Message(ArmorPenSharpCalculated.ToString());

		}

		public void CalculateAPBlunt()
		{
			if(ProjectileShape == "Buckshot")
			{
				ArmorPenBluntCalculated = ArmorPenSharpCalculated * 8f;
			}
			else
			{
				ArmorPenBluntCalculated = ArmorPenSharpCalculated * 3f;
			}
			
			Log.Message(ArmorPenBluntCalculated.ToString());
		}
		public ThingDef propelant;

		public string fillant;

		public int smolshrabcunt;

		public bool isexplosiveprojectile;

		public ThingDef fillantdef;
		public override void DoWindowContents(Rect inRect)
		{
			Rect rect1 = new Rect(inRect);
			rect1.width = HandLoadingWindow.Test.x + 16f;
			rect1.height = HandLoadingWindow.Test.y + 16f;
			rect1 = rect1.CenteredOnXIn(inRect);
			rect1 = rect1.CenteredOnYIn(inRect);
			rect1.x += 16f;
			rect1.y += 16f;
			Rect position = new Rect(rect1.xMin + (rect1.width - HandLoadingWindow.Test.x) / 2f - 10f, rect1.yMin + 20f, HandLoadingWindow.Test.x, HandLoadingWindow.Test.y);

			if (pint != 1)
			{
				ammodef_selected = AmmoClassesDefOf.Ammo_556x45mmNATO_FMJ;

				++pint;
			}

			Rect rect3 = new Rect(inRect);
			rect3.width = 20f;
			rect3.height = 20f;
			rect3 = rect3.CenteredOnXIn(inRect);
			rect3 = rect3.CenteredOnYIn(inRect);
			rect3.x += 16f;
			rect3.y += 16f;
			Rect rect4 = new Rect(inRect);
			rect4.width = 100f;
			rect4.height = 100f;
			rect4 = rect4.CenteredOnXIn(inRect);
			rect4 = rect4.CenteredOnYIn(inRect);
			rect4.x += -320f;
			rect4.y += 200f;

			if (Widgets.ButtonText(rect4, "choose caliber"))
			{
				var options = new List<FloatMenuOption>
				{
				};
				//foreach (AmmoDef)
				List<AmmoDef> shlist = new List<AmmoDef>();
				if (!isexplosiveprojectile)
				{
					shlist = DefDatabase<AmmoDef>.AllDefs.ToList().FindAll(deffer => deffer.Users.Count > 0 && deffer.ammoClass == AmmoClassesDefOf.FullMetalJacket | deffer.ammoClass == AmmoClassesDefOf.MusketBall | deffer.ammoClass.defName == "Slug");
				}
				else
				{
					shlist = DefDatabase<AmmoDef>.AllDefs.ToList().FindAll(deffer => deffer.Users.Count > 0 && deffer.ammoClass.defName == "GrenadeHE");
					shlist.Add(DefDatabase<AmmoDef>.AllDefs.ToList().Find(gaugeshell => gaugeshell.defName == "Ammo_12Gauge_Buck"));
					// && kurwakurwakurwa.fragments.Find(ghjb => ghjb.thingDef == AmmoClassesDefOf.Fragment_Large) != null

					//shlist.Add(DefDatabase<AmmoDef>.AllDefs.ToList().Find(LKHKF => LKHKF.defName == "Ammo_30x29mmGrenade_HE"));
				}


				foreach (AmmoDef ammodef in shlist)
				{
					FloatMenuOption floatmenuoption = new FloatMenuOption(ammodef.label, delegate
					{
						this.ammodef_selected = ammodef;
						this.ammoclass_selected = ammodef.AmmoSetDefs.Find(dod => dod.ammoTypes != null);
						this.SelectedProjectile = ammoclass_selected?.ammoTypes?.Find(A => A.ammo == ammodef_selected).projectile;
						calibers = ammodef.AmmoSetDefs;
						Log.Message($"Chosen ammo def: {SelectedProjectile}, {ammodef_selected}.");
						sometextureidk = ammodef_selected.uiIcon;
						CalPel();
					});

					options.Add(floatmenuoption);
				}
				Find.WindowStack.Add(new FloatMenu(options));
			}
			else
			{
			}
			Rect rect5 = new Rect(inRect);
			rect5.width = 100f;
			rect5.height = 100f;
			rect5 = rect5.CenteredOnXIn(inRect);
			rect5 = rect5.CenteredOnYIn(inRect);
			rect5.x += -320f;
			if(MadeAmmoDef != null)
			{
				sometextureidk = MadeAmmoDef.uiIcon;
			}
			rect5.y += -100f;
			if (Widgets.ButtonText(rect5, "Choose Bullet's projectile material"))
			{
				var options2 = new List<FloatMenuOption>
				{
				};

				List<ThingDef> somelist = DefDatabase<ThingDef>.AllDefs.ToList().Where<ThingDef>(L => L.stuffProps?.categories?.Contains(StuffCategoryDefOf.Metallic) is true).ToList();

				foreach (ThingDef szteel in somelist)
				{
					FloatMenuOption floatmenuoption = new FloatMenuOption(szteel.label, delegate
					{
						projectile_material = szteel;
						hardness_material_multiplier = projectile_material.stuffProps.statFactors.Find(A => A.stat == StatDefOf.MaxHitPoints).value;
					});

					options2.Add(floatmenuoption);
				}
				Find.WindowStack.Add(new FloatMenu(options2));
			}
			Rect rectCasing = new Rect(inRect);
			rectCasing.width = 100f;
			rectCasing.height = 100f;
			rectCasing = rectCasing.CenteredOnXIn(inRect);
			rectCasing = rectCasing.CenteredOnYIn(inRect);
			rectCasing.x += -320f;
			rectCasing.y += 100f;
			if (Widgets.ButtonText(rectCasing, "Choose Bullet's case material"))
			{
				var options2 = new List<FloatMenuOption>
				{
				};

				List<ThingDef> somelist = DefDatabase<ThingDef>.AllDefs.ToList().Where(L => L.stuffProps?.categories?.Contains(StuffCategoryDefOf.Metallic) == true).ToList();
				somelist.Add(ThingDefOf.Chemfuel);

				foreach (ThingDef szteel in somelist)
				{
					FloatMenuOption floatmenuoption = new FloatMenuOption(szteel.label, delegate
					{
						casematerial = szteel;
					});

					options2.Add(floatmenuoption);
				}
				Find.WindowStack.Add(new FloatMenu(options2));
			}
			Rect rectPowder = new Rect(inRect);
			rectPowder.width = 100f;
			rectPowder.height = 100f;
			rectPowder = rectPowder.CenteredOnXIn(inRect);
			rectPowder = rectPowder.CenteredOnYIn(inRect);
			rectPowder.x += -320f;
			rectPowder.y += -0f;

			

			if (Widgets.ButtonText(rectPowder, "Choose propellant"))
			{
				var options3 = new List<FloatMenuOption>
				{
				};
				StatDef state = DefDatabase<StatDef>.AllDefs.ToList().Find(loop => loop.defName == "PowderPower");
				Log.Message(state?.defName ?? "co kurwa");
				List<ThingDef> somelister = DefDatabase<ThingDef>.AllDefs.ToList().FindAll(l => l.comps.Any(L => L is PowderCompProps));
				if(somelister?.Count == 0)
				{
					somelister = DefDatabase<ThingDef>.AllDefs.ToList().FindAll(l => l.statBases.Any(koof => koof.stat == state));
				}
				

				foreach (ThingDef szteel in somelister)
				{
					FloatMenuOption floatmenuoption = new FloatMenuOption(szteel.label, delegate
					{
						propelant = szteel;
						
					});

					options3.Add(floatmenuoption);
				}
				Find.WindowStack.Add(new FloatMenu(options3));
			}
			Rect newshit = new Rect(inRect);
			newshit.width = 80f;
			newshit.height = 80f;
			newshit = newshit.CenteredOnXIn(inRect);
			newshit = newshit.CenteredOnYIn(inRect);
			newshit.x +=345f;
			newshit.y += -240f;
			Widgets.CheckboxLabeled(newshit, "hand load grenades and shells", ref this.isexplosiveprojectile);


			Rect rect69420 = new Rect(inRect);
			rect69420.width = 100f;
			rect69420.height = 100f;
			rect69420 = rect69420.CenteredOnXIn(inRect);
			rect69420 = rect69420.CenteredOnYIn(inRect);
			rect69420.x += -340f;
			rect69420.y += 240f;
			Rect rectFillant = new Rect(inRect);
			rectFillant.width = 100f;
			rectFillant.height = 40f;
			rectFillant = rectFillant.CenteredOnXIn(inRect);
			rectFillant = rectFillant.CenteredOnYIn(inRect);
			rectFillant.x += 340f;
			rectFillant.y += 115f;

			if (AmmoClassesDefOf.CE_AdvancedAmmo.IsFinished && !isexplosiveprojectile)
			{
				if (Widgets.ButtonText(rectFillant, "Choose fillant"))
				{
					var options3 = new List<FloatMenuOption>
					{
					};

					List<String> somelister = new List<string>();
					somelister.Add("nothing");
					somelister.Add("prometheum");
					somelister.Add("FSX");

					foreach (String szteel in somelister)
					{
						FloatMenuOption floatmenuoption = new FloatMenuOption("Fill with: " + szteel, delegate
						{

							fillant = szteel;
						});

						options3.Add(floatmenuoption);
					}
					Find.WindowStack.Add(new FloatMenu(options3));
				}
			}
			Rect rect69252 = new Rect(inRect);
			rect69252.width = 80f;
			rect69252.height = 40f;
			rect69252 = rect69252.CenteredOnXIn(inRect);
			rect69252 = rect69252.CenteredOnYIn(inRect);
			rect69252.x += 343f;
			rect69252.y += 70f;
			Widgets.TextArea(rect69252, "Charge amount above");
			Rect rectcharge = new Rect(inRect);
			rectcharge.width = 33f;
			rectcharge.height = 33f;
			rectcharge = rectcharge.CenteredOnXIn(inRect);
			rectcharge= rectcharge.CenteredOnYIn(inRect);
			rectcharge.x += 307f;
			rectcharge.y += 25f;
			if(Widgets.ButtonText(rectcharge, "+"))
			{
				if (ChargeAmount <= 2.75f)
				{
					ChargeAmount += 0.25f;
				}
				
			}
			Rect rectchargeminus = new Rect(inRect);
			rectchargeminus.width = 33f;
			rectchargeminus.height = 33f;
			rectchargeminus = rectchargeminus.CenteredOnXIn(inRect);
			rectchargeminus = rectchargeminus.CenteredOnYIn(inRect);
			rectchargeminus.x += 373f;
			rectchargeminus.y += 25f;
			if (Widgets.ButtonText(rectchargeminus, "-"))
			{
				if(ChargeAmount >= 0.5)
				{
					ChargeAmount -= 0.25f;
				}
				
			}
			
		
			Rect chargetext = new Rect(inRect);
			chargetext.width = 35f;
			chargetext.height = 33f;
			chargetext = chargetext.CenteredOnXIn(inRect);
			chargetext = chargetext.CenteredOnYIn(inRect);
			chargetext.x += 340f;
			chargetext.y += 25f;
			Widgets.TextArea(chargetext, ChargeAmount.ToString());
			if(isexplosiveprojectile && ProjectileShape == "Buckshot" | ProjectileShape == "Flechette")
			{
				Rect rectplusbigfragment = new Rect(inRect);
				rectplusbigfragment.width = 33f;
				rectplusbigfragment.height = 33f;
				rectplusbigfragment = rectplusbigfragment.CenteredOnXIn(inRect);
				rectplusbigfragment = rectplusbigfragment.CenteredOnYIn(inRect);
				rectplusbigfragment.x += 307f;
				rectplusbigfragment.y += -25f;
				if (Widgets.ButtonText(rectplusbigfragment, "+"))
				{
					
					if(ProjectileShape == "Buckshot")
					{
						if (pelcal <= 9)
						{
							pelcal += 0.10f;
							PelCount -= (int)(Math.Round(pelcal) / 5);
						}
					}
					if (ProjectileShape == "Flechette")
					{
						
						if (PelCount > 1)
						{
							pelcal += 0.50f;
							PelCount -= 1;
						}
					}



				}
				
				Rect rectfragsmalltwo = new Rect(inRect);
				rectfragsmalltwo.width = 33f;
				rectfragsmalltwo.height = 33f;
				rectfragsmalltwo = rectfragsmalltwo.CenteredOnXIn(inRect);
				rectfragsmalltwo = rectfragsmalltwo.CenteredOnYIn(inRect);
				rectfragsmalltwo.x += 373f;
				rectfragsmalltwo.y += -25f;
				if (Widgets.ButtonText(rectfragsmalltwo, "-"))
				{
					if (ProjectileShape == "Buckshot")
					{
						if (pelcal >= 1)
						{
							pelcal -= 0.10f;
							PelCount += (int)(Math.Round(pelcal) / 5);
						}
					}
					if (ProjectileShape == "Flechette")
					{
						if (pelcal >= 1 )
						{
							pelcal -= 0.50f;
							PelCount += 1;
						}
					}



				}
				Rect chargetext6969 = new Rect(inRect);
				chargetext6969.width = 35f;
				chargetext6969.height = 33f;
				chargetext6969 = chargetext6969.CenteredOnXIn(inRect);
				chargetext6969 = chargetext6969.CenteredOnYIn(inRect);
				chargetext6969.x += 340f;
				chargetext6969.y += -25f;
				Widgets.TextArea(chargetext6969, this.pelcal.ToString());

				Rect chargetext69696 = new Rect(inRect);
				chargetext69696.width = 101f;
				chargetext69696.height = 33f;
				chargetext69696 = chargetext69696.CenteredOnXIn(inRect);
				chargetext69696 = chargetext69696.CenteredOnYIn(inRect);
				chargetext69696.x += 340f;
				chargetext69696.y += -60f;
				Widgets.TextArea(chargetext69696, "Pellet size");

				Rect chargetext6969b = new Rect(inRect);
				chargetext6969b.width = 35f;
				chargetext6969b.height = 33f;
				chargetext6969b = chargetext6969b.CenteredOnXIn(inRect);
				chargetext6969b = chargetext6969b.CenteredOnYIn(inRect);
				chargetext6969b.x += 340f;
				chargetext6969b.y += -100f;
				Widgets.TextArea(chargetext6969b, this.PelCount.ToString());

				Rect chargetext69696a = new Rect(inRect);
				chargetext69696a.width = 101f;
				chargetext69696a.height = 33f;
				chargetext69696a = chargetext69696a.CenteredOnXIn(inRect);
				chargetext69696a = chargetext69696a.CenteredOnYIn(inRect);
				chargetext69696a.x += 340f;
				chargetext69696a.y += -140f;
				Widgets.TextArea(chargetext69696a, "Pellet count");
			}
			if (isexplosiveprojectile && ProjectileShape == "fragmentation")
			{
				Rect rectplusbigfragment = new Rect(inRect);
				rectplusbigfragment.width = 33f;
				rectplusbigfragment.height = 33f;
				rectplusbigfragment = rectplusbigfragment.CenteredOnXIn(inRect);
				rectplusbigfragment = rectplusbigfragment.CenteredOnYIn(inRect);
				rectplusbigfragment.x += 307f;
				rectplusbigfragment.y += -25f;
				if (Widgets.ButtonText(rectplusbigfragment, "+"))
				{
					var peni = (CompProperties_Fragments)SelectedProjectile?.comps.Find(G => G is CompProperties_Fragments);
					if(SelectedProjectile != null)
					{
						if (smolshrabcunt <= (peni?.fragments.Find(D => D.thingDef == AmmoClassesDefOf.Fragment_Small)?.count ?? 2) + 5)
						{
							smolshrabcunt += 1;
						}
					}
				

				}
				Rect rectfragsmalltwo = new Rect(inRect);
				rectfragsmalltwo.width = 33f;
				rectfragsmalltwo.height = 33f;
				rectfragsmalltwo = rectfragsmalltwo.CenteredOnXIn(inRect);
				rectfragsmalltwo = rectfragsmalltwo.CenteredOnYIn(inRect);
				rectfragsmalltwo.x += 373f;
				rectfragsmalltwo.y += -25f;
				if (Widgets.ButtonText(rectfragsmalltwo, "-"))
				{
					if (smolshrabcunt >= 1)
					{
						smolshrabcunt -= 1;
					}

				}
				Rect chargetext6969 = new Rect(inRect);
				chargetext6969.width = 35f;
				chargetext6969.height = 33f;
				chargetext6969 = chargetext6969.CenteredOnXIn(inRect);
				chargetext6969 = chargetext6969.CenteredOnYIn(inRect);
				chargetext6969.x += 340f;
				chargetext6969.y += -25f;
				Widgets.TextArea(chargetext6969, smolshrabcunt.ToString());
				Rect chargetext69696 = new Rect(inRect);
				chargetext69696.width = 101f;
				chargetext69696.height = 33f;
				chargetext69696 = chargetext69696.CenteredOnXIn(inRect);
				chargetext69696 = chargetext69696.CenteredOnYIn(inRect);
				chargetext69696.x += 340f;
				chargetext69696.y += -60f;
				Widgets.TextArea(chargetext69696, "Small fragments amount");

				if(SelectedProjectile != null)
				{
					kurwakurwakurwa = SelectedProjectile.comps.Find(g => g is CompProperties_Fragments) as CompProperties_Fragments;
				}
				
				if (SelectedProjectile != null)
				{
					

					Rect rectplusbigbigfragment = new Rect(inRect);
					rectplusbigbigfragment.width = 33f;
					rectplusbigbigfragment.height = 33f;
					rectplusbigbigfragment = rectplusbigbigfragment.CenteredOnXIn(inRect);
					rectplusbigbigfragment = rectplusbigbigfragment.CenteredOnYIn(inRect);
					rectplusbigbigfragment.x += 307f;
					rectplusbigbigfragment.y += -100f;
					if (Widgets.ButtonText(rectplusbigbigfragment, "+"))
					{
						var peni = (CompProperties_Fragments)SelectedProjectile?.comps.Find(G => G is CompProperties_Fragments);

						if (SelectedProjectile != null)
						{
							if (bigshrabint <= (peni?.fragments.Find(D => D.thingDef == AmmoClassesDefOf.Fragment_Large)?.count ?? -1) + 5)
							{
								bigshrabint += 1;
							}
						}
						


					}
					Rect rectfragbigtwo = new Rect(inRect);
					rectfragbigtwo.width = 33f;
					rectfragbigtwo.height = 33f;
					rectfragbigtwo = rectfragbigtwo.CenteredOnXIn(inRect);
					rectfragbigtwo = rectfragbigtwo.CenteredOnYIn(inRect);
					rectfragbigtwo.x += 373f;
					rectfragbigtwo.y += -100f;
					if (Widgets.ButtonText(rectfragbigtwo, "-"))
					{
						if (bigshrabint >= 1)
						{
							bigshrabint -= 1;
						}

					}
					Rect efdwsx = new Rect(inRect);
					efdwsx.width = 35f;
					efdwsx.height = 33f;
					efdwsx = efdwsx.CenteredOnXIn(inRect);
					efdwsx = efdwsx.CenteredOnYIn(inRect);
					efdwsx.x += 340f;
					efdwsx.y += -100f;
					Widgets.TextArea(efdwsx, bigshrabint.ToString());
					Rect sdfecw = new Rect(inRect);
					sdfecw.width = 101f;
					sdfecw.height = 33f;
					sdfecw = sdfecw.CenteredOnXIn(inRect);
					sdfecw = sdfecw.CenteredOnYIn(inRect);
					sdfecw.x += 340f;
					sdfecw.y += -135f;
					Widgets.TextArea(sdfecw, "Big fragments amount");
				}
			}
			
			
			Rect rectDesign = new Rect(inRect);
			rectDesign.width = 100f;
			rectDesign.height = 100f;
			rectDesign = rectDesign.CenteredOnXIn(inRect);
			rectDesign = rectDesign.CenteredOnYIn(inRect);
			rectDesign.x += -320f;
			rectDesign.y += -200f;
			if (Widgets.ButtonText(rectDesign, "Choose projectile type"))
			{
			
				var options3 = new List<FloatMenuOption>
				{
				};

				List<String> somelister = new List<String>();
				if (!isexplosiveprojectile)
				{
					somelister.Add("Hollow point");
					somelister.Add("Armor piercing");
					somelister.Add("Full Metal Jacket");
					somelister.Add("Sabot");
				}
				else
				{
				
					somelister.Add("fragmentation");
					somelister.Add("Buckshot");
					somelister.Add("Flechette");
				}

				
				

				foreach (var szteel in somelister)
				{
					FloatMenuOption floatmenuoption = new FloatMenuOption(szteel, delegate
					{
						ProjectileShape = szteel;
					});
					options3.Add(floatmenuoption);
				}
				Find.WindowStack.Add(new FloatMenu(options3));
			}

			Rect rect7 = new Rect(inRect);
			rect7.width = 80f;
			rect7.height = 80f;
			rect7 = rect7.CenteredOnXIn(inRect);
			rect7 = rect7.CenteredOnYIn(inRect);
			rect7.x += 320f;
			rect7.y += -200f;

			Rect rect6 = new Rect(inRect);
			rect6.width = 100f;
			rect6.height = 100f;
			rect6 = rect6.CenteredOnXIn(inRect);
			rect6 = rect6.CenteredOnYIn(inRect);
			rect6.x += 340f;
			rect6.y += 200f;
			List<ThingDef> list12345 = new List<ThingDef>();
			list12345.Add(CE_ThingDefOf.AmmoBench);
			
			if (Widgets.ButtonText(rect6, "Finish"))
			{
				++pint;
				CalculateAP();
				
				MadeProjectile = new ThingDef() { tickerType = TickerType.Normal, graphic = SelectedProjectile.graphic, label = projectile_material.label + " " + ammoclass_selected.label + " " + ProjectileShape, defName = $"GeneratedDef_Projectile{pint}" + "a" + Rand.Range(0, 256790333).ToString() + "v", graphicData = SelectedProjectile?.graphicData, projectile = new ProjectilePropertiesCE() { damageDef = DamageDefOf.Bullet, armorPenetrationSharp = ArmorPenSharpCalculated, armorPenetrationBlunt = ArmorPenSharpCalculated * 6, speed = 150f, flyOverhead = false, ai_IsIncendiary = false, dropsCasings = true, pelletCount = 1, stoppingPower = 2.5f }, thingClass = AmmoClassesDefOf.testshit.thingClass };
				MadeAmmoDef = new AmmoDef()
				{
					thingClass = AmmoClassesDefOf.Ammo_556x45mmNATO_FMJ.thingClass,
					label = projectile_material.label + " " + ammoclass_selected.label + "  " + ProjectileShape,
					defName = $"GeneratedDef_Ammo{pint}" +"b" + Rand.Range(0, 694201314).ToString() + "c",
					graphicData = ammodef_selected?.graphicData,
					cookOffProjectile = MadeProjectile,
					projectile = MadeProjectile.projectile,
					
					ammoClass = new AmmoCategoryDef
					{
						defName = $"GeneratedDef_Ammoclass{pint}" + Rand.Range(0, 256923213).ToString() +"a" ,
						label = projectile_material.label + " " + ammoclass_selected.label,
						description = projectile_material.label + " " + ammoclass_selected.label + " " + casematerial.label + " cased",
						labelShort = projectile_material.label + " " + ammoclass_selected.label
					}
				};
			
				
				foreach(AmmoSetDef caliber in calibers)
				{
					caliber.ammoTypes.Add(new AmmoLink() { ammo = MadeAmmoDef, projectile = MadeProjectile });
					foreach (AmmoLink amlink in ammoclass_selected.ammoTypes)
					{
						Log.Error(caliber.ToString());
					}
				}
				DefGenerator.AddImpliedDef(MadeAmmoDef);
				DefGenerator.AddImpliedDef(MadeProjectile);
				List<ThingDefCountClass> products = new List<ThingDefCountClass>();
				products.Add(new ThingDefCountClass
				{
					thingDef = MadeAmmoDef,
					count = 500
				});
				ThingFilter filter = new ThingFilter();
				filter.AllowedThingDefs.ToList().Add(projectile_material);
				filter.SetAllow(projectile_material, true);

				ThingFilter filter2 = new ThingFilter();
				filter2.AllowedThingDefs.ToList().Add(casematerial);
				filter2.SetAllow(casematerial, true);
				Log.Error(filter.AllowedDefCount.ToString());
				CalculateDamage();
				List<IngredientCount> ingredientss = new List<IngredientCount>();
				IngredientCount ingredient1 = new IngredientCount() { filter = filter };
				IngredientCount ingredient2 = new IngredientCount() { filter = filter2 };
				ingredient1.SetBaseCount(DefDatabase<RecipeDef>.AllDefs.ToList().Find(A => A.defName == "Make" + ammodef_selected.defName).ingredients.Find(B => B.GetBaseCount() != 0).GetBaseCount() / 2);
				ingredientss.Add(ingredient1);
				ingredient2.SetBaseCount(DefDatabase<RecipeDef>.AllDefs.ToList().Find(A => A.defName == "Make" + ammodef_selected.defName).ingredients.Find(B => B.GetBaseCount() != 0).GetBaseCount() / 2);
				ingredientss.Add(ingredient2);

				ThingFilter filter3 = new ThingFilter();
				filter3.AllowedThingDefs.ToList().Add(propelant);
				filter3.SetAllow(propelant, true);
				IngredientCount ingredient3 = new IngredientCount() { filter = filter3 };
				ingredient3.SetBaseCount(ChargeAmount);
				ingredientss.Add(ingredient3);

				List<ThingDef> users = new List<ThingDef>();
				
				users.Add(CE_ThingDefOf.AmmoBench);

				RecipeDef makeammorecipe = new RecipeDef
				{
					products = products,
					workSkill = SkillDefOf.Crafting,
					fixedIngredientFilter = filter,
					ingredients = ingredientss,
					workAmount = DefDatabase<RecipeDef>.AllDefs.ToList().Find(A => A.defName == "Make" + ammodef_selected.defName).workAmount,
					jobString = "Making ammo",
					effectWorking = EffecterDefOf.Clean,
					label = "Make" + " " + MadeAmmoDef.label,
					workSpeedStat = StatDefOf.GeneralLaborSpeed,
					defName = "Make" + MadeAmmoDef.defName,
					recipeUsers = users,
					unfinishedThingDef = AmmoClassesDefOf.UnfinishedAmmo,
					defaultIngredientFilter = filter,
					



			};
				RecipeDefGenerator.ImpliedRecipeDefs().ToList().Add(makeammorecipe);
				List<Building> benchesidk = mpaa.listerBuildings.AllBuildingsColonistOfDef(CE_ThingDefOf.AmmoBench).ToList();
				if(benchesidk.Count >= 1)
				{
					benchesidk.RandomElement().def.AllRecipes.Add(makeammorecipe);
					foreach (Building a in benchesidk)
					{
						Log.Error(makeammorecipe.defName);
						
					}
				}
				
				Log.Error(mpaa.ToString());
				Log.Error(postition.ToString());
				MadeAmmoDef.selectable = true;
				MadeAmmoDef.stackLimit = 500;
				Log.Error((casematerial.BaseMass * 2).ToString());
				Log.Error((ammodef_selected.BaseMass).ToString());
				//Log.Error(MadeAmmoDef.statBases.Find(ABC => ABC.stat.defName == "Mass").value.ToString());
				MadeAmmoDef.statBases = new List<StatModifier>();
				MadeAmmoDef.statBases.Add(new StatModifier
				{
					stat = StatDefOf.Mass,
					value = ammodef_selected.BaseMass * (casematerial.BaseMass * 2)
				});
				foreach(StatModifier amongsussybaka in MadeAmmoDef.statBases)
				{
					Log.Error(amongsussybaka.value.ToString());
				}
				float athink = 0f;
				if (propelant.statBases.Find(Poof => Poof.stat.defName == "PowderPower").value > 1 && ProjectileShape != "Sabot")
				{
					athink = (ChargeAmount * propelant.statBases.Find(Poof => Poof.stat.defName == "PowderPower").value) / 10;
					//Log.Message(athink.ToString());
				}
				if (propelant.statBases.Find(Poof => Poof.stat.defName == "PowderPower").value > 1 && ProjectileShape == "Sabot")
				{
					athink = ((ChargeAmount * propelant.statBases.Find(Poof => Poof.stat.defName == "PowderPower").value) / 10) * 2;
					//Log.Message(athink.ToString());
				}
				Log.Message(athink.ToString() + "testing");

				MadeAmmoDef.comps.Add(new damagercproprs { dammpoints = athink});
				MadeAmmoDef.useHitPoints = true;
				MadeAmmoDef.tickerType = TickerType.Normal;
				MadeAmmoDef.comps.Add(new CompProperties_Forbiddable { });
				MadeAmmoDef.category = ThingCategory.Item;
				MadeAmmoDef.altitudeLayer = AltitudeLayer.Item;
				MadeAmmoDef.statBases = ammodef_selected.statBases;
				MadeAmmoDef.alwaysHaulable = true;
				MadeAmmoDef.drawGUIOverlay = true;
				this.Close();
				List<DefModExtension> defModExtensions = new List<DefModExtension>();
				defModExtensions.Add(new BulletModExtension { });
				MadeProjectile.modExtensions = defModExtensions;
				MadeProjectile.GetModExtension<BulletModExtension>().FixedDamage = DamageCalculated;
				MadeAmmoDef.description = "Actual damage is: " + DamageCalculated.ToString();
				ProjectilePropertiesCE projpropce = MadeProjectile.projectile as ProjectilePropertiesCE;
				List<SecondaryDamage> secdmgs = new List<SecondaryDamage>();
				if(casematerial == ThingDefOf.Chemfuel)
				{
					MadeAmmoDef.label = "polymer cased " + projectile_material.label + " " + "projectile " + " " + ammoclass_selected.label + " " + ProjectileShape;
					MadeAmmoDef.ammoClass.description = "polymer cased " + projectile_material.label + " " + ammoclass_selected.label;
				}
				CalculateSecDmg(fillant, makeammorecipe, secdmgs);
				 
				if (fillantdef != null)
				{
					
					projpropce.secondaryDamage = secdmgs;
				}
				if (isexplosiveprojectile && ProjectileShape == "fragmentation")
				{
					MakeFrags();
					List<DefModExtension> defModExtensionss = new List<DefModExtension>();
					defModExtensionss.Add(new BulletModExtension { });
					smolshrab.modExtensions = defModExtensions;
					smolshrab.GetModExtension<BulletModExtension>().FixedDamage = 10;
					//MadeProjectile.thingClass = typeof(ProjectileCE_Explosive);
					ProjectilePropertiesCE ProjCE = MadeProjectile.projectile as ProjectilePropertiesCE;
					ProjectilePropertiesCE selectedProjCE = SelectedProjectile.projectile as ProjectilePropertiesCE;
					//ProjCE.explosionRadius = selectedProjCE.explosionRadius;
					projpropce.secondaryDamage = new List<SecondaryDamage>();
					CompProperties_ExplosiveCE proper = new CompProperties_ExplosiveCE { damageAmountBase = selectedProjCE.GetDamageAmount(1), explosiveDamageType = DamageDefOf.Bomb, explosiveRadius = selectedProjCE.explosionRadius  };
					List<ThingDefCountClass> frags = new List<ThingDefCountClass>();
					frags.Add(new ThingDefCountClass { thingDef = AmmoClassesDefOf.Fragment_Small, count = smolshrabcunt});
					if(bigshrabint >= 1)
					{
						frags.Add(new ThingDefCountClass { thingDef = AmmoClassesDefOf.Fragment_Large, count = bigshrabint });
					}
					
					CompProperties_Fragments proper2 = new CompProperties_Fragments { fragments =  frags};
					MadeProjectile.comps.Add(proper);
					MadeProjectile.comps.Add(proper2);
					MadeAmmoDef.statBases.Find(gvhj => gvhj.stat == StatDefOf.Mass).value += ((smolshrabcunt * 0.03f) + (bigshrabint * 0.06f));
					

				}
				if (ProjectileShape == "Buckshot" && isexplosiveprojectile)
				{
					ProjectilePropertiesCE prohectyl = MadeProjectile.projectile as ProjectilePropertiesCE;
					MadeProjectile.graphic = AmmoClassesDefOf.Bullet_12Gauge_Buck.graphic;
					prohectyl.pelletCount = PelCount;
					prohectyl.spreadMult = 17.9f * (PelCount / 10f);
				}
				if (ProjectileShape == "Flechette" && isexplosiveprojectile)
				{
					ProjectilePropertiesCE prohectyl = MadeProjectile.projectile as ProjectilePropertiesCE;
					MadeProjectile.graphic = AmmoClassesDefOf.Bullet_12Gauge_Buck.graphic;
					prohectyl.pelletCount = PelCount;
					prohectyl.spreadMult = 36f * (PelCount / 10f);
					
				}
				if (SelectedProjectile.projectile.flyOverhead)
				{
					ProjectilePropertiesCE projectile = (ProjectilePropertiesCE)MadeProjectile.projectile;
					projectile.speed = 0;
					projectile.flyOverhead = true;
					
				}
				bool flag = ProjectileShape == "Buckshot";
				if (isexplosiveprojectile && ProjectileShape == "Buckshot")
				{
					makeammorecipe.products.First().count = 200;
				}
				if (ammodef_selected.defName == "Ammo_90mmCannonShell_HE")
				{
					makeammorecipe.products.First().count = 5;
				}
				List<ThingCategoryDef> thingCategories = new List<ThingCategoryDef>();
				thingCategories.Add(DefDatabase<ThingCategoryDef>.AllDefs.ToList().Find(G => G.defName == "HandsLoadedAmmo"));
				DefDatabase<ThingCategoryDef>.AllDefs.ToList().Find(G => G.defName == "HandsLoadedAmmo").childThingDefs.Add(MadeAmmoDef);
				//DefDatabase<ThingCategoryDef>.AllDefs.ToList().Find(G => G.defName == "HandsLoadedAmmo").
				MadeAmmoDef.thingCategories = thingCategories;
				foreach(ThingDef thing in DefDatabase<ThingCategoryDef>.AllDefs.ToList().Find(G => G.defName == "HandsLoadedAmmo").childThingDefs)
				{
					Log.Error(thing.label);
				}
				MadeAmmoDef.comps.Add(new CompProperties { compClass = typeof(HaulComp) });
				MadeAmmoDef.alwaysHaulable = true;
				DefDatabase<ThingDef>.Add(MadeAmmoDef);
				Find.World.GetComponent<SaverComp>().SaveAmmodef(MadeAmmoDef, MadeAmmoDef.ammoClass, MadeProjectile, ammoclass_selected, makeammorecipe, ChargeAmount, smolshrab, athink);
				List<Zone> bone = new List<Zone>();
				//bone = Find.CurrentMap.zoneManager.AllZones.FindAll(OOF => OOF is Zone_Stockpile); 
				foreach (Zone_Stockpile zonee in bone)
				{
					Zone_Stockpile cockpile = zonee as Zone_Stockpile;
					cockpile.settings.filter.AllowedThingDefs.AddItem(MadeAmmoDef);
					cockpile.settings.filter.SetAllow(MadeAmmoDef, true);
					List<ThingDef> things = new List<ThingDef>();
					things.Add(MadeAmmoDef);
					cockpile.settings.filter = new ThingFilter {};
					cockpile.settings.filter.AllowedThingDefs.AddItem(MadeAmmoDef);
					cockpile.settings.filter.SetAllow(MadeAmmoDef, true);
					Log.Message(cockpile.settings.filter.Allows(MadeAmmoDef).ToString() + "a,p");
					Log.Error(cockpile.settings.filter.AllowedThingDefs.ToList().Find(o => o == MadeAmmoDef).ToString() + cockpile.label);
				}
				
				
				
				//cockpile.settings.filter.


			}
			Rect rect69 = new Rect(inRect);
			rect69.width = 100f;
			rect69.height = 100f;
			rect69 = rect69.CenteredOnXIn(inRect);
			rect69 = rect69.CenteredOnYIn(inRect);
			rect69.x += 0f;
			rect69.y += 0f;
			//colour = GUI.VerticalSlider(rect69, colour, 0, 255);
			Color col = new Color { r = colour, b = 0, a = 0, g = 0 };
			if(MadeAmmoDef != null)
			{
				
				MadeAmmoDef.colorGenerator = new ColorGenerator_Single { color = col };
				MadeAmmoDef.uiIconColor = col;
				GUI.DrawTexture(position, MadeAmmoDef.uiIcon);
			}
			else
			{
				GUI.DrawTexture(position, sometextureidk);
			}
			
			

			Text.Font = GameFont.Medium;
			Text.Anchor = TextAnchor.MiddleCenter;
			Widgets.Label(new Rect(0f, 0f, inRect.width, 32f), "Design bullets");
			Text.Font = GameFont.Small;
			Text.Anchor = TextAnchor.MiddleLeft;
			float num = 32f;
			Rect rect2 = new Rect(32f, num, 240f, 24f);
		}
		public DamageDef secondarydamagedef;
		public bool usefillatn;
		public void CalculateDamage()
		{


			int DamageMult = new int();
			if (ProjectileShape == "Hollow point")
			{
				DamageMult = SelectedProjectile.projectile.GetDamageAmount(1f) * 2;

				Log.Message(DamageMult.ToString());
			}
			if (ProjectileShape == "Armor piercing")
			{
				DamageMult = SelectedProjectile.projectile.GetDamageAmount(1f) / 2;

				Log.Message(DamageMult.ToString());
			}
			if (ProjectileShape == "Full Metal Jacket")
			{
				DamageMult = SelectedProjectile.projectile.GetDamageAmount(1f);

				Log.Message(DamageMult.ToString());
			}
			if (ProjectileShape == "Sabot")
			{
				DamageMult = SelectedProjectile.projectile.GetDamageAmount(1f) / 2;

				Log.Message(DamageMult.ToString());
			}
			if (projectile_material.label == "Korwinium" && ammodef_selected.ammoClass == AmmoClassesDefOf.MusketBall)
			{
				DamageMult += 240;
				Log.Error(DamageMult.ToString());
			}
			float Damagemult2 = new float();
			
			//Log.Error(propelant.statBases.Find(abc => abc.stat.defName == "PowderPower").value.ToString());
			Damagemult2 = propelant.statBases.Find(abc => abc.stat.defName == "PowderPower").value;
			if (Damagemult2 == 0f)
			{
				Log.Error("propelant most likely has no PowderPower statbase");
			}
			if(ProjectileShape == "Buckshot")
			{
				DamageCalculated = (float)Math.Round(8 * ChargeAmount * Damagemult2 * (pelcal / 10f));
			}
			else if (ProjectileShape == "Flechette")
			{
				DamageCalculated = (float)Math.Round(12 * ChargeAmount * Damagemult2 * (pelcal / 10f));
			}
			else
			{
				DamageCalculated = (float)Math.Round(DamageMult * Damagemult2 * (ChargeAmount));
			}
			


		}
		
		public void CalculateSecDmg(string tring, RecipeDef recipe, List<SecondaryDamage> secondaries)
		{
			switch (tring)
			{
				case "FSX":
					fillantdef = CE_ThingDefOf.FSX;
					secondaries.Add(new SecondaryDamage { amount = (int)MadeProjectile.GetModExtension<BulletModExtension>().FixedDamage / 2, chance = 1f, def = DamageDefOf.Bomb });
					ThingFilter filteR = new ThingFilter { };
					filteR.AllowedThingDefs.ToList().Add(CE_ThingDefOf.FSX);
					filteR.SetAllow(CE_ThingDefOf.FSX, true);
					IngredientCount ingrcount = new IngredientCount { filter = filteR };
					ingrcount.SetBaseCount(2);
					recipe.ingredients.Add(ingrcount);
					MadeProjectile.label += " (HE)";
					break;
				case "prometheum":
					secondaries.Add(new SecondaryDamage { amount = (int)MadeProjectile.GetModExtension<BulletModExtension>().FixedDamage / 2, chance = 1f, def = CE_DamageDefOf.Flame_Secondary });
					fillantdef = AmmoClassesDefOf.Prometheum;
					ThingFilter filteRr = new ThingFilter { };
					filteRr.AllowedThingDefs.ToList().Add(CE_ThingDefOf.FSX);
					filteRr.SetAllow(AmmoClassesDefOf.Prometheum, true);
					IngredientCount ingrcount2 = new IngredientCount { filter = filteRr };
					ingrcount2.SetBaseCount(2);
					recipe.ingredients.Add(ingrcount2);
					MadeProjectile.label += " (API)";
					break;
				case "nothing":
					fillantdef = null;
					break;
			}
				
		}

		public ThingDef bigshrap;
		public ThingDef smolshrab;
		public float colour;
		public void MakeFrags()
		{
			ThingDef shrab = new ThingDef { thingClass = typeof(HandLoading.Bullet), tickerType = TickerType.Normal, graphic = AmmoClassesDefOf.Fragment_Small.graphic, graphicData = AmmoClassesDefOf.Fragment_Small.graphicData, uiIcon = AmmoClassesDefOf.Fragment_Small.uiIcon, projectile = new ProjectilePropertiesCE() { flyOverhead = false, pelletCount = 1, alwaysFreeIntercept = true, speed = AmmoClassesDefOf.Fragment_Small.projectile.speed * ChargeAmount, damageDef = DamageDefOf.Bullet, armorPenetrationSharp = 1 * hardness_material_multiplier * ChargeAmount, armorPenetrationBlunt = 12 * hardness_material_multiplier * ChargeAmount  }, defName = Rand.Range(0, 6896).ToString() + "d" + Rand.Range(0, 231) + "ghj", label = "custom shrabel"  };
			smolshrab = shrab;
			ProjectilePropertiesCE projproceagain = shrab.projectile as ProjectilePropertiesCE;
			//projproceagain.gravityFactor = 15;
			projproceagain.speed = 20f;
			
		}
		
		public float DamageCalculated;
	}

	public class PowderComp : ThingComp
	{
		public float Power => Props.Power;
		public PowderCompProps Props => (PowderCompProps)this.props;

		public override void Initialize(CompProperties props)
		{
			Log.Message("Initialized.");
			
		}
	}

	public class PowderCompProps : CompProperties
	{
		public float Power;
		//public float power = null;

		public PowderCompProps()
		{
			this.compClass = typeof(PowderComp);
		}

		public PowderCompProps(Type compClass) : base(compClass)
		{
			this.compClass = compClass;
		}
	}
}
