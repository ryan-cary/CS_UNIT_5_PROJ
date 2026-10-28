using UnityEngine;

public class Fruit : SliceableObject
{
    [SerializeField] private Color _gibletColor;

    public override void OnSliced()
    {
        // Destroy / Break apart fruit object
        // Spawn giblets
        // Add score
        Debug.Log(("slice!"));
    }

    public override void SliceEffect()
    {
        throw new System.NotImplementedException();
    }
}
