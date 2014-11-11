/* KEYLOOK.C -- Displays keyboard & character messages */

#include <windows.h>
#include <stdio.h>

long  FAR PASCAL WndProc (HWND, unsigned, WORD, LONG) ;

RECT  rect ;
short xChar, yChar ;

int PASCAL WinMain (hInstance, hPrevInstance, lpszCmdLine, nCmdShow)
     HANDLE  hInstance, hPrevInstance ;
     LPSTR   lpszCmdLine ;
     int     nCmdShow ;
     {
     static   char szAppName [] = "KeyLook" ;
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
          wndclass.hbrBackground = (HBRUSH) GetStockObject (WHITE_BRUSH) ;
          wndclass.lpszMenuName  = NULL ;
          wndclass.lpszClassName = szAppName ;

          if (!RegisterClass (&wndclass))
               return FALSE ;
          }

     hWnd = CreateWindow (szAppName, "Keyboard Message Looker",
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

void ShowKey (hWnd, iType, szMessage, wParam, lParam)
     HWND        hWnd ;
     int         iType ;
     char        *szMessage ;
     WORD        wParam ;
     LONG        lParam ;
     {
     static char *szFormat [2] = { "%-11s %3d    %c %6u %4d %3s %4s %4s",
                                   "%-11s    %3d %c %6u %4d %3s %4s %4s" } ;
     char        szBuffer [80] ;
     HDC         hDC ;

     ScrollWindow (hWnd, 0, -yChar, &rect, &rect) ;
     hDC = GetDC (hWnd) ;

     TextOut (hDC, xChar, rect.bottom - yChar, szBuffer,
          sprintf (szBuffer, szFormat [iType], szMessage, wParam,
               (BYTE) (iType ? wParam : ' '),
               LOWORD (lParam),
               HIWORD (lParam) & 0x1FF,
               0x20000000 & lParam ? "Yes"  : "No",
               0x40000000 & lParam ? "Down" : "Up",
               0x80000000 & lParam ? "Up"   : "Down")) ;

     ReleaseDC (hWnd, hDC) ;
     ValidateRect (hWnd, NULL) ;
     }

long FAR PASCAL WndProc (hWnd, iMessage, wParam, lParam)
     HWND        hWnd ;
     unsigned    iMessage ;
     WORD        wParam ;
     LONG        lParam ;
     {
     static char szTop [] = "Message     Key Char Repeat Scan ALT Prev Tran" ;
     static char szUnd [] = "_______     ___ ____ ______ ____ ___ ____ ____" ; 
     HDC         hDC ;
     PAINTSTRUCT ps ;
     TEXTMETRIC  tm ;

     switch (iMessage)
          {
          case WM_CREATE:
               hDC = GetDC (hWnd) ;
               GetTextMetrics (hDC, &tm) ;
               xChar = tm.tmAveCharWidth ;
               yChar = tm.tmHeight ;
               ReleaseDC (hWnd, hDC) ;

               rect.top = 3 * yChar / 2 ; 
               break ;

          case WM_SIZE:
               rect.right  = LOWORD (lParam) ;
               rect.bottom = HIWORD (lParam) ;
               UpdateWindow (hWnd) ;
               break ;

          case WM_PAINT:
               InvalidateRect (hWnd, NULL, TRUE) ;
               hDC = BeginPaint (hWnd, &ps) ;
               SetBkMode (hDC, TRANSPARENT) ;
               TextOut (hDC, xChar, yChar / 2, szTop, (sizeof szTop) - 1) ;
               TextOut (hDC, xChar, yChar / 2, szUnd, (sizeof szUnd) - 1) ;
               EndPaint (hWnd, &ps) ;
               break ;

          case WM_KEYDOWN:
               ShowKey (hWnd, 0, "KEYDOWN", wParam, lParam) ;
               break ;

          case WM_KEYUP:
               ShowKey (hWnd, 0, "KEYUP", wParam, lParam) ;
               break ;

          case WM_CHAR:
               ShowKey (hWnd, 1, "CHAR", wParam, lParam) ;
               break ;     

          case WM_DEADCHAR:
               ShowKey (hWnd, 1, "DEADCHAR", wParam, lParam) ;
               break ;     

          case WM_SYSKEYDOWN:
               ShowKey (hWnd, 0, "SYSKEYDOWN", wParam, lParam) ;
               return DefWindowProc (hWnd, iMessage, wParam, lParam) ;

          case WM_SYSKEYUP:
               ShowKey (hWnd, 0, "SYSKEYUP", wParam, lParam) ;
               return DefWindowProc (hWnd, iMessage, wParam, lParam) ;

          case WM_SYSCHAR:
               ShowKey (hWnd, 1, "SYSCHAR", wParam, lParam) ;
               return DefWindowProc (hWnd, iMessage, wParam, lParam) ;

          case WM_SYSDEADCHAR:
               ShowKey (hWnd, 1, "SYSDEADCHAR", wParam, lParam) ;
               return DefWindowProc (hWnd, iMessage, wParam, lParam) ;

          case WM_DESTROY:
               PostQuitMessage (0) ;
               break ;

          default:
               return DefWindowProc (hWnd, iMessage, wParam, lParam) ;
          }
     return 0L ;
     }
