using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectP.Util
{
    /// <summary>
    /// 오브젝트가 재사용 될 수 있음을 나타내는 인터페이스
    /// 오브젝트 풀링으로 관리할 오브젝트들은 무조건 지니고 있어야함
    /// </summary>
    public interface IPoolableObject
    {
        bool CanReCycle { get; set; }
    }
}
