using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleChar : MonoBehaviour
{
    public bool isPlayer;
    public string[] movesAviable;

    public string charName;
    public int currentHP, maxHP, currentMP, maxMP, strength, defence, wpnPower, armrPower;
    public bool hasDie;

    public SpriteRenderer theSprite;
    public Sprite deadSprite, aliveSprite;

    public bool shouldFade;
    public float fadeSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFade)
        {
            theSprite.color = new Color(0, 0, 0, 0);
            gameObject.SetActive(false);
        }
    }

    public void EnemyFade()
    {
        shouldFade = true;
    }
}
