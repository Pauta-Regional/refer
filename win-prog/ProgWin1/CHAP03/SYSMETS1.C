/* SYSMETS1.C -- System Metrics Display Program No. 1 */

#include <windows.h>
#include <stdio.h>
#include "sysmets.h"

long FAR PASCAL WndProc (HWND, unsigned, WORD, LONG) ;

int PASCAL WinMain (hInstance, hPrevInstance, lpszCmdLine, nCmdShow)
     HANDLE   hInstance, hPrevInstance ;
     LPSTR    lpszCmdLine ;
     int      nCmdShow ;
     {
     static   char szAppName [] = "SysMets1" ;
     HWND     hWnd ;
     MSG      msg ;
     WNDCLASS wndclass ;

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

     hWnd = CreateWindow (szAppName, "Get System Metrics",
                         WS_OVERLAPPEDWINDOW,
                         CW_USEDEFAULT, 0,
                         CW_USEDEFAULT, 0,
                         NULL, NULL, hInstance, NULL) ;

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
     HWND         hWnd ;
     unsigned     iMessage ;
     WORD         wParam ;
     LONG         lParam ;
     {
     static short xChar, yChar ;
     char         szBuffer [80] ;
     HDC          hDC ;
     int          i ;
     PAINTSTRUCT  ps ;
     TEXTMETRIC   tm ;

     switch (iMessage)
          {
          case WM_CREATE:
               hDC = GetDC (hWnd) ;
               GetTextMetrics (hDC, &tm) ;
               xChar = tm.tmAveCharWidth ;
               yChar = tm.tmHeight + tm.tmExternalLeading ;
               ReleaseDC (hWnd, hDC) ;
               break ;

          case WM_PAINT:
               BeginPaint (hWnd, &ps) ;

               for (i = 0 ; i < NUMLINES ; i++)
                    TextOut (ps.hdc, xChar, yChar * (1 + i), szBuffer, 
                         sprintf (szBuffer, "%-20s%-35s%5d",
                              sysmetrics[i].szLabel,
                              sysmetrics[i].szDesc,
                              GetSystemMetrics (sysmetrics[i].nIndex))) ;

               EndPaint (hWnd, &ps) ;
               break ;

          case WM_DESTROY:
               PostQuitMessage (0) ;
               break ;

          default:
               return DefWindowProc (hWnd, iMessage, wParam, lParam) ;
          }
     return 0L ;
     }
