using UnityEngine;
using UnityEngine.U2D;

public class SoftBody : MonoBehaviour
{
    [SerializeField] private  float _splineOffset = 0.5f;
    [SerializeField] private SpriteShapeController _spriteShape;
    [SerializeField] private Transform[] _points;

    private void Awake()
    {
        UpdateVerticies();
    }

    private void Update()
    {
        UpdateVerticies();
    }

    private void UpdateVerticies()
    {
        for (int i = 0; i < _points.Length - 1; i++)
        {
            Vector2 vertex = _points[i].localPosition;
            Vector2 towardsCenter = (Vector2.zero - vertex).normalized;
            float colliderRadius = _points[i].gameObject.GetComponent<CircleCollider2D>().radius;
            try
            {
                _spriteShape.spline.SetPosition(i, (vertex - towardsCenter * colliderRadius));
            }
            catch
            {
                Debug.Log("Spline points are too close to each other, recalculate");
                _spriteShape.spline.SetPosition(i, (vertex - towardsCenter * (colliderRadius + _splineOffset)));
            }

            Vector2 lt = _spriteShape.spline.GetLeftTangent(i);//left tangent
            Vector2 newRt = Vector2.Perpendicular(towardsCenter) * lt.magnitude;//right tangent
            Vector2 newLt = Vector2.zero - newRt;//new tangent 

            _spriteShape.spline.SetRightTangent(i, newRt);
            _spriteShape.spline.SetLeftTangent(i, newLt);
        }
    }
}
