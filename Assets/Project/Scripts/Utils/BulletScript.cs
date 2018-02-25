using UnityEngine;
using System.Collections;

/*
 * Moves an object until it hits a target, at which time it calls the Damage(float) method on all scripts on the hit object
 * Can also home in on targets and "detonate" when in close proximity.
 * */

namespace ParagonAI
{
    public class BulletScript : MonoBehaviour
    {

        public float speed = 50;
        public LayerMask layerMask;
        public float maxLifeTime = 4;

        //Bullet Stuff
        public int damage = 16;

        //use for shotguns
        public float bulletForce = 4;

        //Hit Effects
        public GameObject hitEffect;
        public float hitEffectDestroyTime = 1;
        public string hitEffectTag = "HitBox";
        public GameObject missEffect;
        public float missEffectDestroyTime = 6;
        public float timeToDestroyAfterHitting = 0.2f;
        private RaycastHit hit;
        private Transform myTransform;
        private Quaternion targetRotation;
        private Transform target = null;
        public float homingTrackingSpeed = 5;

        public float minDistToDetonate = 2f;
        private float minDistToDetonateSqr;
        private Actor TargetActor;
        public Actor AttackActor;
        void Awake()
        {
            layerMask = LayerMask.GetMask("Default");
            hitEffect = LoadResource.Instance.Load<GameObject>("Model/Weapons/Explosion Missile 2"); 
            missEffect = hitEffect;
            myTransform = transform;
            Move();
            StartCoroutine(SetTimeToDestroy());
        }

        IEnumerator SetTimeToDestroy()
        {
            yield return new WaitForSeconds(maxLifeTime);
            if (target)
            {
                Instantiate(hitEffect, myTransform.position, myTransform.rotation);
            }
            Destroy(gameObject);
        }

        IEnumerator ApplyDamage()
        {
            if (hit.collider.gameObject.GetComponent<ActorBehavior>() != null)
            {
                TargetActor = hit.collider.gameObject.GetComponent<ActorBehavior>().Owner;
                TargetActor.BeDamage(AttackActor, damage);
            }
            if (hit.transform.tag == hitEffectTag && hitEffect)
            {
                GameObject currentHitEffect = (GameObject)(Instantiate(hitEffect, hit.point, myTransform.rotation));
                GameObject.Destroy(currentHitEffect, hitEffectDestroyTime);
            }
            else if (missEffect)
            {
                GameObject currentMissEffect = (GameObject)(Instantiate(missEffect, hit.point + hit.normal * 0.01f, Quaternion.LookRotation(hit.normal)));
                GameObject.Destroy(currentMissEffect, missEffectDestroyTime);
            }
            this.enabled = false;
            yield return null;
            Destroy(gameObject, timeToDestroyAfterHitting);
        }

        // Update is called once per frame
        void Update()
        {
            Move();
        }

        void Move()
        {
            //Check to see if we're going to hit anything.  If so, move right to it and deal damage
            if (Physics.Raycast(myTransform.position, myTransform.forward, out hit, speed * Time.deltaTime, layerMask.value))
            {
                myTransform.position = hit.point;
                StartCoroutine(ApplyDamage());
            }
            else
            {
                //Move the bullet forwards
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }

            //Home in on the target
            if (target != null)
            {
                //Firgure out the rotation required to move directly towards the target
                targetRotation = Quaternion.LookRotation(target.position - transform.position);
                //Smoothly rotate to face the target over several frames.  The slower the rotation, the easier it is to dodg.
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, homingTrackingSpeed * Time.deltaTime);

                Debug.DrawRay(transform.position, (target.position - transform.position).normalized * minDistToDetonate, Color.red);
                //Projectile will "detonate" upon getting close enough to the target..
                if (Vector3.SqrMagnitude(target.position - transform.position) < minDistToDetonateSqr)
                {
                    //The hitEffect should be your explosion.
                    Instantiate(hitEffect, myTransform.position, myTransform.rotation);
                    GameObject.Destroy(gameObject);
                }
            }
        }

    }
}
