using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{

    public static SceneManagerScript sceneManagerInstance;

    public Vector2 startCoord;
    public bool checkpoint;
    public Vector2 checkpointCoord;
    public bool checkpointBoss;
    public Vector2 checkpointCoordBoss;
    [HideInInspector]
    public Player player;
    [HideInInspector]
    public Boss boss;
    [HideInInspector]
    public CameraBehaviour camera;

    // CUTSCENES

    public bool cutscene;
    public bool cutsceneStart;
    public bool cutsceneMiddle;
    public bool cutsceneBeforeBoss;
    public bool cutsceneBeforeBossOk;
    public bool cutsceneAfterBoss;

    private float playerPosX;

    void Start()
    {
        DontDestroyOnLoad(gameObject);

        if (sceneManagerInstance == null) {
            sceneManagerInstance = this;
        } else {
            DestroyObject(gameObject);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        playerPosX = player.transform.position.x;
        CutsceneManager();
        SetCheckpoint();
        if (cutscene) {
            frame++;
            if (cutsceneBeforeBoss) {
                if (camera.transform.position.x < 31.65f) {
                    float x = camera.transform.position.x;
                    float y = camera.transform.position.y;
                    float z = camera.transform.position.z;
                    camera.transform.position = new Vector3 (x + 0.01f, y, z);
                }
            }
        }
    }

    void CutsceneManager () {
        if (!cutsceneStart && !checkpoint && !checkpointBoss) {
            cutscene = true;
            cutsceneStart = true;
            StartCoroutine(CutsceneStart());
        } else if (cutsceneStart && !cutsceneMiddle && !checkpoint && !checkpointBoss) {
            if (playerPosX >= 2.4f) {
                cutscene = true;
                cutsceneMiddle = true;
                StartCoroutine(CutsceneMiddle());
            }
        } else if (cutsceneMiddle && !cutsceneBeforeBoss && checkpointBoss) {
            if (playerPosX >= 29.8f) {
                cutscene = true;
                cutsceneBeforeBoss = true;
                StartCoroutine(CutsceneBeforeBoss());
            }
        } else if (cutsceneMiddle && cutsceneBeforeBoss && checkpointBoss && cutsceneBeforeBossOk) {
            if (playerPosX >= 29.8f) {
                cutscene = true;
                cutsceneBeforeBossOk = false;
                StartCoroutine(CutsceneBeforeBoss2());
            }
        }
        
        if (cutsceneBeforeBoss && !cutsceneAfterBoss && checkpointBoss) {
            if (boss.isDead && !boss.battleOn) {
                cutscene = true;
                cutsceneAfterBoss = true;
                StartCoroutine(CutsceneAfterBoss());
            }
        }

        if (playerPosX >= 35.1f) {
            SceneManager.LoadScene("Main Menu");
            DestroyObject(gameObject);
        }
    }

    private int frame = 0;
    private GameObject fala;

    void NewFala (string path, Transform parent) {
        path = "Graphics/Falas/" + path;
        GameObject falaPrefab = Resources.Load<GameObject>("Prefabs/Fala");
        falaPrefab.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(path);
        fala = Instantiate (falaPrefab, parent.position, parent.rotation, parent);
    }

    void NewTutorial (string path, Transform parent) {
        path = "Graphics/Falas/" + path;
        GameObject falaPrefab = Resources.Load<GameObject>("Prefabs/Fala");
        falaPrefab.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(path);
        float x = parent.position.x;
        float y = parent.position.y;
        float z = parent.position.z;
        fala = Instantiate (falaPrefab, new Vector3 (x, y + 0.16f, z), parent.rotation, parent);
    }

    // CUTSCENE START

    IEnumerator CutsceneStart () {

        float posx = 1;
        player.rb.velocity = new Vector2 (player.moveSpeed, 0);
        yield return new WaitUntil (() => playerPosX >= posx);
        player.rb.velocity = new Vector2 (0, 0);

        NewFala ("raposa1", player.transform);
        frame = 0;
        yield return new WaitUntil (() => frame >= 120);
        Destroy(fala);

        NewFala ("raposa2", player.transform);
        frame = 0;
        yield return new WaitUntil (() => frame >= 120);
        Destroy(fala);

        NewTutorial ("tutorial1", player.transform);
        frame = 0;
        yield return new WaitUntil (() => frame >= 180);
        Destroy(fala);
        
        cutscene = false;
    }

    // CUTSCENE MIDDLE

    IEnumerator CutsceneMiddle () {

        player.rb.velocity = new Vector2 (0, player.rb.velocity.y);

        NewFala ("raposa3", player.transform);
        frame = 0;
        yield return new WaitUntil (() => frame >= 240);
        Destroy(fala);

        NewTutorial ("tutorial2", player.transform);
        frame = 0;
        yield return new WaitUntil (() => frame >= 180);
        Destroy(fala);
        
        cutscene = false;
    }

    // CUTSCENE BEFORE BOSS

    private bool camerafollow;

    IEnumerator CutsceneBeforeBoss () {

        float posx = 31.65f;
        
        if (camera.transform.position.x < posx && !camerafollow) {
            player.rb.velocity = new Vector2 (0, player.rb.velocity.y);
            camera.follow = false;
            yield return new WaitUntil (() => camera.transform.position.x >= posx);
            camerafollow = true;
        }

        posx = 30.6f;

        if (playerPosX < posx) {
            player.rb.velocity = new Vector2 (player.moveSpeed, 0);
            yield return new WaitUntil (() => playerPosX >= posx);
            player.rb.velocity = new Vector2 (0, 0);
        }

        GameObject[] barreiras = new GameObject[2];
        barreiras[0] = GameObject.Find("/Barreira 1");
        barreiras[1] = GameObject.Find("/Barreira 2");

        if (barreiras[0].GetComponent<BoxCollider2D>().isTrigger) {
            foreach (GameObject barreira in barreiras) {
                barreira.GetComponent<BoxCollider2D>().isTrigger = false;
            }

            float y;
            float z;
            y = camera.minCameraPos.y;
            z = camera.minCameraPos.z;
            camera.minCameraPos = new Vector3 (31.65f, y, z);
            y = camera.maxCameraPos.y;
            z = camera.maxCameraPos.z;
            camera.maxCameraPos = new Vector3 (32.83f, y, z);

        }

        NewFala ("esquilo1", boss.transform);
        frame = 0;
        yield return new WaitUntil (() => frame >= 150);
        Destroy(fala);

        NewFala ("raposa4", player.transform);
        frame = 0;
        yield return new WaitUntil (() => frame >= 150);
        Destroy(fala);

        NewFala ("esquilo2", boss.transform);
        frame = 0;
        yield return new WaitUntil (() => frame >= 90);
        Destroy(fala);

        camera.follow = true;
        
        boss.battleOn = true;

        cutscene = false;
        camerafollow = false;
    }

    IEnumerator CutsceneBeforeBoss2 () {

        float posx = 31.65f;
        
        if (camera.transform.position.x < posx && !camerafollow) {
            player.rb.velocity = new Vector2 (0, player.rb.velocity.y);
            camera.follow = false;
            yield return new WaitUntil (() => camera.transform.position.x >= posx);
            camerafollow = true;
        }

        posx = 30.6f;

        if (playerPosX < posx) {
            player.rb.velocity = new Vector2 (player.moveSpeed, 0);
            yield return new WaitUntil (() => playerPosX >= posx);
            player.rb.velocity = new Vector2 (0, 0);
        }

        GameObject[] barreiras = new GameObject[2];
        barreiras[0] = GameObject.Find("/Barreira 1");
        barreiras[1] = GameObject.Find("/Barreira 2");

        if (barreiras[0].GetComponent<BoxCollider2D>().isTrigger) {
            foreach (GameObject barreira in barreiras) {
                barreira.GetComponent<BoxCollider2D>().isTrigger = false;
            }

            float y;
            float z;
            y = camera.minCameraPos.y;
            z = camera.minCameraPos.z;
            camera.minCameraPos = new Vector3 (31.65f, y, z);
            y = camera.maxCameraPos.y;
            z = camera.maxCameraPos.z;
            camera.maxCameraPos = new Vector3 (32.83f, y, z);

        }

        NewFala ("esquilo2", boss.transform);
        frame = 0;
        yield return new WaitUntil (() => frame >= 90);
        Destroy(fala);

        camera.follow = true;
        
        boss.battleOn = true;

        cutscene = false;
        camerafollow = false;
    }

    // CUTSCENE AFTER BOSS

    IEnumerator CutsceneAfterBoss () {

        if (!player.facingRight) {
            player.Flip();
        }

        NewFala ("esquilo3", boss.transform);
        //Debug.Log("Voltou para levar o que sobrou?");
        frame = 0;
        yield return new WaitUntil (() => frame >= 90);
        Destroy(fala);

        NewFala ("raposa5", player.transform);
        //Debug.Log("Já disse que não roubei suas nozes!");
        frame = 0;
        yield return new WaitUntil (() => frame >= 120);
        Destroy(fala);

        NewFala ("raposa6", player.transform);
        //Debug.Log("Também estou atrás de algo que levaram de mim.");
        frame = 0;
        yield return new WaitUntil (() => frame >= 150);
        Destroy(fala);

        NewFala ("esquilo4", boss.transform);
        //Debug.Log("Se é assim, eu vi quando o ladrão fugiu para a floresta.");
        frame = 0;
        yield return new WaitUntil (() => frame >= 150);
        Destroy(fala);

        NewFala ("raposa7", player.transform);
        //Debug.Log("Eu vou atrás dele! Preciso recuperar minhas frutas.");
        frame = 0;
        yield return new WaitUntil (() => frame >= 150);
        Destroy(fala);

        NewFala ("esquilo5", boss.transform);
        //Debug.Log("Boa sorte.");
        frame = 0;
        yield return new WaitUntil (() => frame >= 90);
        Destroy(fala);

        GameObject barreira = GameObject.Find("/Barreira 2");
        barreira.GetComponent<BoxCollider2D>().isTrigger = true;

        float y;
        float z;
        y = camera.maxCameraPos.y;
        z = camera.maxCameraPos.z;
        camera.maxCameraPos = new Vector3 (34, y, z);
        
        cutscene = false;
    }

    void SetCheckpoint () {
        if (player != null) {
            if (!checkpoint) {
                if (playerPosX >= checkpointCoord.x) {
                    checkpoint = true;
                }
            } else {
                if (playerPosX>= checkpointCoordBoss.x) {
                    checkpointBoss = true;
                }
            }
        }
    }
}
