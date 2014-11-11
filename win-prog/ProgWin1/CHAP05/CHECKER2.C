/* CHECKER2.C -- Mouse Hit-Test Demo Program No. 2 */

#include <windows.h>

#define DIVISIONS 5

long FAR PASCAL WndProc (HWND, unsigned, WORD, LONG) ;

int PASCAL WinMain (hInstance, hPrevInstance, lpszCmdLine, nCmdShow)
     HANDLE      hInstance, hPrevInstance ;
     LPSTR       lpszCmdLine ;
     int         nCmdShow ;
     {
     static char szAppName [] = "Checker2" ;
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

     hWnd = CreateWindow (szAppName, "Checker2 Mouse Hit-Test Demo",
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
     static BOOL  bState [DIVISIONS] [DIVISIONS] ;
     static short xBlock, yBlock ;
     HDC          hDC ;
     PAINTSTRUCT  ps ;
     POINT        point ;
     RECT         rect ;
     short        x, y ;

     switch (iMessage)
          {
          case WM_SIZE:
               xBlock = LOWORD (lParam) / DIVISIONS ;
               yBlock = HIWORD (lParam) / DIVISIONS ;
               break ;

          case WM_SETFOCUS:
               ShowCursor (TRUE) ;
               break ;

          case WM_KILLFOCUS:
               ShowCursor (FALSE) ;
               break ;

          case WM_KEYDOWN:
               GetCursorPos (&point) ;
               ScreenToClient (hWnd, &point) ;

               x = max (0, min (DIVISIONS - 1, point.x / xBlock)) ;
               y = max (0, min (DIVISIONS - 1, point.y / yBlock)) ;
  
               switch (wParam)
                    {
                    case VK_UP:
                         y -- ;
                         break ;

                    case VK_DOWN:
                         y ++ ;
                         break ;

                    case VK_LEFT:
                         x -- ;
                         break ;

                    case VK_RIGHT:
                         x ++ ;
                         break ;

                    case VK_HOME:
                         x = y = 0 ;
                         break ;

                    case VK_END:
                         x = y = DIVISIONS - 1 ;
                         break ;

                    case VK_RETURN:
                    case VK_SPACE:
                         SendMessage (hWnd, WM_LBUTTONDOWN, MK_LBUTTON,
                                   MAKELONG (x * xBlock, y * yBlock)) ;
                         break ;
                    }
               x = (x + DIVISIONS) % DIVISIONS ;
               y = (y + DIVISIONS) % DIVISIONS ;

               point.x = x * xBlock + xBlock / 2 ;
               point.y = y * yBlock + yBlock / 2 ;

               ClientToScreen (hWnd, &point) ;
               SetCursorPos (point.x, point.y) ;
               break ;

          case WM_LBUTTONDOWN:
               x = LOWORD (lParam) / xBlock ;
               y = HIWORD (lParam) / yBlock ;

               if (x < DIVISIONS && y < DIVISIONS)
                    {
                    bState [x][y] ^= 1 ;

                    rect.left   = x * xBlock ;
                    rect.top    = y * yBlock ;
                    rect.right  = (x + 1) * xBlock ;
                    rect.bottom = (y + 1) * yBlock ;

                    InvalidateRect (hWnd, &rect, FALSE) ;
                    }
               else
                    MessageBeep (0) ;
               break ;

          case WM_PAINT:
               hDC = BeginPaint (hWnd, &ps) ;

               for (x = 0 ; x < DIVISIONS ; x++)
                    for (y = 0 ; y < DIVISIONS ; y++)
                         {
                         Rectangle (hDC, x * xBlock, y * yBlock,
                                   (x + 1) * xBlock, (y + 1) * yBlock) ;

                         if (bState [x][y])
                              {
                              MoveTo (hDC,  x    * xBlock,  y    * yBlock) ;
                              LineTo (hDC, (x+1) * xBlock, (y+1) * yBlock) ;
                              MoveTo (hDC,  x    * xBlock, (y+1) * yBlock) ;
                              LineTo (hDC, (x+1) * xBlock,  y    * yBlock) ;
                              }
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
