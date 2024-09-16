using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damage;
    public float speed;
    public float destructTime;
    private void Start()
    {
        StartCoroutine(SelfDestruct(destructTime));
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name == "Enemy(Clone)")
        {
            col.gameObject.GetComponent<Enemy>().currentHP -= damage;
            Destroy(gameObject);
        }
    }

    public void SetDamage(float dmg)
    {
        damage = dmg;
    }

    private IEnumerator SelfDestruct(float destructTime)
    {
        yield return new WaitForSeconds(destructTime);
        Destroy(gameObject);
    }
}
