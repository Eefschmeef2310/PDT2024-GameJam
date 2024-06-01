using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPlayer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI health;
    [SerializeField] TextMeshProUGUI shield;
    [SerializeField] TextMeshProUGUI level;
    [SerializeField] TextMeshProUGUI experience;

    [SerializeField] PlayerScript player;
    [SerializeField] TowerManager towerManager;

    [SerializeField] GameObject PlayerLevelUI;
    [SerializeField] GameObject ChoosingTowerUI;

    // Start is called before the first frame update
    void Start()
    {
        PlayerLevelUI.SetActive(false);
        ChoosingTowerUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        health.text = "Health: " + player.Health;
        shield.text = "Shield: " + player.Shield;
        level.text = "Lvl: " + player.Level;
        experience.text = "Exp: " + player.Experience + "/" + player.MaxExperience;
    }

    public void PlayerLevelUpUI()
    {
        PlayerLevelUI.SetActive(true);
    }

    public void ActivateChooseTowerUI()
    {
        PlayerLevelUI.SetActive(false);
        ChoosingTowerUI.SetActive(true);
    }

    public void LevelUpTower()
    {
        Time.timeScale = 1;
    }

    public void PlaceGenericTowerButton()
    {
        towerManager.CreateGenericTower();
        ChoosingTowerUI.SetActive(false);
    }
    public void PlaceShieldTowerButton()
    {
        towerManager.CreateShieldTower();
        ChoosingTowerUI.SetActive(false);
    }
}
