using System.Collections;
using System.Collections.Generic;
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

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        currentBombe = limiteBombe;
        anim = gameObject.GetComponent<Animator>();
        //canCrit = false;
    }
    
    private void Update()
    {
        GatherInput();
        Look();
        PlaceBomb();
        
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

        bombEnterrement.PlayOneShot(bombEnterrement.clip, volume);

        yield return new WaitForSeconds(waitSound);

        compteurBombe++;
        currentBombe--;

        var gameObject = Instantiate(bombPrefab, transform.position, transform.rotation);

        if (CanCrit())
        {
            var param = GlobalBombParam.Instance;
            gameObject.GetComponent<ExplosionHandler>().ApplyCritStatus(param.critBlastRadius, param.critBlastForce, param.critDeadZoneRadius);

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
            return true;
        }
        return false;
    }

}
