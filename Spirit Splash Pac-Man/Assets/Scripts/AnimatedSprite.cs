using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; }
    public Sprite[] sprites;
    public float animationTime = 0.25f;
    //way of knowing which index we're currently on
    public int animationFrame { get; private set; }
    //Animation will almost always loop
    public bool loop = true;

    private void Awake()
    {
        //Allows you to call this.spriteRenderer in other scripts to call it rather than GetComponent every time
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {   //Repeatedly call Advance every 0.25 seconds. First is initial time, second is every time after.
        InvokeRepeating(nameof(Advance), this.animationTime, this.animationTime);
    }

    private void Advance()
    {
        this.animationFrame++;
        //this means the animation has overflowed
        if (this.animationFrame >= this.sprites.Length)
        {
            //start from frame 0
            this.animationFrame = 0;
        }

        //Checks that animation frame is in the range of the array (greater than or equal to 0 and less than the sprites array length
        if (this.animationFrame >= 0 && this.animationFrame < this.sprites.Length)
        {
            //equals the current frame
            this.spriteRenderer.sprite = this.sprites[this.animationFrame];
        }
    }

    public void ResetAnimation()
    {
        //Restarts the animation by bringing it back and then running advance which has ++
        this.animationFrame = -1;

        Advance();
    }
}
