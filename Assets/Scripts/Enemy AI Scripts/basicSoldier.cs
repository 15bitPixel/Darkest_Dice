using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicSoldier : MonoBehaviour
{
    public float enemyHealth;


    public float enemySpeed;
    public float enemyStoppingDistance;
    public float enemyRetreatDistance;

    private float timeBetweenShots;
    public float startTimeBetweenShots;

    public GameObject bullet;

    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBetweenShots = startTimeBetweenShots;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) > enemyStoppingDistance)
        {

            transform.position = Vector3.MoveTowards(transform.position, player.position, enemySpeed * Time.deltaTime);

        }
        else if (Vector3.Distance(transform.position, player.position) < enemyStoppingDistance && Vector3.Distance(transform.position, player.position) > enemyRetreatDistance)
        {
            transform.position = this.transform.position;
        }
        else if (Vector3.Distance(transform.position, player.position) < enemyRetreatDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, -enemySpeed * Time.deltaTime);
        }

        if(timeBetweenShots <= 0)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            timeBetweenShots = startTimeBetweenShots;
        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
        }

        if(enemyHealth == 0.0f)
        {
            Destroy(gameObject);
        }

    }
}
