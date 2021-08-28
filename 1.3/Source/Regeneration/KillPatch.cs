using System.Linq;
using RimWorld;
using Verse;
using HarmonyLib;
using UnityEngine;

namespace Regeneration
{
    [DefOf]
    public static class RegenerationThoughtDefs
    {
        public static ThoughtDef KnownColonistRegeneratedSocial;
        public static ThoughtDef KnownColonistRegenerated;
        public static ThoughtDef RecentlyRegenerated;
    }

    [DefOf]
    public static class RegenerationTaleDefs
    {
        public static TaleDef ColonistRegenerated;
    }

    //Harmony patching
    [StaticConstructorOnStartup]
    static class Patches
    {
        static Patches()
        {
            var harmony = new Harmony("com.demeggy.regeneration");
            harmony.PatchAll();
        }
    }

    //Patch the death process
    [HarmonyPatch(typeof(Pawn))]
    [HarmonyPatch("Kill")]
    static class PawnKill_Patch
    {

        //Patch for standard death
        [HarmonyPriority(Priority.First)]
        static void Postfix(Pawn __instance)
        {
            //Only autoresurrect if colonist has the regeneration nuclei hediff and has less than 12 regenerations left
            HediffDef RegenerationHediff = HediffDef.Named("Regeneration");

            if (HasRegen(__instance))
            {
                Log.Warning("has regen");

                if (!__instance.health.hediffSet.HasHediff(HediffDef.Named("Regeneration13")))
                {
                    Regenerate(__instance);
                }
            }
        }

        //Patch for Caravan/Worldmap death
        //TBF

        //Patch death messages
        //TBF

