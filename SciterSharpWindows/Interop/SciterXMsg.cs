using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable CS0169

namespace SciterSharp.Interop
{
	class SciterXMsg
	{
		enum SCITER_X_MSG_CODE
		{
			SXM_CREATE = 0,
			SXM_DESTROY = 1,
			SXM_SIZE = 2,
			SXM_PAINT = 3,
			SXM_RESOLUTION = 4,
			SXM_HEARTBIT = 5,
			SXM_MOUSE = 6,
			SXM_KEY = 7,
			SXM_FOCUS = 8
		}

		struct SCITER_X_MSG
		{
			uint msg;  // [in] one of the codes of #SCITER_X_MSG_CODE.
		}

		enum SL_TARGET : uint
		{
			SL_TARGET_BITMAP,
			SL_TARGET_OPENGL,
			SL_TARGET_OPENGLES,
			SL_TARGET_DX9_TEXTURE,
			SL_TARGET_DX11_TEXTURE
		}


		struct SCITER_X_MSG_CREATE
		{
			SCITER_X_MSG header;
			SL_TARGET backend;
			// bool transparent;
			IntPtr device;
		}

		struct SCITER_X_MSG_DESTROY
		{
			SCITER_X_MSG header;
		}

		struct SL_SURFACE
		{
			IntPtr texture;
			uint stride;
		}
		struct SCITER_X_MSG_SIZE
		{
			SCITER_X_MSG header;
			uint width;
			uint height;
			SL_SURFACE surface;
		}

		enum SCITER_PAINT_TARGET_TYPE
		{
			SPT_DEFAULT = 0,
			SPT_RECEIVER = 1,
			SPT_SURFACE = 2,
		}

		struct SCITER_X_MSG_PAINT
		{
			SCITER_X_MSG header;
			IntPtr element;// HELEMENT
			bool isFore;
			PInvokeUtils.RECT rcPaint;
		}
	}
}

#pragma warning restore CS1069