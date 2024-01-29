using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class special_balloon_moving : MonoBehaviour
{
    [SerializeField] private float _length = 0.3f;
    float x;
    float y;
    float z;
    float switch_z;
    float advance_z;

    // Start is called before the first frame update
    void Start()
    {
        Transform myTransform = this.transform;
        Vector3 worldPos = myTransform.position;
        x = worldPos.x;
        y = worldPos.y;
        z = worldPos.z;
        switch_z = -z;
        advance_z = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        var amplitude = _length; // 振幅
        var period = 4; // 一往復する周期（秒）
        var t = 4 * amplitude * Time.time / period; // 時間の進行速度を調整

        // 指定された振幅と周期のPingPong
        float value = Mathf.PingPong(t, 2 * amplitude) - amplitude;

        z = z + advance_z;
        transform.localPosition = new Vector3(x, y + value, z);

        if (z > switch_z && advance_z > 0)
        {
            switch_z = -switch_z;
            advance_z = -advance_z;
        }

        else if (z < switch_z && advance_z < 0)
        {
            switch_z = -switch_z;
            advance_z = -advance_z;
        }


    }
}