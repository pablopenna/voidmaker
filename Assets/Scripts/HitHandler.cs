using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitHandler : MonoBehaviour
{
    BaseEntity managedEntity;

    void Start() {
        managedEntity = GetComponent<BaseEntity>(); //It works! Can fetch by interface
    }

    public void Hit() {
        managedEntity.Destroy();
    }
}
