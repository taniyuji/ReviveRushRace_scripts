using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class SmokeController : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem smoke;

    private List<ParticleSystem> smokes = new List<ParticleSystem>();


    // Start is called before the first frame update

    void Start()
    {
        smokes.Add(smoke);

        for (int i = 0, amount = ResourceProvider.i.characters.Count; i < amount; i++)
        {
            if(i != 0) smokes.Add(Instantiate(smoke, transform));

            SetSubscribe(i);
        }
    }

    private void SetSubscribe(int i)
    {
        ResourceProvider.i.characters[i].collide.Subscribe(pos =>
      {
          if (smokes[i].isPlaying) return;

          smokes[i].gameObject.transform.position = pos;

          smokes[i].Play();

          SoundManager.i.PlayOneShot(6, 0.1f);
      });
    }
}
