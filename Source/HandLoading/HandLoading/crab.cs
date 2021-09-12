using System;
using System.Collections.Generic;
using System.Linq;
using CombatExtended;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.AI;
using HarmonyLib;
using HarmonyMod;
using HandLoading.CustomCalibers;

namespace HandLoading
{
   public class HaulComp : ThingComp
    {
        public override IEnumerable<FloatMenuOption> CompFloatMenuOptions(Pawn selPawn)
        {
            
            yield return new FloatMenuOption("Haul to nearest stockpile", delegate
            {
                Zone_Stockpile s = Find.CurrentMap.zoneManager.AllZones.FindAll(OOP => OOP is Zone_Stockpile).RandomElement() as Zone_Stockpile;
                IntVec3 ss = s.Cells.FindAll(Loof => Loof.GetThingList(Find.CurrentMap).Any(O => O.def == this.parent.def)).RandomElement();
                if(ss.z == 0 && ss.x == 0)
                {
                    ss = s.Cells.FindAll(Loof => (Loof.GetThingList(Find.CurrentMap).Count == 0)).RandomElement();
                    Log.Error(ss.ToString());
                   
                }
                Job robbery = new Job { def = JobDefOf.HaulToCell, targetA = this.parent, targetB = ss};
                robbery.count = 250;
                selPawn.jobs.StartJob(robbery, JobCondition.InterruptForced);

            });
        }
        public override void PostPostMake()
        {
            Log.Message("hello world");
            base.PostPostMake();
        }
    }
}
