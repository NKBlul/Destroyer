using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    public SpriteRenderer wireEnd;
    public GameObject lightOn;
    private Camera minigameCam;

    Vector3 startPoint;
    Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.parent.position;
        startPosition = transform.position;
        minigameCam = ChangeCamera.Instance.cameras[3];       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDrag()
    {
        // mouse position to world point
        //Vector3 newPos = Camera.main.ScreenToWorldPoint( Input.mousePosition );
        Vector3 newPos = minigameCam.ScreenToWorldPoint(Input.mousePosition);
        newPos.z = 0;

        //check for nearby connection points
        Collider2D[] colliders = Physics2D.OverlapCircleAll(newPos, 0.2f);
        foreach (Collider2D collider in colliders)
        {
            // make sure not my own collider
            if (collider.gameObject != gameObject)
            {
                //update wire to the connection point position
                UpdateWire(collider.transform.position);

                //check if wires are same color
                if (transform.parent.name.Equals(collider.transform.parent.name))
                {
                    //count wire connected
                    Minigame.Instance.WireConnected(1);

                    //finish step
                    collider.GetComponent<Wire>()?.Connected();
                    Connected();
                }

                return;
            }
        }

        //update wire
        UpdateWire(newPos);
        
    }

    void Connected()
    {
        // turn on light
        lightOn.SetActive(true);

        //destroy script
        Destroy(this);
    }

    private void OnMouseUp()
    {
        //reset wire position
        UpdateWire(startPosition);
    }

    void UpdateWire(Vector3 newPos)
    {
        //update position
        transform.position = newPos;

        //update direction
        Vector3 direction = newPos - startPoint;
        transform.right = direction * transform.lossyScale.x;

        //update scale
        float dist = Vector2.Distance(startPoint, newPos);
        wireEnd.size = new Vector2(dist, wireEnd.size.y);
    }
}
