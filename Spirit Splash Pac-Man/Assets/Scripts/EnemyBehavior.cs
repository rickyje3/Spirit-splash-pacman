using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Makes it so you can't have a stray enemy behavior it has to go here
public abstract class EnemyBehavior : MonoBehaviour
{
    public EnemyScript enemy { get; private set; }
    public float duration;


    // Start is called before the first frame update
    private void Start()
    {
        enemy = GetComponent<EnemyScript>();
        enabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void Enable()
    {
        Enable(duration);
    }

    //Virtual allows you to override the function if needed.
    public virtual void Enable(float duration)
    {
        enabled = true;

        CancelInvoke();
        //Whenever a behavior is activated it's set to a timer but it should re enable if reactivated
        Invoke(nameof(Disable), duration);
    }

    public virtual void Disable()
    {
        enabled = false;
        CancelInvoke();
    }
}
