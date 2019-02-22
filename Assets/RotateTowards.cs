using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowards : MonoBehaviour
{
    public float RotationSpeed = 5;
    public GameObject Player;

    Vector2 target;

    // Update is called once per frame
    void Update()
    {
        target = Player.transform.position;
        Vector2 position = gameObject.transform.position;
        Vector2 direction = target - position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, RotationSpeed * Time.deltaTime);
    }
}
