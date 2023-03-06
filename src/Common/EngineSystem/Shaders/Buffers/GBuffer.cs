using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace LearnDirectX.src.Common.EngineSystem.Shaders.Buffers
{
    public sealed class GBuffer : IDisposable
    {
        public List<Texture2D> RTs = new List<Texture2D>();
        public List<ShaderResourceView> SRVs = new List<ShaderResourceView>();
        public List<RenderTargetView> RTVs = new List<RenderTargetView>();

        public Texture2D DS0;
        public ShaderResourceView DSSRV;
        public DepthStencilView DSV;

        int width;
        int height;

        SampleDescription sampleDescription;
        SharpDX.DXGI.Format[] RTFormats;

        public GBuffer(int width, int height, SampleDescription sampleDesc, params SharpDX.DXGI.Format[] targetFormats)
        {
            System.Diagnostics.Debug.Assert(
                targetFormats != null
                && targetFormats.Length > 0
                && targetFormats.Length < 9,
                "Between 1 and 8 render target formats must be provided");

            this.width = width;
            this.height = height;
            sampleDescription = sampleDesc;
            RTFormats = targetFormats;
        }

        public void Initialize()
        {
            var device = Window.Instance.Device;

            bool isMSAA = sampleDescription.Count > 1;

            var texDesc = new Texture2DDescription()
            {
                BindFlags = BindFlags.ShaderResource | BindFlags.RenderTarget,
                ArraySize = 1,
                CpuAccessFlags = CpuAccessFlags.None,
                Usage = ResourceUsage.Default,
                Width = width,
                Height = height,
                MipLevels = 1,
                SampleDescription = sampleDescription,
            };

            var rtvDesc = new RenderTargetViewDescription()
            {
                Dimension = isMSAA ? RenderTargetViewDimension.Texture2DMultisampled
                : RenderTargetViewDimension.Texture2D,
            };

            var srvDesc = new ShaderResourceViewDescription()
            {
                Format = Format.R8G8B8A8_UNorm,
                Dimension = isMSAA ? ShaderResourceViewDimension.Texture2DMultisampled
                : ShaderResourceViewDimension.Texture2D,
            };

            foreach (var format in RTFormats)
            {
                texDesc.Format = format;
                srvDesc.Format = format;
                rtvDesc.Format = format;

                RTs.Add(new Texture2D(device, texDesc));
                SRVs.Add(new ShaderResourceView(device, RTs.Last(), srvDesc));
                RTVs.Add(new RenderTargetView(device, RTs.Last(), rtvDesc));
            }

            texDesc.BindFlags = BindFlags.ShaderResource | BindFlags.DepthStencil;
            texDesc.Format = Format.R32G8X24_Typeless;
            DS0 = new Texture2D(device, texDesc);
            srvDesc.Format = SharpDX.DXGI.Format.R32_Float_X8X24_Typeless;
            DSSRV = new ShaderResourceView(device, DS0, srvDesc);

            var dsvDesc = new DepthStencilViewDescription()
            {
                Flags = DepthStencilViewFlags.None,
                Dimension = isMSAA ? DepthStencilViewDimension.Texture2DMultisampled
                : DepthStencilViewDimension.Texture2D,
                Format = Format.D32_Float_S8X24_UInt,
            };
            DSV = new DepthStencilView(device, DS0, dsvDesc);
        }

        public void Bind(DeviceContext1 context)
        {
            context.OutputMerger.SetTargets(DSV, 0, new UnorderedAccessView[0], RTVs.ToArray());
        }
        public void Unbind(DeviceContext1 context)
        {
            context.OutputMerger.ResetTargets();
        }

        public void Clear(DeviceContext1 context, RawColor4 backGroundColor)
        {
            foreach (var rtv in RTVs)
            {
                context.ClearRenderTargetView(rtv, backGroundColor);
            }

            context.ClearDepthStencilView(
                DSV,
                DepthStencilClearFlags.Depth | DepthStencilClearFlags.Stencil, 1f, 0);
        }

        public void Dispose()
        {
            RTs.ForEach(rt => rt.Dispose());
            SRVs.ForEach(srv => srv.Dispose());
            RTVs.ForEach(rtv => rtv.Dispose());
            RTs.Clear();
            SRVs.Clear();
            RTVs.Clear();

            DS0.Dispose();
            DS0 = null;
            DSSRV.Dispose();
            DSSRV = null;
            DSV.Dispose();
            DSV = null;

        }
    }
}
