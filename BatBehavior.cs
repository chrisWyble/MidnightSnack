using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBehavior : MonoBehaviour
{
    public Camera batView;
    public float batSpeed = 1;
    public float maxHunger = 100;
    private float currentHunger;

    public float PositionUpdateTime = 0.2f;
    public float mouseSpeed = 2.0f;
    float xDelta;
    float zDelta;
    // Start is called before the first frame update
    void Start()
    {

        currentHunger = maxHunger;
        xDelta = 0;
        zDelta = 0;
        Invoke("PositionUpdate", PositionUpdateTime);
    }

    // Update is called once per frame
    void Update()
    {
        float h;
        float angle;

        h = mouseSpeed * Input.GetAxis("Mouse X");
        transform.Rotate(0, h, 0);

        angle = Mathf.Deg2Rad*transform.rotation.eulerAngles.y;
        xDelta = batSpeed * Mathf.Sin(angle);
        zDelta = batSpeed * Mathf.Cos(angle);
    }

    void PositionUpdate()
    {
        Vector3 holdPos;

        holdPos = transform.position;
        holdPos.z += zDelta;
        holdPos.x += xDelta;
        transform.position = holdPos;

        Invoke("PositionUpdate", PositionUpdateTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("collision");
        if (collision.gameObject.name == "Leaf (13)") {
            print("collision");
            Vector3 holdPos;

            holdPos = transform.position;
            holdPos.z -= zDelta;
            holdPos.x -= xDelta;
            transform.position = holdPos;
        }

    }
}
