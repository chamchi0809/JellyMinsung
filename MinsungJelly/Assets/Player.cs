using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Vector2 moveInput;
    public Vector2 clickStartPos;
    public Vector2 mousePos;

    public float moveForce;
    public Rigidbody2D rb;

    public Image clickPointer;
    public Image arrowPointer;

    public CircleCollider2D[] jointCols;
    public bool onGround;
    public LayerMask groundLayer;
    public int jumpCount;
    // Start is called before the first frame update
    void Start()
    {
        arrowPointer.enabled = false;
        clickPointer.enabled = false;

        jointCols = transform.GetComponentsInChildren<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        onGround = false;
        foreach (CircleCollider2D jointcol in jointCols){
            if (jointcol.IsTouchingLayers(groundLayer))
            {
                onGround = true;
                jumpCount = 0;
                break;
            }
        }
        //이밑에서부터 코드 작성
        if (Input.GetMouseButtonDown(0))
        {
            clickStartPos = Input.mousePosition;
            clickPointer.enabled = true;
            clickPointer.rectTransform.position = Input.mousePosition;
            arrowPointer.enabled = true;            
        }
        if (Input.GetMouseButton(0))
        {
            mousePos = Input.mousePosition;
            moveInput = (mousePos - clickStartPos).normalized;
            clickPointer.rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg));

        }
        if (Input.GetMouseButtonUp(0))
        {
            Move();
            arrowPointer.enabled = false;
            clickPointer.enabled = false;
        }
    }
    void Move()
    {
        if (jumpCount < 2)
        {
            rb.AddForce(moveInput * moveForce,ForceMode2D.Impulse);
            jumpCount++;
        }
    }
}
