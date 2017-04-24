using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Gemserk.ReplacerWizard
{
    public class ReplacerWizardWindow : ScriptableWizard
    {
        public GameObject replacer;
        public bool usePrefab = false;

        [MenuItem("Window/Gemserk/ReplacerWizard")]
        static void CreateWizard()
        {
            ScriptableWizard.DisplayWizard<ReplacerWizardWindow>("Replacer", "Replace");
        }

        void OnWizardCreate()
        {
            var selection = Selection.gameObjects.ToList();
            foreach (var selectedGO in selection)
            {
                GameObject newGO;

                if (!usePrefab)
                {
                    newGO = GameObject.Instantiate(replacer, selectedGO.transform.parent);
                }
                else
                {
                    newGO = PrefabUtility.InstantiatePrefab(replacer) as GameObject;
                    newGO.transform.SetParent(selectedGO.transform.parent);
                }

                newGO.transform.localPosition = selectedGO.transform.localPosition;
                newGO.name = selectedGO.name;
                GameObject.DestroyImmediate(selectedGO, true);
            }
        }

        void OnWizardUpdate()
        {

        }
    }
}