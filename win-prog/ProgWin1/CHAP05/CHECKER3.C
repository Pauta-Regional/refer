/* CHECKER3.C -- Mouse Hit-Test Demo Program No. 3 */

#include <windows.h>

#define DIVISIONS 5

long FAR PASCAL WndProc      (HWND, unsigned, WORD, LONG) ;
long FAR PASCAL ChildWndProc (HWND, unsigned, WORD, LONG) ;

char szChildClass [] = "Checker3_Child" ;

int PASCAL WinMain (hInstance, hPrevInstance, lpszCmdLine, nCmdShow)
     HANDLE      hInstance, hPrevInstance ;
     LPSTR       lpszCmdLine ;
     int         nCmdShow ;
     {
     static char szAppName [] = "Checker3" ;
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

          wndclass.lpfnWndProc   = ChildWndProc ;
          wndclass.cbWndExtra    = sizeof (WORD) ;
          wndclass.hIcon         = NULL ;
          wndclass.lpszClassName = szChildClass ;

          if (!RegisterClass (&wndclass))
               return FALSE ;
          }

     hWnd = CreateWindow (szAppName, "Checker3 Mouse Hit-Test Demo",
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
     HWND        hWnd ;
     unsigned    iMessage ;
     WORD        wParam ;
     LONG        lParam ;
     {
     static HWND hChild [DIVISIONS] [DIVISIONS] ;
     short       xBlock, yBlock, x, y ;

     switch (iMessage)
          {
          case WM_CREATE:
               for (x = 0 ; x < DIVISIONS ; x++)
                    for (y = 0 ; y < DIVISIONS ; y++)
                         {
                         hChild [x][y] = CreateWindow (szChildClass, NULL,
                              WS_CHILDWINDOW | WS_VISIBLE,
                              0, 0, 0, 0,
                              hWnd, y << 8 | x,
                              GetWindowWord (hWnd, GWW_HINSTANCE), NULL) ;
                         }
               break ;

          case WM_SIZE:
               xBlock = LOWORD (lParam) / DIVISIONS ;
               yBlock = HIWORD (lParam) / DIVISIONS ;

               for (x = 0 ; x < DIVISIONS ; x++)
                    for (y = 0 ; y < DIVISIONS ; y++)
                         MoveWindow (hChild [x][y],
                              x * xBlock, y * yBlock, xBlock, yBlock, TRUE) ;
               break ;

          case WM_LBUTTONDOWN:
               MessageBeep (0) ;
               break ;

          case WM_DESTROY:
               PostQuitMessage (0) ;
               break ;

          default:
               return DefWindowProc (hWnd, iMessage, wParam, lParam) ;
          }
     return 0L ;
     }

long FAR PASCAL ChildWndProc (hWnd, iMessage, wParam, lParam)
     HWND        hWnd ;
     unsigned    iMessage ;
     WORD        wParam ;
     LONG        lParam ;
     {
     HDC         hDC ;
     PAINTSTRUCT ps ;
     RECT        rect ;

     switch (iMessage)
          {
          case WM_CREATE:
               SetWindowWord (hWnd, 0, 0) ;       /* on/off flag */
               break ;

          case WM_LBUTTONDOWN:
               SetWindowWord (hWnd, 0, 1 ^ GetWindowWord (hWnd, 0)) ;
               InvalidateRect (hWnd, NULL, FALSE) ;
               break ;

          case WM_PAINT:
               hDC = BeginPaint (hWnd, &ps) ;

               GetClientRect (hWnd, &rect) ;
               Rectangle (hDC, 0, 0, rect.right, rect.bottom) ;

               if (GetWindowWord (hWnd, 0))
                    {
                    MoveTo (hDC, 0,          0) ;
                    LineTo (hDC, rect.right, rect.bottom) ;
                    MoveTo (hDC, 0,          rect.bottom) ;
                    LineTo (hDC, rect.right, 0) ;
                    }

               EndPaint (hWnd, &ps) ;
               break ;

          default:
               return DefWindowProc (hWnd, iMessage, wParam, lParam) ;
          }
     return 0L ;
     }
