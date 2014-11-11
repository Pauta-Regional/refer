/* HEAD.C -- Displays beginning (head) of file */

#include <windows.h>
#include <io.h>
#include <string.h>
#include <direct.h>

#define  MAXPATH     100
#define  MAXREAD    2048

long FAR PASCAL WndProc  (HWND, unsigned, WORD, LONG) ;
long FAR PASCAL ListProc (HWND, unsigned, WORD, LONG) ;

char    sReadBuffer [MAXREAD] ;
FARPROC lpfnOldList ;

int PASCAL WinMain (hInstance, hPrevInstance, lpszCmdLine, nCmdShow)
     HANDLE      hInstance, hPrevInstance ;
     LPSTR       lpszCmdLine ;
     int         nCmdShow ;
     {
     static char szAppName [] = "Head" ;
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
          wndclass.hbrBackground = COLOR_WINDOW + 1 ;
          wndclass.lpszMenuName  = NULL ;
          wndclass.lpszClassName = szAppName ;

          if (!RegisterClass (&wndclass))
               return FALSE ;
          }

     hWnd = CreateWindow (szAppName, "File Head",
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
     HWND            hWnd ;
     unsigned        iMessage ;
     WORD            wParam ;
     LONG            lParam ;
     {
     static BOOL     bValidFile ;
     static char     szFile [16] ;
     static HWND     hWndList, hWndText ;
     static OFSTRUCT ofs ;
     static RECT     rect ;
     char            szBuffer [MAXPATH + 1] ;
     HDC             hDC ;
     int             iHandle, i, iCount ;
     PAINTSTRUCT     ps ;
     TEXTMETRIC      tm ;

     switch (iMessage)
          {
          case WM_CREATE:
               hDC = GetDC (hWnd) ;
               GetTextMetrics (hDC, &tm) ;
               ReleaseDC (hWnd, hDC) ;

               rect.left = 20 * tm.tmAveCharWidth ;
               rect.top  =  3 * tm.tmHeight ;

               hWndList = CreateWindow ("listbox", NULL,
                              WS_CHILDWINDOW | WS_VISIBLE | LBS_STANDARD,
                              tm.tmAveCharWidth, tm.tmHeight * 3,
                              tm.tmAveCharWidth * 13 +
                                   GetSystemMetrics (SM_CXVSCROLL),
                              tm.tmHeight * 10,
                              hWnd, 1,
                              GetWindowWord (hWnd, GWW_HINSTANCE), NULL) ;

               hWndText = CreateWindow ("static", getcwd (szBuffer, MAXPATH),
                              WS_CHILDWINDOW | WS_VISIBLE | SS_LEFT,
                              tm.tmAveCharWidth,           tm.tmHeight,
                              tm.tmAveCharWidth * MAXPATH, tm.tmHeight,
                              hWnd, 2,
                              GetWindowWord (hWnd, GWW_HINSTANCE), NULL) ;

               lpfnOldList = (FARPROC) GetWindowLong (hWndList, GWL_WNDPROC) ;

               SetWindowLong (hWndList, GWL_WNDPROC,
                         (LONG) MakeProcInstance ((FARPROC) ListProc,
                                   GetWindowWord (hWnd, GWW_HINSTANCE))) ;

               SendMessage (hWndList, LB_DIR, 0x37, (LONG) "*.*") ;
               break ;

         case WM_SIZE:
               rect.right  = LOWORD (lParam) ;
               rect.bottom = HIWORD (lParam) ;
               break ;

          case WM_SETFOCUS:
               SetFocus (hWndList) ;
               break ;

          case WM_COMMAND:

               if (wParam == 1 && HIWORD (lParam) == LBN_DBLCLK)
                    {
                    if (LB_ERR == (i = (WORD) SendMessage (hWndList,
                                                  LB_GETCURSEL, 0, 0L)))
                         break ;

                    SendMessage (hWndList, LB_GETTEXT, i,
                                        (LONG) (char far *) szBuffer) ;

                    if (-1 != OpenFile (szBuffer, &ofs, OF_EXIST | OF_READ))
                         {
                         bValidFile = TRUE ;
                         strcpy (szFile, szBuffer) ;
                         getcwd (szBuffer, MAXPATH) ;
                         if (szBuffer [strlen (szBuffer) - 1] != '\\')
                              strcat (szBuffer, "\\") ;
                         SetWindowText (hWndText, strcat (szBuffer, szFile)) ;
                         }
                    else
                         {
                         bValidFile = FALSE ;
                         szBuffer [strlen (szBuffer) - 1] = '\0' ;
                         chdir (szBuffer + 1) ;
                         getcwd (szBuffer, MAXPATH) ;
                         SetWindowText (hWndText, szBuffer) ;
                         SendMessage (hWndList, LB_RESETCONTENT, 0, 0L) ;
                         SendMessage (hWndList, LB_DIR, 0x37, (LONG) "*.*") ;
                         }
                    InvalidateRect (hWnd, NULL, TRUE) ;
                    }
               break ;

          case WM_PAINT:
               hDC = BeginPaint (hWnd, &ps) ;

               if (bValidFile && -1 != (iHandle =
                         OpenFile (szFile, &ofs, OF_REOPEN | OF_READ)))
                    {
                    i = read (iHandle, sReadBuffer, MAXREAD) ;
                    close (iHandle) ;
                    DrawText (hDC, sReadBuffer, i, &rect, DT_WORDBREAK |
                                   DT_EXPANDTABS | DT_NOCLIP | DT_NOPREFIX) ;
                    }
               else
                    bValidFile = FALSE ;

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

long FAR PASCAL ListProc (hWnd, iMessage, wParam, lParam)
     HWND     hWnd ;
     unsigned iMessage ;
     WORD     wParam ;
     LONG     lParam ;
     {
     short    n = GetWindowWord (hWnd, GWW_ID) ;

     if (iMessage == WM_KEYDOWN && wParam == VK_RETURN)

          SendMessage (GetParent (hWnd), WM_COMMAND, 1,
                         MAKELONG (hWnd, LBN_DBLCLK)) ;

     return CallWindowProc (lpfnOldList, hWnd, iMessage, wParam, lParam) ;
     }
