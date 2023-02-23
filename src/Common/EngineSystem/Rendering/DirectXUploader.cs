using System.Collections.Generic;

namespace LearnDirectX.src.Common.EngineSystem.Rendering
{
    public abstract class DirectXUploader
    {
        public abstract void Upload<T>(T obj);
    }

    public class CameraContextUploader : DirectXUploader
    {
        public override void Upload<T>(T obj)
        {
            throw new System.NotImplementedException();
        }
    }

    public class SetUploader
    {
        private SetUploader _instance;
        private List<DirectXUploader> _uploaders;

        public SetUploader Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SetUploader();
                }
                return _instance;
            }
        }

        private SetUploader() { }
    
        
    }
}
