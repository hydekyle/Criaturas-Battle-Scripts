using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Block List", menuName = "Scriptables/Bloq List", order = 1)]
public class BloqTable : ScriptableObject
{
    public GameObject suelo;
    public GameObject pared;
    public GameObject pared2;

}

[CreateAssetMenu(fileName = "Candy Sprites", menuName = "Scriptables/Candy Sprites", order = 2)]
public class CandySprites : ScriptableObject
{
    public Sprite red, yellow, blue, green, purple;
}
