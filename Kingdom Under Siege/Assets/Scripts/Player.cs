using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
   

    [SerializeField]
    private Stat mana;

    //player's target
    public Transform MyTarget { get; set; }

 
      
    private float initMana = 50; //for adjusting mana of player

    private SpellBook spellBook;

  

    [SerializeField]
    private Transform[] exitPoints; //for adjusting spell exit point from the character prefab staff

    [SerializeField]
    private Block[] blocks; //line of sight blocker


    //assigned initially to index 2 as player respawns looking down initially
    private int exitIndex = 2; //use to assign rigt direction (will be used for the inputs below)

    // Use this for initialization
    protected override void Start ()
    {
        spellBook = GetComponent<SpellBook>();
      
        mana.Initialize(initMana, initMana);

       // target = GameObject.Find("Target").transform;//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!


        base.Start();
    }
	
	// Update is called once per frame
	protected override void Update () {
        GetInput();
        //health.MyCurrentValue = 100;
        
        base.Update();  //executes Character.cs update|| base = access inheritance
       
        

        
	}
   

    //player direction/movement control
    private void GetInput()
    {
       




        //reset direction after every loop
        direction = Vector2.zero; //Prevents incrementing speed every loop causing player to run faster and faster.
                                 
        
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
        if (Input.GetKey(KeyCode.W))
        {
            exitIndex = 0;
            direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            exitIndex = 3;
            direction += Vector2.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            exitIndex = 2;
            direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            exitIndex = 1;
            direction += Vector2.right;
        }
      
    }
   
    //good for puttign delays
    private IEnumerator Attack(int spellIndex) //spellindex determines which prefab to use
    {

        Transform currentTarget = MyTarget; //Made current Target to prevent player from swapping target mid cast
        Spell newSpell = spellBook.CastSpelll(spellIndex);
        isAttacking = true;
        myAnimator.SetBool("attack", isAttacking);
        
        yield return new WaitForSeconds(newSpell.MyCastTime); //cast time 
      
       if (currentTarget != null && InLineOfSight()) //fix for the bug where the player can still hit enemy even out of sight once cast time is activated
        {
            SpellScript s = Instantiate(newSpell.MySpellPrefab, exitPoints[exitIndex].position, Quaternion.identity).GetComponent<SpellScript>();
            s.Initialize(currentTarget, newSpell.MyDamage);
        }
       
        StopAttack();
        
    }

    public void CastSpell(int spellIndex)
    {

        Block();
        //if not attacking and not moving, then the player can attack again
        if (MyTarget != null && !isAttacking && !IsMoving && InLineOfSight())
        {
            //coroutine = an action that can be done simoultaenously with other functions
            attackRoutine = StartCoroutine(Attack(spellIndex));

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

    public override void StopAttack()
    {
        spellBook.StopCasting();
        base.StopAttack();
    }

}
