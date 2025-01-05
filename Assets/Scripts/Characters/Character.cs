using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Character/Create Character", order = 1)]
public class Character : ScriptableObject
{
    public string NickName;
    public string[] Names;
    public GameObject[] Contents;
    public Sprite[] Icons;
}