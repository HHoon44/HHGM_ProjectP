using ProjectP_Define;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.U2D;

namespace ProejctP_Resource
{
    /// <summary>
    /// 게임에 사용되는 모든 아틀라스를 관리하는 클래스
    /// </summary>
    public static class SpriteLoader
    {
        private static Dictionary<AtlasType, SpriteAtlas> atlasDic = new Dictionary<AtlasType, SpriteAtlas>();

        /// <summary>
        /// 아틀라스 목록을 딕셔너리에 저장하는 메서드
        /// </summary>
        /// <param name="atlases"></param>
        public static void SetAtlas(SpriteAtlas[] atlases)
        {
            for (int i = 0; i < atlases.Length; i++)
            {
                // 키 값으로 사용하기 위해서 형변환 작업
                var key = (AtlasType)Enum.Parse(typeof(AtlasType), atlases[i].name);

                atlasDic.Add(key, atlases[i]);
            }
        }

        /// <summary>
        /// 아틀라스 저장소에서 요청한 스트라이트를 반환해주는 메서드
        /// </summary>
        /// <param name="type"></param>
        /// <param name="spriteName"></param>
        /// <returns></returns>
        public static Sprite GetSprite(AtlasType type, string spriteName)
        {
            if (!atlasDic.ContainsKey(type))
            {
                return null;
            }

            return atlasDic[type].GetSprite(spriteName);
        }
    }
}