using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletManager : MonoBehaviour
{
    //[SerializeField] GameObject bulletInsPosi;
    [SerializeField] int bulletNum = 30;
    [SerializeField] GameObject originBullet;
    List<GameObject> bullets = new List<GameObject>();
    int bulletIndex = 0;
    //Quaternion bulletRotate;

    [SerializeField] float firingInterval = 0.3f;
    float intervalTime = 0;
    BulletMove[] bulletMove;
    public bool deathEnemy = false;

    private void Awake()
    {
        bulletMove = new BulletMove[bulletNum];
        for (int i = 0; i < bulletNum; i++)
        {
            bullets.Add(Instantiate(originBullet, transform.position, Quaternion.identity));
            bulletMove[i] = bullets[i].GetComponent<BulletMove>();
            bullets[i].SetActive(false);
        }
    }

    private void Update()
    {
        FireBullet();
    }

    private void FireBullet()
    {
        if (deathEnemy) return;

        intervalTime += Time.deltaTime;
        if (intervalTime > firingInterval)
        {
            intervalTime = 0;
            bullets[bulletIndex].transform.position = transform.position;
            bullets[bulletIndex].transform.rotation = transform.rotation;
            bulletMove[bulletIndex].Fire();
            //bulletRotate = bulletInsPosi.transform.rotation;
            
            bullets[bulletIndex].SetActive(true);
            bulletIndex++;
            if (bulletIndex >= bullets.Count)
            {
                bulletIndex = 0;
            }
        }
    }

    public void OnDead()
    {
        deathEnemy = true;
        foreach (var item in bullets)
        {
            Destroy(item, 5.0f);
            //item.SetActive(true);
        }
        bulletMove = null;
        bullets = null;
    }
}
