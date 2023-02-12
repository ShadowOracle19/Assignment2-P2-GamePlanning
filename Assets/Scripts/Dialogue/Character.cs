using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "ScriptableObjects/CharacterCreator")]
public class Character : ScriptableObject
{
    public string fullName;
    public Sprite neutral;

    public Sprite happy;
    public Sprite sad;
    public Sprite angry;
    public Sprite suprised;

    public Sprite namePlate;
}
