/* MENUDEMO.C -- Menu Demonstration */

#include <windows.h>
#include "menudemo.h"

long FAR PASCAL WndProc (HWND, unsigned, WORD, LONG) ;

char szAppName [] = "MenuDemo" ;

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
          wndclass.style         = CS_HREDRAW | CS_VREDRAW ;
          wndclass.lpfnWndProc   = WndProc ;
          wndclass.cbClsExtra    = 0 ;
          wndclass.cbWndExtra    = 0 ;
          wndclass.hInstance     = hInstance ;
          wndclass.hIcon         = LoadIcon (NULL, IDI_APPLICATION) ;
          wndclass.hCursor       = LoadCursor (NULL, IDC_ARROW) ;
          wndclass.hbrBackground = GetStockObject (WHITE_BRUSH) ;
          wndclass.lpszMenuName  = szAppName ;
          wndclass.lpszClassName = szAppName ;

          if (!RegisterClass (&wndclass))
               return FALSE ;
          }

     hWnd = CreateWindow (szAppName, "Menu Demonstration",
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
     static int  wColorID [6] = { WHITE_BRUSH, LTGRAY_BRUSH, GRAY_BRUSH,
                                   DKGRAY_BRUSH, BLACK_BRUSH, HOLLOW_BRUSH } ;
     static WORD wSelection = IDM_WHITE ;
     HMENU       hMenu ;

     switch (iMessage)
          {
          case WM_COMMAND:

               switch (wParam)
                    {
                    case IDM_NEW:
                    case IDM_OPEN:
                    case IDM_SAVE:
                    case IDM_SAVEAS:

                    case IDM_UNDO:
                    case IDM_CUT:
                    case IDM_COPY:
                    case IDM_PASTE:
                    case IDM_CLEAR:

                         MessageBeep (0) ;
                         break ;

                    case IDM_WHITE:          /* Note: Logic below        */
                    case IDM_LTGRAY:         /*   assumes that IDM_WHITE */
                    case IDM_GRAY:           /*   through IDM_HOLLOW are */
                    case IDM_DKGRAY:         /*   consecutive numbers in */
                    case IDM_BLACK:          /*   the order shown here.  */
                    case IDM_HOLLOW:

                         hMenu = GetMenu (hWnd) ;
                         CheckMenuItem (hMenu, wSelection, MF_UNCHECKED) ;
                         wSelection = wParam ;
                         CheckMenuItem (hMenu, wSelection, MF_CHECKED) ;
     
                         SetClassWord (hWnd, GCW_HBRBACKGROUND,
                              GetStockObject (wColorID [wParam - IDM_WHITE])) ;

                         InvalidateRect (hWnd, NULL, TRUE) ;
                         break ;

                    case IDM_ABOUT:

                         MessageBox (hWnd, "Menu Demonstration Program.",
                              szAppName, MB_ICONASTERISK | MB_OK) ;
                         break ;

                    case IDM_EXIT:

                         SendMessage (hWnd, WM_CLOSE, 0, 0L) ;
                         break ;

                    case IDM_HELP:

                         MessageBox (hWnd, "Help not yet implemented.",
                              szAppName, MB_ICONASTERISK | MB_OK) ;
                         break ;

                    default:
                         break ;
                    }
               break ;

          case WM_DESTROY :
               PostQuitMessage (0) ;
               break ;

          default :
               return DefWindowProc (hWnd, iMessage, wParam, lParam) ;
          }
     return 0L ;
     }
