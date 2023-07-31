using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class M_GhostProjection : Singleton<M_GhostProjection>
{
    private Scene _simulationScene;
    private PhysicsScene _physicsScene;
    [SerializeField] private Transform _obstaclesParent;
    [SerializeField] private Transform _otherParent;
    [SerializeField] private LineRenderer _line;
    [SerializeField] private int _maxPhysicsFrameIterations = 100;

    void Start()
    {
        CreatePhysicsScene();
    }

    void CreatePhysicsScene()
    {
        _simulationScene = SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
        _physicsScene = _simulationScene.GetPhysicsScene();

        CreateSceneElements("Obstacles", _obstaclesParent);
        CreateSceneElements("Others", _otherParent);

        void CreateSceneElements(string parentsName, Transform parentTrans)
        {
            GameObject parent = new GameObject(parentsName);

            foreach (Transform obj in parentTrans)
            {
                var ghostObj = Instantiate(obj.gameObject, obj.localPosition, obj.rotation, parent.transform);
                //ghostObj.transform.localScale = obj.localScale;
                ghostObj.GetComponent<Renderer>().enabled = false;
                //SceneManager.MoveGameObjectToScene(ghostObj, _simulationScene);
            }

            parent.transform.position = _obstaclesParent.position;
            parent.transform.rotation = _obstaclesParent.rotation;
            parent.transform.localScale = _obstaclesParent.localScale;
            SceneManager.MoveGameObjectToScene(parent, _simulationScene);
        }
    }

    public void SimulateTrajectory(GameObject ballPrefab,Vector3 pos,Vector3 velocity) 
    {
        var ghostObj = Instantiate(ballPrefab, pos, Quaternion.identity);
        SceneManager.MoveGameObjectToScene(ghostObj, _simulationScene);
        ghostObj.GetComponent<O_GhostBall>().Shoot(velocity);
        _line.positionCount = _maxPhysicsFrameIterations;
        for (int i = 0; i < _maxPhysicsFrameIterations; i++)
        {
            _physicsScene.Simulate(Time.fixedDeltaTime);
            _line.SetPosition(_maxPhysicsFrameIterations - 1 - i, ghostObj.transform.position);
        }
        Destroy(ghostObj.gameObject);
    }

    public void EraseTrajectory()
    {
        StartCoroutine(TrajectoryErase());
    }

    IEnumerator TrajectoryErase()
    {
        int count = _line.positionCount;
        for (int i = 1; i < count; i++)
        {
            yield return new WaitForSeconds(0.01f);
            if(_line.positionCount>0)_line.positionCount--;
        }
    }
}
