using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void EnemyEscapedHandler(EnemyController enemy);

public class EnemyController : Shape, IKillable
{

    public event EnemyEscapedHandler EnemyEscaped;
    public event Action<int> EnemyKilled;

    protected override void Start()
    {
        base.Start();
        Name = "Enemy";

        Debug.Log("Enemy Nasceu");
    }

    void Update()
    {
        MoveEnemy();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (EnemyKilled != null)
        {
            EnemyKilled(10);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void MoveEnemy()
    {
        //enviar o objeto pra baixo
        transform.Translate(Vector2.down * Time.deltaTime, Space.World);

        float bottom = transform.position.y - halfHeight;

        //se o inimigo, passou pela tela e não foi destruído, então remover da memória
        if(bottom <= -gameSceneController.screenBounds.y)
        {
            //a classe shape (Base) se ligou ao GameSceneController, para poder dar KillObject
            //gameSceneController.KillObject(this);
            if (EnemyEscaped != null)
            {
                EnemyEscaped(this);
            }
        }
    }

    private void InternalOutputText(string output)
    {
        Debug.LogFormat("{0} escrito por EnemyController", output);
    }

    public void Kill()
    {
        //função que destrói objetos em cena
        Destroy(gameObject);
    }

    public string GetName()
    {
        return Name;
    }
}
