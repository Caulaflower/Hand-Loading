using Verse;
using Verse.AI;
using RimWorld;
using System;
using CombatExtended;
using System.Collections.Generic;
using System.Linq;
namespace HandLoading
{

    [DefOf]
    public class RecipeShapDefOf : DefOf
    {
        public static RecipeShapeDef buckshit;

        public static RecipeShapeDef fleshit;

        public static RecipeShapeDef thermo_shape;
    }
    public class RecipeShapeDef : Def
    {
        public ResearchProjectDef needed;

        public float damagemult;

        public float penmult;

        public int pelletcount = 1;

        public bool ischarge = false;

        public bool ismulti = false;

        public float spreadmult = 0f;

        public bool isfrag = false;

        public float workmult = 1f;

        public bool isbootleq = false;

        public TechLevel techlevel = TechLevel.Industrial;

        public bool dmgusestatpawn = false;

        public bool penusestatpawn = true;

        public bool isjacketed = false;

        public float AI_cost_mult = 1f;

        public bool ThermoBar = false;

        public int popcount = 200;

        //idk why tf are you looking into the code, but anyway, the default list breaks
        public List<ThingDef> Users = new List<ThingDef> { CE_ThingDefOf.AmmoBench };
    }
}
