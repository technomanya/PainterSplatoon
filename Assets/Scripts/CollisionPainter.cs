using UnityEngine;

public class CollisionPainter : MonoBehaviour{
    public Color paintColor;
    
    public float radius = 1;
    public float strength = 1;
    public float hardness = 1;

    private void OnCollisionStay(Collision other) {
        PaintableTexture p = other.collider.GetComponent<PaintableTexture>();
        if(p != null){
            Vector3 pos = other.contacts[0].point;
            PaintManager.instance.Paint(p, pos, radius, hardness, strength, paintColor);
        }
    }
}
