using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCreatorController : MonoBehaviour
{
    [SerializeField] private List<Monster> monsters;
    private Dictionary<string, Monster> _recipes;
    private GameObject _currentMonster;

    [SerializeField] private Transform monsterTarget;
    private void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        _recipes = new Dictionary<string, Monster>();

        foreach (var monster in monsters)
        {
            _recipes[monster.monsterID] = monster;
        }
    }

    public void CreateMonster(string recipe, Action destroyAction)
    {
        if (_currentMonster != null)
        {
            Destroy(_currentMonster);
        }

        if (_recipes.TryGetValue(recipe, out var monster))
        {
            _currentMonster = Instantiate(monster.monsterPrefab, monsterTarget);
            _currentMonster.GetComponent<DraggableMonster>().OnDestroyEvent += destroyAction;
        }
    }
}
