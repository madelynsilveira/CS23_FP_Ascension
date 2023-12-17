using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public List<Transform> targets; //targets
    public Transform platform;
    int currentTarget = 0; 
    public float speed = 5f;  

    void Start() {
    }

    void Update()
    {
        moveToTarget();
    }

    private void moveToTarget()
    {
        if (targets.Count > 0) {
            // go to target
            platform.position = Vector2.MoveTowards(platform.position, targets[currentTarget].position, speed * Time.deltaTime);

            // if you are close enough to target, switch target
            if (Vector2.Distance(platform.position, targets[currentTarget].position) < 0.2f) {
                if (currentTarget == targets.Count - 1) {
                    currentTarget = 0;
                } else {
                    currentTarget++;
                }
            }
        }
    }
}
