using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class CharacterMouvement : MonoBehaviour
{
    
    private Rigidbody _rb;
    public float _speed = 5.0f;
   [SerializeField] private float _turnSpeed = 360;
    [SerializeField] private GameObject bombPrefab; 
    private Vector3 _input;



    //Test limite bombe
    public int limiteBombe = 3;
    public int currentBombe;
    public int compteurBombe = 0;

    //public bool canCrit;


    //Audio
    //public AudioClip bombEnter;
    public float volume;
    public AudioSource bombEnterrement;
    public float waitSound;


    //Animation
    Animator anim;
    public GameObject perso;
    Animator bombAnim;
    public GameObject bomb;
    public bool critPossible;
    public bool noBomb;


    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        currentBombe = limiteBombe;
        anim = perso.GetComponent<Animator>();
        bombAnim = bomb.GetComponent<Animator>();
        critPossible = false;
        noBomb = false;
        //canCrit = false;
    }
    
    private void Update()
    {
        GatherInput();
        Look();
        PlaceBomb();


        if(currentBombe == 0)
        {
            noBomb = true;
        }
        else
        {
            noBomb = false;
        }



        if (critPossible == true && noBomb == false)
        {
            bombAnim.SetBool("BombCrit", true);
            bombAnim.SetBool("NoBomb", false);

        }
        else if (noBomb == true && critPossible == false)
        {

            bombAnim.SetBool("BombCrit", false);
            bombAnim.SetBool("NoBomb", true);
        }
        else
        {
            bombAnim.SetBool("BombCrit", false);
            bombAnim.SetBool("NoBomb", false);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }


    void Look()
    {

        if(_input != Vector3.zero) 
        {
          
            //Iso up and down
           //var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));

            //var skewedInput = matrix.MultiplyPoint3x4(_input);


            //Normal uo and down
            var relative = (transform.position + _input) - transform.position;

            //Iso up and down
            //var relative = (transform.position + skewedInput) - transform.position;

            var rot = Quaternion.LookRotation(relative, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _turnSpeed * Time.deltaTime);
        }

;    }
    void GatherInput()
    {
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
    }


    private void Move()
    {
        //_rb.MovePosition(transform.position + (transform.forward * _input.magnitude) * _speed * Time.deltaTime);

        _rb.MovePosition(transform.position +_input * _speed * Time.deltaTime);


   



        //test bombAdd
        //if(Input.GetKeyDown(KeyCode.B))
        //{
        //    BombAdd();
        //}
    }

    private void PlaceBomb()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            
            if (currentBombe > 0)
            {

                StartCoroutine(BombPlacement());
                //bombEnterrement.PlayOneShot(bombEnterrement.clip, volume);


                //compteurBombe++;
                //currentBombe--;

                //var gameObject = Instantiate(bombPrefab, transform.position, transform.rotation);

                //if (CanCrit())
                //{
                //    var param = GlobalBombParam.Instance;
                //    gameObject.GetComponent<ExplosionHandler>().ApplyCritStatus(param.critBlastRadius, param.critBlastForce, param.critDeadZoneRadius);

                 
                //}
            }
            //var gameObject = Instantiate(bombPrefab, transform.position, transform.rotation);

            //if (CanCrit())
            //{
            //    var param = GlobalBombParam.Instance;
            //    gameObject.GetComponent<ExplosionHandler>().ApplyCritStatus(param.critBlastRadius, param.critBlastForce, param.critDeadZoneRadius);


            //}


        }
    }


    private IEnumerator BombPlacement()
    {
        anim.SetTrigger("Placement");

      



        if(critPossible == true && noBomb == false)
        {
            bombAnim.SetTrigger("CritPlanted");
        }
        else if (critPossible == false && noBomb == false)
        {
            bombAnim.SetTrigger("SmallPlanted");
        }


        bombEnterrement.PlayOneShot(bombEnterrement.clip, volume);

        yield return new WaitForSeconds(waitSound);

        compteurBombe++;
        currentBombe--;

        var gameObject = Instantiate(bombPrefab, transform.position, transform.rotation);

      
        if (CanCrit())
        {
            
            var param = GlobalBombParam.Instance;
            gameObject.GetComponent<ExplosionHandler>().ApplyCritStatus(param.critBlastRadius, param.critBlastForce, param.critDeadZoneRadius);
            yield return new WaitForSeconds(0.1f);
            critPossible = false;
            //canCrit = false;
        }
    }

    public void BombAdd()
    {

        if(currentBombe < limiteBombe)
        {
            currentBombe++;
        }

    }


    private bool CanCrit()
    {
        if(compteurBombe % 3 == 0)
        {
            critPossible = true;
            return true;
        }
        return false;
    }

}
