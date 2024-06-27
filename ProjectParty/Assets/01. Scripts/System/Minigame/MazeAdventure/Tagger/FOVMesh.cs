using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct ViewCastInfo
{
    public bool hit;
    public Vector3 point;
    public float distance;
    public float angle;
}

public struct EdgeInfo
{
    public Vector3 pointA;
    public Vector3 pointB;
}

public class FOVMesh : MonoBehaviour
{
    [Range(0, 360)] public float viewAngle; //각도
    public float viewRadius; //반경
    public float viewHeight;

    [SerializeField] private LayerMask _obstacleMask;

    [SerializeField] private float _meshResolution;
    [SerializeField] private int _edgeResolveIterations; //3회
    [SerializeField] private float _edgeDistanceThreshold;

    private MeshFilter _viewMeshFilter;
    private Mesh _viewMesh;

    private void Awake()
    {
        _viewMeshFilter = transform.GetComponent<MeshFilter>();
        _viewMesh = new Mesh();
        _viewMesh.name = "Sight_Mesh";
        _viewMeshFilter.mesh = _viewMesh;
    }

    public Vector3 DirFromAngle(float degree, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            degree += transform.eulerAngles.y; //로컬회전치라면 글로벌 회전치로 변경한다.
        }

        float rad = degree * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(rad), 0, Mathf.Cos(rad));
    }
    private void LateUpdate()
    {
        DrawFieldOfView();
    }

    private EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
    {
        float minAngle = minViewCast.angle;
        float maxAngle = maxViewCast.angle;

        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for (int i = 0; i < _edgeResolveIterations; ++i)
        {
            float angle = (minAngle + maxAngle) * 0.5f; //절반으로 이분
            var castInfo = ViewCast(angle); //새롭게 캐스트를 쏜다.

            //거리가 지정된 쓰레스 홀드보다 커졌는가?
            bool distanceExceed = Mathf.Abs(minViewCast.distance - castInfo.distance) > _edgeDistanceThreshold;

            //피격상태가 이전상태와 동일하고 거리도 일정거리이내라면
            if (castInfo.hit == minViewCast.hit && !distanceExceed)
            {
                minAngle = angle;
                minPoint = castInfo.point;
            }
            else
            {
                maxAngle = angle;
                maxPoint = castInfo.point;
            }
        }

        return new EdgeInfo { pointA = minPoint, pointB = maxPoint };
    }

    private void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * _meshResolution);
        float stepAngleSize = viewAngle / stepCount;

        Vector3 pos = transform.position;

        List<Vector3> viewPoints = new List<Vector3>();
        var oldViewCastInfo = new ViewCastInfo();
        for (int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle * 0.5f + stepAngleSize * i;
            Debug.DrawLine(pos, pos + DirFromAngle(angle, true) * viewRadius, Color.red);

            var info = ViewCast(angle);

            if (i > 0)
            {
                //이미 한번 쏜 기록이 old에 남아있을꺼니까 여기서부터 비교를 하면 된다.
                bool distanceExceeded = Mathf.Abs(oldViewCastInfo.distance - info.distance) > _edgeDistanceThreshold;

                if (oldViewCastInfo.hit != info.hit || (oldViewCastInfo.hit && info.hit && distanceExceeded))
                {
                    var edge = FindEdge(oldViewCastInfo, info);

                    if (edge.pointA != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointA);
                    }

                    if (edge.pointB != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointB);
                    }
                }
            }

            viewPoints.Add(info.point);

            oldViewCastInfo = info; //이전인포에 현재 인포를 저장하자.
        }

        int vertCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertCount * 2];
        int[] triangles = new int[(vertCount - 2) * 2 * 3 + 12 + (viewPoints.Count-1) * 6]; // 이게 뭘 뜻하는 걸까?

        vertices[0] = Vector3.zero;


        //하단 면을 그리는 곳
        for (int i = 0; i < vertCount - 1; i++)
        {
            //정점을 넣어주고
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            if (i < vertCount - 2)
            {
                int tIndex = i * 3;
                triangles[tIndex + 0] = 0;
                triangles[tIndex + 1] = i + 1;
                triangles[tIndex + 2] = i + 2;
            }
        }

        //하단 면 +viewHeight인 상단 면을 그리는 곳
        int topIndex = vertCount;
        vertices[topIndex] = Vector3.zero;

        for (int i = vertCount; i < vertCount * 2 - 1; i++)
        {
            vertices[i + 1] = vertices[i - vertCount + 1] + Vector3.up * viewHeight;

            if (i < vertCount * 2 - 2)
            {
                int tIndex = (i - 2) * 3;
                triangles[tIndex + 0] = topIndex;
                triangles[tIndex + 1] = i + 1;
                triangles[tIndex + 2] = i + 2;
            }
        }


        //하단 면과 상단 면 양옆을 연결하는 곳

        triangles[(vertCount - 2) * 2 * 3 + 0] = vertCount - 1;
        triangles[(vertCount - 2) * 2 * 3 + 1] = topIndex;
        triangles[(vertCount - 2) * 2 * 3 + 2] = vertCount * 2 - 1;

        triangles[(vertCount - 2) * 2 * 3 + 3] = vertCount - 1;
        triangles[(vertCount - 2) * 2 * 3 + 4] = 0;
        triangles[(vertCount - 2) * 2 * 3 + 5] = topIndex;


        triangles[(vertCount - 2) * 2 * 3 + 6] = 0;
        triangles[(vertCount - 2) * 2 * 3 + 7] = 1;
        triangles[(vertCount - 2) * 2 * 3 + 8] = vertCount;

        triangles[(vertCount - 2) * 2 * 3 + 9] = 1;
        triangles[(vertCount - 2) * 2 * 3 + 10] = vertCount + 1;
        triangles[(vertCount - 2) * 2 * 3 + 11] = vertCount;

        //하단 면과 상당 면의 끝 부분을 연결하는 곳

        for(int i = 0; i < viewPoints.Count-1; i++)
        {
            int tIndex = i * 6;
            triangles[(vertCount - 2) * 2 * 3 + 12 + tIndex] = i + 1;
            triangles[(vertCount - 2) * 2 * 3 + 12 + tIndex + 1] = i + 2;
            triangles[(vertCount - 2) * 2 * 3 + 12 + tIndex + 2] = i + vertCount + 1;

            triangles[(vertCount - 2) * 2 * 3 + 12 + tIndex + 3] = i + vertCount + 2;
            triangles[(vertCount - 2) * 2 * 3 + 12 + tIndex + 4] = i + vertCount + 1;
            triangles[(vertCount - 2) * 2 * 3 + 12 + tIndex + 5] = i + 2;
        }

        _viewMesh.Clear();
        _viewMesh.vertices = vertices;
        _viewMesh.triangles = triangles;
        _viewMeshFilter.mesh = _viewMesh;
        _viewMesh.RecalculateNormals();
    }
    private ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);

        if (Physics.Raycast(transform.position, dir, out RaycastHit hit, viewRadius, _obstacleMask))
        {
            return new ViewCastInfo
            {
                hit = true,
                point = hit.point,
                distance = hit.distance,
                angle = globalAngle
            };
        }
        else
        {
            return new ViewCastInfo
            {
                hit = false,
                point = transform.position + dir * viewRadius,
                distance = viewRadius,
                angle = globalAngle
            };
        }
    }
}
