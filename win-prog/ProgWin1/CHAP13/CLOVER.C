/* CLOVER.C -- Clover Drawing Program using Regions */

#include <windows.h>
#include <math.h>
#define TWO_PI (2.0 * 3.14159)

long FAR PASCAL WndProc (HWND, unsigned, WORD, LONG) ;

int PASCAL WinMain (hInstance, hPrevInstance, lpszCmdLine, nCmdShow)
     HANDLE      hInstance, hPrevInstance ;
     LPSTR       lpszCmdLine ;
     int         nCmdShow ;
     {
     static char szAppName [] = "Clover" ;
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

     hWnd = CreateWindow (szAppName, "Draw a Clover",
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
     static HRGN  hRgnClip ;
     static short xClient, yClient ;
     double       fAngle, fRadius ;
     HCURSOR      hCursor ;
     HDC          hDC ;
     HRGN         hRgnTemp [6] ;
     PAINTSTRUCT  ps ;
     short        i ;

     switch (iMessage)
          {
          case WM_SIZE:
               xClient = LOWORD (lParam) ;
               yClient = HIWORD (lParam) ;

               hCursor = SetCursor (LoadCursor (NULL, IDC_WAIT)) ;
               ShowCursor (TRUE) ;

               if (hRgnClip)
                    DeleteObject (hRgnClip) ;

               hRgnTemp [0] = CreateEllipticRgn (0, yClient / 3,
                                        xClient / 2, 2 * yClient / 3) ;
               hRgnTemp [1] = CreateEllipticRgn (xClient / 2, yClient / 3,
                                        xClient, 2 * yClient / 3) ;
               hRgnTemp [2] = CreateEllipticRgn (xClient / 3, 0,
                                        2 * xClient / 3, yClient / 2) ;
               hRgnTemp [3] = CreateEllipticRgn (xClient / 3, yClient / 2,
                                        2 * xClient / 3, yClient) ;
               hRgnTemp [4] = CreateRectRgn (0, 0, 1, 1) ;
               hRgnTemp [5] = CreateRectRgn (0, 0, 1, 1) ;
               hRgnClip     = CreateRectRgn (0, 0, 1, 1) ;

               CombineRgn (hRgnTemp [4], hRgnTemp [0], hRgnTemp [1], RGN_OR) ;
               CombineRgn (hRgnTemp [5], hRgnTemp [2], hRgnTemp [3], RGN_OR) ;
               CombineRgn (hRgnClip,     hRgnTemp [4], hRgnTemp [5], RGN_XOR) ;

               for (i = 0 ; i < 6 ; i++)
                    DeleteObject (hRgnTemp [i]) ;

               SetCursor (hCursor) ;
               ShowCursor (FALSE) ;
               break ;

          case WM_PAINT:
               hDC = BeginPaint (hWnd, &ps) ;

               SetViewportOrg (hDC, xClient / 2, yClient / 2) ;
               SelectClipRgn (hDC, hRgnClip) ;

               fRadius = hypot (xClient / 2.0, yClient / 2.0) ;

               for (fAngle = 0.0 ; fAngle < TWO_PI ; fAngle += TWO_PI / 360)
                    {
                    MoveTo (hDC, 0, 0) ;
                    LineTo (hDC, (short) ( fRadius * cos (fAngle) + 0.5),
                                 (short) (-fRadius * sin (fAngle) + 0.5)) ;
                    }
               EndPaint (hWnd, &ps) ;
               break ;

          case WM_DESTROY:
               DeleteObject (hRgnClip) ;
               PostQuitMessage (0) ;
               break ;

          default:
               return DefWindowProc (hWnd, iMessage, wParam, lParam) ;
          }
     return 0L ;
     }
