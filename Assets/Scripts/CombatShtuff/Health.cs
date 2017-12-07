using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private float curHealth;
    public float MaxHealth;
    public bool MARKEDFORDEATH = false;
    private float DeathTim = 1;
    private MovementController movyForDodgy;
    private MeshRenderer _mesh;
    private float _colorMod = 0;
    public float StaggerTime = 0.5f;
    private float _stageTim = 0;
    public float Poise = 10;
    public float PoiseRecovery = 1;
    private float _curPoise = 10;
    public bool IsStaggered;
    public AudioClip[] HurtClips;
    public AudioClip[] DeathClips;
    public bool CanStagger;
	// Use this for initialization
	void Start ()
	{
	    CanStagger = true;
        curHealth = MaxHealth;
        movyForDodgy = GetComponent<MovementController>();
        _mesh = GetComponent<MeshRenderer>();
	    _curPoise = Poise;
	    Debug.Assert(HurtClips.Length > 0, "ERROR IN HEALTH: No hurtclips provided for " + gameObject.name);
    }
	
	// Update is called once per frame
	void Update ()
	{
	    if (Input.GetKeyDown(KeyCode.K) && gameObject.name == "Player")
	    {
	        DealDamage(float.MaxValue);
	    }
	    _mesh.material.color = new Color(1, _colorMod, _colorMod);
	    if (_colorMod < 1)
	    {
	        _colorMod += Time.deltaTime;

	    }
        if (MARKEDFORDEATH)
        {
            DeathTim -= Time.deltaTime;
            var forw = transform.forward;
            forw.y = 0;

            //for glitchy do transform.right
            transform.rotation = Quaternion.LookRotation(forw, Vector3.up) *
                                 Quaternion.AngleAxis(90 * Mathf.Cos(Mathf.PI / 2 * DeathTim), Vector3.right); 
            _stageTim -= Time.deltaTime;
            if (DeathTim <= 0)
            Destroy(gameObject);
        }
	    if (Input.GetKeyDown(KeyCode.P))
	    {
	        curHealth += 10;
	    }
	    IsStaggered = _stageTim > 0;
        if (_curPoise < Poise)
        {
            _curPoise += Time.deltaTime * PoiseRecovery;

        }
        
        if (IsStaggered)
        {
            var forw = transform.forward;
            forw.y = 0;
            
            //for glitchy do transform.right
            transform.rotation = Quaternion.LookRotation(forw, Vector3.up) *
                                 Quaternion.AngleAxis(30 * Mathf.Sin(Mathf.PI * (_stageTim / StaggerTime)), Vector3.right);
            _stageTim -= Time.deltaTime;

	    }
        else if (!MARKEDFORDEATH)
        {
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, transform.eulerAngles.z);
        }
	}

    public void DealDamage(float damage)
    {
        if(!movyForDodgy.isDodging() && damage > 0.01f)
        {
            _colorMod = 0;
            curHealth -= damage;
            MARKEDFORDEATH = curHealth <= 0;
            if (gameObject.name == "Player")
            {
                GetComponent<PlayerController>().MyController.VibrateFor(1, Mathf.Min(damage, MaxHealth) / MaxHealth);
            }
            if (MARKEDFORDEATH)
            {
                PlayDeath();
            }
            else if (CanStagger)
            {
                _curPoise -= damage;
                if (_curPoise < 0)
                {
                    _curPoise = Poise;
                    GetComponent<CombatController>().Stagger();
                    _stageTim = StaggerTime;
                    GetComponent<AudioSource>().PlayOneShot(HurtClips[Random.Range(0,HurtClips.Length)]);
                }
            }
        }
    }

    public float GetHealth()
    {
        return curHealth;
    }

    private void PlayDeath()
    {
        var foo = new GameObject("wew", typeof(AudioSource));
        foreach (var clip in DeathClips)
        {
            foo.GetComponent<AudioSource>().PlayOneShot(clip);
        }
    }

    public void HealFull()
    {
        curHealth = MaxHealth;
    }
}
