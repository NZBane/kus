using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // need for using Image type

public class Stat : MonoBehaviour {

    private Image content; //references to Image properties on unity
    [SerializeField]
    private Text statValue;
    [SerializeField]
    private float lerpSpeed;
    private float currentFill;
    public float MyMaxValue { get; set; }
    private float currentValue;

    public float MyCurrentValue //used for other script access
    {
        get
        {
            return currentValue;
        }

        set
        {
            if(value > MyMaxValue) //makes sure current value of health never goes over the set max value
            {
                currentValue = MyMaxValue;
            }
            else if (value < 0)  //fixes problem when damage of enemy causes the players health to be less than 0 and it wont trigger death.
            {
                currentValue = 0; 
            }
            else
            {
                currentValue = value;
            }
            
            currentFill = currentValue / MyMaxValue;
            if(statValue != null)
            {
                statValue.text = currentValue + " / " + MyMaxValue; // for health and mana bar text
            }

            
        }


    }

   


	// Use this for initialization
	void Start () {
    
        content = GetComponent<Image>();
      
	}
	
	// Update is called once per frame
	void Update () {

        HandleBar();
	}

    public void Initialize(float currentValue, float maxValue) //initialize health and mana
    {
        if(content == null)
        {
            content = GetComponent<Image>();
        }
        MyMaxValue = maxValue;
        MyCurrentValue = currentValue;
        content.fillAmount = MyCurrentValue / MyMaxValue; //fix bug were the health bar of enemies plays a "fill up" animation when switching targets from a dead one to an alive one.
    }

    private void HandleBar()
    {
        if (currentFill != content.fillAmount)
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount, currentFill, Time.deltaTime * lerpSpeed); //so animation moves equally on all different computers
        }
    }
}
