using UnityEngine;

namespace Racing
{
    public class UIRaceButtonSpawner : MonoBehaviour
    {
        [SerializeField] private Transform parent;
        [SerializeField] private UIRaceButton prefab;
        [SerializeField] private RaceInfo[] properties;

        [ContextMenu(nameof(Spawn))]
        public void Spawn()
        {
            if (Application.isPlaying == true) return;

            GameObject[] allObjects = new GameObject[parent.childCount];

            for (int i = 0; i < parent.childCount; i++)
            {
                allObjects[i] = parent.GetChild(i).gameObject;
            }
            for (int i = 0; i < allObjects.Length; i++)
            {
                DestroyImmediate(allObjects[i]);
            }
            for (int i = 0; i < properties.Length; i++)
            {
                UIRaceButton button = Instantiate(prefab, parent);
                button.ApplyProperty(properties[i]);
            }
        }
    }
}