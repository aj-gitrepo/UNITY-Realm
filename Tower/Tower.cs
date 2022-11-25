using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 75; //cost to instatntiate one tower
    [SerializeField] float buildDelay = 1f;

    void Start()
    {
        StartCoroutine(Build());
    }

    public bool CreateTower(Tower tower, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();

        if(bank == null)
        {
            return false;
        }

        if(bank.CurrentBalance >= cost)
        {
            Instantiate(tower.gameObject, position, Quaternion.identity);
            bank.Withdraw(cost);
            return true;
        }

        return false;
    }

    IEnumerator Build()
    {
        // turn off the tower on instantiation
        foreach(Transform child in transform) //top and base
        {
            child.gameObject.SetActive(false);
            foreach(Transform grandchild in child) //particleSystem in tower top
            {
                grandchild.gameObject.SetActive(false); 
            }
        }
        // turn on the tower part by part
        foreach(Transform child in transform) //top and base
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(buildDelay);
            foreach(Transform grandchild in child) //particleSystem in tower top
            {
                grandchild.gameObject.SetActive(true); 
            }
        }
    }
}
