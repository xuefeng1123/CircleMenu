using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA;
using UnityEngine.XR.WSA.Persistence;

public class WorldAnchorScript : MonoBehaviour
{
    public WorldAnchorStore store;
    public WorldAnchor savedRoot;
    public GameObject rootGameObject; 
    // Start is called before the first frame update
    void Start()
    {
        rootGameObject = GameObject.Find("Sphere");
        WorldAnchorStore.GetAsync(StoreLoaded);
    }

    private void StoreLoaded(WorldAnchorStore store)
    {
        this.store = store;
    }
    public void SaveGame()
    {
        WorldAnchor anchor = GameObject.Find("Cube").AddComponent<WorldAnchor>();
        // Save data about holograms positioned by this world anchor
        this.store.Save("rootGameObject", anchor);
        Destroy(GameObject.Find("Cube").GetComponent<WorldAnchor>());
    }
    // Update is called once per frame
    public void LoadGame()
    {
        // Save data about holograms positioned by this world anchor
        this.savedRoot = this.store.Load("rootGameObject", GameObject.Find("Cube"));
        if (!this.savedRoot)
        {
            // We didn't actually have the game root saved! We have to re-place our objects or start over
            print("no root");
        }
    }

    private void listAllAnchor()
    {
        string[] ids = this.store.GetAllIds();
        for (int index = 0; index < ids.Length; index++)
        {
            Debug.Log(ids[index]);
        }
    }

}
