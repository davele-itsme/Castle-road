using System.Collections;
using Player;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour
{
    [SerializeField] private Image jumpAbility;
    private void Start()
    {
        jumpAbility.fillAmount = 1;
        PlayerJump.OnJumped += CreateCooldown;
    }

    private void CreateCooldown(float cooldown)
    {
        StartCoroutine(UpdateIcon(cooldown));
    }

    private IEnumerator UpdateIcon(float cooldown)
    {
        while (jumpAbility.fillAmount > 0)
        {
            jumpAbility.fillAmount -= 1 / cooldown * Time.deltaTime;
            yield return null;
        }
        jumpAbility.fillAmount = 1;
    }

    private void OnDestroy()
    {
        PlayerJump.OnJumped -= CreateCooldown;
    }
}
