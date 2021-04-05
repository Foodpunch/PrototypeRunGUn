using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEntity : EnemyEntity
{
    float stopRadiusSqrd = 16f;
    float separateSpeed = 0.5f;
    float separateRadius = 1f;
    Vector2 sum = Vector2.zero;
    float count = 0f;

    protected override void Start()
    {
        base.Start();
    }

    protected override void DoBehaviour()
    {
        float f = Utilities.Map(distToPlayerSquared, 0, entityStat.speed, 0, stopRadiusSqrd);
        _rb.velocity = new Vector2(DirectionToPlayer.x, DirectionToPlayer.y) * f;

        var hits = Physics2D.OverlapCircleAll(transform.position, separateRadius);
        foreach (var hit in hits)
        {
            // make sure it is a fellow enemy
            if (hit.GetComponent<MeleeEntity>() != null && hit.transform != transform)
            {
                // get the difference so you know which way to go
                Vector2 difference = transform.position - hit.transform.position;

                // weight by distance so being closer means moving more
                difference = difference.normalized / Mathf.Abs(difference.magnitude);

                // add together to get average of the group
                // this allows those at the edges of a group to move out while
                // the enemies in the center of a group to not move much
                sum += difference;
                count++;
            }
        }

        if (count > 0)
        {
            // average the direction
            sum /= count;

            // set the speed of movement
            sum = sum.normalized * separateSpeed;

            // this is where you would apply this vector for movement
            transform.position = Vector2.MoveTowards(transform.position, transform.position + (Vector3)sum, separateSpeed * Time.deltaTime);
        }
    }
}
