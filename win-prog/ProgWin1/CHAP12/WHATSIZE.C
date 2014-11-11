/*  WHATSIZE.C -- What Size is the Window? */

#include <windows.h>
#include <stdio.h>

long FAR PASCAL WndProc (HWND, unsigned, WORD, LONG) ;

int PASCAL WinMain (hInstance, hPrevInstance, lpszCmdLine, nCmdShow)
     HANDLE      hInstance, hPrevInstance ;
     LPSTR       lpszCmdLine ;
     int         nCmdShow ;
     {
     static char szAppName [] = "WhatSize" ;
     HWND        hWnd ;
     MSG         msg ;
     WNDCLASS    wndclass ;

     if (!hPrevInstance) 
          {
          wndclass.style         = CS_HREDRAW | CS_VREDRAW;
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

     hWnd = CreateWindow (szAppName, "What Size is the Window?",
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

void Show (hWnd, hDC, xText, yText, nMapMode, szMapMode)
     HWND  hWnd ;
     HDC   hDC ;
     short xText, yText, nMapMode ;
     char  *szMapMode ;
     {
     RECT  rect ;
     char  szBuffer [60] ;

     SaveDC (hDC) ;

     SetMapMode (hDC, nMapMode) ;
     GetClientRect (hWnd, &rect) ;
     DPtoLP (hDC, (LPPOINT) &rect, 2) ;

     RestoreDC (hDC, -1) ;

     TextOut (hDC, xText, yText, szBuffer,
               sprintf (szBuffer, "%-20s %7d %7d %7d %7d", szMapMode,
                    rect.left, rect.right, rect.top, rect.bottom)) ;
     }  

long FAR PASCAL WndProc (hWnd, iMessage, wParam, lParam)
     HWND         hWnd ;
     unsigned     iMessage ;
     WORD         wParam ;
     LONG         lParam ;
     {
     static char  szHeading [] =
                    "Mapping Mode            Left   Right     Top  Bottom" ;
     static char  szUndLine [] = 
                    "------------            ----   -----     ---  ------" ;
     static short xChar, yChar ;
     HDC          hDC ;
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
               hDC = BeginPaint (hWnd, &ps) ;

               SetMapMode (hDC, MM_ANISOTROPIC) ;
               SetWindowExt (hDC, 1, 1) ;
               SetViewportExt (hDC, xChar, yChar) ;

               TextOut (hDC, 1, 1, szHeading, sizeof szHeading - 1) ;
               TextOut (hDC, 1, 2, szUndLine, sizeof szUndLine - 1) ;

               Show (hWnd, hDC, 1, 3, MM_TEXT,      "TEXT (pixels)") ;
               Show (hWnd, hDC, 1, 4, MM_LOMETRIC,  "LOMETRIC (.1 mm)") ;
               Show (hWnd, hDC, 1, 5, MM_HIMETRIC,  "HIMETRIC (.01 mm)") ;
               Show (hWnd, hDC, 1, 6, MM_LOENGLISH, "LOENGLISH (.01 in)") ;
               Show (hWnd, hDC, 1, 7, MM_HIENGLISH, "HIENGLISH (.001 in)") ;
               Show (hWnd, hDC, 1, 8, MM_TWIPS,     "TWIPS (1/1440 in)") ;

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
