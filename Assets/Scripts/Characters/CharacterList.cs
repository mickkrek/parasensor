using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterList", menuName = "Character/Create Character List", order = 2)]
public class CharacterList : ScriptableObject
{
    public CharacterTracker[] characterTrackers;
}
[System.Serializable]
public struct CharacterTracker
{
    public Character character;
    public int currentState;
}
