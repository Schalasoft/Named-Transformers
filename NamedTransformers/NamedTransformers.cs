using Harmony;

namespace NamedTransformers
{
    // Handles naming transformers
    [HarmonyPatch(typeof(PowerTransformer), "OnSpawn")]
    public class PowerTransformerOnSpawn_Patch
    {
        public static void Postfix(PowerTransformer __instance)
        {
            SetTransformerName(__instance);
        }

        // Sets the name of the transformer
        private static void SetTransformerName(PowerTransformer transformer)
        {
            {
                KSelectable selectable = transformer.GetComponent<KSelectable>();
                if (selectable != null)
                {
                    string transformerName = transformer.GetProperName();

                    transformerName += " " + TransformerCounter.IncrementTransformerCount();

                    selectable.SetName(transformerName);
                }
            }
        }

        // Class to handle numbering of the transformers
        private static class TransformerCounter
        {
            public static int TransformerCount = 1;

            public static int IncrementTransformerCount()
            {
                return TransformerCount++;
            }
        }
    }
}