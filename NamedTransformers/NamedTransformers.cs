using Harmony;
using System;

namespace NamedTransformers
{
    // Handles naming transformers
    [HarmonyPatch(typeof(PowerTransformer), "OnSpawn")]
    public class PowerTransformerOnSpawn_Patch
    {
        // Transformer name strings from the prefabs for localisation
        public static string TransformerName = STRINGS.BUILDINGS.PREFABS.POWERTRANSFORMER.NAME;
        public static string TransformerSmallName = STRINGS.BUILDINGS.PREFABS.POWERTRANSFORMERSMALL.NAME;

        public static void Postfix(PowerTransformer __instance)
        {
            SetTransformerName(__instance);
        }

        // Sets the name of the transformer
        public static void SetTransformerName(PowerTransformer transformer)
        {
            {
                KSelectable selectable = transformer.GetComponent<KSelectable>();
                if (selectable != null)
                {
                    string transformerName = selectable.name;
                    {
                        transformerName = TransformerName + " " + TransformerCounter.IncrementTransformerCount();
                    }
                    selectable.SetName(transformerName);
                }
            }
        }

        // Class to handle numbering of the transformers
        public static class TransformerCounter
        {
            public static int TransformerCount = 1;

            public static int IncrementTransformerCount()
            {
                return TransformerCount++;
            }
        }

        
        // Returns true if the transformer name passed in is for the big or small transformer
        public static Boolean IsTransformer(string name)
        {
            return name.Equals(TransformerName) || name.Equals(TransformerSmallName);
        }
    }
}