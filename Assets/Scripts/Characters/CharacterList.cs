using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterList", menuName = "Character/Create Character List", order = 2)]
public class CharacterList : ScriptableObject
{
    public CharacterTracker[] CharacterTrackers;
}
[System.Serializable]
public struct CharacterTracker
{
    public string Name;
    public Character CharacterInfo;
    public bool Unlocked;
    [Range(0, 3)]
    public int CharacterState;
    [HideInInspector] public List<string> CompletedAsks;
    public bool Alert;
}
