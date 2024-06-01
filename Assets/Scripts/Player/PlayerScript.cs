using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : BaseCharacterScript
{
    private int playerLevel = 1;

    [SerializeField] private int experience = 0;
    [SerializeField] private int maxExperience = 5;
    [SerializeField] private UIPlayer playerUI;

    public int Level { get { return playerLevel; } }
    public int Experience { get { return experience; } }
    public int MaxExperience { get { return maxExperience; } }

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void AddExperience()
    {
        experience++;
        HandleExperience();
    }

    private void HandleExperience()
    {
        if (experience >= maxExperience)
        {
            Time.timeScale = 0;
            PlayerLevelUp();
            playerUI.PlayerLevelUpUI();
        }
    }

    private void PlayerLevelUp()
    {
        playerLevel++;
        experience = 0;
    }
}
