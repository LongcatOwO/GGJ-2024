using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {
  Camera _camera;

  void Awake() { _camera = Camera.main; }

  // Start is called before the first frame update
  void Start() {}

  // Update is called once per frame
  void Update() { transform.rotation = _camera.transform.rotation; }
}
