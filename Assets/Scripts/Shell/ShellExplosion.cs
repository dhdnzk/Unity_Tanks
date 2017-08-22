using UnityEngine;

public class ShellExplosion : MonoBehaviour
{
    public LayerMask m_TankMask;
    public ParticleSystem m_ExplosionParticles;       
    public AudioSource m_ExplosionAudio;              
    public float m_MaxDamage = 100f;                  
    public float m_ExplosionForce = 1000f;            
    public float m_MaxLifeTime = 2f;                  
    public float m_ExplosionRadius = 5f;              


    private void Start()
    {
        Destroy(gameObject, m_MaxLifeTime);
    }


    private void OnTriggerEnter(Collider other)
    {

		Collider[] touchedColliders = Physics.OverlapSphere (this.transform.position, m_ExplosionRadius, m_TankMask);

		for (int i = 0; i < touchedColliders.Length; i++) {

			Rigidbody targetRigidbody = touchedColliders [i].GetComponent <Rigidbody> ();

			if (targetRigidbody == null)
				continue;

			targetRigidbody.AddExplosionForce (m_ExplosionForce, this.transform.position, m_ExplosionRadius);

			TankHealth targetHealth = targetRigidbody.GetComponent <TankHealth> ();

			if (targetHealth == null)
				continue;


			float damage = CalculateDamage (targetRigidbody.GetComponent <Transform>().position);

			targetHealth.TakeDamage (damage);

		}

		m_ExplosionParticles.transform.parent = null;

		m_ExplosionParticles.Play ();

		m_ExplosionAudio.Play ();

		Destroy (m_ExplosionParticles.gameObject, m_ExplosionParticles.main.duration);

		Destroy (this.gameObject);

    }


    private float CalculateDamage(Vector3 targetPosition)
    {

//		return m_MaxDamage / Vector3.Distance (targetPosition, this.transform.position);

		Vector3 explosionToTarget = targetPosition - transform.position;

		float explosionDistance = explosionToTarget.magnitude;

		float relativeDistance = (m_ExplosionRadius - explosionDistance) / m_ExplosionRadius;

		float damage = relativeDistance * m_MaxDamage;

		damage = Mathf.Max (0f, damage);

		return damage;

    }
}

///*
// * With the Shell GameObject still selected drag the child GameObject called ShellExplosion onto the ExplosionParticles and Explosion Audio public variables.
// * set the Tank Mask public variable to Players.
// * Drag the Shell GameObject to the Prefabs folder in the Project panel to save it as a prefab.
// * 
// * 
//* Shell GameObject를 선택하고 ShellExplosion이라는 하위 GameObject를 ExplosionParticles 및 Explosion Audio 공용 변수로 드래그합니다.
//  * Tank Mask 공용 변수를 Players로 설정하십시오.
//  * 쉘 게임 객체를 [프로젝트] 패널의 [프리 팹] 폴더로 드래그하여 프리 팹으로 저장합니다.
//  *
//  *
// * /