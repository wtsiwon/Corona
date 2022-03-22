using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Collider))]
public abstract class Enemy : MonoBehaviour
{
    [Header("��ü �Ӽ�")]
    [SerializeField] protected float spd;
    [SerializeField] protected float hp;

    [Header("���� �Ӽ�")]
    public float atkDmg;
    [SerializeField] protected Transform firePos;
    [SerializeField] protected Bullet bulletObj;

    [Header("���� ���� �Ӽ�")]
    [SerializeField] private bool isUnlimitShotcnt;
    [SerializeField] private int shotCnt;
    [SerializeField] private float continiousShotInterval;

    [Header("����� ���� �Ӽ�")]
    [SerializeField] private int wayCnt;

    [Header("����Ʈ")]
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private GameObject dieEffect;

    protected Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (isUnlimitShotcnt) Invoke("Attack", 1f);
        //else InvokeRepeating("Attack", 1f, bulle);

        Move();
    }
    protected abstract void Attack();
    private void Move()
    {
        rb.velocity = Vector3.back * spd;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            Destroy(other.gameObject);
            Instantiate(hitEffect).transform.position = other.transform.position;

            hp = Mathf.Max(0, hp - other.GetComponent<Bullet>().dmg);
            if(hp == 0)
            {
                OnDie();
            }
        }

    }
    public void OnDie()
    {
        Instantiate(dieEffect).transform.position = transform.position;
        Destroy(gameObject);
    }
}
