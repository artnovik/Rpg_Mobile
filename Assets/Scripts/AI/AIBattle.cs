using TDC;
using UnityEngine;
using UnityEngine.AI;

public class AIBattle : MonoBehaviour
{
    public NavMeshAgent agent;
    private bool chasedRecently;

    public EnemyUI enemyUI;

    public bool isBattle;
    public Locomotion locomotion;
    public Transform target;
    public CoreTrigger viewTrigger;

    public float delayAttack = 1.2f;
    private float currentTimerAttack = 0;

    public float intervalBlock = 0f;
    private float currentIntervalBlock = 0;

    #region Unity

    private void Start()
    {
        locomotion = GetComponent<Locomotion>();
        locomotion.animator.SetBool("EnemyZombie", true);
        SetRagdoll(false);
        enemyUI = GetComponent<EnemyUI>();
    }

    private void Update()
    {
        ViewControl();

        if(target && !target.gameObject.activeSelf)
        {
            target = PlayerData.Instance.locomotion.transform;
        }

        if (target && !target.gameObject.GetComponent<Health>().isDead)
        {
            PlayerData.Instance.inBattle = true;
            chasedRecently = true;

            enemyUI.SetHealthBarStatus(true);
            agent.SetDestination(target.position);
            agent.isStopped = false;
            isBattle = true;
        }
        else
        {
            if (chasedRecently)
            {
                PlayerData.Instance.inBattle = false;
                chasedRecently = false;
            }

            enemyUI.SetHealthBarStatus(false);
            agent.isStopped = true;
            isBattle = false;
        }

        Locomotion();
    }

    #endregion

    #region Core

    private void Locomotion()
    {
        if (!target)
        {
            // ToDo: Patrolling fix related to this.
            locomotion.Movement(Vector3.zero);
            return;
        }

        Vector3 fixDirection = Vector3.zero;

        if (Vector3.Distance(target.position, transform.position) >= agent.stoppingDistance)
        {
            fixDirection = (agent.steeringTarget - transform.position).normalized;
            locomotion.Rotate(fixDirection);
            locomotion.targetLocomotion = null;
            currentTimerAttack = delayAttack;
        }
        else if (target.GetComponent<Health>().currentHealth > 0)
        {
            locomotion.targetLocomotion = target.GetComponent<Locomotion>();

            if(locomotion.typeLocomotion != global::Locomotion.TLocomotion.Attack)
            {
                if (currentIntervalBlock > 0)
                {
                    currentIntervalBlock -= Time.deltaTime;
                    locomotion.animator.SetBool("Block", true);
                }
                else
                {
                    currentTimerAttack += Time.deltaTime;
                    locomotion.animator.SetBool("Block", false);
                }
            }

            if (currentTimerAttack >= delayAttack)
            {
                locomotion.AttackControl();
                currentTimerAttack = 0;

                if(Random.Range(0, 101) >= 80)
                {
                    currentIntervalBlock = Random.Range(3, 6);
                }
            }
        }

        locomotion.Movement(fixDirection);
    }

    private void ViewControl()
    {
        foreach (Transform tar in viewTrigger.listObject)
            if (tar)
            {
                target = tar;
                locomotion.targetLocomotion = target.GetComponent<Locomotion>();
                return;
            }

        locomotion.targetLocomotion = null;
        target = null;
    }

    private void SetKinematic(bool newValue)
    {
        var bodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in bodies) rb.isKinematic = newValue;
    }

    public void SetRagdoll(bool value)
    {
        SetKinematic(!value);
        GetComponent<Animator>().enabled = !value;
    }

    #endregion
}