using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    [SerializeField] bool isSpeed;
    [SerializeField] int speed;

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

    public void SetPowerup()
    {
        currentTime = timer;
        countDown = true;

        if (isSpeed)
        {
            WorldHolder.instance.SetSpeedMultiplier(speed);
        }
        else
        {
            SetEnemySpeedMultiplier(-.5f);
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