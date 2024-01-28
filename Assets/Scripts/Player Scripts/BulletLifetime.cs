using System.Collections;
using UnityEngine;

public class BulletLifetime : MonoBehaviour
{
    [SerializeField]
    private float lifetime = 5f;

    private void Start()
    {
        StartCoroutine(DisableBullet(lifetime));
    }

    IEnumerator DisableBullet(float timer)
    {
        print("BUllet lifetime disable bullet");
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }
}