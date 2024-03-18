using ProejctP.UI;
using ProjectP.Util;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectP.UI
{
    public class UIWindowManager : Singleton<UIWindowManager>
    {
        private List<UIWindow> totalOpenWindows = new List<UIWindow>();

        private List<UIWindow> totalUIWindows = new List<UIWindow>();

        private Dictionary<string, UIWindow> cachedTotalUIwindowDic = new Dictionary<string, UIWindow>();

        private Dictionary<string, object> cachedInstanceDic = new Dictionary<string, object>();

        public void Initialzie()
        {
            InitAllWindow();
        }

        public void InitAllWindow()
        {
            for (int i = 0; i < totalUIWindows.Count; i++)
            {
                if (totalUIWindows[i] != null)
                {
                    totalUIWindows[i].InitWindow();
                }
            }
        }

        public void AddTotalWindow(UIWindow uiWindow)
        {
            var key = uiWindow.GetType().Name;

            bool hasKey = false;

            if (totalUIWindows.Contains(uiWindow) || cachedTotalUIwindowDic.ContainsKey(key))
            {
                if (cachedTotalUIwindowDic[key] != null)
                {
                    return;
                }
                else
                {
                    hasKey = true;

                    for (int i = 0; i < totalUIWindows.Count; i++)
                    {
                        if (totalUIWindows[i] == null)
                        {
                            totalUIWindows.RemoveAt(i);
                        }
                    }
                }
            }

            totalUIWindows.Add(uiWindow);

            if (hasKey)
            {
                cachedInstanceDic[key] = uiWindow;
            }
            else
            {
                cachedTotalUIwindowDic.Add(key, uiWindow);
            }
        }

        public T GetWindow<T>() where T : UIWindow
        {
            string key = typeof(T).Name;

            if (!cachedTotalUIwindowDic.ContainsKey(key))
            {
                return null;
            }

            if (!cachedInstanceDic.ContainsKey(key))
            {
                cachedInstanceDic.Add(key, (T)Convert.ChangeType(cachedTotalUIwindowDic[key], typeof(T)));
            }
            else if (cachedInstanceDic[key].Equals(null))
            {
                cachedInstanceDic[key] = (T)Convert.ChangeType(cachedTotalUIwindowDic[key], typeof(T));
            }

            return (T)cachedInstanceDic[key];

        }

        public void AddOpenWindow(UIWindow uiWindow)
        {
            if (!totalOpenWindows.Contains(uiWindow))
            {
                totalOpenWindows.Add(uiWindow);
            }
        }

        public void RemoveOpenWindow(UIWindow uiWindow)
        {
            if (totalOpenWindows.Contains(uiWindow))
            {
                totalOpenWindows.Remove(uiWindow);
            }
        }
    }
}