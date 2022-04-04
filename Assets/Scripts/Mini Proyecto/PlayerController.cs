using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speedMultiplier = 1f;
    private float speed = 0;
    private bool cooldown = false;
    private void Update()
    {
        transform.Translate(Vector3.up * speedMultiplier * speed * Time.deltaTime);
        if (Input.touchCount > 0 && !cooldown && RocksUIController.INSTANCE.playing)
        {
            StartCoroutine(Cooldown(1.5f));
            speed += RocksUIController.INSTANCE.value * 0.000001f;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        Debug.Log(RocksUIController.INSTANCE.playing);
        if (other.CompareTag("Finish") && RocksUIController.INSTANCE.playing) GameplayBehaviour.INSTANCE.FinishGame(true);
    }
    IEnumerator Cooldown(float time)
    {
        RocksUIController.INSTANCE.indicator.style.unityBackgroundImageTintColor = new UnityEngine.UIElements.StyleColor(Color.gray);
        cooldown = true;
        yield return new WaitForSecondsRealtime(time);
        RocksUIController.INSTANCE.indicator.style.unityBackgroundImageTintColor = new UnityEngine.UIElements.StyleColor(Color.white);
        cooldown = false;
    }
}
