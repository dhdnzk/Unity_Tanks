using UnityEngine;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour
{
    public float m_StartingHealth = 100f;
    public Image m_FillImage;
    public Color m_FullHealthColor = Color.green;
    public Color m_ZeroHealthColor = Color.red;
    public GameObject m_ExplosionPrefab;
    
	public float m_CurrentHealth;

	private Slider m_Slider;
    private AudioSource m_ExplosionAudio;
    private ParticleSystem m_ExplosionParticles;
    private bool m_Dead;


    private void Awake()
    {
        m_ExplosionParticles = Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();

        m_ExplosionAudio = m_ExplosionParticles.GetComponent<AudioSource>();

        m_ExplosionParticles.gameObject.SetActive(false);

		m_Slider = GetComponentInChildren<Slider> ();

    }


    private void OnEnable()
    {
        m_CurrentHealth = m_StartingHealth;
        m_Dead = false;

        SetHealthUI();
    }

    public void TakeDamage(float amount)
    {
		m_CurrentHealth -= amount;

		SetHealthUI ();

		if (m_CurrentHealth <= 0) {

			OnDeath ();

		}
    }


    private void SetHealthUI()
    {

		m_Slider.value = m_CurrentHealth;
//		if(m_CurrentHealth <= 0) {
//
//			// 색깔 설정
//
//		}
        // Adjust the value and colour of the slider.
    }


    private void OnDeath()
    {

        // Play the effects for the death of the tank and deactivate it.

    }

}