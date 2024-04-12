
using System;
using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using Verse;

namespace LetterPermanentInjury
{
    [HarmonyPatch(typeof(DamageWorker_AddInjury), "FinalizeAndAddInjury", new Type[]
    {
    typeof(Pawn),
    typeof(Hediff_Injury),
    typeof(DamageInfo),
    typeof(DamageWorker.DamageResult)
    })]
    public class Patch
    {
        public static void Prefix(ref Pawn pawn, ref Hediff_Injury injury, out LPIData __state)
        {
            __state = new LPIData(ref pawn, ref injury);
        }

        public static void Postfix(LPIData __state)
        {
            Pawn pawn = __state.Pawn;
            Hediff_Injury injury = __state.Injury;
            bool alertColonist = ((Mod)LoadedModManager.GetMod<LPIMod>()).GetSettings<LPISetting>().alertColonist;
            bool alertPrisoner = ((Mod)LoadedModManager.GetMod<LPIMod>()).GetSettings<LPISetting>().alertPrisoner;
            bool alertPet = ((Mod)LoadedModManager.GetMod<LPIMod>()).GetSettings<LPISetting>().alertPet;
            bool flag = ((Thing)pawn).Faction == Faction.OfPlayer;
            string text = "";
            if (pawn.RaceProps.Animal && flag && alertPet)
            {
                text = "Pet";
            }
            else if (pawn.IsPrisonerOfColony && alertPrisoner)
            {
                text = "Prisoner";
            }
            else if (pawn.IsColonist && flag && alertColonist)
            {
                text = "Colonist";
            }
           
            bool dead = pawn.health.Dead;
            if (!dead && text != "")
            {
                string labelShort = ((Entity)pawn).LabelShort;
                string label = ((Hediff)injury).Part.Label;
                float partHealth = pawn.health.hediffSet.GetPartHealth(((Hediff)injury).Part);
                HediffComp_GetsPermanent val = HediffUtility.TryGetComp<HediffComp_GetsPermanent>((Hediff)(object)injury);
                bool flag2 = false;
                if (val != null)
                {
                    flag2 = val.IsPermanent;
                }
                bool flag3 = HediffUtility.IsPermanent((Hediff)(object)injury);
                if (((Mod)LoadedModManager.GetMod<LPIMod>()).GetSettings<LPISetting>().debug)
                {
                    Log.Message($"LetterPermanentInjury: Pawn: {labelShort}, title: {text}, part: {label}, health: {partHealth} ,isPerm1: {flag2}, isPerm2: {flag3}, isDead: {dead}.");
                }
                if (partHealth <= 0f || flag2 || flag3)
                {
                    TaggedString val2 = "LPI_Letter_Label".Translate(labelShort);
                    TaggedString val3 = "LPI_Letter_Text".Translate(text, labelShort, label);
                    LookTargets val4 = new LookTargets((Thing)(object)pawn);
                    ChoiceLetter val5 = LetterMaker.MakeLetter(val2, val3, LetterDefOf.NegativeEvent, val4, (Faction)null, (Quest)null, (List<ThingDef>)null);
                    Find.LetterStack.ReceiveLetter((Letter)(object)val5, (string)null);
                }
            }
        }
    }
}