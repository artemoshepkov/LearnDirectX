﻿using LearnDirectX.src.Common.EngineSystem;
using System.Linq;

namespace LearnDirectX.src.Common.Components
{
    public class SliceRenderer : Component
    {
        private bool _sliceI;
        private bool _sliceJ;
        private bool _sliceK;

        private int _i;
        private int _j;
        private int _k;

        public bool SliceI
        {
            get
            {
                return _sliceI;
            }
            set
            {
                _sliceI = value;
                UpdateSlices();
            }
        }
        public bool SliceJ
        {
            get
            {
                return _sliceJ;
            }
            set
            {
                _sliceJ = value;
                UpdateSlices();
            }
        }
        public bool SliceK
        {
            get
            {
                return _sliceK;
            }
            set
            {
                _sliceK = value;
                UpdateSlices();
            }
        }

        public int I
        {
            get
            {
                return _i;
            }
            set
            {
                _i = value;
                if (SliceI)
                    UpdateSlices();
            }
        }
        public int J
        {
            get
            {
                return _j;
            }
            set
            {
                _j = value;
                if (SliceJ)
                    UpdateSlices();
            }
        }
        public int K
        {
            get
            {
                return _k;
            }
            set
            {
                _k = value;
                if (SliceK)
                    UpdateSlices();
            }
        }

        public SliceRenderer()
        {
            _i = _j = _k = 0;
            _sliceI = false;
            _sliceJ = false;
            _sliceK = false;
        }

        public void UpdateSlices()
        {
            var quads = Owner.Children;

            if (!(SliceI || SliceJ || SliceK))
            {
                foreach (var quad in quads)
                {
                    quad.IsEnabled = true;
                }

                return;
            }

            foreach (var quad in quads)
            {
                var quadIndex = quad.GetComponent<QuadIndex>();
                if (SliceI && quadIndex.I == I || SliceJ && quadIndex.J == J || SliceK && quadIndex.K == K)
                {
                    quad.IsEnabled = true;
                }
                else
                {
                    quad.IsEnabled = false;
                }
            }
        }
    }
}
