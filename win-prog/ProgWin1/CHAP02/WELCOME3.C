/*  WELCOME3.C -- Welcome to Windows Program No. 3 */

#include <windows.h>

long FAR PASCAL WndProc (HWND, unsigned, WORD, LONG) ;

int PASCAL WinMain (hInstance, hPrevInstance, lpszCmdLine, nCmdShow)
     HANDLE      hInstance, hPrevInstance ;
     LPSTR       lpszCmdLine ;
     int         nCmdShow ;
     {
     static char szAppName [] = "Welcome3" ;
     HWND        hWnd ;
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
          }

     xScreen = GetSystemMetrics (SM_CXSCREEN) ;
     yScreen = GetSystemMetrics (SM_CYSCREEN) ;

     hWnd = CreateWindow (szAppName,         /* window class name       */
                    "Welcome to Windows",    /* window caption          */

                    WS_OVERLAPPED |          /* window style            */
                         WS_CAPTION | WS_SYSMENU | WS_MINIMIZEBOX,     

                    xScreen / 4,             /* initial x position      */
                    yScreen / 4,             /* initial y position      */
                    xScreen / 2,             /* initial x size          */
                    yScreen / 2,             /* initial y size          */
                    NULL,                    /* parent window handle    */
                    NULL,                    /* window menu handle      */
                    hInstance,               /* program instance handle */
                    NULL) ;                  /* create parameters       */

     ShowWindow (hWnd, nCmdShow) ;
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
