using System.Reflection;
using HarmonyLib;
using Verse;

namespace LetterPermanentInjury
{
    [StaticConstructorOnStartup]
    public class LPIPatcher
    {
        static LPIPatcher()
        {
            Harmony val = new Harmony("Buggy.LetterPermanentInjury");
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            val.PatchAll(executingAssembly);
        }
    }
}