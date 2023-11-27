using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerControls : MonoBehaviour
{

    [SerializeField] InputAction movement;
    [SerializeField] float controlSpeed = .5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        movement.Enable();
    }

    void OnDisable()
    {
        movement.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalThrow = movement.ReadValue<Vector2>().x;
        float verticalThrow = movement.ReadValue<Vector2>().y;
       

        float xOffset = horizontalThrow * Time.deltaTime * controlSpeed;
        float yOffset = verticalThrow * Time.deltaTime * controlSpeed;

        float newXPos = transform.localPosition.x + xOffset;
        float newYPos = transform.localPosition.y + yOffset;

        transform.localPosition = new Vector3(newXPos, newYPos, transform.localPosition.z);

        Debug.Log(horizontalThrow);
        Debug.Log(verticalThrow);


        //float horizontalThrow = Input.GetAxis("Horizontal");
        //Debug.Log(horizontalThrow);

        //float verticalThrow = Input.GetAxis("Vertical");
        //Debug.Log(verticalThrow);

    }
}
