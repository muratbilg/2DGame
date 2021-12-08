using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    [SerializeField] private float speed = 5 ;
    private float horizontalInput;
    private float verticalInput;
    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
         horizontalInput = Input.GetAxis("Horizontal");
         verticalInput = Input.GetAxis("Vertical");
    }
    // Update is called once per frame
    void FixedUpdate()
    {
       
        gameObject.transform.Translate(Vector2.right * horizontalInput * speed * Time.deltaTime);

        
        gameObject.transform.Translate(Vector2.up * verticalInput * speed * Time.deltaTime);
    }
}
