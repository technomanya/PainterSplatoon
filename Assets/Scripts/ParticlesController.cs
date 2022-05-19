using System.Collections;
using System.Collections.Generic;
using Project.Runtime.Scripts.Behaviours;
using UnityEngine;

public class ParticlesController: MonoBehaviour{
    public Color paintColor;
    
    public float minRadius = 0.05f;
    public float maxRadius = 0.2f;
    public float strength = 1;
    public float hardness = 1;
    [Space]
    ParticleSystem part;
    List<ParticleCollisionEvent> collisionEvents;

    void Start(){
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
        //var pr = part.GetComponent<ParticleSystemRenderer>();
        //Color c = new Color(pr.material.color.r, pr.material.color.g, pr.material.color.b, .8f);
        //paintColor = c;
    }

    void OnParticleCollision(GameObject other) {
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

       // Debug.Log(other.tag);
        PaintableTexture p = other.GetComponent<PaintableTexture>();
        if(p != null){
            for  (int i = 0; i< numCollisionEvents; i++){
                Vector3 pos = collisionEvents[i].intersection;
                float radius = Random.Range(minRadius, maxRadius);
                PaintManager.instance.Paint(p, pos, radius, hardness, strength, paintColor);
                GridPointBehaviour pointBehaviour;
                pointBehaviour = other.GetComponent<GridPointBehaviour>();
                if (!pointBehaviour) continue;
                if (transform.root.CompareTag("Player"))
                {
                    pointBehaviour.PaintAndTag(PointColourState.Player);
                }
                else if (transform.root.CompareTag("Enemy"))
                {
                    pointBehaviour.PaintAndTag(PointColourState.Enemy);
                }
            }
        }
    }
}