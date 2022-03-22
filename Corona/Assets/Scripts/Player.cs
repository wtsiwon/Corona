using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField]
    private float Speed;
    public int Hp
    {
        get
        {
            return Hp;
        }

        set
        {
            if (Hp > 0)
            {

                OnDamaged(value);
            }
            else
            {
                OnDie();
            }
        }
    }
    void OnDie()
    {
        Destroy(gameObject);
    }
    void OnDamaged(int value)
    {
        Hp -= value;
    }
    void Update()
    {
        
        

    }
}