        //Change Appearance, Traits and memories of pawns
        static void Regenerate(Pawn __instance)
        {

            // Health State Changes ----------------------------------------------------------------------------------------------------------

            //Resurrect the __instance
            ResurrectionUtility.Resurrect(__instance);

            //Remove all bad hediffs
            foreach (Hediff h in __instance.health.hediffSet.GetHediffs<Hediff>())
            {
                if (h.def.isBad)
                {
                    __instance.health.RemoveHediff(h);
                }
            }

            //Add resurrection sickness
            __instance.health.AddHediff(HediffDef.Named("ResurrectionSickness"));

            //Remove current RegenHediff and add the next
            BodyPartRecord heart = __instance.health.hediffSet.GetNotMissingParts().First(bpr => bpr.def == BodyPartDefOf.Heart);

            HediffDef Regen1Def = HediffDef.Named("Regeneration01");
            HediffDef Regen2Def = HediffDef.Named("Regeneration02");
            HediffDef Regen3Def = HediffDef.Named("Regeneration03");
            HediffDef Regen4Def = HediffDef.Named("Regeneration04");
            HediffDef Regen5Def = HediffDef.Named("Regeneration05");
            HediffDef Regen6Def = HediffDef.Named("Regeneration06");
            HediffDef Regen7Def = HediffDef.Named("Regeneration07");
            HediffDef Regen8Def = HediffDef.Named("Regeneration08");
            HediffDef Regen9Def = HediffDef.Named("Regeneration09");
            HediffDef Regen10Def = HediffDef.Named("Regeneration10");
            HediffDef Regen11Def = HediffDef.Named("Regeneration11");
            HediffDef Regen12Def = HediffDef.Named("Regeneration12");
            HediffDef Regen13Def = HediffDef.Named("Regeneration13");

            if (__instance.health.hediffSet.HasHediff(Regen1Def)) { __instance.health.RemoveHediff(__instance.health.hediffSet.GetFirstHediffOfDef(Regen1Def)); __instance.health.AddHediff(Regen2Def, heart); }
            else if (__instance.health.hediffSet.HasHediff(Regen2Def)) { __instance.health.RemoveHediff(__instance.health.hediffSet.GetFirstHediffOfDef(Regen2Def)); __instance.health.AddHediff(Regen3Def, heart); }
            else if (__instance.health.hediffSet.HasHediff(Regen3Def)) { __instance.health.RemoveHediff(__instance.health.hediffSet.GetFirstHediffOfDef(Regen3Def)); __instance.health.AddHediff(Regen4Def, heart); }
            else if (__instance.health.hediffSet.HasHediff(Regen4Def)) { __instance.health.RemoveHediff(__instance.health.hediffSet.GetFirstHediffOfDef(Regen4Def)); __instance.health.AddHediff(Regen5Def, heart); }
            else if (__instance.health.hediffSet.HasHediff(Regen5Def)) { __instance.health.RemoveHediff(__instance.health.hediffSet.GetFirstHediffOfDef(Regen5Def)); __instance.health.AddHediff(Regen6Def, heart); }
            else if (__instance.health.hediffSet.HasHediff(Regen6Def)) { __instance.health.RemoveHediff(__instance.health.hediffSet.GetFirstHediffOfDef(Regen6Def)); __instance.health.AddHediff(Regen7Def, heart); }
            else if (__instance.health.hediffSet.HasHediff(Regen7Def)) { __instance.health.RemoveHediff(__instance.health.hediffSet.GetFirstHediffOfDef(Regen7Def)); __instance.health.AddHediff(Regen8Def, heart); }
            else if (__instance.health.hediffSet.HasHediff(Regen8Def)) { __instance.health.RemoveHediff(__instance.health.hediffSet.GetFirstHediffOfDef(Regen8Def)); __instance.health.AddHediff(Regen9Def, heart); }
            else if (__instance.health.hediffSet.HasHediff(Regen9Def)) { __instance.health.RemoveHediff(__instance.health.hediffSet.GetFirstHediffOfDef(Regen9Def)); __instance.health.AddHediff(Regen10Def, heart); }
            else if (__instance.health.hediffSet.HasHediff(Regen10Def)) { __instance.health.RemoveHediff(__instance.health.hediffSet.GetFirstHediffOfDef(Regen10Def)); __instance.health.AddHediff(Regen11Def, heart); }
            else if (__instance.health.hediffSet.HasHediff(Regen11Def)) { __instance.health.RemoveHediff(__instance.health.hediffSet.GetFirstHediffOfDef(Regen11Def)); __instance.health.AddHediff(Regen12Def, heart); }
            else if (__instance.health.hediffSet.HasHediff(Regen12Def)) { __instance.health.RemoveHediff(__instance.health.hediffSet.GetFirstHediffOfDef(Regen12Def)); __instance.health.AddHediff(Regen13Def, heart); }

            // Visual Changes ----------------------------------------------------------------------------------------------------------

            //Gender
            __instance.gender = GenderSwap(__instance.gender);

            //Skintone
            __instance.story.melanin = 0.01f * Rand.Range(10, 200);

            //Head
            var graphicPath = GraphicDatabaseHeadRecords.GetHeadRandom(__instance.gender, __instance.story.SkinColor, __instance.story.crownType, false).GraphicPath;
            Traverse.Create(__instance.story).Field("headGraphicPath").SetValue(graphicPath);

            //Hair
            __instance.story.hairDef = PawnStyleItemChooser.RandomHairFor(__instance);
            __instance.story.hairColor = HairColor();

            //Body
            __instance.story.bodyType = BodySwap(__instance.gender);

            //Redraw the __instance to trigger the above affects
            __instance.Drawer.renderer.graphics.nakedGraphic = null;

            // Bio Changes ----------------------------------------------------------------------------------------------------------

            //randomise traits
            __instance.story.traits.allTraits.Clear();
            int i = 0;
            int rInt = Rand.RangeInclusive(1, 3);
            while (i < rInt)
            {
                TraitDef random = DefDatabase<TraitDef>.GetRandom();
                Trait trait = new Trait(random, PawnGenerator.RandomTraitDegree(random), false);
                __instance.story.traits.GainTrait(trait);
                Trait trait2 = new Trait(random, PawnGenerator.RandomTraitDegree(random), false);
                __instance.story.traits.GainTrait(trait2);
                i++;
            }

            //Add Memory
            __instance.needs.mood.thoughts.memories.TryGainMemory(RegenerationThoughtDefs.RecentlyRegenerated, null);

            //Add debuff thought to all related colonists
            foreach (Pawn p in __instance.relations.RelatedPawns)
            {
                //Log.Warning("related to: " + p.Name);
                try
                {
                    if (!p.health.Dead)
                    {
                        p.needs.mood.thoughts.memories.TryGainMemory(RegenerationThoughtDefs.KnownColonistRegeneratedSocial, __instance);
                        p.needs.mood.thoughts.memories.TryGainMemory(RegenerationThoughtDefs.KnownColonistRegenerated);
                    }
                }
                catch
                {
                    Log.Warning("Couldn't add social debuff to " + p.Name);
                }
            }

            // Visual effects -------------------------------------------------------------------------------------------------------

            // Glow effect (ugly approach, look at cleaning this up)
            for (i = 0; i < 5; i++)
			{
                MoteMaker.MakeBombardmentMote(__instance.Position, __instance.Map, 3f);
            }
        }

