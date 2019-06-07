using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] Transform parent;
    [SerializeField] int scorePerHit = 12;

    ScoreBoard scoreboard;

    // Start is called before the first frame update
    void Start()
    {
        AddNonTriggerBoxCollider();
        scoreboard = FindObjectOfType<ScoreBoard>();
    }

    void AddNonTriggerBoxCollider()
    {
        Collider boxEnemy = gameObject.AddComponent<BoxCollider>();
        boxEnemy.isTrigger = false;
    }

    void OnParticleCollision(GameObject other)
    {
        scoreboard.ScoreHit(scorePerHit);
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent;
        Destroy(gameObject);
    }
}
