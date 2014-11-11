/*  PARTY.C -- Overlapped, Popup, and Child Windows */

#include <windows.h>

long FAR PASCAL WndProc (HWND, unsigned, WORD, LONG) ;
long FAR PASCAL PopupWndProc (HWND, unsigned, WORD, LONG) ;
long FAR PASCAL ChildWndProc (HWND, unsigned, WORD, LONG) ;

int PASCAL WinMain (hInstance, hPrevInstance, lpszCmdLine, nCmdShow)
     HANDLE      hInstance, hPrevInstance ;
     LPSTR       lpszCmdLine ;
     int         nCmdShow ;
     {
     static char szAppName [] = "Party" ;
     static char szPopupClass [] = "Party_Popup" ;
     static char szChildClass [] = "Party_Child" ;
     HWND        hWnd, hWndMain, hWndPopup1, hWndPopup2 ;
     MSG         msg ;
     short       xScreen, yScreen ;
     WNDCLASS    wndclass ;

     if (!hPrevInstance) 
          {
          wndclass.style         = CS_HREDRAW | CS_VREDRAW ;
          wndclass.lpfnWndProc   = WndProc ;
          wndclass.cbClsExtra    = 0 ;
          wndclass.cbWndExtra    = 0 ;
          wndclass.hInstance     = hInstance ;
          wndclass.hIcon         = LoadIcon (NULL, IDI_APPLICATION) ;
          wndclass.hCursor       = LoadCursor (NULL, IDC_ARROW) ;
          wndclass.hbrBackground = GetStockObject (WHITE_BRUSH) ;
          wndclass.lpszMenuName  = NULL ;
          wndclass.lpszClassName = szAppName ;

          if (!RegisterClass (&wndclass))
               return FALSE ;

          wndclass.style         = CS_HREDRAW | CS_VREDRAW ;
          wndclass.lpfnWndProc   = PopupWndProc ;
          wndclass.cbClsExtra    = 0 ;
          wndclass.cbWndExtra    = 0 ;
          wndclass.hInstance     = hInstance ;
          wndclass.hIcon         = NULL ;
          wndclass.hCursor       = LoadCursor (NULL, IDC_CROSS) ;
          wndclass.hbrBackground = COLOR_WINDOW + 1 ;
          wndclass.lpszMenuName  = NULL ;
          wndclass.lpszClassName = szPopupClass ;

          if (!RegisterClass (&wndclass))
               return FALSE ;

          wndclass.style         = CS_HREDRAW | CS_VREDRAW ;
          wndclass.lpfnWndProc   = ChildWndProc ;
          wndclass.cbClsExtra    = 0 ;
          wndclass.cbWndExtra    = 0 ;
          wndclass.hInstance     = hInstance ;
          wndclass.hIcon         = NULL ;
          wndclass.hCursor       = LoadCursor (NULL, IDC_UPARROW) ;
          wndclass.hbrBackground = GetStockObject (LTGRAY_BRUSH) ;
          wndclass.lpszMenuName  = NULL ;
          wndclass.lpszClassName = szChildClass ;

          if (!RegisterClass (&wndclass))
               return FALSE ;
          }

     xScreen = GetSystemMetrics (SM_CXSCREEN) ;
     yScreen = GetSystemMetrics (SM_CYSCREEN) ;

     hWnd = CreateWindow (szAppName,         /* window class name       */
                    "Party Program",         /* window caption          */
                    WS_OVERLAPPEDWINDOW,     /* window style            */
                    0,                       /* initial x position      */
                    0,                       /* initial y position      */
                    CW_USEDEFAULT,           /* initial x size          */
                    0,                       /* initial y size          */
                    NULL,                    /* parent window handle    */
                    NULL,                    /* window menu handle      */
                    hInstance,               /* program instance handle */
                    NULL) ;                  /* create parameters       */

     ShowWindow (hWnd, nCmdShow) ;
     UpdateWindow (hWnd) ;

     hWndMain = hWnd ;

     hWnd = CreateWindow (szPopupClass,      /* window class name       */
                    "Popup with Parent",     /* window caption          */

                    WS_POPUP | WS_CAPTION |  /* window style            */
                         WS_SYSMENU | WS_THICKFRAME | WS_MAXIMIZEBOX,     

                    xScreen / 9,             /* initial x position      */
                    yScreen / 8,             /* initial y position      */
                    xScreen / 3,             /* initial x size          */
                    yScreen / 4,             /* initial y size          */
                    hWndMain,                /* parent window handle    */
                    NULL,                    /* window menu handle      */
                    hInstance,               /* program instance handle */
                    NULL) ;                  /* create parameters       */

     ShowWindow (hWnd, SW_SHOWNORMAL) ;
     UpdateWindow (hWnd) ;

     hWndPopup1 = hWnd ;

     hWnd = CreateWindow (szPopupClass,      /* window class name       */
                    "Popup without Parent",  /* window caption          */

                    WS_POPUP | WS_CAPTION |  /* window style            */
                         WS_SYSMENU | WS_THICKFRAME | WS_MAXIMIZEBOX,     

                    5 * xScreen / 9,         /* initial x position      */
                    yScreen / 8,             /* initial y position      */
                    xScreen / 3,             /* initial x size          */
                    yScreen / 4,             /* initial y size          */
                    NULL,                    /* parent window handle    */
                    NULL,                    /* window menu handle      */
                    hInstance,               /* program instance handle */
                    NULL) ;                  /* create parameters       */

     ShowWindow (hWnd, SW_SHOWNORMAL) ;
     UpdateWindow (hWnd) ;

     hWndPopup2 = hWnd ;

     hWnd = CreateWindow (szChildClass,      /* window class name       */
                    "Child Window 1",        /* window caption          */

                    WS_CHILD | WS_CAPTION |  /* window style            */
                         WS_CLIPSIBLINGS | WS_SYSMENU |
                         WS_THICKFRAME | WS_MAXIMIZEBOX,     

                    xScreen / 9,             /* initial x position      */
                    yScreen / 2,             /* initial y position      */
                    xScreen / 3,             /* initial x size          */
                    yScreen / 4,             /* initial y size          */
                    hWndMain,                /* parent window handle    */
                    1,                       /* window menu handle      */
                    hInstance,               /* program instance handle */
                    NULL) ;                  /* create parameters       */

     ShowWindow (hWnd, SW_SHOWNORMAL) ;
     UpdateWindow (hWnd) ;

     hWnd = CreateWindow (szChildClass,      /* window class name       */
                    "Child Window 2",        /* window caption          */

                    WS_CHILD | WS_CAPTION |  /* window style            */
                         WS_CLIPSIBLINGS | WS_SYSMENU |
                         WS_THICKFRAME | WS_MAXIMIZEBOX,     

                    5 * xScreen / 9,         /* initial x position      */
                    yScreen / 2,             /* initial y position      */
                    xScreen / 3,             /* initial x size          */
                    yScreen / 4,             /* initial y size          */
                    hWndMain,                /* parent window handle    */
                    2,                       /* window menu handle      */
                    hInstance,               /* program instance handle */
                    NULL) ;                  /* create parameters       */

     ShowWindow (hWnd, SW_SHOWNORMAL) ;
     UpdateWindow (hWnd) ;

     hWnd = CreateWindow ("Edit",            /* window class name       */
                    "Edit Box Text",         /* window caption          */

                    WS_CHILD | WS_BORDER |   /* window style            */
                         WS_VSCROLL | WS_HSCROLL |
                         ES_MULTILINE | ES_AUTOVSCROLL | ES_AUTOHSCROLL,

                    10,                      /* initial x position      */
                    10,                      /* initial y position      */
                    xScreen / 4,             /* initial x size          */
                    yScreen / 8,             /* initial y size          */
                    hWndPopup1,              /* parent window handle    */
                    3,                       /* window menu handle      */
                    hInstance,               /* program instance handle */
                    NULL) ;                  /* create parameters       */

     ShowWindow (hWnd, SW_SHOWNORMAL) ;
     UpdateWindow (hWnd) ;

     hWnd = CreateWindow ("Button",          /* window class name       */
                    "Push Button",           /* window caption          */
                    WS_CHILD | BS_PUSHBUTTON,/* window style            */
                    10,                      /* initial x position      */
                    10,                      /* initial y position      */
                    xScreen / 4,             /* initial x size          */
                    yScreen / 8,             /* initial y size          */
                    hWndPopup2,              /* parent window handle    */
                    4,                       /* window menu handle      */
                    hInstance,               /* program instance handle */
                    NULL) ;                  /* create parameters       */

     ShowWindow (hWnd, SW_SHOWNORMAL) ;
     UpdateWindow (hWnd) ;

     while (GetMessage (&msg, NULL, 0, 0))
          {
          TranslateMessage (&msg) ;
          DispatchMessage (&msg) ;
          }
     return msg.wParam ;
     }

long FAR PASCAL WndProc (hWnd, iMessage, wParam, lParam)
     HWND     hWnd ;
     unsigned iMessage ;
     WORD     wParam ;
     LONG     lParam ;
     {
     switch (iMessage)
          {
          case WM_DESTROY:
               PostQuitMessage (0) ;
               break ;

          default:
               return DefWindowProc (hWnd, iMessage, wParam, lParam) ;
          }
     return 0L ;
     }

long FAR PASCAL PopupWndProc (hWnd, iMessage, wParam, lParam)
     HWND     hWnd ;
     unsigned iMessage ;
     WORD     wParam ;
     LONG     lParam ;
     {
     return DefWindowProc (hWnd, iMessage, wParam, lParam) ;
     }

long FAR PASCAL ChildWndProc (hWnd, iMessage, wParam, lParam)
     HWND     hWnd ;
     unsigned iMessage ;
     WORD     wParam ;
     LONG     lParam ;
     {
     return DefWindowProc (hWnd, iMessage, wParam, lParam) ;
     }

