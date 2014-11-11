/* RESOURC2.C -- Icon and Cursor Demonstration Program No. 2 */

#include <windows.h>

long FAR PASCAL WndProc  (HWND, unsigned, WORD, LONG) ;

char   szAppName[] = "Resourc2" ;
HANDLE hInst ;

int PASCAL WinMain (hInstance, hPrevInstance, lpszCmdLine, nCmdShow)
     HANDLE   hInstance, hPrevInstance ;
     LPSTR    lpszCmdLine ;
     int      nCmdShow ;
     {
     HBITMAP  hBitmap ;
     HBRUSH   hBrush ;
     HWND     hWnd ;
     MSG      msg ;
     WNDCLASS wndclass ;

     hBitmap = LoadBitmap (hInstance, szAppName) ;
     hBrush = CreatePatternBrush (hBitmap) ;

     if (!hPrevInstance) 
          {
          wndclass.style         = CS_HREDRAW | CS_VREDRAW ;
          wndclass.lpfnWndProc   = WndProc ;
          wndclass.cbClsExtra    = 0 ;
          wndclass.cbWndExtra    = 0 ;
          wndclass.hInstance     = hInstance ;
          wndclass.hIcon         = LoadIcon (hInstance, szAppName) ;
          wndclass.hCursor       = LoadCursor (hInstance, szAppName) ;
          wndclass.hbrBackground = hBrush ;
          wndclass.lpszMenuName  = NULL ;
          wndclass.lpszClassName = szAppName ;

          if (!RegisterClass (&wndclass))
               return FALSE ;
          }
     hInst = hInstance ;

     hWnd = CreateWindow (szAppName, "Icon and Cursor Demo",
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

     DeleteObject (hBrush) ;       /* clean-up */
     DeleteObject (hBitmap) ;

     return msg.wParam ;
     }

long FAR PASCAL WndProc (hWnd, iMessage, wParam, lParam)
     HWND         hWnd ;
     unsigned     iMessage ;
     WORD         wParam ;
     LONG         lParam ;
     {
     static HICON hIcon ;
     static short xIcon, yIcon, xClient, yClient ;
     HDC          hDC ;
     PAINTSTRUCT  ps ;
     RECT         rect ;
     short        x, y ;

     switch (iMessage)
          {
          case WM_CREATE:
               hIcon = LoadIcon (hInst, szAppName) ;
               xIcon = GetSystemMetrics (SM_CXICON) ;
               yIcon = GetSystemMetrics (SM_CYICON) ;
               break ;

          case WM_SIZE:
               xClient = LOWORD (lParam) ;
               yClient = HIWORD (lParam) ;
               break ;

          case WM_PAINT:
               hDC = BeginPaint (hWnd, &ps) ;

               for (y = yIcon ; y < yClient ; y += 2 * yIcon)
                    for (x = xIcon ; x < xClient ; x += 2 * xIcon)
                         DrawIcon (hDC, x, y, hIcon) ;

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
