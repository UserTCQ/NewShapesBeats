using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl player;

    public bool controllable = true;

    public float speed;
    public float rotSpeed;
    public float dashSpeed;
    public float damagePushSpeed;

    public float compression;
    public float dashCompressionAdditive;
    public float compressionSpeed;

    public KeyCode dashKey;

    public AudioSource hitAudioSource;
    public AudioSource musicSource;

    public ParticleSystem graphicParticleSystem;

    public Transform graphicContainer;

    public SpriteRenderer graphic;
    public SpriteRenderer shadow;

    public Slider hpBar;

    Vector2 damagePush;

    Vector2 normalScale;
    Vector2 scale;

    float additiveCompression = 0f;
    float currentSpeed;

    bool dashAvailable = true;
    bool vulnerable = true;

    public int hp = 4;

#if UNITY_EDITOR
    [SerializeField]
    bool noclip = false;
#endif

    void Start()
    {
        normalScale = graphic.transform.localScale;
        currentSpeed = speed;
        player = this;
        controllable = true;
    }

    void Update()
    {
        Vector2 move;

        if (controllable)
            move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) + damagePush;
        else
            move = Vector2.zero;

        if (move != Vector2.zero)
        {
            scale.y = normalScale.y * (1 - compression - additiveCompression);
            scale.x = normalScale.x * (1 + compression + additiveCompression);

            if (Input.GetKeyDown(dashKey) && dashAvailable)
            {
                StartCoroutine(Dash());
            }
        }
        else
        {
            scale.y = normalScale.y;
            scale.x = normalScale.x;
        }

        transform.Translate(move * currentSpeed * Time.deltaTime);

        graphicContainer.localScale = Vector3.Slerp(graphicContainer.localScale, scale, Time.deltaTime * compressionSpeed);

        float angle = Mathf.Atan2(move.y, move.x) * Mathf.Rad2Deg;
        Quaternion newRot = Quaternion.AngleAxis(angle, Vector3.forward);
        graphicContainer.rotation = Quaternion.Slerp(graphicContainer.rotation, newRot, Time.deltaTime * rotSpeed);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Damage")
        {
#if UNITY_EDITOR
            if (vulnerable && dashAvailable && !noclip)
            {
                StartCoroutine(Damage());
                hitAudioSource.Play();

                StartCoroutine(HPLerp(hp, hp - 1));
                hp--;
            }
#endif

#if UNITY_STANDALONE
            if (vulnerable && dashAvailable)
            {
                StartCoroutine(Damage());
                hitAudioSource.Play();

                StartCoroutine(HPLerp(hp, hp - 1));
                hp--;
            }
#endif
        }
    }

    IEnumerator HPLerp(int a, int b)
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * 4f;
            hpBar.value = Mathf.Lerp(a, b, EaseOut(t)) / 4f;
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator DamageFlash()
    {
        var sr = graphic;

        for (int i = 0; i < 5; i++)
        {
            sr.color = new Color(1f, 0f, 0f);

            yield return new WaitForSeconds(0.06f);

            sr.color = new Color(0f, 1f, 1f);

            yield return new WaitForSeconds(0.06f);
        }

        vulnerable = true;
    } 

    public IEnumerator die()
    {
        PauseScript.pausable = false;
        controllable = false;
        graphic.color = new Color(0, 0, 0, 0);
        shadow.color = new Color(0, 0, 0, 0);

        if (LevelSystem.system.processor != null)
            StopCoroutine(LevelSystem.system.processor);

        float t = 60;
        while (t < 135)
        {
            t += Time.deltaTime * 60;
            musicSource.pitch = Mathf.Cos(t * Mathf.Deg2Rad) * 2f;
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(0.25f);
        LevelSystem.system.stop = true;
        Time2.elapsed = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    Coroutine co;

    IEnumerator Damage()
    {
        vulnerable = false;

        Settings.hits++;

        if (hp > 1)
        {
            co = StartCoroutine(DamageFlash());

            damagePush = Random.onUnitSphere * damagePushSpeed;

            yield return new WaitForSeconds(0.1f);

            damagePush = Vector2.zero;
        }
        else
        {
            if (co != null) StopCoroutine(co);
            Settings.deaths++;
            StartCoroutine(die());
        }
    }

    IEnumerator Dash()
    {
        dashAvailable = false;
        currentSpeed = dashSpeed;
        additiveCompression = dashCompressionAdditive;

        var em = graphicParticleSystem.emission;
        em.rateOverDistance = 1f;

        yield return new WaitForSeconds(0.2f);

        additiveCompression = 0f;
        em.rateOverDistance = 5f;
        currentSpeed = speed;

        yield return new WaitForSeconds(0.25f);

        dashAvailable = true;

    }

    float EaseOut(float t)
    {
        return flip(flip(t) * flip(t));
    }

    float flip(float x)
    {
        return 1 - x;
    }
}
