using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Shake : MonoBehaviour
{
    public bool start = false;
    public AnimationCurve animCurve;
    private float shakeDuration = 0.5f;

    // Update is called once per frame
    void Update()
    {
        if (PlayerHeal.beingAttacked) {
            PlayerHeal.beingAttacked = false;
            StartCoroutine(CamShake());
        }
    }

    public IEnumerator CamShake(){
        
        Vector3 startCamPos = transform.position;

        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration) {
            elapsedTime += Time.deltaTime;
            float strength = animCurve.Evaluate(elapsedTime / shakeDuration);
            transform.position = startCamPos + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.position = startCamPos;
    }
}
