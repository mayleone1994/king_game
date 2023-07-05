using KingGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PrefabsController : MonoBehaviour
{
    // EDITOR REFERENCE:

    [SerializeField] private List<PrefabsDict> _gamePrefabs;

    public List<PrefabsDict> GamePrefabs => _gamePrefabs;

    public GameObject GetPrefab(PrefabKey key)
    {
        return _gamePrefabs.FirstOrDefault(g => g.key == key).prefab;
    }
}
