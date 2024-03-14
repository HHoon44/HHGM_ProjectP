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
    /// ���ӿ� ���Ǵ� ��� ��Ʋ�󽺸� �����ϴ� Ŭ����
    /// </summary>
    public static class SpriteLoader
    {
        private static Dictionary<AtlasType, SpriteAtlas> atlasDic = new Dictionary<AtlasType, SpriteAtlas>();

        /// <summary>
        /// ��Ʋ�� ����� ��ųʸ��� �����ϴ� �޼���
        /// </summary>
        /// <param name="atlases"></param>
        public static void SetAtlas(SpriteAtlas[] atlases)
        {
            for (int i = 0; i < atlases.Length; i++)
            {
                // Ű ������ ����ϱ� ���ؼ� ����ȯ �۾�
                var key = (AtlasType)Enum.Parse(typeof(AtlasType), atlases[i].name);

                atlasDic.Add(key, atlases[i]);
            }
        }

        /// <summary>
        /// ��Ʋ�� ����ҿ��� ��û�� ��Ʈ����Ʈ�� ��ȯ���ִ� �޼���
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