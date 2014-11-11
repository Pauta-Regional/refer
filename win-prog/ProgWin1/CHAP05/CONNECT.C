/* CONNECT.C -- Connect-the-Dots Mouse Demo Program */

#include <windows.h>

#define MAXPOINTS 1000

long FAR PASCAL WndProc (HWND, unsigned, WORD, LONG) ;

int PASCAL WinMain (hInstance, hPrevInstance, lpszCmdLine, nCmdShow)
     HANDLE      hInstance, hPrevInstance ;
     LPSTR       lpszCmdLine ;
     int         nCmdShow ;
     {
     static char szAppName [] = "Connect" ;
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
          wndclass.hIcon         = LoadIcon (NULL, IDI_APPLICATION) ;
          wndclass.hCursor       = LoadCursor (NULL, IDC_ARROW) ;
          wndclass.hbrBackground = GetStockObject (WHITE_BRUSH) ;
          wndclass.lpszMenuName  = NULL ;
          wndclass.lpszClassName = szAppName ;

          if (!RegisterClass (&wndclass))
               return FALSE ;
          }

     hWnd = CreateWindow (szAppName, "Connect-the-Points Mouse Demo",
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
     static POINT points [MAXPOINTS] ;
     static short nCount ;
     HDC          hDC ;
     PAINTSTRUCT  ps ;
     short        i, j ;

     switch (iMessage)
          {
          case WM_LBUTTONDOWN:
               nCount = 0 ;
               InvalidateRect (hWnd, NULL, TRUE) ;
               break ;

          case WM_MOUSEMOVE:
               if (wParam & MK_LBUTTON && nCount < 1000)
                    {
                    points [nCount++] = MAKEPOINT (lParam) ;
                    hDC = GetDC (hWnd) ;
                    SetPixel (hDC, LOWORD (lParam), HIWORD (lParam), 0L) ;
                    ReleaseDC (hWnd, hDC) ;
                    }
               break ;

          case WM_LBUTTONUP:
               InvalidateRect (hWnd, NULL, FALSE) ;
               break ;

          case WM_PAINT:
               hDC = BeginPaint (hWnd, &ps) ;

               for (i = 0 ; i < nCount - 1 ; i++)
                    for (j = i ; j < nCount ; j++)
                         {
                         MoveTo (hDC, points[i].x, points[i].y) ;
                         LineTo (hDC, points[j].x, points[j].y) ;
                         }

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
