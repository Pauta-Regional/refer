/*  WELCOME4.C -- Welcome to Windows Program No. 4 */

#include <windows.h>
#include <stdio.h>

long FAR PASCAL WndProc (HWND, unsigned, WORD, LONG) ;

int PASCAL WinMain (hInstance, hPrevInstance, lpszCmdLine, nCmdShow)
     HANDLE      hInstance, hPrevInstance ;
     LPSTR       lpszCmdLine ;
     int         nCmdShow ;
     {
     static char szAppName [] = "Welcome4" ;
     char        szCaption [60] ;
     HWND        hWnd ;
     MSG         msg ;
     short       i ;
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

     for (i = 0 ; i < 10 ; i++)
          {
          hWnd = CreateWindow (szAppName,         /* window class name       */
                         NULL,                    /* window caption          */
                         WS_OVERLAPPEDWINDOW,     /* window style            */
                         CW_USEDEFAULT,           /* initial x position      */
                         0,                       /* initial y position      */
                         CW_USEDEFAULT,           /* initial x size          */
                         0,                       /* initial y size          */
                         NULL,                    /* parent window handle    */
                         NULL,                    /* window menu handle      */
                         hInstance,               /* program instance handle */
                         NULL) ;                  /* create parameters       */

          sprintf (szCaption, "Instance Handle = %X, Window Handle = %X",
                         hInstance, hWnd) ;

          SetWindowText (hWnd, szCaption) ;
          ShowWindow (hWnd, SW_SHOWNORMAL) ;
          UpdateWindow (hWnd) ;
          }

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
