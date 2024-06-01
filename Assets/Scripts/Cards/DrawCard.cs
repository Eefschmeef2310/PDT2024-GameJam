using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCard : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform playerHand;
    public void Draw() {
        if(playerHand.childCount < 10) {
            Instantiate(cardPrefab, playerHand);
        }
    }
}