        //Randomise Body
        static BodyTypeDef BodySwap(Gender gender)
        {
            var body = Rand.Value;
            if (gender == Gender.Male)
            {
                //Male
                if (body > 0f && body < 0.25f)
                {
                    return BodyTypeDefOf.Male;
                }
                else if (body > 0.25f && body < 0.5f)
                {
                    return BodyTypeDefOf.Thin;
                }
                else if (body > 0.5f && body < 0.75f)
                {
                    return BodyTypeDefOf.Fat;
                }
                else if (body > 0.75f && body < 1f)
                {
                    return BodyTypeDefOf.Hulk;
                }
            }
            else
            {
                //Female
                if (body > 0f && body < 0.25f)
                {
                    return BodyTypeDefOf.Female;
                }
                else if (body > 0.25f && body < 0.5f)
                {
                    return BodyTypeDefOf.Thin;
                }
                else if (body > 0.5f && body < 0.75f)
                {
                    return BodyTypeDefOf.Fat;
                }
                else if (body > 0.75f && body < 1f)
                {
                    return BodyTypeDefOf.Hulk;
                }
            }

            return BodyTypeDefOf.Thin;

        }

        //Randomise HairColor
        static Color HairColor()
        {
            var rand = Rand.Value;

            //Blonde
            if (rand < 0.25f)
                return new Color(0.96f, 0.94f, 0.77f);

            //Dark
            if (rand < 0.5f)
                return new Color(0.31f, 0.28f, 0.26f);

            //Red
            if (rand < 0.75f)
                return new Color(0.82f, 0.47f, 0.15f);

            //Grey
            if (rand < 0.75f)
                return new Color(0.7f, 0.7f, 0.7f);

            return new Color(0.3f, 0.2f, 0.1f);
        }

        //Swap Gender
        static Gender GenderSwap(Gender gender)
        {
            var rand = Rand.Value;
            if (gender == Gender.Male)
            {
                if (rand > 0.95f)
                {
                    Log.Warning("Switching to Female");
                    return Gender.Female;
                }
                else
                {
                    Log.Warning("Staying the same gender");
                }
            }
            else
            {
                if (rand > 0.95f)
                {
                    Log.Warning("Switching to Male");
                    return Gender.Male;
                }
                else
                {
                    Log.Warning("Staying the same gender");
                }
            }
            return gender;

        }

        //Check for existing incarnation
        static bool HasRegen(Pawn __instance)
        {
            //If pawn has any of the Hediffs belonging to the Regen Class, return true
            if (__instance.health.hediffSet.HasHediff(HediffDef.Named("Regeneration01")) ||
                __instance.health.hediffSet.HasHediff(HediffDef.Named("Regeneration02")) ||
                __instance.health.hediffSet.HasHediff(HediffDef.Named("Regeneration03")) ||
                __instance.health.hediffSet.HasHediff(HediffDef.Named("Regeneration04")) ||
                __instance.health.hediffSet.HasHediff(HediffDef.Named("Regeneration05")) ||
                __instance.health.hediffSet.HasHediff(HediffDef.Named("Regeneration06")) ||
                __instance.health.hediffSet.HasHediff(HediffDef.Named("Regeneration07")) ||
                __instance.health.hediffSet.HasHediff(HediffDef.Named("Regeneration08")) ||
                __instance.health.hediffSet.HasHediff(HediffDef.Named("Regeneration09")) ||
                __instance.health.hediffSet.HasHediff(HediffDef.Named("Regeneration10")) ||
                __instance.health.hediffSet.HasHediff(HediffDef.Named("Regeneration11")) ||
                __instance.health.hediffSet.HasHediff(HediffDef.Named("Regeneration12"))
                )
            {
                return true;
            }

            return false;
        }

    }

}