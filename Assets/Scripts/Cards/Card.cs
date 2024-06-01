using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Card
{
    public int id;
    public string cardName;
    public int exp;
    public int level;
    public int power;
    public string cardDescription;

    //Constructors
    public Card() {

    }

    public Card(int Id, string CardName, int Exp, int Level, int Power, string CardDescription) {
        id = Id;
        cardName = CardName;
        exp = Exp;
        level = Level;
        power = Power;
        cardDescription = CardDescription;
    }
}
