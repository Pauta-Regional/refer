/* POEPOEM.C -- Demonstrates User-Defined Resource */

#include <windows.h>
#include "poepoem.h"

long FAR PASCAL WndProc  (HWND, unsigned, WORD, LONG) ;

char   szAppName [10] ;
char   szCaption [35] ;
HANDLE hInst ;

int PASCAL WinMain (hInstance, hPrevInstance, lpszCmdLine, nCmdShow)
     HANDLE   hInstance, hPrevInstance ;
     LPSTR    lpszCmdLine ;
     int      nCmdShow ;
     {
     HWND     hWnd ;
     MSG      msg ;
     WNDCLASS wndclass ;

     if (!hPrevInstance) 
          {
          LoadString (hInstance, IDS_APPNAME, szAppName, sizeof szAppName) ;
          LoadString (hInstance, IDS_CAPTION, szCaption, sizeof szCaption) ;

          wndclass.style         = CS_HREDRAW | CS_VREDRAW ;
          wndclass.lpfnWndProc   = WndProc ;
          wndclass.cbClsExtra    = 0 ;
          wndclass.cbWndExtra    = 0 ;
          wndclass.hInstance     = hInstance ;
          wndclass.hIcon         = LoadIcon (hInstance, szAppName) ;
          wndclass.hCursor       = LoadCursor (NULL, IDC_ARROW) ;
          wndclass.hbrBackground = GetStockObject (WHITE_BRUSH) ;
          wndclass.lpszMenuName  = NULL ;
          wndclass.lpszClassName = szAppName ;

          if (!RegisterClass (&wndclass))
               return FALSE ;
          }
     else
          {
          GetInstanceData (hPrevInstance, szAppName, sizeof szAppName) ;
          GetInstanceData (hPrevInstance, szCaption, sizeof szCaption) ;
          }

     hInst = hInstance ;

     hWnd = CreateWindow (szAppName, szCaption,
                         WS_OVERLAPPEDWINDOW | WS_CLIPCHILDREN,
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
     HWND          hWnd ;
     unsigned      iMessage ;
     WORD          wParam ;
     LONG          lParam ;
     {
     static HANDLE hResource ;
     static HWND   hScroll ;
     static short  nPosition, xChar, yChar, yClient, nNumLines, xScroll ;
     char          szPoemRes [15] ;
     char far      *lpText ;
     HDC           hDC ;
     PAINTSTRUCT   ps ;
     RECT          rect ;
     TEXTMETRIC    tm ;

     switch (iMessage)
          {
          case WM_CREATE:

               hDC = GetDC (hWnd) ;
               GetTextMetrics (hDC, &tm) ;
               xChar = tm.tmAveCharWidth ;
               yChar = tm.tmHeight + tm.tmExternalLeading ;
               ReleaseDC (hWnd, hDC) ;

               xScroll = GetSystemMetrics (SM_CXVSCROLL) ;

               hScroll = CreateWindow ("scrollbar", NULL,
                              WS_CHILD | WS_VISIBLE | SBS_VERT,
                              0, 0, 0, 0,
                              hWnd, 1, hInst, NULL) ;

               LoadString (hInst, IDS_POEMRES, szPoemRes, sizeof szPoemRes) ;
               hResource = LoadResource (hInst, 
                           FindResource (hInst, szPoemRes, "TEXT")) ;

               lpText = LockResource (hResource) ;

               nNumLines = 0 ;
               while (*lpText != '\0' && *lpText != '\x1A')
                    {
                    if (*lpText == '\n')
                         nNumLines ++ ;
                    lpText = AnsiNext (lpText) ;
                    }
               *lpText = '\0' ;

               GlobalUnlock (hResource) ;

               SetScrollRange (hScroll, SB_CTL, 0, nNumLines, FALSE) ;
               SetScrollPos   (hScroll, SB_CTL, 0, FALSE) ;
               break ;

          case WM_SIZE:
               MoveWindow (hScroll, LOWORD (lParam) - xScroll, 0,
                    xScroll, yClient = HIWORD (lParam), TRUE) ;
               SetFocus (hWnd) ;
               break ; 

          case WM_SETFOCUS:
               SetFocus (hScroll) ;
               break ;

          case WM_VSCROLL:
               switch (wParam)
                    {
                    case SB_TOP:
                         nPosition = 0 ;
                         break ;
                    case SB_BOTTOM:
                         nPosition = nNumLines ;
                         break ;
                    case SB_LINEUP:
                         nPosition -= 1 ;
                         break ;
                    case SB_LINEDOWN:
                         nPosition += 1 ;
                         break ;
                    case SB_PAGEUP:
                         nPosition -= yClient / yChar ;
                         break ;
                    case SB_PAGEDOWN:
                         nPosition += yClient / yChar ;
                         break ;
                    case SB_THUMBPOSITION:
                         nPosition = LOWORD (lParam) ;
                         break ;
                    }
               nPosition = max (0, min (nPosition, nNumLines)) ;
               if (nPosition != GetScrollPos (hScroll, SB_CTL))
                    {
                    SetScrollPos (hScroll, SB_CTL, nPosition, TRUE) ;
                    InvalidateRect (hWnd, NULL, TRUE) ;
                    }
               break ;

          case WM_PAINT:
               hDC = BeginPaint (hWnd, &ps) ;

               lpText = LockResource (hResource) ;

               GetClientRect (hWnd, &rect) ;
               rect.left += xChar ;
               rect.top  += yChar * (1 - nPosition) ;
               DrawText (hDC, lpText, -1, &rect, DT_EXTERNALLEADING) ;

               GlobalUnlock (hResource) ;

               EndPaint (hWnd, &ps) ;
               break ;

          case WM_DESTROY:
               FreeResource (hResource) ;
               PostQuitMessage (0) ;
               break ;

          default:
               return DefWindowProc (hWnd, iMessage, wParam, lParam) ;
          }
     return 0L ;
     }
