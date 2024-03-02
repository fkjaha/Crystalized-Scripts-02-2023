using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> levelPrefabs;
    [SerializeField] private Transform levelParent;

    private GameObject _level;
    
    public void SpawnLevel(int index)
    {
        index %= levelPrefabs.Count;

        _level = Instantiate(levelPrefabs[index], levelParent);
    }
}
