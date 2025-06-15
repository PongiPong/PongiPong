using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Pong/Character")]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public GameObject characterPrefab;
    public Sprite characterIcon;
}