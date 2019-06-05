using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipD List", menuName = "Scriptables/EquipD List", order = 1)]
public class EquipD_List : ScriptableObject
{
    public List<EquipD> helmets, armors, backs, weapons_1H, weapons_2H, shields;
}