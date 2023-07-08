using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KingGame;

public class PrefabsControllerSO : ScriptableObject
{
    [SerializeField] private List<PrefabsDict> _gamePrefabs;

    public List<PrefabsDict> GamePrefabs => _gamePrefabs;

    public GameObject GetPrefab(PrefabKey key)
    {
        return _gamePrefabs.FirstOrDefault(g => g.key == key).prefab;
    }
}
