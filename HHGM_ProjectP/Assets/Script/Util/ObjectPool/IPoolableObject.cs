using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectP.Util
{
    /// <summary>
    /// ������Ʈ�� ���� �� �� ������ ��Ÿ���� �������̽�
    /// ������Ʈ Ǯ������ ������ ������Ʈ���� ������ ���ϰ� �־����
    /// </summary>
    public interface IPoolableObject
    {
        bool CanReCycle { get; set; }
    }
}
