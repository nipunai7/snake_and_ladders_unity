using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public Route route;
    public List<Node> nodeList = new List<Node>();

    int routePosition;
    int stoneId;

    float speed = 8f;

    int stepsToMove;
    public int doneSteps;

    bool isMoving;

    float cTime=0;
    float amplitute=0.5f;

    void Start()
    {
        
        foreach (Transform c in route.nodeList)
        {
            Node n = c.GetComponentInChildren<Node>();
            if(n!=null)
            {
                nodeList.Add(n);
            }
        }
    }

    IEnumerator Move()
    {
        if(isMoving)
        {
            yield break;
        }

        isMoving = true;

        nodeList[routePosition].RemoveStone(this);

        while(stepsToMove>0)
        {
            routePosition++;
            Vector3 nextPos = route.nodeList[routePosition].transform.position;

            Vector3 startPos = route.nodeList[routePosition-1].transform.position;
            while(MoveInArc(startPos, nextPos, 4f)){yield return null;}

            // while(MovetoNext(nextPos)){yield return null;}

            yield return new WaitForSeconds(0.1f);

            cTime = 0;
            stepsToMove--;
            doneSteps++;

        }

        yield return new WaitForSeconds(0.2f);

        if (nodeList[routePosition].connectionNode != null)
        {
            int connectionNodeId = nodeList[routePosition].connectionNode.nodeId;
            Vector3 nextPos = nodeList[routePosition].connectionNode.transform.position;
            
            while(MovetoNext(nextPos)) {yield return null;}
            doneSteps = connectionNodeId;
            routePosition = connectionNodeId;
        }

        nodeList[routePosition].AddStone(this);

        if(doneSteps == nodeList.Count-1)
        {
            GameManager.instance.ReportWinner();
            yield break;
        }

        GameManager.instance.state = GameManager.States.SWITCH_PLAYER;

        isMoving = false;
        //Debug.Log("CurrentPos: " + doneSteps);
        GameManager.instance.LaunchScene(doneSteps);
        //CurrentNode();
        /*    if (doneSteps == 4)
        {
            SceneManager.LoadScene("snake");
        }*/

    }

    bool MovetoNext(Vector3 nextPos)
    {
        return nextPos != (transform.position = Vector3.MoveTowards(transform.position,nextPos,speed*Time.deltaTime));
    }

    bool MoveInArc(Vector3 startPos, Vector3 nextPos, float _speed)
    {
        cTime += _speed * Time.deltaTime;
        Vector3 myPos = Vector3.Lerp(startPos,nextPos,cTime);
        myPos.y += amplitute*Mathf.Sin(Mathf.Clamp01(cTime)*Mathf.PI);

        return nextPos != (transform.position = Vector3.Lerp(transform.position, myPos,cTime));
    }

    public void MakeTurn(int diceNumber)
    {
        stepsToMove = diceNumber;
        if(doneSteps+stepsToMove < route.nodeList.Count)
            {
                StartCoroutine(Move());
            }
            else
            {
                print("Nodes Exceeded");
                GameManager.instance.state = GameManager.States.SWITCH_PLAYER;
            }

    }

    public int CurrentNode()
    {
        return doneSteps;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag== "Stone")
        {
            Debug.Log("Another Stone");
        }
    }
}
