using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    //singleton is used for when theres only one type of obejct in the game and needed to ben access from other palces in the game
    private static Player instance;


    public static Player MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Player>();
            }
            return instance;
        }
    }

    [SerializeField]
    private Stat mana;


 
      
    private float initMana = 50; //for adjusting mana of player


    private Vector3 min, max;
    
    [SerializeField]
    private Transform[] exitPoints; //for adjusting spell exit point from the character prefab staff

    [SerializeField]
    private Block[] blocks; //line of sight blocker


    //assigned initially to index 2 as player respawns looking down initially
    private int exitIndex = 2; //use to assign rigt direction (will be used for the inputs below)

    // Use this for initialization
    protected override void Start ()
    {
      
        mana.Initialize(initMana, initMana);

       // target = GameObject.Find("Target").transform;//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!


        base.Start();
    }
	
	// Update is called once per frame
	protected override void Update () {
        GetInput();
        //health.MyCurrentValue = 100;

        //Camera clamping onto player
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, min.x, max.x), Mathf.Clamp(transform.position.y, min.y, max.y), transform.position.z);

        base.Update();  //executes Character.cs update|| base = access inheritance
       
        

        
	}
   

    //player direction/movement control
    private void GetInput()
    {
       




        //reset direction after every loop
        Direction = Vector2.zero; //Prevents incrementing speed every loop causing player to run faster and faster.
                                 
        
        ///DEBUGGING ONLY!!!
                                  ///
        if (Input.GetKeyDown(KeyCode.I))
        {
            health.MyCurrentValue -= 10;
            mana.MyCurrentValue -= 10;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            health.MyCurrentValue += 10;
            mana.MyCurrentValue += 10;
        }




        //GetKey = Hold key down
        //GetKeyDown = Toggle mode
        if (Input.GetKey(KeybindManager.MyInstance.Keybinds["UP"]))
        {
            exitIndex = 0;
            Direction += Vector2.up;
        }
        if (Input.GetKey(KeybindManager.MyInstance.Keybinds["LEFT"]))
        {
            exitIndex = 3;
            Direction += Vector2.left;
        }
        if (Input.GetKey(KeybindManager.MyInstance.Keybinds["DOWN"]))
        {
            exitIndex = 2;
            Direction += Vector2.down;
        }
        if (Input.GetKey(KeybindManager.MyInstance.Keybinds["RIGHT"]))
        {
            exitIndex = 1;
            Direction += Vector2.right;
        }
        if (IsMoving)
        {
            StopAttack();
        }

        foreach (string action in KeybindManager.MyInstance.ActionBinds.Keys)
        {
            if(Input.GetKeyDown(KeybindManager.MyInstance.ActionBinds[action]))
            {
                UIManager.MyInstance.ClickActionButton(action);
            }
        }


    }


    //Set limits of where the player can go
    public void SetLimits(Vector3 min, Vector3 max)
    {
        this.min = min;
        this.max = max;
    }
   
    //good for puttign delays
    private IEnumerator Attack(string spellName) //spellindex determines which prefab to use
    {

        Transform currentTarget = MyTarget; //Made current Target to prevent player from swapping target mid cast
        Spell newSpell = SpellBook.MyInstance.CastSpelll(spellName);
        IsAttacking = true;
        MyAnimator.SetBool("attack", IsAttacking);
        
        yield return new WaitForSeconds(newSpell.MyCastTime); //cast time 
      
       if (currentTarget != null && InLineOfSight()) //fix for the bug where the player can still hit enemy even out of sight once cast time is activated
        {
            SpellScript s = Instantiate(newSpell.MySpellPrefab, exitPoints[exitIndex].position, Quaternion.identity).GetComponent<SpellScript>();
            s.Initialize(currentTarget, newSpell.MyDamage, transform);
        }
       
        StopAttack();
        
    }

    public void CastSpell(string spellName)
    {

        Block();
        //if not attacking and not moving, then the player can attack again
        if (MyTarget != null && MyTarget.GetComponentInParent<Character>().IsAlive && !IsAttacking && !IsMoving && InLineOfSight())
        {
            //coroutine = an action that can be done simoultaenously with other functions
            attackRoutine = StartCoroutine(Attack(spellName));

        }
        //quaternion = rotation of the prefab
        //instantiate the spell prefab on the position of the exit points which are determined by the inputs
        
       
    }
    
    //draw line of sight that points to targeted enemy
    private bool InLineOfSight()
    {
        if(MyTarget !=null)
        {
            Vector3 targetDirection = (MyTarget.transform.position - transform.position).normalized;
            Debug.DrawRay(transform.position, targetDirection, Color.red);

            //if collider hits block then cant cast spell
            RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection, Vector2.Distance(transform.position, MyTarget.transform.position), 256);

            if (hit.collider == null)
            {
                return true;
            }
        }
       
        return false;
    }


    //method for preventing player to cast spell when not in line of sight
    private void Block()
    {
        foreach(Block i in blocks)
        {
            i.Deactivate();

        }
        blocks[exitIndex].Activate(); //exitIndex keeps track of direction player is facing
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "SwordCollider")
        {

            health.MyCurrentValue -= 5;
            if (health.MyCurrentValue <= 0)
            {
                Direction = Vector2.zero;

             MyAnimator.SetTrigger("die");

              //  Application.Quit(); //works
                //////////////////INSERT GAME OVER SCENE HERE!!!!!!!!!//////////////////////////////(but since enemy is using this, it will also trigger this event when enemy dies)
                //character dies
            }
        }

    }


    public void StopAttack()
    {
        SpellBook.MyInstance.StopCasting();
        IsAttacking = false;
        MyAnimator.SetBool("attack", IsAttacking);
        if (attackRoutine != null)
        {
            StopCoroutine(attackRoutine);

        }

    }

    

}
