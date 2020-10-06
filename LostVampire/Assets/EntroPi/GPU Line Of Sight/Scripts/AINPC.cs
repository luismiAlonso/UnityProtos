using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class AINPC : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent m_NavAgent;

    private static System.Random m_Random;

    private static PlayerInfo m_PlayerInfo;
    private static bool viewPlayer = false;
   

    private void OnEnable()
    {
        m_PlayerInfo = GameObject.FindObjectOfType<PlayerInfo>();
    }

    // Use this for initialization
    private void Start()
    {
        if (null == m_Random)
            m_Random = new System.Random();

      //  m_NavAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    private void Update()
    {
       
    }

    private void LateUpdate()
    {
       checkPlayer();
    }

    private void checkPlayer()
    {
        if (m_PlayerInfo != null)
        {
            if (m_PlayerInfo.VisibleSources.Count > 0)
            {
                viewPlayer = true;
               // Debug.Log("te veo");
            }
            else
            {
                viewPlayer = false;
               // Debug.Log("No veo");

            }
        }
    }

    
}