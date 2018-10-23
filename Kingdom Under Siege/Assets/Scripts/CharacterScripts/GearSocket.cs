using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearSocket : MonoBehaviour {

    public Animator MyAnimator { get; set; }

    protected SpriteRenderer spriteRenderer;

    private Animator parentAnimator;

    private AnimatorOverrideController animatorOverrideController;

    private void Awake()
    {
        //OVERRIDE ANIMATIONS
        spriteRenderer = GetComponent<SpriteRenderer>();
        parentAnimator = GetComponentInParent<Animator>();
        MyAnimator = GetComponent<Animator>();

        animatorOverrideController = new AnimatorOverrideController(MyAnimator.runtimeAnimatorController);
        MyAnimator.runtimeAnimatorController = animatorOverrideController;
    }
       

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void SetXAndY(float  x, float y)
    {
        MyAnimator.SetFloat("x", x);
        MyAnimator.SetFloat("y", y);
    }


    public void ActivateLayer(string layerName)
    {
        for (int i = 0; i < MyAnimator.layerCount; i++)
        {
            MyAnimator.SetLayerWeight(i, 0);
        }
        MyAnimator.SetLayerWeight(MyAnimator.GetLayerIndex(layerName), 1);
    }

    public void Equip(AnimationClip[] animations)
    {
        spriteRenderer.color = Color.white;
        animatorOverrideController["wizard_attack_back"] = animations[0];
        animatorOverrideController["wizard_attack_front"] = animations[1];
        animatorOverrideController["wizard_attack_left"] = animations[2];
        animatorOverrideController["wizard_attack_right"] = animations[3];

        animatorOverrideController["wizard_idle_back"] = animations[4];
        animatorOverrideController["wizard_idle_front"] = animations[5];
        animatorOverrideController["wizard_idle_left"] = animations[6];
        animatorOverrideController["wizard_idle_right"] = animations[7];

        animatorOverrideController["wizard_walk_back"] = animations[8];
        animatorOverrideController["wizard_walk_front"] = animations[9];
        animatorOverrideController["wizard_walk_left"] = animations[10];
        animatorOverrideController["wizard_walk_right"] = animations[11];

    }
    public void Dequip()
    {
        animatorOverrideController["wizard_attack_back"] = null;
        animatorOverrideController["wizard_attack_front"] = null;
        animatorOverrideController["wizard_attack_left"] = null;
        animatorOverrideController["wizard_attack_right"] = null;

        animatorOverrideController["wizard_idle_back"] = null;
        animatorOverrideController["wizard_idle_front"] = null;
        animatorOverrideController["wizard_idle_left"] = null;
        animatorOverrideController["wizard_idle_right"] = null;

        animatorOverrideController["wizard_walk_back"] = null;
        animatorOverrideController["wizard_walk_front"] = null;
        animatorOverrideController["wizard_walk_left"] = null;
        animatorOverrideController["wizard_walk_right"] = null;

        Color c = spriteRenderer.color;
        c.a = 0;
        spriteRenderer.color = c;

    }

}
