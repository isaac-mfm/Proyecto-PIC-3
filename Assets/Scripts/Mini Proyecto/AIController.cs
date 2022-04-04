using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] private float speedMultiplier = 1f;
    private float speed = 0;
    private bool cooldown = false;
    private void Update()
    {
        transform.Translate(Vector3.up * speedMultiplier * speed * Time.deltaTime);
        if (!cooldown && RocksUIController.INSTANCE.playing)
        {
            StartCoroutine(Cooldown(1.5f));
            speed += Random.Range(40, 85) * 0.000001f;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish") && RocksUIController.INSTANCE.playing) GameplayBehaviour.INSTANCE.FinishGame(false);
    }
    IEnumerator Cooldown(float time)
    {
        cooldown = true;
        yield return new WaitForSecondsRealtime(time);
        cooldown = false;
    }
}
