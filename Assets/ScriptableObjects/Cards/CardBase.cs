using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Rarities {
    Common,
    Rare,
    Legendary
}

[CreateAssetMenu(fileName ="New Card", menuName ="Card")]
public class CardBase : ScriptableObject
{
    [SerializeField] float increaseValue;
    [SerializeField] Rarities rarity;
}
