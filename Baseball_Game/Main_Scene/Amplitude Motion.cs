using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NewBehaviourScript : MonoBehaviour
{
    // 往復する長さ
    [SerializeField] private float _length = 1;
    [SerializeField] private float x_com;
    [SerializeField] private float z_com;
    float x;
    float y;
    float z;

    // Start is called before the first frame update
    void Start()
    {
        Transform myTransform = this.transform;
        Vector3 worldPos = myTransform.position;
        x = worldPos.x;
        y = worldPos.y;
        z = worldPos.z;
    }


    private void Update()
    {
        var amplitude = _length; // 振幅
        var period = 2; // 一往復する周期（秒）
        var t = 4 * amplitude * Time.time / period; // 時間の進行速度を調整

        // 指定された振幅と周期のPingPong
        var value = Mathf.PingPong(t, 2 * amplitude) - amplitude;

        transform.localPosition = new Vector3(x + x_com * value, y, z + z_com * value);
    }
}
