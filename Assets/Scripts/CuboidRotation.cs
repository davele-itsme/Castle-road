using System;
using System.Collections;
using UnityEngine;

public class CuboidRotation : MonoBehaviour
{
    [SerializeField] private Vector3 rotationAngle;
    [SerializeField] private float rotationSpeed, floatSpeed, floatRate;
    private bool _goingUp;
    private float _floatTimer;
    private bool _pause;
    
    private void Start()
    {
        ResumeAnimation();
        PauseMenu.OnPaused += PauseAnimation;
        PauseMenu.OnResumed += ResumeAnimation;
    }

    private IEnumerator Rotate()
    {
        do
        {
            transform.Rotate(rotationAngle * (rotationSpeed * Time.deltaTime));
            yield return null;
        } while (!_pause);
    }
    
    private IEnumerator Float()
    {
        do
        {
            _floatTimer += Time.deltaTime;
            var moveDir = new Vector3(0.0f, 0.0f, floatSpeed);
            transform.Translate(moveDir);

            switch (_goingUp)
            {
                case true when _floatTimer >= floatRate:
                    _goingUp = false;
                    _floatTimer = 0;
                    floatSpeed = -floatSpeed;
                    break;
                case false when _floatTimer >= floatRate:
                    _goingUp = true;
                    _floatTimer = 0;
                    floatSpeed = +floatSpeed;
                    break;
            }
            yield return null;
        } while (!_pause);
    }

    private void PauseAnimation()
    {
        _pause = true;
    }
    
    private void ResumeAnimation()
    {
        _pause = false;
        StartCoroutine(Rotate());
        StartCoroutine(Float());
    }

    private void OnDestroy()
    {
        PauseMenu.OnPaused -= PauseAnimation;
        PauseMenu.OnResumed -= ResumeAnimation;
    }
}
