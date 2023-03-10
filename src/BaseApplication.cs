using SharpDX.DirectInput;
using System.Collections.Generic;
using System;
using LearnDirectX.src.Common.EngineSystem;

namespace LearnDirectX.src
{
    public abstract class BaseApplication
    {
        protected Dictionary<Key, Action> _controlBinds;

        protected virtual void Update()
        {
            foreach (var bindKey in _controlBinds.Keys)
            {
                if (KeyboardInput.GetKeyDown(bindKey))
                {
                    _controlBinds[bindKey]();
                }
            }
        }
    }
}
