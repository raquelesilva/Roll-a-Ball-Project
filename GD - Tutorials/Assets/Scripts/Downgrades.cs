using System.Collections;
using System.Linq;
using UnityEngine;

public class Downgrades : MonoBehaviour
{
    [SerializeField] bool isSpeed;
    [SerializeField] float speed;

    [SerializeField] float timer = 10;
    [SerializeField] float currentTime;
    [SerializeField] bool countDown = false;

    private void Update()
    {
        if (countDown)
        {
            currentTime -= Time.deltaTime;
        }

        if (currentTime <= 0)
        {
            countDown = false;
        }
    }

    public void SetDowngrade()
    {
        currentTime = timer;
        countDown = true;

        if (isSpeed)
        {
            Debug.Log("Player Speed downgrade");
            WorldHolder.instance.SetSpeedMultiplier(speed);
        }
        else
        {
            SetEnemySpeedMultiplier(.5f);
        }

        gameObject.GetComponent<MeshRenderer>().enabled = false;
        StartCoroutine(DisablePowerup());
    }

    public void SetEnemySpeedMultiplier(float newEnemySpeed)
    {
        var allEnemys = FindObjectsByType<EnemyFollow>(FindObjectsSortMode.None).ToList();
        allEnemys.ForEach(x => x.SetSpeed(newEnemySpeed));
    }

    IEnumerator DisablePowerup()
    {
        yield return new WaitForSeconds(timer);

        if (isSpeed)
        {
            WorldHolder.instance.SetSpeedMultiplier(1);
        }
        else
        {
            SetEnemySpeedMultiplier(1);
        }

        Destroy(gameObject);
    }
}