using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.BehaviorTrees;
using UnityEngine;

public class BossController : MonoBehaviour
{

    private BehaviorTree tree;
    private MovementController controller;
    private GameObject player;
    private int curNode;
    private AIBehaviors behaviors;
    public float speed;
    public float ApproachDist;
    public float ShortAttackDist;
    public float LongAttackDist;

    public string[] Attacks;
    public float AITickRate = 0.5f;

    private float curTick;

    private GameObject _player;
    private GameObject _win;
    private GameObject _end;
    private GameObject Boos;

    // Use this for initialization
    void Start()
    {
        behaviors = GetComponent<AIBehaviors>();
        curTick = AITickRate;
        tree = new BehaviorTree(gameObject).AddNode((int)BTNodeTypes.Selector, 0).
                                                AddNode((int)BTNodeTypes.Parrallel, 1).
                                                    AddNode((int)BTNodeTypes.IgnoreStat, 2, new IgnoreData(NodeStatus.Success)).
                                                        AddNode((int)BTNodeTypes.WithinInRange, 3, new RangeData(ApproachDist, 100000)).
                                                    AddNode((int)BTNodeTypes.Idle, 2).
                                                AddNode((int)BTNodeTypes.Parrallel, 1).
                                                    AddNode((int)BTNodeTypes.IgnoreStat, 2, new IgnoreData(NodeStatus.Success)).
                                                        AddNode((int)BTNodeTypes.WithinInRange, 3, new RangeData(LongAttackDist, ApproachDist)).
                                                    AddNode((int)BTNodeTypes.ApproachTarget, 2).
                                                AddNode((int)BTNodeTypes.Parrallel, 1).
                                                    AddNode((int)BTNodeTypes.IgnoreStat, 2, new IgnoreData(NodeStatus.Success)).
                                                        AddNode((int)BTNodeTypes.WithinInRange, 3, new RangeData(ShortAttackDist, LongAttackDist)).
                                                    AddNode((int)BTNodeTypes.Forever, 2).
                                                        AddNode((int)BTNodeTypes.RandomSeq, 3).
                                                            AddNode((int)BTNodeTypes.AttackTrig, 4, new AttackData("SweepLong")).
                                                            AddNode((int)BTNodeTypes.AttackTrig, 4, new AttackData("Quick1")).
                                                AddNode((int)BTNodeTypes.Parrallel, 1).
                                                    AddNode((int)BTNodeTypes.IgnoreStat, 2, new IgnoreData(NodeStatus.Success)).
                                                        AddNode((int)BTNodeTypes.WithinInRange, 3, new RangeData(0, ShortAttackDist)).
                                                    AddNode((int)BTNodeTypes.Forever, 2).
                                                        AddNode((int)BTNodeTypes.RandomSeq, 3).
                                                            AddNode((int)BTNodeTypes.AttackTrig, 4, new AttackData("JumpBack")).
                                                            AddNode((int)BTNodeTypes.AttackTrig, 4, new AttackData("SweepShort"));
        
        player = GameObject.Find("Player");
        tree.Initialize();
        controller = GetComponent<MovementController>();
        GameObject.Find("BossText").GetComponent<CanvasRenderer>().SetAlpha(0);

        _win = GameObject.Find("WinPan");
        _end = GameObject.Find("EndPanel");
        Boos = GameObject.Find("BossPan");
        _win.SetActive(false);
        _end.SetActive(false);
        Boos.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Health>().MARKEDFORDEATH)
        {
            return;
        }
        if (player == null || Time.timeScale < 0.001f)
        {
            return;
        }
        curTick -= Time.deltaTime;
        if (curTick <= 0)
        {
            BehaviorTreeSystem.Update(tree);
            curTick = AITickRate;
        }
        behaviors.Target = player.transform.position;

        var move = (behaviors.MovementTarget - transform.position).normalized;
        if ((behaviors.Target - transform.position).sqrMagnitude < ApproachDist * ApproachDist &&
            !player.GetComponent<AudioSource>().isPlaying)
        {
            player.GetComponent<AudioSource>().Play();
            Boos.SetActive(true);
            GameObject.Find("BossText").GetComponent<CanvasRenderer>().SetAlpha(1);
            GameObject.Find("BossHealthPanel").GetComponent<HealthBar>().HideWhenFull = false;
        }
       
        if (!GetComponent<CombatController>().IsAttacking())
        {
            var foo = player.transform.position;

            foo.y = transform.position.y;

            transform.LookAt(foo);
        }
        controller.MoveDir(move * speed);
    }

    void OnDestroy()
    {
        if (!GameObject.Find("EventSystem"))
        {
            return;
        }
        _win.SetActive(true);
        _end.SetActive(true);
        GameObject.Find("EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(GameObject.Find("Restart"));
        GameObject.Find("Player").GetComponent<PlayerController>().Stap = true;
    }
}
