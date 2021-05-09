using UnityEngine;

public class CuboidRotation : MonoBehaviour
{
    [SerializeField] private Vector3 rotationAngle;
    [SerializeField] private float rotationSpeed, floatSpeed, floatRate;
    private bool _goingUp;
    private float _floatTimer;
    private void Update()
    {
        Rotate();
        Float();
    }

    private void Rotate()
    {
        transform.Rotate(rotationAngle * (rotationSpeed * Time.deltaTime));
    }
    
    private void Float()
    {
        _floatTimer += Time.deltaTime;
        var moveDir = new Vector3(0.0f, 0.0f, floatSpeed);
        transform.Translate(moveDir);

        if (_goingUp && _floatTimer >= floatRate)
        {
            _goingUp = false;
            _floatTimer = 0;
            floatSpeed = -floatSpeed;
        }

        else if(!_goingUp && _floatTimer >= floatRate)
        {
            _goingUp = true;
            _floatTimer = 0;
            floatSpeed = +floatSpeed;
        }
    }
}
