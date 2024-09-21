using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMouvement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    public float _speed = 5.0f;
   [SerializeField] private float _turnSpeed = 360;
    [SerializeField] private GameObject prefab; 
    private Vector3 _input;



    //Test limite bombe
    public int limiteBombe = 3;

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
    }

    private void PlaceBomb()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(prefab, transform.position, transform.rotation);
        }
    }

}
