using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 moveDirection;
    public float moveSpeed;
    public float jumpForce;
    public float gravityScale;

    Camera sceneCamera;

    public Inventory inventory; //static?

    Spell currentSpell = null;
    GameObject spellProjectile = null;
    float spellTimer;
    bool spellIsActive = false; //Set this to true if a timed spell is performed.


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (inventory == null) {
            Debug.Log("Inventory created");
            //inventory = new Inventory();
        }
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = new Vector3(-1*Input.GetAxis("Horizontal") * moveSpeed, 0f, -1*Input.GetAxis("Vertical") * moveSpeed);

        //Gravity
        moveDirection.y = moveDirection.y + (gravityScale * Physics.gravity.y);
        controller.Move(moveDirection * Time.deltaTime);

        // Z brings up the spell menu.
        if(Input.GetKeyDown("z"))
        {
            Time.timeScale = 0.05f;
            inventory.bringUpSpellMenu();
        }
        if (Input.GetKeyUp("z")) {
            Time.timeScale = 1;
            inventory.quitSpellMenu();
        }

        /*
         *  This is when we have activated a spell that is meant to
         *  e.g shoot projectiles. We need to activate it inside the Spell
         *  object and then this character can cast the spell several times.
         */
        if (Input.GetKeyDown("space")) {
            //Debug.Log(currentSpell);
            if (spellTimer > 0)
            {
                Debug.Log("hej");
                shootProjectile();
            }
        }

        if(spellTimer > 0)
        {
            spellTimer -= Time.deltaTime;
            //Decrease the time.
            if(spellTimer <= 0)
            {
                turnOffSpell();
            }
        }

        transform.rotation = Camera.main.transform.rotation;
        //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, transform.localEulerAngles.z);

        //Vector3 forward = Camera.main.transform.forward;
        //forward.y = 0;
        //forward.Normalize();
        //Vector3 right = Camera.main.transform.right;
        //right.y = 0;
        //right.Normalize();
        //transform.position += Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed * right + Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed * forward;

    }

    public Inventory getInventory()
    {
        return inventory;
    }

    /*
     *  Not sure why you would want this, but let's keep it here anyways.
     */
    public void setCurrentSpell(Spell s)
    {
        currentSpell = s;
    }

    public void setCurrentProjectile(GameObject s)
    {
        spellProjectile = s;
    }

    public void setSpellTimer(float t)
    {
        spellTimer = t;
    }

    void shootProjectile()
    {
        if(spellProjectile != null)
        {
            Instantiate(spellProjectile);
        }
    }

    void turnOffSpell()
    {
        currentSpell = null;
        spellTimer = -1;
        spellProjectile = null;
    }
}
