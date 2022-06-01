using UnityEngine;

public class NFTShapeFactory : ScriptableObject
{
    [SerializeField] GameObject[] loaderControllersPrefabs;

    static NFTShapeFactory instance = null;
    public static NFTShapeFactory i
    {
        get
        {
            if (instance == null)
            {
                instance = ABEYController.i.OtherRefs.NFTShapeFactory;
            }
            return instance;
        }
    }

    public static GameObject InstantiateLoaderController(int index)
    {
        if (i != null && index >= 0 && index < i.loaderControllersPrefabs.Length)
        {
            return Object.Instantiate(i.loaderControllersPrefabs[index]);
        }
        return Object.Instantiate(ABEYController.i.GetPrefab("NFTShapeLoader_Classic") as GameObject);
    }

    private NFTShapeFactory() { }
}