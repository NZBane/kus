using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellBook : MonoBehaviour {

    [SerializeField]
    private Spell[] spells;

    [SerializeField]
    private Image castingBar;

    [SerializeField]
    private Image icon;

    [SerializeField]
    private Text spellName;

    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private Text castTime;

    private Coroutine spellRoutine;
    private Coroutine fadeRoutine;


    public Spell CastSpelll(int index)
    {
        castingBar.fillAmount = 0;

        castingBar.color = spells[index].MyBarColor;

        spellName.text = spells[index].MyName;

        icon.sprite = spells[index].MyIcon;
        spellRoutine = StartCoroutine(Progress(index));
        fadeRoutine = StartCoroutine(FadeBar());
        return spells[index];
    }

    private IEnumerator Progress(int index)
    {
        float timePassed = Time.deltaTime;
        float rate =  1.0f / spells[index].MyCastTime; //maximum divided by cast time
        float progress = 0.0f;
        while (progress <= 1.0) //while less than 1 move bar from left to right
        {
            castingBar.fillAmount = Mathf.Lerp(0, 1, progress);
            progress += rate * Time.deltaTime;
            timePassed += Time.deltaTime;
            castTime.text = (spells[index].MyCastTime - timePassed).ToString("F2");//cast time number
            if (spells[index].MyCastTime - timePassed < 0)
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
}
