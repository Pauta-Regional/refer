/* BOUNCE.C -- Bouncing Ball Program */

#include <windows.h>

long FAR PASCAL WndProc (HWND, unsigned, WORD, LONG) ;

int PASCAL WinMain (hInstance, hPrevInstance, lpszCmdLine, nCmdShow)
     HANDLE      hInstance, hPrevInstance ;
     LPSTR       lpszCmdLine ;
     int         nCmdShow ;
     {
     static char szAppName [] = "Bounce" ;
     HWND        hWnd ;
     MSG         msg ;
     WNDCLASS    wndclass ;

     if (!hPrevInstance) 
          {
          wndclass.style         = CS_HREDRAW | CS_VREDRAW ;
          wndclass.lpfnWndProc   = WndProc ;
          wndclass.cbClsExtra    = 0 ;
          wndclass.cbWndExtra    = 0 ;
          wndclass.hInstance     = hInstance ;
          wndclass.hIcon         = NULL ;
          wndclass.hCursor       = LoadCursor (NULL, IDC_ARROW) ;
          wndclass.hbrBackground = GetStockObject (WHITE_BRUSH) ;
          wndclass.lpszMenuName  = NULL ;
          wndclass.lpszClassName = szAppName ;

          if (!RegisterClass (&wndclass))
               return FALSE ;
          }
     hWnd = CreateWindow (szAppName, "Bouncing Ball",
                         WS_OVERLAPPEDWINDOW,
                         CW_USEDEFAULT, 0,
                         CW_USEDEFAULT, 0,
                         NULL, NULL, hInstance, NULL) ;

     if (!SetTimer (hWnd, 1, 50, NULL))
          {
          MessageBox (hWnd, "Too many clocks or timers!",
                         szAppName, MB_ICONEXCLAMATION | MB_OK) ;
          return FALSE ;
          }

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
     HWND          hWnd ;
     unsigned      iMessage ;
     WORD          wParam ;
     LONG          lParam ;
     {
     static HANDLE hBitmap ;
     static short  xClient, yClient, xCenter, yCenter, xTotal, yTotal,
                   xRadius, yRadius, xMove, yMove, xPixel, yPixel ;
     HBRUSH        hBrush ;
     HDC           hDC, hMemDC ;
     short         nScale ;

     switch (iMessage)
          {
          case WM_CREATE:
               hDC = GetDC (hWnd) ;
               xPixel = GetDeviceCaps (hDC, ASPECTX) ;
               yPixel = GetDeviceCaps (hDC, ASPECTY) ;
               ReleaseDC (hWnd, hDC) ;
               break ;

          case WM_SIZE:
               xCenter = (xClient = LOWORD (lParam)) / 2 ;
               yCenter = (yClient = HIWORD (lParam)) / 2 ;

               nScale = min (xClient * xPixel, yClient * yPixel) / 16 ;

               xRadius = nScale / xPixel ;
               yRadius = nScale / yPixel ;

               xMove = max (1, xRadius / 4) ;
               yMove = max (1, yRadius / 4) ;

               xTotal = 2 * (xRadius + xMove) ;
               yTotal = 2 * (yRadius + yMove) ;

               if (hBitmap)
                    DeleteObject (hBitmap) ;

               hDC = GetDC (hWnd) ;
               hMemDC = CreateCompatibleDC (hDC) ;
               hBitmap = CreateCompatibleBitmap (hDC, xTotal, yTotal) ;
               ReleaseDC (hWnd, hDC) ;

               SelectObject (hMemDC, hBitmap) ;
               Rectangle (hMemDC, -1, -1, xTotal + 1, yTotal + 1) ;

               hBrush = CreateHatchBrush (HS_DIAGCROSS, 0L) ;
               SelectObject (hMemDC, hBrush) ;
               SetBkColor (hMemDC, RGB (255, 0, 255)) ;
               Ellipse (hMemDC, xMove, yMove, xTotal - xMove, yTotal - yMove) ;

               DeleteDC (hMemDC) ;
               DeleteObject (hBrush) ;
               break ;

          case WM_TIMER:
               if (!hBitmap)
                    break ;

               hDC = GetDC (hWnd) ;
               hMemDC = CreateCompatibleDC (hDC) ;
               SelectObject (hMemDC, hBitmap) ;

               BitBlt (hDC, xCenter - xTotal / 2,
                            yCenter - yTotal / 2, xTotal, yTotal,
                       hMemDC, 0, 0, SRCCOPY) ;

               ReleaseDC (hWnd, hDC) ;
               DeleteDC (hMemDC) ;

               xCenter += xMove ;
               yCenter += yMove ;

               if ((xCenter + xRadius >= xClient) ||
                   (xCenter - xRadius <= 0))
                         xMove = -xMove ;

               if ((yCenter + yRadius >= yClient) ||
                   (yCenter - yRadius <= 0))
                         yMove = -yMove ;
               break ;

          case WM_DESTROY:
               if (hBitmap)
                    DeleteObject (hBitmap) ;
               KillTimer (hWnd, 1) ;
               PostQuitMessage (0) ;
               break ;

          default:
               return DefWindowProc (hWnd, iMessage, wParam, lParam) ;
          }
     return 0L ;
     }
