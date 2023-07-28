using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class M_GhostProjection : Singleton<M_GhostProjection>
{
    private Scene _simulationScene;
    private PhysicsScene _physicsScene;
    [SerializeField] private Transform _obstaclesParent;
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

        GameObject parent = new GameObject("Obstacles");

        foreach (Transform obj in _obstaclesParent)
        {
            var ghostObj = Instantiate(obj.gameObject, obj.localPosition, obj.rotation, parent.transform);
            //ghostObj.transform.localScale = obj.localScale;
            //ghostObj.GetComponent<Renderer>().enabled = false;
            //SceneManager.MoveGameObjectToScene(ghostObj, _simulationScene);
        }

        parent.transform.position = _obstaclesParent.position;
        parent.transform.rotation = _obstaclesParent.rotation;
        parent.transform.localScale = _obstaclesParent.localScale;
        SceneManager.MoveGameObjectToScene(parent, _simulationScene);
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
            _line.SetPosition(i, ghostObj.transform.position);
        }
        Destroy(ghostObj.gameObject);
    }
}
