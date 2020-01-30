using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Para suavizar el movimiento
    public float smoothing = 0.5f;
    
    // Objetivos que puede seguir la camara
    public Transform[] targets;
    // Objetivo actual que sigue la camara
    Transform target;
    // Switch para cambiar de personaje
    public bool characterSwitch = true;
    // Semaforo para evitar saltos al cambiar de personaje
    public bool wait = false;

    // Start is called before the first frame update
    void Start(){
        target = targets[1];
    }
    
    // Update is called once per frame
    void LateUpdate(){
        ChangeCharacter();
    }


    void FixedUpdate(){
        CameraMovement();
    }

    void ChangeCharacter(){
        target = targets[characterSwitch ? 1 : 0];

        if (Input.GetButtonUp("L Trigger"))
            wait = false;

        if (Input.GetButton("L Trigger") && !wait){
            characterSwitch = !characterSwitch;
            wait = true;
        }
    }
    void CameraMovement(){
        if(transform.position != target.position){
            // Primero asigno la posicion objetivo a un vector,
            // conservando la posicion Y de la camara
            Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
            // Hace una transformacion suave entre la posicion actual
            // y la posicion destino
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
    }
}
