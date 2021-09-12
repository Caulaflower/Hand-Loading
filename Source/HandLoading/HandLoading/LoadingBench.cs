using System;
using System.Collections.Generic;
using System.Linq;
using CombatExtended;
using RimWorld;
using UnityEngine;
using Verse;
using HarmonyLib;
using HarmonyMod;
using HandLoading.CustomCalibers;


namespace HandLoading
{
	// Token: 0x0200004F RID: 79
	public class LoadingBench : Building, IThingHolder
	{
		public ThingOwner<Thing> ingredients = new ThingOwner<Thing>();

		
		public bool StorageTabVisible
		{
			get
			{
				return true;
			}
		}

		public void doob()
		{
			Find.WindowStack.Add(new HandLoading.HandLoadingWindow(this.Position, this.Map));
		}

		public void GetChildHolders(List<IThingHolder> outChildren)
		{
		}

		public ThingOwner GetDirectlyHeldThings()
		{
			return this.ingredients;
		}
		public override void SpawnSetup(Map map, bool respawningAfterLoad)
		{
			base.SpawnSetup(map, respawningAfterLoad);
			if (!respawningAfterLoad)
				doob();
		}
	}
	public class BenchComp : ThingComp
	{
		public bool dee = false;
		public BenchCompProps Props => (BenchCompProps)this.props;
		public override void Initialize(CompProperties props)
		{





		}
		public override IEnumerable<Gizmo> CompGetGizmosExtra()
		{
			HandLoadingWindow winda = new HandLoading.HandLoadingWindow(this.parent.Position,  this.parent.Map) { ammodef_selected = AmmoClassesDefOf.Ammo_556x45mmNATO_FMJ };
			yield return new Command_Action()
			{
				defaultLabel = "design custom ammunition",
				defaultDesc = "design your own new ammunition",
				icon = ContentFinder<Texture2D>.Get("Things/Ammo/Rifle/AP/AP_c", true),

				action = delegate { Log.Error(this.parent.Position.ToString()); Log.Message(ParentHolder.ToString()); Find.WindowStack.Add(winda);}


			};
			if (this.dee)
			{
				yield return new Command_Action()
				{
					defaultLabel = "design custom caliber",
					defaultDesc = "design your own new caliber",
					icon = ContentFinder<Texture2D>.Get("Things/Ammo/Rifle/AP/AP_c", true),

					action = delegate { Log.Error(this.parent.Position.ToString()); Log.Message(ParentHolder.ToString()); Find.WindowStack.Add(new CaliberMacher()); }


				};
			}
			

		}
	}
	public class BenchCompProps : CompProperties
	{

		public BenchCompProps()
		{
			this.compClass = typeof(BenchComp);
		}

		public BenchCompProps(Type compClass) : base(compClass)
		{
			this.compClass = compClass;
		}
	}
}
