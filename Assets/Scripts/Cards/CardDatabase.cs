using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    //Keeps track of all possible cards that can be created
    public static List<Card> cards = new List<Card>();

    void Awake() 
    {
        cards.Add(new Card(0, "FireRateUp", 0, 1, 1, "Increase Fire Rate"));
        cards.Add(new Card(1, "PowerUp", 0, 1, 1, "Increase Power"));
        cards.Add(new Card(2, "RangeUp", 0, 1, 1, "Increase Range"));
    }
}
