using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // need for using Image type

public class Stat : MonoBehaviour {

    private Image content; //references to Image properties on unity
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
        }


    }

   


	// Use this for initialization
	void Start () {
     MyMaxValue = 100;
        content = GetComponent<Image>();
       //content.fillAmount = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {

        if (currentFill != content.fillAmount)
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount, currentFill, Time.deltaTime * lerpSpeed); //so animation moves equally on all different computers
        }
	}

    public void Initialize(float currentValue, float maxValue) //initialize health and mana
    {
        MyMaxValue = maxValue;
        MyCurrentValue = currentValue;
    }
}
