using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Racing
{
    public class RaceSeekerForMapCompletion : MonoBehaviour
    {
        [SerializeField] private Transform parent;
        private MapCompletion mapCompletion;
        private SpawnObjectByPropertiesList[] raceSpawner;
        private ScriptableObject[] properties;

        [ContextMenu(nameof(SeekRaces))]
        public void SeekRaces()
        {
            if (Application.isPlaying == true) return;

            mapCompletion = GetComponent<MapCompletion>();
            GameObject[] allObjects = new GameObject[parent.childCount];


            for (int i = 0; i < parent.childCount; i++)
            {
                allObjects[i] = parent.GetChild(i).gameObject;
                raceSpawner[i] = allObjects[i].GetComponent<SpawnObjectByPropertiesList>();
                properties = raceSpawner[i].GetPropreties();
                for (int j = 0; j< properties.Length; j++)
                {
                    if(properties[j] is RaceInfo == true) 
                        mapCompletion.SetCompletionData(properties[j] as RaceInfo);
                }
            }
        }
    }
}