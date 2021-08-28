using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using RimWorld.Planet;
using Verse;
using Verse.AI;
using Verse.AI.Group;
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

        //Patch for worldmap death
        [HarmonyPriority(Priority.First)]
        static void Prefix(Pawn __instance, out RoyalTitleDef __state)
        {

            HediffDef RegenerationHediff = HediffDef.Named("Regeneration");
            RoyalTitleDef titleDef = null;

            if (HasRegen(__instance))
            {
                //Royal Prefix

                if (__instance.royalty.HasAnyTitleIn(Find.FactionManager.FirstFactionOfDef(FactionDefOf.Empire)))
                {
                    titleDef = __instance.GetCurrentTitleIn(Find.FactionManager.FirstFactionOfDef(FactionDefOf.Empire));
                    Log.Warning("Title is: " + titleDef.defName);
                }
                

                //Caravan Prefix
                if (__instance.IsCaravanMember())
                {
                    Log.Warning("In caravan: " + __instance.GetCaravan().Label);

                    //Map map = CaravanIncidentUtility.GetOrGenerateMapForIncident(__instance.GetCaravan(), new IntVec3(100, 1, 100), WorldObjectDefOf.Site);
                    //map.Parent.SetFaction(__instance.Faction);
                    //IntVec3 playerSpot;
                    //IntVec3 enemySpot;
                    //MultipleCaravansCellFinder.FindStartingCellsFor2Groups(map, out playerSpot, out enemySpot);
                    //CaravanEnterMapUtility.Enter(__instance.GetCaravan(), map, (Pawn p) => CellFinder.RandomClosewalkCellNear(playerSpot, map, 12, null), CaravanDropInventoryMode.DoNotDrop, true);
                    
                    

                }
            }

            __state = titleDef;
        }

        //Patch for generated map death
        [HarmonyPriority(Priority.First)]
        static void Postfix(Pawn __instance, RoyalTitleDef __state)
        {
            //Only autoresurrect if colonist has the regeneration nuclei hediff and has less than 12 regenerations left
            HediffDef RegenerationHediff = HediffDef.Named("Regeneration");
            Caravan caravan = __instance.GetCaravan();

            Debug.Log(caravan);

            if (HasRegen(__instance))
            {
                if (!__instance.health.hediffSet.HasHediff(HediffDef.Named("Regeneration13")))
                {
                    Regenerate(__instance, __state);
                }
            }
        }

        //Change Appearance, Traits and memories of pawns
        static void Regenerate(Pawn __instance, RoyalTitleDef __state)
        {
            Log.Warning("Triggering regen");

            // Health State Changes ----------------------------------------------------------------------------------------------------------

            //Resurrect the __instance
            //ResurrectionUtility.Resurrect(__instance);
            RegenResurrect(__instance, __instance.GetCaravan());

            //Remove all bad hediffs
            foreach (Hediff h in __instance.health.hediffSet.GetHediffs<Hediff>())
            {
                if(h.def.isBad)
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
            if(__instance.gender == Gender.Female)
            {
                __instance.style.beardDef = null;
            }

            //Skintone
            __instance.story.melanin = 0.01f * Rand.Range(10, 200);

            //Head
            var graphicPath = GraphicDatabaseHeadRecords.GetHeadRandom(__instance.gender, __instance.story.SkinColor, __instance.story.crownType,true).GraphicPath;
            Traverse.Create(__instance.story).Field("headGraphicPath").SetValue(graphicPath);

            //Hair
            //__instance.story.hairDef = PawnHairChooser.RandomHairDefFor(__instance, FactionDefOf.PlayerColony);

            __instance.story.hairDef = PawnStyleItemChooser.RandomHairFor(__instance);
            __instance.story.hairColor = PawnHairColors.RandomHairColor(__instance.story.SkinColor,__instance.ageTracker.AgeBiologicalYears);

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

            //Royalty Patch
            if(__state != null)
            {
                __instance.royalty.SetTitle(Find.FactionManager.FirstFactionOfDef(FactionDefOf.Empire), __state, true, false, false);
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

            //Glow effect (ugly approach, look at cleaning this up)
            FleckMaker.ThrowFireGlow(__instance.Position.ToVector3(), __instance.Map, 3f);
            FleckMaker.ThrowFireGlow(__instance.Position.ToVector3(), __instance.Map, 3f);
            FleckMaker.ThrowFireGlow(__instance.Position.ToVector3(), __instance.Map, 3f);
            FleckMaker.ThrowFireGlow(__instance.Position.ToVector3(), __instance.Map, 3f);
            FleckMaker.ThrowFireGlow(__instance.Position.ToVector3(), __instance.Map, 3f);
        }

        //Custom Resurrection
        public static void RegenResurrect(Pawn pawn, Caravan caravan)
        {
            if (!pawn.Dead)
            {
                Log.Error("Tried to resurrect a pawn who is not dead: " + pawn.ToStringSafe<Pawn>());
                return;
            }
            if (pawn.Discarded)
            {
                Log.Error("Tried to resurrect a discarded pawn: " + pawn.ToStringSafe<Pawn>());
                return;
            }

            Corpse corpse = pawn.Corpse;
            bool flag = false;
            IntVec3 loc = IntVec3.Invalid;
            Map map = null;
            if (corpse != null)
            {
                Log.Warning("a");
                flag = corpse.Spawned;
                loc = corpse.Position;
                map = corpse.Map;
                corpse.InnerPawn = null;
                corpse.Destroy(DestroyMode.Vanish);
            }

            Log.Warning("flag: " + flag);
            Log.Warning("world pawn: " + pawn.IsWorldPawn());

            if (flag && pawn.IsWorldPawn())
            {
                Find.WorldPawns.RemovePawn(pawn);
            }
            else
            {
                Log.Warning("b");
                pawn.GetCaravan().AddPawn(pawn, true);
            }

            pawn.ForceSetStateToUnspawned();
            PawnComponentsUtility.CreateInitialComponents(pawn);
            pawn.health.Notify_Resurrected();
            if (pawn.Faction != null && pawn.Faction.IsPlayer)
            {
                if (pawn.workSettings != null)
                {
                    pawn.workSettings.EnableAndInitialize();
                }
                Find.StoryWatcher.watcherPopAdaptation.Notify_PawnEvent(pawn, PopAdaptationEvent.GainedColonist);
            }
            if (flag)
            {
                Log.Warning("c");
                GenSpawn.Spawn(pawn, loc, map, WipeMode.Vanish);
                for (int i = 0; i < 10; i++)
                {
                    FleckMaker.ThrowAirPuffUp(pawn.DrawPos, map);
                }
                if (pawn.Faction != null && pawn.Faction != Faction.OfPlayer && pawn.HostileTo(Faction.OfPlayer))
                {
                    LordMaker.MakeNewLord(pawn.Faction, new LordJob_AssaultColony(pawn.Faction, true, true, false, false, true, false, false), pawn.Map, Gen.YieldSingle<Pawn>(pawn));
                }
                if (pawn.apparel != null)
                {
                    List<Apparel> wornApparel = pawn.apparel.WornApparel;
                    for (int j = 0; j < wornApparel.Count; j++)
                    {
                        wornApparel[j].Notify_PawnResurrected();
                    }
                }
            }
            PawnDiedOrDownedThoughtsUtility.RemoveDiedThoughts(pawn);
            if (pawn.royalty != null)
            {
                pawn.royalty.Notify_Resurrected();
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
                else if(body > 0.25f && body < 0.5f)
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

        //Swap Gender
        static Gender GenderSwap(Gender gender)
        {
            var rand = Rand.Value;
            if (gender == Gender.Male)
            {
                if (rand > 0.5f)
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
                if (rand > 0.5f)
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

        //Rejoin carvan
        static void RejoinCaravan(Caravan caravan, Pawn ___instance)
        {
            Log.Warning("Pawn added back to caravan");
            caravan.AddPawn(___instance,true);
        }

    }

}