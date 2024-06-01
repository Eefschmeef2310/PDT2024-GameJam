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
    [SerializeField] TextMeshProUGUI LevelUpTowerText;

    [SerializeField] PlayerScript player;
    [SerializeField] TowerManager towerManager;

    [SerializeField] GameObject PlayerLevelUI;
    [SerializeField] GameObject ChoosingTowerUI;

    private Camera mainCamera;
    private bool isLeveling = false;

    // Start is called before the first frame update
    void Start()
    {
        PlayerLevelUI.SetActive(false);
        ChoosingTowerUI.SetActive(false);
        SetLevelingTowerState(false);
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        health.text = "Health: " + player.Health;
        shield.text = "Shield: " + player.Shield;
        level.text = "Lvl: " + player.Level;
        experience.text = "Exp: " + player.Experience + "/" + player.MaxExperience;
        if (isLeveling)
        {
            HandleLevelTower();
        }
    }

    private void HandleLevelTower()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            if (hit.collider != null)
            {
                GameObject hitObject = hit.collider.gameObject;
                if (hitObject.CompareTag("Tower"))
                {
                    BaseTower baseTower = hitObject.GetComponent<BaseTower>();
                    if (baseTower != null)
                    {
                        baseTower.LevelTower();
                        SetLevelingTowerState(false);
                        Time.timeScale = 1f;
                    }
                }
            }
        }
    }

    private void SetLevelingTowerState(bool state)
    {
        isLeveling = state;
        LevelUpTowerText.enabled = state;
    }


    // Button functions
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
        SetLevelingTowerState(true);
        PlayerLevelUI.SetActive(false);
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
