using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _options;
    [SerializeField] private GameObject _levels;
    [SerializeField] private GameObject _playground;
    [SerializeField] private GameObject _about;

    [SerializeField] private List<SceneAsset> _scenes;


    private void Update()
    {
        CheckUserPosition();
    }

    private void CheckInput()
    {
        if (Input.GetKeyDown("Space"))
        {

        }
    }

    private void CheckUserPosition()
    {

    }

    private void LoadScene(int sceneId)
    {

    }

}
