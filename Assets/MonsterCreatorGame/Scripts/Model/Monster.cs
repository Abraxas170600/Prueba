using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMonster", menuName = "MonsterCreator/Monster")]
public class Monster : ScriptableObject
{
    public GameObject monsterPrefab;
    public string monsterID;
    public string monsterName;
}
