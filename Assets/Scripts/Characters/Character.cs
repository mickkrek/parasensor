using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Character/Create Character", order = 1)]
public class Character : ScriptableObject
{
    public string codeName;
    public CharacterState[] characterState;
    public string inkHubKnot;
}
[System.Serializable]
public struct CharacterState
{
    public string displayName;
    public Sprite icon;
    public Color bubbleColor;
}