using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private Player player;

    private NPC currentTarget;
	
	// Update is called once per frame
	void Update ()
    {
        ClickTarget();
        //Debug.Log(LayerMask.GetMask("Clickable"));
	}

    private void ClickTarget()
    {
        if( Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) //&& !EventSystem.current.IsPointerOverGameObject())//index 0 is left click, checks if mouse is hovering over game object which fixes bug were buttons instead detargets currently targeted enemy before being able to be click
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector2.zero,Mathf.Infinity,512); //for clickable tag

            //check if raycast collider hit has target and checks if its the object has Enemy Tag
            if (hit.collider != null)
            {
                if (currentTarget != null)
                {
                    currentTarget.DeSelect();
                }
                currentTarget = hit.collider.GetComponent<NPC>();

                player.MyTarget = currentTarget.Select();

                UIManager.MyInstance.ShowTargetFrame(currentTarget);

            }
            else //else deselect the target
            {
                UIManager.MyInstance.HideTargetFrame(); //hides target frame
                if (currentTarget != null) //deselects current target if another target is seleted
                {
                    currentTarget.DeSelect();
                }
                currentTarget = null;
                player.MyTarget = null;
            }
           
            


        }
    }
}
