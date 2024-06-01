using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPlayerStats : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI health;
    [SerializeField] TextMeshProUGUI shield;
    [SerializeField] TextMeshProUGUI experience;

    [SerializeField] BaseCharacterScript player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        health.text = "Health: " + player.Health;
        shield.text = "Shield: " + player.Shield;
        experience.text = "EXP: " + 0;
    }
}
