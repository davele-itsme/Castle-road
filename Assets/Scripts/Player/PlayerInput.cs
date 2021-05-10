using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public delegate void HorInput(float value);

    public static event HorInput HorizontalInput;

    public delegate void VerInput(float value);

    public static event VerInput VerticalInput;
    private void Update()
    {
        var horValue = Input.GetAxisRaw("Horizontal");
        var verValue = Input.GetAxisRaw("Vertical");
        if (horValue != 0 && HorizontalInput != null)
        {
            HorizontalInput(horValue);
        }
        else if (verValue != 0 && VerticalInput != null)
        {
            VerticalInput(verValue);
        }
    }
}
