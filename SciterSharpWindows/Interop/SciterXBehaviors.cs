// Copyright 2016 Ramon F. Mendes
//
// This file is part of SciterSharp.
// 
// SciterSharp is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// SciterSharp is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with SciterSharp.  If not, see <http://www.gnu.org/licenses/>.

#pragma warning disable 0169

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace SciterSharp.Interop
{
	public static class SciterXBehaviors
	{
		public enum EVENT_GROUPS : uint
		{
			HANDLE_INITIALIZATION = 0x0000, /** attached/detached */
			HANDLE_MOUSE = 0x0001,          /** mouse events */
			HANDLE_KEY = 0x0002,            /** key events */
			HANDLE_FOCUS = 0x0004,          /** focus events, if this flag is set it also means that element it attached to is focusable */
			HANDLE_SCROLL = 0x0008,         /** scroll events */
			HANDLE_TIMER = 0x0010,          /** timer event */
			HANDLE_SIZE = 0x0020,           /** size changed event */
			HANDLE_DRAW = 0x0040,           /** drawing request (event) */
			HANDLE_DATA_ARRIVED = 0x080,    /** requested data () has been delivered */
			HANDLE_BEHAVIOR_EVENT = 0x0100, /** logical, synthetic events:
													BUTTON_CLICK, HYPERLINK_CLICK, etc.,
													a.k.a. notifications from intrinsic behaviors */
			HANDLE_METHOD_CALL = 0x0200, /** behavior specific methods */
			HANDLE_SCRIPTING_METHOD_CALL = 0x0400, /** behavior specific methods */
			//HANDLE_TISCRIPT_METHOD_CALL  = 0x0800, /** behavior specific methods using direct tiscript::value's */
			HANDLE_STYLE_CHANGE          = 0x0800, /**< element's style has changed */

			HANDLE_EXCHANGE = 0x1000, /** system drag-n-drop */
			HANDLE_GESTURE = 0x2000, /** touch input events */
 			HANDLE_ATTRIBUTE_CHANGE      = 0x4000, /**< attribute change notification */

			HANDLE_SOM = 0x8000,

			HANDLE_ALL = 0xFFFF, /* all of them */

			SUBSCRIPTIONS_REQUEST = 0xFFFFFFFF, /* special value for getting subscription flags */
		}


		// alias BOOL function(LPVOID tag, HELEMENT he, UINT evtg, LPVOID prms) ElementEventProc;
		public delegate bool FPTR_ElementEventProc(IntPtr tag, IntPtr he, uint evtg, IntPtr prms);
		// alias BOOL function(LPCSTR, HELEMENT, LPElementEventProc*, LPVOID*) SciterBehaviorFactory;
		public delegate bool FPTR_SciterBehaviorFactory([MarshalAs(UnmanagedType.LPStr)] string s, IntPtr he, out FPTR_ElementEventProc proc, out IntPtr tag);

		[Flags]
		public enum PHASE_MASK : uint
		{
			BUBBLING = 0,       // bubbling (emersion) phase
			SINKING = 0x8000,   // capture (immersion) phase, this flag is or'ed with EVENTS codes below
			HANDLED = 0x10000
		}

		[Flags]
		public enum MOUSE_BUTTONS : uint
		{
			MAIN_MOUSE_BUTTON = 0x1, // aka left button
			PROP_MOUSE_BUTTON = 0x2,  // aka right button
			MIDDLE_MOUSE_BUTTON = 0x4,
		}

		[Flags]
		public enum KEYBOARD_STATES : uint
		{
			KEYBOARD_STATE_LSHIFT = 0x0001,
			KEYBOARD_STATE_RSHIFT = 0x0002,
			KEYBOARD_STATE_LCONTROL = 0x0040,
			KEYBOARD_STATE_RCONTROL = 0x0080,
			KEYBOARD_STATE_LALT = 0x0100,
			KEYBOARD_STATE_RALT = 0x0200,
			KEYBOARD_STATE_LCOMMAND = 0x0400,
			KEYBOARD_STATE_RCOMMAND = 0x0800,
			KEYBOARD_STATE_NUM = 0x1000,
			KEYBOARD_STATE_CAPS = 0x2000,
			KEYBOARD_STATE_MODE = 0x4000,

			KEYBOARD_STATE_CONTROL = (KEYBOARD_STATE_LCONTROL | KEYBOARD_STATE_RCONTROL),
			KEYBOARD_STATE_SHIFT = (KEYBOARD_STATE_LSHIFT | KEYBOARD_STATE_RSHIFT),
			KEYBOARD_STATE_ALT = (KEYBOARD_STATE_LALT | KEYBOARD_STATE_RALT),
			KEYBOARD_STATE_COMMAND = (KEYBOARD_STATE_LCOMMAND | KEYBOARD_STATE_RCOMMAND), // "command key" on OSX, "win" on Windows
		}

		public enum INITIALIZATION_EVENTS : uint
		{
			BEHAVIOR_DETACH = 0,
			BEHAVIOR_ATTACH = 1
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct INITIALIZATION_PARAMS
		{
			public INITIALIZATION_EVENTS cmd;     // INITIALIZATION_EVENTS
		}

		enum SOM_EVENTS
		{
			SOM_GET_PASSPORT = 0,
			SOM_GET_ASSET = 1
		}

		struct SOM_PARAMS
		{
			uint cmd; // SOM_EVENTS
			IntPtr passport_or_asset;// som_passport_t* or som_asset_t*
		}

		public enum DRAGGING_TYPE : uint
		{
			NO_DRAGGING,
			DRAGGING_MOVE,
			DRAGGING_COPY
		}

		public enum MOUSE_EVENTS : uint
		{
			MOUSE_ENTER = 0,
			MOUSE_LEAVE,
			MOUSE_MOVE,
			MOUSE_UP,
			MOUSE_DOWN,
			MOUSE_DCLICK, /// double click
			MOUSE_WHEEL,
			MOUSE_TICK,   /// generated periodically while mouse is pressed
			MOUSE_IDLE,   /// generated when mouse stays idle for some time

			/*OBSOLETE*/ DROP        = 9,   // item dropped, target is that dropped item
			/*OBSOLETE*/ DRAG_ENTER  = 0xA, // drag arrived to the target element that is one of current drop targets.
			/*OBSOLETE*/ DRAG_LEAVE  = 0xB, // drag left one of current drop targets. target is the drop target element.
			/*OBSOLETE*/ DRAG_REQUEST = 0xC,  // drag src notification before drag start. To cancel - return true from handler.

			MOUSE_TCLICK = 0xF, // tripple click

			//MOUSE_TOUCH_START = 0xFC, // touch device pressed somehow
			//MOUSE_TOUCH_END = 0xFD,   // touch device depressed - clear, nothing on it

			MOUSE_DRAG_REQUEST = 0xFE, // mouse drag start detected event

			MOUSE_CLICK = 0xFF, // mouse click event

			/*OBSOLETE*/ DRAGGING = 0x100, // ORed with MOUSE_ENTER...MOUSE_DOWN codes above

			MOUSE_HIT_TEST = 0xFFE,    // sent to element, allows to handle elements with non-trivial shapes.
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct MOUSE_PARAMS
		{
			public MOUSE_EVENTS cmd;// MOUSE_EVENTS
			public IntPtr target;// HELEMENT
			public PInvokeUtils.POINT pos;// POINT
			public PInvokeUtils.POINT pos_view;// POINT
			public uint button_state;// UINT ->> actually SciterXBehaviorsMOUSE_BUTTONS, but for MOUSE_EVENTS.MOUSE_WHEEL event it is the delta
			public KEYBOARD_STATES alt_state;// UINT
			public uint cursor_type;// UINT
			public bool is_on_icon;// BOOL - mouse is over icon (foreground-image, foreground-repeat:no-repeat)
			public IntPtr dragging;// HELEMENT - element is being dragged over, this field is not NULL if (cmd & DRAGGING) != 0
			public uint dragging_mode;// UINT - see DRAGGING_TYPE.
		}

		public enum CURSOR_TYPE : uint
		{
			CURSOR_ARROW, //0
			CURSOR_IBEAM, //1
			CURSOR_WAIT,  //2
			CURSOR_CROSS, //3
			CURSOR_UPARROW,  //4
			CURSOR_SIZENWSE, //5
			CURSOR_SIZENESW, //6
			CURSOR_SIZEWE,   //7
			CURSOR_SIZENS,   //8
			CURSOR_SIZEALL,  //9
			CURSOR_NO,       //10
			CURSOR_APPSTARTING, //11
			CURSOR_HELP,        //12
			CURSOR_HAND,        //13
			CURSOR_DRAG_MOVE,   //14
			CURSOR_DRAG_COPY,   //15
		}

// parameters of evtg = HANDLE_KEY
		public enum KEY_EVENTS : uint
		{
			KEY_DOWN = 0,
			KEY_UP,
			KEY_CHAR
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct KEY_PARAMS
		{
			public KEY_EVENTS cmd;   // KEY_EVENTS
			public IntPtr target;  // HELEMENT - target element
			public uint key_code;  // key scan code, or character unicode for KEY_CHAR
			public KEYBOARD_STATES alt_state;  // KEYBOARD_STATES.
		}

 /** #HANDLE_FOCUS commands */
		public enum FOCUS_EVENTS : uint
		{
			FOCUS_OUT = 0,      // container got focus on element inside it, target is an element that got focus
			FOCUS_IN = 1,       // container lost focus from any element inside it, target is an element that lost focus
			FOCUS_GOT = 2,      // target element got focus
			FOCUS_LOST = 3,     // target element lost focus
			FOCUS_REQUEST = 4,  // bubbling event/request, gets sent on child-parent chain to accept/reject focus to be set on the child (target)
			FOCUS_ADVANCE_REQUEST = 5,// bubbling event/request, gets sent on child-parent chain to advance focus
		}

		enum FOCUS_CMD_TYPE
		{
			FOCUS_RQ_NEXT,
			FOCUS_RQ_PREV,
			FOCUS_RQ_HOME,
			FOCUS_RQ_END,
			FOCUS_RQ_LEFT,
			FOCUS_RQ_RIGHT,
			FOCUS_RQ_UP,
			FOCUS_RQ_DOWN,  // all these - by key
			FOCUS_RQ_FIRST, // these two - by_code
			FOCUS_RQ_LAST,  //
			FOCUS_RQ_END_REACHED = 0x8000
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct FOCUS_PARAMS
		{
			public FOCUS_EVENTS cmd;
			public IntPtr target;// HELEMENT
			// public bool by_mouse_click;
			public uint cause;// FOCUS_CMD_TYPE 
			public bool cancel;
		}

		// parameters of evtg == HANDLE_SCROLL
		public enum SCROLL_EVENTS : uint
		{
			SCROLL_HOME = 0,
			SCROLL_END,
			SCROLL_STEP_PLUS,
			SCROLL_STEP_MINUS,
			SCROLL_PAGE_PLUS,
			SCROLL_PAGE_MINUS,
			SCROLL_POS,
			SCROLL_SLIDER_RELEASED,
			SCROLL_CORNER_PRESSED,
			SCROLL_CORNER_RELEASED,
			SCROLL_SLIDER_PRESSED,

			SCROLL_ANIMATION_START,
			SCROLL_ANIMATION_END,
		}

		public enum SCROLL_SOURCE
		{
			SCROLL_SOURCE_UNKNOWN,
			SCROLL_SOURCE_KEYBOARD,  // SCROLL_PARAMS::reason <- keyCode
			SCROLL_SOURCE_SCROLLBAR, // SCROLL_PARAMS::reason <- SCROLLBAR_PART
			SCROLL_SOURCE_ANIMATOR,
			SCROLL_SOURCE_WHEEL,
		}

		public enum SCROLLBAR_PART
		{
			SCROLLBAR_BASE,
			SCROLLBAR_PLUS,
			SCROLLBAR_MINUS,
			SCROLLBAR_SLIDER,
			SCROLLBAR_PAGE_MINUS,
			SCROLLBAR_PAGE_PLUS,
			SCROLLBAR_CORNER,
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct SCROLL_PARAMS
		{
			public SCROLL_EVENTS cmd;
			public IntPtr target; // HELEMENT - target element
			public int pos;   // scroll position if SCROLL_POS
			public bool vertical;  // true if from vertical scrollbar
			public SCROLL_SOURCE source;    // SCROLL_SOURCE
			public uint reason; // key or SCROLLBAR_PART
		}

		// public enum GESTURE_CMD : uint
		// {
		// 	GESTURE_REQUEST = 0,
		// 	GESTURE_ZOOM,
		// 	GESTURE_PAN,
		// 	GESTURE_ROTATE,
		// 	GESTURE_TAP1,
		// 	GESTURE_TAP2,
		// }

		// public enum GESTURE_STATE : uint
		// {
		// 	GESTURE_STATE_BEGIN = 1,
		// 	GESTURE_STATE_INERTIA = 2,
		// 	GESTURE_STATE_END = 4,
		// }

		// public enum GESTURE_TYPE_FLAGS : uint
		// {
		// 	GESTURE_FLAG_ZOOM = 0x0001,
		// 	GESTURE_FLAG_ROTATE = 0x0002,
		// 	GESTURE_FLAG_PAN_VERTICAL = 0x0004,
		// 	GESTURE_FLAG_PAN_HORIZONTAL = 0x0008,
		// 	GESTURE_FLAG_TAP1 = 0x0010,
		// 	GESTURE_FLAG_TAP2 = 0x0020,
		// 	GESTURE_FLAG_PAN_WITH_GUTTER = 0x4000,
		// 	GESTURE_FLAG_PAN_WITH_INERTIA = 0x8000,
		// 	GESTURE_FLAGS_ALL = 0xFFFF,
		// }

		[StructLayout(LayoutKind.Sequential)]
		public struct GESTURE_PARAMS
		{
			public uint cmd;    // GESTURE_EVENTS
			public IntPtr target;   // target element
			public PInvokeUtils.POINT pos;  // position of cursor, element relative
			public PInvokeUtils.POINT pos_view;  // position of cursor, view relative
			/// <summary>
			/// GESTURE_TYPE_FLAGS or GESTURE_STATE combination
			/// </summary>
			// public uint flags;
			// public uint delta_time;
			// public PInvokeUtils.SIZE delta_xy;
			// public double delta_v;
		}

		public enum EXCHANGE_CMD
		{
			X_DRAG_ENTER = 0,       // drag enters the element
			X_DRAG_LEAVE = 1,       // drag leaves the element  
			X_DRAG = 2,             // drag over the element
			X_DROP = 3,             // data dropped on the element  
			X_PASTE = 4,            // N/A
			X_DRAG_REQUEST = 5,     // N/A
			X_DRAG_CANCEL = 6,      // drag cancelled (e.g. by pressing VK_ESCAPE)
			X_WILL_ACCEPT_DROP = 7, // drop target element shall consume this event in order to receive X_DROP 
		}

		public enum DD_MODES
		{
			DD_MODE_NONE = 0, // DROPEFFECT_NONE	( 0 )
			DD_MODE_COPY = 1, // DROPEFFECT_COPY	( 1 )
			DD_MODE_MOVE = 2, // DROPEFFECT_MOVE	( 2 )
			DD_MODE_COPY_OR_MOVE = 3, // DROPEFFECT_COPY	( 1 ) | DROPEFFECT_MOVE	( 2 )
			DD_MODE_LINK = 4, // DROPEFFECT_LINK	( 4 )
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct EXCHANGE_PARAMS
		{
			public uint cmd;                        // EXCHANGE_EVENTS
			public int target;                      // target element
			public int source;                      // source element (can be null if D&D from external window)
			public PInvokeUtils.POINT pos;          // position of cursor, element relative
			public PInvokeUtils.POINT pos_view;     // position of cursor, view relative
			public uint mode;                       // DD_MODE 
			public SciterXValue.VALUE data;         // packaged drag data
		}

		public enum DRAW_EVENTS : uint
		{
			DRAW_BACKGROUND = 0,
			DRAW_CONTENT = 1,
			DRAW_FOREGROUND = 2,
			DRAW_OUTLINE = 3,
		}

		//struct SCITER_GRAPHICS;

		[StructLayout(LayoutKind.Sequential)]
		public struct DRAW_PARAMS
		{
			public DRAW_EVENTS cmd;
			public IntPtr gfx;  // HGFX - hdc to paint on
			public PInvokeUtils.RECT area;  // element area, to get invalid area to paint use GetClipBox,
			public uint reserved;   // for DRAW_BACKGROUND/DRAW_FOREGROUND - it is a border box
									// for DRAW_CONTENT - it is a content box
		}

		public enum CONTENT_CHANGE_BITS : uint // for CONTENT_CHANGED reason
		{
			CONTENT_ADDED = 0x01,
			CONTENT_REMOVED = 0x02,
		}

		public enum BEHAVIOR_EVENTS : uint
		{
			BUTTON_CLICK = 0,              // click on button
			BUTTON_PRESS = 1,              // mouse down or key down in button
			
			VALUE_CHANGED = 2,             // value 
			VALUE_CHANGING = 3,            // before text change
			
			SELECTION_CHANGED = 5,         // selection changed in <select>, <textarea>, etc
			SELECTION_CHANGING = 0xC,      // selection is changeing in <select>, <textarea>, etc

			POPUP_REQUEST   = 7,           // request to show popup just received,
											//     here DOM of popup element can be modifed.
			POPUP_READY     = 8,           // popup element has been measured and ready to be shown on screen,
											//     here you can use functions like ScrollToView.
			POPUP_DISMISSED = 9,           // popup element is closed,
											//     here DOM of popup element can be modifed again - e.g. some items can be removed
											//     to free memory.

			MENU_ITEM_ACTIVE = 0xA,        // menu item activated by mouse hover or by keyboard,
			MENU_ITEM_CLICK = 0xB,         // menu item click,
											//   BEHAVIOR_EVENT_PARAMS structure layout
											//   BEHAVIOR_EVENT_PARAMS.cmd - MENU_ITEM_CLICK/MENU_ITEM_ACTIVE
											//   BEHAVIOR_EVENT_PARAMS.heTarget - owner(anchor) of the menu
											//   BEHAVIOR_EVENT_PARAMS.he - the menu item, presumably <li> element
											//   BEHAVIOR_EVENT_PARAMS.reason - BY_MOUSE_CLICK | BY_KEY_CLICK


			CONTEXT_MENU_REQUEST = 0x10,   // "right-click", BEHAVIOR_EVENT_PARAMS::he is current popup menu HELEMENT being processed or NULL.
											// application can provide its own HELEMENT here (if it is NULL) or modify current menu element.

			VISUAL_STATUS_CHANGED = 0x11,  // sent to the element being shown or hidden
			DISABLED_STATUS_CHANGED = 0x12,// broadcast notification, sent to all elements of some container that got new value of :disabled state

			POPUP_DISMISSING = 0x13,       // popup is about to be closed

			CONTENT_CHANGED = 0x15,        // content has been changed, is posted to the element that gets content changed,  reason is combination of CONTENT_CHANGE_BITS.
											// target == NULL means the window got new document and this event is dispatched only to the window.

			// "grey" event codes  - notfications from behaviors from this SDK
			HYPERLINK_CLICK = 0x80,        // hyperlink click

			ELEMENT_COLLAPSED = 0x90,      // element was collapsed, so far only behavior:tabs is sending these two to the panels
			ELEMENT_EXPANDED = 0x91,       // element was expanded,

			ACTIVATE_CHILD = 0x92,         // activate (select) child,
											// used for example by accesskeys behaviors to send activation request, e.g. tab on behavior:tabs.

			FORM_SUBMIT = 0x96,            // behavior:form detected submission event. BEHAVIOR_EVENT_PARAMS::data field contains data to be posted.
											// BEHAVIOR_EVENT_PARAMS::data is of type T_MAP in this case key/value pairs of data that is about
											// to be submitted. You can modify the data or discard submission by returning true from the handler.
			FORM_RESET  = 0x97,            // behavior:form detected reset event (from button type=reset). BEHAVIOR_EVENT_PARAMS::data field contains data to be reset.
											// BEHAVIOR_EVENT_PARAMS::data is of type T_MAP in this case key/value pairs of data that is about
											// to be rest. You can modify the data or discard reset by returning true from the handler.

			DOCUMENT_COMPLETE = 0x98,             // document in behavior:frame or root document is complete.

			HISTORY_PUSH = 0x99,                  // requests to behavior:history (commands)
			HISTORY_DROP = 0x9A,
			HISTORY_PRIOR = 0x9B,
			HISTORY_NEXT = 0x9C,
			HISTORY_STATE_CHANGED = 0x9D,  // behavior:history notification - history stack has changed

			CLOSE_POPUP = 0x9E,            // close popup request,
			REQUEST_TOOLTIP = 0x9F,        // request tooltip, evt.source <- is the tooltip element.

			ANIMATION         = 0xA0,      // animation started (reason=1) or ended(reason=0) on the element.
			TRANSITION        = 0xA1,      // transition started (reason=1) or ended(reason=0) on the element.
			SWIPE             = 0xB0,      // swipe gesture detected, reason=4,8,2,6 - swipe direction, only from behavior:swipe-touch

			DOCUMENT_CREATED  = 0xC0,      // document created, script namespace initialized. target -> the document
			DOCUMENT_CLOSE_REQUEST = 0xC1, // document is about to be closed, to cancel closing do: evt.data = sciter::value("cancel");
			DOCUMENT_CLOSE    = 0xC2,      // last notification before document removal from the DOM
			DOCUMENT_READY    = 0xC3,      // document has got DOM structure, styles and behaviors of DOM elements. Script loading run is complete at this moment.
			DOCUMENT_PARSED   = 0xC4,      // document just finished parsing - has got DOM structure. This event is generated before DOCUMENT_READY
			//DOCUMENT_RELOAD        = 0xC5, // request to reload the document
			DOCUMENT_CLOSING  = 0xC6, // view::notify_close
			CONTAINER_CLOSE_REQUEST = 0xC7, // window of host document is processing DOCUMENT_CLOSE_REQUEST
			CONTAINER_CLOSING = 0xC8,       // window of host document is processing DOCUMENT_CLOSING

			VIDEO_INITIALIZED = 0xD1,      // <video> "ready" notification
			VIDEO_STARTED     = 0xD2,      // <video> playback started notification
			VIDEO_STOPPED     = 0xD3,      // <video> playback stoped/paused notification
			VIDEO_BIND_RQ     = 0xD4,      // <video> request for frame source binding,
											//   If you want to provide your own video frames source for the given target <video> element do the following:
											//   1. Handle and consume this VIDEO_BIND_RQ request
											//   2. You will receive second VIDEO_BIND_RQ request/event for the same <video> element
											//      but this time with the 'reason' field set to an instance of sciter::video_destination interface.
											//   3. add_ref() it and store it for example in worker thread producing video frames.
											//   4. call sciter::video_destination::start_streaming(...) providing needed parameters
											//      call sciter::video_destination::render_frame(...) as soon as they are available
											//      call sciter::video_destination::stop_streaming() to stop the rendering (a.k.a. end of movie reached)

			VIDEO_SOURCE_CREATED = 0xD5,

			VIDEO_FRAME_REQUEST = 0xD8,    // animation step, a.k.a. animation frame

			PAGINATION_STARTS  = 0xE0,     // behavior:pager starts pagination
			PAGINATION_PAGE    = 0xE1,     // behavior:pager paginated page no, reason -> page no
			PAGINATION_ENDS    = 0xE2,     // behavior:pager end pagination, reason -> total pages

			CUSTOM             = 0xF0,     // event with custom name

			EGL_RENDER         = 0x20,    //  

			FIRST_APPLICATION_EVENT_CODE = 0x100
			// all custom event codes shall be greater
			// than this number. All codes below this will be used
			// solely by application - Sciter will not intrepret it
			// and will do just dispatching.
			// To send event notifications with  these codes use
			// SciterSend/PostEvent API.
		}

		public enum EVENT_REASON : uint
		{
			BY_MOUSE_CLICK,
			BY_KEY_CLICK,
			SYNTHESIZED,   // synthesized, programmatically generated.
			BY_MOUSE_ON_ICON,
		}

		public enum EDIT_CHANGED_REASON : uint
		{
			BY_INS_CHAR,  // single char insertion
			BY_INS_CHARS, // character range insertion, clipboard
			BY_DEL_CHAR,  // single char deletion
			BY_DEL_CHARS, // character range deletion (selection)
			BY_UNDO_REDO, // undo/redo
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct BEHAVIOR_EVENT_PARAMS
		{
			public BEHAVIOR_EVENTS cmd;
			public IntPtr heTarget;// HELEMENT
			public IntPtr he;// HELEMENT
			public IntPtr reason;// UINT_PTR
			public SciterXValue.VALUE data;// SCITER_VALUE
			public IntPtr name;  
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct TIMER_PARAMS
		{
			public IntPtr timerId;// UINT_PTR - timerId that was used to create timer by using SciterSetTimer
		}

		public enum BEHAVIOR_METHOD_IDENTIFIERS : uint
		{
			DO_CLICK = 0,

			SET_CURRENT_GL_CONTEXT = 0x10,
			RELEASE_CURRENT_GL_CONTEXT = 0x11,

			IS_EMPTY      = 0xFC,       // p - IS_EMPTY_PARAMS // set VALUE_PARAMS::is_empty (false/true) reflects :empty state of the element.
			GET_VALUE     = 0xFD,       // p - VALUE_PARAMS
			SET_VALUE     = 0xFE,       // p - VALUE_PARAMS
			FIRST_APPLICATION_METHOD_ID = 0x100

		}

		[StructLayout(LayoutKind.Sequential)]
		public struct SCRIPTING_METHOD_PARAMS
		{
			public IntPtr name;// LPCSTR - method name
			public IntPtr argv;// VALUE* - vector of arguments
			public uint argc;  // arguement count
			public SciterXValue.VALUE result;   // plz note, Sciter will internally call ValueClear to this VALUE,
												// that is, it own this data, so always assign a copy with a positive ref-count of your VALUE to this variable
												// you will know that if you get an "Access Violation" error
		}

		// SCRIPTING_METHOD_PARAMS wraper
		public struct SCRIPTING_METHOD_PARAMS_Wraper
		{
			public SCRIPTING_METHOD_PARAMS_Wraper(SCRIPTING_METHOD_PARAMS prms)
			{
				name = Marshal.PtrToStringAnsi(prms.name);
				args = new SciterValue[prms.argc];
				result = SciterValue.Undefined;

				for(int i = 0; i < prms.argc; i++)
					args[i] = new SciterValue((SciterXValue.VALUE)Marshal.PtrToStructure(IntPtr.Add(prms.argv, i * Marshal.SizeOf(typeof(SciterXValue.VALUE))), typeof(SciterXValue.VALUE)));
			}

			public string name;
			public SciterValue[] args;
			public SciterValue result;
		}

		// GET_VALUE/SET_VALUE methods params
		[StructLayout(LayoutKind.Sequential)]
		public struct VALUE_PARAMS
		{
			public uint methodID;
			public SciterXValue.VALUE val;// SCITER_VALUE
		}

		// IS_EMPTY method params
		[StructLayout(LayoutKind.Sequential)]
		public struct IS_EMPTY_PARAMS
		{
			public uint methodID;
			public uint is_empty; // !0 - is empty
		}

		[StructLayout(LayoutKind.Sequential)]

		// see SciterRequestElementData
		public struct DATA_ARRIVED_PARAMS
		{
			public IntPtr initiator;// HELEMENT - element initiator of SciterRequestElementData request,
			public byte[] data;// LPCBYTE - data buffer
			public uint dataSize;  // size of data 
			public uint dataType;  // data type passed "as is" from SciterRequestElementData 
			public uint status;   // status = 0 (dataSize == 0) - unknown error.
								  // status = 100..505 - http response status, Note: 200 - OK!
								  // status > 12000 - wininet error code, see ERROR_INTERNET_*** in wininet.h
			[MarshalAs(UnmanagedType.LPWStr)]
			public string uri;// LPCWSTR - requested url
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct ATTRIBUTE_CHANGE_PARAMS
		{
			public IntPtr he;   // this element
			public IntPtr name;  // LPCSTR - attribute name
			[MarshalAs(UnmanagedType.LPWStr)]
			public string value;  // new attribute value, NULL if attribute was deleted
		}
	}
}

#pragma warning restore 0169