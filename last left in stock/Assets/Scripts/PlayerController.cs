using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]GameObject[] Obstacles;
    [SerializeField]GameObject[] boxes;

    [SerializeField]bool readyToMove;
    public bool isPlayer1;
    Vector2 moveInput;
    Vector2 startPos;
    // Start is called before the first frame update
    void Start()
    {
        Obstacles = GameObject.FindGameObjectsWithTag("Obstacles");
        boxes = GameObject.FindGameObjectsWithTag("box");
        startPos = transform.position;
    }

    public void ReCallObjects()
    {
        Obstacles = GameObject.FindGameObjectsWithTag("Obstacles");
        boxes = GameObject.FindGameObjectsWithTag("box");
    }

    public void SetResetPos()
    {
        startPos = transform.position;
    }

    public void ResetPos()
    {
        transform.position = startPos;
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayer1)
        {
             moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
        else
        {
             moveInput = new Vector2(Input.GetAxisRaw("Horizontal2"), Input.GetAxisRaw("Vertical2"));
        }
        moveInput.Normalize();
        if(moveInput.sqrMagnitude > 0.5)
        {
            if(readyToMove)
            {
                Move(moveInput);
                readyToMove = false;
            }
        }
        else
        {
            readyToMove = true;
        }
    }

    public bool Move(Vector2 direction)
    {
        if(Mathf.Abs(direction.x) < 0.5)
        {
            direction.x = 0;
        }
        else
        {
            direction.y = 0;
        }
        direction.Normalize();
        if(Blocked(transform.position, direction))
        {
            return false;
        }
        else
        {
            transform.Translate(direction);
            return true;
        }
    }

    public bool Blocked(Vector3 position, Vector2 direction)
    {
        Vector2 newPos = new Vector2(position.x, position.y) + direction;
        foreach(var obj in Obstacles)
        {
            if(obj.transform.position.x == newPos.x && obj.transform.position.y == newPos.y)
            {
                return true;
            }
        }

         foreach(var box in boxes)
        {
            if(box.transform.position.x == newPos.x && box.transform.position.y == newPos.y)
            {
                Push objpush = box.GetComponent<Push>();
                if(box && objpush.Move(direction))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        return false;
    }
}
