using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

//Pulls data from backend cards for display
public class DisplayCard : MonoBehaviour
{
    public Card displayedCard;
    public int displayId;

    public int id;
    public string cardName;
    public int exp;
    public int level;
    public int power;
    public string cardDescription;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI expText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI descText;

    void Start() {
        displayedCard = CardDatabase.cards[Random.Range(0, CardDatabase.cards.Count)];
    }

    void Update() {
        id = displayedCard.id;
        cardName = displayedCard.cardName;
        exp = displayedCard.exp;
        level = displayedCard.level;
        power = displayedCard.power;
        cardDescription = displayedCard.cardDescription;

        nameText.text = " " + cardName;
        expText.text = " " + exp;
        levelText.text = " " + level;
        descText.text = " " + cardDescription;
    }
}
