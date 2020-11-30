using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutInController : MonoBehaviour
{

    private SugorokuDirector director;

    //private GameObject[] prefabs;

    private GameObject activeObject;

    private int cutInID;

    private LimitTimeCounter activeTime;

    public bool IsFinished { get; private set; }

    [SerializeField]
    private List<CutInData> CutInList;

    [SerializeField]
    private List<CutInSoundData> CutInSoundList;

    private AudioSource[] audios;

    // Start is called before the first frame update

    void Awake()
    {

    }
    void Start()
    {
        //this.Open();
        this.activeTime = new LimitTimeCounter();
        this.IsFinished = true;


        this.audios = new AudioSource[this.CutInSoundList.Count];

        //foreach (CutInSoundData soundData in this.CutInSoundList)
        for(int audioID=0; audioID< this.CutInSoundList.Count; audioID++)
        {

            CutInSoundData soundData = this.CutInSoundList[audioID];

            AudioSource audioSource;
            audioSource = this.gameObject.AddComponent<AudioSource>();
            audioSource.clip = soundData.AudioClip;
            this.audios[audioID] = audioSource;
        }
    }

    public void CutInNextStart()
    {
        /*
        if (this.activeObject != null)
            GameObject.Destroy(this.activeObject);
        */

        //this.activeObject = Instantiate(this.prefabs[this.cutInID]);
        this.activeObject = Instantiate(this.CutInList[cutInID].Prefab, Vector3.zero, Quaternion.identity);
        this.activeObject.transform.SetParent( this.gameObject.transform , false );
        this.activeObject.SetActive(true);
        //this.activeAnimator = this.activeObject.GetComponent<Animator>();

        //this.activeTime.Start(1.5f);
        this.activeTime.Start( this.CutInList[cutInID].Time );

        this.cutInID++;
    }

    public void Open()
    {
        this.director = SugorokuDirector.GetInstance();
        //this.prefabs = director.Resource.PrefabsCutIn;

        this.activeTime = new LimitTimeCounter();

        this.activeObject = null;
        this.cutInID = 0;
        this.IsFinished = false;


        this.cutInID = 0;

        Debug.Log("count " + this.CutInList.Count);
        if (this.CutInList.Count > 0)
            this.CutInNextStart();
        else
            this.Close();
    }

    public void Close()
    {
        if (this.activeObject != null)
            GameObject.Destroy(this.activeObject);

        this.IsFinished = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (!this.IsFinished)
        {
            this.activeTime.Update();

            if (this.activeTime.IsFinished)
            {
                //if (this.cutInID < this.prefabs.Length)
                if (this.cutInID < this.CutInList.Count)
                    this.CutInNextStart();
                else
                    this.Close();
            }
        }
    }

    public void SoundPlay( string requestName )
    {
        for( int audioID=0; audioID<this.CutInSoundList.Count; audioID++)
        {
            CutInSoundData soundData = this.CutInSoundList[audioID];

            if( soundData.Name.Equals(requestName))
                this.audios[audioID].Play();
        }
    }
}
