using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using danielnyan;

public class SkillDispatcher : MonoBehaviour
{
    public enum Categories
    {
        Throwable, Straight, Aimed, Continuous
    }
    public static string[] description = new string[4]
    {
        "Left click to throw a projectile in a fixed parabola!",
        "Left click to fire a projectile in the direction your character is facing!",
        "Left click to fire a projectile at your mouse's position!",
        "Left click and hold to continue casting!"
    };

    public Categories category = 0;
    public int index = 0;
    public TextMeshProUGUI categoryText;
    public TextMeshProUGUI categoryDescription;
    public SkillCategory[] skillCategories;

    private WitchMovement player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<WitchMovement>();
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        skillCategories[(int)category].HandleInput(player, index);
        HandleSkillSwitch();
    }

    public void HandleSkillSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            int newCategory = (int)category - 1;
            if (newCategory < 0)
            {
                newCategory += 4;
            }
            category = (Categories)newCategory;
            index = 0;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            category = (Categories)(((int)category + 1) % 4);
            index = 0;
        }
        int maxSize = skillCategories[(int)category].skills.Length;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            index = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && maxSize > 1)
        {
            index = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && maxSize > 2)
        {
            index = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && maxSize > 3)
        {
            index = 3;
        }

        if (Input.anyKeyDown)
        {
            UpdateUI();
        }
    }

    public void UpdateUI()
    {
        categoryText.text = "Category: " + category.ToString() + "\n" +
            "Index: " + index.ToString();
        categoryDescription.text = description[(int)category];
    }
}
