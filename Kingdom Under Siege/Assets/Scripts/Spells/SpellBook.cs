using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellBook : MonoBehaviour {

    //singleton is used for when theres only one type of obejct in the game and needed to ben access from other palces in the game
    private static SpellBook instance;


    public static SpellBook MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SpellBook>();
            }
            return instance;
        }
    }

    [SerializeField]
    private Spell[] spells;

    [SerializeField]
    private Image castingBar;

    [SerializeField]
    private Image icon;

    [SerializeField]
    private Text currentSpell;

    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private Text castTime;

    private Coroutine spellRoutine;
    private Coroutine fadeRoutine;


    public Spell CastSpelll(string spellName)
    {
        Spell spell = Array.Find(spells, x => x.MyName == spellName);
        castingBar.fillAmount = 0;

        castingBar.color = spell.MyBarColor;

        currentSpell.text = spell.MyName;

        icon.sprite = spell.MyIcon;
        spellRoutine = StartCoroutine(Progress(spell));
        fadeRoutine = StartCoroutine(FadeBar());
        return spell;
    }

    private IEnumerator Progress(Spell spell)
    {
        float timePassed = Time.deltaTime;
        float rate =  1.0f / spell.MyCastTime; //maximum divided by cast time
        float progress = 0.0f;
        while (progress <= 1.0) //while less than 1 move bar from left to right
        {
            castingBar.fillAmount = Mathf.Lerp(0, 1, progress);
            progress += rate * Time.deltaTime;
            timePassed += Time.deltaTime;
            castTime.text = (spell.MyCastTime - timePassed).ToString("F2");//cast time number
            if (spell.MyCastTime - timePassed < 0)
            {
                castTime.text = "0.00"; //set to 0
            }
            yield return null;
        }
        StopCasting(); //reset routine
     }

 

    private IEnumerator FadeBar()
    {
       
        float rate = 1.0f / 0.25f; //maximum divided by cast time
        float progress = 0.0f;
        while (progress <= 1.0) //while less than 1 move bar from left to right
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, progress);
            progress += rate * Time.deltaTime;
            yield return null;
        }
    }

    public void StopCasting()
    {
        if (fadeRoutine != null)
        {
            StopCoroutine(fadeRoutine);
            canvasGroup.alpha = 0;
            fadeRoutine = null;
        }
        if (spellRoutine != null)
        {
            StopCoroutine(spellRoutine);
            spellRoutine = null;
        }
    }

    //get spell method
    public Spell GetSpell(string spellName)
    {
        Spell spell = Array.Find(spells, x => x.MyName == spellName);
        return spell;
    }
}
